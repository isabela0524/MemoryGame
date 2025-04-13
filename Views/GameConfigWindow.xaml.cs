using MemoryGame2.Models;
using System;
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
using System.Windows.Shapes;

namespace MemoryGame2.Views
{
    /// <summary>
    /// Interaction logic for GameConfigWindow.xaml
    /// </summary>
    public partial class GameConfigWindow : Window
    {
        public int SelectedPairs {  get; private set; }
        public int SelectedTime {  get; private set; }
        public string SelectedCategory {  get; private set; }
        public GameConfigWindow()
        {
            InitializeComponent();
        }

        private void StartGame_Click(object sender, RoutedEventArgs e)
        {

            if (pairComoBox.SelectedItem is ComboBoxItem pairItem &&
                timeComboBox.SelectedItem is ComboBoxItem timeItem &&
                categoryComboBox.SelectedItem is ComboBoxItem categoryItem)
            {
                SelectedPairs = int.Parse(pairItem.Content.ToString());
                SelectedTime = int.Parse(timeItem.Content.ToString());
                SelectedCategory = categoryItem.Content.ToString();

                var gameState=new GameStateModel(SelectedCategory, SelectedPairs);
                var gameWindow = new GameWindow(gameState, SelectedTime);

                gameWindow.Show();

                //DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Completează toate selecțiile.", "Atenție", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
