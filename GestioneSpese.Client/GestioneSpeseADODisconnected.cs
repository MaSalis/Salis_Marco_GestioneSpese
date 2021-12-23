using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestioneSpese.Client
{
    public static class GestioneSpeseADODisconnected
    {
        static string connectionStringSQL = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GestioneSpese;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public static void InserisciSpesa()
        {
            DataSet speseDS = new DataSet();
            using SqlConnection conn = new SqlConnection(connectionStringSQL);
            try
            {

                conn.Open();
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    //Console.WriteLine("Connessi al DB!");

                }
                else
                    Console.WriteLine("Non connessi!");

                var spesaAdapter = InizializzaSpesaAdapter(conn);
                spesaAdapter.Fill(speseDS, "Spesa");
               


                conn.Close();
                //Console.WriteLine("Connessione chiusa");


                DataRow nuovaRiga = speseDS.Tables["Spesa"].NewRow();
                Console.WriteLine("inserisci data spesa nel formato yyyy-mm-dd");
                nuovaRiga["DataSpesa"] = Console.ReadLine();

                Console.WriteLine("inserisci descrizione");
                nuovaRiga["Descrizione"] = Console.ReadLine();

                Console.WriteLine("inserisci utente");
                nuovaRiga["Utente"] = Console.ReadLine();

                Console.WriteLine("inserisci importo");
                nuovaRiga["Importo"] = Console.ReadLine();

                nuovaRiga["Approvato"] = 0;

                GestioneSpeseADOConnected.MostraCategorie();
                Console.WriteLine("inserisci  id categoria");
                // mostrare categorie
                

                nuovaRiga["CategoriaId"] = Console.ReadLine(); ;

                speseDS.Tables["Spesa"].Rows.Add(nuovaRiga); 
              
                spesaAdapter.Update(speseDS, "Spesa");
                Console.WriteLine("Database aggiornato");

            }
            catch (SqlException e) 
            {

                Console.WriteLine($"errore : {e.Message}");
            }
            catch (Exception ex)
            {

                Console.WriteLine($"errore : {ex.Message}");
            }
            finally
            {
                conn.Close();
            }


        }

        public static void ApprovaSpesa(int id)
        {
            DataSet speseDS = new DataSet();
            using SqlConnection conn = new SqlConnection(connectionStringSQL);
            try
            {

                conn.Open();
                if (conn.State == System.Data.ConnectionState.Open) { }
                   // Console.WriteLine("Connessi al DB!");
                else
                    Console.WriteLine("Non connessi!");

                var spesaAdapter = InizializzaSpesaAdapter(conn);
                spesaAdapter.Fill(speseDS, "Spesa");
                conn.Close();
                //Console.WriteLine("Connessione chiusa");

                DataRow rigaDaAggiornare = speseDS.Tables["Spesa"].Rows.Find(id);
                if (rigaDaAggiornare != null)
                    rigaDaAggiornare["Approvato"] = true;

                
                spesaAdapter.Update(speseDS, "Spesa");
            }
            catch (SqlException e) 
            {

                Console.WriteLine($"errore : {e.Message}");
            }
            catch (Exception ex)
            {

                Console.WriteLine($"errore : {ex.Message}");
            }
            finally
            {
                //Console.WriteLine("connessione chiusa nel finally"); 
                conn.Close();
            }
        }

        public static void CancellaSpesa(int id)
        {
            DataSet speseDS = new DataSet();
            using SqlConnection conn = new SqlConnection(connectionStringSQL);
            try
            {

                conn.Open();
                if (conn.State == System.Data.ConnectionState.Open) { }
                   // Console.WriteLine("Connessi al DB!");
                    else
                        Console.WriteLine("Non connessi!");

                var spesaAdapter = InizializzaSpesaAdapter(conn);
                spesaAdapter.Fill(speseDS, "Spesa");
                conn.Close();
                //Console.WriteLine("Connessione chiusa");

                DataRow rigaDaEliminare = speseDS.Tables["Spesa"].Rows.Find(id);
                if (rigaDaEliminare != null)
                {
                    rigaDaEliminare.Delete();
                    Console.WriteLine("riga eliminata");
                }


                //vero salvataggio sul DB
                spesaAdapter.Update(speseDS, "Spesa");
            }
            catch (SqlException e)
            {

                Console.WriteLine($"errore : {e.Message}");
            }
            catch (Exception ex)
            {

                Console.WriteLine($"errore : {ex.Message}");
            }
            finally
            {
                conn.Close();
            }
        }

        //internal static DataSet getSpesaDS()
        //{
        //    DataSet spesaDS = new DataSet();
        //    using SqlConnection conn = new SqlConnection(connectionStringSQL);
        //    conn.Open();
        //    var spesaAdapter = InizializzaSpesaAdapter(conn);
        //    spesaAdapter.Fill(spesaDS, "Spesa");



        //    conn.Close();
        //    return spesaDS;

        //}

        private static SqlDataAdapter InizializzaSpesaAdapter( SqlConnection conn)
        {
            SqlDataAdapter spesaAdapter = new SqlDataAdapter();
           
            spesaAdapter.SelectCommand = new SqlCommand("select * from Spesa", conn);       
           
            spesaAdapter.UpdateCommand = GenerateApprovaCommand(conn);
            spesaAdapter.InsertCommand = GenerateInsertCommand(conn);
            spesaAdapter.DeleteCommand = GenerateDeleteCommand(conn);

            spesaAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;

            return spesaAdapter;
        }

        private static SqlCommand GenerateDeleteCommand(SqlConnection conn)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "delete from Spesa where SpesaId = @id";

            cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int, 0, "SpesaId"));

            return cmd;
        }

        private static SqlCommand GenerateInsertCommand(SqlConnection conn)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "insert into Spesa values (@data, @desc, @utente, @importo, @approvato, @catId )" ;

            //cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int, 0, "IdIngrediente"));
            cmd.Parameters.Add(new SqlParameter("@data", SqlDbType.DateTime, 0, "DataSpesa"));
            cmd.Parameters.Add(new SqlParameter("@desc", SqlDbType.NVarChar, 500, "Descrizione"));
            cmd.Parameters.Add(new SqlParameter("@utente", SqlDbType.NVarChar, 100, "Utente"));
            cmd.Parameters.Add(new SqlParameter("@importo", SqlDbType.Decimal, 10, "Importo"));
            cmd.Parameters.Add(new SqlParameter("@approvato", SqlDbType.Bit, 0, "Approvato"));
            cmd.Parameters.Add(new SqlParameter("@catId", SqlDbType.Int, 0, "CategoriaId"));
            return cmd;
        }

        private static SqlCommand GenerateApprovaCommand(SqlConnection conn)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "update Spesa set Approvato = @approvato where SpesaId = @id";

            cmd.Parameters.Add(new SqlParameter("@approvato", SqlDbType.Bit, 0, "Approvato"));
            cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int, 0, "SpesaId"));

            return cmd;

        }
    }
}
