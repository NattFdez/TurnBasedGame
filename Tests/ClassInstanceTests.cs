using TurnBasedGame.Source;
using NUnit.Framework;

namespace TurnBasedGame.Tests
{
    public class ClassInstanceTests
    {
        #region Character Negative Values

        [Test]
        public void CharNegative_HpDefAtt()
        {
            Assert.Throws<Exception>(() =>
            {
                Character character = new Character("Char1", Character.CharType.Human, -1, -1, -1);
            });
        }
        
        [Test]
        public void CharNegative_DefAtt()
        {
            Assert.Throws<Exception>(() =>
            {
                Character character = new Character("Char1", Character.CharType.Human, 1, -1, -1);
            });
        }
        
        [Test]
        public void CharNegative_Att()
        {
            Assert.Throws<Exception>(() =>
            {
                Character character = new Character("Char1", Character.CharType.Human, 1, 1, -1);
            });
        }
        
        [Test]
        public void CharNoNegatives()
        {
            Assert.DoesNotThrow(() =>
            {
                Character character = new Character("Char1", Character.CharType.Human, 1, 1, 1);
            });
        }

        #endregion

        #region Weapon Negative Values

        [Test]
        public void WeaponNegative_PowDur()
        {
            Assert.Throws<Exception>(() =>
            {
                Weapon weapon = new Weapon("Weapon1", Equipment.UserType.Human, -1,-1);
            });
        }
        
        [Test]
        public void WeaponNegative_Dur()
        {
            Assert.Throws<Exception>(() =>
            {
                Weapon weapon = new Weapon("Weapon1", Equipment.UserType.Human, 1,-1);
            });
        }

        #endregion

        #region Armor Negative Values

        [Test]
        public void ArmorNegative_PowDur()
        {
            Assert.Throws<Exception>(() =>
            {
                Armor armor = new Armor("Armor1", Equipment.UserType.Human, -1,-1);
            });
        }
        
        [Test]
        public void ArmorNegative_Dur()
        {
            Assert.Throws<Exception>(() =>
            {
                Armor armor = new Armor("Armor1", Equipment.UserType.Human, 1,-1);
            });
        }

        #endregion

        #region Weapon 0 Values

        [Test]
        public void WeaponZero_PowDur()
        {
            Assert.Throws<Exception>(() =>
            {
                Weapon weapon = new Weapon("Weapon1", Equipment.UserType.Human, 0,0);
            });
        }
        
        [Test]
        public void WeaponZero_Dur()
        {
            Assert.Throws<Exception>(() =>
            {
                Weapon weapon = new Weapon("Weapon1", Equipment.UserType.Human, 1,0);
            });
        }

        #endregion
        
        #region Armor 0 Values

        [Test]
        public void ArmorZero_PowDur()
        {
            Assert.Throws<Exception>(() =>
            {
                Armor armor = new Armor("Armor1", Equipment.UserType.Human, 0,0);
            });
        }
        
        [Test]
        public void ArmorZero_Dur()
        {
            Assert.Throws<Exception>(() =>
            {
                Armor armor = new Armor("Armor1", Equipment.UserType.Human, 1,0);
            });
        }

        #endregion

        #region Weapon/Armor Min. Values

        [Test]
        public void ArmorMinValues()
        {
            Assert.DoesNotThrow(() =>
            {
                Armor armor = new Armor("Armor1", Equipment.UserType.Human, 1,1);
            });
        }
        
        [Test]
        public void WeaponMinValues()
        {
            Assert.DoesNotThrow(() =>
            {
                Weapon weapon = new Weapon("Weapon1", Equipment.UserType.Human, 1,1);
            });
        }

        #endregion
        
        #region Character HP below 1

        [Test]
        public void CharHPLessThan1()
        {
            Assert.Throws<Exception>(() =>
            {
                Character character = new Character("Char1", Character.CharType.Human, 0, 1, 1);
            });
        }

        #endregion

        #region Equip Weapon

        [Test]
        public void CharEquipWeapon_Diff()
        {
            Weapon weapon = new Weapon("Weapon1", Equipment.UserType.Human, 1, 1);
            Character character = new Character("Char1", Character.CharType.Beast, 1, 1,1);
            
            Assert.False(character.EquipWeapon(weapon));
        }
        
        [Test]
        public void CharEquipWeapon_Equal()
        {
            Weapon weapon = new Weapon("Weapon1", Equipment.UserType.Human, 1, 1);
            Character character = new Character("Char1", Character.CharType.Human, 1, 1,1);
            
            Assert.True(character.EquipWeapon(weapon));
        }
        
        [Test]
        public void CharEquipWeapon_Any()
        {
            Weapon weapon = new Weapon("Weapon1", Equipment.UserType.Any, 1, 1);
            Character character = new Character("Char1", Character.CharType.Human, 1, 1,1);
            
            Assert.True(character.EquipWeapon(weapon));
        }

        #endregion

        #region Equip Armor

        [Test]
        public void CharEquipArmor_Diff()
        {
            Armor armor = new Armor("Armor1", Equipment.UserType.Human, 1, 1);
            Character character = new Character("Char1", Character.CharType.Beast, 1, 1,1);
            
            Assert.False(character.EquipArmor(armor));
        }
        
        [Test]
        public void CharEquipArmor_Equal()
        {
            Armor armor = new Armor("Armor1", Equipment.UserType.Human, 1, 1);
            Character character = new Character("Char1", Character.CharType.Human, 1, 1,1);
            
            Assert.True(character.EquipArmor(armor));
        }
        
        [Test]
        public void CharEquipArmor_Any()
        {
            Armor armor = new Armor("Armor1", Equipment.UserType.Any, 1, 1);
            Character character = new Character("Char1", Character.CharType.Human, 1, 1,1);
            
            Assert.True(character.EquipArmor(armor));
        }

        #endregion

        #region Equip +1 Weapon/Armor

        [Test]
        public void CharExtraWeapon()
        {
            Weapon weapon1 = new Weapon("Weapon1", Equipment.UserType.Human, 1, 1);
            Weapon weapon2 = new Weapon("Weapon2", Equipment.UserType.Human, 1, 1);
            
            Character character = new Character("Char1", Character.CharType.Human, 1, 1,1, weapon1, null);
            
            Assert.True(character.EquipWeapon(weapon2));
            
            Assert.AreNotSame(character.Weapon, weapon1);
        }
        
        [Test]
        public void CharExtraArmor()
        {
            Armor armor1 = new Armor("Armor1", Equipment.UserType.Human, 1, 1);
            Armor armor2 = new Armor("Armor2", Equipment.UserType.Human, 1, 1);
            
            Character character = new Character("Char1", Character.CharType.Human, 1, 1,1, null, armor1);
            
            Assert.True(character.EquipArmor(armor2));
            
            Assert.AreNotSame(character.Armor, armor1);
        }

        #endregion
    }
}