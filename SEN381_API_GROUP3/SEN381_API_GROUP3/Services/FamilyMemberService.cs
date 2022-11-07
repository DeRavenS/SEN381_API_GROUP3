
using SEN381_API_Group3.shared.models;
using SEN381_API_GROUP3.Database;
using System.Data;
using System.Data.SqlClient;

namespace SEN381_API_GROUP3.Services
{
    public class FamilyMemberService
    {

        // Get all family members
        public List<FamilyMember> getAllFamilyMembers(string clientID)
        {
            List<FamilyMember> modules = new List<FamilyMember>();
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();

            SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[FamilyMember] WHERE ClientID = @clientID ORDER BY FamilyMemberID ;", scon);
            command.Parameters.AddWithValue("@clientID", clientID);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    modules.Add(new FamilyMember(reader.GetInt32(0).ToString(), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetString(7)));
                }
            }


            return modules;
        }


        // Get familymember by ID
        public FamilyMember getFamilyMemberById(int id)
        {
            List<FamilyMember> modules = new List<FamilyMember>();
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();
            SqlCommand command = new SqlCommand($@"SELECT * FROM [dbo].[FamilyMember] where FamilyMemberID = @id", scon);
            command.Parameters.AddWithValue("@id",id);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    modules.Add(new FamilyMember(reader.GetInt32(0).ToString(), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetString(7)));
                }
            }


            return modules[0];
        }

        // Add new Family member
        public FamilyMember addNewFamilyMember(FamilyMember family)
        {
            string query = @"INSERT INTO
                             FamilyMember(FamilyMemberName, FamilyMemberSurname, FamilyMemberPhone,FamilyMemberEmail, FamilyMemberAddress, FamilyRole,ClientID)
                             VALUES(@FamilyMemberName, @FamilyMemberSurname,@FamilyMemberPhone,@FamilyMemberEmail,@FamilyMemberAddress, @FamilyRole,@ClientID)";

            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();


            SqlCommand insertCommand = new SqlCommand(query, scon);
            insertCommand.Parameters.AddWithValue("@FamilyMemberName", family.MemberName);
            insertCommand.Parameters.AddWithValue("@FamilyMemberSurname", family.MemberSurname);
            insertCommand.Parameters.AddWithValue("@FamilyMemberPhone", family.PhoneNumber);
            insertCommand.Parameters.AddWithValue("@FamilyMemberEmail", family.Email);
            insertCommand.Parameters.AddWithValue("@FamilyMemberAddress", family.Address);
            insertCommand.Parameters.AddWithValue("@FamilyRole", family.Role);
            insertCommand.Parameters.AddWithValue("@ClientID", family.ClientID);

            insertCommand.ExecuteNonQuery();
            scon.Close();
            return family;
        }

        // Update Family member
        public FamilyMember updateFamilyMember(FamilyMember family)
        {
            string query = $@"UPDATE FamilyMember 
                              SET FamilyMemberName = @FamilyMemberName, FamilyMemberSurname = @FamilyMemberSurname, FamilyMemberPhone = @FamilyMemberPhone,FamilyMemberEmail = @FamilyMemberPhone,
                                  FamilyMemberAddress = @FamilyMemberAddress, FamilyRole = @FamilyRole, ClientID = @ClientID
                              WHERE FamilyMemberID = @id;";

            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();


            SqlCommand insertCommand = new SqlCommand(query, scon);
            insertCommand.Parameters.AddWithValue("@FamilyMemberName", family.MemberName);
            insertCommand.Parameters.AddWithValue("@FamilyMemberSurname", family.MemberSurname);
            insertCommand.Parameters.AddWithValue("@FamilyMemberPhone", family.PhoneNumber);
            insertCommand.Parameters.AddWithValue("@FamilyMemberEmail", family.Email);
            insertCommand.Parameters.AddWithValue("@FamilyMemberAddress", family.Address);
            insertCommand.Parameters.AddWithValue("@FamilyRole", family.Role);
            insertCommand.Parameters.AddWithValue("@ClientID", family.ClientID);
            insertCommand.Parameters.AddWithValue("@id", family.MemberId);

            insertCommand.ExecuteNonQuery();
            scon.Close();
            return family;
        }


        // Delete family member
        public FamilyMember deleteFamilyMember(int id)
        {
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();
            string query = $@"DELETE from FamilyMember WHERE FamilyMemberID = @id";
            SqlCommand com = new SqlCommand(query, scon);
            com.Parameters.AddWithValue("@id",id);

            com.ExecuteNonQuery();
            scon.Close();
            return new FamilyMember();
        }

    }
}
