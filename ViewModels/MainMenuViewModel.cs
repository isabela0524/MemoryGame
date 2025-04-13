using MemoryGame2.Services;
using MemoryGame2.Resources;
using MemoryGame2.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MemoryGame2.ViewModels
{
    public class MainMenuViewModel
    {
        public ICommand NewGameCommand { get; set; }
        public ICommand OpenGameCommand { get; set; }
        public ICommand SaveGameCommand { get; set; }
        public ICommand StatisticsCommand { get; set; }
        public ICommand ExitCommand { get; set; }
        public ICommand AboutCommand { get; set; }

        public MainMenuViewModel() 
        {
            NewGameCommand = new RelayCommand(o => startNewGame());
            OpenGameCommand=new RelayCommand(o => openGame());
            SaveGameCommand=new RelayCommand(o=>  saveGame());
            StatisticsCommand = new RelayCommand(o => ShowStats());
            ExitCommand = new RelayCommand(o => CloseApp());
            AboutCommand = new RelayCommand(o => ShowAbout());
        }

        private void startNewGame()
        {
            var configWindow = new GameConfigWindow();
            configWindow.ShowDialog();

        }

        private void openGame()
        {
            var gameState = GameService.LoadGame();
            if (gameState != null)
            {
                var gameWindow = new GameWindow(gameState,60);
                gameWindow.Show();
            }
            else
            {
                MessageBox.Show("Nu exista niciun joc salvat.", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void saveGame()
        {
            GameService.SaveCurrentGame();
            MessageBox.Show("Jocul a fost salvat cu succes.", "Salvare", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ShowStats()
        {
            var statsWindow = new StatisticsWindow();
            statsWindow.ShowDialog();

        }

        private void CloseApp()
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void ShowAbout()
        {
            System.Windows.MessageBox.Show("Nume:Ciutacu Elena-Isabela\nEmail: elena.ciutacu@student.unitbv.ro\nGrupa:10LF331\nSpecializarea:Informatica aplicata");

        }

    }
}
