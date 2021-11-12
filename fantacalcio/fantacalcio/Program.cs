using System;
using System.IO;
using System.Collections.Generic;
using System.Text;


namespace fantacalcio
{
    class Program
    {
        static public bool cominciaAsta = false;
        static public bool accessoEseguito = false;//variabile per indicare se l'utente 
        static public bool astaEseguita = false;
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
        static public string fileCalciatori = "0::";
        static public char carattereDivisore=':';
        static public int righeFile=0;
        static public int righeFileCalciatori = 0;
        static void Main(string[] args)
        {
            int cicloInfinito = 0;
            acquisizioneFileOggetti();
            acquisizioneFileCalciatori();
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
                //visualizzaRosaGiocatore();
            }
            else if (nScelta == 5)
            {
                Console.Clear();
                //inserisciRosaProssimaGiornata();
            }
            else if (nScelta == 6)
            {
                Console.WriteLine("chiusura programma");
                chiusuraProgramma();
            }
        }
        static public void creazioneID()//funzione che crea l'ID
        {
            if (astaEseguita==false)
            {
                string variabileSceltaUtente = "";//variabile utilizzata per la scelta dell'azione dell'utente
                Console.WriteLine("CREAZIONE ID");
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
                while (variabileSceltaUtente != "SI" && variabileSceltaUtente != "NO")//ciclo while per controllare se l'utente digita una parola accettabile dal programma
                {
                    Console.WriteLine("scelta non valida, digita SI oppure NO");//in caso sbagli nell'inserimento gli viene richiesto nuovamente
                    variabileSceltaUtente = Convert.ToString(Console.ReadLine()).ToUpper();
                }
                if (variabileSceltaUtente == "SI")//se la scelta è si
                {
                    Array.Resize(ref player, player.Length + 1);
                    numeroPlayer++;//viene aumentato il numero di giocatori
                    player[numeroPlayer - 1] = new player();//viene istanziato un nuovo oggetto della classe player e ne vengono richiamati due metodi
                    player[numeroPlayer - 1].getNome();
                    player[numeroPlayer - 1].getPassword();
                    righeFile++;//aumenta di 1 il numero delle righe del file
                    string[,] arrayDiSalvataggio = new string[righeFile, 4];//array utilizzato per il salvataggio su File
                    for (int i = 0; i < righeFile; i++)//ciclo che inserisce i valori nell'array di salvataggio
                    {
                        if (i == righeFile - 1)//se si tratta dell'ultimo player da inserire non viene inserito il  carattere divisore alla fine
                        {
                            arrayDiSalvataggio[i, 0] = player[i].nome + carattereDivisore;//le varie colonne dell'array vengono impostate con le caratteristiche del nuovo utente
                            arrayDiSalvataggio[i, 1] = player[i].password + carattereDivisore;
                            arrayDiSalvataggio[i, 2] = Convert.ToString(player[i].crediti) + carattereDivisore;
                            arrayDiSalvataggio[i, 3] = Convert.ToString(player[i].punteggioTotale);
                        }
                        else
                        {
                            arrayDiSalvataggio[i, 0] = player[i].nome + carattereDivisore;//le varie colonne dell'array vengono impostate con le caratteristiche del nuovo utente
                            arrayDiSalvataggio[i, 1] = player[i].password + carattereDivisore;
                            arrayDiSalvataggio[i, 2] = Convert.ToString(player[i].crediti) + carattereDivisore;
                            arrayDiSalvataggio[i, 3] = Convert.ToString(player[i].punteggioTotale) + carattereDivisore;
                        }
                    }
                    string salvataggioID = "";
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
                    }
                    accessoEseguito = true;
                    numeroOggettoPlayer = righeFile - 1;
                    File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "IDePunteggiFanta", salvataggioID);//esegue la scrittura su file
                    Console.Clear();
                    sceltaUtenteLogIn();
                }
                else if (variabileSceltaUtente == "NO")
                {
                    Console.WriteLine("annullamento eseguito");
                    Console.Clear();
                }
            }
        }
        static public void logIn()//funzione per il log in
        {
            nome_Password();//viene richiamata la funzione per l'inserimento di nome e passord da parte dell'utente
            for (int i = 0; i < numeroPlayer; i++)//ciclo for per "puntare" al singolo oggetto della classe player
            {
                if (player[i].nome == nomePlayer&& player[i].password == passwordPlayer)//ciclo if che effetua il controllo tra i nomi e le password
                {           //se viene trovata una corrispondenza 
                    Console.WriteLine("login effettuato");
                    accessoEseguito = true;//variabile che indica se il log in è stato eseguito impostata a true
                    numeroOggettoPlayer = i;//viene salvato il numero relativo all'oggetto player
                    Console.Clear();
                    Console.WriteLine("{0}",player[i].nome);
                    sceltaUtenteLogIn();

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
            Console.Clear();
            sceltaUtenteNoLogIn();     
        }
        static public void iniziaAsta()
        {

            if (astaEseguita==false)
            {
                if (player.Length < 2)
                {
                    Console.WriteLine("sono necessari almeno due utenti registrati per poter iniziare l'asta");
                    Console.Clear();
                    sceltaUtenteLogIn();
                }
                else
                {
                    if (cominciaAsta==false)
                    {
                        string variabileDiConfrontoPassword = "";
                        string annullaLogIn = "";
                        //la funzione asta funziona solo se tutti i giocatori sono presenti allo stesso tempo, quindi richiederà il log-in di tutti
                        for (int i = 0; i < numeroPlayer; i++)
                        {
                            
                            if (i == numeroOggettoPlayer)//in caso i sia pari al numero dell'oggetto player dell'utente già loggato in precedenza
                            {
                                break;//il programma non esegue nulla
                            }
                            else//se i è pari al numero dell'oggetto player degli altri utenti
                            {
                                while (player[i].password != variabileDiConfrontoPassword && annullaLogIn != "SI")
                                {
                                    
                                    Console.WriteLine("{0} inserisci la tua password", player[i].nome);
                                    variabileDiConfrontoPassword = Console.ReadLine();
                                    if (variabileDiConfrontoPassword != player[i].password)
                                    {
                                        Console.WriteLine("se vuoi uscire dalla modalità inizia asta digita SI, per reinserire la password di {0} digita NO", player[i].nome);
                                        annullaLogIn = Convert.ToString(Console.ReadLine()).ToUpper();
                                    }
                                }
                                if (annullaLogIn == "SI")
                                {
                                    sceltaUtenteLogIn();
                                }
                            }
                        }
                        cominciaAsta = true;
                    }
                    if (cominciaAsta==true)
                    {
                        bool giocatoriMinimi = false;//variabile utilizzata per porre fine all'asta
                        int numeroPlayerOfferta;//all'inizio dell'asta la variabile che indica a che oggetto player appartiene l'offerta per il giocatore è impostata su un valore impossibile
                        int offerta;//variabile utilizzata per indicare l'offerta maggiore
                        int offertaPlayer;//variabile utilizzata per l'inserimento delle offerte da parte degli utenti e per effettuare dei controlli di validità
                        while (giocatoriMinimi == false)//l'asta dura fino a quando i giocatori per ogni player non sono 11
                        {
                            controlloNumeroGiocatoriECreditiPlayer();//viene richiamata la funzione controlloNumeroGiocatoriECreditiPlayer
                            controlloNumeroGiocatori(giocatoriMinimi);//viene richiamata la funzione controlloNumeroGiocatori
                            inserimentoNomeCalciatore();
                            inserimentoCognomeCalciatore();
                            Array.Resize(ref calciatore, calciatore.Length + 1);
                            calciatore[calciatore.Length-1] = new calciatore();//viene creato il nuovo calciatore e gli vengono assegnati nome e cognome
                            calciatore[calciatore.Length - 1].getNome();
                            calciatore[calciatore.Length - 1].getCognome();
                            offerta = 0;
                            offertaPlayer = 0;
                            numeroPlayerOfferta = numeroPlayer++;
                            for (int i = 0; i < numeroPlayer; i++)//ciclo for che permette la rotazione dell'inserimento delle offerte per il calciatore tra i diversi player
                            {
                                if (player[i].offerta == offerta)
                                {
                                    break;
                                }
                                if (numeroPlayerOfferta != i)//se il player che dovrebbe inserire un'offerta è lo stesso ad aver già effettuato l'offerta maggiore il programma non esegue nessun comando
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
                                                offertaPlayer = 0;
                                                player[i].getOfferta(offertaPlayer);
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
                                            player[i].getOfferta(offertaPlayer);
                                        }
                                    }
                                    if (i == numeroPlayer - 1 && offerta != offertaPlayer)//se
                                    {
                                        i = 0;
                                    }
                                }
                            }
                            calciatore[calciatore.Length - 1].getPlayerPossessore(player[numeroPlayerOfferta].nome);
                            calciatore[calciatore.Length - 1].getPrezzo(offerta);
                            player[numeroPlayerOfferta].getRosa(numerocalciatori);
                            player[numeroPlayerOfferta].getCrediti(offerta);
                            numerocalciatori++;
                            nomeCalciatore = "";
                            cognomeCalciatore = "";
                        }
                        astaEseguita = true;
                    }
                    string[,] arraySalvataggioCalciatori = new string[numerocalciatori, 4];
                    for (int i = 0; i < numerocalciatori; i++)
                    {
                        if (i == numerocalciatori - 1)
                        {
                            arraySalvataggioCalciatori[i, 0] = calciatore[i].nome + carattereDivisore;
                            arraySalvataggioCalciatori[i, 1] = calciatore[i].cognome + carattereDivisore;
                            arraySalvataggioCalciatori[i, 2] = Convert.ToString(calciatore[i].prezzo) + carattereDivisore;
                            arraySalvataggioCalciatori[i, 3] = calciatore[i].playerPossessore;
                        }
                        else
                        {
                            arraySalvataggioCalciatori[i, 0] = calciatore[i].nome + carattereDivisore;
                            arraySalvataggioCalciatori[i, 1] = calciatore[i].cognome + carattereDivisore;
                            arraySalvataggioCalciatori[i, 2] = Convert.ToString(calciatore[i].prezzo) + carattereDivisore;
                            arraySalvataggioCalciatori[i, 3] = calciatore[i].playerPossessore + carattereDivisore;
                        }
                    }
                    string salvataggioID = "";
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
                            salvataggioID = salvataggioID + arraySalvataggioCalciatori[i, j];
                        }
                    }
                    File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "calciatori", salvataggioID);//esegue la scrittura su file

                }
            }
            else
            {
                Console.WriteLine("non è possibile eseguire due volta l'asta per i giocatori");
                Console.Clear();
                sceltaUtenteLogIn();
            }              
        }
        public static void controlloNumeroGiocatoriECreditiPlayer()
        {
            int prezzoBot = 0;
            for (int i = 0; i < numeroPlayer; i++)
            {
                if (player[i].arrayRosaCalciatori.Length < 11 && player[i].crediti == 0)
                {
                    nomeCalciatore = "bot";
                    cognomeCalciatore = "bot";
                    for (int j = 0; j < (11-player[i].arrayRosaCalciatori.Length); j++)
                    {
                        Array.Resize(ref calciatore, calciatore.Length + 1);
                        calciatore[numerocalciatori] = new calciatore();
                        calciatore[numerocalciatori].getNome();
                        calciatore[numerocalciatori].getCognome();
                        calciatore[numerocalciatori].getPrezzo(prezzoBot);
                        calciatore[numerocalciatori].getPlayerPossessore(player[i].nome);
                        player[i].getRosa(numerocalciatori);
                        numerocalciatori++;
                    }
                }
            }
        }
        static public string inserimentoNomeCalciatore()
        {
            while (nomeCalciatore == "")
            {
                Console.WriteLine("inserire il nome del giocatore");//inserimento nome del nuovo calciatore
                nomeCalciatore = Console.ReadLine();
            }
            return nomeCalciatore;
        }
        static public string inserimentoCognomeCalciatore()
        {
            while (cognomeCalciatore == "")
            {
                Console.WriteLine("inserire il cognome del giocatore");//inserimento nome del nuovo calciatore
                cognomeCalciatore = Console.ReadLine();
            }
            return cognomeCalciatore;
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
                Console.WriteLine("qual'e' la tua password?");
                passwordPlayer = Console.ReadLine();//inserimento della password
                Console.WriteLine("se le credenziali vanno bene digita SI, altrimenti digita NO per reinserirle");
                scelta = Convert.ToString(Console.ReadLine()).ToUpper();//variabile per l'eventuale reinserimento di nome e cognome                                          
            }
        }
        private static void acquisizioneFileOggetti()//funzione che gestisce l'eventuale creazione e caricamento di dati presenti su file nel programma
        {
            try
            {
                file = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory+ "IDePunteggiFanta");//il programma prova a leggere il file
            }
            catch (FileNotFoundException)//in caso si verifichi l'eccezione in cui non viene trovato il file il programma ne creerà uno 
            {
                Console.WriteLine("ERRORE: il file che contiene gli ID non è stato trovato. Il gioco ne creerà uno per te");
                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "IDePunteggiFanta", file);
                Console.WriteLine("Il file è stato creato con successo.");
            }
            catch (IOException)//Se si verifica questo errore, cioè errore di lettura, esegue le operazioni seguenti.
            {
                Console.WriteLine("ERRORE: si è verificato un errore durante la lettura dei file. Prova ad aprilo di nuovo.");
                chiusuraProgramma();
            }
            string[] elementiFileDaOrdinare = file.Split(carattereDivisore);//gli elementi presenti su file vengono inseriti nell'array 
            righeFile = Convert.ToInt32(elementiFileDaOrdinare[0]);//la variabile righefile assume il valore del primo elemento dell'array elementiFileDaOrdinare(infatti questa cella contiene il numero delle righe)
            int n1 = 1; //Si inizializza la variabile necessaria per l'estrazione del contenuto dell'array monodimensionale elementi.
            numeroPlayer = righeFile;
            Array.Resize(ref player,righeFile);
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
                        creditiPlayer = 1000-Convert.ToInt32(elementiFileDaOrdinare[n1]);//viene asseganto all'intero creditiPlayer il valore della cella dell'array
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
        private static void acquisizioneFileCalciatori()//funzione che gestisce l'eventuale creazione e caricamento di dati presenti su file nel programma
        {
            try
            {
                fileCalciatori = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "calciatori");//il programma prova a leggere il file
            }
            catch (FileNotFoundException)//in caso si verifichi l'eccezione in cui non viene trovato il file il programma ne creerà uno 
            {
                Console.WriteLine("ERRORE: il file che contiene i calciatori non è stato trovato. Il gioco ne creerà uno per te");
                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "calciatori", fileCalciatori);
                Console.WriteLine("Il file è stato creato con successo.");
            }
            catch (IOException)//Se si verifica questo errore, cioè errore di lettura, esegue le operazioni seguenti.
            {
                Console.WriteLine("ERRORE: si è verificato un errore durante la lettura dei file. Prova ad aprilo di nuovo.");
                chiusuraProgramma();
            }
            string[] elementiDaOrdinare = file.Split(carattereDivisore);//gli elementi presenti su file vengono inseriti nell'array 
            righeFileCalciatori = Convert.ToInt32(elementiDaOrdinare[0]);//la variabile righefile assume il valore del primo elemento dell'array elementiFileDaOrdinare(infatti questa cella contiene il numero delle righe)
            int n1 = 1; //Si inizializza la variabile necessaria per l'estrazione del contenuto dell'array monodimensionale elementi.
            numerocalciatori = righeFileCalciatori;
            Array.Resize(ref calciatore, numerocalciatori);
            string nomeCalciatore = "";
            string cognomeCalciatore = "";
            int prezzoCalciatore = 0;
            string playerPossessoreCalciatore = "";
            for (int i = 0; i < righeFileCalciatori; i++) //Inserisce nell'array multidimensionale elementiFileOrdinati il contenuto dell'array monodimensionale elementiFileDaOrdinare, 
            {
                calciatore[i] = new calciatore();//a ogni ciclo viene istanziato un nuovo oggetto della classe player
                for (int j = 0; j < 4; j++)//ciclo per l'inserimento dei 4 dati presenti su file per ogni oggetto
                {
                    //serie di if che si occupa della assegnazione dei dati presenti nell'array elementiFileDaOrdinare
                    if (j == 0)//j =0 se si tratta del primo elemento della riga dell'array ossia il nome del player
                    {
                        nomeCalciatore = elementiDaOrdinare[n1];//viene asseganto alla  stringa nomePlayer il valore della cella dell'array
                        calciatore[i].getNome();//viene richiamato il metodo per l'inserimento del nome dell'oggetto
                    }
                    else if (j == 1)//j=1 se si tratta del secondo elemento della riga dell'array ossia la password dell'ID del player
                    {
                        cognomeCalciatore = elementiDaOrdinare[n1];//viene asseganto alla  stringa passwordPlayer il valore della cella dell'array
                        calciatore[i].getCognome();//viene richiamato il metodo per l'inserimento della password dell'oggetto
                    }
                    else if (j == 2)//j=2 se si tratta del terzo elemento della riga dell'array ossia i crediti del player
                    {
                        prezzoCalciatore = Convert.ToInt32(elementiDaOrdinare[n1]);//viene asseganto all'intero creditiPlayer il valore della cella dell'array
                        calciatore[i].getPrezzo(prezzoCalciatore);//viene richiamato il metodo per l'inserimento dei crediti dell'oggetto
                    }
                    else if (j == 3)//j=3 se si tratta del quarto elemento della riga dell'array ossia il punteggioTotale del player
                    {
                        playerPossessoreCalciatore = elementiDaOrdinare[n1];//viene asseganto all'intero punteggioTotalePlayer il valore della cella dell'array
                        calciatore[i].getPlayerPossessore(playerPossessoreCalciatore);//viene richiamato il metodo per l'inserimento del punteggioTotale dell'oggetto
                        for (int k = 0; k < player.Length; k++)
                        {
                            if (playerPossessoreCalciatore==player[k].nome)
                            {
                                player[k].getRosa(i);
                                player[k].getCrediti(prezzoCalciatore);
                            }
                        }
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
        public string nome;
        public string cognome;
        public int prezzo;
        public string playerPossessore;
        public double punteggioPartita; 
        public calciatore()
        {
            nome = "";
            cognome = "";
            prezzo = 0;
            playerPossessore = "";
            punteggioPartita = 0;
        }
        public void getNome( )
        {
            nome = Program.nomeCalciatore;
        }
        public void getCognome( )
        {
            cognome = Program.cognomeCalciatore;          
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
        public int offerta;
        public int[] arrayRosaCalciatori = new int[0];
        public int punteggioTotale;
        public player()
        {
            nome = "";
            password = "";
            crediti=1000;
            punteggioTotale = 0;
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
        public void getOfferta(int offertaPlayer)
        {
            offerta = offertaPlayer;
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
