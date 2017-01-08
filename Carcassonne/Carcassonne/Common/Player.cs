using Carcassonne.GameObjects;
using Carcassonne.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carcassonne.Common
{
    class Player : IPlayer
    {
        //fields
        private string name;
        public int score;
        private int colorId;
        private bool hasAvatar;
        public int turnOrder;
        private int pawnCount;
        private List<Pawn> pawns;

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

        public int PawnCount
        {
            get { return this.pawnCount; }
            set { this.pawnCount = value; }
        }

        public int Score
        {
            get { return this.score; }
            set { this.score = value; }
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

        public List<Pawn> Pawns
        {
            get { return this.pawns; }
            set { this.pawns = value; }
        }

        //Constructor

        public Player(string name,int colorId,bool hasAvatar)
        {
            this.Name = name;
            this.ColorID = colorId;
            this.HasAvatar = hasAvatar;
        }

        //Methods
        public void AddPawn(Pawn pawn)
        {
            this.PawnCount++;
            pawns.Add(pawn);
        }

        public void RemovePawn(Pawn pawn)
        {
            this.pawnCount--;
            pawns.Remove(pawn);

        }
    }
}
