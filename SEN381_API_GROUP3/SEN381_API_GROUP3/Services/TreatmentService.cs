using Microsoft.AspNetCore.Mvc.RazorPages;
using SEN381_API_Group3.shared.models;
using SEN381_API_GROUP3.Database;
using SEN381_API_GROUP3.shared.models;
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

            //Query for treatments
            string query = "dbo.GetAllTreatments";
            SqlCommand command = new SqlCommand(query, scon);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@offset", offset);
            command.Parameters.AddWithValue("@size", size);
            SqlDataReader reader = command.ExecuteReader();

            List<string> ids = new List<string>();
            Dictionary<string, Treatment> dic = new Dictionary<string, Treatment>();// Used for storing existing ids
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    dic.Add(reader.GetString(0),new Treatment(reader.GetString(0), reader.GetString(1), reader.GetString(2), new List<MedicalServiceProviderTreatment>()));
                    ids.Add(reader.GetString(0));
                }
            }
            reader.Close();

            //Joining Table
            SqlCommand command2 = new SqlCommand("", scon);
            //Query for providers
            List<string> idText = new List<string>();
            int idCount = 0;
            if (ids.Count != 0)
            {
                foreach (string id in ids)
                {
                    idCount++;
                    command2.Parameters.AddWithValue($"@id{idCount}", id);
                    idText.Add($"@id{idCount}");
                }

                query = "Select MSP.MedicalServiceProviderID,MSP.MedicalServiceProviderName,MSP.MedicalServiceProviderAddress,MSP.MedicalServiceProviderEmail,MSP.MedicalServiceProviderPhone," +
                    "    MSPT.MSPTID,MSPT.ProviderStatus,MSPT.MedicalServiceProviderID,MSPT.TreatmentID " +
                    "    FROM MedicalServiceProvider MSP " +
                    "    INNER JOIN MedicalServiceProviderTreatment MSPT " +
                    "           ON MSPT.MedicalServiceProviderID = MSP.MedicalServiceProviderID " +
                    $"    WHERE MSPT.TreatmentID in ({string.Join(", ", idText)})";
                command2.CommandText = query;
                SqlDataReader reader2 = command2.ExecuteReader();
                if (reader2.HasRows)
                {
                    while (reader2.Read())
                    {
                        if (dic.TryGetValue(reader2.GetString(8), out Treatment treatment))
                        {
                            dic[reader2.GetString(8)].MedicalServiceProviderTreatments.Add(new MedicalServiceProviderTreatment(reader2.GetInt32(5), reader2.GetString(6), new MedicalServiceProvider(reader2.GetInt32(0).ToString(), reader2.GetString(1), reader2.GetString(2), reader2.GetString(3), reader2.GetString(4))));
                        }
                    }
                }
            }
            
            modules = dic.Values.ToList();
            return modules;
        }
        public Treatment getTreatmentsByID(string id) {
            List<Treatment> modules = new List<Treatment>();
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();

            string query = "dbo.GetTreatmentByID";
            SqlCommand command = new SqlCommand(query, scon);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    modules.Add(new Treatment(reader.GetString(0), reader.GetString(1), reader.GetString(2), new List<MedicalServiceProviderTreatment>()));
                }
            }
            reader.Close();
            //Query for providers
            query = $"Select * FROM MedicalServiceProvider MSP " +
                "    INNER JOIN MedicalServiceProviderTreatment MSPT " +
                "           ON MSPT.MedicalServiceProviderID = MSP.MedicalServiceProviderID " +
                "    WHERE MSPT.TreatmentID = @id";
            SqlCommand command2 = new SqlCommand(query, scon);
            command2.Parameters.AddWithValue("@id", id);
            SqlDataReader reader2 = command2.ExecuteReader();
            if (reader2.HasRows)
            {
                while (reader2.Read())
                {
                    modules[0].MedicalServiceProviderTreatments.Add(new MedicalServiceProviderTreatment(reader2.GetInt32(5), reader2.GetString(6), new MedicalServiceProvider(reader2.GetInt32(0).ToString(), reader2.GetString(1), reader2.GetString(2), reader2.GetString(3), reader2.GetString(4))));
                }
            }
            return modules[0];
        
        }
        public Treatment addNewTreatment(Treatment treatment) {

            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();
            string query = $@"dbo.InsertTreatment";

            SqlCommand updateCommand = new SqlCommand(query, scon);
            updateCommand.CommandType = CommandType.StoredProcedure;

            updateCommand.Parameters.AddWithValue("@treatmentID", treatment.TreatmentID);
            updateCommand.Parameters.AddWithValue("@treatmentName",treatment.TreatmentName);
            updateCommand.Parameters.AddWithValue("@treatmentDescription", treatment.TreatmentDescription);

            updateCommand.ExecuteNonQuery();
            //Joining Table
            query = $@"dbo.InsertMedicalServiceProviderTreatment";
            updateCommand.CommandText = query;
            foreach (MedicalServiceProviderTreatment item in treatment.MedicalServiceProviderTreatments)
            {
                updateCommand.Parameters.Clear();

                updateCommand.Parameters.AddWithValue("@treatmentID", treatment.TreatmentID);
                updateCommand.Parameters.AddWithValue("@providerID", int.Parse(item.MedicalServiceProvidor.PolicyProviderID));
                updateCommand.Parameters.AddWithValue("@providerStatus", item.ProviderStatus);

                updateCommand.ExecuteNonQuery();
            }

            scon.Close();
            return treatment;
        }
        public Treatment updateTreatment(string id,Treatment treatment) {
            
            string query = $@"dbo.UpdateTreatment";
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();

            SqlCommand updateCommand = new SqlCommand(query, scon);
            updateCommand.CommandType = CommandType.StoredProcedure;

            updateCommand.Parameters.AddWithValue("@treatmentName",treatment.TreatmentName);
            updateCommand.Parameters.AddWithValue("@treatmentDescription", treatment.TreatmentDescription);
            updateCommand.Parameters.AddWithValue("@id", treatment.TreatmentID);
            updateCommand.ExecuteNonQuery();

            query = $@"dbo.InsertMedicalServiceProviderTreatment";
            foreach (MedicalServiceProviderTreatment item in treatment.MedicalServiceProviderTreatments)
            {
                updateCommand.Parameters.Clear();
                updateCommand.CommandText = query;

                updateCommand.Parameters.AddWithValue("@treatmentID", treatment.TreatmentID);
                updateCommand.Parameters.AddWithValue("@providerID", int.Parse(item.MedicalServiceProvidor.PolicyProviderID));
                updateCommand.Parameters.AddWithValue("@providerStatus", item.ProviderStatus);

                updateCommand.ExecuteNonQuery();
            }

            scon.Close();
            return treatment;
        }
        public Treatment deleteTreatment(string id) {
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();
            string query = $@"dbo.DeleteTreatment";

            SqlCommand comMSPT = new SqlCommand(query, scon);
            comMSPT.CommandType = CommandType.StoredProcedure;

            comMSPT.Parameters.AddWithValue("@id", id);
            comMSPT.ExecuteNonQuery();
            scon.Close();

            return new Treatment();
        }
    }
}
