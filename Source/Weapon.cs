using System;

namespace TurnBasedGame.Source
{
    public class Weapon : Equipment
    {
        public Weapon(string name, UserType type, int power, int durability) : base(name, type, power, durability)
        {
        }

        #region Methods

        public override bool Use(int damage)
        {
            if (Durability > 0) Durability -= 1;
            else Durability = 0;

            //Weapon must be destroyed or unassigned
            if (Durability == 0) return true;

            //Weapon can be used again
            return false;
        }

        public override void ChangeName(string name)
        {
            if (!instantiated)
            {
                Name = name;
            }
        }

        #endregion
    }
}