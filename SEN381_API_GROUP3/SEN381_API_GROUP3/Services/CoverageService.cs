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

            SqlCommand command = new SqlCommand("dbo.GetAllCoverages", scon);
            command.CommandType = System.Data.CommandType.StoredProcedure;

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

            SqlCommand command = new SqlCommand("dbo.GetCoverageByID", scon);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@id",id);

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
            SqlCommand command = new SqlCommand("dbo.InsertCoverage", scon);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@des", coverage.CoverageDescription);
            command.Parameters.AddWithValue("@gen", coverage.NumberOfGeneralVisits);
            command.Parameters.AddWithValue("@spec", coverage.NumberOfSpecialistsVisits);
            command.Parameters.AddWithValue("@user", coverage.TotalCoverageUser);

            command.ExecuteNonQuery();
            return coverage;
        }

        public TreatmentCoverage updateCoverage(int id, TreatmentCoverage coverage)
        {
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();
            SqlCommand command = new SqlCommand("dbo.UpdateCoverage", scon);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@des", coverage.CoverageDescription);
            command.Parameters.AddWithValue("@gen", coverage.NumberOfGeneralVisits);
            command.Parameters.AddWithValue("@spec", coverage.NumberOfSpecialistsVisits);
            command.Parameters.AddWithValue("@user", coverage.TotalCoverageUser);
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();


            return coverage;
        }

        public TreatmentCoverage? deleteCoverage(int id)
        {
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();

            SqlCommand command = new SqlCommand("dbo.DeleteCoverage", scon);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@id",id);
            command.ExecuteNonQuery();

            return new TreatmentCoverage();
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
