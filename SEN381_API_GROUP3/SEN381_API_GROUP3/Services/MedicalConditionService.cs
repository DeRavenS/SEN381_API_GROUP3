using DevExpress.Xpo.DB.Helpers;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SEN381_API_Group3.shared.models;
using SEN381_API_GROUP3.Controllers;
using SEN381_API_GROUP3.Database;
using SEN381_API_GROUP3.shared.models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Security.Claims;

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

            SqlCommand command = new SqlCommand("dbo.AllMedicalConditionsByID", scon)
            {
                CommandType = CommandType.StoredProcedure
            };
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
        public bool addMedicalCondition(InsertMedicalConditionRequest medical) {

            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();
            SqlCommand cmd = new SqlCommand("NewMedicalCondition", scon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MedicalCname", SqlDbType.VarChar).Value = medical.Name;
            cmd.Parameters.AddWithValue("@MedicalDescription", SqlDbType.VarChar).Value = medical.Description;
            decimal id = (decimal) cmd.ExecuteScalar();
            SqlCommand command = new SqlCommand("InsertMedicalConditionTreatment", scon);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@MedicalID", SqlDbType.Int).Value = id;
            command.Parameters.AddWithValue("@TreatmentsID", SqlDbType.VarChar).Value = "";
            foreach (string item in medical.IDs1)
            {
                Console.WriteLine(item);
                command.Parameters["@TreatmentsID"].Value = item;
                command.ExecuteNonQuery();
            }
            scon.Close();


            return true;

        }
        public void updateMedicalCondition(int id,MedicalCondition medical)
        {


            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();


            SqlCommand updateCommand = new SqlCommand("dbo.UpdateMedicalConditions", scon);
            updateCommand.CommandType = CommandType.StoredProcedure;
            updateCommand.Parameters.AddWithValue("@MedicalName", SqlDbType.VarChar).Value = medical.MedicalConditionName;
            updateCommand.Parameters.AddWithValue("@MedicalDescription", SqlDbType.VarChar).Value = medical.MedicalConditionDescription;
            updateCommand.Parameters.AddWithValue("@MeddicalID", SqlDbType.Int).Value = id;

            updateCommand.ExecuteNonQuery();
            scon.Close();
        }
        // Medical Condition Queries
        public List<MedicalConditionTreatment> getAllMedicalConditionsTreatments(int page, int size)
        {
            int offset = (page - 1) * size;
            List<MedicalConditionTreatment> modules = new List<MedicalConditionTreatment>();
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();

            SqlCommand cmd = new SqlCommand("dbo.AllTheTreatments", scon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@offset", SqlDbType.Int).Value = page;
            cmd.Parameters.AddWithValue("@row", SqlDbType.Int).Value = size;

            SqlDataReader reader = cmd.ExecuteReader();

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
        public void deleteMedicalTreatment(int id, string Tid) {
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();
            SqlCommand com = new SqlCommand("dbo.DeleteMedicalTreatment", scon);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@MedicalID", SqlDbType.Int).Value = id;
            com.Parameters.AddWithValue("@TreatmentID", SqlDbType.VarChar).Value = Tid;
            com.ExecuteNonQuery();
            scon.Close();
        }


    }
}
