using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Resources;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Phone.UI.Input;
using Windows.Storage;
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
    public sealed partial class Punteggio : Page
    {
        public Punteggio()
        {
            this.InitializeComponent();
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

        private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {

            e.Handled = true;
            Frame.Navigate(typeof(MainPage)); // se voglio tornare indietro di una pagina faccio Frame.Navigate
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            
            switch(VarGlobal.mod){

                case 1:{

                    if (VarGlobal.dom <= 30)
                    {
                        totdomande.Text = VarGlobal.dom.ToString();
                    }
                    else
                    { totdomande.Text = "30"; }

                totpunteggio.Text = VarGlobal.punteggio.ToString();
                int punti = (int)ApplicationData.Current.LocalSettings.Values["PunteggioQuiz"];
                if (VarGlobal.punteggio > punti)
                {
                    ApplicationData.Current.LocalSettings.Values["PunteggioQuiz"] = VarGlobal.punteggio;
                }
                    break;
                }

                case 2:
                    {
                        ResourceLoader loader = new ResourceLoader();
                        string prova = loader.GetString("Corrette/Text");
                        VarGlobal.domt = VarGlobal.domt - 1; //esce una domanda in +
                        totdomande.Text = VarGlobal.domt.ToString();
                        corrette.Visibility = Visibility.Visible;
                        corrette.Text = prova+" "+ VarGlobal.dom.ToString();
                        totpunteggio.Text = VarGlobal.punteggiot.ToString();
                        int punti = (int)ApplicationData.Current.LocalSettings.Values["PunteggioTime"];
                        if (VarGlobal.punteggiot > punti)
                        {
                            ApplicationData.Current.LocalSettings.Values["PunteggioTime"] = VarGlobal.punteggiot;
                        }
                        break;
                    }

                case 3:
                    {
                        domande.Visibility = Visibility.Collapsed;
                        punteggio.Visibility = Visibility.Collapsed;
                        player1.Visibility = Visibility.Visible;
                        player2.Visibility = Visibility.Visible;
                        totdomande.Text = VarGlobal.punti1.ToString();
                        totpunteggio.Text = VarGlobal.punti2.ToString();
                        PlayerVictory.Visibility = Visibility.Visible;

                        if (VarGlobal.punti1 > VarGlobal.punti2)
                        {
                            PlayerVictory.Text += " Player 1";
                        }
                        else
                        {
                            PlayerVictory.Text += " Player 2";
                        }


                        break;
                    }
                case 4:
                    {
                        VarGlobal.dom -= 1;
                        totdomande.Text = VarGlobal.dom.ToString();
                        totpunteggio.Text = VarGlobal.punteggio.ToString();
                        break;
                    }
                        

            }
           

        }

        private void Button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }
    }
}
