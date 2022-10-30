using SEN381_API_Group3.shared.models;
using SEN381_API_GROUP3.Database;
using System.Data.SqlClient;
using System.Reflection;

namespace SEN381_API_GROUP3.Services
{
    public class CoverageService
    {
        public List<TreatmentCoverage> getCoverageList(int page, int size)
        {
            int offset = (page - 1) * size;
            List<TreatmentCoverage> modules = new List<TreatmentCoverage>();
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();

            SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[Coverage] WHERE [CoverageDESCRIPTION] IN ('Gold','Silver','Platinum') ORDER BY CoverageID OFFSET @offset ROWS FETCH NEXT @size ROWS ONLY;", scon);
            command.Parameters.AddWithValue("@offset", offset);
            command.Parameters.AddWithValue("@size", size);
            SqlDataReader reader = command.ExecuteReader();

            CoverageFactory fac = new CoverageFactory();
            if (reader.HasRows)
            {
                while (reader.Read()) // Factory
                {
                    TreatmentCoverage cov = fac.InstantiateCoverage(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetInt32(4));
                    if (cov != null) modules.Add(cov);
                }
            }

            return modules;
        }

        public TreatmentCoverage getCoverageByID(int id)
        {
            List<TreatmentCoverage> modules = new List<TreatmentCoverage>();
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();
            SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[Coverage] where CoverageID = " + id, scon);
            SqlDataReader reader = command.ExecuteReader();

            CoverageFactory fac = new CoverageFactory();
            if (reader.HasRows)
            {
                while (reader.Read()) // Factory
                {
                    TreatmentCoverage cov = fac.InstantiateCoverage(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetInt32(4));
                    if (cov != null) modules.Add(cov);
                }
            }


            return modules[0];
        }

        public TreatmentCoverage insertCoverage(TreatmentCoverage coverage)
        {
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();
            SqlCommand command = new SqlCommand("INSERT INTO Coverage(CoverageDESCRIPTION,NumberOfGeneralVisits,NumberOfSpecialistsVisits,TotalCoverageUser) " +
                "                               VALUES (@des,@gen,@spec,@user)", scon);//Change number of users to be aggregate value
            command.Parameters.AddWithValue("@des", coverage.CoverageDescription);
            command.Parameters.AddWithValue("@gen", coverage.NumberOfGeneralVisits);
            command.Parameters.AddWithValue("@spec", coverage.NumberOfSpecialistsVisits);
            command.Parameters.AddWithValue("@user", coverage.TotalCoverageUser);
            command.ExecuteNonQuery();
            Console.WriteLine("Inserted");
            return coverage;
        }

        public TreatmentCoverage updateCoverage(int id, TreatmentCoverage coverage)
        {
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();
            SqlCommand command = new SqlCommand("UPDATE Coverage " +
                "                               SET CoverageDESCRIPTION=@des,NumberOfGeneralVisits=@gen,NumberOfSpecialistsVisits=@spec,TotalCoverageUser=@user" +
                "                               WHERE CoverageID=@id ", scon);//Change number of users to be aggregate value
            command.Parameters.AddWithValue("@des", coverage.CoverageDescription);
            command.Parameters.AddWithValue("@gen", coverage.NumberOfGeneralVisits);
            command.Parameters.AddWithValue("@spec", coverage.NumberOfSpecialistsVisits);
            command.Parameters.AddWithValue("@user", coverage.TotalCoverageUser);
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();


            return coverage;//Add Error checking for 404
        }

        public TreatmentCoverage? deleteCoverage(int id)
        {
            List<TreatmentCoverage> coverages = new List<TreatmentCoverage>();
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();

            SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[Coverage] where CoverageID = " + id, scon);
            SqlDataReader reader = command.ExecuteReader();

            CoverageFactory fac = new CoverageFactory();
            if (reader.HasRows)
            {
                while (reader.Read()) // Factory
                {
                    TreatmentCoverage cov = fac.InstantiateCoverage(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetInt32(4));
                    if (cov != null) coverages.Add(cov);
                }
            }
            reader.Close();
            SqlCommand command2 = new SqlCommand("DELETE FROM [dbo].[Coverage] WHERE CoverageID = @id", scon);
            command2.Parameters.AddWithValue("@id",id);
            command2.ExecuteNonQuery();

            return coverages.Count!=0?coverages[0]:null;
        }
    }

    public class CoverageFactory
    {
        public TreatmentCoverage InstantiateCoverage(int id, string description, int general, int specialist, int user)
        {
            if (description == "Silver")
            {
                return new SilverCoverage(id, description, general, specialist, user);
            }
            else if (description == "Gold")
            {
                return new GoldCoverage(id, description, general, specialist, user);
            }
            else if (description == "Platinum")
            {
                return new PlatinumCoverage(id, description, general, specialist, user);
            }
            else return null;
        }

        
    }
}
