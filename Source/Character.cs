using System;

namespace TurnBasedGame.Source
{
    public class Character
    {
        public enum CharType
        {
            Human, Beast, Hybrid
        }
        
        private string name;
        private CharType type;
        private int hp, baseDefense, baseAttack;
        private Weapon weapon;
        private Armor armor;
        
        #region Access Modifiers
        
        public string Name
        {
            get => name;
            set
            {
                if (!string.IsNullOrEmpty(value) && !string.IsNullOrWhiteSpace(value))
                {
                    name = value;
                }
                else
                {
                    throw new Exception("Character's Name can't be assigned. There's no value to assign.");
                }
            }
        }

        public CharType Type
        {
            get => type;
        }
        
        public int Hp
        {
            get => hp;
            set
            {
                if (hp == 0 && value < 1)
                {
                    throw new Exception("Character's HP value can't be initialize with a value below 1.");
                }
                
                if (value <= 0)
                {
                    hp = 0;
                }
                else if (value <= 100)
                {
                    hp = value;
                }
                else
                {
                    throw new Exception("Character's HP value can't be assigned. Out of HP range limits.");
                }
            }
        }
        
        public int BaseDefense
        {
            get => baseDefense;
            set
            {
                if (baseDefense == 0 && value < 1)
                {
                    throw new Exception("Character's Base Defense value can't be initialize with a value below 1.");
                }
                
                if (value <= 0)
                {
                    baseDefense = 0;
                }
                else if (value <= 100)
                {
                    baseDefense = value;
                }
                else
                {
                    throw new Exception("Character's Base Defense value can't be assigned. Out of Defense range limits.");
                }
            }
        }
        
        public int BaseAttack
        {
            get => baseAttack;
            set
            {
                if (baseAttack == 0 && value < 1)
                {
                    throw new Exception("Character's Base Attack value can't be initialize with a value below 1.");
                }
                
                if (value <= 0)
                {
                    baseAttack = 0;
                }
                else if (value <= 100)
                {
                    baseAttack = value;
                }
                else
                {
                    throw new Exception("Character's Base Attack value can't be assigned. Out of Attack range limits.");
                }
            }
        }

        public Weapon Weapon
        {
            get
            {
                if (weapon != null)
                    return weapon;
                else throw new Exception("Character has no weapon assigned to be retrieved.");
            }
        }

        public Armor Armor
        {
            get
            {
                if (armor != null)
                    return armor;
                else throw new Exception("Character has no armor assigned to be retrieved.");
            }
        }
        
        #endregion

        public Character(string name, CharType type, int hp, int baseDefense, int baseAttack)
        {
            Name = name;
            this.type = type;
            Hp = hp;
            BaseDefense = baseDefense;
            BaseAttack = baseAttack;
            weapon = null;
            weapon = null;
        }
        
        public Character(string name, CharType type, int hp, int baseDefense, int baseAttack, Weapon weapon, Armor armor)
        {
            Name = name;
            this.type = type;
            Hp = hp;
            BaseDefense = baseDefense;
            BaseAttack = baseAttack;
            EquipWeapon(weapon);
            EquipArmor(armor);
        }

        #region Methods

        public bool EquipWeapon(Weapon weaponToAssign)
        {
            if (hp > 0)
            {
                if (weaponToAssign != null)
                {
                    if (weaponToAssign.Type.ToString() != type.ToString() && weaponToAssign.Type != Equipment.UserType.Any)
                    {
                        return false;
                    }
                    
                    if(weaponToAssign.Durability == 0) return false;
                }

                /*if (weapon == null)
                {
                    //New weapon assigned successfully!
                }
                else
                {
                    //Weapon changed successfully!
                }*/
                
                weapon = weaponToAssign;
                return true;
            }
            else
            {
                //Can't assign a new weapon to a dead character!
                
                return false;
            }
        }
        
        public bool EquipArmor(Armor armorToAssign)
        {
            if (hp > 0)
            {
                if (armorToAssign != null)
                {
                    if (armorToAssign.Type.ToString() != type.ToString() && armorToAssign.Type != Equipment.UserType.Any)
                    {
                        return false;
                    }
                    
                    if(armorToAssign.Durability == 0) return false;
                }
                
                /*if (armor == null)
                {
                    //New armor assigned successfully!
                }
                else
                {
                    //Armor changed successfully!
                }*/
                
                armor = armorToAssign;
                return true;
            }
            else
            {
                //Can't assign a new armor to a dead character!
                
                return false;
            }
        }

        public int Attack()
        {
            if (weapon != null)
            {
                //Add weapon power to character's base attack power
                byte result = (byte)(baseAttack + weapon.Power);
                
                //Check if weapon must be unequipped
                if(weapon.Use(baseAttack)) UnequipWeapon();
                
                return result;
            }
            
            return baseAttack;
        }

        public void ReceiveDamage(Character enemy, int damage)
        {
            if (armor != null)
            {
                //Check if enemy is not using weapon. Direct attack to player if true.
                if (enemy.baseAttack == damage)
                {
                    Hp -= Math.Max(0,(damage / 2) - baseDefense);
                    baseDefense -= (damage / 2);
                }
                else
                {
                    //Check if armor must be unequipped
                    if(armor.Use(damage)) UnequipArmor();
                }
            }
            else
            {
                Hp -= Math.Max(0,damage - baseDefense);
                baseDefense -= damage;
            }
        }
        
        private void UnequipWeapon()
        {
            weapon = null;
        }
        
        private void UnequipArmor()
        {
            armor = null;
        }

        #endregion
    }
}