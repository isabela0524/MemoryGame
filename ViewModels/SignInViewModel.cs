using MemoryGame2.Models;
using MemoryGame2.Services;
using MemoryGame2.Resources;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MemoryGame2.Views;

namespace MemoryGame2.ViewModels
{
    public class SignInViewModel : INotifyPropertyChanged
    {
        private string nume;
        private string imagineProfil;
        private Window window;

        public string Nume
        {
            get => nume;
            set { nume = value; OnPropertyChanged(); }
        }

        public string ImagineProfil
        {
            get => imagineProfil;
            set { imagineProfil = value; OnPropertyChanged(); }
        }

        public ICommand SelecteazaImagineCommand { get; set; }
        public ICommand ContinuuCommand { get; set; }

        private MainMenuViewModel mainVM;

        public SignInViewModel(MainMenuViewModel mainVm)
        {
            this.mainVM = mainVM;
            SelecteazaImagineCommand = new RelayCommand(o => AlegeImagine());
            ContinuuCommand = new RelayCommand(o => MergiMaiDeparte());
        }

        private void AlegeImagine()
        {
            OpenFileDialog dlg=new OpenFileDialog();
            dlg.Filter = "Imagini|*.jpg;*.png";
            if(dlg.ShowDialog()==true)
            {
                ImagineProfil=dlg.FileName;
            }
        }

        private void MergiMaiDeparte()
        {
            if(string.IsNullOrWhiteSpace(Nume))
            {
                MessageBox.Show("Completeaza campul!");
                return;
            }

            UserModel utilizator = new UserModel
            {
                Nume = Nume,
                ImagineProfil = ImagineProfil
            };

            UserService.SaveUsers(new List<UserModel> { utilizator });

            var meniu = new MainMenuWindow();
            meniu.Show();
            window.Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string prop = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));



    }
}
