using Microsoft.AspNetCore.Mvc.RazorPages;
using SEN381_API_Group3.shared.models;
using SEN381_API_GROUP3.Database;
using System.Data;
using System.Data.SqlClient;

namespace SEN381_API_GROUP3.Services
{
    public class TreatmentService
    {
        public List<Treatment> getTreatment(int page, int size) {
            int offset = (page - 1) * size;
            List<Treatment> modules = new List<Treatment>();
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
                    modules.Add(new Treatment(reader.GetInt32(0).ToString(), reader.GetString(1), reader.GetString(2), reader.GetInt32(3).ToString()));


                }
            }


            return modules;
        }
        public List<Treatment> getTreatmentsByID(int id) {
            List<Treatment> modules = new List<Treatment>();
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();
            SqlCommand command = new SqlCommand($@"SELECT * FROM [dbo].[Treatment] where TreatmentID = '{id}'", scon);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    modules.Add(new Treatment(reader.GetInt32(0).ToString(), reader.GetString(1), reader.GetString(2), reader.GetInt32(3).ToString()));
                }
            }


            return modules;
        
        }
        public void addNewTreatment(string TreatmentName, string TreatmentDescription, int MedicalServiceProvidorID) {
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();
            string query = $@"INSERT INTO Treatment (TreatmentName, TreatmentDescription,MedicalServiceProvidorID)VALUES('{TreatmentName}', '{TreatmentDescription}', '{MedicalServiceProvidorID}')";
            SqlParameter Treatmentname = new SqlParameter("@ClientName", SqlDbType.VarChar);
            SqlParameter Treatmentdescription = new SqlParameter("@ClientAdress", SqlDbType.VarChar);
            SqlParameter MedicalServiceProvidorId = new SqlParameter("@ClientEmail", SqlDbType.Int);
            Treatmentname.Value = TreatmentName.ToString();
            Treatmentdescription.Value = TreatmentDescription.ToString();
            MedicalServiceProvidorId.Value = MedicalServiceProvidorID.ToString();
            SqlCommand updateCommand = new(query, scon);
            updateCommand.Parameters.Add(Treatmentname);
            updateCommand.Parameters.Add(Treatmentdescription);
            updateCommand.Parameters.Add(MedicalServiceProvidorId);

            updateCommand.ExecuteNonQuery();
            scon.Close();
        }
        public void updateTreatment(int id, string TreatmentName, string TreatmentDescription, int MedicalServiceProvidorID) {
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();
            string query = $@"Update Treatment set TreatmentName = '{TreatmentName}', TreatmentDescription = '{TreatmentDescription}', MedicalServiceProvidorID = '{MedicalServiceProvidorID}' WHERE TreatmentID = '{id}'";
            SqlParameter Treatmentname = new SqlParameter("@ClientName", SqlDbType.VarChar);
            SqlParameter Treatmentdescription = new SqlParameter("@ClientAdress", SqlDbType.VarChar);
            SqlParameter MedicalServiceProvidorId = new SqlParameter("@ClientEmail", SqlDbType.Int);
            Treatmentname.Value = TreatmentName.ToString();
            Treatmentdescription.Value = TreatmentDescription.ToString();
            MedicalServiceProvidorId.Value = MedicalServiceProvidorID.ToString();
            SqlCommand updateCommand = new(query, scon);
            updateCommand.Parameters.Add(Treatmentname);
            updateCommand.Parameters.Add(Treatmentdescription);
            updateCommand.Parameters.Add(MedicalServiceProvidorId);

            updateCommand.ExecuteNonQuery();
            scon.Close();
        }
        public void deleteTreatment(int id) {
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();
            string query = $@"DELETE from Treatment WHERE TreatmentID = '{id}'";
            SqlCommand com = new SqlCommand(query, scon);
            com.ExecuteNonQuery();
            scon.Close();
        }
    }
}
