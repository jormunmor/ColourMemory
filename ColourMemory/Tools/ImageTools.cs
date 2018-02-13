using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ColourMemory.Tools
{
   class ImageTools
   {
      public static BitmapSource CreateBitmapSource(System.Windows.Media.Color color)
      {
         int width = 128;
         int height = width;
         int stride = width / 8;
         byte[] pixels = new byte[height * stride];

         List<System.Windows.Media.Color> colors = new List<System.Windows.Media.Color>();
         colors.Add(color);
         BitmapPalette myPalette = new BitmapPalette(colors);

         BitmapSource image = BitmapSource.Create(
             width,
             height,
             96,
             96,
             PixelFormats.Indexed1,
             myPalette,
             pixels,
             stride);

         return image;
      }

      public static List<Color> GenerateRandomColorList(int size)
      {
         List<Color> colorList = new List<Color>(size);
         Random random = new Random();
         for (int i = 0; i < size / 2; i++)
         {
            Color color = Color.FromRgb((byte)random.Next(255), (byte)random.Next(255), (byte)random.Next(255));
            colorList.Add(color);
            colorList.Add(color);
         }

         return colorList;
      }
   }
}
