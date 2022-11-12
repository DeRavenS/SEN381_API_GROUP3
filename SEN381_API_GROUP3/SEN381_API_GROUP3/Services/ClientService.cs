using SEN381_API_Group3.shared.models;
using SEN381_API_GROUP3.Database;
using System;
using System.Data;
using System.Data.SqlClient;

namespace SEN381_API_GROUP3.Services
{
    public class ClientService
    {

        //Get Clients
        public List<Client> getClients(int page, int size)
        {
            int offset = (page - 1) * size;
            List<Client> modules = new List<Client>();
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();
            string query = "SELECT " +
                "                 C.ClientID,C.ClientName,C.ClientSurname,C.ClientAddress,C.clientEmail,C.ClientPhonenumber,C.ClientStatus,C.ClientAdHocNotes," +
                "                 P.PolicyID,P.PolicyName,PS.startTime,PS.endTime, PS.PolicyStatusDate,PS.PolicyID " +
                "           FROM " +
                "               [dbo].[Client] C " +
                "           LEFT JOIN " +
                "               ClientPolicy CP ON C.ClientID=CP.ClientID " +
                "           LEFT JOIN" +
                "               Policy P ON CP.PolicyID = P.PolicyID " +
                "           LEFT JOIN " +
                "               PolicyStatus PS ON P.PolicyID=PS.PolicyID" +
                "           WHERE C.ClientID IN (SELECT ClientID FROM Client ORDER BY ClientID OFFSET @offset ROWS FETCH NEXT @size ROWS ONLY)" +
                "           ORDER BY C.ClientID ASC,PS.PolicyID ASC,PS.PolicyStatusDate DESC;";
            SqlCommand command = new SqlCommand(query, scon);
            command.Parameters.AddWithValue("@offset", offset);
            command.Parameters.AddWithValue("@size", size);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                int index = 0;
                while (reader.Read())
                {
                    index = modules.FindIndex((x) => x.ClientID == reader.GetString(0));

                    if (index == -1)
                    {
                        // Checking potential null values from LEFT JOIN
                        ClientPolicy? policy = !reader.IsDBNull(8)? new ClientPolicy(reader.GetInt32(8).ToString(), reader.GetString(9), getPolicyStatus(reader.IsDBNull(10) ? null : reader.GetDateTime(10), reader.IsDBNull(11) ? null : reader.GetDateTime(11))) : null;

                        modules.Add(new Client(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), policy, reader.GetString(6), reader.GetString(7)));
                    }

                }
            }

