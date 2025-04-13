using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGame2.Models
{
    public class UserModel
    { 
        public string Nume {  get; set; }
        public string Email {  get; set; }
        public string ImagineProfil {  get; set; }
        public int JocuriCastigate {  get; set; }
        public int JocuriPierdute {  get; set; }

    }
}
