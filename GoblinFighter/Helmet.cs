using System;


namespace GoblinFighter
{
    public class Helmet : Equipment
    {
        public Helmet(string name, int defense, int attack, 
            int health, string description = "", int price = 0) : 
            base(name, Equipment.TYPE_HELMET, defense, attack, health,
                description, price)
        {
        }

        public Helmet() : base()
        {
            generateRandomStats();
        }

        public void generateRandomStats()
        {
            Name = Equipment.getRandomEquipmentAdjective() + " Helmet";
            Type = Equipment.TYPE_HELMET;
            Defense = Equipment.getRandomDefense() / 2;
            Attack = Equipment.getRandomAttack() / 4;
            Health = Equipment.getRandomHealth() / 2;
            Description = "A thing to protect your nugget";
            Price = Equipment.getRandomPrice();
        }
    }

}
