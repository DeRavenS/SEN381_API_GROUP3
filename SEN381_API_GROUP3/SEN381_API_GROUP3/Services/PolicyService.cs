using SEN381_API_Group3.shared.models;
using SEN381_API_GROUP3.Database;
using System.Data.SqlClient;
using System.Web.Http;
using System.Net;

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
                "                                       p.Policyname, " +
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

            return policies;
        }

        public static Policy getPolicyByID(string id)
        {
            List<Policy> policies = new List<Policy>();
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();
            SqlCommand command = new SqlCommand("SELECT " +
                "                                       p.PolicyID, " +
                "                                       p.Policyname, " +
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
            //if (policies.Count() == 0)
            //{
            //    throw new HttpResponseException(HttpStatusCode.NotFound);
            //} TO DO ==> Implement Later


            return policies[0];
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
