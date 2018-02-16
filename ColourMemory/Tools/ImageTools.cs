using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ColourMemory.Tools
{
   /// <summary>
   /// Helper class to manage image and graphics drawing.
   /// </summary>
   public static class ImageTools
   {
      /// <summary>
      /// This methods creates an image given a color.
      /// </summary>
      /// <param name="color">The color of the image.</param>
      /// <returns>The new image as BitmapSource.</returns>
      public static BitmapSource CreateBitmapSource(System.Windows.Media.Color color)
      {
         int width = 80;
         int height = 100;
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

      /// <summary>
      /// This method generates a random list of colors.
      /// </summary>
      /// <param name="size">The list size</param>
      /// <returns>The list of colors.</returns>
      /// <remarks>
      /// Each color is repeated twice, for the GameColour rules.
      /// </remarks>
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

      public static void ApplyBlur(Window win)
      {
         var objBlur = new System.Windows.Media.Effects.BlurEffect();
         objBlur.Radius = 10;
         win.Effect = objBlur;
      }

      public static void SupressEffects(Window win)
      {
         win.Effect = null;
      }
   }
}
