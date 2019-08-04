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
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Il modello di elemento per la pagina vuota è documentato all'indirizzo http://go.microsoft.com/fwlink/?LinkId=391641

namespace Adventure_Time_Quiz
{
    /// <summary>
    /// Pagina vuota che può essere utilizzata autonomamente oppure esplorata all'interno di un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        /// <summary>
        /// Richiamato quando la pagina sta per essere visualizzata in un Frame.
        /// </summary>
        /// <param name="e">Dati dell'evento in cui vengono descritte le modalità con cui la pagina è stata raggiunta.
        /// Questo parametro viene in genere utilizzato per configurare la pagina.</param>
        


       //QUesto serve per levare batteria orologio ecc. async
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Object vota = ApplicationData.Current.LocalSettings.Values["vota"]; // serve per capire qundo deve uscire il messagg box
            Object stop = ApplicationData.Current.LocalSettings.Values["stop"]; // serve per non far uscire più il messag box


            if (vota == null) // se è nullo ( all'inzio è ovvio)
            {
                ApplicationData.Current.LocalSettings.Values["vota"] = 4; // vota a 4 per esempio imposto , così dopo la seconda volta che va in main page gia esce, poi dopo uscirà dopo ogni 6 volte
                ApplicationData.Current.LocalSettings.Values["stop"] = 0; // stop a 0 per ora
            }

            if ((int)ApplicationData.Current.LocalSettings.Values["stop"] == 1) // se stop diventa 1 vota sarà sempre zero e quindi se è sempre zero non sarà mai maggiore di 5( vedi dopo) e quindi non esce più il messag box
            {
                ApplicationData.Current.LocalSettings.Values["vota"] = 0;
            }

            ApplicationData.Current.LocalSettings.Values["vota"] = (int)ApplicationData.Current.LocalSettings.Values["vota"] + 1; // incremento vota ogni volta che l'untente va in mainpage
            if ((int)ApplicationData.Current.LocalSettings.Values["vota"] > 5)  //se è maggior di 5 allaora faccio tutto quello dopo , se no nulla
            {
                ResourceLoader loader = new ResourceLoader();
                string resource1 = loader.GetString("Store/Text");
                string resource2 = loader.GetString("Si/Text");

                var messageDialog = new Windows.UI.Popups.MessageDialog(resource1);
                messageDialog.Commands.Add(new UICommand(resource2, (command) =>
                {
                    store(sender, e);         // se si evoco sta funzione store che trovi sotto che lo rimanda allo store
                    ApplicationData.Current.LocalSettings.Values["stop"] = 1; // metto  a 1 stop, così non esce più il messagg
                }));
                messageDialog.Commands.Add(new UICommand("No", (command) => { ApplicationData.Current.LocalSettings.Values["vota"] = 0; })); // se no imposto il
                messageDialog.CancelCommandIndex = 1;
                await messageDialog.ShowAsync();
            } 



            await Windows.UI.ViewManagement.StatusBar.GetForCurrentView().HideAsync();
            JakeAnimation.Begin();

            Object valueQuiz = ApplicationData.Current.LocalSettings.Values["PunteggioQuiz"];

            Object valueTime = ApplicationData.Current.LocalSettings.Values["PunteggioTime"];

            if(valueQuiz == null){
                ApplicationData.Current.LocalSettings.Values["PunteggioQuiz"] = 0;
            }

            if (valueTime == null)
            {
                ApplicationData.Current.LocalSettings.Values["PunteggioTime"] = 0;
            }

            
        }

        private async void store(object sender, RoutedEventArgs e)
        {
            string appid = "a05cac85-3b7b-4879-b78b-d67b940f9adf"; 
            var uri = new Uri(string.Format("ms-windows-store:reviewapp?appid={0}", appid));
            await Windows.System.Launcher.LaunchUriAsync(uri);
        }
        // questo serve per far navigare il bottone
        private void Button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(Middlepage));
        }

        //per tornare indietro con il bottone fisico
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
            Application.Current.Exit(); // se voglio tornare indietro di una pagina faccio Frame.Navigate
        }

        private void Record_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(Record));
        }

        private void Credits_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(Credit));
        }

        private void Istruction_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(Istruction));
        }
     
 
    }
}
