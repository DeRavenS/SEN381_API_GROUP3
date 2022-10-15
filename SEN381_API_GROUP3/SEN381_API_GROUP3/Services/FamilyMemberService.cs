
using SEN381_API_Group3.shared.models;
using SEN381_API_GROUP3.Database;
using System.Data.SqlClient;
namespace SEN381_API_GROUP3.Services
{
    public class FamilyMemberService
    {

        // Get all family members
        public List<FamilyMember> getAllFamilyMembers(int page, int size)
        {
            int offset = (page - 1) * size;
            List<FamilyMember> modules = new List<FamilyMember>();
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();

            SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[FamilyMember] ORDER BY FamilyMemberID OFFSET @offset ROWS FETCH NEXT @size ROWS ONLY;", scon);
            command.Parameters.AddWithValue("@offset", offset);
            command.Parameters.AddWithValue("@size", size);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    modules.Add(new FamilyMember(reader.GetInt32(0).ToString(), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetString(7), reader.GetInt32(8)));


                }
            }


            return modules;
        }


        // Get familymember by ID
        public List<FamilyMember> getFamilyMemerById(int id)
        {
            List<FamilyMember> modules = new List<FamilyMember>();
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();
            SqlCommand command = new SqlCommand($@"SELECT * FROM [dbo].[FamilyMember] where FamilyMemberID = '{id}'", scon);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    modules.Add(new FamilyMember(reader.GetInt32(0).ToString(), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetString(7), reader.GetInt32(8)));
                }
            }


            return modules;
        }

        // Add new Family member


        // Update Family member



        // Delete family member

    }
}
