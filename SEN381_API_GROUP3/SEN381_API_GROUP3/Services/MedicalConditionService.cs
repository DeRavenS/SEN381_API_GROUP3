using SEN381_API_Group3.shared.models;
using SEN381_API_GROUP3.Database;
using System.Data;
using System.Data.SqlClient;

namespace SEN381_API_GROUP3.Services
{
    public class MedicalConditionService
    {
        public List<MedicalCondition> getAllMedicalConditions(int page, int size)
        {
            int offset = (page - 1) * size;
            List<MedicalCondition> modules = new List<MedicalCondition>();
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();

            SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[MedicalCondition] ORDER BY MedicalConditionID OFFSET @offset ROWS FETCH NEXT @size ROWS ONLY;", scon);
            command.Parameters.AddWithValue("@offset", offset);
            command.Parameters.AddWithValue("@size", size);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    modules.Add(new MedicalCondition(reader.GetInt32(0).ToString(), reader.GetString(1), reader.GetString(2), reader.GetInt32(3).ToString()));

                }
            }


            return modules;
        }

        public List<MedicalCondition> getMedicalConditionById(int id)
        {
            List<MedicalCondition> modules = new List<MedicalCondition>();
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();
            SqlCommand command = new SqlCommand($@"SELECT * FROM [dbo].[MedicalCondition] where MedicalConditionID = '{id}'", scon);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    modules.Add(new MedicalCondition(reader.GetInt32(0).ToString(), reader.GetString(1), reader.GetString(2), reader.GetInt32(3).ToString()));
                }
            }


            return modules;
        }
    }
}
