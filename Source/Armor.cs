using System;

namespace TurnBasedGame.Source
{
    public class Armor : Equipment
    {
        public Armor(string name, UserType type, int power, int durability) : base(name, type, power, durability)
        {
        }

        #region Methods

        public override bool Use(int damage)
        {
            if (damage == 0 && Durability > 0) Durability -= 1;
            else if (damage > Durability) Durability = 0;
            else Durability -= damage;

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