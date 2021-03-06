﻿using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sraper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Otwieranie głównego okna
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnNewGame_Click(object sender, RoutedEventArgs e)
        {
            //tworzy obiekt game
            Game Game = new Game();

            //otwiera okno gry
            Game.Show();

            //zamyka MainWindow
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Maximized;

        }

        private void BtnInstruction(object sender, RoutedEventArgs e)
        {
            Instruction instruction = new Instruction();

            instruction.Show();

            this.Close();

        }
    }
}
