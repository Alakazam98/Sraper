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
        


       


        private void button_Click(object sender, RoutedEventArgs e)
        {
           
            
            Button button = sender as Button;
            if (this.RandomNumber.Contains(this.grid.Children.IndexOf(button)))
            {
                Image image = new Image();
                image.Source = new BitmapImage(new Uri("images/Sraper.Tlo.jpg", UriKind.RelativeOrAbsolute));

                button.Background = Brushes.White;
                button.Content = image;
                MessageBox.Show("You stepped in poop!!! So probably it's a lost");
                grid.IsEnabled = false;
            }
            else
            {
                CheckSurrounding(button);
                
            }

            button.Background = Brushes.White;


        }

        public void CheckSurrounding(Button button)
        {
            int howManyMines = 0;
        

            if (RandomNumber.Contains(((this.grid.Children.IndexOf(button)-11))))
            {
                howManyMines++;
            }
            if (RandomNumber.Contains(((this.grid.Children.IndexOf(button) - 10))))
            {
                howManyMines++;
            }
            if (RandomNumber.Contains(((this.grid.Children.IndexOf(button) - 9))))
            {
                howManyMines++;
            }
            if (RandomNumber.Contains(((this.grid.Children.IndexOf(button) + 1))))
            {
                howManyMines++;
            }
            if (RandomNumber.Contains(((this.grid.Children.IndexOf(button) + 11))))
            {
                howManyMines++;
            }
            if (RandomNumber.Contains(((this.grid.Children.IndexOf(button) + 10))))
            {
                howManyMines++;
            }
            if (RandomNumber.Contains(((this.grid.Children.IndexOf(button) + 9))))
            {
                howManyMines++;
            }
            if (RandomNumber.Contains(((this.grid.Children.IndexOf(button) - 1))))
            {
                howManyMines++;
            }
            button.Content = howManyMines;
            
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
