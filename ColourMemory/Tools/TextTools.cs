using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColourMemory.Tools
{
   /// <summary>
   /// Static helper class to manage the game records.
   /// </summary>
   /// <remarks>
   /// A easy implementation has been choosen. The records are stored in a text file
   /// in the same directory on which the executable file is. Only values that are greater
   /// than all the values in the file are stored as new records. Be careful, as if the directory
   /// need administrator privileges, you must run the application as administrator or an 
   /// exception will be thrown when trying to create or edit the file. No exception handling is done
   /// here for clarity and simplicity (and available time). If you want to reset the records, you must delete
   /// the file yourself.
   /// </remarks>
   /// TODO: manage IO exceptions.
   /// TODO: give the user the option of reset records.
   public static class TextTools
   {
      /// <summary>
      /// This method returns the content of the Records file as a list of integers.
      /// </summary>
      /// <returns>A list of integers with all the records.</returns>
      public static List<int> GetRecords()
      {
         if(!File.Exists(ConfigurationManager.AppSettings["record_file_path"]))
         {
            FileStream stream = File.Create(ConfigurationManager.AppSettings["record_file_path"]);
            stream.Close();
         }
         string[] content = File.ReadAllLines(ConfigurationManager.AppSettings["record_file_path"]);         

         return Array.ConvertAll(content, int.Parse).ToList(); 
      }

      /// <summary>
      /// This method returns the content of the records file as string.
      /// </summary>
      /// <returns>A string with the content of the records file.</returns>
      public static string GetRecordsText()
      {
         if (!File.Exists(ConfigurationManager.AppSettings["record_file_path"]))
         {
            FileStream stream = File.Create(ConfigurationManager.AppSettings["record_file_path"]);
            stream.Close();
         }

         return File.ReadAllText(ConfigurationManager.AppSettings["record_file_path"]);
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="records">A int array with the records, sorted in decreasing order.</param>
      public static void SaveRecords(int[] records)
      {
         if (File.Exists(ConfigurationManager.AppSettings["record_file_path"]))
         {
            File.Delete(ConfigurationManager.AppSettings["record_file_path"]);
         }
         FileStream stream = File.Create(ConfigurationManager.AppSettings["record_file_path"]);
         stream.Close();
         File.WriteAllLines(ConfigurationManager.AppSettings["record_file_path"], records.Select(d => d.ToString()));
      }
   }
}
