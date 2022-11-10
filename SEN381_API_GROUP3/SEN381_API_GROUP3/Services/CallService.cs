using SEN381_API_Group3.shared.models;
using SEN381_API_GROUP3.Database;
using System.Data;
using System.Data.SqlClient;


namespace SEN381_API_GROUP3.Services
{
    public class CallService
    {

        // Get all Call Details
        public List<CallDetails> getAllCallDetails(int page, int size)
        {
            int offset = (page - 1) * size;

            List<CallDetails> modules = new List<CallDetails>();
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();

            SqlCommand command = new SqlCommand($@"dbo.GetAllCalls", scon);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@offset", offset);
            command.Parameters.AddWithValue("@size", size);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {

                    modules.Add(new CallDetails(reader.GetInt32(0).ToString(), reader.GetDateTime(1), reader.GetDateTime(2)));


                }
            }


            return modules;
        }

        // Get Call By Id
        public List<CallDetails> getCallById(int id)
        {
            List<CallDetails> modules = new List<CallDetails>();
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();
            SqlCommand command = new SqlCommand($@"dbo.GetCallByID", scon);
            command.Parameters.AddWithValue("@id",id);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    modules.Add(new CallDetails(reader.GetInt32(0).ToString(), reader.GetDateTime(1), reader.GetDateTime(2)));
                }
            }


            return modules;
        }

        // Add new Call
        public void addNewCall(string StartTime, string EndTime)
        {
            string query = $@"dbo.InsertCallDetails";

            SqlParameter starttime = new SqlParameter("@StartTime", SqlDbType.VarChar);
            SqlParameter endtime = new SqlParameter("@EndTime", SqlDbType.VarChar);

            starttime.Value = StartTime.ToString();
            endtime.Value = EndTime.ToString();


            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();


            SqlCommand insertCommand = new SqlCommand(query, scon);
            insertCommand.Parameters.Add(starttime);
            insertCommand.Parameters.Add(endtime);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.ExecuteNonQuery();
            scon.Close();
        }

        // Update Call
        public void updateCall(int id, string StartTime, string EndTime)
        {
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();
            string query = $@"dbo.UpdateCallDetails";
            
            SqlCommand updateCommand = new(query, scon);
            updateCommand.CommandType = CommandType.StoredProcedure;
            updateCommand.Parameters.AddWithValue("@StartTime",StartTime);
            updateCommand.Parameters.AddWithValue("@EndTime", EndTime);
            updateCommand.Parameters.AddWithValue("@id", id);

            updateCommand.ExecuteNonQuery();
            scon.Close();
        }

        // Delete Call
        public void deleteCall(int id)
        {
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();
            string query = $@"dbo.DeleteCallDetails";
            SqlCommand com = new SqlCommand(query, scon);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@id",id);
            com.ExecuteNonQuery();
            scon.Close();
        }


    }
}
