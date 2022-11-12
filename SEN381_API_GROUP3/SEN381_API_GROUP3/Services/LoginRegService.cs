using SEN381_API_GROUP3.Database;
using SEN381_API_GROUP3.shared.models;
using System.Data;
using System.Data.SqlClient;

namespace SEN381_API_GROUP3.Services
{
    public class LoginRegService
    {
        public List<EmployeeDetails> GetEmployeeDetails()
        {

            List<EmployeeDetails> modules = new List<EmployeeDetails>();
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();

            SqlCommand cmd = new SqlCommand("dbo.AllEmployeeDetails", scon);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    modules.Add(new EmployeeDetails(reader.GetString(0), reader.GetString(1), reader.GetString(2)));
                }

            }

            return modules;

        }
        public EmployeeDetails? GetEmployeeByInput(string email, string password, string name)
        {
            List<EmployeeDetails> modules = new List<EmployeeDetails>();
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();
            SqlCommand command = new SqlCommand("dbo.FindEmployeeByInput", scon)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("@Email", SqlDbType.VarChar).Value = email;
            command.Parameters.AddWithValue("@Name", SqlDbType.VarChar).Value = name;
            command.Parameters.AddWithValue("@Pass", SqlDbType.VarChar).Value = password;

            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    modules.Add(new EmployeeDetails(reader.GetString(0), reader.GetString(1), reader.GetString(2)));

                }
                return modules[0];
            }

        }
        public EmployeeDetails? addEmployee(EmployeeDetails employee) {
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();

            SqlCommand sqlCommand = new SqlCommand("AddNewEmployee", scon);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@EmployeeName", SqlDbType.VarChar).Value = employee.EmployeeName;
            sqlCommand.Parameters.AddWithValue("@EmployeeEmail", SqlDbType.VarChar).Value = employee.EmployeeEmail;
            sqlCommand.Parameters.AddWithValue("@EmployeePassowrd", SqlDbType.VarChar).Value = employee.EmployeePassword;
            int rows = sqlCommand.ExecuteNonQuery();

            return employee;
        }
        public EmployeeDetails updateEmployee( EmployeeDetails employee) {
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();

            SqlCommand sqlCommand = new SqlCommand("UpdateEmployeeDetails", scon);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@EmployeeName", SqlDbType.VarChar).Value = employee.EmployeeName;
            sqlCommand.Parameters.AddWithValue("@EmployeeEmail", SqlDbType.VarChar).Value = employee.EmployeeEmail;
            sqlCommand.Parameters.AddWithValue("@EmployeePassowrd", SqlDbType.VarChar).Value = employee.EmployeePassword;
            sqlCommand.Parameters.AddWithValue("@email", SqlDbType.VarChar).Value = employee.EmployeeEmail;

            sqlCommand.ExecuteNonQuery();
            scon.Close();

            return employee;
        }
        public bool removeEmployee(string employee) {
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();

            SqlCommand com = new SqlCommand("dbo.DeleteEmployee", scon);
            com.CommandType = CommandType.StoredProcedure;

            com.Parameters.AddWithValue("@Email", SqlDbType.VarChar).Value = employee;

            com.ExecuteNonQuery();
            scon.Close();

            return true;
        }
    }
}
