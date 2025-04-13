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
    public class UserService
    {
        private static readonly string FilePath = "Data/users.json";

        public static List<UserModel>LoadUsers()
        {
            if(!File.Exists(FilePath))
            {
                return new List<UserModel>();
            }

            var json=File.ReadAllText(FilePath);
            return JsonSerializer.Deserialize<List<UserModel>>(json);
        }

        public static void SaveUsers(List<UserModel> users)
        {
            var json=JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented=true});
            File.WriteAllText(FilePath, json);
        }
    }
}
