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
    /// Interaction logic for StatisticsWindow.xaml
    /// </summary>
    public partial class StatisticsWindow : Window
    {
        private readonly string statisticsPath = "Data/statistics.txt";
        public StatisticsWindow()
        {
            InitializeComponent();
            LoadStatistics();
        }

        private void LoadStatistics()
        {
            List<string> stats = new();
            var utilizatorStats = new Dictionary<string, (int Castiguri, int Pierderi)>();

            if (File.Exists(statisticsPath))
            {
                stats.AddRange(File.ReadAllLines(statisticsPath));

                foreach (var line in stats)
                {
                    if (line.Contains("Joc câștigat de:") || line.Contains("Joc pierdut de:"))
                    {
                        var parts = line.Split(':')[1].Split('-')[0].Trim();
                        var user = parts;

                        if (!utilizatorStats.ContainsKey(user))
                            utilizatorStats[user] = (0, 0);

                        if (line.Contains("câștigat"))
                            utilizatorStats[user] = (utilizatorStats[user].Castiguri + 1, utilizatorStats[user].Pierderi);
                        else
                            utilizatorStats[user] = (utilizatorStats[user].Castiguri, utilizatorStats[user].Pierderi + 1);
                    }
                }
            }
            else
            {
                stats.Add("No statistics available yet.");
            }

            var afisaj = utilizatorStats.Select(kvp => $"{kvp.Key}: {kvp.Value.Castiguri} câștiguri / {kvp.Value.Pierderi} pierderi").ToList();
            StatsList.ItemsSource = afisaj;
        }
    

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
