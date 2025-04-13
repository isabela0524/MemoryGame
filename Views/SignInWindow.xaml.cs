using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
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
using Microsoft.VisualBasic;

namespace MemoryGame2.Views
{
    /// <summary>
    /// Interaction logic for SignInWindow.xaml
    /// </summary>
    public partial class SignInWindow : Window
    {
        private ObservableCollection<string> Users;
        private readonly string usersFile = "Data/users.json";
        private List<string> imagePaths = new()
        {
            "Images/profile2.jpg",
            "Images/profile3.jpg",
            "Images/profile4.jpg",
            "Images/profile5.jpg"
        };

        private int currentImageIndex = 0;

        public SignInWindow()
        {
            Users= new ObservableCollection<string>();
            InitializeComponent();
            LoadUsersFromFile();

            UserList.ItemsSource = Users;
            UpdateImage();
        }

        private void LoadUsersFromFile()
        {
            try
            {
                if (File.Exists(usersFile))
                {
                    var fromFile = JsonSerializer.Deserialize<List<string>>(File.ReadAllText(usersFile));

                    Users.Clear(); 
                    if (fromFile != null)
                    {
                        foreach (var user in fromFile)
                        {
                            Users.Add(user);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la încărcarea utilizatorilor: " + ex.Message);
            }
        }
        private void UpdateImage()
        {
            try
            {
                string path = $"/Images/profile{currentImageIndex + 1}.jpg";
                Uri uri = new Uri($"pack://application:,,,{path}", UriKind.Absolute);
                UserImage.Source = new BitmapImage(uri);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Nu s-a putut încărca imaginea: " + ex.Message);
            }
        }

        private void SaveUsersToFile()
        {
            try
            {
                Directory.CreateDirectory("Data");
                File.WriteAllText(usersFile, JsonSerializer.Serialize(Users));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la salvarea utilizatorilor: " + ex.Message);
            }
        }

        private void PreviousImage_Click(object sender, RoutedEventArgs e)
        {
            currentImageIndex = (currentImageIndex - 1 + imagePaths.Count) % imagePaths.Count;
            UpdateImage();
        }

        private void NextImage_Click(object sender, RoutedEventArgs e)
        {
            currentImageIndex=(currentImageIndex+1) % imagePaths.Count;
            UpdateImage();
        }

        private void NewUser_Click(object sender, RoutedEventArgs e)
        {
            string newUserName = NewUserNameTextBox.Text?.Trim();

            if (!string.IsNullOrEmpty(newUserName))
            {
                if (!Users.Contains(newUserName))
                {
                    Users.Add(newUserName);
                    SaveUsersToFile();
                    MessageBox.Show($"Utilizatorul '{newUserName}' a fost adăugat cu succes!");
                }
                else
                {
                    MessageBox.Show("Acest utilizator există deja.");
                }
            }
            else
            {
                MessageBox.Show("Te rugăm să introduci un nume valid pentru utilizator.");
            }
        }

        private void DeleteUser_Click(Object sender, RoutedEventArgs e)
        {
            if (UserList.SelectedItem is string selectedUser)
            {
                var result = MessageBox.Show($"Sigur vrei să ștergi utilizatorul '{selectedUser}'?",
                                             "Confirmare",
                                             MessageBoxButton.YesNo,
                                             MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    Users.Remove(selectedUser);
                    SaveUsersToFile();

                    UserList.ItemsSource = null;
                    UserList.ItemsSource = Users;
                }
            }
            else
            {
                MessageBox.Show("Selectează un utilizator de șters.", "Eroare", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            if(UserList.SelectedItem is string selectedUSer)
            {
                App.Current.Properties["Utilizitaor"]=selectedUSer;

                var mainMenu = new MainMenuWindow();
                mainMenu.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Te rugăm să selectezi un utilizator.", "Eroare", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
