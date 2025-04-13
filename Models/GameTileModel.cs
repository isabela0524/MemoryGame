using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGame2.Models
{
    public class GameTileModel:INotifyPropertyChanged
    {
        private bool esteDescoperit;
        private bool esteGasit;
        private string imagine;

        public string Imagine 
        {  get => imagine;
            set { 
                imagine = value;
                OnPropertyChanged();
            } 
        }

        public bool EsteDescoperit
        {
            get { return esteDescoperit; }
            set
            {
                if (esteDescoperit != value)
                {
                    esteDescoperit = value;
                    OnPropertyChanged(); 
                }
            }
        }

        public bool EsteGasit
        {
            get => esteGasit;
            set
            {
                if (esteGasit != value)
                {
                    esteGasit = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string nume=null)=>
            PropertyChanged?.Invoke( this, new PropertyChangedEventArgs( nume ) );
    }
}
