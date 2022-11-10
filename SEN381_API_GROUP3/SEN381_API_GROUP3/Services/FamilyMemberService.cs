
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

            SqlCommand command = new SqlCommand("dbo.GetAllFamilyMembers", scon);
            command.CommandType = CommandType.StoredProcedure;
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

            SqlCommand command = new SqlCommand($@"dbo.GetFamilyMemberByID", scon);
            command.CommandType = CommandType.StoredProcedure;
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
            string query = @"dbo.InsertFamilyMember";

            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();


            SqlCommand insertCommand = new SqlCommand(query, scon);
            insertCommand.CommandType = CommandType.StoredProcedure;

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
            string query = $@"dbo.UpdateFamilyMember";

            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();


            SqlCommand insertCommand = new SqlCommand(query, scon);
            insertCommand.CommandType = CommandType.StoredProcedure;

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
            string query = $@"dbo.DeleteFamilyMember";
            SqlCommand com = new SqlCommand(query, scon);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@id",id);

            com.ExecuteNonQuery();
            scon.Close();
            return new FamilyMember();
        }

    }
}
