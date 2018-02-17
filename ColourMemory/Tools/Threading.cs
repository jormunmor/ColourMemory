using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace ColourMemory.Tools
{
   /// <summary>
   /// This class is thought to help other threads execute code in the main thread.
   /// </summary>
   /// <remarks>
   /// Using this avoid exceptions related with the owner of objects used.
   /// </remarks>
   public static class Threading
   {
      /// <summary>
      /// Executes the given action in the main thread.
      /// </summary>
      /// <param name="action">The action to execute.</param>
      public static void Invoke(Action action)
      {
         Dispatcher dispatchObject = Application.Current.Dispatcher;
         if (dispatchObject == null || dispatchObject.CheckAccess())
         {
            action();
         }
         else
         {
            dispatchObject.Invoke(action);
         }
      }      
   }
}
