using System;

public class Equipment
{
	public const string TYPE_HELMET = "Helmet";
	public const string TYPE_ARMOR = "Armor";
	public const string TYPE_SWORD = "Sword";
	public const string TYPE_EMPTY = "Empty";

	public const int EQUIPMENT_MAX_DEFENSE = 15;
	public const int EQUIPMENT_MAX_ATTACK = 20;
	public const int EQUIPMENT_MAX_HEALTH = 50;
	public const int EQUIPMENT_MAX_PRICE = 100;

	private static string[] EQUIPMENT_ADJECTIVES = ["Golden", "Red", "Blue", "Green", "Rainbow", "Black",
													"Rusty", "Shiny", "Embroidered", "Furry", "Soft", 
													"Sour", "Succulent", "Tart", "Spoicy", "Moldy",
													"Perturbed", "Spooked", "Confident", "Giggling", "Sassy",
											        "Mythic", "Flimsy", "Turbo", "Mediocre", "Stromg"];

    private static int _id = 0;
    private string _name;
    private string _description;
    private int _price;
    private string _type;

    private int _defense;
    private int _attack;
	private int _health;

    public Equipment(string name, string type, int defense, 
		int attack, int health, string description = "", 
		int price = 0)
	{
		_id++;
		_name = name;
		_description = description;
		_price = price;
		_type = type;

		_defense = defense;
		_attack = attack;
		_health = health;
	}

	public Equipment()
	{
		_id++;
		setEmpty();
    }

	public void setEmpty()
	{
        _name = "Empty";
        _description = "Empty";
        _price = 0;
        _type = TYPE_EMPTY;
        _defense = 0;
        _attack = 0;
		_health = 0;
    }

	public bool isEmpty()
	{
		return (_type == TYPE_EMPTY);
	}


	public override string ToString()
	{
		return ("" +
			$"Name:........... {Name}\n" +
			$"Description:.... {Description}\n" +
			$"Attack:......... {Attack}\n" +
			$"Defense:........ {Defense}\n" +
			$"Health:......... {Health}\n");
	}

	protected int ID {  get { return _id; } }

	public string Name { get { return _name; } set { _name = value; } }
	public string Description { get { return _description;} set { _description = value;  } }
	public int Price { get { return _price; } set { _price = value; } }
	public string Type { get { return _type;} set { _type = value; } }
	public int Defense { get { return _defense; } set { _defense = value; } }
	public int Attack { get { return _attack; } set { _attack = value; } }
	public int Health { get { return _health;} set { _health = value; } }

	public static string getRandomEquipmentAdjective()
	{
		Random random = new Random();
		int rn = random.Next(0, EQUIPMENT_ADJECTIVES.Length);
		return EQUIPMENT_ADJECTIVES[rn];
    }

	public static int getRandomDefense(int lowerLimit = 0, int upperLimit = EQUIPMENT_MAX_DEFENSE)
	{
		Random random = new Random();
		return random.Next(Utils.coerce(lowerLimit, 0, EQUIPMENT_MAX_DEFENSE),
                           Utils.coerce(upperLimit, 0, EQUIPMENT_MAX_DEFENSE));
	}

	public static int getRandomAttack(int lowerLimit = 0, int upperLimit = EQUIPMENT_MAX_ATTACK)
	{
		Random random = new Random();
		return random.Next(Utils.coerce(lowerLimit, 0, EQUIPMENT_MAX_ATTACK),
                           Utils.coerce(upperLimit, 0, EQUIPMENT_MAX_ATTACK));
    }

	public static int getRandomHealth(int lowerLimit = 0, int upperLimit = EQUIPMENT_MAX_HEALTH)
	{
		Random random = new Random();
		return random.Next(Utils.coerce(lowerLimit, 0, EQUIPMENT_MAX_HEALTH),
						   Utils.coerce(upperLimit, 0, EQUIPMENT_MAX_HEALTH));
	}

	public static int getRandomPrice(int lowerLimit = 0, int upperLimit = EQUIPMENT_MAX_PRICE)
	{
        Random random = new Random();
        return random.Next(Utils.coerce(lowerLimit, 0, EQUIPMENT_MAX_PRICE),
                           Utils.coerce(upperLimit, 0, EQUIPMENT_MAX_PRICE));
    }
}
