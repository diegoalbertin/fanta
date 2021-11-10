using System;
using System.IO;
using System.Collections.Generic;
using System.Text;


namespace fantacalcio
{
    class Program
    {
        static public bool accessoEseguito = false;//variabile per indicare se l'utente 
        static public int numeroOggettoPlayer;
        static public string nomeCalciatore = "";
        static public string cognomeCalciatore = "";
        static public string playerPossessoreCalciatore = "";
        static public string nomePlayer = "";
        static public string passwordPlayer = "";
        static public int creditiPlayer = 0;
        static public int punteggioTotalePlayer = 0;
        static public int numeroPlayer = 0;
        static public player[] player = new player[numeroPlayer];
        static public int numerocalciatori = 0;
        static public calciatore[] calciatore = new calciatore[numerocalciatori];
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
                if (accessoEseguito == false)
                {
                    sceltaUtenteNoLogIn();
                }
                else if (accessoEseguito == true)
                {
                    sceltaUtenteLogIn();
                }
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
            else if (nScelta == 4)//scelta n°4
            {
                Console.WriteLine("chiusura programma");
                Environment.Exit(0);
            }
        }
        static public void sceltaUtenteLogIn()
        {
            Console.WriteLine("cosa vorresti fare?inserisci il numero corrispondente alla tua scelta" + "\n" + "1-inizia asta, 2-log out, 3-visualizza classifica, 4-visualizza la tua rosa, 5-chiudi programma");
            string inserimentoSceltaUtente = Console.ReadLine();
            int nScelta = 0;
            bool success = int.TryParse(inserimentoSceltaUtente, out nScelta);//viene effettuato un controllo sull'inserimento della scelta
            if (success == false)//in caso non sia stato inserito un numero
            {
                Console.WriteLine("non hai inserito un numero");
                sceltaUtenteNoLogIn();
            }
            else if (nScelta != 1 && nScelta != 2 && nScelta != 3 && nScelta != 4)//in caso non sia stato inserito un numero compreso tra 1-3
            {
                Console.WriteLine("non hai inserito un numero tra quelli elencati");
                sceltaUtenteNoLogIn();
            }
            else if (nScelta == 1)//scelta n°1
            {
                Console.Clear();
                iniziaAsta();
            }
            else if (nScelta == 2)//scelta n°2
            {
                Console.Clear();
                logOut();
            }
            else if (nScelta == 3)//scelta n°3
            {
                Console.Clear();
                visualizzaClassifica();
            }
            else if (nScelta==4)
            {
                Console.Clear();
                visualizzaRosaGiocatore();
            }
            else if (nScelta == 5)
            {
                Console.Clear();
                inserisciRosaProssimaGiornata();
            }
            else if (nScelta == 6)
            {
                Console.WriteLine("chiusura programma");
                chiusuraProgramma();
            }
        }
        static public void creazioneID()//funzione che crea l'ID
        {
            string variabileSceltaUtente = "";//variabile utilizzata per la scelta dell'azione dell'utente
            Console.WriteLine("CREAZIONE ID" );
            nome_Password();//viene richiamata la funzione per l'inserimento di nome e passord da parte dell'utente
            for (int i = 0; i < numeroPlayer; i++)//ciclo for per "puntare" al singolo oggetto della classe player
            {
                if (player[i].nome == nomePlayer)//ciclo if per il controllo dell'unicità del nome inserito dall'utente 
                {
                    Console.WriteLine("nome utente già in uso" + "\n" + "inserirne uno valido");
                    Console.Clear();
                    creazioneID();//in caso esista già un utente con quel nome viene richiamata la funzione per la creazione dell'ID
                }
            }
            Console.WriteLine("sei sicuro di voler creare un nuovo ID con queste credenziali?" + "\n" + "username e password non saranno modificabili dopo la creazione dell'ID" + "\n" + "se vuoi interrompere la creazione digita NO, altrimenti digita SI");
            while (variabileSceltaUtente !="SI" && variabileSceltaUtente != "NO")//ciclo while per controllare se l'utente digita una parola accettabile dal programma
            {
                Console.WriteLine("scelta non valida, digita SI oppure NO");//in caso sbagli nell'inserimento gli viene richiesto nuovamente
                variabileSceltaUtente = Convert.ToString(Console.ReadLine()).ToUpper();
            }
            if (variabileSceltaUtente == "SI")//se la scelta è si
            {
                numeroPlayer++;//viene aumentato il numero di giocatori
                player[numeroPlayer - 1] = new player();//viene istanziato un nuovo oggetto della classe player e ne vengono richiamati due metodi
                player[numeroPlayer - 1].getNome();
                player[numeroPlayer - 1].getPassword();
                righeFile++;//aumenta di 1 il numero delle righe del file
                string variabiliNonImpostate = "0";//stringa per le variabili che dovranno essere impostate in futuro
                string[,] arrayDiSalvataggio = new string[righeFile, 4];//array utilizzato per il salvataggio su File
                for (int i = 0; i < righeFile; i++)//ciclo che inserisce i valori nell'array di salvataggio
                {
                    if (i == 0)//se i=0, quindi si sta lavorando della prima riga dell'array, salva il nuovo ID con punteggio=0
                    {
                        arrayDiSalvataggio[i, 0] = nomePlayer + carattereDivisore;//le varie colonne dell'array vengono impostate con le caratteristiche del nuovo utente
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
            else if(variabileSceltaUtente == "NO")
            {
                Console.WriteLine("annullamento eseguito");
                Console.Clear();
            }
        }
        static public void logIn()//funzione per il log in
        {
            nome_Password();//viene richiamata la funzione per l'inserimento di nome e passord da parte dell'utente
            for (int i = 0; i < numeroPlayer; i++)//ciclo for per "puntare" al singolo oggetto della classe player
            {
                if (player[i].nome==nomePlayer&&player[i].password==passwordPlayer)//ciclo if che effetua il controllo tra i nomi e le password
                {           //se viene trovata una corrispondenza 
                    Console.WriteLine("login effettuato");
                    accessoEseguito = true;//variabile che indica se il log in è stato eseguito impostata a true
                    numeroOggettoPlayer = i;//viene salvato il numero relativo all'oggetto player
                    break;
                }
            }
        }
        static public void visualizzaClassifica()
        {
            //coming soon
        }
        static public void logOut()//funzione per il log out
        {
            accessoEseguito = false;//variabile che indica se il log in è stato eseguito impostata a false
            numeroOggettoPlayer = numeroPlayer;//il numero relativo all'oggetto player viene impostato su un valore impossibile
        }
        static public void iniziaAsta()
        {
            string variabileDiConfrontoPassword="";
            string annullaLogIn = "";
            //la funzione asta funziona solo se tutti i giocatori sono presenti allo stesso tempo, quindi richiederà il log-in di tutti
            for (int i = 0; i < numeroPlayer; i++)
            {
                if (i==numeroOggettoPlayer)//in caso i sia pari al numero dell'oggetto player dell'utente già loggato in precedenza
                {
                    break;//il programma non esegue nulla
                }
                else//se i è pari al numero dell'oggetto player degli altri utenti
                {
                    while (variabileDiConfrontoPassword!=player[i].password||annullaLogIn!="SI")
                    {
                        Console.WriteLine("{0} inserisci la tua password", player[i].nome);
                        variabileDiConfrontoPassword = Console.ReadLine();
                        if(variabileDiConfrontoPassword != player[i].password)
                        {
                            Console.WriteLine("se vuoi uscire dalla modalità inizia asta digita SI, per reinserire la password di {0} digita NO", player[i].nome);
                            annullaLogIn=Convert.ToString(Console.ReadLine()).ToUpper();
                        }
                    }
                    if (annullaLogIn=="SI")
                    {
                        sceltaUtenteLogIn();
                    }
                }
            }
            bool giocatoriMinimi = false;//variabile utilizzata per porre fine all'asta
            string nomeCalciatore="";//variabile utilizzata per assegnare un nome al nuovo calciatore
            string cognomeCalciatore = "";//variabile utilizzata per assegnare un cognome al nuovo calciatore
            int numeroPlayerOfferta = numeroPlayer++;//all'inizio dell'asta la variabile che indica a che oggetto player appartiene l'offerta per il giocatore è impostata su un valore impossibile
            int offerta = 0;//variabile utilizzata per indicare l'offerta maggiore
            int offertaPlayer = 0;//variabile utilizzata per l'inserimento delle offerte da parte degli utenti e per effettuare dei controlli di validità
            while(giocatoriMinimi==false)//l'asta dura fino a quando i giocatori per ogni player non sono 11
            {
                controlloNumeroGiocatoriECreditiPlayer();//viene richiamata la funzione controlloNumeroGiocatoriECreditiPlayer
                controlloNumeroGiocatori(giocatoriMinimi);//viene richiamata la funzione controlloNumeroGiocatori
                Console.WriteLine("inserire il nome del giocatore");//inserimento nome del nuovo calciatore
                nomeCalciatore = Console.ReadLine();                
                Console.WriteLine("inserire il cognome del giocatore");//inserimento cognome del nuovo calciatore
                cognomeCalciatore = Console.ReadLine();
                calciatore[numerocalciatori] = new calciatore();//viene creato il nuovo calciatore e gli vengono assegnati nome e cognome
                calciatore[numerocalciatori].getNome(nomeCalciatore);
                calciatore[numerocalciatori].getCognome(cognomeCalciatore);
                for (int i = 0; i < numeroPlayer; i++)//ciclo for che permette la rotazione dell'inserimento delle offerte per il calciatore tra i diversi player
                {
                    if (offerta==offerta)
                    {

                    }
                    if (numeroPlayerOfferta!=i)//se il player che dovrebbe inserire un'offerta è lo stesso ad aver già effettuato l'offerta maggiore il programma non esegue nessun comando
                    {
                        if (player[i].arrayRosaCalciatori.Length < 11)//il programma controlla se il player ha ancora spazio in rosa
                        {
                            bool success = false;
                            while (success == false)//il programma permette al player l'inserimento di un'offerta
                            {
                                Console.WriteLine("player {0} inserisci la tua offerta oppure oppure digita 0", player[i].nome);
                                string valoreInserito = Console.ReadLine();
                                offertaPlayer = 0;
                                success = int.TryParse(valoreInserito, out offertaPlayer);//il programma controlla se il valore inserito da tastiera è un intero
                                if (offertaPlayer > player[i].crediti)//se l'offerta è maggiore dei crediti del player il programma porta offertaPlayer a 0
                                {
                                    offertaPlayer = offerta;
                                }
                            }
                            if (offertaPlayer <= offerta)//se il player inserisce un'offerta minore di quella da battere il programma lo segnala
                            {
                                Console.WriteLine("offerta di {0} non valida", player[i].nome);
                            }
                            else if (offertaPlayer > offerta)//se la nuova offerta è maggiore di quella da battere
                            {
                                numeroPlayerOfferta = i;//viene salvato il numero delloggetto player a cui appartiene l'offerta
                                offerta = offertaPlayer;//viene aggiornata l'offerta da battere
                            }
                        }
                        if (i == numeroPlayer - 1 && offerta != offertaPlayer)//se
                        {
                            i = 0;
                        }
                    } 
                }
                calciatore[numerocalciatori].getPlayerPossessore(player[numeroPlayerOfferta].nome);
                calciatore[numerocalciatori].getPrezzo(offerta);
                player[numeroPlayerOfferta].getRosa(numerocalciatori);
                player[numeroPlayerOfferta].getCrediti(offerta);
                numerocalciatori++;
            }
        }
        public static void controlloNumeroGiocatoriECreditiPlayer()
        {
            string nomeBot = "bot";
            string cognomeBot = "bot";
            int prezzoBot = 0;
            for (int i = 0; i < numeroPlayer; i++)
            {
                if (player[i].arrayRosaCalciatori.Length < 11 && player[i].crediti == 0)
                {
                    for (int j = 0; j < (11-player[i].arrayRosaCalciatori.Length); j++)
                    {
                        calciatore[numerocalciatori] = new calciatore();
                        calciatore[numerocalciatori].getNome(nomeBot);
                        calciatore[numerocalciatori].getCognome(cognomeBot);
                        calciatore[numerocalciatori].getPrezzo(prezzoBot);
                        calciatore[numerocalciatori].getPlayerPossessore(player[i].nome);
                        player[i].getRosa(numerocalciatori);
                        numerocalciatori++;
                    }
                }
            }
        }
        static public bool controlloNumeroGiocatori(bool giocatoriMinimi)
        {
            giocatoriMinimi = true;
            for (int i = 0; i < numeroPlayer; i++)
            {
                if (player[i].arrayRosaCalciatori.Length<11)
                {
                    giocatoriMinimi = false;
                }
            }
            return giocatoriMinimi;
        }
        static public void nome_Password()//funzione per l'inserimento di nome e password del player
        {
            string scelta = "NO";//la stringa viene impostata a NO per poter far partire il ciclo successivo
            while(scelta != "SI")//se la scelta dell'utente non è SI esegue il ciclo
            {
                Console.WriteLine("qual'e' il tuo username?");
                nomePlayer = Console.ReadLine();//inserimento del nome
                Console.WriteLine("qual'e' sarà la tua password?");
                passwordPlayer = Console.ReadLine();//inserimento della password
                Console.WriteLine("se le credenziali vanno bene digita SI, altrimenti digita NO per reinserirle");
                scelta = Convert.ToString(Console.ReadLine()).ToUpper();//variabile per l'eventuale reinserimento di nome e cognome                                          
            }
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
                chiusuraProgramma();
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
                        player[i].getCrediti(creditiPlayer);//viene richiamato il metodo per l'inserimento dei crediti dell'oggetto
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
        static public void chiusuraProgramma()//funzione che effettua la chiusura del programma 
        {
            Environment.Exit(0);
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
        public void getNome(string nomeCalciatore)
        {
            nome = nomeCalciatore;
        }
        public void getCognome(string cognomeCalciatore)
        {
            cognome = cognomeCalciatore;          
        }
        public void getPrezzo(int offerta)
        {
            prezzo = offerta;
        }
        public void getPlayerPossessore(string nomePlayerPossessore)
        {
            playerPossessore = nomePlayerPossessore;
        }
        public void getPunteggioPartita()
        {
            if (nome == "bot")
            {
                punteggioPartita = 0;
            }
            else
            {
                //riceverà in ingresso i dati immessi dall'utente
            }
        }
    }
    class player
    {
        public string nome = "";
        public string password = "";
        public int crediti;
        public int[] arrayRosaCalciatori = new int[0];
        public player()
        {
            string nome = "";
            string password = "";
            int crediti=1000;
        }
        public void getNome()
        {
            nome = Program.nomePlayer;
        }
        public void getPassword()
        {
            password = Program.passwordPlayer;
        }
        public void getCrediti(int offerta)
        {
            crediti = crediti - offerta;
        }
        public void getRosa(int posizioneGiocatore)
        {
            Array.Resize(ref arrayRosaCalciatori, arrayRosaCalciatori.Length + 1);
            arrayRosaCalciatori[arrayRosaCalciatori.Length - 1] = posizioneGiocatore;
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
