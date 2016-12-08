using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace ConsoleApplication1
{
    public class SQLConnection
    {
        private static string connectionstring = "server=ealdb1.eal.local; Database= ejl04_db; User Id=ejl04_usr; Password=Baz1nga4;";
        SqlConnection SqlCon = new SqlConnection(connectionstring);

        public void LogInd()
        {
                try
                {
                    SqlCon.Open();
                }
                catch (SqlException errormessage)
                {
                    Console.WriteLine("Fejl ved log ind: " + errormessage);
                }
        }

        public void LogUd()
        {
            try
            {
                SqlCon.Close();
            }
            catch (SqlException errormessage)
            {
                Console.WriteLine("Fejl ved log ud: " + errormessage);
            }
        }

        public bool LoggedInd()
        {
            if (SqlCon.State == ConnectionState.Open)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void TilføjElev(string Efternavn, string Fornavn, string Klasse)
        {
            try
            {
                SqlCommand cmd1 = new SqlCommand("TilføjElev", SqlCon);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add(new SqlParameter("ElevEfternavn", Efternavn));
                cmd1.Parameters.Add(new SqlParameter("ElevFornavn", Fornavn));
                cmd1.Parameters.Add(new SqlParameter("ElevKlasse", Klasse));
                cmd1.ExecuteNonQuery();
            }
            catch (SqlException errormessage)
            {
                Console.WriteLine("Fejl ved tilføjelse af TilføjElev:" + errormessage);
            }
        }

    }
}
