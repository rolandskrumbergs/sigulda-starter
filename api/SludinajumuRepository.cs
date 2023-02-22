using Microsoft.Data.SqlClient;
using System.Data.Common;

namespace ApiDemoProject
{
    public class SludinajumuRepository
    {
        private const string ConnectionString = @"Server=localhost\SQLEXPRESS;
                            Database=AUTO_SLUDINAJUMI;
                            TrustServerCertificate=True;
                            Integrated Security=True;";

        public void Add(Sludinajums sludinajums)
        {
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);

            sqlConnection.Open();

            string insertSkolaSql = @"
                INSERT INTO dbo.Sludinajumi (Marka, Gads, Ipasnieks)
                VALUES (@marka, @gads, @ipasnieks);";

            // Uztaisām vaicājuma komandu
            DbCommand insertSludinajumsCommand = sqlConnection.CreateCommand();

            insertSludinajumsCommand.CommandText = insertSkolaSql;
            insertSludinajumsCommand.CommandType = System.Data.CommandType.Text;

            // Nodefinējam @marka parametru un tā vērtību
            DbParameter markaParameter = insertSludinajumsCommand.CreateParameter();
            markaParameter.ParameterName = "marka";
            markaParameter.Value = sludinajums.Marka;

            // Nodefinējam @gads parametru un tā vērtību
            DbParameter gadsParameter = insertSludinajumsCommand.CreateParameter();
            gadsParameter.ParameterName = "gads";
            gadsParameter.Value = sludinajums.Gads;

            // Nodefinējam @ipasnieks parametru un tā vērtību
            DbParameter ipasnieksParameter = insertSludinajumsCommand.CreateParameter();
            ipasnieksParameter.ParameterName = "ipasnieks";
            ipasnieksParameter.Value = sludinajums.Ipasnieks;

            // Pievienojam kopējam parametru sarakstam
            insertSludinajumsCommand.Parameters.Add(markaParameter);
            insertSludinajumsCommand.Parameters.Add(gadsParameter);
            insertSludinajumsCommand.Parameters.Add(ipasnieksParameter);

            insertSludinajumsCommand.ExecuteNonQuery();

            sqlConnection.Close();
        }

        public List<Sludinajums> GetAll()
        {
            List<Sludinajums> sludinajumi = new List<Sludinajums>();

            SqlConnection sqlConnection = new SqlConnection(ConnectionString);

            sqlConnection.Open();

            string selectSkolaSql = "SELECT * FROM dbo.Sludinajumi;";

            DbCommand selectSludinajumsCommand = sqlConnection.CreateCommand();

            selectSludinajumsCommand.CommandText = selectSkolaSql;
            selectSludinajumsCommand.CommandType = System.Data.CommandType.Text;

            DbDataReader dataReader = selectSludinajumsCommand.ExecuteReader();

            while (dataReader.Read())
            {
                Sludinajums sludinajumsFromDatabase = new Sludinajums();

                sludinajumsFromDatabase.Id = (int)dataReader["ID"];
                sludinajumsFromDatabase.Marka = (string)dataReader["Marka"];
                sludinajumsFromDatabase.Gads = (int)dataReader["Gads"];
                sludinajumsFromDatabase.Ipasnieks = (string)dataReader["Ipasnieks"];

                sludinajumi.Add(sludinajumsFromDatabase);
            }

            sqlConnection.Close();

            return sludinajumi;
        }
    }
}
