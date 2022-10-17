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
        public void addMedicalCondition(string MedicalConditionName, string MedicalConditionDescription, int TreatmentID) {
            string query = $@"INSERT INTO MedicalCondition (MedicalConditionName, MedicalConditionDescription,TreatmentID)VALUES('{MedicalConditionName}', '{MedicalConditionDescription}', '{TreatmentID}')";

            SqlParameter MedicalConditionname = new SqlParameter("@MedicalConditionName", SqlDbType.VarChar);
            SqlParameter MedicalConditiondescription = new SqlParameter("@MedicalConditionDescription", SqlDbType.VarChar);
            SqlParameter TreatmentId = new SqlParameter("@TreatmentID", SqlDbType.Int);

            MedicalConditionname.Value = MedicalConditionName.ToString();
            MedicalConditiondescription.Value = MedicalConditionDescription.ToString();
            TreatmentId.Value = TreatmentID.ToString();

            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();


            SqlCommand insertCommand = new SqlCommand(query, scon);
            insertCommand.Parameters.Add(MedicalConditionname);
            insertCommand.Parameters.Add(MedicalConditiondescription);
            insertCommand.Parameters.Add(TreatmentId);

            insertCommand.ExecuteNonQuery();
            scon.Close();
        }
        public void updateMedicalCondition(int id,string MedicalConditionName, string MedicalConditionDescription, int TreatmentID)
        {
            string query = $@"Update Client set ClientName = '{MedicalConditionName}', ClientAddress = '{MedicalConditionDescription}', clientEmail = {TreatmentID} WHERE MedicalConditionID = {id}";

            SqlParameter MedicalConditionname = new SqlParameter("@MedicalConditionName", SqlDbType.VarChar);
            SqlParameter MedicalConditiondescription = new SqlParameter("@MedicalConditionDescription", SqlDbType.VarChar);
            SqlParameter TreatmentId = new SqlParameter("@TreatmentID", SqlDbType.Int);

            MedicalConditionname.Value = MedicalConditionName.ToString();
            MedicalConditiondescription.Value = MedicalConditionDescription.ToString();
            TreatmentId.Value = TreatmentID.ToString();

            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();


            SqlCommand insertCommand = new SqlCommand(query, scon);
            insertCommand.Parameters.Add(MedicalConditionname);
            insertCommand.Parameters.Add(MedicalConditiondescription);
            insertCommand.Parameters.Add(TreatmentId);

            insertCommand.ExecuteNonQuery();
            scon.Close();
        }
        public void deleteMedicalCondition(int id)
        {
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();
            string query = $@"DELETE from MedicalCondition WHERE MedicalConditionID = '{id}'";
            SqlCommand com = new SqlCommand(query, scon);
            com.ExecuteNonQuery();
            scon.Close();
        }
    }
}
