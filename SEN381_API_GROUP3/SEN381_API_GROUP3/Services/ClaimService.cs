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
                    modules.Add(new Claim(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetString(3), reader.GetInt32(4), reader.GetString(5)));
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
                    modules.Add(new Claim(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetString(3), reader.GetInt32(4), reader.GetString(5)));

                }
            }

            return modules[0];
        }
        public void addNewClaim(Claim claim)
        {
            string query = $@"INSERT into Claim
    (ClientID, MedicalCondition, placeOfTreatment, CallID, ClaimeStatus)
VALUES
    ('{claim.Client}',{claim.MedicalConditions},'{claim.PlaceOfTreatment}', {claim.CallDetails}, '{claim.ClaimeStatus}')";

            SqlParameter ClientId = new SqlParameter("@ClientID", SqlDbType.VarChar);
            SqlParameter Medicalcondition = new SqlParameter("@MedicalCondition", SqlDbType.Int);
            SqlParameter PlaceOftreament = new SqlParameter("@PlaceOfTreament", SqlDbType.VarChar);
            SqlParameter CallId = new SqlParameter("@CallID", SqlDbType.Int);
            SqlParameter Claimestatus = new SqlParameter("@ClaimeStatus", SqlDbType.VarChar);

            ClientId.Value = claim.Client.ToString();
            Medicalcondition.Value = claim.MedicalConditions;
            PlaceOftreament.Value = claim.PlaceOfTreatment.ToString();
            CallId.Value = claim.CallDetails;
            Claimestatus.Value = claim.ClaimeStatus.ToString();

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
        public void UpdateClaim(int id, Claim claim)
        {
            string query = $@"UPDATE Claim set ClientID = '{claim.Client}', MedicalCondition = {claim.MedicalConditions}, placeOfTreatment = '{claim.PlaceOfTreatment}', CallID = {claim.CallDetails}, ClaimeStatus = '{claim.ClaimeStatus}' WHERE CLAIMID = {id}";

            SqlParameter ClientId = new SqlParameter("@ClientID", SqlDbType.VarChar);
            SqlParameter Medicalcondition = new SqlParameter("@MedicalCondition", SqlDbType.Int);
            SqlParameter PlaceOftreament = new SqlParameter("@PlaceOfTreament", SqlDbType.VarChar);
            SqlParameter CallId = new SqlParameter("@CallID", SqlDbType.Int);
            SqlParameter Claimestatus = new SqlParameter("@ClaimeStatus", SqlDbType.VarChar);

            ClientId.Value = claim.Client.ToString();
            Medicalcondition.Value = claim.MedicalConditions;
            PlaceOftreament.Value = claim.PlaceOfTreatment.ToString();
            CallId.Value = claim.CallDetails;
            Claimestatus.Value = claim.ClaimeStatus.ToString();

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
            string query = $@"DELETE FROM [dbo].[Claim]
            WHERE CLAIMID = {id}";
            SqlCommand com = new SqlCommand(query, scon);
            com.ExecuteNonQuery();
            scon.Close();
        }
    }
}
