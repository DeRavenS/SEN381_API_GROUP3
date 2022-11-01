using SEN381_API_Group3.shared.models;
using SEN381_API_GROUP3.Database;
using System.Data;
using System.Data.SqlClient;

namespace SEN381_API_GROUP3.Services
{
    public class ClaimService
    {

        public List<Claim> getClaims(int page, int size)
        {

            int offset = (page - 1) * size;
            List<Claim> modules = new List<Claim>();
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();

            SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[Claim] ORDER BY CLAIMID OFFSET @offset ROWS FETCH NEXT @size ROWS ONLY;", scon);
            command.Parameters.AddWithValue("@offset", offset);
            command.Parameters.AddWithValue("@size", size);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    modules.Add(new Claim(reader.GetInt32(0).ToString(), reader.GetString(1), reader.GetInt32(2).ToString(), reader.GetString(3), reader.GetInt32(4).ToString(), reader.GetString(5)));
                }
            }


            return modules;
        }
        public Claim getClaimById(int id)
        {
            List<Claim> modules = new List<Claim>();
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();
            SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[Claim] where CLAIMID = " + id, scon);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    modules.Add(new Claim(reader.GetInt32(0).ToString(), reader.GetInt32(1).ToString(), reader.GetInt32(2).ToString(), reader.GetString(3), reader.GetInt32(4).ToString(), reader.GetString(5)));

                }
            }


            return modules[0];
        }
        public void addNewClaim(int ClientID, int MedicalCondition, string PlaceOfTreament, int CallID, string ClaimeStatus)
        {
            string query = $@"INSERT into Claim
    (ClientID, MedicalCondition, placeOfTreatment, CallID, ClaimeStatus)
VALUES
    ('{ClientID}','{MedicalCondition}','{PlaceOfTreament}', '{CallID}', '{ClaimeStatus}')";

            SqlParameter ClientId = new SqlParameter("@ClientID", SqlDbType.Int);
            SqlParameter Medicalcondition = new SqlParameter("@MedicalCondition", SqlDbType.Int);
            SqlParameter PlaceOftreament = new SqlParameter("@PlaceOfTreament", SqlDbType.VarChar);
            SqlParameter CallId = new SqlParameter("@CallID", SqlDbType.Int);
            SqlParameter Claimestatus = new SqlParameter("@ClaimeStatus", SqlDbType.VarChar);

            ClientId.Value = ClientID.ToString();
            Medicalcondition.Value = MedicalCondition.ToString();
            PlaceOftreament.Value = PlaceOfTreament.ToString();
            CallId.Value = CallID.ToString();
            Claimestatus.Value = ClaimeStatus.ToString();

            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();


            SqlCommand insertCommand = new SqlCommand(query, scon);
            insertCommand.Parameters.Add(ClientId);
            insertCommand.Parameters.Add(Medicalcondition);
            insertCommand.Parameters.Add(PlaceOftreament);
            insertCommand.Parameters.Add(CallId);
            insertCommand.Parameters.Add(Claimestatus);

            insertCommand.ExecuteNonQuery();
            scon.Close();
        }
        public void UpdateClaim(int calimID, int ClientID, int MedicalCondition, string PlaceOfTreament, int CallID, string ClaimeStatus)
        {
            string query = $@"UPDATE Claim set ClientID = '{ClientID}', MedicalCondition = '{MedicalCondition}', placeOfTreatment = '{PlaceOfTreament}', CallID = '{CallID}', ClaimeStatus = '{ClaimeStatus}' WHERE CLAIMID = '{calimID}'";

            SqlParameter ClientId = new SqlParameter("@ClientID", SqlDbType.Int);
            SqlParameter Medicalcondition = new SqlParameter("@MedicalCondition", SqlDbType.Int);
            SqlParameter PlaceOftreament = new SqlParameter("@PlaceOfTreament", SqlDbType.VarChar);
            SqlParameter CallId = new SqlParameter("@CallID", SqlDbType.Int);
            SqlParameter Claimestatus = new SqlParameter("@ClaimeStatus", SqlDbType.VarChar);

            ClientId.Value = ClientID.ToString();
            Medicalcondition.Value = MedicalCondition.ToString();
            PlaceOftreament.Value = PlaceOfTreament.ToString();
            CallId.Value = CallID.ToString();
            Claimestatus.Value = ClaimeStatus.ToString();

            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();


            SqlCommand updateCommand = new SqlCommand(query, scon);
            updateCommand.Parameters.Add(ClientId);
            updateCommand.Parameters.Add(Medicalcondition);
            updateCommand.Parameters.Add(PlaceOftreament);
            updateCommand.Parameters.Add(CallId);
            updateCommand.Parameters.Add(Claimestatus);

            updateCommand.ExecuteNonQuery();
            scon.Close();
        }
        public void deleteClaim(int id)
        {
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();
            string query = $@"DELETE from Claim WHERE CLAIMID = '{id}'";
            SqlCommand com = new SqlCommand(query, scon);
            com.ExecuteNonQuery();
            scon.Close();
        }
    }
}
