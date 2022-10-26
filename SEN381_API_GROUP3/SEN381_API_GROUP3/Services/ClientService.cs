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
        public void addNewClient(string ClientName, string ClientSurname, string ClientAdress, string ClientEmail, string ClientPhonenumber, string ClientPolicies, string ClientStatus, string ClientAdHocNotes)
        {
            string query = $@"INSERT INTO Client (ClientName, ClientSurname, ClientAddress,clientEmail,ClientPhonenumber,ClientPolicies,ClientStatus,ClientAdHocNotes)VALUES('{ClientName}','{ClientSurname}', '{ClientAdress}', '{ClientEmail}','{ClientPhonenumber}','{ClientPolicies}', '{ClientStatus}', '{ClientAdHocNotes}')";

            SqlParameter Clientname = new SqlParameter("@ClientName", SqlDbType.VarChar);
            SqlParameter Clientsurname = new SqlParameter("@ClientSurname", SqlDbType.VarChar);
            SqlParameter Clientadress = new SqlParameter("@ClientAdress", SqlDbType.VarChar);
            SqlParameter Clientemail = new SqlParameter("@ClientEmail", SqlDbType.VarChar);
            SqlParameter ClientphoneNumber = new SqlParameter("@ClientPhonenumber", SqlDbType.VarChar);
            SqlParameter Clientpolicies = new SqlParameter("@ClientPolicies", SqlDbType.VarChar);
            SqlParameter Clientstatus = new SqlParameter("@ClientStatus", SqlDbType.VarChar);
            SqlParameter ClientadHocNotes = new SqlParameter("@ClientAdHocNotes", SqlDbType.VarChar);


            Clientname.Value = ClientName.ToString();
            Clientsurname.Value = ClientSurname.ToString();
            Clientadress.Value = ClientAdress.ToString();
            Clientemail.Value = ClientEmail.ToString();
            ClientphoneNumber.Value = ClientPhonenumber.ToString();
            Clientpolicies.Value = ClientPolicies.ToString();
            Clientstatus.Value = ClientStatus.ToString();
            ClientadHocNotes.Value = ClientAdHocNotes.ToString();


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
        }

        //Update Client
        public void updateClient(int id, string ClientName, string ClientAdress, string ClientEmail)
        {
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();
            string query = $@"Update Client set ClientName = '{ClientName}', ClientAddress = '{ClientAdress}', clientEmail = '{ClientEmail}' WHERE ClientID = '{id}'";
            SqlParameter Clientname = new SqlParameter("@ClientName", SqlDbType.VarChar);
            SqlParameter Clientadress = new SqlParameter("@ClientAdress", SqlDbType.VarChar);
            SqlParameter Clientemail = new SqlParameter("@ClientEmail", SqlDbType.VarChar);
            Clientname.Value = ClientName.ToString();
            Clientadress.Value = ClientAdress.ToString();
            Clientemail.Value = ClientEmail.ToString();
            SqlCommand updateCommand = new(query, scon);
            updateCommand.Parameters.Add(Clientname);
            updateCommand.Parameters.Add(Clientadress);
            updateCommand.Parameters.Add(Clientemail);

            updateCommand.ExecuteNonQuery();
            scon.Close();
        }

        //Delete Client
        public void deleteClient(int id)
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
