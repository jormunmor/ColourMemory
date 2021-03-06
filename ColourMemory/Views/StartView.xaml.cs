﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ColourMemory.Views
{
   /// <summary>
   /// Lógica de interacción para StartView.xaml
   /// </summary>
   public partial class StartView : Page
   {
      public StartView()
      {
         InitializeComponent();
      }

      private void Button_Play_Click(object sender, RoutedEventArgs e)
      {
         NavigationService?.Navigate(new PlayView());
      }

      private void Button_Exit_Click(object sender, RoutedEventArgs e)
      {
         Application.Current.MainWindow.Close();
      }

      private void Button_Scores_Click(object sender, RoutedEventArgs e)
      {
         NavigationService?.Navigate(new RecordsView());
      }
   }
}
