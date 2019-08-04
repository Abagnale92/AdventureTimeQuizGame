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
    public sealed partial class level : Page
    {

        //VARIABILE PER IL CONTROLLO DELLE DOMANDE CHE NON SI DEVONO RIPETERE
        bool controllo_id = false;

        //CREO ED INIZIALIZZO LE VARIABILI INERENTI ALLA DOMANDA
        string check;
        // VARIABILE DI CONFRONTO  
        string c;

        int i;
        // Conta gli errori;
        int count = 0;
        DispatcherTimer dt;

        XDocument loadedCustomData = null;
        

        private Button rispScelta;
        public level()
        {
            this.InitializeComponent();
            VarGlobal.dom = 0;
            VarGlobal.punteggio = 0;

        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
           
            int RandomNumber;
            ResourceLoader loader = new ResourceLoader();
            string dbchisei = loader.GetString("chiseidb/Text");


            if (dbchisei == "ita")
            {
                loadedCustomData = XDocument.Load("livello1.xml");
            }
            else
            {

                loadedCustomData = XDocument.Load("livello1eng.xml");
            }


            if (VarGlobal.dom == 30 && count == 0)
            {
                Frame.Navigate(typeof(Congratulation));
            }
            else if (VarGlobal.dom == 30)
            {
                Frame.Navigate(typeof(Punteggio));
            }

            VarGlobal.mod = 1;


            //INIZIALIZZO IL GENERATORE DI NUMERI CASUALI
                Random rnd = new Random(DateTime.Now.Millisecond);
                int RandomId = 0;
                do
                {

                    controllo_id = false;
                    //RICHEDO UN NUMERO CASUALE COMPRESO FRA UN MINIMO DI 1 E UN MASSIMO DI ID  DELLA DOMANDA
                    RandomId = rnd.Next(1,80);
                    for (i = 0; i <30; i++)
                    {
                        if (VarGlobal.vettore[i] == RandomId)
                        {
                            i = 30;
                            controllo_id = true;
                        }
                    }
                } while (controllo_id == true);

                VarGlobal.vettore[VarGlobal.dom] = RandomId;
                
                VarGlobal.dom += 1;

                var domanda = from c in loadedCustomData.Descendants("Domanda")
                              where c.Attribute("id").Value == RandomId.ToString()
                              select c.Attribute("Testo").Value;

                var risposta = from c in loadedCustomData.Descendants("Domanda")
                               where c.Attribute("id").Value == RandomId.ToString()
                               select c.Attribute("Risposta").Value;
                check = risposta.First();

                var errata1 = from c in loadedCustomData.Descendants("Domanda")
                              where c.Attribute("id").Value == RandomId.ToString()
                              select c.Attribute("Errata1").Value;

                var errata2 = from c in loadedCustomData.Descendants("Domanda")
                              where c.Attribute("id").Value == RandomId.ToString()
                              select c.Attribute("Errata2").Value;

                var errata3 = from c in loadedCustomData.Descendants("Domanda")
                              where c.Attribute("id").Value == RandomId.ToString()
                              select c.Attribute("Errata3").Value;

                //RICHEDO UN NUMERO CASUALE COMPRESO FRA UN MINIMO DI 1 E UN MASSIMO DI 4
                RandomNumber = rnd.Next(1, 5);


                //ASSEGNO LA DOMANDA (PRIMO RISULTATO DELLA QUERY) AL SUO TEXTBLOCK
                Domanda.Text = domanda.First();


                switch (RandomNumber)
                {
                    case 1:
                        {
                            risp1.Content = risposta.First();
                            risp2.Content = errata1.First();
                            risp3.Content = errata2.First();
                            risp4.Content = errata3.First();
                            break;
                        }
                    case 2:
                        {
                            risp1.Content = errata2.First();
                            risp2.Content = risposta.First();
                            risp3.Content = errata1.First();
                            risp4.Content = errata3.First();
                            break;
                        }
                    case 3:
                        {
                            risp1.Content = errata2.First();
                            risp2.Content = errata1.First();
                            risp3.Content = risposta.First();
                            risp4.Content = errata3.First();
                            break;
                        }
                    case 4:
                        {
                            risp1.Content = errata3.First();
                            risp2.Content = errata1.First();
                            risp3.Content = errata2.First();
                            risp4.Content = risposta.First();
                            break;

                        }

                }

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
         //  dt.Stop();
            var messageDialog = new Windows.UI.Popups.MessageDialog(resource1);
            messageDialog.Commands.Add(new UICommand(resource2, (command) =>
            {
              // dt.Stop();
                this.Frame.Navigate(typeof(QuizPage));

            }));
            messageDialog.Commands.Add(new UICommand("No", (command) => { }));
            messageDialog.CancelCommandIndex = 1;
            await messageDialog.ShowAsync();


        }



        private void risp1_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ResourceLoader loader = new ResourceLoader();
            string sbagliato = loader.GetString("Sbagliato/Text");
            string giusto = loader.GetString("Giusto/Text");
            //IMPOSTO IL TIMER
            dt = new DispatcherTimer();
            dt.Interval = new TimeSpan(0, 0, 0, 0, 1000);
            dt.Tick += dt_Tick;

            //AVVIO IL TIMER
            dt.Start();


            rispScelta = (Button)sender;
            c = rispScelta.Content.ToString();
            // se la risposta è corretta
            if (c == check)
            {
                prova.Content = giusto;
                prova.Visibility = Visibility.Visible;
                VarGlobal.punteggio += 5;
                PhoneApplicationPage_Loaded(sender, e);
            }

            else
            {
                count++;
                Errore.Text = "Errori : " + count;
                prova.Content = sbagliato;
                prova.Visibility = Visibility.Visible;
                if(count==3)
                {
                    
                    Frame.Navigate(typeof(Punteggio));
                }
                else
                {
                    PhoneApplicationPage_Loaded(sender, e);
                }

            }

        }


        void dt_Tick(object sender, object e)
        {
            //FERMO IL TIMER 
            dt.Stop();
            // controllo_messaggio = false;
            prova.Visibility = Visibility.Collapsed;

        }

    }
}
