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

            string query = "dbo.GetAllClients";

            SqlCommand command = new SqlCommand(query, scon);
            command.CommandType = CommandType.StoredProcedure;

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
            return modules;
        }

        //Get Client by ID
        public Client getClientById(string id)
        {
            List<Client> modules = new List<Client>();
            string query = "dbo.GetClientByID";

            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();

            SqlCommand command = new SqlCommand(query, scon);
            command.CommandType = CommandType.StoredProcedure;
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
            string query = $@"dbo.InsertClient";

            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();


            SqlCommand insertCommand = new SqlCommand(query, scon);
            insertCommand.CommandType = CommandType.StoredProcedure;

            string clientID = primaryKey();
            insertCommand.Parameters.AddWithValue("@id", clientID);
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
                query = "dbo.InsertClientPolicy";
                insertCommand.Parameters.AddWithValue("@clientID", clientID);
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
            string query = $@"dbo.UpdateClient";

            SqlCommand updateCommand = new(query, scon);
            updateCommand.CommandType = CommandType.StoredProcedure;

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
                query = "dbo.UpdateClientPolicy";
                updateCommand.Parameters.AddWithValue("@clientID", client.ClientID);
                updateCommand.Parameters.AddWithValue("@policyID", client.Policy.PolicyID);
                updateCommand.CommandText = query;
                updateCommand.ExecuteNonQuery();
            }
            else
            {
                updateCommand.Parameters.Clear();
                query = "dbo.DeleteClientPolicy";
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
            string query = $@"dbo.DeleteClient";
            SqlCommand com = new SqlCommand(query, scon);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@id",id);
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
