using System;

namespace GoblinFighter
{
    public class Battle
    {
        private const string MENU_OPT_ATTACK = "e";
        private const string MENU_OPT_ATTACK_DESC = " - ATTACK THIS PIECE OF SHIT!\n";
        private const string MENU_OPT_RUN = "q";
        private const string MENU_OPT_RUN_DESC = " - OH SHIT! RUUUUUUN!\n";
        private const string MENU_OPT_FALL_ON_SWORD = "f";
        private const string MENU_OPT_FALL_ON_SWORD_DESC = " - YOU KNOW WHAT... I'M OUT...\n";

        private string[,] MENU_OPTIONS = new string[,] { { MENU_OPT_ATTACK, MENU_OPT_ATTACK_DESC },
                                                         { MENU_OPT_RUN, MENU_OPT_RUN_DESC },
                                                         { MENU_OPT_FALL_ON_SWORD, MENU_OPT_FALL_ON_SWORD_DESC } };

        private string[] ENEMY_APPEAR_DESC = new string[] { "appears, frothing at the mouth for a piece of dat ass!",
                                                            "stumbles up, looking at you with malice.",
                                                            "turns around, glaring at you reminicent of your mom being pissed off."};

        private const int M_RANDOM_LIMIT = 10;
        private const int M_RUN_THRESHOLD = 5;

        Player _player;
        Enemy _enemy;

        public Battle(Player player, Enemy enemy)
        {
            _player = player;
            _enemy = enemy;
        }

        public bool run()
        {
            bool battleContinue = true;

            showEnemy();

            while (true == battleContinue)
            {
                _player.displayBattleStats();
                _enemy.displayBattleStats();

                showMenu();

                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case MENU_OPT_ATTACK:
                        battleContinue = attackSequence();
                        break;
                    case MENU_OPT_RUN:
                        battleContinue = runSequence();
                        break;
                    case MENU_OPT_FALL_ON_SWORD:
                        battleContinue = fosSequence();
                        break;
                    default:
                        Console.WriteLine("HMM, I JUST HAD A REALLY DUMB IDEA... ALRIGHT I GOTTA ACTUALLY DO SOMETHING\n");
                        break;
                }
            }

            if (_player.isDead())
            {
                return false;
            }
            else
            {
                return true;
            }

            
        }

        private void showMenu()
        {
            for (int i = 0; i < MENU_OPTIONS.GetLength(0); i++)
            {
                Console.WriteLine(MENU_OPTIONS[i, 0] + MENU_OPTIONS[i, 1]);
            }
        }

        private string getEnemyAppearedDesc()
        {
            Random random = new Random();
            return ENEMY_APPEAR_DESC[random.Next(ENEMY_APPEAR_DESC.Length)];
        }

        private void showEnemy()
        {
            Console.WriteLine($"{_enemy.Name} {getEnemyAppearedDesc()}\n");
            Console.WriteLine(_enemy.ToString());
        }

