using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MemoryGame2.Models;

namespace MemoryGame2.Models
{
    public class GameStateModel
    {
        public List<GameTileModel> Jetoane { get; set; } = default!;
        public string Categorie { get; set; } = default!;
        public int Nivel { get; set; } = default;
        public int TimpRamas { get; set; }
        public string Utilizator { get; set; }

        private static readonly Dictionary<string, string> CategoriiPath = new()
        {
            {"animale", "Images/animale" },
            {"flori", "Images/flori" },
            {"fructe", "Images/fructe" }
        };
        public GameStateModel() { }

        public GameStateModel(string categorie, int numarPerechi)
        {
            Categorie = categorie.ToLower();
            Nivel= numarPerechi;
            TimpRamas = 60;

            if (!CategoriiPath.ContainsKey(Categorie))
                throw new AggregateException($"Categorie necunoscuta:{Categorie}");

            //string caleFolder = CategoriiPath[Categorie];
            string caleFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, CategoriiPath[Categorie]);

            if (!Directory.Exists(caleFolder))
                throw new DirectoryNotFoundException($"Folderul de imagini nu exista: {caleFolder}");

            var toateImaginile = Directory.GetFiles(caleFolder, "*.png")
                .Union(Directory.GetFiles(caleFolder, "*.jpg"))
                .ToList();

            if (toateImaginile.Count < numarPerechi)
                throw new InvalidOperationException("Nu sunt suficiente imagini pentru numarul de perechi cerut.");

            var imaginiSelectate=toateImaginile.OrderBy(x=>Guid.NewGuid())
                .Take(numarPerechi)
                .ToList();

            var jetoane = new List<GameTileModel>();
            foreach(var imagine in imaginiSelectate)
            {
                jetoane.Add(new GameTileModel { Imagine = imagine });
                jetoane.Add(new GameTileModel { Imagine = imagine });
            }

            Jetoane = jetoane.OrderBy(x => Guid.NewGuid()).ToList();
        }
    }
}
