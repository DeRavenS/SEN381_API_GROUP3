using SEN381_API_Group3.shared.models;
using SEN381_API_GROUP3.Database;
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

            SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[Client] ORDER BY ClientID OFFSET @offset ROWS FETCH NEXT @size ROWS ONLY;", scon);
            command.Parameters.AddWithValue("@offset", offset);
            command.Parameters.AddWithValue("@size", size);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    modules.Add(new Client(reader.GetInt32(0).ToString(), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetString(7), reader.GetString(8)));


                }
            }


            return modules;
        }

        //Get Client by ID
        public List<Client> getClientById(int id)
        {
            List<Client> modules = new List<Client>();
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();
            SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[Client] where ClientID = " + id, scon);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    modules.Add(new Client(reader.GetInt32(0).ToString(), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetString(7), reader.GetString(8)));

                }
            }


            return modules;
        }

        //Create / Add new Client
        public Client addNewClient(Client client)
        {
            string query = $@"INSERT INTO Client (ClientName, ClientSurname, ClientAddress,clientEmail,ClientPhonenumber,ClientPolicies,ClientStatus,ClientAdHocNotes)VALUES('{client.ClientName}','{client.ClientSurname}', '{client.ClientAddress}', '{client.ClientEmail}','{client.ClientPhoneNumber}','{client.Policies}', '{client.ClientStatus}', '{client.ClientAdHocNotes}')";

            SqlParameter Clientname = new SqlParameter("@ClientName", SqlDbType.VarChar);
            SqlParameter Clientsurname = new SqlParameter("@ClientSurname", SqlDbType.VarChar);
            SqlParameter Clientadress = new SqlParameter("@ClientAdress", SqlDbType.VarChar);
            SqlParameter Clientemail = new SqlParameter("@ClientEmail", SqlDbType.VarChar);
            SqlParameter ClientphoneNumber = new SqlParameter("@ClientPhonenumber", SqlDbType.VarChar);
            SqlParameter Clientpolicies = new SqlParameter("@ClientPolicies", SqlDbType.VarChar);
            SqlParameter Clientstatus = new SqlParameter("@ClientStatus", SqlDbType.VarChar);
            SqlParameter ClientadHocNotes = new SqlParameter("@ClientAdHocNotes", SqlDbType.VarChar);


            Clientname.Value = client.ClientName.ToString();
            Clientsurname.Value = client.ClientSurname.ToString();
            Clientadress.Value = client.ClientAddress.ToString();
            Clientemail.Value = client.ClientEmail.ToString();
            ClientphoneNumber.Value = client.ClientPhoneNumber.ToString();
            Clientpolicies.Value = client.Policies.ToString();
            Clientstatus.Value = client.ClientStatus.ToString();
            ClientadHocNotes.Value = client.ClientAdHocNotes.ToString();

            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();


            SqlCommand insertCommand = new SqlCommand(query, scon);
            insertCommand.Parameters.Add(Clientname);
            insertCommand.Parameters.Add(Clientsurname);
            insertCommand.Parameters.Add(Clientadress);
            insertCommand.Parameters.Add(Clientemail);
            insertCommand.Parameters.Add(ClientphoneNumber);
            insertCommand.Parameters.Add(Clientpolicies);
            insertCommand.Parameters.Add(Clientstatus);
            insertCommand.Parameters.Add(ClientadHocNotes);

            insertCommand.ExecuteNonQuery();

            scon.Close();
            return client;
        }

        //Update Client
        public Client updateClient(string id, Client client)
        {
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();
            string query = $@"Update Client set ClientName = '{client.ClientName}',ClientSurname = '{client.ClientSurname}', ClientAddress = '{client.ClientAddress}', ClientEmail = '{client.ClientEmail}',ClientPhonenumber = '{client.ClientPhoneNumber}',ClientPolicies = '{client.Policies}',ClientStatus = '{client.ClientStatus}',ClientAdHocNotes = '{client.ClientAdHocNotes}' WHERE ClientID = '{id}'";
            SqlParameter Clientname = new SqlParameter("@ClientName", SqlDbType.VarChar);
            SqlParameter Clientsurname = new SqlParameter("@ClientSurname", SqlDbType.VarChar);
            SqlParameter Clientadress = new SqlParameter("@ClientAdress", SqlDbType.VarChar);
            SqlParameter Clientemail = new SqlParameter("@ClientEmail", SqlDbType.VarChar);
            SqlParameter ClientphoneNumber = new SqlParameter("@ClientPhonenumber", SqlDbType.VarChar);
            SqlParameter Clientpolicies = new SqlParameter("@ClientPolicies", SqlDbType.VarChar);
            SqlParameter Clientstatus = new SqlParameter("@ClientStatus", SqlDbType.VarChar);
            SqlParameter ClientadHocNotes = new SqlParameter("@ClientAdHocNotes", SqlDbType.VarChar);


            Clientname.Value = client.ClientName.ToString();
            Clientsurname.Value = client.ClientSurname.ToString();
            Clientadress.Value = client.ClientAddress.ToString();
            Clientemail.Value = client.ClientEmail.ToString();
            ClientphoneNumber.Value = client.ClientPhoneNumber.ToString();
            Clientpolicies.Value = client.Policies.ToString();
            Clientstatus.Value = client.ClientStatus.ToString();
            ClientadHocNotes.Value = client.ClientAdHocNotes.ToString();

            SqlCommand updateCommand = new(query, scon);

            updateCommand.Parameters.Add(Clientname);
            updateCommand.Parameters.Add(Clientsurname);
            updateCommand.Parameters.Add(Clientadress);
            updateCommand.Parameters.Add(Clientemail);
            updateCommand.Parameters.Add(ClientphoneNumber);
            updateCommand.Parameters.Add(Clientpolicies);
            updateCommand.Parameters.Add(Clientstatus);
            updateCommand.Parameters.Add(ClientadHocNotes);

            updateCommand.ExecuteNonQuery();
            scon.Close();
            return client;
        }

        //Delete Client
        public void deleteClient(string id)
        {
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();
            string query = $@"DELETE from Client WHERE ClientID = '{id}'";
            SqlCommand com = new SqlCommand(query, scon);
            com.ExecuteNonQuery();
            scon.Close();
        }

    }
}
