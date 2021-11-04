using System;
using System.IO;
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
        static public string passwordPlayer = "";
        static public int creditiPlayer = 0;
        static public int punteggioTotalePlayer = 0;
        static public int numeroPlayer = 0;
        static public player[] player = new player[1];
        static public string file = "0::";
        static public char carattereDivisore=':';
        static public int righeFile=0;
        static public string[,] elementiFileOrdinati;
        static void Main(string[] args)
        {
            int cicloInfinito = 0;
            acquisizioneFileOggetti();
            while (cicloInfinito==0)
            {
                Console.WriteLine("BENVENUTO NEL GIOCO FANTACALCIO");
                sceltaUtenteNoLogIn();
                Console.ReadKey();
            }
        }
        static public void sceltaUtenteNoLogIn()//funzione che permette all'utente di eseguire una tra le azioni base 
        {
            Console.WriteLine("cosa vorresti fare?inserisci il numero corrispondente alla tua scelta"+"\n"+"1-creazione ID, 2-log in, 3-visualizza classifica, 4-chiudi programma");
            string inserimentoSceltaUtente = Console.ReadLine();
            int nScelta = 0;
            bool success = int.TryParse(inserimentoSceltaUtente, out nScelta);//viene effettuato un controllo sull'inserimento della scelta
            if (success == false)//in caso non sia stato inserito un numero
            {
                Console.WriteLine("non hai inserito un numero");
                sceltaUtenteNoLogIn();
            }
            else if (nScelta!=1&&nScelta!=2&&nScelta!=3&&nScelta!=4)//in caso non sia stato inserito un numero compreso tra 1-3
            {
                Console.WriteLine("non hai inserito un numero tra quelli elencati");
                sceltaUtenteNoLogIn();
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
            else if (nScelta == 4)
            {
                Console.WriteLine("chiusura programma");
                Environment.Exit(0);
            }
        }
        static public void creazioneID()//funzione che crea l'ID
        {
            string variabileSceltaUtente = "";
            Console.WriteLine("CREAZIONE ID" );
            nome_Password();
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
                righeFile++;//aumenta di 1 il numero delle righe del file
                string variabiliNonImpostate = "0";
                string[,] arrayDiSalvataggio = new string[righeFile, 4];//array utilizzato per il salvataggio su File
                for (int i = 0; i < righeFile; i++)//ciclo che inserisce i valori nell'array di salvataggio
                {
                    if (i == 0)//se i=0, quindi si sta lavorando della prima riga dell'array, salva il nuovo ID con punteggio=0
                    {
                        arrayDiSalvataggio[i, 0] = nomePlayer + carattereDivisore;
                        arrayDiSalvataggio[i, 1] = passwordPlayer + carattereDivisore;
                        arrayDiSalvataggio[i, 2] = variabiliNonImpostate + carattereDivisore;
                        arrayDiSalvataggio[i, 3] = variabiliNonImpostate + carattereDivisore + "\n";
                    }
                    else if (i >= 1)//dopodichè inserirà quelli già presenti nel file
                    {
                        for (int j = 0; j < 4; j++)//ciclo che permette di variare colonna
                        {
                            if (i == (righeFile - 1) && j == 3)//se si tratta dell'ultimo elemento del file non viene aggiunto il carattere divisore
                            {
                                arrayDiSalvataggio[i, j] = elementiFileOrdinati[i - 1, j];
                            }
                            else//in caso contrario si
                            {
                                arrayDiSalvataggio[i, j] = elementiFileOrdinati[i - 1, j] + carattereDivisore;
                            }
                        }
                    }
                }
                string salvataggioID="";
                int variabileDiControllo = 0;
                for (int i = 0; i < righeFile; i++)//ciclo che inserisce i valori dell'array di salvataggio nella stringa utilizzata per la scrittura su file
                {
                    if (variabileDiControllo == 0)//inserisce come primo valore il numero di righe del file
                    {
                        salvataggioID = Convert.ToString(righeFile) + carattereDivisore;
                        variabileDiControllo = 1;
                    }
                    for (int j = 0; j < 4; j++)//dopodichè inserisce gli ID e punteggi
                    {
                        salvataggioID = salvataggioID + arrayDiSalvataggio[i, j];
                    }
                    //salvataggioID = salvataggioID + "\n";//dopo ogni ID e punteggio va a capo per salvare il successivo
                }
                File.WriteAllText(@"C:\Users\Studente\Desktop\IDePunteggi", salvataggioID);//esegue la scrittura su file
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
            nome_Password();

        }
        static public void visualizzaClassifica()
        {

        }
        static public void nome_Password()
        {
            Console.WriteLine("qual'e' il tuo username?");
            nomePlayer = Console.ReadLine();
            Console.WriteLine("qual'e' sarà la tua password?");
            passwordPlayer = Console.ReadLine();
        }
        private static void acquisizioneFileOggetti()//funzione che gestisce l'eventuale creazione e caricamento di dati presenti su file nel programma
        {
            try
            {
                file = File.ReadAllText(@"C:\Users\Studente\Desktop\IDePunteggi");//il programma prova a leggere il file
            }
            catch (FileNotFoundException)//in caso si verifichi l'eccezione in cui non viene trovato il file il programma ne creerà uno 
            {
                Console.WriteLine("ERRORE: il file che contiene gli ID non è stato trovato. Il gioco ne creerà uno per te");
                File.WriteAllText(@"C:\Users\Studente\Desktop\IDePunteggi", file);
                Console.WriteLine("Il file è stato creato con successo.");
            }
            catch (IOException)//Se si verifica questo errore, cioè errore di lettura, esegue le operazioni seguenti.
            {
                Console.WriteLine("ERRORE: si è verificato un errore durante la lettura dei file. Prova ad aprilo di nuovo.");
                Environment.Exit(0);
            }
            string[] elementiFileDaOrdinare = file.Split(carattereDivisore);//gli elementi presenti su file vengono inseriti nell'array 
            righeFile = Convert.ToInt32(elementiFileDaOrdinare[0]);//la variabile righefile assume il valore del primo elemento dell'array elementiFileDaOrdinare(infatti questa cella contiene il numero delle righe)
            elementiFileOrdinati = new string[righeFile, 4];//viene modificata la lunghezza dell'array
            int n1 = 1; //Si inizializza la variabile necessaria per l'estrazione del contenuto dell'array monodimensionale elementi.
            for (int i = 0; i < righeFile; i++) //Inserisce nell'array multidimensionale elementiFileOrdinati il contenuto dell'array monodimensionale elementiFileDaOrdinare, 
            {
                player[i] = new player();//a ogni ciclo viene istanziato un nuovo oggetto della classe player
                for (int j = 0; j < 4; j++)//ciclo per l'inserimento dei 4 dati presenti su file per ogni oggetto
                {
                    //serie di if che si occupa della assegnazione dei dati presenti nell'array elementiFileDaOrdinare
                    if (j == 0)//j =0 se si tratta del primo elemento della riga dell'array ossia il nome del player
                    {
                        nomePlayer = elementiFileDaOrdinare[n1];//viene asseganto alla  stringa nomePlayer il valore della cella dell'array
                        player[i].getNome();//viene richiamato il metodo per l'inserimento del nome dell'oggetto
                    }
                    else if (j == 1)//j=1 se si tratta del secondo elemento della riga dell'array ossia la password dell'ID del player
                    {
                        passwordPlayer = elementiFileDaOrdinare[n1];//viene asseganto alla  stringa passwordPlayer il valore della cella dell'array
                        player[i].getPassword();//viene richiamato il metodo per l'inserimento della password dell'oggetto
                    }
                    else if (j == 2)//j=2 se si tratta del terzo elemento della riga dell'array ossia i crediti del player
                    {
                        creditiPlayer = Convert.ToInt32(elementiFileDaOrdinare[n1]);//viene asseganto all'intero creditiPlayer il valore della cella dell'array
                        player[i].getCrediti();//viene richiamato il metodo per l'inserimento dei crediti dell'oggetto
                    }
                    else if (j == 3)//j=3 se si tratta del quarto elemento della riga dell'array ossia il punteggioTotale del player
                    {
                        punteggioTotalePlayer = Convert.ToInt32(elementiFileDaOrdinare[n1]);//viene asseganto all'intero punteggioTotalePlayer il valore della cella dell'array
                        player[i].getPunteggioTotale();//viene richiamato il metodo per l'inserimento del punteggioTotale dell'oggetto
                    }                    
                    n1++;//viene aumentato il puntatore alla cella dell'array
                }
            }
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
