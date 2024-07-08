using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackjackCS
{
    public class Card
    {
        //Card attributes
        public string card { get; set; }
        public int cV { get; set; }

        public Card(string card, int cV)
        {
            this.card = card;
            this.cV = cV;
        }
    }
}
