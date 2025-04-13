using MemoryGame2.Models;
using MemoryGame2.Resources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
//using System.Threading.Tasks;
using System.Windows.Input;
using System.Timers;
using System.IO;
using System.Windows;

namespace MemoryGame2.ViewModels
{
    public  class GameViewModel:INotifyPropertyChanged
    {
        public ObservableCollection<GameTileModel> Jetoane { get; set; } = new();
        private GameTileModel primulJeton, alDoileaJeton;
        private bool blocareClick = false;
        public ICommand ClickJetonCommand { get; set; }

        private System.Timers.Timer cronometru;
        private int timpRamas;
        public string TimpAfisat=>$"Timp ramas:{timpRamas}s";
        public string Categorie => gameState.Categorie;

        public event Action JocFinalizat;
        public event Action TimpExpirat;
        public bool JocTerminat {  get; private set; }

        public int Nivel {  get; private set; }
        public string Utilizator {  get; private set; }
        private GameStateModel gameState;
        public int TimpRamas
        {
            get { return timpRamas; }
            private set
            {
                if (timpRamas != value)
                {
                    timpRamas = value;
                    OnPropertyChanged(nameof(TimpAfisat));
                }
            }
        }

        public GameViewModel(GameStateModel config, int timpSetat)
        {
            gameState= config;
            Nivel=config.Nivel;
            Utilizator=config.Utilizator;

            foreach(var j in config.Jetoane)
                Jetoane.Add(j);

            timpRamas = timpSetat;
            ClickJetonCommand = new RelayCommand(ClickPeJeton);

            cronometru = new System.Timers.Timer(1000);
            cronometru.Elapsed += Cronometru_Elapsed;
            cronometru.Start();
            
        }

        private void Cronometru_Elapsed(object sender, ElapsedEventArgs e)
        {
            timpRamas--;
            OnPropertyChanged(nameof(TimpAfisat));
            if (timpRamas <= 0)
            {
                cronometru.Stop();
                JocTerminat = true;
                ScrieStatistica($"Joc pierdut de: {Utilizator}");
                TimpExpirat?.Invoke();
            }
        }

        private void ClickPeJeton(object param)
        {
            if(blocareClick||JocTerminat) return;
            if (param is not GameTileModel jeton || jeton.EsteDescoperit || jeton.EsteGasit) return;

            jeton.EsteDescoperit = true;
            OnPropertyChanged(nameof(Jetoane));

            if (primulJeton == null)
            {
                primulJeton = jeton;
            }
            else if (alDoileaJeton == null && jeton != primulJeton)
            {
                alDoileaJeton = jeton;
                blocareClick = true;

                if (primulJeton.Imagine == alDoileaJeton.Imagine)
                {
                    primulJeton.EsteGasit = alDoileaJeton.EsteGasit = true;
                    blocareClick = false;

                    primulJeton = null;
                    alDoileaJeton = null;

                    if(Jetoane.All(j=>j.EsteGasit))
                    {
                        cronometru.Stop();
                        JocTerminat = true;
                        ScrieStatistica($"Joc castigat de:{Utilizator}");
                        JocFinalizat?.Invoke();
                    }

                }
                else
                {
                    var dispatcherTimer=new System.Windows.Threading.DispatcherTimer();
                    dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
                    dispatcherTimer.Tick += (s, e) =>
                    {
                        dispatcherTimer.Stop();
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            primulJeton.EsteDescoperit = false;
                            alDoileaJeton.EsteDescoperit = false;
                            primulJeton = null;
                            alDoileaJeton = null;
                            blocareClick = false;
                           // OnPropertyChanged(nameof(Jetoane));

                            if (Jetoane.All(j => j.EsteGasit))
                            {
                                cronometru.Stop();
                                JocTerminat = true;
                                ScrieStatistica($"Joc castigat de:{Utilizator}");
                                JocFinalizat?.Invoke();
                            }
                        });
                    };
                    dispatcherTimer.Start();
                }
            }
        }

        private void ScrieStatistica(string mesaj)
        {
            string path = "Data/statistics.txt";
            string stat = $"{mesaj} - Data: {DateTime.Now}\n";
            File.AppendAllText(path, stat);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string name=null)=>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
