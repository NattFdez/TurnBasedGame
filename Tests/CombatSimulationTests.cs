using TurnBasedGame.Source;
using NUnit.Framework;

namespace TurnBasedGame.Tests
{
    public class CombatSimulationTests
    {
        #region Weapon Durability

        [Test]
        public void WeaponDurability()
        {
            Weapon weapon = new Weapon("Weapon1", Equipment.UserType.Human, 30,80);
            
            Character character1 = new Character("Char1", Character.CharType.Human, 100, 10, 10, weapon, null);
            Character character2 = new Character("Char2", Character.CharType.Human, 100, 10, 10);
            
            character2.ReceiveDamage(character1,character1.Attack());

            Assert.AreEqual(79, character1.Weapon.Durability);
        }
        
        [Test]
        public void WeaponUnequip()
        {
            Weapon weapon = new Weapon("Weapon1", Equipment.UserType.Human, 30,1);
            
            Character character1 = new Character("Char1", Character.CharType.Human, 100, 10, 10, weapon, null);
            Character character2 = new Character("Char2", Character.CharType.Human, 100, 10, 10);
            
            character2.ReceiveDamage(character1,character1.Attack());

            var ex = Assert.Throws<Exception>(() => {
                Assert.IsNull(character1.Weapon);
            });
    
            Assert.AreEqual("Character has no weapon assigned to be retrieved.", ex.Message);
        }
        
        [Test]
        public void WeaponDurNoNegative()
        {
            Weapon weapon = new Weapon("Weapon1", Equipment.UserType.Human, 30,1);

            weapon.Use(1);

            Assert.Throws<Exception>(() => {
                weapon.Use(1);
            });
        }

        #endregion

        #region Armor Durability

        [Test]
        public void ArmorDurability_WithWeapon()
        {
            Weapon weapon = new Weapon("Weapon1", Equipment.UserType.Human, 30,80);
            Armor armor = new Armor("Armor1", Equipment.UserType.Human, 50, 50);
            
            Character character1 = new Character("Char1", Character.CharType.Human, 100, 10, 10, weapon, null);
            Character character2 = new Character("Char2", Character.CharType.Human, 100, 10, 10, null, armor);
            
            character2.ReceiveDamage(character1,character1.Attack());

            Assert.AreEqual(10, character2.Armor.Durability);
        }
        
        [Test]
        public void ArmorDurability_NoWeapon()
        {
            Armor armor = new Armor("Armor1", Equipment.UserType.Human, 50, 50);
            
            Character character1 = new Character("Char1", Character.CharType.Human, 100, 10, 10, null, null);
            Character character2 = new Character("Char2", Character.CharType.Human, 100, 10, 10, null, armor);
            
            character2.ReceiveDamage(character1,character1.Attack());

            Assert.AreEqual(50, character2.Armor.Durability);
        }
        
        [Test]
        public void ArmorDurability_Damage0()
        {
            Armor armor = new Armor("Armor1", Equipment.UserType.Human, 50, 50);
            
            Character character1 = new Character("Char1", Character.CharType.Human, 100, 10, 10, null, null);
            Character character2 = new Character("Char2", Character.CharType.Human, 100, 10, 10, null, armor);
            
            character2.ReceiveDamage(character1,0);

            Assert.AreEqual(49, character2.Armor.Durability);
        }
        
        [Test]
        public void ArmorUnequip()
        {
            Weapon weapon = new Weapon("Weapon1", Equipment.UserType.Human, 50,1);
            Armor armor = new Armor("Armor1", Equipment.UserType.Human, 50, 50);
            
            Character character1 = new Character("Char1", Character.CharType.Human, 100, 10, 10, weapon, null);
            Character character2 = new Character("Char2", Character.CharType.Human, 100, 10, 10, null, armor);
            
            character2.ReceiveDamage(character1,character1.Attack());

            var ex = Assert.Throws<Exception>(() => {
                Assert.IsNull(character2.Armor);
            });
    
            Assert.AreEqual("Character has no armor assigned to be retrieved.", ex.Message);
        }
        
        [Test]
        public void ArmorDurNoNegative()
        {
            Armor armor = new Armor("Armor1", Equipment.UserType.Human, 50, 1);

            armor.Use(1);

            Assert.Throws<Exception>(() => {
                armor.Use(1);
            });
        }

        #endregion

        #region Character HP

        [Test]
        public void HP_NoArmorNoWeapon()
        {
            Character character1 = new Character("Char1", Character.CharType.Human, 100, 10, 30);
            Character character2 = new Character("Char2", Character.CharType.Human, 100, 10, 10);
            
            character2.ReceiveDamage(character1,character1.Attack());

            Assert.AreEqual(80, character2.Hp);
        }
        
        [Test]
        public void HP_ArmorNoWeapon()
        {
            Armor armor = new Armor("Armor1", Equipment.UserType.Human, 50, 50);
            
            Character character1 = new Character("Char1", Character.CharType.Human, 100, 10, 30, null, null);
            Character character2 = new Character("Char2", Character.CharType.Human, 100, 10, 10, null, armor);
            
            character2.ReceiveDamage(character1,character1.Attack());

            Assert.AreEqual(95, character2.Hp);
        }
        
        [Test]
        public void HP_ArmorWeapon()
        {
            Weapon weapon = new Weapon("Weapon1", Equipment.UserType.Human, 30,80);
            Armor armor = new Armor("Armor1", Equipment.UserType.Human, 50, 50);
            
            Character character1 = new Character("Char1", Character.CharType.Human, 100, 10, 10, weapon, null);
            Character character2 = new Character("Char2", Character.CharType.Human, 100, 10, 10, null, armor);
            
            character2.ReceiveDamage(character1,character1.Attack());

            Assert.AreEqual(100, character2.Hp);
        }
        
        [Test]
        public void HP_NoNegatives()
        {
            Character character1 = new Character("Char1", Character.CharType.Human, 100, 10, 30);
            Character character2 = new Character("Char2", Character.CharType.Human, 100, 10, 30);

            character1.ReceiveDamage(character2, 999);

            Assert.AreEqual(0, character1.Hp);
        }

        #endregion
        
        #region Character 0 Dur Equipment
        
        [Test]
        public void Armor_0Dur()
        {
            Armor armor = new Armor("Armor1", Equipment.UserType.Human, 50, 1);
            
            Character character1 = new Character("Char1", Character.CharType.Human, 100, 10, 10);

            armor.Use(1);

            Assert.False(character1.EquipArmor(armor));
        }
        
        [Test]
        public void Weapon_0Dur()
        {
            Weapon weapon = new Weapon("Weapon1", Equipment.UserType.Human, 50, 1);
            
            Character character1 = new Character("Char1", Character.CharType.Human, 100, 10, 10);

            weapon.Use(1);

            Assert.False(character1.EquipWeapon(weapon));
        }
        
        #endregion

        #region Weapon/Armor Change Values

        [Test]
        public void WeaponChangeValues()
        {
            Weapon weapon = new Weapon("Weapon1", Equipment.UserType.Human, 30,80);

            weapon.ChangeName("Hey");

            Assert.AreEqual("Weapon1", weapon.Name);
        }
        
        [Test]
        public void ArmorChangeValues()
        {
            Armor armor = new Armor("Armor1", Equipment.UserType.Human, 50, 1);

            armor.ChangeName("Hey");

            Assert.AreEqual("Armor1", armor.Name);
        }

        #endregion
    }
}