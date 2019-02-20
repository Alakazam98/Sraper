using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using GameLogic;
namespace Sraper
{
    /// <summary>
    /// Interaction logic for Game.xaml
    /// </summary>
    public partial class Game : Window
    {

        GameLog gameLogic = new GameLog();
        private int[] Board { get; set; } = new int[100];
        private int[] RandomNumber { get; set; } = new int[10];
        private int[] LeftColumn { get;  } = new int[10] { 0, 10, 20, 30, 40, 50, 60, 70, 80, 90 };
        private int[] RightColumn { get;  } = new int[10] { 9, 19, 29, 39, 49, 59, 69, 79, 89, 99 };
        private int MineFound { get; set; } = 0;
        private List<int> BlackCells { get; set; } = new List<int>();
        private int Increment { get; set; } = 0;


        /// <summary>
        /// Generuje buttony w oknie gry oraz losuje dziesięć w których bedzie bomba
        /// </summary>
        public Game()
          {
              InitializeComponent();
           

            Random rnd = new Random();

            for (int i = 0; i < 100; ++i)
              {
                  Button button = new Button()
                  {
                      Tag = i
                  };
                  button.Click += new RoutedEventHandler(button_Click);
                  button.MouseRightButtonDown += new MouseButtonEventHandler(button_Right_Click);
                  grid.Children.Add(button);
                Board[i] = grid.Children.IndexOf(button);
            }
              
            //Losuje dziesięc buttonów
            for(int i = 0;i<RandomNumber.Length;)
            {
                int number = rnd.Next(0, 100);
                if (!RandomNumber.Contains(number))
                {
                    RandomNumber[i] = number;
                    i++;
                    


                }
            }
                
            

          }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Maximized;
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timerTicker;
            timer.Start();
            
        }
        /// <summary>
        /// Timer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerTicker(object sender, EventArgs e)
        {
            Increment++;

            TimerLabelContent.Content = Increment.ToString();
        }


        /// <summary>
        /// Wygrana
        /// </summary>
        private void Win()
        {
           
           
            for (int i = 0; i < RandomNumber.Count(); i++)
            {
                Button poop = new Button();
                Image image = new Image();
                image.Source = new BitmapImage(new Uri("images/Sraper.Tlo.jpg", UriKind.RelativeOrAbsolute));
                poop.Background = Brushes.White;
                poop.Content = image;
                grid.Children.Insert(RandomNumber[i], poop);
            }
            MessageBox.Show("Wygrałeś!!! Twoje buty są czyste!" +
                   "Twój czas: " + Increment.ToString());

            grid.IsEnabled = false;

        }

        /// <summary>
        /// Otwarcie pola z bombą
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Click(object sender, RoutedEventArgs e)
        {

            Button button = sender as Button;
            int index = grid.Children.IndexOf(button);

            if (RandomNumber.Contains(grid.Children.IndexOf(button)))
            {
                Image image = new Image();
                image.Source = new BitmapImage(new Uri("images/Sraper.Tlo.jpg", UriKind.RelativeOrAbsolute));

                button.Background = Brushes.White;
                button.Content = image;
                MessageBox.Show("Wdepłeś w mine!!!");
                grid.IsEnabled = false;
            }
            else
            {
                if (gameLogic.CheckSurrounding(index, RandomNumber, LeftColumn, RightColumn) == 0)
                    button.Content = null;
                else
                button.Content = gameLogic.CheckSurrounding(index, RandomNumber,LeftColumn, RightColumn);


                ShowFreeCells(button);
            }

            button.Background = Brushes.White;


        }
       
       
        private void button_Right_Click(object sender, RoutedEventArgs e)
        {
            
            Button button = sender as Button;
            int x;
             if (button.Background == Brushes.Black)
            {
                button.Background = Brushes.LightGray;
                button.Click += button_Click;
                button.Content = null;
                x = BlackCells.IndexOf(grid.Children.IndexOf(button));
                BlackCells.RemoveAt(x);
            }
            else 
            {
                button.Background = Brushes.Black;

                button.Click -= button_Click;

                BlackCells.Add(grid.Children.IndexOf(button));
            }
            if (RandomNumber.Contains(grid.Children.IndexOf(button)) && button.Background == Brushes.Black)
            {
                MineFound++;
            }
            else if (RandomNumber.Contains(grid.Children.IndexOf(button)) && button.Background == Brushes.LightGray)
                MineFound--;

            if (MineFound == 10 && BlackCells.Count()==10)
            {
                Win();
            }

        }

