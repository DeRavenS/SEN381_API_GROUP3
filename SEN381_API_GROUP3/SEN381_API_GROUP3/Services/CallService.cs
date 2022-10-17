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

            SqlCommand command = new SqlCommand($@"SELECT * FROM [dbo].[CallDetails] ORDER BY CALLID OFFSET @offset ROWS FETCH NEXT @size ROWS ONLY;", scon);
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
            SqlCommand command = new SqlCommand($@"SELECT * FROM [dbo].[CallDetails] where CALLID = '{id}'", scon);
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
            string query = $@"INSERT INTO CallDetails (startTime, endTime)VALUES('{StartTime}', '{EndTime}')";

            SqlParameter starttime = new SqlParameter("@StartTime", SqlDbType.VarChar);
            SqlParameter endtime = new SqlParameter("@EndTime", SqlDbType.VarChar);

            starttime.Value = StartTime.ToString();
            endtime.Value = EndTime.ToString();


            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();


            SqlCommand insertCommand = new SqlCommand(query, scon);
            insertCommand.Parameters.Add(starttime);
            insertCommand.Parameters.Add(endtime);

            insertCommand.ExecuteNonQuery();
            scon.Close();
        }

        // Update Call
        public void updateCall(int id, string StartTime, string EndTime)
        {
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();
            string query = $@"Update CallDetails set startTime = '{StartTime}', endTime = '{EndTime}' WHERE CALLID = '{id}'";
            SqlParameter Starttime = new SqlParameter("@ClientName", SqlDbType.VarChar);
            SqlParameter Endtime = new SqlParameter("@ClientAdress", SqlDbType.VarChar);

            Starttime.Value = StartTime.ToString();
            Endtime.Value = EndTime.ToString();
            SqlCommand updateCommand = new(query, scon);
            updateCommand.Parameters.Add(Starttime);
            updateCommand.Parameters.Add(Endtime);


            updateCommand.ExecuteNonQuery();
            scon.Close();
        }

        // Delete Call
        public void deleteCall(int id)
        {
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();
            string query = $@"DELETE from CallDetails WHERE CALLID = '{id}'";
            SqlCommand com = new SqlCommand(query, scon);
            com.ExecuteNonQuery();
            scon.Close();
        }


    }
}
