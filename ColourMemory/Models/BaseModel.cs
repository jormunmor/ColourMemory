using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ColourMemory.Models
{
   /// <summary>
   /// Base class for the models.
   /// </summary>
   public class BaseModel : INotifyPropertyChanged
   {
      public void RaisePropertyChanged(string prop)
      {
         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
      }
      public event PropertyChangedEventHandler PropertyChanged;
   }
}
