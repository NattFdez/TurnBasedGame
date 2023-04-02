using System;

namespace TurnBasedGame.Source
{
    public abstract class Equipment : IEquipment
    {
        public enum UserType
        {
            Human, Beast, Hybrid, Any
        }
        
        public bool instantiated { get; protected set; }
        
        private string name;
        private UserType type;
        private int power, durability;
        
        #region Access Modifiers
        
        public string Name
        {
            get => name;
            protected set
            {
                if (!string.IsNullOrEmpty(value) && !string.IsNullOrWhiteSpace(value))
                {
                    name = value;
                }
                else
                {
                    throw new Exception("Equipment's Name can't be assigned. There's no value to assign.");
                }
            }
        }
        
        public UserType Type
        {
            get => type;
        }
        
        public int Power
        {
            get => power;
            protected set
            {
                if (power == 0 && value < 1)
                {
                    throw new Exception("Equipment's Power value can't be initialize with a value below 1.");
                }
                
                if (value <= 0)
                {
                    power = 0;
                }
                else if (value <= 50)
                {
                    power = value;
                }
                else
                {
                    throw new Exception("Equipment's Power value can't be assigned. Out of Power range limits.");
                }
            }
        }
        
        public int Durability
        {
            get => durability;
            protected set
            {
                if (durability == 0 && value < 1)
                {
                    throw new Exception("Equipment's Durability value can't be initialize with a value below 1.");
                }
                
                if (value <= 0)
                {
                    durability = 0;
                }
                else if (value <= 100)
                {
                    durability = value;
                }
                else
                {
                    throw new Exception("Equipment's Durability value can't be assigned. Out of Durability range limits.");
                }
            }
        }
        
        #endregion
        
        public Equipment(string name, UserType type, int power, int durability)
        {
            Name = name;
            this.type = type;
            Power = power;
            Durability = durability;
            instantiated = true;
        }

        #region Methods
        
        public abstract bool Use(int damage);

        #endregion

        #region TestMethod

        public abstract void ChangeName(string name);

        #endregion
    }
}