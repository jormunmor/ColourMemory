using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColourMemory.Tools
{
   public static class TextTools
   {
      public static List<int> GetRecords()
      {
         if(!File.Exists("Records.txt"))
         {
            FileStream stream = File.Create("Records.txt");
            stream.Close();
         }
         string[] content = File.ReadAllLines("Records.txt");         

         return Array.ConvertAll(content, int.Parse).ToList(); 
      }

      public static string GetRecordsText()
      {
         if (!File.Exists("Records.txt"))
         {
            FileStream stream = File.Create("Records.txt");
            stream.Close();
         }

         return File.ReadAllText("Records.txt");
      }

      public static void SaveRecords(int[] records)
      {
         if (File.Exists("Records.txt"))
         {
            File.Delete("Records.txt");
         }
         FileStream stream = File.Create("Records.txt");
         stream.Close();
         File.WriteAllLines("Records.txt", records.Select(d => d.ToString()));
      }
   }
}
