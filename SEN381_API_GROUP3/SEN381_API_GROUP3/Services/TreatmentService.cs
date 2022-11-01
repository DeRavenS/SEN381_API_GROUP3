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
            string query = "SELECT * FROM [dbo].[Treatment] T " +
                "           ORDER BY T.TreatmentID" +
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

            string query = "SELECT * FROM [dbo].[Treatment] T " +
                "           WHERE T.TreatmentID=@id ";
            SqlCommand command = new SqlCommand(query, scon);
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

            Treatment treatment = null;
            if (reader2.HasRows)
            {
                while (reader2.Read())
                {
                    modules[0].MedicalServiceProviderTreatments.Add(new MedicalServiceProviderTreatment(reader2.GetInt32(5), reader2.GetString(6), new MedicalServiceProvider(reader2.GetInt32(0).ToString(), reader2.GetString(1), reader2.GetString(2), reader2.GetString(3), reader2.GetString(4))));
                }
                return modules[0];
            }
            else return treatment;
        
        }
        public Treatment addNewTreatment(Treatment treatment) {
            Console.WriteLine("Start Intsert");
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();
            string query = $@"INSERT INTO Treatment (TreatmentID,TreatmentName, TreatmentDescription)VALUES(@treatmentID,@treatmentName, @treatmentDescription)";
            SqlCommand updateCommand = new SqlCommand(query, scon);
            updateCommand.Parameters.AddWithValue("@treatmentID", treatment.TreatmentID);
            updateCommand.Parameters.AddWithValue("@treatmentName",treatment.TreatmentName);
            updateCommand.Parameters.AddWithValue("@treatmentDescription", treatment.TreatmentDescription);

            updateCommand.ExecuteNonQuery();
            //Joining Table
            SqlCommand updateJoiningTableCommand = new("", scon);
            query = $@"INSERT INTO MedicalServiceProviderTreatment (TreatmentID, MedicalServiceProviderID,ProviderStatus)VALUES(@treatmentID, @providerID,@providerStatus)";
            foreach (MedicalServiceProviderTreatment item in treatment.MedicalServiceProviderTreatments)
            {
                updateJoiningTableCommand.CommandText = query;
               
                updateJoiningTableCommand.Parameters.AddWithValue("@treatmentID", treatment.TreatmentID);
                updateJoiningTableCommand.Parameters.AddWithValue("@providerID", int.Parse(item.MedicalServiceProvidor.PolicyProviderID));
                updateJoiningTableCommand.Parameters.AddWithValue("@providerStatus", item.ProviderStatus);

                updateJoiningTableCommand.ExecuteNonQuery();
                updateJoiningTableCommand.Parameters.Clear();
            }

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
            updateCommand.Parameters.AddWithValue("@id", treatment.TreatmentID);
            updateCommand.ExecuteNonQuery();

            //Joining Table
            updateCommand.Parameters.Clear();
            query = $"DELETE FROM MedicalServiceProviderTreatment WHERE TreatmentID=@id";
            updateCommand.Parameters.AddWithValue("@id",treatment.TreatmentID);
            updateCommand.CommandText = query;
            updateCommand.ExecuteNonQuery();

            query = $@"INSERT INTO MedicalServiceProviderTreatment (TreatmentID, MedicalServiceProviderID,ProviderStatus)VALUES(@treatmentID, @providerID,@providerStatus)";
            foreach (MedicalServiceProviderTreatment item in treatment.MedicalServiceProviderTreatments)
            {
                updateCommand.CommandText = query;

                updateCommand.Parameters.AddWithValue("@treatmentID", treatment.TreatmentID);
                updateCommand.Parameters.AddWithValue("@providerID", int.Parse(item.MedicalServiceProvidor.PolicyProviderID));
                updateCommand.Parameters.AddWithValue("@providerStatus", item.ProviderStatus);

                updateCommand.ExecuteNonQuery();
                updateCommand.Parameters.Clear();
            }

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
            //Joining ClaimMedicalConditionTreatment
            query = $@"DELETE from ClaimMedicalConditionTreatment WHERE MCTID IN (SELECT TreatmentID FROM MedicalConditionTreatment WHERE TreatmentID=@id)";
            SqlCommand comCMCT = new SqlCommand(query, scon);
            comCMCT.Parameters.AddWithValue("@id", id);
            comCMCT.ExecuteNonQuery();
            //Joining MedicalConditionTreatment
            query = $@"DELETE from MedicalConditionTreatment WHERE TreatmentID = @id";
            SqlCommand comMCT = new SqlCommand(query, scon);
            comMCT.Parameters.AddWithValue("@id", id);
            comMCT.ExecuteNonQuery();
            //Treatment Table
            query = $@"DELETE from Treatment WHERE TreatmentID = @id";
            SqlCommand comFinal = new SqlCommand(query, scon);
            comFinal.Parameters.AddWithValue("@id", id);
            int effectedRows=comFinal.ExecuteNonQuery();
            scon.Close();

            return effectedRows!=0? new Treatment():null;
        }
    }
}
