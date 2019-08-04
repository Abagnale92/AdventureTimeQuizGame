﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
    public sealed partial class Middlepage : Page
    {
        public Middlepage()
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
            Frame.Navigate(typeof(QuizPage));
        }

        private void Indovina_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(Indovina));
        }

        private void ChiSei_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(Chisei));
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await Windows.UI.ViewManagement.StatusBar.GetForCurrentView().HideAsync();
            Storyboard1.Begin();
        }

        private void Button1_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }
    }
}
