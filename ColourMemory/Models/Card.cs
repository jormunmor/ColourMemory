using ColourMemory.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ColourMemory.Models
{
   /// <summary>
   /// This class represent a card.
   /// </summary>
   /// <remarks>
   /// It contains a card color, an image for the back side, an image for the front side, 
   /// the side that is currently visible by the user, and an identifier for debugging purposes.
   /// </remarks>
   class Card : BaseModel
   {
      /// <summary>
      ///  The color of the card.
      /// </summary>
      Color cardColor;
      public Color CardColor
      {
         get
         {
            return cardColor;
         }
         set
         {
            if (cardColor != value)
            {
               cardColor = value;
               RaisePropertyChanged("CardColor");
            }
         }
      }

      /// <summary>
      /// The image used as the back side of the card.
      /// </summary>
      /// <remarks>
      /// Will be displayed when the card is flipped down by the user.
      /// </remarks>
      Image backImage;
      public Image BackImage
      {
         get
         {
            return backImage;
         }
         set
         {
            if (backImage != value)
            {
               backImage = value;
               RaisePropertyChanged("BackImage");
            }
         }
      }

      /// <summary>
      /// The image used as the front side of the card.
      /// </summary>
      /// <remarks>
      /// Will be displayed when the card is flipped up by the user.
      /// </remarks>
      Image frontImage;
      public Image FrontImage
      {
         get
         {
            return frontImage;
         }
         set
         {
            if (frontImage != value)
            {
               frontImage = value;
               RaisePropertyChanged("FrontImage");
            }
         }
      }

      /// <summary>
      /// The image that is currently visible in the view: either the BackImage or FrontImage.
      /// </summary>
      Image visibleSide;
      public Image VisibleSide
      {
         get
         {
            return visibleSide;
         }
         set
         {
            if (visibleSide != value)
            {
               visibleSide = value;
               RaisePropertyChanged("VisibleSide");
            }
         }
      }

      /// <summary>
      ///   This Property is used only for debugging purposes.
      /// </summary>
      int cardID;
      public int CardID
      {
         get
         {
            return cardID;
         }
         set
         {
            if (cardID != value)
            {
               cardID = value;
               RaisePropertyChanged("CardID");
            }
         }
      }

      public Card(Color color, int id)
      {
         // Assign the id.
         CardID = id;
         // Create the back image.
         BackImage = new Image();
         BackImage.Source = new BitmapImage(new Uri(Application.Current.FindResource("BackImagePath") as string));
         // Create the front image.
         FrontImage = new Image();
         FrontImage.Source = ImageTools.CreateBitmapSource(color);
         // Set the back as the visible side .
         VisibleSide = BackImage;
      }

   }
}
