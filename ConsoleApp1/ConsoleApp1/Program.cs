using System;

namespace Napilnik1
{
    class Weapon
    {
        private int _damage;
        private int _bullets;
       
        public void Fire(Player player)
        {
            player.TakeDamage(_damage);
            _bullets -= 1;
        }
    }

    class Player
    {
        private int _health;

        public void TakeDamage(int damage)
        {
            _health -= damage;
        }
    }

    class Bot
    {
        private Weapon _weapon;
        private Player _player;

        public Bot(Player player, Weapon weapon)
        {
            _player = player;
            _weapon = weapon;
        }

        public void OnSeePlayer()
        {
            _weapon.Fire(_player);
        }
    }
}

