// See https://aka.ms/new-console-template for more information
using GestioneSpese.Client;
using System.Data;

Console.WriteLine("-------Gestione Spese--------");


bool quit = false;
do
{
    Console.WriteLine("\nOpzioni disponibili:\n");
    Console.WriteLine("[1] : Inserisci nuova spesa\n");
    Console.WriteLine("[2] : Approva una spesa\n");
    Console.WriteLine("[3] : Elimina una spesa\n");
    Console.WriteLine("[4] : Mostra spese approvate\n");
    Console.WriteLine("[5] : Mostra spese di un utente\n");
    Console.WriteLine("[6] : Mostra totale spese per categoria\n");



    string scelta = Console.ReadLine(); 
    switch (scelta)
    {
        case "1":
            GestioneSpeseADODisconnected.InserisciSpesa();
            GestioneSpeseADOConnected.MostraSpese("select * from Spesa");

            break;
        case "2":
            GestioneSpeseADOConnected.MostraSpese("select * from Spesa");

            Console.WriteLine("Inserire id spesa da approvare");
            int id = 0;
            if (int.TryParse(Console.ReadLine(), out id))
            {
                GestioneSpeseADODisconnected.ApprovaSpesa(id);
            }
            else
                Console.WriteLine("inserire id valido");
            GestioneSpeseADOConnected.MostraSpese("select * from Spesa");

            break;
        case "3":
            GestioneSpeseADOConnected.MostraSpese("select * from Spesa");

            Console.WriteLine("Inserire id spesa da eliminare");
            int idSpesa= 0;
            if (int.TryParse(Console.ReadLine(), out idSpesa))
            {
                GestioneSpeseADODisconnected.CancellaSpesa(idSpesa);
            }
            else
                Console.WriteLine("inserire id valido");
            GestioneSpeseADOConnected.MostraSpese("select * from Spesa");

            break;
        case "4":
            GestioneSpeseADOConnected.MostraSpese("select * from Spesa");

            GestioneSpeseADOConnected.MostraSpese("select * from Spesa where Approvato = 1");
            break;
        case "5":
            GestioneSpeseADOConnected.MostraSpese("select * from Spesa");

            Console.WriteLine("Inserire nome utente ");
            string utente = Console.ReadLine();
           
            GestioneSpeseADOConnected.MostraSpese($"select * from Spesa where Utente = '{utente}' ");
            
            //GestioneSpeseADOConnected.MostraSpese("select * from Spesa");

            break;
        case "6":
            GestioneSpeseADOConnected.MostraCategorie();
            Console.WriteLine("Inserire id della categoria");
            int idCateg = 0;

            if (int.TryParse(Console.ReadLine(), out idCateg))
            {
                GestioneSpeseADOConnected.MostraTotPerCategoria(idCateg);
            }
            else
                Console.WriteLine("inserire id valido"); 
            break;
        case "q":
            quit = true;
            break;

        default:
            Console.WriteLine("Scelta non valida");
            break;
    }
}while (!quit);

