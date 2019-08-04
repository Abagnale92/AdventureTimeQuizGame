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
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// Il modello di elemento per la pagina vuota è documentato all'indirizzo http://go.microsoft.com/fwlink/?LinkID=390556

namespace Adventure_Time_Quiz
{
    /// <summary>
    /// Pagina vuota che può essere utilizzata autonomamente oppure esplorata all'interno di un frame.
    /// </summary>
    public sealed partial class Personaggio : Page
    {

        string source;

        Image img = new Image();

        XDocument loadedCustomData = null;

        public Personaggio()
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
            Frame.Navigate(typeof(Middlepage)); // se voglio tornare indietro di una pagina faccio Frame.Navigate
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {


            ResourceLoader loader = new ResourceLoader();
            string dbchisei = loader.GetString("chiseidb/Text");

            if (dbchisei == "ita")
            {
                loadedCustomData = XDocument.Load("immaginichisei.xml");
            }
            else
            {

                loadedCustomData = XDocument.Load("immaginichiseieng.xml");
            }
            

            if ((VarGlobal.A > VarGlobal.B) && (VarGlobal.A > VarGlobal.C) && (VarGlobal.A > VarGlobal.D) && (VarGlobal.A > VarGlobal.E)) {
                int numero = 1;
                var immagine = from c in loadedCustomData.Descendants("Domanda")
                               where c.Attribute("id").Value == numero.ToString()
                               select c.Attribute("source").Value;
                                source = immagine.First();
                                image.Source = new BitmapImage(new Uri(this.BaseUri, source));
                                var testo = from c in loadedCustomData.Descendants("Domanda")
                                            where c.Attribute("id").Value == numero.ToString()
                                            select c.Attribute("testo").Value;

                                Description.Text = testo.First();
            }

            else if ((VarGlobal.B > VarGlobal.A) && (VarGlobal.B > VarGlobal.C) && (VarGlobal.B > VarGlobal.D) && (VarGlobal.B > VarGlobal.E)){

                int numero = 2;
                var immagine = from c in loadedCustomData.Descendants("Domanda")
                               where c.Attribute("id").Value == numero.ToString()
                               select c.Attribute("source").Value;
                var testo = from c in loadedCustomData.Descendants("Domanda")
                               where c.Attribute("id").Value == numero.ToString()
                               select c.Attribute("testo").Value;

                Description.Text = testo.First();
                source = immagine.First();
                image.Source = new BitmapImage(new Uri(this.BaseUri, source));
            }

            else  if ((VarGlobal.C > VarGlobal.A) && (VarGlobal.C > VarGlobal.B) && (VarGlobal.C > VarGlobal.D) && (VarGlobal.C > VarGlobal.E))
            {

                int numero = 3;
                var immagine = from c in loadedCustomData.Descendants("Domanda")
                               where c.Attribute("id").Value == numero.ToString()
                               select c.Attribute("source").Value;
                var testo = from c in loadedCustomData.Descendants("Domanda")
                            where c.Attribute("id").Value == numero.ToString()
                            select c.Attribute("testo").Value;

                Description.Text = testo.First();
                source = immagine.First();
                image.Source = new BitmapImage(new Uri(this.BaseUri, source));
            }

            else  if ((VarGlobal.D > VarGlobal.A) && (VarGlobal.D > VarGlobal.B) && (VarGlobal.D > VarGlobal.C) && (VarGlobal.D > VarGlobal.E))
            {

                int numero = 4;
                var immagine = from c in loadedCustomData.Descendants("Domanda")
                               where c.Attribute("id").Value == numero.ToString()
                               select c.Attribute("source").Value;
                var testo = from c in loadedCustomData.Descendants("Domanda")
                            where c.Attribute("id").Value == numero.ToString()
                            select c.Attribute("testo").Value;

                Description.Text = testo.First();
                source = immagine.First();
                image.Source = new BitmapImage(new Uri(this.BaseUri, source));
            }

            else   if ((VarGlobal.E > VarGlobal.A) && (VarGlobal.E > VarGlobal.B) && (VarGlobal.E > VarGlobal.D) && (VarGlobal.E > VarGlobal.C))
            {

                int numero = 5;
                var immagine = from c in loadedCustomData.Descendants("Domanda")
                               where c.Attribute("id").Value == numero.ToString()
                               select c.Attribute("source").Value;
                var testo = from c in loadedCustomData.Descendants("Domanda")
                            where c.Attribute("id").Value == numero.ToString()
                            select c.Attribute("testo").Value;

                Description.Text = testo.First();
                source = immagine.First();
                image.Source = new BitmapImage(new Uri(this.BaseUri, source));
            }

            else  if ((VarGlobal.C == VarGlobal.A) && (VarGlobal.C == VarGlobal.B) && (VarGlobal.C == VarGlobal.D) && (VarGlobal.C == VarGlobal.E))
            {

                int numero = 6;
                var immagine = from c in loadedCustomData.Descendants("Domanda")
                               where c.Attribute("id").Value == numero.ToString()
                               select c.Attribute("source").Value;
                var testo = from c in loadedCustomData.Descendants("Domanda")
                            where c.Attribute("id").Value == numero.ToString()
                            select c.Attribute("testo").Value;

                Description.Text = testo.First();
                source = immagine.First();
                image.Source = new BitmapImage(new Uri(this.BaseUri, source));
            }

           else{
             int numero = 7;
                var immagine = from c in loadedCustomData.Descendants("Domanda")
                               where c.Attribute("id").Value == numero.ToString()
                               select c.Attribute("source").Value;
                var testo = from c in loadedCustomData.Descendants("Domanda")
                            where c.Attribute("id").Value == numero.ToString()
                            select c.Attribute("testo").Value;

                Description.Text = testo.First();
                source = immagine.First();
                image.Source = new BitmapImage(new Uri(this.BaseUri, source));
            } 

            }

        private void Button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

           

        }
    }

