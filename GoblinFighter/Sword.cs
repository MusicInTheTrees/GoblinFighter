using System;

public class Sword : Equipment
{
    public Sword(string name, int defense, int attack, 
        int health, string description = "", int price = 0) :
        base(name, Equipment.TYPE_SWORD, defense, attack, health,
            description, price)
    {
    }

    public Sword() : base()
    {
        generateRandomStats();
    }

    public void generateRandomStats()
    {
        Name = Equipment.getRandomEquipmentAdjective() + " Sword";
        Type = Equipment.TYPE_SWORD;
        Defense = Equipment.getRandomDefense() / 4;
        Attack = Equipment.getRandomAttack();
        Health = Equipment.getRandomHealth() / 4;
        Description = "A thing to whack your enemy";
        Price = Equipment.getRandomPrice();
    }
}
