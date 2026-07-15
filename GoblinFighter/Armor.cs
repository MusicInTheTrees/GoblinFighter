using System;

namespace GoblinFighter
{
    public class Armor : Equipment
    {
        public Armor(string name, int defense, int attack,
            int health, string description = "", int price = 0) :
            base(name, Equipment.TYPE_ARMOR, defense, attack, health,
                description, price)
        {
        }

        public Armor() : base()
        {
            generateRandomStats();
        }

        public void generateRandomStats()
        {
            Name = Equipment.getRandomEquipmentAdjective() + " Armor";
            Type = Equipment.TYPE_ARMOR;
            Defense = Equipment.getRandomDefense();
            Attack = Equipment.getRandomAttack() / 4;
            Health = Equipment.getRandomHealth();
            Description = "A thing to protect your gut";
            Price = Equipment.getRandomPrice();
        }
    }
}

