using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using static System.Console;

namespace ColourMemory.Views
{
   /// <summary>
   /// Interaction logic for LogoView.xaml. This is the first view that is displayed when the
   /// application is started.
   /// </summary>
   /// <remarks>The intention of this view is to show the logo of the game during a few seconds
   /// and then navigate to the StartView, which holds the game menu.
   /// </remarks>
   public partial class LogoView : Page
   {
      public LogoView()
      {
         InitializeComponent();
      }

      /// <summary>
      ///   Visibility event callback used to know when the image is visible for the user.
      /// </summary>
      /// <remarks>
      ///   The purpose of this callback is to launch a thread that will wait for a few seconds
      ///   before navigating to the StartView. The thread is needed to avoid blocking the
      ///   GUI thread, as the mouse cursor will change to notify the blocking and the user will see it.
      /// </remarks>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void Image_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
      {
         bool visible = (bool) e.NewValue;
         if (visible)
         {            
            Thread thread = new Thread(SleepAndNavigate);
            thread.Start();
         }
      }

      /// <summary>
      ///   Thread function to show the logo during a seconds and automatically navigate to the StartWindow.
      /// </summary>
      /// <remarks>
      ///   We need to use the Dispatcher because from this thread we can't manage UI thread objects.
      ///   This thread doesn't really shows the logo, as it appears when the page is rendered, but lets the user
      ///   see it during a few seconds before starting the game.
      /// </remarks>
      private void SleepAndNavigate()
      {
         try
         {
            Thread.Sleep(1500);
            Dispatcher.Invoke(() =>
            {
               NavigationService?.Navigate(new StartView());
            });
         } catch(System.Threading.Tasks.TaskCanceledException)
         {
            // This exception will raise if the user presses the close button before navigating to the StartView.
            // We only print in console for debugging purposes.
            WriteLine("Task cancelled.");
         }
      }
   }
}
