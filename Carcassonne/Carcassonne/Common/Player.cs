using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carcassonne.Common
{
    class Player
    {
        //fields
        private string name;
        private int colorId;
        private bool hasAvatar;
        public int turnOrder;

        //properties
        public string Name
        {
            get { return this.name; }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("Please enter some name");
                }
                if (value.Length > 30)
                {
                    throw new ArgumentOutOfRangeException("Name should be no more than 30 symbols");
                }
                this.name = value;
            }
        }
        public int ColorID
        {
            get { return this.colorId; }
            set
            {
                if(value<0 || value > 4)
                {
                    throw new ArgumentNullException("Player color is not correct ,please choose correctly");
                }
                this.colorId = value;
            }
        }

        public bool HasAvatar
        {
            get { return this.hasAvatar; }
            set { this.hasAvatar = value; }
        }
        public int TurnOrder
        {
            get { return this.turnOrder; }
            set { this.turnOrder = value; }
        }

        //Constructor

        public Player(string name,int colorId,bool hasAvatar)
        {
            this.Name = name;
            this.ColorID = colorId;
            this.HasAvatar = hasAvatar;
        }

    }
}
