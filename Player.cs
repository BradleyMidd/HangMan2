using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangMan2
{
    internal class Player
    {

        string name;
        public string Name
        {
            get { return name; }
            private set
            {
                if (value != "")
                    name = value;
                else
                    name = $"Player{rnd.Next()}";
            }
        }

        int score;
        public int Score 
        {
            get { return score; } 
            set 
            {
                if (value > 0)
                    score = value;
            }
        }

        Random rnd = new Random();

        public bool CorrectGuess { get; set; } = false;

        public List<char> Guessed { get; } = new List<char>();
        public int guesses;
        

        public int Life { get; set; } = 3;

        public Player(string name) 
        {
            this.Name = name;
        }



    }
}
