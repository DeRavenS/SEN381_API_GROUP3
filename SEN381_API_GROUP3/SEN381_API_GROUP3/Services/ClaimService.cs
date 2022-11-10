using SEN381_API_Group3.shared.models;
using SEN381_API_GROUP3.Database;
using SEN381_API_GROUP3.shared.models;
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

            SqlCommand cmd = new SqlCommand("dbo.AllClaims", scon);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    modules.Add(new Claim(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetString(4)));
                }
            }


            return modules;
        }
        public Claim getClaimById(int id)
        {
            List<Claim> modules = new List<Claim>();
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();
            SqlCommand command = new SqlCommand("dbo.ClaimByID", scon)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("@ClaimID", SqlDbType.Int).Value = id;
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    modules.Add(new Claim(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetString(4)));

                }
            }

            return modules[0];
        }
        public AddClaim addNewClaim(AddClaim claim)
        {
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();
            SqlCommand sqlCommand = new SqlCommand("AddCallDetails", scon);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@startTime", SqlDbType.VarChar).Value = claim.StartTime;
            sqlCommand.Parameters.AddWithValue("@endTime", SqlDbType.VarChar).Value = claim.EndTime;
            decimal callid = (decimal)sqlCommand.ExecuteScalar();


            SqlCommand cmd = new SqlCommand("NewClaim", scon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@clientID", SqlDbType.VarChar).Value = claim.Client;
            cmd.Parameters.AddWithValue("@placeOfTreatment", SqlDbType.VarChar).Value = claim.PlaceOfTreatment;
            cmd.Parameters.AddWithValue("@callIDs", SqlDbType.VarChar).Value = callid;
            cmd.Parameters.AddWithValue("@ClaimeStatus", SqlDbType.VarChar).Value = claim.ClaimeStatus;
            decimal id = (decimal) cmd.ExecuteScalar();

            SqlCommand command = new SqlCommand("AddnewClaimMedical", scon);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@claimID", SqlDbType.Int).Value = id;
            command.Parameters.AddWithValue("@MedicalTreatmentID", SqlDbType.VarChar).Value = "";
            foreach (int item in claim.MedicalConditions)
            {
                command.Parameters["@MedicalTreatmentID"].Value = item;
                command.ExecuteNonQuery();
            }
            scon.Close();
            return claim;
        }
        public Claim UpdateClaim(int id, Claim claim)
        {
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();
            SqlCommand updateCommand = new SqlCommand("UpdateClaim", scon);
            updateCommand.CommandType = CommandType.StoredProcedure;
            updateCommand.Parameters.AddWithValue("@ClaimIDs", SqlDbType.VarChar).Value = id;
            updateCommand.Parameters.AddWithValue("@clientID", SqlDbType.VarChar).Value = claim.Client;
            updateCommand.Parameters.AddWithValue("@Place", SqlDbType.VarChar).Value = claim.PlaceOfTreatment;
            updateCommand.Parameters.AddWithValue("@CallID", SqlDbType.VarChar).Value = claim.CallDetails;
            updateCommand.Parameters.AddWithValue("@stuatus", SqlDbType.Int).Value = claim.ClaimeStatus;

            updateCommand.ExecuteNonQuery();
            scon.Close();
            return claim;
        }
        public void deleteClaim(int id)
        {
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();
            SqlCommand com = new SqlCommand("dbo.DeleteClaimDetails", scon);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = id;
            com.ExecuteNonQuery();
            scon.Close();
        }
    }
}
