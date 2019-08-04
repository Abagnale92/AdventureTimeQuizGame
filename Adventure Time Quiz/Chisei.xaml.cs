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
using Windows.UI.Xaml.Navigation;

// Il modello di elemento per la pagina vuota è documentato all'indirizzo http://go.microsoft.com/fwlink/?LinkID=390556

namespace Adventure_Time_Quiz
{
    /// <summary>
    /// Pagina vuota che può essere utilizzata autonomamente oppure esplorata all'interno di un frame.
    /// </summary>
    public sealed partial class Chisei : Page
    {


        // VARIABILE DI CONFRONTO  
        string c;


        XDocument loadedCustomData = null;


        private Button rispScelta;

        public Chisei()
        {
            this.InitializeComponent();
            VarGlobal.dom = 0;
            VarGlobal.A = 0;
            VarGlobal.B = 0;
            VarGlobal.C = 0;
            VarGlobal.D = 0;
            VarGlobal.E = 0;
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
                loadedCustomData = XDocument.Load("chisei.xml");
            }
            else {

                loadedCustomData = XDocument.Load("chiseieng.xml");
                }


            VarGlobal.dom += 1;

            var domandaa = from c in loadedCustomData.Descendants("Domanda")
                          where c.Attribute("id").Value == VarGlobal.dom.ToString()
                          select c.Attribute("Testo").Value;

            var a = from c in loadedCustomData.Descendants("Domanda")
                    where c.Attribute("id").Value == VarGlobal.dom.ToString()
                           select c.Attribute("a").Value;

            var b = from c in loadedCustomData.Descendants("Domanda")
                    where c.Attribute("id").Value == VarGlobal.dom.ToString()
                          select c.Attribute("b").Value;

            var cc = from c in loadedCustomData.Descendants("Domanda")
                     where c.Attribute("id").Value == VarGlobal.dom.ToString()
                          select c.Attribute("c").Value;

            var d = from c in loadedCustomData.Descendants("Domanda")
                    where c.Attribute("id").Value == VarGlobal.dom.ToString()
                          select c.Attribute("d").Value;

            var ee = from c in loadedCustomData.Descendants("Domanda")
                     where c.Attribute("id").Value == VarGlobal.dom.ToString()
                    select c.Attribute("e").Value;

            //ASSEGNO LA DOMANDA (PRIMO RISULTATO DELLA QUERY) AL SUO TEXTBLOCK
            Domanda.Text = domandaa.First();

            risp1.Content = a.First();
            risp2.Content = b.First();
            risp3.Content = cc.First();
            risp4.Content = d.First();
            risp5.Content = ee.First();

         

        }

        private void Button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            rispScelta = (Button)sender;
            c = rispScelta.Content.ToString();

            if (c == risp1.Content.ToString()) {
                VarGlobal.A += 1;
            
            }
            if (c == risp2.Content.ToString())
            {
                VarGlobal.B += 1;

            }
            if (c == risp3.Content.ToString())
            {
                VarGlobal.C += 1;

            }
            if (c == risp4.Content.ToString())
            {
                VarGlobal.D += 1;

            }
            if (c == risp5.Content.ToString())
            {
                VarGlobal.E += 1;

            }

            Page_Loaded(sender, e);

            if (VarGlobal.dom > 10) {
                Frame.Navigate(typeof(Personaggio)); 
            }

        }



    }
}
