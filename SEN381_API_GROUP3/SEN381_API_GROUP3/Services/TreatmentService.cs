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
            //Query for treatments
            string query = "SELECT * FROM [dbo].[Treatment] T " +
                "           ORDER BY T.TreatmentID,MSPT.ProviderStatus " +
                "           OFFSET @offset ROWS FETCH NEXT @size ROWS ONLY;";
            SqlCommand command = new SqlCommand(query, scon);
            command.Parameters.AddWithValue("@offset", offset);
            command.Parameters.AddWithValue("@size", size);
            SqlDataReader reader = command.ExecuteReader();

            List<string> ids = new List<string>();
            Dictionary<string, Treatment> dic = new Dictionary<string, Treatment>();// Used for storing existing ids
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    dic.Add(reader.GetString(0),new Treatment(reader.GetString(0), reader.GetString(1), reader.GetString(2), new List<MedicalServiceProvider>()));
                    ids.Add(reader.GetString(0));
                }
            }

            //Query for providers
            query = $"Select * FROM MedicalServiceProvider " +
                "    INNER JOIN MedicalServiceProvider MSP " +
                "           ON MSPT.MedicalServiceProvidorID = MSP.MedicalServiceProvidorID " +
                "    WHERE MSPT.TreatmentID in @ids";
            SqlCommand command2 = new SqlCommand(query,scon);
            command2.Parameters.AddWithValue("@ids",ids);
            SqlDataReader reader2 = command.ExecuteReader();

            if (reader2.HasRows)
            {
                while (reader2.Read())
                {
                    if(dic.TryGetValue(reader2.GetString(8), out Treatment treatment)) {
                        dic[reader2.GetString(8)].MedicalServiceProviders.Add(new MedicalServiceProvider(reader2.GetString(0),reader2.GetString(1),reader2.GetString(2),reader2.GetString(3),reader2.GetString(4)));
                    }
                }
            }
            modules = dic.Values.ToList();
            return modules;
        }
        public List<Treatment> getTreatmentsByID(string id) {
            List<Treatment> modules = new List<Treatment>();
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();

            string query = "SELECT * FROM [dbo].[Treatment] T " +
                "           ORDER BY T.TreatmentID" +
                "           WHERE T.TreatmentID=@id";
            SqlCommand command = new SqlCommand(query, scon);
            command.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    modules.Add(new Treatment(reader.GetInt32(0).ToString(), reader.GetString(1), reader.GetString(2), new List<MedicalServiceProvider>()));
                }
            }

            //Query for providers
            query = $"Select * FROM MedicalServiceProvider " +
                "    INNER JOIN MedicalServiceProvider MSP " +
                "           ON MSPT.MedicalServiceProvidorID = MSP.MedicalServiceProvidorID " +
                "    WHERE MSPT.TreatmentID = @id";
            SqlCommand command2 = new SqlCommand(query, scon);
            command2.Parameters.AddWithValue("@id", id);
            SqlDataReader reader2 = command.ExecuteReader();

            if (reader2.HasRows)
            {
                while (reader2.Read())
                {
                    modules[0].MedicalServiceProviders.Add(new MedicalServiceProvider(reader2.GetString(0), reader2.GetString(1), reader2.GetString(2), reader2.GetString(3), reader2.GetString(4)));
                }
            }
            return modules;
        
        }
        public Treatment addNewTreatment(Treatment treatment) {
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();
            string query = $@"INSERT INTO Treatment (TreatmentName, TreatmentDescription)VALUES(@treatmentName, @treatmentDescription)";
            SqlCommand updateCommand = new(query, scon);
            updateCommand.Parameters.AddWithValue("@treatmentName",treatment.TreatmentName);
            updateCommand.Parameters.AddWithValue("@treatmentDescription", treatment.TreatmentDescription);

            updateCommand.ExecuteNonQuery();

            query = $@"INSERT INTO MedicalServiceProviderTreatment (TreatmentID, ProviderID)VALUES(@treatmentID, @providerID)";
            SqlCommand updateJoiningTableCommand = new(query, scon);
            updateJoiningTableCommand.Parameters.AddWithValue("@treatmentID", treatment.TreatmentID);
            updateJoiningTableCommand.Parameters.AddWithValue("@treatmentDescription", treatment.TreatmentDescription);

            updateJoiningTableCommand.ExecuteNonQuery();
            scon.Close();
            return treatment;
        }
        public Treatment updateTreatment(string id,Treatment treatment) {
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();
            string query = $@"Update Treatment set TreatmentName = @treatmentName, TreatmentDescription = @treatmentDescription WHERE TreatmentID = @id";
            SqlCommand updateCommand = new(query, scon);
            updateCommand.Parameters.AddWithValue("@treatmentName",treatment.TreatmentName);
            updateCommand.Parameters.AddWithValue("@treatmentDescription", treatment.TreatmentDescription);
            updateCommand.ExecuteNonQuery();
            scon.Close();
            return treatment;
        }
        public Treatment deleteTreatment(string id) {
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();
            //Joining MedicalServiceProviderTreatment
            string query = $@"DELETE from MedicalServiceProviderTreatment WHERE TreatmentID = @id";
            SqlCommand comMSPT = new SqlCommand(query, scon);
            comMSPT.Parameters.AddWithValue("@id", id);
            comMSPT.ExecuteNonQuery();
            //Joining PolicyTreatmentCoverage
            query = $@"DELETE from PolicyTreatmentCoverage WHERE TreatmentID = @id";
            SqlCommand comPTC = new SqlCommand(query, scon);
            comPTC.Parameters.AddWithValue("@id", id);
            comPTC.ExecuteNonQuery();
            //Joining MedicalConditionTreatment
            query = $@"DELETE from MedicalConditionTreatment WHERE TreatmentID = @id";
            SqlCommand comMCT = new SqlCommand(query, scon);
            comMCT.Parameters.AddWithValue("@id", id);
            comMCT.ExecuteNonQuery();
            //Joining ClaimMedicalConditionTreatment
            query = $@"DELETE from ClaimMedicalConditionTreatment WHERE TreatmentID = @id";
            SqlCommand comCMCT = new SqlCommand(query, scon);
            comCMCT.Parameters.AddWithValue("@id", id);
            comCMCT.ExecuteNonQuery();
            //Treatment Table
            query = $@"DELETE from Treatment WHERE TreatmentID = @id";
            SqlCommand comFinal = new SqlCommand(query, scon);
            comFinal.Parameters.AddWithValue("@id", id);
            comFinal.ExecuteNonQuery();
            scon.Close();

            return new Treatment();
        }
    }
}
