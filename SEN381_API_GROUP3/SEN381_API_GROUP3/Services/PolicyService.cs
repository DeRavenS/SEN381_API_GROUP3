using SEN381_API_Group3.shared.models;
using SEN381_API_GROUP3.shared.models;
using SEN381_API_GROUP3.Database;
using SEN381_API_GROUP3.shared.requests;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SEN381_API_GROUP3.Services
{
    public class PolicyService
    {
        public static List<Policy> getPolicyList(int size, int page)
        {
            int offset = (page - 1) * size;
            List<Policy> policies = new List<Policy>();
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();
            SqlCommand command = new SqlCommand("dbo.PolicyList", scon);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@offset", offset);
            command.Parameters.AddWithValue("@size", size);

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                int polIndex = policies.FindIndex((x) => x.PolicyId==reader.GetInt32(0).ToString());
                //adding new policy to list if it is not there already
                if (polIndex == -1) policies.Add(
                    new Policy(reader.GetInt32(0).ToString(), reader.GetString(1), getStatus(!reader.IsDBNull(2) ? reader.GetDateTime(2):null, !reader.IsDBNull(3) ? reader.GetDateTime(3):null), new List<Package>(), !reader.IsDBNull(2) ? reader.GetDateTime(2):null, !reader.IsDBNull(3) ? reader.GetDateTime(3):null)
                    );
                polIndex = policies.FindIndex((x) => x.PolicyId == reader.GetInt32(0).ToString());

                //seeing if package already exists and adding a new one if not
                if (!reader.IsDBNull(4))
                {

                    int packIndex = policies[polIndex].Package.FindIndex((x) => x.PackageID.Equals(reader.GetInt32(4).ToString()));

                    if (packIndex == -1) policies[polIndex].Package.Add(new Package(reader.GetInt32(4).ToString(), !reader.IsDBNull(5) ? reader.GetDateTime(5) : null, !reader.IsDBNull(6) ? reader.GetDateTime(6) : null, new List<PackageTreatmentCoverage>()));

                    //adding package details to specific package if not null
                    packIndex = policies[polIndex].Package.FindIndex((x) => x.PackageID.Equals(reader.GetInt32(4).ToString()));
                    if (!reader.IsDBNull(7)) policies[polIndex].Package[packIndex].TreatmentCoverages.Add(new PackageTreatmentCoverage(
                                                                                                              new Treatment(reader.GetString(7), reader.GetString(8), reader.GetString(9), new List<MedicalServiceProviderTreatment>()),
                                                                                                              new TreatmentCoverage(reader.GetInt32(10), reader.GetString(11), 0, 0, 0)
                                                                                                          ));
                }
            }

            return policies;
        }

        public static Policy getPolicyByID(string id)
        {
            Policy policy = new Policy();
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();
            SqlCommand command = new SqlCommand("dbo.PolicyByID", scon);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@id", int.Parse(id));

            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    if(policy.PolicyId==null) policy=new Policy(reader.GetInt32(0).ToString(), reader.GetString(1), getStatus(reader.GetDateTime(2),reader.GetDateTime(3)),  new List<Package>(), reader.GetDateTime(2), reader.GetDateTime(3));

                    if (!reader.IsDBNull(4))
                    {
                        //seeing if package already exists and adding a new one if not
                        int packIndex = policy.Package.FindIndex((x) => x.PackageID.Equals(reader.GetInt32(4).ToString()));
                        if (packIndex == -1) policy.Package.Add(new Package(reader.GetInt32(4).ToString(), !reader.IsDBNull(5)?reader.GetDateTime(5):null,!reader.IsDBNull(6)?reader.GetDateTime(6):null,new List<PackageTreatmentCoverage>()));
                    
                        //adding package details to specific package if not null
                        packIndex = policy.Package.FindIndex((x) => x.PackageID.Equals(reader.GetInt32(4).ToString()));
                        if (!reader.IsDBNull(7)) policy.Package[packIndex].TreatmentCoverages.Add(new PackageTreatmentCoverage(
                                                                                                      new Treatment(reader.GetString(7), reader.GetString(8), reader.GetString(9), new List<MedicalServiceProviderTreatment>()), 
                                                                                                      new TreatmentCoverage(reader.GetInt32(10),reader.GetString(11),0,0,0)
                                                                                                  ));
                    }
                }
            }
            else policy=new Policy();
            

            return policy;
        }

        public Policy createPolicy(Policy req)
        {
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();
            SqlCommand command = new SqlCommand("dbo.InsertPolicy",scon);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@name",req.PolicyName);
            command.Parameters.AddWithValue("@startTime", req.StatusStartDate);
            command.Parameters.AddWithValue("@endTime", req.StatusEndDate);
            command.Parameters.AddWithValue("@statusDate", DateTime.Today);
            int policyID=int.Parse(command.ExecuteScalar().ToString());
            Console.WriteLine(policyID);
            Console.WriteLine();
            //get inserted id
            foreach (Package package in req.Package)//Adding multiple inserts to 1 table
            {
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@policyID", policyID);
                command.Parameters.AddWithValue("@packageID",int.Parse(package.PackageID));
                command.CommandText = "dbo.insertPolicyPackage";
                command.ExecuteNonQuery();
                
                foreach (PackageTreatmentCoverage item in package.TreatmentCoverages)//Insert PackageTreatmentCoverages
                {
                    command.Parameters.Clear();
                    command.CommandText = "dbo.insertPackageTreatmentCoverage";
                    command.Parameters.AddWithValue("@treatmentID",item.Treatment.TreatmentID);
                    command.Parameters.AddWithValue("@coverageID", item.Coverage.CoverageID);
                    command.Parameters.AddWithValue("@packageID", package.PackageID);
                    command.ExecuteNonQuery();
                }
            }

            return req;
        }

        public Policy updatePolicy(int id,Policy req)
        {
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();
            SqlCommand command = new SqlCommand("dbo.UpdatePolicy", scon);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@name", req.PolicyName);
            command.Parameters.AddWithValue("@startTime", req.StatusStartDate);
            command.Parameters.AddWithValue("@endTime", req.StatusEndDate);
            command.Parameters.AddWithValue("@statusDate", DateTime.Today);
            command.Parameters.AddWithValue("@policyID", int.Parse(req.PolicyId));

            command.ExecuteNonQuery();

            foreach (Package package in req.Package)//Adding multiple inserts to 1 table
            {
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@policyID", req.PolicyId);
                command.Parameters.AddWithValue("@packageID", int.Parse(package.PackageID));
                command.CommandText = "dbo.insertPolicyPackage";
                command.ExecuteNonQuery();

                foreach (PackageTreatmentCoverage item in package.TreatmentCoverages)//Insert PackageTreatmentCoverages
                {
                    command.Parameters.Clear();
                    command.CommandText = "dbo.insertPackageTreatmentCoverage";
                    command.Parameters.AddWithValue("@treatmentID", item.Treatment.TreatmentID);
                    command.Parameters.AddWithValue("@coverageID", item.Coverage.CoverageID);
                    command.Parameters.AddWithValue("@packageID", package.PackageID);
                    command.ExecuteNonQuery();
                }
            }

            return req;
        }

        public Policy deletePolicy(int id)
        {
            Console.WriteLine("Delete");
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();
            string query = "dbo.DeletePolicy";
            SqlCommand command = new SqlCommand(query, scon);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@id", id);

            command.ExecuteNonQuery();

            return new Policy();
        }

        public static string getStatus(DateTime? start, DateTime? end)
        {
            if((start != null && start <= DateTime.Today)&&(end != null && end >= DateTime.Today))
            {
                return "limited";
            }

            if (start != null && start <= DateTime.Today && end==null)
            {
                return "active";
            }

            if(end != null && end < DateTime.Today)
            {
                return "expired";
            }

            if (end == null && start == null)
            {
                return "unlisted";
            }

            if (start != null && start>DateTime.Today)
            {
                return "upcoming";
            }

            return null;
        }
    }
}