        private enum SurroundingCells
        {
            TopLeft = -11,
            Top = -10,
            TopRight = -9,
            Left = -1,
            Right = 1,
            BottomLeft = 9,
            Bottom = 10,
            BottomRight = 11
        }
        
        private void ShowCell(int cellPosition, Button button)
        {
            int index = grid.Children.IndexOf(button);
            Button button1 = new Button();
            button1.Background = Brushes.White;
            button.Background = Brushes.White;

            grid.Children.RemoveAt(index + cellPosition);
            grid.Children.Insert(index + cellPosition, button1);
            if (gameLogic.CheckSurrounding(grid.Children.IndexOf(button1), RandomNumber, LeftColumn, RightColumn) == 0)
                button1.Content = null;
            else
            button1.Content = gameLogic.CheckSurrounding(grid.Children.IndexOf(button1), RandomNumber, LeftColumn, RightColumn);

        }
        /// <summary>
        /// Otwiera wolne komórki w okolicy
        /// </summary>
        /// <param name="button"></param>
        private void ShowFreeCells(Button button)
        {
            int index = grid.Children.IndexOf(button);

            if ( gameLogic.FreeCellsCondition(index, (int)SurroundingCells.TopLeft, RandomNumber, Board, BlackCells ) && !LeftColumn.Contains(index) )
            {
                ShowCell((int)SurroundingCells.TopLeft, button);

            }
            if (gameLogic.FreeCellsCondition(index, (int)SurroundingCells.Top,RandomNumber, Board, BlackCells))
            {
                ShowCell((int)SurroundingCells.Top, button);

            }
            if (gameLogic.FreeCellsCondition(index, (int)SurroundingCells.TopRight, RandomNumber, Board, BlackCells) && !RightColumn.Contains(index) )
            {
                ShowCell((int)SurroundingCells.TopRight, button);

            }
            if (gameLogic.FreeCellsCondition(index, (int)SurroundingCells.Right, RandomNumber, Board, BlackCells) && !RightColumn.Contains(index) )
            {
                ShowCell((int)SurroundingCells.Right, button);

            }
            if (gameLogic.FreeCellsCondition(index, (int)SurroundingCells.BottomRight, RandomNumber, Board, BlackCells) && !RightColumn.Contains(index) )
            {

                ShowCell((int)SurroundingCells.BottomRight, button);

            }
            if (gameLogic.FreeCellsCondition(index, (int)SurroundingCells.Bottom, RandomNumber, Board, BlackCells))
            {

                ShowCell((int)SurroundingCells.Bottom, button);

            }
            if (gameLogic.FreeCellsCondition(index, (int)SurroundingCells.BottomLeft,RandomNumber, Board, BlackCells) && !LeftColumn.Contains(index ) )
            {

                ShowCell((int)SurroundingCells.BottomLeft, button);

            }
            if (gameLogic.FreeCellsCondition(index, (int)SurroundingCells.Left, RandomNumber, Board, BlackCells) && !LeftColumn.Contains(index) )
            {

                ShowCell((int)SurroundingCells.Left, button);

            }


        }
           
        

       
        /// <summary>
        /// Powrót do menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnBackToMenu_Click(object sender, RoutedEventArgs e)
        {
            //Tworzy nowy obiekt MainWindow
            MainWindow MainWindow = new MainWindow();

            //Pokazuje okno MainWindow
            MainWindow.Show();

            //Zamyka okno gry
            this.Close();
        }

       

     

      
        /// <summary>
        /// Przycisk try again
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnTryAgaind_Click(object sender, RoutedEventArgs e)
        {
            //Tworzy nowy obiekt MainWindow
            Game Game = new Game();

            //Pokazuje okno MainWindow
            Game.Show();

            //Zamyka okno gry
            this.Close();
        }
    }
}
