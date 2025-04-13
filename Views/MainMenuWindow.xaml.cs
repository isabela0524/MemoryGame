using MemoryGame2.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MemoryGame2.Views
{
    /// <summary>
    /// Interaction logic for MainMenuWindow.xaml
    /// </summary>
    public partial class MainMenuWindow : Window
    {
        public MainMenuWindow()
        {
            InitializeComponent();
        }

        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            var configWindow = new GameConfigWindow();
            if (configWindow.ShowDialog() == true)
            {
                var config = new GameStateModel(configWindow.SelectedCategory, configWindow.SelectedPairs)
                {
                    TimpRamas = configWindow.SelectedTime,
                    Utilizator = "Jucator"
                };

                var gameWindow = new GameWindow(config, config.TimpRamas);
                gameWindow.ShowDialog();
            }
        }

        private void LoadGame_Click( object sender, RoutedEventArgs e )
        {
            if (File.Exists("Data/saved_game.json"))
            {
                string json = File.ReadAllText("Data/saved_game.json");
                var gameState = JsonSerializer.Deserialize<GameStateModel>(json);
                var gameWindow = new GameWindow(gameState, gameState.TimpRamas);
                gameWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Nu există niciun joc salvat.");
            }
        }

        private void Statistics_Click( object sender, RoutedEventArgs e )
        {
            var statsWindow = new StatisticsWindow();
            statsWindow.ShowDialog();
        }

        private void Exit_Click( object sender, RoutedEventArgs e )
        {
            Application.Current.Shutdown();
        }
    }
}
