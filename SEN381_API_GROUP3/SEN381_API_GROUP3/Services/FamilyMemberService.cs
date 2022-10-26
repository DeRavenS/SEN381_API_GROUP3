
using SEN381_API_Group3.shared.models;
using SEN381_API_GROUP3.Database;
using System.Data;
using System.Data.SqlClient;
using static DevExpress.Xpo.DB.DataStoreLongrunnersWatch;

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
        public List<FamilyMember> getFamilyMemberById(int id)
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
        public void addNewFamilyMember(FamilyMember family)
        {
            string query = $@"INSERT INto FamilyMember
    (FamilyMemberName, FamilyMemberSurname, FamilyMemberPhone,FamilyMemberEmail, FamilyMemberAddress, FamilyIDnumber, FamilyRole,ClientID)
VALUES('{family.MemberName}', '{family.MemberSurname}', '{family.PhoneNumber}', '{family.Email}','{family.Address}', '{family.IdNumber1}', '{family.Role}', {family.ClientID} )";

            SqlParameter FamilyMembername = new SqlParameter("@FamilyMemberName", SqlDbType.VarChar);
            SqlParameter FamilyMembersurname = new SqlParameter("@FamilyMemberSurname", SqlDbType.VarChar);
            SqlParameter FamilyMemberphone = new SqlParameter("@FamilyMemberPhone", SqlDbType.VarChar);
            SqlParameter FamilyMemberemail = new SqlParameter("@FamilyMemberEmail", SqlDbType.VarChar);
            SqlParameter FamilyMemberaddress = new SqlParameter("@FamilyMemberAddress", SqlDbType.VarChar);
            SqlParameter FamilyIdnumber = new SqlParameter("@FamilyIDnumber", SqlDbType.VarChar);
            SqlParameter Familyrole = new SqlParameter("@FamilyRole", SqlDbType.VarChar);
            SqlParameter ClientId = new SqlParameter("@ClientID", SqlDbType.VarChar);

            FamilyMembername.Value = family.MemberName.ToString();
            FamilyMembersurname.Value = family.MemberSurname.ToString();
            FamilyMemberphone.Value = family.PhoneNumber.ToString();
            FamilyMemberemail.Value = family.Email.ToString();
            FamilyMemberaddress.Value = family.Address.ToString();
            FamilyIdnumber.Value = family.IdNumber1.ToString();
            Familyrole.Value = family.Role.ToString();
            ClientId.Value = family.ClientID.ToString();

            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();


            SqlCommand insertCommand = new SqlCommand(query, scon);
            insertCommand.Parameters.Add(FamilyMembername);
            insertCommand.Parameters.Add(FamilyMembersurname);
            insertCommand.Parameters.Add(FamilyMemberphone);
            insertCommand.Parameters.Add(FamilyMemberemail);
            insertCommand.Parameters.Add(FamilyMemberaddress);
            insertCommand.Parameters.Add(FamilyIdnumber);
            insertCommand.Parameters.Add(Familyrole);
            insertCommand.Parameters.Add(ClientId);

            insertCommand.ExecuteNonQuery();
            scon.Close();
        }

        // Update Family member
        public void updateFamilyMemer(int id,string FamilyMemberName, string FamilyMemberSurname, string FamilyMemberPhone, string FamilyMemberEmail, string FamilyMemberAddress, string FamilyIDnumber, string FamilyRole, string ClientID)
        {
            string query = $@"UPDATE FamilyMember set FamilyMemberName = '{FamilyMemberName}', FamilyMemberSurname = '{FamilyMemberSurname}', FamilyMemberPhone = '{FamilyMemberPhone}',FamilyMemberEmail = '{FamilyMemberEmail}', FamilyMemberAddress = '{FamilyMemberAddress}', FamilyMemberID = '{FamilyIDnumber}', FamilyRole = '{FamilyRole}', ClientID = {ClientID} WHERE FamilyMemberID = {id};";

            SqlParameter FamilyMembername = new SqlParameter("@FamilyMemberName", SqlDbType.VarChar);
            SqlParameter FamilyMembersurname = new SqlParameter("@FamilyMemberSurname", SqlDbType.VarChar);
            SqlParameter FamilyMemberphone = new SqlParameter("@FamilyMemberPhone", SqlDbType.VarChar);
            SqlParameter FamilyMemberemail = new SqlParameter("@FamilyMemberEmail", SqlDbType.VarChar);
            SqlParameter FamilyMemberaddress = new SqlParameter("@FamilyMemberAddress", SqlDbType.VarChar);
            SqlParameter FamilyIdnumber = new SqlParameter("@FamilyIDnumber", SqlDbType.VarChar);
            SqlParameter Familyrole = new SqlParameter("@FamilyRole", SqlDbType.VarChar);
            SqlParameter ClientId = new SqlParameter("@ClientID", SqlDbType.VarChar);

            FamilyMembername.Value = FamilyMemberName.ToString();
            FamilyMembersurname.Value = FamilyMemberSurname.ToString();
            FamilyMemberphone.Value = FamilyMemberPhone.ToString();
            FamilyMemberemail.Value = FamilyMemberEmail.ToString();
            FamilyMemberaddress.Value = FamilyMemberAddress.ToString();
            FamilyIdnumber.Value = FamilyIDnumber.ToString();
            Familyrole.Value = FamilyRole.ToString();
            ClientId.Value = ClientID.ToString();

            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();


            SqlCommand insertCommand = new SqlCommand(query, scon);
            insertCommand.Parameters.Add(FamilyMembername);
            insertCommand.Parameters.Add(FamilyMembersurname);
            insertCommand.Parameters.Add(FamilyMemberphone);
            insertCommand.Parameters.Add(FamilyMemberemail);
            insertCommand.Parameters.Add(FamilyMemberaddress);
            insertCommand.Parameters.Add(FamilyIdnumber);
            insertCommand.Parameters.Add(Familyrole);
            insertCommand.Parameters.Add(ClientId);

            insertCommand.ExecuteNonQuery();
            scon.Close();
        }


        // Delete family member
        public void deleteFamilyMember(int id)
        {
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();
            string query = $@"DELETE from FamilyMember WHERE FamilyMemberID = '{id}'";
            SqlCommand com = new SqlCommand(query, scon);
            com.ExecuteNonQuery();
            scon.Close();
        }

    }
}
