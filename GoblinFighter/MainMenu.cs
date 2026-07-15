using System;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;

namespace GoblinFighter
{
    public class MainMenu
    {
        private const string M_OPT_MOVE = "w";
        private const string M_OPT_MOVE_DESC = " - Move";

        private const string M_OPT_SHOW_PLAYER_STATS = "p";
        private const string M_OPT_SHOW_PLAYER_STATS_DESC = " - Show Player stats";

        private const string M_OPT_QUIT = "0";
        private const string M_OPT_QUIT_DESC = " - quit";

        private string[,] M_OPTIONS = new string[,] { { M_OPT_MOVE, M_OPT_MOVE_DESC },
                                                      { M_OPT_SHOW_PLAYER_STATS, M_OPT_SHOW_PLAYER_STATS_DESC },
                                                      { M_OPT_QUIT, M_OPT_QUIT_DESC } };

        private string[] M_ENEMY_NAMES = [ "Gobby", "LOTR Neck Beard", "Turbo Goblin", "Goblin Deez Nutz", "Erik", "Eric with a 'C'",
                                           "Gob Lynn", "Chip da Goblin", "Roberto Francisco Gobalin", "Peter Pimpston", "Gobert",
                                           "Hobert Goblin", "Lamar Gobs", "Fabio Goblinstein", "Rob Goblinson", "Gob Johnson"];

        private const int M_RANDOM_LIMIT = 10;
        private const int M_BATTLE_THRESHOLD = 3;
        private const int M_FIND_LOOT_THRESHOLD = 3;
        private Random m_rng = new Random();

        Player _player;

        public MainMenu()
        {
            _player = new Player("Beep", "Boop");
        }

        private void showOptions()
        {
            for (int i = 0; i < M_OPTIONS.GetLength(0); i++)
            {
                Console.WriteLine(M_OPTIONS[i, 0] + M_OPTIONS[i, 1]);
            }
        }

        public bool run()
        {
            welcomeSequence();

            bool gameContinue = true;

            while (true == gameContinue)
            {
                showOptions();

                string playerOptionSelect = Console.ReadLine();

                Console.WriteLine("");

                switch (playerOptionSelect)
                {
                    case M_OPT_MOVE:
                        gameContinue = moveSequence();
                        break;
                    case M_OPT_SHOW_PLAYER_STATS:
                        Console.WriteLine(_player);
                        break;
                    case M_OPT_QUIT:
                        gameContinue = quitSequence();
                        break;
                    default:
                        Console.WriteLine("Player doesn't know what to do! Rethinking strategy...");
                        break;
                }
            }
            return true;
        }

        private int rn()
        {
            return m_rng.Next(M_RANDOM_LIMIT);
        }

        private bool isPlayerAttacked()
        {
            return (rn() > (M_RANDOM_LIMIT / 4));
        }

        private bool doesPlayerFindTreasure()
        {
            return (rn() > (M_RANDOM_LIMIT / 4));
        }

        private void welcomeSequence()
        {
            Console.WriteLine("What up homie?!?! Glad you wanna fight some goblins, yo!\n");
            Console.WriteLine("I know you're my homie, but wtf was your name again?\n");
            string userInput = Console.ReadLine();
            _player.Name = userInput;
            Console.WriteLine("\nLOL that's a nerdy ass name, but ok...");
            Console.WriteLine("How do folks know you? What's your reputation?\n");
            Console.WriteLine("Go ahead, I'm honestly listening, I promise!\n");
            userInput = Console.ReadLine();
            _player.Description = userInput;
            Console.WriteLine($"\nOk... {_player.Name} who apparents thinks themselves like\n");
            Console.WriteLine($"{_player.Description}\n");
            Console.WriteLine("Get your ass ready to pound or be pounded by some goblins!\n");
        }

        private bool moveSequence()
        {
            Console.WriteLine("Look at you go! You moved a couple feed, hot dayum!");
            _player.incrementMoveCount();
            _player.heal(1);
            
            if (rn() > M_BATTLE_THRESHOLD)
            {
                Battle _battleStage = new Battle(_player, getEnemy());

                Console.WriteLine("\n<<<<<<<<<<<<<<<<<<< GOBLIN APPEARS >>>>>>>>>>>>>>>>>>>>>");
                return _battleStage.run();
            }
            else if (rn() > M_FIND_LOOT_THRESHOLD)
            {
                int randNum = rn();
                if (randNum <= (int) ((double)M_RANDOM_LIMIT / 5.0))
                {
                    Helmet foundHelmet = new Helmet();
                    Console.WriteLine("Woah! Dude, you totally found a cool looking helmet... just laying there... sus...");
                    _player.displayCompareHelmet(foundHelmet);
                    Console.WriteLine($"Take new helmet: {foundHelmet.Name}?");
                    if (Battle.displayTakeOptionMenu())
                    {
                        Console.WriteLine($"{_player.Name} aqcuired {foundHelmet.Name}");
                        _player.setHelmet(foundHelmet);
                    }
                }
                else if (randNum <= (int) ((double)M_RANDOM_LIMIT / 2.0))
                {
                    Armor foundArmor = new Armor();
                    if (false == foundArmor.isEmpty())
                    {
                        _player.displayCompareArmor(foundArmor);
                        Console.WriteLine($"Take new armor: {foundArmor.Name}?");
                        if (Battle.displayTakeOptionMenu())
                        {
                            Console.WriteLine($"{_player.Name} aqcuired {foundArmor.Name}");
                            _player.setArmor(foundArmor);
                        }
                    }
                }
                else
                {
                    Sword foundSword = new Sword();
                    if (false == foundSword.isEmpty())
                    {
                        _player.displayCompareSword(foundSword);
                        Console.WriteLine($"Take new sword: {foundSword.Name}?");
                        if (Battle.displayTakeOptionMenu())
                        {
                            Console.WriteLine($"{_player.Name} aqcuired {foundSword.Name}");
                            _player.setSword(foundSword);
                        }
                    }
                }
            }

            return true;
        }

        private bool quitSequence()
        {
            Console.WriteLine("Hmmm, leaving the dungeon? Bummer, I thought we were just starting to have fun :(\n");

            _player.displayEndGameResults();

            return false;
        }

        private Enemy getEnemy()
        {
            return new Enemy(getEnemyName(), getEnemyLevel());
        }

        private string getEnemyName()
        {
            return M_ENEMY_NAMES[m_rng.Next(M_ENEMY_NAMES.Length)];
        }

        private int getEnemyLevel()
        {
            int lwr = Utils.coerce(_player.getLevel(), _player.getLevel() - 2, _player.getLevel());
            int upr = _player.getLevel() + 2;

            return m_rng.Next(lwr, upr);
        }


    }

}
