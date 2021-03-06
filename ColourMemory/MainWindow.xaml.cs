﻿using ColourMemory.Tools;
using System;
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

namespace ColourMemory
{
   /// <summary>
   /// Interaction logic for MainWindow.xaml
   /// </summary>
   /// <remarks> 
   /// This is our main window. It's a navigation window. It will hold the different views.
   /// </remarks>
   public partial class MainWindow
   {
      public NavigationService navigationService;

      public MainWindow()
      {
         InitializeComponent();
      }

      /// <summary>
      ///   This is the event callback for the main window's close button.
      /// </summary>
      /// <remarks> A MessageBox is shown to confirm if the user wants to exit de game. While open,
      /// the background window will be shown blurred. The effect disappears if the user cancels.
      /// </remarks>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void NavigationWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
      {
         ImageTools.ApplyBlur(Application.Current.MainWindow);         
         var result = MessageBox.Show("Are you sure you want to exit?", "Confirmation Required", MessageBoxButton.YesNo);
         ImageTools.SupressEffects(Application.Current.MainWindow);
         if (result.Equals(MessageBoxResult.No))
         {
            e.Cancel = true;
         }
      }
   }
}
