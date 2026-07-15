using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GoblinFighter
{
    public class Character
    {
        private const int CHARACTER_BASE_HEALTH = 25;
        private const int HEALTH_BAR_SIZE = 40;

        protected string _name;
        protected string _description;
        protected int _attack;
        protected int _defense;
        protected int _maxHealth;
        protected int _health;
        protected int _level;
        protected int _currentExperience;
        protected int _nextExperience;
        protected int _money;

        protected int _levelAttack;
        protected int _levelDefense;
        protected int _levelMaxHealth;

        protected Level _levelStats;

        protected Helmet _helmet;
        protected Armor _armor;
        protected Sword _sword;

        protected Helmet _emptyHelmet;
        protected Armor _emptyArmor;
        protected Sword _emptySword;


        private Random rand = new Random();


        public Character(string name, string description = "An absolute beast!")
        {
            // ----- Armor -----
            _emptyArmor = new Armor();
            _emptyArmor.setEmpty();

            _emptySword = new Sword();
            _emptySword.setEmpty();

            _emptyHelmet = new Helmet();
            _emptyHelmet.setEmpty();

            _helmet = _emptyHelmet;
            _armor = _emptyArmor;
            _sword = _emptySword;

            // ----- Attributes -----
            _name = name;
            _description = description;
            _level = 1;
            _currentExperience = 0;
            _nextExperience = calcNextExperience();
            _money = 0;

            _levelStats = new Level(_level);
            _levelAttack = _levelStats.Attack;
            _levelDefense = _levelStats.Defense;
            _levelMaxHealth = CHARACTER_BASE_HEALTH;
            _maxHealth = CHARACTER_BASE_HEALTH;
            _health = CHARACTER_BASE_HEALTH;

            updateOverallStats();
        }

        protected void updateOverallStats()
        {
            _attack = _levelAttack + getEquipmentAttackBonus();
            _defense = _levelDefense + getEquipmentDefenseBonus();
            int memMaxHealth = _maxHealth;
            _maxHealth = _levelMaxHealth + getEquipmentHealthBonus();
            int deltaMaxHealth = memMaxHealth - _maxHealth;
            _health = Utils.coerceToMin(_health - deltaMaxHealth, 1);
        }

        private int calcNextExperience(int level = 0)
        {
            if (level <= 0)
            {
                level = _level;
            }

            return level * rand.Next(3, 10);
        }

        protected int calcDroppedExperience(int level = 0)
        {
            if (level <= 0)
            {
                level = _level;
            }

            return (int)( (double)level + (double)level * (double)rand.Next(10) / 5.0);
        }

        public bool addExperience(int exp)
        {
            _currentExperience += exp;
            if (_currentExperience >= _nextExperience)
            {
                return true;
            }

            return false;
        }

        public void levelUp()
        {
            _level++;
            _levelStats.newLevel(_level);

            _nextExperience = calcNextExperience();
            _currentExperience = 0;

            _levelAttack += _levelStats.Attack;
            _levelDefense += _levelStats.Defense;
            _levelMaxHealth += _levelStats.Health;
            updateOverallStats();
            _health = _maxHealth; // full recovery
        }

        public void levelDown()
        {
            if (_level <= 1)
            {
                _level = 1;
                return;
            }

            _level--;
            _levelStats.newLevel(_level);
            _nextExperience = calcNextExperience();
            _currentExperience = 0;

            _levelAttack = Utils.coerceToMin(_levelAttack - _levelStats.Attack, 1);
            _levelDefense = Utils.coerceToMin(_levelDefense - _levelStats.Defense, 1);
            _levelMaxHealth = Utils.coerceToMin(_levelMaxHealth - _levelStats.Health, CHARACTER_BASE_HEALTH);
            updateOverallStats();
            _health = _maxHealth;
        }

        public string displayLevelUpStats()
        {
            string lvlUpStr = "\n!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!\n" +
                              "!!!!!!!!!! LEVEL UP !!!!!!!!!!\n" +
                              "!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!\n";
            return lvlUpStr + _levelStats.displayLevelStats();
        }

        public int attack()
        {
            return _attack;
        }

        public bool takeHit(int attack)
        {
            int damage = attack - (int)((double)_defense / 1.5);
            if (damage <= 0)
            {
                damage = 0;
            }
            _health -= damage;
            return _health > 0;
        }

        public bool isDead()
        {
            return (_health == 0);
        }

        public void addMoney(int amount)
        {
            _money += amount;
        }

        public void removeMoney(int amount)
        {
            _money -= amount;
            if (_money < 0)
            {
                _money = 0;
            }
        }

        public void heal(int amount)
        {
            _health += amount;
            if (_health > _maxHealth)
            {
                _health = _maxHealth;
            }

        }
        public void setLevel(int level)
        {
            if (level < 1)
            {
                level = 1;
            }

            int deltaLevel = 0;

            if (level > _level)
            {
                deltaLevel = level - _level;
                for (int i = 0; i < deltaLevel; i++)
                {
                    levelUp();
                }

            }
            else if (level < _level)
            {
                deltaLevel = _level - level;
                for (int i = 0; i < deltaLevel; i++)
                {
                    levelDown();
                }
            }
            _level = level;
        }

        public int getLevel() { return _level; }

        public string Name { get { return _name; } set { _name = value; } }
        public string Description { get { return _description; } set { _description = value; } }
        public int Money { get { return _money; } set { _money = value; } }

        public void setHelmet(Helmet helmet) 
        {
            removeHelmet();
            _helmet = helmet;
            updateOverallStats();
        }
        public Helmet getHelmet() { return _helmet; }
        public void removeHelmet() 
        {
            _helmet = _emptyHelmet;
            updateOverallStats();
        }

        public void setArmor(Armor armor) 
        { 
            removeArmor();
            _armor = armor;
            updateOverallStats();
        }
        public Armor getArmor() { return _armor; }
        public void removeArmor() 
        {
            _armor = _emptyArmor;
            updateOverallStats();
        }

        public void setSword(Sword sword) 
        {
            removeSword();
            _sword = sword;
            updateOverallStats();
        }
        public Sword getSword() { return _sword; }
        public void removeSword() 
        {
            _sword = _emptySword; 
            updateOverallStats();
        }

        public override string ToString()
        {

            return "" +
                $"{getNameFrame()}\n" +
                $"Reputation: {Description}\n" +
                "----- Stats -----\n" +
                $"Level:........... {_level}\n" +
                $"Attack:.......... {_attack} [{_levelAttack} + {getEquipmentAttackBonus()}]\n" +
                $"Defense:......... {_defense} [{_levelDefense} + {getEquipmentDefenseBonus()}]\n" +
                $"Max Health:...... {_maxHealth} [{_levelMaxHealth} + {getEquipmentHealthBonus()}]\n" +
                $"Current Health:.. {getHealthBar()}\n" +
                $"Money:........... {Money}\n" +
                $"Current Exp:..... {_currentExperience}\n" +
                $"Exp to Next Lvl:. {_nextExperience}\n" +
                "----- Equipment -----\n" +
                $"Helmet:.......... {_helmet.Name}\n" +
                $"Armor:........... {_armor.Name}\n" +
                $"Sword:........... {_sword.Name}\n";

        }

        private string getNameFrame()
        {
            // Want to build a box frame around the characters name
            int nameCount = Name.Length;
            int frameCount = nameCount * 3;
            int spaceCount = nameCount - 1;

            string spaceStr = new string(' ', spaceCount);
            string nameStr = "|" + spaceStr + Name + spaceStr + "|\n";
            string frameStr = new string('-', frameCount);

            return frameStr + "\n" + nameStr + frameStr;
        }

        protected int getEquipmentDefenseBonus()
        {
            return _sword.Defense + _helmet.Defense + _armor.Defense;
        }

        protected int getEquipmentAttackBonus()
        {
            return _sword.Attack + _helmet.Attack + _armor.Attack;
        }

        protected int getEquipmentHealthBonus()
        {
            return _sword.Health + _helmet.Health + _armor.Health;
        }

        public void displayCompareHelmet(Helmet newHelmet)
        {
            Console.WriteLine("\n***** NEW HELMET *****");
            Console.WriteLine(newHelmet.ToString());
            Console.WriteLine("\n----- CURRENT HELMET -----");
            Console.WriteLine(_helmet.ToString());
        }

        public void displayCompareArmor(Armor newArmor)
        {
            Console.WriteLine("\n***** NEW ARMOR *****");
            Console.WriteLine(newArmor.ToString());
            Console.WriteLine("\n----- CURRENT ARMOR -----");
            Console.WriteLine(_armor.ToString());
        }

        public void displayCompareSword(Sword newSword)
        {
            Console.WriteLine("\n***** NEW SWORD *****");
            Console.WriteLine(newSword.ToString());
            Console.WriteLine("\n----- CURRENT SWORD -----");
            Console.WriteLine(_sword.ToString());
        }

        public void displayBattleStats()
        {
            Console.WriteLine($"{_name} (Lvl {_level}) {getHealthBar()}");
            Console.WriteLine($"Attack: {_attack}");
            Console.WriteLine($"Defense: {_defense}\n");
        }

        public string getHealthBar()
        {
            int hpBarLength = (int) ( (double)HEALTH_BAR_SIZE * ((double)_health / (double)_maxHealth) );
            string hpBar = "[";
            for (int i = 0; i < HEALTH_BAR_SIZE; i++)
            {
                if (i < hpBarLength)
                {
                    hpBar += "=";
                }
                else
                {
                    hpBar += " ";
                }
            }
            hpBar += "]";
            hpBar += $" {_health} / {_maxHealth}";
            return hpBar;
        }

        public void die()
        {
            Console.WriteLine("\nXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");
            Console.WriteLine("WHOMP! You died lol.");
            Console.WriteLine("Sowy, but you're armor got stolen and you lost 5 levels :'(");
            Console.WriteLine("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX\n");
            removeHelmet();
            removeArmor();
            removeSword();
            setLevel(_level - 5);
        }

        public bool saveStats()
        {



            return true;
        }
    }
}

