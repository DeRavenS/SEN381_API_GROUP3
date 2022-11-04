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
            Package package = new Package();
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();
            string query = "SELECT DISTINCT" +
               "               P.PackageID," +
               "               T.TreatmentID,T.TreatmentName,T.TreatmentDescription, " +
               "               C.CoverageID, C.CoverageDESCRIPTION, C.NumberOfGeneralVisits, C.NumberOfSpecialistsVisits, C.TotalCoverageUser" +
               "           FROM " +
               "               Package P " +
               "           LEFT JOIN " +
               "               PackagePolicyTreatmentCoverage PPTC ON P.PackageID = PPTC.PackageID" +
               "           LEFT JOIN " +
               "               PolicyTreatmentCoverage PTC ON PTC.PolicyTypeID = PPTC.PolicyTypeID" +
               "           LEFT JOIN" +
               "               Treatment T on T.TreatmentID=PTC.TreatmentID" +
               "           LEFT JOIN" +
               "               Coverage C on C.CoverageID=PTC.CoverageID" +
               "           WHERE P.PackageID=@id";
            SqlCommand command = new SqlCommand(query, scon);
            command.Parameters.AddWithValue("@id",id);
            SqlDataReader reader = command.ExecuteReader();

            //Dictionaries for building objects
            List<PackageTreatmentCoverage> lPTC = new List<PackageTreatmentCoverage>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    if (!lPTC.Exists((x)=>x.Treatment.TreatmentID == reader.GetString(1) && x.Coverage.CoverageID == reader.GetInt32(4)))
                    {
                        lPTC.Add(new PackageTreatmentCoverage(
                            new Treatment(reader.GetString(1),reader.GetString(2),reader.GetString(3),new List<MedicalServiceProviderTreatment>()),
                            new TreatmentCoverage(reader.GetInt32(4),reader.GetString(5),reader.GetInt32(6), reader.GetInt32(7), reader.GetInt32(8)))
                            );
                    }
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

            string query = "SELECT DISTINCT" +
               "               P.PackageID,P.PackageStartDate,P.PackageEndDate," +
               "               T.TreatmentID,T.TreatmentName,T.TreatmentDescription, " +
               "               C.CoverageID, C.CoverageDESCRIPTION, C.NumberOfGeneralVisits, C.NumberOfSpecialistsVisits, C.TotalCoverageUser" +
               "           FROM " +
               "               Package P " +
               "           INNER JOIN " +
               "               PackagePolicyTreatmentCoverage PPTC ON P.PackageID = PPTC.PackageID" +
               "           INNER JOIN " +
               "               PolicyTreatmentCoverage PTC ON PTC.PolicyTypeID = PPTC.PolicyTypeID" +
               "           INNER JOIN" +
               "               Treatment T on T.TreatmentID=PTC.TreatmentID" +
               "           INNER JOIN" +
               "               Coverage C on C.CoverageID=PTC.CoverageID" +
               "           WHERE P.PackageID IN " +
               "               (SELECT PackageID FROM Package ORDER BY PackageID OFFSET @offset ROWS FETCH NEXT @size ROWS ONLY)";

            SqlCommand command = new SqlCommand(query, scon);

            command.Parameters.AddWithValue("@offset", offset);
            command.Parameters.AddWithValue("@size", size);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    if ( !packages.Exists((x) => x.PackageID==reader.GetInt32(0).ToString() ))
                    {
                        packages.Add(new Package(reader.GetInt32(0).ToString(),reader.GetDateTime(1),reader.GetDateTime(2), 
                            new List<PackageTreatmentCoverage>() { new PackageTreatmentCoverage(
                                new Treatment(reader.GetString(3),reader.GetString(4),reader.GetString(5),new List<MedicalServiceProviderTreatment>()),
                                new TreatmentCoverage(reader.GetInt32(6),reader.GetString(7),reader.GetInt32(8), reader.GetInt32(9), reader.GetInt32(8))
                                )
                            }
                        ));
                    }
                    else
                    {
                        packages[packages.FindIndex((x)=>x.PackageID == reader.GetInt32(0).ToString())].TreatmentCoverages.Add(new PackageTreatmentCoverage(
                                new Treatment(reader.GetString(3), reader.GetString(4), reader.GetString(5), new List<MedicalServiceProviderTreatment>()),
                                new TreatmentCoverage(reader.GetInt32(6), reader.GetString(7), reader.GetInt32(8), reader.GetInt32(9), reader.GetInt32(10))
                                ));
                    }
                }
            }


            return packages;
        }
        public Package addPackage(Package package)
        {
            string query = $@"INSERT INTO Package (PackageStartDate,PackageEndDate)" +
                            " VALUES(@start, @end)";

            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();


            SqlCommand insertCommand = new SqlCommand(query, scon);
            insertCommand.Parameters.AddWithValue("@start", package.PackageStartDate !=null? package.PackageStartDate:null);
            insertCommand.Parameters.AddWithValue("@end", package.PackageEndDate != null ? package.PackageEndDate:null);

            insertCommand.ExecuteNonQuery();

            //Finding new PackageID
            int packID = 0;
            query = "SELECT TOP 1 PackageID FROM  Package ORDER BY PackageID DESC";
            insertCommand.Parameters.Clear();
            insertCommand.CommandText = query;
            SqlDataReader reader = insertCommand.ExecuteReader();
            while (reader.Read())
            {
                packID = reader.GetInt32(0);
            }
            reader.Close();

            int polType=0;
            foreach (PackageTreatmentCoverage item in package.TreatmentCoverages)
            {
                //PolicyTreatmentCoverage Table
                query = "INSERT INTO PolicyTreatmentCoverage(TreatmentID,CoverageID) VALUES(@treatID,@covID)";
                insertCommand.Parameters.Clear();
                insertCommand.CommandText = query;
                insertCommand.Parameters.AddWithValue("@treatID",item.Treatment.TreatmentID);
                insertCommand.Parameters.AddWithValue("@covID", item.Coverage.CoverageID);
                insertCommand.ExecuteNonQuery();

                //Finding new PolicyTypeID
                query = "SELECT TOP 1 PolicyTypeID FROM  PolicyTreatmentCoverage ORDER BY PolicyTypeID DESC";
                insertCommand.Parameters.Clear();
                insertCommand.CommandText = query;
                reader = insertCommand.ExecuteReader();
                while (reader.Read())
                {
                    polType = reader.GetInt32(0);
                }
                reader.Close();
                //PackagePolicyTreatmentCoverage Table
                query = "INSERT INTO PackagePolicyTreatmentCoverage(PolicyTypeID,PackageID) VALUES(@polTypeID,@packID)";
                insertCommand.Parameters.Clear();
                insertCommand.CommandText = query;
                insertCommand.Parameters.AddWithValue("@polTypeID", polType);
                insertCommand.Parameters.AddWithValue("@packID", packID);
                insertCommand.ExecuteNonQuery();
            }
            scon.Close();
            return package;
        }
        public Package updatePackage(Package package)
        {
            string query = $@"Update Package set " +
                "             PackageStartDate = @start, PackageEndDate = @end WHERE PackageID= @id";


            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();


            SqlCommand insertCommand = new SqlCommand(query, scon);
            insertCommand.Parameters.AddWithValue("@start", package.PackageStartDate);
            insertCommand.Parameters.AddWithValue("@end", package.PackageEndDate);
            insertCommand.Parameters.AddWithValue("@id", package.PackageID);
            insertCommand.ExecuteNonQuery();

            insertCommand.Parameters.Clear();
            query = "DELETE FROM PackagePolicyTreatmentCoverage WHERE PacakgeID = @id";
            insertCommand.Parameters.AddWithValue("@id", package.PackageID);
            insertCommand.CommandText = query;
            insertCommand.ExecuteNonQuery();

            int polType = 0;
            foreach (PackageTreatmentCoverage item in package.TreatmentCoverages)
            {
                insertCommand.Parameters.Clear();
                query = "INSERT INTO PolicyTreatmentCoverage(TreatmentID,CoverageID) VALUES(@treatID,@covID)";
                insertCommand.Parameters.AddWithValue("@treatID", item.Treatment.TreatmentID);
                insertCommand.Parameters.AddWithValue("@covID", item.Coverage.CoverageID);
                insertCommand.CommandText = query;
                insertCommand.ExecuteNonQuery();

                //Finding new PolicyTypeID
                query = "SELECT TOP 1 PolicyTypeID FROM  PolicyTreatmentCoverage ORDER BY PolicyTypeID DESC";
                insertCommand.Parameters.Clear();
                insertCommand.CommandText = query;
                SqlDataReader reader = insertCommand.ExecuteReader();
                while (reader.Read())
                {
                    polType = reader.GetInt32(0);
                }
                //PackagePolicyTreatmentCoverage Table
                query = "INSERT INTO PackagePolicyTreatmentCoverage(PolicyTypeID,PackageID) VALUES(@polTypeID,@packID)";
                insertCommand.Parameters.Clear();
                insertCommand.CommandText = query;
                insertCommand.Parameters.AddWithValue("@polTypeID", polType);
                insertCommand.Parameters.AddWithValue("@packID", package.PackageID);
                insertCommand.ExecuteNonQuery();
            }

            scon.Close();
            return package;
        }
        public Package deletePackage(int id)
        {
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();
            SqlCommand com = new SqlCommand("", scon);
            string query = "";
            query = "DELETE FROM PackagePolicyTreatmentCoverage WHERE PackageID=@id";
            com.Parameters.AddWithValue("@id", id);
            com.CommandText = query;
            com.ExecuteNonQuery();

            com.Parameters.Clear();
            query = "DELETE FROM PolicyPackage WHERE PacakgeID=@id";
            com.Parameters.AddWithValue("@id", id);
            com.CommandText = query;
            com.ExecuteNonQuery();

            com.Parameters.Clear();
            query = "DELETE FROM Package WHERE PacakgeID=@id";
            com.Parameters.AddWithValue("@id", id);
            com.CommandText = query;
            com.ExecuteNonQuery();

            scon.Close();
            return new Package();
        }
    }
}
