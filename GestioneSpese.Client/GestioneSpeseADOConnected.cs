using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestioneSpese.Client
{
    public static class GestioneSpeseADOConnected
    {
        static string connectionStringSQL = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GestioneSpese;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";




        public static void MostraSpese(string query)
        {
            using SqlConnection conn = new SqlConnection(connectionStringSQL);
            try
            {
                conn.Open();
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    //Console.WriteLine("siamo connessi al db");
                }
                else
                {
                    Console.WriteLine("Non connessi al db");
                }

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = query;

              

                SqlDataReader reader = cmd.ExecuteReader();

                Console.WriteLine("----Spese----");
                while (reader.Read())  
                {
                     
                    var id = (int)reader["SpesaId"];
                    var dataSpesa = (DateTime)reader["DataSpesa"];
                    var descrizione = (string)reader["Descrizione"];
                    var utente = (string)reader["Utente"];
                    var importo = (decimal)reader["Importo"];
                    var approvato = (bool)reader["Approvato"];
                    var categoriaId = (int)reader["CategoriaId"];

                    Console.WriteLine($"{id} - {dataSpesa} - {descrizione}- {utente}- {importo} - {approvato} - {categoriaId}");

                }

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

        public static void MostraCategorie()
        {
            using SqlConnection conn = new SqlConnection(connectionStringSQL);
            try
            {
                conn.Open();
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    //Console.WriteLine("siamo connessi al db");
                }
                else
                {
                    Console.WriteLine("Non connessi al db");
                }

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "select * from Categoria";



                SqlDataReader reader = cmd.ExecuteReader();

                Console.WriteLine("----Categorie----");
                while (reader.Read()) 
                { 
                var id = (int)reader["CategoriaId"];
                var categoria = (string)reader["Categoria"];

                Console.WriteLine($"[ID] = {id} - [Nome categoria] :{categoria} ");

                }

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

        public static void MostraTotPerCategoria(int idCateg)
        {
            using SqlConnection conn = new SqlConnection(connectionStringSQL);
            try
            {
                conn.Open();
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    //Console.WriteLine("siamo connessi al db");
                }
                else
                {
                    Console.WriteLine("Non connessi al db");
                }

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = $"select c.Categoria, sum(s.Importo) as tot"+ 
                                    " from Spesa s join Categoria c on s.CategoriaId = c.CategoriaId "+
                                    $"where c.CategoriaId = {idCateg}"+
                                    "group by c.Categoria";



                SqlDataReader reader = cmd.ExecuteReader();

                Console.WriteLine("----Spese----");
                while (reader.Read())
                {

                    var categ = (string)reader["Categoria"];
                    var totSpese = (decimal)reader["tot"];


                    Console.WriteLine($"{categ} - {totSpese} ");

                }

            }
            catch (SqlException e)
            {

                Console.WriteLine($"errore : {e.Message}");
                //Console.WriteLine($"linea errore : {e.LineNumber}");
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
    }
}
