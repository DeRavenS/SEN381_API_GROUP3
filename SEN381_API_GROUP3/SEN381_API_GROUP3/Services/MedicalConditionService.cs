using Microsoft.AspNetCore.Mvc.RazorPages;
using SEN381_API_Group3.shared.models;
using SEN381_API_GROUP3.Database;
using SEN381_API_GROUP3.shared.models;
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

            SqlCommand cmd = new SqlCommand("dbo.AllMedicalConditions", scon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@offset", SqlDbType.Int).Value = page;
            cmd.Parameters.AddWithValue("@row", SqlDbType.Int).Value = size;

            SqlDataReader reader = cmd.ExecuteReader(); 

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    modules.Add(new MedicalCondition(reader.GetInt32(0), reader.GetString(1), reader.GetString(2)));

                }
            }
            return modules;
        }

        public MedicalCondition getMedicalConditionById(int id)
        {
            List<MedicalCondition> modules = new List<MedicalCondition>();
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();

            SqlCommand command = new SqlCommand("dbo.AllMedicalConditionsByID", scon);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@MedicalID", SqlDbType.Int).Value = id;

            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    modules.Add(new MedicalCondition(reader.GetInt32(0), reader.GetString(1), reader.GetString(2)));
                }
            }


            return modules[0];
        }
        public void addMedicalCondition(MedicalCondition medical) {
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();
            SqlCommand cmd = new SqlCommand("NewMedicalCondition", scon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Medicalname", SqlDbType.Int).Value = medical.MedicalConditionName;
            cmd.Parameters.AddWithValue("@MedicalDescription", SqlDbType.Int).Value = medical.MedicalConditionDescription;

            cmd.ExecuteNonQuery();
            scon.Close();
        }
        public void updateMedicalCondition(int id,MedicalCondition medical)
        {
            string query = $@"Update MedicalCondition set MedicalConditionName = '{medical.MedicalConditionName}', MedicalConditionDescription = '{medical.MedicalConditionDescription}' WHERE MedicalConditionID = {id}";

            SqlParameter MedicalConditionname = new SqlParameter("@MedicalConditionName", SqlDbType.VarChar);
            SqlParameter MedicalConditiondescription = new SqlParameter("@MedicalConditionDescription", SqlDbType.VarChar);
            SqlParameter TreatmentId = new SqlParameter("@TreatmentID", SqlDbType.Int);

            MedicalConditionname.Value = medical.MedicalConditionName.ToString();
            MedicalConditiondescription.Value = medical.MedicalConditionDescription.ToString();


            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();


            SqlCommand insertCommand = new SqlCommand(query, scon);
            insertCommand.Parameters.Add(MedicalConditionname);
            insertCommand.Parameters.Add(MedicalConditiondescription);

            insertCommand.ExecuteNonQuery();
            scon.Close();
        }
        // Medical Condition Queries
        public List<MedicalConditionTreatment> getAllMedicalConditionsTreatments(int page, int size)
        {
            int offset = (page - 1) * size;
            List<MedicalConditionTreatment> modules = new List<MedicalConditionTreatment>();
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();

            SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[Treatment] ORDER BY TreatmentID OFFSET @offset ROWS FETCH NEXT @size ROWS ONLY;", scon);
            command.Parameters.AddWithValue("@offset", offset);
            command.Parameters.AddWithValue("@size", size);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    modules.Add(new MedicalConditionTreatment(reader.GetString(0), reader.GetString(1), reader.GetString(2)));

                }
            }
            return modules;
        }
        public void deleteMedicalCondition(int id)
        {
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();
            SqlCommand com = new SqlCommand("dbo.DeleteMedicalCondition", scon);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@param1", SqlDbType.Int).Value = id;
            com.ExecuteNonQuery();
            scon.Close();
        }
        // Medical Condition Treatment
        public List<MedicalConditionTreatment> getMedicalConditon(int id) {
            List<MedicalConditionTreatment> modules = new List<MedicalConditionTreatment>();
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();
            SqlCommand command = new SqlCommand($@"dbo.TreatmentByMedical", scon);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@MedicalID", SqlDbType.Int).Value = id;
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    modules.Add(new MedicalConditionTreatment(reader.GetString(0), reader.GetString(1), reader.GetString(2)));
                }
            }
            return modules;
        }
        public void deleteMedicalTreatment(string id) {
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();
            SqlCommand com = new SqlCommand("dbo.DeleteMedicalTreatment", scon);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@MedicalID", SqlDbType.Int).Value = id;
            com.ExecuteNonQuery();
            scon.Close();
        }


    }
}
