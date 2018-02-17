using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColourMemory.ViewModels
{
   /// <summary>
   /// Helper class to hold all the view models instances.
   /// </summary>
   /// <remarks>
   /// The main purpose is to reference the instances as static resources, so it's good
   /// to have all view models instances under the same namespace and class. This is not
   /// a singleton class, as every time a property is get, a new instance of the view model
   /// is created and returned.
   /// </remarks>   
    public class ViewModelLocator
    {
      // Add new view models here
      public CardsViewModel CardsViewModel => new CardsViewModel();
   }
}
