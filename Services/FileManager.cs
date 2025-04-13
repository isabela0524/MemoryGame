using MemoryGame2.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MemoryGame2.Services
{
    public static class FileManager
    {
        private const string SaveFilePath = "Data/saved_game.json";

        public static void SalveazaJoc(GameStateModel gameState)
        {
            var json=JsonSerializer.Serialize(gameState);
            File.WriteAllText(SaveFilePath, json);
        }

        public static GameStateModel IncarcaJoc()
        {
            if (!File.Exists(SaveFilePath)) return null;
            var json=File.ReadAllText(SaveFilePath);
            return JsonSerializer.Deserialize<GameStateModel>(json);
        }
    }
}
