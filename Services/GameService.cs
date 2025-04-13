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
    public static class GameService
    {
        private static readonly string GameSavePath = "Data/saved_game.json";
        private static GameStateModel jocCurent;

        public static void SaveGame(GameStateModel game)
        {
            jocCurent = game;
            var json = JsonSerializer.Serialize(game, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(GameSavePath, json);
        }

        public static void SaveCurrentGame()
        {
            if(jocCurent!=null)
            {
                SaveGame(jocCurent);
            }
        }

        public static GameStateModel LoadGame()
        {
            if (!File.Exists(GameSavePath))
                return null;

            var json=File.ReadAllText(GameSavePath);
            jocCurent = JsonSerializer.Deserialize<GameStateModel>(json);
            return jocCurent;
        }
    }
    
}