            Console.WriteLine(modules.Count);
            return modules;
        }

        //Get Client by ID
        public Client getClientById(string id)
        {
            string query = "SELECT " +
                "                 C.ClientID,C.ClientName,C.ClientSurname,C.ClientAddress,C.clientEmail,C.ClientPhonenumber,C.ClientStatus,C.ClientAdHocNotes," +
                "                 P.PolicyID,P.PolicyName,PS.startTime,PS.endTime, PS.PolicyStatusDate,PS.PolicyID " +
                "           FROM " +
                "               [dbo].[Client] C " +
                "           LEFT JOIN " +
                "               ClientPolicy CP ON C.ClientID=CP.ClientID " +
                "           LEFT JOIN" +
                "               Policy P ON CP.PolicyID = P.PolicyID " +
                "           LEFT JOIN " +
                "               PolicyStatus PS ON P.PolicyID=PS.PolicyID" +
                "           WHERE C.ClientID =@id" +
                "           ORDER BY C.ClientID ASC,PS.PolicyID ASC,PS.PolicyStatusDate DESC;";

            List<Client> modules = new List<Client>();
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();
            SqlCommand command = new SqlCommand(query, scon);
            command.Parameters.AddWithValue("@id",id);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                int index = 0;
                while (reader.Read())
                {
                    index = modules.FindIndex((x) => x.ClientID == reader.GetString(0));

                    if (index == -1)
                    {
                        // Checking potential null values from LEFT JOIN
                        ClientPolicy? policy = !reader.IsDBNull(8) ? new ClientPolicy(reader.GetInt32(8).ToString(), reader.GetString(9), getPolicyStatus(reader.IsDBNull(10) ? null : reader.GetDateTime(10), reader.IsDBNull(11) ? null : reader.GetDateTime(11))) : null;

                        modules.Add(new Client(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), policy, reader.GetString(6), reader.GetString(7)));
                    }
                }
            }


            return modules[0];
        }

        //Create / Add new Client
        public Client addNewClient(Client client)
        {
            Console.WriteLine("Inserting Client");
            string query = $@"INSERT INTO Client (ClientID, ClientName, ClientSurname, ClientAddress,clientEmail,ClientPhonenumber,ClientStatus,ClientAdHocNotes)
                              VALUES(@id,@ClientName,@ClientSurname, @ClientAddress, @ClientEmail,@ClientPhonenumber, @ClientStatus, @ClientAdHocNotes)";

            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();


            SqlCommand insertCommand = new SqlCommand(query, scon);

            insertCommand.Parameters.AddWithValue("@id", primaryKey());
            insertCommand.Parameters.AddWithValue("@ClientName", client.ClientName);
            insertCommand.Parameters.AddWithValue("@ClientSurname", client.ClientSurname);
            insertCommand.Parameters.AddWithValue("@ClientAddress", client.ClientAddress);
            insertCommand.Parameters.AddWithValue("@ClientEmail", client.ClientEmail);
            insertCommand.Parameters.AddWithValue("@ClientPhonenumber", client.ClientPhoneNumber);
            insertCommand.Parameters.AddWithValue("@ClientStatus", client.ClientStatus);
            insertCommand.Parameters.AddWithValue("@ClientAdHocNotes", client.ClientAdHocNotes);

            insertCommand.ExecuteNonQuery();

            if (client.Policy!=null && client.Policy.PolicyID != null)
            {
                insertCommand.Parameters.Clear();
                query = "INSERT INTO ClientPolicy(ClientID,PolicyID)" +
                    "    VALUES (@clientID,@policyID)";
                insertCommand.Parameters.AddWithValue("@clientID",client.ClientID);
                insertCommand.Parameters.AddWithValue("@policyID", client.Policy.PolicyID);
                insertCommand.CommandText = query;
                insertCommand.ExecuteNonQuery();
            }

            scon.Close();
            return client;
        }

        //Update Client
        public Client updateClient(string id, Client client)
        {
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();
            string query = $@"UPDATE Client SET ClientName = @ClientName, ClientSurname = @ClientSurname, ClientAddress = @ClientAddress, 
                                ClientEmail = @ClientEmail, ClientPhonenumber = @ClientPhonenumber, ClientStatus = @ClientStatus,
                                ClientAdHocNotes = @ClientAdHocNotes WHERE ClientID = @id";

            SqlCommand updateCommand = new(query, scon);

            updateCommand.Parameters.AddWithValue("@ClientName",client.ClientName);
            updateCommand.Parameters.AddWithValue("@ClientSurname", client.ClientSurname);
            updateCommand.Parameters.AddWithValue("@ClientAddress", client.ClientAddress);
            updateCommand.Parameters.AddWithValue("@ClientEmail", client.ClientEmail);
            updateCommand.Parameters.AddWithValue("@ClientPhonenumber", client.ClientPhoneNumber);
            updateCommand.Parameters.AddWithValue("@ClientStatus", client.ClientStatus);
            updateCommand.Parameters.AddWithValue("@ClientAdHocNotes", client.ClientAdHocNotes);
            updateCommand.Parameters.AddWithValue("@id", client.ClientID);

            updateCommand.ExecuteNonQuery();

            if (client.Policy != null && client.Policy.PolicyID!=null)
            {
                updateCommand.Parameters.Clear();
                query = "UPDATE ClientPolicy SET ClientID = @clientID, PolicyID = @policyID WHERE ClientID=@clientID";
                updateCommand.Parameters.AddWithValue("@clientID", client.ClientID);
                updateCommand.Parameters.AddWithValue("@policyID", client.Policy.PolicyID);
                updateCommand.CommandText = query;
                int rows = updateCommand.ExecuteNonQuery();
                if ( rows == 0)
                {
                    updateCommand.Parameters.Clear();
                    query = "INSERT INTO ClientPolicy(ClientID,PolicyID)" +
                    "    VALUES (@clientID,@policyID)";
                    updateCommand.Parameters.AddWithValue("@clientID", client.ClientID);
                    updateCommand.Parameters.AddWithValue("@policyID", client.Policy.PolicyID);
                    updateCommand.CommandText = query;
                    updateCommand.ExecuteNonQuery();
                };
            }
            else
            {
                updateCommand.Parameters.Clear();
                query = "DELETE FROM ClientPolicy WHERE ClientID = @clientID";
                updateCommand.Parameters.AddWithValue("@clientID", client.ClientID);
                updateCommand.CommandText = query;
                updateCommand.ExecuteNonQuery();
            }
            scon.Close();
            return client;
        }

        //Delete Client
        public Client deleteClient(string id)
        {
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();
            string query = $@"DELETE from ClientPolicy WHERE ClientID = @id";
            SqlCommand com = new SqlCommand(query, scon);
            com.Parameters.AddWithValue("@id",id);
            com.ExecuteNonQuery();

            query = $@"DELETE from FamilyMember WHERE ClientID = @id";
            com.CommandText = query;
            com.ExecuteNonQuery();

            query = $@"DELETE from Client WHERE ClientID = @id";
            com.CommandText = query;
            com.ExecuteNonQuery();

            scon.Close();
            return new Client();
        }
        private string primaryKey()
        {
            Random random = new Random();
            int digits = random.Next(80000000);
            string digits_ = digits.ToString();
            while (digits_.Length != 8)
            {
                digits_ = "0" + digits_;
            }
            string[] alphabetic = new string[5] { "G", "H", "J", "K", "L" };
            int letter_type = random.Next(alphabetic.Length);
            string letter = alphabetic[letter_type];
            string pk = letter + digits_;
            Console.WriteLine(pk);
            return pk;

        }

        private string getPolicyStatus(DateTime? start, DateTime? end)
        {
            string status = "";

            if (start == null && end == null) status = "Unspecified";
            else if ((start == null && end >= DateTime.Today) || (start <= DateTime.Today && (end == null || end >= DateTime.Today))) status = "Active";
            else if (start > DateTime.Today) status = "Coming Soon";
            return status;
        }

    }
}
