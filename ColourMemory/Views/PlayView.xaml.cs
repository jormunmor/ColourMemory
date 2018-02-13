using ColourMemory.ViewModels;
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
   /// Lógica de interacción para PlayView.xaml
   /// </summary>
   public partial class PlayView : Page
   {
      public PlayView()
      {
         InitializeComponent();
         CardsViewModel vm = new CardsViewModel();
         this.DataContext = vm;
         vm.PrintCardDeck();
      }
   }
}
