using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Resources;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Phone.UI.Input;
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
    public sealed partial class Istruction : Page
    {
        public Istruction()
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

        private void Button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

            ResourceLoader loader = new ResourceLoader();
               string prova = loader.GetString("Istruzioni/Text");
               if (prova=="ita")
               {

                   istruzioni.Text = "Salve a tutti i fan di Adventure Time! In questa app troverete diversi Minigiochi : " + "\n\n" +
                       "Nella modalita' Quiz Time, ci saranno 3 tipologie di quiz : " + "\n\n" + "- QUIZ : scopri quante ne sai sul mondo di Adventure Time! sono consentiti solo 3 errori su 30 domande, quindi attenzione!" + "\n\n" +
                       "- Duello : Sfida un tuo amico!" + "\n\n" + "- Modalita' a Tempo : rispondi in 30 secondi a piu' domande possibili e fai il miglior punteggio!" + "\n\n" + " - Indovina chi? : conosci tutti i personaggi di Adventure time? provalo! " + "\n\n" +
                        " - Chi sei? : scopri quale personaggio di Adventure Time sei!" + "\n\n" + " Buon Divertimento! ";
               }
               else { 
                   
                   istruzioni.Text = "Hello to all the fans of Adventure Time! In this app you will find several mini-games:" + "\n \n" +
                 "In the mode 'Quiz Time, there will be 3 types of quiz:" + "\n \n" + "- QUIZ: find out how much you know about the world of Adventure Time! Are allowed only 3 errors on 30 questions, so watch out!" + "\n \n" +
                 "- Duel: Challenge your friend!" + "\n \n" + "- Hurry Up!: answer in 30 seconds at the most' questions as possible and make the best score!" + "\n \n" + "- Who is that?: You know all the characters of Adventure time? Try it!" + "\n \n" +
                  "- Who are you?: Discover which character of Adventure Time are!" + "\n \n" + "Have Fun!"; }

              }

 
    }
}
