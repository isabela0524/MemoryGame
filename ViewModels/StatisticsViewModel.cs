using MemoryGame2.Models;
using MemoryGame2.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGame2.ViewModels
{
    public class StatisticsViewModel
    {
        public ObservableCollection<UserModel> Utilizatori { get; set; }
   

        public StatisticsViewModel()
        {
            Utilizatori = new ObservableCollection<UserModel>(UserService.LoadUsers());
        }
    
    }
}
