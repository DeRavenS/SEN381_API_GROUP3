using SEN381_API_Group3.shared.models;
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
            List<Policy> policies = new List<Policy>();
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();
            SqlCommand command = new SqlCommand("SELECT " +
                "                                       p.PolicyID, " +
                "                                       p.PolicyName, " +
                "                                       ca.startTime, " +
                "                                       ca.endTime " +
                "                               FROM " +
                "                                       [dbo].[Policy] p " +
                "                               CROSS APPLY " +
                "                                       (SELECT TOP 1 startTime, endTime" +
                "                                        FROM" +
                "                                            PolicyStatus ps " +
                "                                        WHERE ps.PolicyID=p.PolicyID " +
                "                                        ORDER BY " +
                "                                            ps.PolicyStatusDate DESC) as ca"+
                "                               ORDER BY " +
                "                                        p.PolicyID " +
                "                               OFFSET @offset ROWS FETCH NEXT @size ROWS ONLY ", scon);
            command.Parameters.AddWithValue("@offset", page*size);
            command.Parameters.AddWithValue("@size", size);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    policies.Add(new Policy(reader.GetInt32(0).ToString(), reader.GetString(1), getStatus(reader.GetDateTime(2), reader.GetDateTime(3)), new List<Package>()));
                }
            }
            //if (policies.Count() == 0)
            //{
            //    throw new HttpRequestException();
            //}
            return policies;
        }

        public static Policy getPolicyByID(string id)
        {
            List<Policy> policies = new List<Policy>();
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();
            SqlCommand command = new SqlCommand("SELECT " +
                "                                       p.tbPolicyID, " +
                "                                       p.tbPolicyname, " +
                "                                       ca.startTime, " +
                "                                       ca.endTime " +
                "                               FROM " +
                "                                       [dbo].[Policy] p " +
                "                               CROSS APPLY " +
                "                                       (SELECT TOP 1 startTime, endTime" +
                "                                        FROM" +
                "                                            PolicyStatus ps " +
                "                                        WHERE ps.PolicyID=p.PolicyID " +
                "                                        ORDER BY " +
                "                                            ps.PolicyStatusDate DESC) as ca"+
                "                               WHERE p.PolicyID = @id", scon);
            command.Parameters.AddWithValue("@id", int.Parse(id));

            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    policies.Add(new Policy(reader.GetInt32(0).ToString(), reader.GetString(1), getStatus(reader.GetDateTime(2),reader.GetDateTime(3)), new List<Package>()));
                }
            }
            if (policies.Count() == 0)
            {
                throw new HttpRequestException();
            }
            Policy policy = policies[0];

            return policies[0];
        }

        public ICreatePolicyRequest createPolicy(ICreatePolicyRequest req)
        {
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();
            SqlCommand command = new SqlCommand("" +
                "BEGIN TRANSACTION" +
                "   INSERT INTO Policy(PolicyName) VALUES(@name);" +
                "   INSERT INTO PolicyStatus(startTime,endTime,PolicyStatusDate) VALUES(@startTime,@endTime,@statusDate);" +
                "   INSERT INTO PolicyTreatmentCoverage(TreatmentID,PolicyID,CoverageID) VALUES(@treatmentID,@policyID,@coverageID);" +
                "END TRANSACTION",scon);

            command.Parameters.AddWithValue("@name",req.PolicyName);
            command.Parameters.AddWithValue("@startTime", req.StartTime);
            command.Parameters.AddWithValue("@endTime", req.EndTime);
            command.Parameters.AddWithValue("@statusDate", DateTime.Today);
            foreach (KeyValuePair<string, string> kvp in req.TreatmentCoverages)
            {
                Console.WriteLine(string.Format("Key = {0}, Value = {1}", kvp.Key, kvp.Value));
                command.Parameters.AddWithValue("@treatmentID", kvp.Key);
                command.Parameters.AddWithValue("@policyID", req.PolicyID);
                command.Parameters.AddWithValue("@coverageID", kvp.Value);
            }

            command.ExecuteNonQuery();


            return req;
        }

        public IUpdatePolicyRequest updatePolicy(int id,IUpdatePolicyRequest req)
        {
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();
            SqlCommand command = new SqlCommand("" +
                "BEGIN TRANSACTION" +

                    "UPDATE Policy p" +
                    "SET " +
                    "   p.PolicyName=@name" +
                    "WHERE p.PolicyID=@ID;"+

                    "INSERT INTO " +
                    "   PolicyStatus" +
                    "       (startTime,endTime,PolicyStatusDate) " +
                    "   VALUES" +
                    "       (@startTime,@endTime,@statusDate);"+

                "END TRANSACTION",scon);

            command.Parameters.AddWithValue("@name", req.PolicyName);
            command.Parameters.AddWithValue("@startTime", req.StartTime);
            command.Parameters.AddWithValue("@endTime", req.EndTime);
            command.Parameters.AddWithValue("@statusDate", DateTime.Today);
            command.Parameters.AddWithValue("@ID", id);

            command.ExecuteNonQuery();

            return req;
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
