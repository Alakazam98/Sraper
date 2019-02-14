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
namespace Sraper
{
    /// <summary>
    /// Interaction logic for Game.xaml
    /// </summary>
    public partial class Game : Window
    {


        int[] Board = new int[100];
        int[] RandomNumber = new int[10];
        int[] LeftColumn = new int[10] { 0, 10, 20, 30, 40, 50, 60, 70, 80, 90 };
        int[] RightColumn = new int[10] { 9, 19, 29, 39, 49, 59, 69, 79, 89, 99 };
        int mineFound = 0;
        List<int> BlackCells = new List<int>();

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
                  this.grid.Children.Add(button);
                Board[i]=this.grid.Children.IndexOf(button);
            }
              
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
        
        public void Win()
        {
            
            
                MessageBox.Show("You' ve WON!" +
                    "Your Time: "+ increment.ToString());

            
        }

       


        private void button_Click(object sender, RoutedEventArgs e)
        {

            
            Button button = sender as Button;
            if (this.RandomNumber.Contains(this.grid.Children.IndexOf(button)))
            {
                Image image = new Image();
                image.Source = new BitmapImage(new Uri("images/Sraper.Tlo.jpg", UriKind.RelativeOrAbsolute));

                button.Background = Brushes.White;
                button.Content = image;
                MessageBox.Show("You stepped in a poop!");
                grid.IsEnabled = false;
            }
            else
            {
                CheckSurrounding(button);
                CheckForFreeCells(button);
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
                mineFound++;
            }
            else if (RandomNumber.Contains(grid.Children.IndexOf(button)) && button.Background == Brushes.LightGray)
                mineFound--;

            if (mineFound == 10)
            {
                Win();
            }

        }

        public void CheckSurrounding(Button button)
        {
            int howManyMines = 0;
            int index = grid.Children.IndexOf(button);

            if (RandomNumber.Contains(this.grid.Children.IndexOf(button)-11) && !LeftColumn.Contains(index))
            {
                howManyMines++;
            }
            if (RandomNumber.Contains(((this.grid.Children.IndexOf(button) - 10))))
            {
                howManyMines++;
            }
            if (RandomNumber.Contains(this.grid.Children.IndexOf(button) - 9) && !RightColumn.Contains(index))
            {
                howManyMines++;
            }
            if (RandomNumber.Contains(this.grid.Children.IndexOf(button) + 1) && !RightColumn.Contains(index))
            {
                howManyMines++;
            }
            if (RandomNumber.Contains(this.grid.Children.IndexOf(button) + 11) && !RightColumn.Contains(index))
            {
                howManyMines++;
            }
            if (RandomNumber.Contains(((this.grid.Children.IndexOf(button) + 10))))
            {
                howManyMines++;
            }
            if (RandomNumber.Contains(this.grid.Children.IndexOf(button) + 9) && !LeftColumn.Contains(index))
            {
                howManyMines++;
            }
            if (RandomNumber.Contains(this.grid.Children.IndexOf(button) - 1) && !LeftColumn.Contains(index))
            {
                howManyMines++;
            }
            if(howManyMines==0)
            {
                button.Content = null;
            }
            else
            button.Content = howManyMines;
            
        }
        

        public void CheckForFreeCells(Button button)
        {
            int freeCells = 0;
            int index = grid.Children.IndexOf(button);
            if (!RandomNumber.Contains(index - 11) && Board.Contains(index -11 ) && !LeftColumn.Contains(index) && !BlackCells.Contains(index - 11))
            {
                Button button1 = new Button();
                button1.Background = Brushes.White;
                button.Background = Brushes.White;

                grid.Children.RemoveAt(index - 11);
                    grid.Children.Insert(index - 11, button1);
                CheckSurrounding(button1);


            }
            if (!RandomNumber.Contains(index - 10) && Board.Contains(index - 10) && !BlackCells.Contains(index - 10))
            {
                Button button1 = new Button();
                button1.Background = Brushes.White;

                grid.Children.RemoveAt(index - 10);

                grid.Children.Insert(index - 10, button1);
                CheckSurrounding(button1);


            }
            if (!RandomNumber.Contains(index - 9)&&Board.Contains(index - 9) && !RightColumn.Contains(index) && !BlackCells.Contains(index - 9))
            {
                Button button1 = new Button();
                button1.Background = Brushes.White;

                grid.Children.RemoveAt(index - 9);

                grid.Children.Insert(index - 9, button1);
                CheckSurrounding(button1);


            }
            if (!RandomNumber.Contains(index + 1)&& Board.Contains(index + 1) &&  !RightColumn.Contains(index) && !BlackCells.Contains(index + 1))
            {
                Button button1 = new Button();
                button1.Background = Brushes.White;
                grid.Children.RemoveAt(index + 1);

                grid.Children.Insert(index + 1, button1);
                CheckSurrounding(button1);


            }
            if (!RandomNumber.Contains(index + 11)&&Board.Contains(index + 11) && !RightColumn.Contains(index) && !BlackCells.Contains(index + 11))
            {
                Button button1 = new Button();
                button1.Background = Brushes.White;
                grid.Children.RemoveAt(index + 11);

                grid.Children.Insert(index + 11, button1);
                CheckSurrounding(button1);


            }
            if (!RandomNumber.Contains(index + 10)&& Board.Contains(index + 10) && !BlackCells.Contains(index + 10))
            {
                Button button1 = new Button();
                button1.Background = Brushes.White;
                grid.Children.RemoveAt(index + 10);

                grid.Children.Insert(index + 10, button1);
                CheckSurrounding(button1);


            }
            if (!RandomNumber.Contains( index + 9) && Board.Contains(index + 9) && !LeftColumn.Contains(index ) && !BlackCells.Contains(index + 9))
            {
                Button button1 = new Button();
                button1.Background = Brushes.White;
                grid.Children.RemoveAt(index  +9);

                grid.Children.Insert(index +9, button1);
                CheckSurrounding(button1);


            }
            if (!RandomNumber.Contains(index - 1) && Board.Contains(index - 1) && !LeftColumn.Contains(index) && !BlackCells.Contains(index - 1))
            {
                Button button1 = new Button();
                button1.Background = Brushes.White;
                grid.Children.RemoveAt(index - 1);

                grid.Children.Insert(index - 1, button1);
                CheckSurrounding(button1);


            }
            if (freeCells == 0)
            {
                
            }

        }
           
        






        private void BtnBackToMenu_Click(object sender, RoutedEventArgs e)
        {
            //Tworzy nowy obiekt MainWindow
            MainWindow MainWindow = new MainWindow();

            //Pokazuje okno MainWindow
            MainWindow.Show();

            //Zamyka okno gry
            this.Close();
        }

       

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Maximized;
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timerTicker;
            timer.Start();
            
        }

         private int increment = 0;

        private void timerTicker(object sender, EventArgs e)
        {
            increment++;

            TimerLabelContent.Content = increment.ToString();
        }

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
