using MemoryGame2.Models;
using MemoryGame2.Services;
using MemoryGame2.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace MemoryGame2.Views
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        public GameWindow(GameStateModel gameState, int timpSetat)
        {
            InitializeComponent();
            var gameViewModel = new GameViewModel(gameState, timpSetat);
            this.DataContext = gameViewModel;
            // DataContext = new GameViewModel(config, timp);
            gameViewModel.JocFinalizat += GameViewModel_JocFinalizat;
            gameViewModel.TimpExpirat += GameViewModel_TimpExpirat;
        }

        private void ScrieStatistica(string mesaj)
        {
            string path = "Data/statistics.txt";
            string stat = $"{mesaj} - Data: {DateTime.Now}\n";
            File.AppendAllText(path, stat);
        }

        private void GameViewModel_JocFinalizat()
        {
            var vm=DataContext as GameViewModel;
            if(vm!=null)
            {
                MessageBox.Show($"User: {vm.Utilizator} win", "Congratulations!", MessageBoxButton.OK, MessageBoxImage.Information);
                ScrieStatistica($"Joc castigat de: {vm.Utilizator}");
            }
            
        }

        private void GameViewModel_TimpExpirat()
        {
            var vm = DataContext as GameViewModel;
            if(vm!=null)
            {
                MessageBox.Show("Game Over! Time's up!", "You Lost", MessageBoxButton.OK, MessageBoxImage.Warning);
                ScrieStatistica($"Joc pierdut de: {vm.Utilizator}");
            }
           
        }

        private void SaveGame_Click(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as GameViewModel;
            var gameState = new GameStateModel
            {
                Categorie = vm.Categorie,
                Nivel = vm.Nivel,
                TimpRamas = vm.TimpRamas,
                Utilizator = vm.Utilizator,
                Jetoane = vm.Jetoane.ToList()
            };

            FileManager.SalveazaJoc(gameState);
            MessageBox.Show("Jocul a fost salvat.");
        }

    }
}
