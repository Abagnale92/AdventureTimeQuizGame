using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Xml.Linq;
using Windows.ApplicationModel.Resources;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Phone.UI.Input;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Il modello di elemento per la pagina vuota è documentato all'indirizzo http://go.microsoft.com/fwlink/?LinkID=390556

namespace Adventure_Time_Quiz
{
    /// <summary>
    /// Pagina vuota che può essere utilizzata autonomamente oppure esplorata all'interno di un frame.
    /// </summary>
    public sealed partial class Quiztime : Page
    {

         int t = 30;

        //VARIABILE PER IL CONTROLLO DELLE DOMANDE CHE NON SI DEVONO RIPETERE
        bool controllo_id = false;


        //CREO ED INIZIALIZZO LE VARIABILI INERENTI ALLA DOMANDA
        //string uri;
        string  check;

        //CREO INIZIALIZZO LA VARIABILE DI CONTROLLO OGNI VOLTA CHE FACCIO UNA DOMANDA
      //  bool controllo_risp = true;
       // bool controllo_messaggio = true;
        int i;

        DispatcherTimer dt;
        
            XDocument loadedCustomData = null;
        public Quiztime()
        {
            this.InitializeComponent();
            //IMPOSTO IL TIMER
            dt = new DispatcherTimer();
            dt.Interval = new TimeSpan(0, 0, 0, 0, 1000);
            dt.Tick += dt_Tick;
            VarGlobal.domt = 0;
            VarGlobal.dom = 0;
            VarGlobal.punteggiot = 0;
            //AVVIO IL TIMER
            dt.Start();

            Timer.Text = t.ToString();
        }

        /// <summary>
        /// Richiamato quando la pagina sta per essere visualizzata in un Frame.
        /// </summary>
        /// <param name="e">Dati dell'evento in cui vengono descritte le modalità con cui la pagina è stata raggiunta.
        /// Questo parametro viene in genere utilizzato per configurare la pagina.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            HardwareButtons.BackPressed += HardwareButtons_BackPressed;
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            //remove the handler before you leave!    
            HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
        }

        private async void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {

           ResourceLoader loader = new ResourceLoader();
            string resource1 = loader.GetString("Domanda/Text");
            string resource2 = loader.GetString("Si/Text");


            e.Handled = true;
           
            dt.Stop();
            var messageDialog = new Windows.UI.Popups.MessageDialog(resource1);
            messageDialog.Commands.Add(new UICommand(resource2, (command) =>
            {
               
                dt.Stop();
                this.Frame.Navigate(typeof(QuizPage));

            }));
            messageDialog.Commands.Add(new UICommand("No", (command) => { dt.Start(); }));
            messageDialog.CancelCommandIndex = 1;
            await messageDialog.ShowAsync();
        }
        

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (VarGlobal.domt > 99) {
                dt.Stop();
                VarGlobal.mod = 2;
                Frame.Navigate(typeof(Punteggio));
            }

            ResourceLoader loader = new ResourceLoader();
            string dbchisei = loader.GetString("chiseidb/Text");

            if (dbchisei == "ita")
            {
                loadedCustomData = XDocument.Load("VeroFalso.xml");
            }
            else
            {

                loadedCustomData = XDocument.Load("VeroFalsoeng.xml");
            }

            //INIZIALIZZO IL GENERATORE DI NUMERI CASUALI
            Random rnd = new Random(DateTime.Now.Millisecond);
            int RandomId = 0;
            do
            {
                controllo_id = false;
                //RICHEDO UN NUMERO CASUALE COMPRESO FRA UN MINIMO DI 1 E UN MASSIMO DI 100 CHE SARA' L'ID DELLA DOMANDA
                RandomId = rnd.Next(1, 103);
                for (i = 0; i < VarGlobal.domt; i++)
                {
                    if (VarGlobal.vettore[i] == RandomId)
                    {
                        i = VarGlobal.domt;
                        controllo_id = true;
                    }
                }
            } while (controllo_id == true);

            VarGlobal.vettore[VarGlobal.domt] = RandomId;
            VarGlobal.domt++;

            var domanda = from c in loadedCustomData.Descendants("Domanda")
                          where c.Attribute("id").Value == RandomId.ToString()
                          select c.Attribute("Testo").Value;

            var risposta = from c in loadedCustomData.Descendants("Domanda")
                           where c.Attribute("id").Value == RandomId.ToString()
                           select c.Attribute("Risposta").Value;
            check = risposta.First();

            

            //ASSEGNO LA DOMANDA (PRIMO RISULTATO DELLA QUERY) AL SUO TEXTBLOCK
            labelDomanda.Text = domanda.First();

        }
        void dt_Tick(object sender, object e)
        {

            //CONTROLLO DEL TIMER (VIENE ESEGUITO OGNI SECONDO)
            if (t <= -1)
            {
                //FERMO IL TIMER E TERMINO LA PARTITA
                dt.Stop();
               // controllo_messaggio = false;
                VarGlobal.mod = 2;
                 Frame.Navigate(typeof(Punteggio));
                
            }
            else
            {
                //AGGIORNO LA TEXTBLOCK DEL TIMER
                Timer.Text = t.ToString();
                t--;
            }

           

        }

        private void Button_Tapped(object sender, TappedRoutedEventArgs e)
        {


            
            Button rispScelta = (Button)sender;
            string c = rispScelta.Content.ToString();
            
            if (c == check)
            {
                VarGlobal.dom += 1;
                VarGlobal.punteggiot += 5;
                Page_Loaded(sender, e);
            }

            else
            {
                Page_Loaded(sender, e);
            }
           
        }
      }
    }

