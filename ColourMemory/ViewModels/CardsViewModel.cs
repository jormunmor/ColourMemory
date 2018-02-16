using ColourMemory.Models;
using ColourMemory.Tools;
using ColourMemory.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Threading;
using static System.Console;

namespace ColourMemory.ViewModels
{
   /// <summary>
   ///  This class represents the view-model (DataContext) for the PlayView.
   /// </summary>
   public class CardsViewModel : BaseViewModel
   {
      #region PROPERTIES

      /// <summary>
      /// An array containing all the cards in the game.
      /// </summary>
      public Card[,] CardDeck { get; set; }

      /// <summary>
      /// The first card flipped by the user.
      /// </summary>
      private Card FlippedCard1 { get; set; }

      /// <summary>
      /// The second card flipped by the user.
      /// </summary>
      private Card FlippedCard2 { get; set; }

      /// <summary>
      /// The score of the user.
      /// </summary>
      private int Score { get; set; }

      /// <summary>
      /// A data structure used in bindings from the PlayView. It represents the board with all cards.
      /// </summary>
      private DataView dataView;
      public DataView DataView {
         get { return dataView; }
         set
         {
            if (dataView != value)
            {
               dataView = value;
               RaisePropertyChanged("DataView");
            }
         }
      }

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

      /// <summary>
      /// This property is modified by the CurrentCell event of the DataGrid.
      /// </summary>
      /// <remarks>
      /// When the user clicks over an image, the CurrentCell event is fired. Then, if the card is unpaired,
      /// it will be flipped.
      /// </remarks>
      private DataGridCellInfo _cellInfo;
      public DataGridCellInfo CellInfo
      {
         get { return _cellInfo; }
         set
         {
            if(_cellInfo != value)
            {
               _cellInfo = value;
               RaisePropertyChanged("CellInfo");
            }
            
            if (_cellInfo.Column == null)
            {
               return;
            }
            CheckGameStatus(GetCardFromCellInfo(_cellInfo));         
         }
      }

      #endregion

      #region CONSTRUCTORS

      /// <summary>
      /// The constructor for the CardsViewModel. It nitializes the game.
      /// </summary>      
      public CardsViewModel()
      {
         InitializeGame();
      }

      #endregion

      #region METHODS

      /// <summary>
      /// This method initializes a new game.
      /// </summary>
      /// <remarks>
      /// It generates a color list, a card deck and a DataView from the cards.
      /// </remarks>
      /// /// <seealso cref="Tools.ImageTools.GenerateRandomColorList(int)"/>
      /// <seealso cref="GenerateCardDeck()"/>
      /// <seealso cref="FillDataView()"/>
      private void InitializeGame()
      {
         // Get the size of the game (width*width).
         BoardWidth = int.Parse(ConfigurationManager.AppSettings["board_width"]);
         GameSize = BoardWidth * BoardWidth;
         // Initialize to null the flipped cards.
         FlippedCard1 = null;
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

      Card GetCardFromCellInfo(DataGridCellInfo info)
      {
         // First we recover the clicked card.
         DataGridColumn col = _cellInfo.Column;
         int columnIndex = col.DisplayIndex;
         DataRowView row = (DataRowView)_cellInfo.Item;
         Card card = row.Row.ItemArray[columnIndex] as Card;

         return card;
      }

      /// <summary>
      /// This method checks if there is one card currently face up in the game.
      /// </summary>
      /// <returns>True if and only if one card is face up. False if all cards are face down.</returns>
      bool CheckOneCardFlipped()
      {
         return (FlippedCard1 != null);
      }

      /// <summary>
      /// This method checks the game status after trying to flip a card.
      /// </summary>
      /// <param name="card"></param>
      void CheckGameStatus(Card card)
      {
         // Check card and game status.
         if(FlippedCard1 != null && FlippedCard2 != null)
         {
            return;
         }
         if (!card.IsRemoved() && card.IsFaceDown())
         {
            card.PutFaceUp();
            if (FlippedCard1 != null) // We have two cards face up. Compare them.
            {
               FlippedCard2 = card;
               Thread thread = new Thread(() => SleepAndUpdateGame());
               thread.Start();               
            }
            else // We only have one card face up (the new). Update FlippedCard1.
            {
               FlippedCard1 = card;
            }            
         }
         RefresDataView();
      }

      bool GameEnd()
      {
         foreach(Card card in CardDeck)
         {
            if(card.IsFaceDown())
            {
               return false;
            }
         }
         return true;
      }

      void CheckScoreAndFinish()
      {
         MessageBox.Show($"You finished the game with a score of: {Score}.", "Congratulations!");
         Threading.Invoke(() =>
         {
            NavigationWindow mainWindow = Application.Current.MainWindow as NavigationWindow;
            mainWindow.NavigationService.Navigate(new StartView());
         });

      }

      /// <summary>
      /// This method is executed as a thread. It has the purpose of invoking some main thread code
      /// after waiting 2 seconds.
      /// </summary>
      /// <remarks>
      /// This thread does not block the main thread with sleep, and the code executed in invoke is called
      /// within the main thread, so we don't have to manage concurrency and avoid using locks.
      /// </remarks>
      private void SleepAndUpdateGame()
      {
         Thread.Sleep(2000);
         Threading.Invoke(() =>
         {            
            if (FlippedCard2.Equals(FlippedCard1))
            {
               Score += 1;
               FlippedCard1.Remove();
               FlippedCard2.Remove();
               if(GameEnd())
               {
                  CheckScoreAndFinish();
               }
            }
            else
            {
               Score -= 1;
               FlippedCard1.PutFaceDown();
               FlippedCard2.PutFaceDown();               
            }
            FlippedCard1 = null;
            FlippedCard2 = null;
            RefresDataView();
         });         
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
            t.Columns.Add(new DataColumn(c.ToString(), typeof(Card))); // We need to tell the type of objects or by default string datatype will be used.
         }
         for (var r = 0; r < rows; r++)
         {
            var newRow = t.NewRow();
            for (var c = 0; c < columns; c++)
            {
               newRow[c] = CardDeck[r, c];
            }
            t.Rows.Add(newRow);
         }
         DataView = t.DefaultView;
      }

      /// <summary>
      /// This method is used to force updating the DataView.
      /// </summary>
      /// <remarks>
      /// As we are not using an ObservableCollection, changing an object inside de DataView does not take effect
      /// on the View. To force that, set to null and recover the original reference. That forces the View to refresh.
      /// </remarks>
      private void RefresDataView()
      {
         DataView tmpDataView = DataView;
         DataView = null;
         DataView = tmpDataView;
      }

      #endregion

      #region DEBUGGING METHODS

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

      #endregion

   }
}
