using System;
using System.Collections.Generic;
using System.Text;


namespace fantacalcio
{
    class Program
    {
        static public string nomeCalciatore = "";
        static public string cognomeCalciatore = "";
        static public int prezzoCalciatore = 0;
        static public string playerPossessoreCalciatore = "";
        static public string nomePlayer = "";
        static public string passwordPlayer ="";
        static public int creditiPlayer = 0;
        static public int numeroPlayer = 0;
        static public player[] player = new player[1];

        static void Main(string[] args)
        {
            Console.WriteLine("BENVENUTO NEL GIOCO FANTACALCIO");
            sceltaUtente();
        }
        static public void sceltaUtente()//funzione che permette all'utente di eseguire una tra le azioni base 
        {
            Console.WriteLine("cosa vorresti fare?inserisci il numero corrispondente alla tua scelta"+"\n"+"1-creazione ID, 2-log in, 3-visualizza classifica");
            string inserimentoSceltaUtente = Console.ReadLine();
            int nScelta = 0;
            bool success = int.TryParse(inserimentoSceltaUtente, out nScelta);//viene effettuato un controllo sull'inserimento della scelta
            if (success == false)//in caso non sia stato inserito un numero
            {
                Console.WriteLine("non hai inserito un numero");
                sceltaUtente();
            }
            else if (nScelta!=1&&nScelta!=2&&nScelta!=3)//in caso non sia stato inserito un numero compreso tra 1-3
            {
                Console.WriteLine("non hai inserito un numero tra quelli elencati");
                sceltaUtente();
            }
            else if (nScelta==1)//scelta n°1
            {
                Console.Clear();
                creazioneID();
            }
            else if (nScelta==2)//scelta n°2
            {
                Console.Clear();
                logIn();
            }
            else if (nScelta==3)//scelta n°3
            {
                Console.Clear();
                visualizzaClassifica();
            }
        }
        static public void creazioneID()//funzione che crea l'ID
        {
            string variabileSceltaUtente = "";
            Console.WriteLine("CREAZIONE ID" );
            Console.WriteLine("quale sarà il tuo username?");
            nomePlayer = Console.ReadLine();
            Console.WriteLine("quale sarà la tua password?");
            passwordPlayer = Console.ReadLine();
            Console.WriteLine("sei sicuro di voler creare un nuovo ID con queste credenziali?" + "\n" + "username e password non saranno modificabili dopo la creazione dell'ID" + "\n" + "se vuoi interrompere la creazione digita NO, altrimenti digita SI");
            while (variabileSceltaUtente!="si"&& variabileSceltaUtente !="SI"&& variabileSceltaUtente !="no" && variabileSceltaUtente != "NO")
            {
                Console.WriteLine("scelta non valida, digita SI oppure NO");
                variabileSceltaUtente = Console.ReadLine();
            }
            if (variabileSceltaUtente == "si" || variabileSceltaUtente == "SI" || variabileSceltaUtente == "Si")
            {
                numeroPlayer++;
                player[numeroPlayer - 1] = new player();//viene istanziato un nuovo oggetto della classe player e ne vengono richiamati due metodi
                player[numeroPlayer - 1].getNome();
                player[numeroPlayer - 1].getPassword();
            }
            else if(variabileSceltaUtente == "no" || variabileSceltaUtente == "NO" || variabileSceltaUtente == "No")
            {
                Console.WriteLine("annullamento eseguito");
                //aggiungere un ritardo di 5s
                Console.Clear();
            }
        }
        static public void logIn()
        {

        }
        static public void visualizzaClassifica()
        {

        }
    }
    class calciatore
    {
        string nome;
        string cognome;
        int prezzo;
        string playerPossessore;
        double punteggioPartita; 
        public calciatore()
        {
            nome = "";
            cognome = "";
            prezzo = 0;
            playerPossessore = "";
            punteggioPartita = 0;
        }
        public void getNome()
        {
        }
        public void getCognome()
        {
            
        }
        public void getPrezzo()
        {

        }
        public void getPlayerPossessore()
        {

        }
    }
    class player
    {
        string nome = "";
        string password = "";
        public player()
        {

        }
        public void getNome()
        {
            nome = Program.nomePlayer;
        }
        public void getPassword()
        {
            password = Program.passwordPlayer;
        }
        public void getCrediti()
        {

        }
        public void getRosa()
        {

        }
        public void getPunteggioGiornata()
        {

        }
        public void getPunteggioTotale()
        {

        }
    }
    class giornata
    {

    }
}
