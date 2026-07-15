using System;

public class Level : Equipment
{
	public Level(int level = 1)
	{
        if (level < 1)
        {
            level = 1;
        }

        this.Name = "";
        this.Type = Equipment.TYPE_EMPTY;
        this.Defense = calcDefense(level);
        this.Attack = calcAttack(level);
        this.Health = calcHealth(level);
        this.Description = "";
        this.Price = 0;
    }

    public void newLevel(int level)
    {
        this.Defense = calcDefense(level);
        this.Attack = calcAttack(level);
        this.Health = calcHealth(level);
    }

    public string displayLevelStats()
    {
        return "" +
            $"Attack:..... +{Attack}\n" +
            $"Defense:.... +{Defense}\n" +
            $"Health:..... +{Health}\n";
    }

    private int calcDefense(int level)
    {
        int numerator = (int)((double)Equipment.getRandomDefense() * 1.3);
        int denominator = EQUIPMENT_MAX_DEFENSE / 2;
        return (level + (numerator / denominator));
    }

    private int calcAttack(int level)
    {
        int numerator = (int)((double)Equipment.getRandomAttack() * 1.3 );
        int denominator = EQUIPMENT_MAX_ATTACK / 2;
        return (level + (numerator / denominator));
    }

    private int calcHealth(int level)
    {
        int numerator = (int)((double)Equipment.getRandomHealth() * 1.3);
        int denominator = EQUIPMENT_MAX_HEALTH / 2;
        return (level + (numerator / denominator));
    }
}
