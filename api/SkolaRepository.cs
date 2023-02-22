using Microsoft.Data.SqlClient;
using System.Data.Common;

namespace ApiDemoProject
{
    public class SkolaRepository
    {
        public List<Skola> GetAll()
        {
            List<Skola> skolas = new List<Skola>();

            string connectionString = @"Server=localhost\SQLEXPRESS;
                            Database=DBDEMO;
                            TrustServerCertificate=True;
                            Integrated Security=True;";

            SqlConnection sqlConnection = new SqlConnection(connectionString);

            sqlConnection.Open();

            string selectSkolaSql = "SELECT * FROM dbo.Skola;";

            DbCommand selectSkolaCommand = sqlConnection.CreateCommand();

            selectSkolaCommand.CommandText = selectSkolaSql;
            selectSkolaCommand.CommandType = System.Data.CommandType.Text;

            DbDataReader dataReader = selectSkolaCommand.ExecuteReader();

            while (dataReader.Read())
            {
                Skola skolaFromDatabase = new Skola();

                skolaFromDatabase.Id = (int)dataReader["Skolas_ID"];
                skolaFromDatabase.Address = (string)dataReader["Adrese"];
                skolaFromDatabase.Name = (string)dataReader["Nosaukums"];
                skolaFromDatabase.Principal = (string)dataReader["Direktors"];

                skolas.Add(skolaFromDatabase);
            }

            sqlConnection.Close();

            return skolas;
        }

        public void Add(Skola skola)
        {
            string connectionString = @"Server=localhost\SQLEXPRESS;
                            Database=DBDEMO;
                            TrustServerCertificate=True;
                            Integrated Security=True;";

            SqlConnection sqlConnection = new SqlConnection(connectionString);

            sqlConnection.Open();

            string insertSkolaSql = @"
                INSERT INTO dbo.Skola 
                VALUES (@id, @address, @nameOfSchool, @nameOfPrincipal);";

            // Uztaisām vaicājuma komandu
            DbCommand insertSkolaCommand = sqlConnection.CreateCommand();

            insertSkolaCommand.CommandText = insertSkolaSql;
            insertSkolaCommand.CommandType = System.Data.CommandType.Text;

            // Nodefinējam @id parametru un tā vērtību
            DbParameter idParameter = insertSkolaCommand.CreateParameter();
            idParameter.ParameterName = "id";
            idParameter.Value = skola.Id;

            // Nodefinējam @address parametru un tā vērtību
            DbParameter addressParameter = insertSkolaCommand.CreateParameter();
            addressParameter.ParameterName = "address";
            addressParameter.Value = skola.Address;

            // Nodefinējam @nameOfSchool parametru un tā vērtību
            DbParameter nameOfSchoolParameter = insertSkolaCommand.CreateParameter();
            nameOfSchoolParameter.ParameterName = "nameOfSchool";
            nameOfSchoolParameter.Value = skola.Name;

            // Nodefinējam @nameOfPrincipal parametru un tā vērtību
            DbParameter nameOfPrincipalParameter = insertSkolaCommand.CreateParameter();
            nameOfPrincipalParameter.ParameterName = "nameOfPrincipal";
            nameOfPrincipalParameter.Value = skola.Principal;

            // Pievienojam kopējam parametru sarakstam
            insertSkolaCommand.Parameters.Add(idParameter);
            insertSkolaCommand.Parameters.Add(addressParameter);
            insertSkolaCommand.Parameters.Add(nameOfSchoolParameter);
            insertSkolaCommand.Parameters.Add(nameOfPrincipalParameter);

            insertSkolaCommand.ExecuteNonQuery();

            sqlConnection.Close();
        }

        public Skola FindById(int id)
        {
            string connectionString = @"Server=localhost\SQLEXPRESS;
                            Database=DBDEMO;
                            TrustServerCertificate=True;
                            Integrated Security=True;";

            SqlConnection sqlConnection = new SqlConnection(connectionString);

            sqlConnection.Open();

            string selectSkolaSql = @"SELECT * FROM dbo.Skola 
                                      WHERE Skolas_ID = @id;";

            DbCommand selectSkolaCommand = sqlConnection.CreateCommand();

            selectSkolaCommand.CommandText = selectSkolaSql;
            selectSkolaCommand.CommandType = System.Data.CommandType.Text;

            DbParameter parameter = selectSkolaCommand.CreateParameter();
            parameter.ParameterName = "id";
            parameter.Value = id;

            selectSkolaCommand.Parameters.Add(parameter);

            DbDataReader dataReader = selectSkolaCommand.ExecuteReader();

            Skola skola = new Skola();

            while (dataReader.Read())
            {
                skola.Id = (int)dataReader["Skolas_ID"];
                skola.Address = (string)dataReader["Adrese"];
                skola.Name = (string)dataReader["Nosaukums"];
                skola.Principal = (string)dataReader["Direktors"];
                
                break;
            }

            sqlConnection.Close();

            return skola;
        }
    }
}
