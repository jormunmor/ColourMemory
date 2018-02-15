using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColourMemory.ViewModels
{
    class ViewModelLocator
    {
      // Add service models here
      public CardsViewModel CardsViewModel => new CardsViewModel();
   }
}
