using System;
using System.IO;


namespace fantacalcio
{
    internal class Program
    {
        public static bool cominciaAsta = false;//variabili globali per l'inserimento di dati
        public static bool accessoEseguito = false;
        public static bool astaEseguita = false;
        public static int numeroOggettoPlayer;
        public static string nomeCalciatore = "";
        public static string cognomeCalciatore = "";
        public static string playerPossessoreCalciatore = "";
        public static string nomePlayer = "";
        public static string passwordPlayer = "";
        public static int creditiPlayer = 0;
        public static int punteggioTotalePlayer = 0;
        public static int numeroPlayer = 0;
        public static player[] player = new player[numeroPlayer];
        public static int numerocalciatori = 0;
        public static calciatore[] calciatore = new calciatore[numerocalciatori];
        public static string file = "0::";
        public static string fileCalciatori = "0::";
        public static char carattereDivisore = ':';
        public static int righeFile = 0;
        public static int righeFileCalciatori = 0;
        public static int[,] arrayOrdinamento;
        public static bool giocatoriMinimi = false;

        private static void Main(string[] args)
        {
            int cicloInfinito = 0;
            acquisizioneFileOggetti();
            acquisizioneFileCalciatori();
            while (cicloInfinito == 0)//ciclo while che gestisce le scelte dell'utente e il funzionamento di base del programma
            {
                Console.WriteLine("BENVENUTO NEL GIOCO FANTACALCIO");
                if (accessoEseguito == false)
                {
                    sceltaUtenteNoLogIn();//set di opzioni per l'utente non loggato
                }
                else if (accessoEseguito == true)
                {
                    sceltaUtenteLogIn();//set di opzioni per l'utente loggato
                }
                Console.ReadKey();
            }
        }
        public static void sceltaUtenteNoLogIn()//funzione che permette all'utente di eseguire una tra le azioni base 
        {
            Console.WriteLine("cosa vorresti fare?inserisci il numero corrispondente alla tua scelta" + "\n" + "1-creazione ID, 2-log in, 3-visualizza classifica, 4-chiudi programma");
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
                creazioneID();
            }
            else if (nScelta == 2)//scelta n°2
            {
                Console.Clear();
                logIn();
            }
            else if (nScelta == 3)//scelta n°3
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
        public static void sceltaUtenteLogIn()
        {
            Console.WriteLine("cosa vorresti fare?inserisci il numero corrispondente alla tua scelta" + "\n" + "1-inizia asta, 2-log out, 3-visualizza classifica, 4-resetta gioco eliminando i giocatori, 5-inserisci i punteggi dei giocatori per la giornata, 6-chiudi programma");
            string inserimentoSceltaUtente = Console.ReadLine();
            int nScelta = 0;
            bool success = int.TryParse(inserimentoSceltaUtente, out nScelta);//viene effettuato un controllo sull'inserimento della scelta
            if (success == false)//in caso non sia stato inserito un numero
            {
                Console.WriteLine("non hai inserito un numero");
                sceltaUtenteLogIn();
            }
            else if (nScelta != 1 && nScelta != 2 && nScelta != 3 && nScelta != 4 && nScelta != 5 && nScelta != 6)//in caso non sia stato inserito un numero compreso tra 1-3
            {
                Console.WriteLine("non hai inserito un numero tra quelli elencati");
                sceltaUtenteLogIn();
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
            else if (nScelta == 4)//scelta n°4
            {
                Console.Clear();
                reset();
            }
            else if (nScelta == 5)//scelta n°5
            {
                Console.Clear();
                inserimentoPunteggiCalciatori();
            }
            else if (nScelta == 6)//scelta n°6
            {
                Console.Clear();
                Console.WriteLine("chiusura programma");
                chiusuraProgramma();
            }
        }
        public static void creazioneID()//funzione che crea l'ID
        {
            if (astaEseguita == false)
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
                    salvataggioID();
                    accessoEseguito = true;
                    numeroOggettoPlayer = numeroPlayer - 1;
                    Console.Clear();
                    sceltaUtenteLogIn();
                }
                else if (variabileSceltaUtente == "NO")
                {
                    Console.WriteLine("annullamento eseguito");
                    Console.Clear();
                }
            }
            else
            {
                Console.WriteLine("non è possibile creare un utente in questo momneto visto che l'asta è già stata eseguita");
            }
        }
        public static void logIn()//funzione per il log in
        {
            nome_Password();//viene richiamata la funzione per l'inserimento di nome e passord da parte dell'utente
            for (int i = 0; i < numeroPlayer; i++)//ciclo for per "puntare" al singolo oggetto della classe player
            {
                if (player[i].nome == nomePlayer && player[i].password == passwordPlayer)//ciclo if che effetua il controllo tra i nomi e le password
                {           //se viene trovata una corrispondenza 
                    Console.WriteLine("login effettuato");
                    accessoEseguito = true;//variabile che indica se il log in è stato eseguito impostata a true
                    numeroOggettoPlayer = i;//viene salvato il numero relativo all'oggetto player
                    Console.Clear();
                    Console.WriteLine("{0}", player[i].nome);
                    sceltaUtenteLogIn();
                }
            }
        }
        public static void visualizzaClassifica()
        {
            for (int i = 0; i < numeroPlayer; i++)// stampa a schermo il nome del player e il relativo punteggio
            {
                Console.WriteLine("{0}-{1}", player[i].nome, player[i].punteggioTotale);
            }
            Console.ReadLine();
            Console.Clear();
        }
        public static void logOut()//funzione per il log out
        {
            accessoEseguito = false;//variabile che indica se il log in è stato eseguito impostata a false
            numeroOggettoPlayer = numeroPlayer;//il numero relativo all'oggetto player viene impostato su un valore impossibile
            Console.Clear();
            sceltaUtenteNoLogIn();
        }
        public static void iniziaAsta()//funzione asta
        {
            if (astaEseguita == false)//viene controllato se l'asta è già stata eseguita
            {
                if (player.Length < 2)//i player devono essere almeno 2
                {
                    Console.WriteLine("sono necessari almeno due utenti registrati per poter iniziare l'asta");
                    Console.Clear();
                    sceltaUtenteLogIn();
                }
                else
                {
                    if (cominciaAsta == false)
                    {
                        string variabileDiConfrontoPassword = "";
                        string annullaLogIn = "";
                        //la funzione asta funziona solo se tutti i giocatori sono presenti allo stesso tempo, quindi richiederà il log-in di tutti
                        for (int i = 0; i < numeroPlayer; i++)
                        {
                            if (i == numeroOggettoPlayer)//in caso i sia pari al numero dell'oggetto player dell'utente già loggato in precedenza
                            {
                                //break;//il programma non esegue nulla
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
                        Console.Clear();
                    }
                    if (cominciaAsta == true)
                    {
                        arrayOrdinamento = new int[numeroPlayer, 2];
                        //variabile utilizzata per porre fine all'asta
                        int numeroPlayerOfferta;//all'inizio dell'asta la variabile che indica a che oggetto player appartiene l'offerta per il giocatore è impostata su un valore impossibile
                        int offertaPlayer;//variabile utilizzata per l'inserimento delle offerte da parte degli utenti e per effettuare dei controlli di validità
                        while (giocatoriMinimi == false) //l'asta dura fino a quando i giocatori per ogni player non sono 11
                        {
                            controlloNumeroGiocatoriECreditiPlayer();//viene richiamata la funzione controlloNumeroGiocatoriECreditiPlayer
                            controlloNumeroGiocatori();//viene richiamata la funzione controlloNumeroGiocatori
                            if (giocatoriMinimi == false)
                            {
                                inserimentoNomeCalciatore();
                                inserimentoCognomeCalciatore();
                                Array.Resize(ref calciatore, calciatore.Length + 1);
                                calciatore[calciatore.Length - 1] = new calciatore();//viene creato il nuovo calciatore e gli vengono assegnati nome e cognome
                                calciatore[calciatore.Length - 1].getNome();
                                calciatore[calciatore.Length - 1].getCognome();
                                offertaPlayer = 0;
                                numeroPlayerOfferta = numeroPlayer + 1;
                                for (int i = 0; i < numeroPlayer; i++)//ciclo for che permette la rotazione dell'inserimento delle offerte per il calciatore tra i diversi player
                                {
                                    if (player[i].crediti != 0)
                                    {
                                        Console.WriteLine("crediti di {0}: {1}", player[i].nome, player[i].crediti);
                                        bool success = false;
                                        while (success == false)//il programma permette al player l'inserimento di un'offerta
                                        {
                                            Console.WriteLine("player {0} inserisci la tua offerta oppure digita 0", player[i].nome);
                                            string valoreInserito = Console.ReadLine();
                                            offertaPlayer = 0;
                                            success = int.TryParse(valoreInserito, out offertaPlayer);//il programma controlla se il valore inserito da tastiera è un intero
                                        }
                                        if (offertaPlayer > player[i].crediti)//se l'offerta è maggiore dei crediti del player il programma porta offertaPlayer a 0
                                        {
                                            offertaPlayer = 0;
                                            player[i].getOfferta(offertaPlayer);
                                        }
                                        arrayOrdinamento[i, 0] = i;
                                        arrayOrdinamento[i, 1] = offertaPlayer;
                                    }
                                    else
                                    {
                                        offertaPlayer = 0;
                                        arrayOrdinamento[i, 0] = i;
                                        arrayOrdinamento[i, 1] = offertaPlayer;
                                    }
                                }
                                sort();
                                if (arrayOrdinamento[(arrayOrdinamento.Length / 2) - 1, 1] != 0)
                                {
                                    calciatore[calciatore.Length - 1].getPlayerPossessore(player[arrayOrdinamento[(arrayOrdinamento.Length / 2) - 1, 0]].nome);
                                    calciatore[calciatore.Length - 1].getPrezzo(arrayOrdinamento[(arrayOrdinamento.Length / 2) - 1, 1]);
                                    player[arrayOrdinamento[(arrayOrdinamento.Length / 2) - 1, 0]].getRosa(numerocalciatori);
                                    offertaPlayer = arrayOrdinamento[(arrayOrdinamento.Length / 2) - 1, 1];
                                    player[arrayOrdinamento[(arrayOrdinamento.Length / 2) - 1, 0]].getCrediti(offertaPlayer);
                                    Console.WriteLine("{0} si aggiudica {1}", player[arrayOrdinamento[(arrayOrdinamento.Length / 2) - 1, 0]].nome, nomeCalciatore);
                                    numerocalciatori++;
                                }//vengono salvati i dati del giocatore e del player possessore
                                nomeCalciatore = "";
                                cognomeCalciatore = "";
                                Console.ReadLine();
                                Console.Clear();
                            }
                        }
                        astaEseguita = true;//viene segnalato che l'asta è stata eseguita
                    }
                    //viene eseguito il salvataggio su file dei calciatori
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
                    string salvataggio = "";
                    int variabileDiControllo = 0;
                    righeFileCalciatori = numerocalciatori;
                    for (int i = 0; i < righeFileCalciatori; i++)//ciclo che inserisce i valori dell'array di salvataggio nella stringa utilizzata per la scrittura su file
                    {
                        if (variabileDiControllo == 0)//inserisce come primo valore il numero di righe del file
                        {
                            salvataggio = Convert.ToString(righeFileCalciatori) + carattereDivisore;
                            variabileDiControllo = 1;
                        }
                        for (int j = 0; j < 4; j++)//dopodichè inserisce gli ID e punteggi
                        {
                            salvataggio = salvataggio + arraySalvataggioCalciatori[i, j];
                        }
                    }
                    File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "calciatori", salvataggio);//esegue la scrittura su file
                    salvataggioID();
                }
            }
            else
            {
                Console.WriteLine("non è possibile eseguire due volta l'asta per i giocatori");
                Console.ReadKey();
                Console.Clear();
                sceltaUtenteLogIn();
            }
        }
        private static void sort()
        {
            int primoElementoComparazione;
            int secondoElementoComparazione;
            for (int i = 0; i < numeroPlayer; i++)
            {
                // Trova il minimo nel subarray da ordinare
                int indice_min = i;
                primoElementoComparazione = 0;
                secondoElementoComparazione = 0;
                for (int j = i + 1; j < numeroPlayer; j++)
                {
                    primoElementoComparazione = arrayOrdinamento[j, 1];
                    secondoElementoComparazione = arrayOrdinamento[indice_min, 1];
                    // Confronto per trovare un nuovo minimo
                    if (primoElementoComparazione < secondoElementoComparazione)
                    {
                        indice_min = j; // Salvo l'indice del nuovo minimo
                    }
                }
                // Scambia il minimo trovato con il primo elemento
                swap(indice_min, i);
            }
        }
        private static void swap(int a, int b)
        {
            int temp1 = arrayOrdinamento[a, 1];
            arrayOrdinamento[a, 1] = arrayOrdinamento[b, 1];
            arrayOrdinamento[b, 1] = temp1;
            int temp2 = arrayOrdinamento[a, 0];
            arrayOrdinamento[a, 0] = arrayOrdinamento[b, 0];
            arrayOrdinamento[b, 0] = temp2;
        }
        public static void controlloNumeroGiocatoriECreditiPlayer()
        {
            int prezzoBot = 0;
            for (int i = 0; i < numeroPlayer; i++)//ciclo for per scorrere tra i player
            {
                if (player[i].arrayRosaCalciatori.Length < 11 && player[i].crediti == 0)//se il player finisce i crediti prima di aver coprato 11 calciatori gli vengono assegnati dei bot
                {
                    nomeCalciatore = "bot";
                    cognomeCalciatore = "bot";
                    int lenght = player[i].arrayRosaCalciatori.Length;
                    for (int j = 0; j < (11 - lenght); j++)
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
            nomeCalciatore = "";
            cognomeCalciatore = "";
        }
        public static string inserimentoNomeCalciatore()
        {
            while (nomeCalciatore == "")
            {
                Console.WriteLine("inserire il nome del giocatore");//inserimento nome del nuovo calciatore
                nomeCalciatore = Console.ReadLine();
            }
            return nomeCalciatore;
        }
        public static string inserimentoCognomeCalciatore()
        {
            while (cognomeCalciatore == "")
            {
                Console.WriteLine("inserire il cognome del giocatore");//inserimento nome del nuovo calciatore
                cognomeCalciatore = Console.ReadLine();
            }
            return cognomeCalciatore;
        }
        public static bool controlloNumeroGiocatori()//funzione per determinare se il numero di giocatori per player è completo(pari a 11)
        {
            astaEseguita = true;
            giocatoriMinimi = true;
            for (int i = 0; i < numeroPlayer; i++)
            {
                if (player[i].arrayRosaCalciatori.Length < 11)
                {
                    giocatoriMinimi = false;
                    astaEseguita = false;
                }
            }

            return giocatoriMinimi;
        }
        public static void nome_Password()//funzione per l'inserimento di nome e password del player
        {
            string scelta = "NO";//la stringa viene impostata a NO per poter far partire il ciclo successivo
            while (scelta != "SI")//se la scelta dell'utente non è SI esegue il ciclo
            {
                Console.WriteLine("qual'e' il tuo username?");
                nomePlayer = Console.ReadLine();//inserimento del nome
                Console.WriteLine("qual'e' la tua password?");
                passwordPlayer = Console.ReadLine();//inserimento della password
                Console.WriteLine("se le credenziali vanno bene digita SI, altrimenti digita NO per reinserirle");
                scelta = Convert.ToString(Console.ReadLine()).ToUpper();//variabile per l'eventuale reinserimento di nome e cognome                                          
            }
        }
        public static void inserimentoPunteggiCalciatori()//funzione per l'inserimento dei punteggi per la giornata dei singoli giocatori
        {
            int[] playerVincitore = new int[1];
            double max = 0;
            string punteggiDaConvertire = "";
            for (int i = 0; i < calciatore.Length; i++)//ciclo for per scorrere tra i calciatori
            {
                if (calciatore[i].nome != "bot")
                {
                    bool success = false;
                    double punteggio = 0;
                    while (success == false)//viene richiesto e controllato il punteggio inserito da tastiera
                    {
                        Console.WriteLine("inserisci il puneteggio di {0}", calciatore[i].nome);
                        punteggiDaConvertire = Console.ReadLine();
                        punteggio = 0;
                        success = double.TryParse(punteggiDaConvertire, out punteggio);
                    }
                    calciatore[i].getPunteggioPartita(punteggio);
                }
            }
            for (int i = 0; i < numeroPlayer; i++)//ciclo for per scorrere tra i player
            {
                double punteggioGiornata = 0;
                for (int j = 0; j < 11; j++)//viene effettuata la somma dei punteggi dei singoli calciatori
                {
                    int puntatore = player[i].arrayRosaCalciatori[j];
                    punteggioGiornata = punteggioGiornata + calciatore[puntatore].punteggioPartita;
                }
                player[i].getPunteggioGiornata(punteggioGiornata);//viene assegnato il punteggiGiornata al player
                if (punteggioGiornata > max)//serie di if per la comparazione dei punteggi dei player
                {
                    if (playerVincitore.Length > 1)
                    {
                        Array.Resize(ref playerVincitore, 1);
                    }
                    playerVincitore[0] = i;//viene salvato nell'array il numero dell'oggetto player con il risultato maggiore
                }
                else if (punteggioGiornata == max)
                {
                    Array.Resize(ref playerVincitore, playerVincitore.Length + 1);
                    playerVincitore[playerVincitore.Length - 1] = i;//in caso di parità viene ridimensionato l'array e vengono salvati i vari numeri degli oggetti player con il risultato maggiore
                }
            }
            for (int i = 0; i < playerVincitore.Length; i++)
            {
                player[playerVincitore[i]].getPunteggioTotale(3);//vengono assegnati 3 punti ai player presenti nell'array
            }
            Console.Clear();
            salvataggioID();
        }
        public static void reset()
        {
            if (astaEseguita == true)
            {
                for (int i = 0; i < player.Length; i++)//vengono ripuliti gli array contenenti i puntatori agli oggetti calciatori di tutti i player
                {
                    if (player[i].arrayRosaCalciatori.Length > 0)
                    {
                        Array.Clear(player[i].arrayRosaCalciatori, 0, player[i].arrayRosaCalciatori.Length);
                        player[i].resetCrediti();
                    }
                }
                Array.Clear(calciatore, 0, numerocalciatori - 1);//vengono eliminati i calciatori
                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "calciatori", "0::");//viene svuotato il file contenente i calciatori
                salvataggioID();
                astaEseguita = false;//vengono impostate a false le variabili legate all'asta
                cominciaAsta = false;
                giocatoriMinimi = false;
                numerocalciatori = 0;
            }
        }
        public static void salvataggioID()
        {
            string[,] arrayDiSalvataggio = new string[numeroPlayer, 4];//array utilizzato per il salvataggio su File
            for (int i = 0; i < numeroPlayer; i++)//ciclo che inserisce i valori nell'array di salvataggio
            {
                if (i == numeroPlayer - 1)//se si tratta dell'ultimo player da inserire non viene inserito il  carattere divisore alla fine
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
            for (int i = 0; i < numeroPlayer; i++)//ciclo che inserisce i valori dell'array di salvataggio nella stringa utilizzata per la scrittura su file
            {
                if (variabileDiControllo == 0)//inserisce come primo valore il numero di righe del file
                {
                    salvataggioID = Convert.ToString(numeroPlayer) + carattereDivisore;
                    variabileDiControllo = 1;
                }
                for (int j = 0; j < 4; j++)//dopodichè inserisce gli ID e punteggi
                {
                    salvataggioID = salvataggioID + arrayDiSalvataggio[i, j];
                }
            }
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "IDePunteggiFanta", salvataggioID);//esegue la scrittura su file
            salvataggioID = "";
        }
        private static void acquisizioneFileOggetti()//funzione che gestisce l'eventuale creazione e caricamento di dati presenti su file nel programma
        {
            try
            {
                file = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "IDePunteggiFanta");//il programma prova a leggere il file
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
            Array.Resize(ref player, righeFile);
            for (int i = 0; i < righeFile; i++) //crea e Inserisce negli oggetti player il contenuto dell'array monodimensionale elementiFileDaOrdinare, 
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
                        creditiPlayer = 1000 - Convert.ToInt32(elementiFileDaOrdinare[n1]);//viene asseganto all'intero creditiPlayer il valore della cella dell'array
                        player[i].getCrediti(creditiPlayer);//viene richiamato il metodo per l'inserimento dei crediti dell'oggetto
                    }
                    else if (j == 3)//j=3 se si tratta del quarto elemento della riga dell'array ossia il punteggioTotale del player
                    {
                        punteggioTotalePlayer = Convert.ToInt32(elementiFileDaOrdinare[n1]);//viene asseganto all'intero punteggioTotalePlayer il valore della cella dell'array
                        player[i].getPunteggioTotale(punteggioTotalePlayer);//viene richiamato il metodo per l'inserimento del punteggioTotale dell'oggetto
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
            string[] elementiDaOrdinare = fileCalciatori.Split(carattereDivisore);//gli elementi presenti su file vengono inseriti nell'array 
            righeFileCalciatori = Convert.ToInt32(elementiDaOrdinare[0]);//la variabile righefile assume il valore del primo elemento dell'array elementiFileDaOrdinare(infatti questa cella contiene il numero delle righe)
            int n1 = 1; //Si inizializza la variabile necessaria per l'estrazione del contenuto dell'array monodimensionale elementi.
            numerocalciatori = righeFileCalciatori;
            if (numerocalciatori > 0)
            {
                astaEseguita = true;
            }
            Array.Resize(ref calciatore, numerocalciatori);
            nomeCalciatore = "";
            cognomeCalciatore = "";
            int prezzoCalciatore = 0;
            playerPossessoreCalciatore = "";
            for (int i = 0; i < righeFileCalciatori; i++) //crea e inserisce negli oggetti calciatori il contenuto dell'array monodimensionale elementiFileDaOrdinare, 
            {
                calciatore[i] = new calciatore();//a ogni ciclo viene istanziato un nuovo oggetto della classe calciatore
                for (int j = 0; j < 4; j++)//ciclo per l'inserimento dei 4 dati presenti su file per ogni oggetto
                {
                    //serie di if che si occupa della assegnazione dei dati presenti nell'array elementiFileDaOrdinare
                    if (j == 0)//j =0 se si tratta del primo elemento della riga dell'array ossia il nome del calciatore
                    {
                        nomeCalciatore = elementiDaOrdinare[n1];//viene asseganto alla  stringa nomePlayer il valore della cella dell'array
                        calciatore[i].getNome();//viene richiamato il metodo per l'inserimento del nome dell'oggetto
                    }
                    else if (j == 1)//j=1 se si tratta del secondo elemento della riga dell'array ossia il cognome del calciatore
                    {
                        cognomeCalciatore = elementiDaOrdinare[n1];//viene asseganto alla  stringa passwordPlayer il valore della cella dell'array
                        calciatore[i].getCognome();//viene richiamato il metodo per l'inserimento della password dell'oggetto
                    }
                    else if (j == 2)//j=2 se si tratta del terzo elemento della riga dell'array ossia il prezzo del calciatore
                    {
                        prezzoCalciatore = Convert.ToInt32(elementiDaOrdinare[n1]);//viene asseganto all'intero creditiPlayer il valore della cella dell'array
                        calciatore[i].getPrezzo(prezzoCalciatore);//viene richiamato il metodo per l'inserimento dei crediti dell'oggetto
                    }
                    else if (j == 3)//j=3 se si tratta del quarto elemento della riga dell'array ossia il player possessore del calciatore
                    {
                        playerPossessoreCalciatore = elementiDaOrdinare[n1];//viene asseganto all'intero punteggioTotalePlayer il valore della cella dell'array
                        calciatore[i].getPlayerPossessore(playerPossessoreCalciatore);//viene richiamato il metodo per l'inserimento del punteggioTotale dell'oggetto
                        for (int k = 0; k < player.Length; k++)
                        {
                            if (playerPossessoreCalciatore == player[k].nome)
                            {
                                player[k].getRosa(i);
                                player[k].getCrediti(prezzoCalciatore);
                            }
                        }
                    }
                    n1++;//viene aumentato il puntatore alla cella dell'array
                }
            }
            controlloNumeroGiocatori();
        }
        public static void chiusuraProgramma()//funzione che effettua la chiusura del programma 
        {
            Environment.Exit(0);
        }
    }

    internal class calciatore
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
        public void getNome()
        {
            nome = Program.nomeCalciatore;
        }
        public void getCognome()
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
        public void getPunteggioPartita(double punteggio)
        {
            if (nome == "bot")
            {
                punteggioPartita = 0;
            }
            else
            {
                punteggioPartita = punteggio;
            }
        }
    }

    internal class player
    {
        public string nome = "";
        public string password = "";
        public int crediti;
        public int offerta;
        public int[] arrayRosaCalciatori = new int[0];
        public int punteggioTotale;
        public double punteggioGiornata;
        public player()
        {
            nome = "";
            password = "";
            crediti = 1000;
            punteggioTotale = 0;
            punteggioGiornata = 0;
            offerta = 0;
        }
        public void getNome()
        {
            nome = Program.nomePlayer;
        }
        public void getPassword()
        {
            password = Program.passwordPlayer;
        }
        public void resetCrediti()
        {
            crediti = 1000;
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
        public void getPunteggioGiornata(double punteggio)
        {
            punteggioGiornata = punteggio;
        }
        public void getPunteggioTotale(int punti)
        {
            punteggioTotale += punti;
        }
    }
}
