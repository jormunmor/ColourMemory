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
   /// It contains a card color, an image for the back side, an image for the front side, an image when it's removed,
   /// the side that is currently visible by the user, and an identifier for debugging purposes.
   /// </remarks>
   public class Card : BaseModel
   {

      #region PROPERTIES

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
      /// The image used as transparent background.
      /// </summary>
      /// <remarks>
      /// Will be displayed when the card is paired with another of the same color.
      /// </remarks>
      Image transparentImage;
      public Image TransparentImage
      {
         get
         {
            return transparentImage;
         }
         set
         {
            if (transparentImage != value)
            {
               transparentImage = value;
               RaisePropertyChanged("TransparentImage");
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

      #endregion

      #region CONSTRUCTORS

      /// <summary>
      /// The constructor for the Card.
      /// </summary>
      /// <param name="color">The color of the card.</param>
      /// <param name="id">A unique identifier.</param>
      /// <seealso cref="InitializeCard(Color, int)"/>
      public Card(Color color, int id)
      {
         InitializeCard(color, id);
      }

      #endregion

      #region METHODS

      /// <summary>
      /// Helper method used when creating a new card.
      /// </summary>
      /// <param name="color">The color of the card.</param>
      /// <param name="id">A unique identifier.</param>
      private void InitializeCard(Color color, int id)
      {
         // Assign the id.
         CardID = id;
         // Assign the color.
         CardColor = color;
         // Create the back image.
         BackImage = new Image();
         BackImage.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/back_side.png"));
         // Create the front image.
         FrontImage = new Image();
         FrontImage.Source = ImageTools.CreateBitmapSource(color);
         FrontImage.Width = BackImage.Width;
         FrontImage.Height = BackImage.Height;
         // Create the transparent image.
         TransparentImage = new Image();
         TransparentImage.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/transparent_side.png"));
         // Set the back as the visible side .
         VisibleSide = new Image();
         VisibleSide.Source = BackImage.Source;
      }

      /// <summary>
      /// This method checks the state of the card (face down or up).
      /// </summary>
      /// <returns>True if the card is face down. False otherwhise.</returns>
      public bool IsFaceDown()
      {
         return (VisibleSide.Source == BackImage.Source);
      }

      /// <summary>
      /// This method puts a card face up.
      /// </summary>
      public void PutFaceUp()
      {
         VisibleSide.Source = FrontImage.Source;
      }

      /// <summary>
      /// This method puts a card face down.
      /// </summary>
      public void PutFaceDown()
      {
         VisibleSide.Source = BackImage.Source;
      }

      /// <summary>
      /// This methods removes a card from the game, by putting it in a transparent background.
      /// </summary>
      public void Remove()
      {
         VisibleSide.Source = TransparentImage.Source;
      }

      /// <summary>
      /// This method checks if a card is in a removed state.
      /// </summary>
      /// <remarks>
      /// A card is considered to be removed if the visible side is set to transparent, in which case the background
      /// of the containing element will be shown.
      /// </remarks>
      /// <returns>True if the car is not present in the board. False otherwise.</returns>
      public bool IsRemoved()
      {
         return VisibleSide.Source == TransparentImage.Source;
      }

      /// <summary>
      /// Override of Equals to compare to card objects equality.
      /// </summary>
      /// <param name="obj"></param>
      /// <returns>True if the cards have the same color. False otherwise.</returns>
      public override bool Equals(object obj)
      {
         var card = obj as Card;

         if (card == null)
         {
            return false;
         }

         return this.CardColor.Equals(card.CardColor);
      }

      /// <summary>
      /// Override of the GetHashCode method.
      /// </summary>
      /// <returns>The hashcode of the card color.</returns>
      /// <remarks>
      /// We don't really need this, but doing that avoids a warning.
      /// </remarks>
      public override int GetHashCode()
      {
         return this.CardColor.GetHashCode();
      }

      #endregion

   }
}
