using System;

namespace GoblinFighter
{
    public class Player : Character
    {
        private int _moveCount;
        private int _enemiesSlain;
        public Player(string name, string description) : base(name, description)
        {
            _moveCount = 0;
            _enemiesSlain = 0;

            // Heuristically need more attack and defense
            _levelAttack += 5;
            _levelDefense += 5;
            updateOverallStats();
        }

        public void incrementMoveCount()
        {
            _moveCount++;
        }

        public void incrementEnemiesSlain()
        {
            _enemiesSlain++;
        }

        public void displayEndGameResults()
        {
            string results = "\n----- End Game Results -----\n" +
                            $"Move Count:...... {_moveCount}\n" +
                            $"Enemies Slain:... {_enemiesSlain}\n";

            Console.WriteLine(results);

            Console.WriteLine(ToString());
        }
    }

}