        private bool attackSequence()
        {
            int atk = _player.attack();
            Console.WriteLine("\nO==========||>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
            Console.WriteLine($"{_player.Name} attacked for {atk} damage.");
            Console.WriteLine("O==========||>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>\n");

            // enemy dies \m/
            if (false == _enemy.takeHit(atk))
            {
                _player.incrementEnemiesSlain();

                Console.WriteLine("^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^");
                Console.WriteLine("^^^^^^^^^^^^^^^^^^^^^^^^^ VICTORY ^^^^^^^^^^^^^^^^^^^^^^^^^");
                Console.WriteLine("^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^\n");
                Console.WriteLine($"{_player.Name} defeated {_enemy.Name}!\n");

                int money = _enemy.dropMoney();
                Console.WriteLine("$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$");
                Console.WriteLine($"{_enemy.Name} dropped {money} money!");
                Console.WriteLine("$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$\n");
                _player.addMoney(money);

                Helmet dropHelmet = _enemy.dropHelmet();
                if (false == dropHelmet.isEmpty())
                {
                    _player.displayCompareHelmet(dropHelmet);
                    Console.WriteLine($"Take new helmet: {dropHelmet.Name}?\n");
                    if (displayTakeOptionMenu())
                    {
                        Console.WriteLine($"{_player.Name} aqcuired {dropHelmet.Name}.\n");
                        _player.setHelmet(dropHelmet);
                    }
                }

                Armor dropArmor = _enemy.dropArmor();
                if (false == dropArmor.isEmpty())
                {
                    _player.displayCompareArmor(dropArmor);
                    Console.WriteLine($"Take new armor: {dropArmor.Name}?\n");
                    if (displayTakeOptionMenu())
                    {
                        Console.WriteLine($"{_player.Name} aqcuired {dropArmor.Name}.\n");
                        _player.setArmor(dropArmor);
                    }
                }

                Sword dropSword = _enemy.dropSword();
                if (false == dropSword.isEmpty())
                {
                    _player.displayCompareSword(dropSword);
                    Console.WriteLine($"Take new sword: {dropSword.Name}?\n");
                    if (displayTakeOptionMenu())
                    {
                        Console.WriteLine($"{_player.Name} aqcuired {dropSword.Name}.\n");
                        _player.setSword(dropSword);
                    }
                }

                int exp = _enemy.dropExp();
                Console.WriteLine("EXPEXPEXPEXPEXPEXPEXPEXPEXPEXPEXPEXPEXP");
                Console.WriteLine($"{_player.Name} gains {exp} experience!");
                Console.WriteLine("EXPEXPEXPEXPEXPEXPEXPEXPEXPEXPEXPEXPEXP\n");
                if (_player.addExperience(exp))
                {
                    _player.levelUp();
                    Console.WriteLine(_player.displayLevelUpStats());
                }

                return false;
            }

            atk = _enemy.attack();
            Console.WriteLine("\n<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<||==========O");
            Console.WriteLine($"{_enemy.Name} attacked for {atk} damage.");
            Console.WriteLine("<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<||==========O\n");

            if (false == _player.takeHit(atk))
            {
                _player.die();
                return false;
            }

            return true;
        }

        public static bool displayTakeOptionMenu()
        {
            bool legitAnswer = false;
            bool take = false;

            while (false == legitAnswer)
            {
                Console.WriteLine("y - Take!\n");
                Console.WriteLine("n - Leave!\n");

                string userInput = Console.ReadLine();
                legitAnswer = true;

                switch (userInput)
                {
                    case "y":
                        take = true;
                        break;
                    case "Y":
                        take = true;
                        break;
                    case "n":
                        take = false;
                        break;
                    case "N":
                        take = false;
                        break;
                    default:
                        legitAnswer = false;
                        break;
                }
            }

            return take;
            
        }

        private bool runSequence()
        {
            Random random = new Random();

            if (random.Next(M_RANDOM_LIMIT) > M_RUN_THRESHOLD)
            {
                Console.WriteLine("Ayyyy you successfully ran away... coward!\n");
                return false;
            }

            Console.WriteLine("WOOOW you couldn't even run away from this stupid slow all goblin >:( this game is BULLSHIT!\n");

            int atk = _enemy.attack();
            Console.WriteLine($"{_enemy.Name} attacked for {atk} damage.\n");

            if (false == _player.takeHit(atk))
            {
                _player.die();
                return false;
            }
            return true;
        }

        private bool fosSequence()
        {
            Console.WriteLine("-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-");
            Console.WriteLine("YOU FALLEN ON YOUR SWORD LIKE A REAL HERO");
            Console.WriteLine("In the words of our lord and saviour...");
            Console.WriteLine("Kill yourself, my man. - Viper\n");
            _player.die();
            Console.WriteLine("-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-");
            return false;
        }
    }

}
