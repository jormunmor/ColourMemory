using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColourMemory.Models
{
   class BaseModel : INotifyPropertyChanged
   {
      public void RaisePropertyChanged(string prop)
      {
         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
      }
      public event PropertyChangedEventHandler PropertyChanged;
   }
}
