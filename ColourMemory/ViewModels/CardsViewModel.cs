using ColourMemory.Models;
using ColourMemory.Tools;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using static System.Console;

namespace ColourMemory.ViewModels
{
   /// <summary>
   ///  This class represents the view-model (DataContext) for the PlayView.
   /// </summary>
   class CardsViewModel : BaseViewModel
   {
      /// <summary>
      /// An array containing all the cards in the game.
      /// </summary>
      private Card[,] CardDeck { get; set; }

      /// <summary>
      /// A data structure used in bindings from the PlayView. It represents the board with all cards.
      /// </summary>
      public DataView DataView { get; set; }

      /// <summary>
      /// A list of the used colors, generated randomnly.
      /// </summary>      
      private List<Color> colorList;

      /// <summary>
      /// The size of a row (its number of cards).
      /// </summary>
      private int BoardWidth { get; set; }

      /// <summary>
      /// The size of the card board (always a squared board with dimensions BoardWidth^2).
      /// </summary>
      private int GameSize { get; set; }

      private DataGridCellInfo _cellInfo;
      public DataGridCellInfo CellInfo
      {
         get { return _cellInfo; }
         set
         {
            _cellInfo = value;
            DataGridColumn col = _cellInfo.Column;
            int columnIndex = col.DisplayIndex;
            DataRowView row = (DataRowView) _cellInfo.Item;
            object val = row.Row.ItemArray[columnIndex];
            Console.WriteLine("val: " + val);
         }
      }

      /// <summary>
      /// The constructor for the CardsViewModel.
      /// </summary>
      /// <remarks>
      /// It generates a color list, a card deck and a DataView from the cards.
      /// </remarks>
      /// <seealso cref="Tools.ImageTools.GenerateRandomColorList(int)"/>
      /// <seealso cref="GenerateCardDeck()"/>
      /// <seealso cref="FillDataView()"/>
      public CardsViewModel()
      {
         // Get the size of the game (width*width).
         BoardWidth = int.Parse(ConfigurationManager.AppSettings["board_width"]);
         GameSize = BoardWidth * BoardWidth;
         // Set the color list, filled with random colors (each repeated twice).
         colorList = ImageTools.GenerateRandomColorList(GameSize);
         // Create the card deck.
         GenerateCardDeck();
         // Fill DataView.
         FillDataView();
      }

      /// <summary>
      /// This method generates all the cards for the game.
      /// </summary>      
      private void GenerateCardDeck()
      {
         int id = 0;
         Random random = new Random();
         CardDeck = new Card[BoardWidth, BoardWidth];
         for (int i = 0; i < BoardWidth; i++)
         {
            for (int j = 0; j < BoardWidth; j++)
            {
               int randomIndex = random.Next(colorList.Count);
               Color color = colorList.ElementAt(randomIndex);
               colorList.RemoveAt(randomIndex);
               CardDeck[i, j] = new Card(color, id);
               id++;
            }
         }
      }

      /// <summary>
      /// This method fills the DataView that is used as binding in the PlayGame view.
      /// </summary>
      /// <remarks>
      /// We use here a DataTable object to generate first the data structure as we want (a squared grid of Cards).
      /// </remarks>
      private void FillDataView()
      {
         var t = new DataTable();
         var columns = BoardWidth;
         var rows = BoardWidth;
         for (var c = 0; c < columns; c++)
         {
            t.Columns.Add(new DataColumn(c.ToString()));
         }
         for (var r = 0; r < rows; r++)
         {
            var newRow = t.NewRow();
            for (var c = 0; c < columns; c++)
            {
               newRow[c] = CardDeck[r, c].CardID;
            }
            t.Rows.Add(newRow);
         }
         DataView = t.DefaultView;
      }

      /// <summary>
      /// This method prints in console the IDs of the cards in the board, in a matrix fashion (row by row).
      /// </summary>
      /// <remarks>
      /// It's only used for debugging purposed.
      /// </remarks>
      public void PrintCardDeck()
      {
         for (int i = 0; i < BoardWidth; i++)
         {
            for (int j = 0; j < BoardWidth; j++)
            {
               Write(CardDeck[i, j].CardID + "\t");
            }
            WriteLine();
         }
      }
   }
}
