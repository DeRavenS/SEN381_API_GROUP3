using SEN381_API_Group3.shared.models;
using SEN381_API_GROUP3.Database;
using SEN381_API_GROUP3.shared.models;
using System.Data.SqlClient;
using System.Drawing.Drawing2D;
using System.Security.Authentication.ExtendedProtection;

namespace SEN381_API_GROUP3.Services
{
    public class PackageService
    {
        public Package getPackageByID(int id)
        {
            Package package = new Package("0",DateTime.Now, DateTime.Now, new List<PackageTreatmentCoverage>());
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();
            string query = "dbo.GetPackageByID";
            SqlCommand command = new SqlCommand(query, scon);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@id",id);
            SqlDataReader reader = command.ExecuteReader();

            //Dictionaries for building objects

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    if (!reader.IsDBNull(3))
                    {
                        package.TreatmentCoverages.Add(new PackageTreatmentCoverage(
                            new Treatment(reader.GetString(3),reader.GetString(4),reader.GetString(5),new List<MedicalServiceProviderTreatment>()),
                            new TreatmentCoverage(reader.GetInt32(6),reader.GetString(7),reader.GetInt32(8), reader.GetInt32(9), reader.GetInt32(10)))
                            );
                    }
                        package.PackageID = reader.GetInt32(0).ToString();
                        package.PackageStartDate = reader.GetDateTime(1);
                        package.PackageEndDate = reader.GetDateTime(2);
                }
            }
            else
            {
                throw new HttpRequestException();
            }
            return package;
        }

        public List<Package> getPackageList(int page, int size)
        {
            int offset = (page - 1) * size;
            List<Package> packages = new List<Package>();
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();

            string query = "dbo.GetAllPackages";

            SqlCommand command = new SqlCommand(query, scon);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@offset", offset);
            command.Parameters.AddWithValue("@size", size);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Treatment treat = null;
                    TreatmentCoverage tc = null;
                    if (!reader.IsDBNull(3))treat = new Treatment(reader.GetString(3), reader.GetString(4), reader.GetString(5), new List<MedicalServiceProviderTreatment>());
                    if(!reader.IsDBNull(6))tc = new TreatmentCoverage(reader.GetInt32(6), reader.GetString(7), reader.GetInt32(8), reader.GetInt32(9), reader.GetInt32(8));

                    if ( !packages.Exists((x) => x.PackageID==reader.GetInt32(0).ToString() ))
                    {


                        packages.Add(
                            new Package(reader.GetInt32(0).ToString(),reader.GetDateTime(1),reader.GetDateTime(2), 
                            treat!=null||tc!=null? new List<PackageTreatmentCoverage>() { new PackageTreatmentCoverage(treat,tc)}:new List<PackageTreatmentCoverage>()
                        ));
                    }
                    else if(treat!=null&&tc!=null)
                    {
                        packages[packages.FindIndex((x)=>x.PackageID == reader.GetInt32(0).ToString())].TreatmentCoverages.Add(
                            new PackageTreatmentCoverage(treat,tc));
                    }
                }
            }


            return packages;
        }

        public Package addPackage(Package package)
        {
            string query = $@"dbo.InsertPackage";

            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();


            SqlCommand insertCommand = new SqlCommand(query, scon);
            insertCommand.CommandType = System.Data.CommandType.StoredProcedure;

            insertCommand.Parameters.AddWithValue("@start", package.PackageStartDate !=null? package.PackageStartDate:null);
            insertCommand.Parameters.AddWithValue("@end", package.PackageEndDate != null ? package.PackageEndDate:null);

            //Finding new PackageID
            int packID = int.Parse(insertCommand.ExecuteScalar().ToString());

            //PackagePolicyTreatmentCoverage
            foreach (PackageTreatmentCoverage item in package.TreatmentCoverages)
            {
                query = "dbo.InsertPackagePolicyTreatmentCoverage";
                insertCommand.Parameters.Clear();
                insertCommand.CommandText = query;
                insertCommand.Parameters.AddWithValue("@treatID",item.Treatment.TreatmentID);
                insertCommand.Parameters.AddWithValue("@covID", item.Coverage.CoverageID);
                insertCommand.Parameters.AddWithValue("@packID", packID);
                insertCommand.ExecuteNonQuery();
            }
            scon.Close();
            return package;
        }

        public Package updatePackage(Package package)
        {
            string query = $@"dbo.UpdatePackage";

            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();


            SqlCommand insertCommand = new SqlCommand(query, scon);
            insertCommand.CommandType = System.Data.CommandType.StoredProcedure;

            insertCommand.Parameters.AddWithValue("@start", package.PackageStartDate);
            insertCommand.Parameters.AddWithValue("@end", package.PackageEndDate);
            insertCommand.Parameters.AddWithValue("@id", package.PackageID);
            insertCommand.ExecuteNonQuery();

            foreach (PackageTreatmentCoverage item in package.TreatmentCoverages)
            {
                insertCommand.Parameters.Clear();
                query = "dbo.UpdatePackagePolicyTreatmentCoverage";
                insertCommand.CommandText = query;
                insertCommand.Parameters.AddWithValue("@treatID", item.Treatment.TreatmentID);
                insertCommand.Parameters.AddWithValue("@covID", item.Coverage.CoverageID);
                insertCommand.Parameters.AddWithValue("@packID", package.PackageID);
                insertCommand.ExecuteNonQuery();
            }

            scon.Close();
            return package;
        }
        public Package deletePackage(int id)
        {
            string query = "dbo.DeletePackage";
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();

            SqlCommand com = new SqlCommand(query, scon);
            com.CommandType = System.Data.CommandType.StoredProcedure;

            com.Parameters.AddWithValue("@id", id);
            com.ExecuteNonQuery();

            scon.Close();
            return new Package();
        }
    }
}
