using ColourMemory.Tools;
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

namespace ColourMemory.Views
{
   /// <summary>
   /// Lógica de interacción para RecordsView.xaml
   /// </summary>
   public partial class RecordsView : Page
   {
      public RecordsView()
      {
         InitializeComponent();
      }

      private void ButtonBack_Click(object sender, RoutedEventArgs e)
      {
         NavigationService?.Navigate(new StartView());
      }

      private void TextBlock_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
      {
         if(RecordTextBlock.IsVisible)
         {
            RecordTextBlock.Text = TextTools.GetRecordsText();
         }
      }
   }
}
