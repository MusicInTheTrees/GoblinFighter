using System;

namespace GoblinFighter
{
    public class Enemy : Character
    {
        private Random random = new Random();
        private double dropPercent = 0.30;
        private const int M_RANDOM_LIMIT = 50;
        private int dropThreshold = (int)(0.3 * (double)M_RANDOM_LIMIT);
        private int generateThreshold = (int)(0.2 * (double)(M_RANDOM_LIMIT));

        public Enemy(string name, int level, string description = "Evil bad doer!") : base(name, description)
        {
            setLevel(level);

            Helmet helmet = new Helmet();
            if (rn() > generateThreshold)
            {
                helmet.setEmpty();
            }
            

            Armor armor = new Armor();
            if (rn() > generateThreshold)
            {
                armor.setEmpty();
            }
            

            Sword sword = new Sword();
            if (rn() > generateThreshold)
            {
                sword.setEmpty();
            }
            
            setHelmet(helmet);
            setArmor(armor);
            setSword(sword);

            Money = rn();
        }

        private int rn()
        {
            return random.Next(M_RANDOM_LIMIT);
        }

        public int dropExp()
        {
            return calcDroppedExperience();
        }

        public Helmet dropHelmet()
        {
            Helmet helmet = getHelmet();

            if (rn() < dropThreshold)
            {
                if (helmet.isEmpty())
                {
                    return new Helmet();
                }
                else
                {
                    return helmet;
                }
            }
            else
            {
                helmet.setEmpty();
                return helmet;
            }
        }

        public Armor dropArmor()
        {
            Armor armor = getArmor();

            if (rn() < dropThreshold)
            {
                if (armor.isEmpty())
                {
                    return new Armor();
                }
                else
                {
                    return armor;
                }
            }
            else
            {
                armor.setEmpty();
                return armor;
            }
        }

        public Sword dropSword()
        {
            Sword sword = getSword();

            if (rn() < dropThreshold)
            {
                if (sword.isEmpty())
                {
                    return new Sword();
                }
                else
                {
                    return sword;
                }
            }
            else
            {
                sword.setEmpty();
                return sword;
            }
        }

        public int dropMoney()
        {
            return Money;
        }
    }
}

