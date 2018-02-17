using ColourMemory.Models;
using ColourMemory.Tools;
using ColourMemory.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
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
   /// Lógica de interacción para PlayView.xaml
   /// </summary>
   public partial class PlayView : Page
   {
      public PlayView()
      {         
         InitializeComponent();
      }

      private void ButtonBack_Click(object sender, RoutedEventArgs e)
      {
         ImageTools.ApplyBlur(Application.Current.MainWindow);
         var result = MessageBox.Show("All your progress will be lost. Leave current game?", "Confirmation required", MessageBoxButton.YesNo);
         ImageTools.SupressEffects(Application.Current.MainWindow);
         if (result.Equals(MessageBoxResult.Yes))
         {
            NavigationService?.Navigate(new StartView());
         }         
      }
   }

   public class DataConverter : IValueConverter
   {
      public static int columnIndex = 0;

      public object Convert(object value, Type targetType, object parameter, CultureInfo culture) // value is a DataRowView, so we have to do some trick
      {
         DataRowView drv = value as DataRowView;
         if (drv != null)
         {
            Card card = (Card) drv.Row.ItemArray[columnIndex];
            columnIndex++;
            if (columnIndex == drv.Row.ItemArray.Count())
            {
               columnIndex = 0;
            }
            return card.VisibleSide.Source;
         }

         return DependencyProperty.UnsetValue;
      }

      public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
      {
         throw new NotImplementedException();
      }
   }

}
