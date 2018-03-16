using UnityEngine;

namespace Model
{
    public class BattleField
    {
        private Hero ownHero;
        private Hero enemyHero;

        public BattleField(Hero own, Hero enemy)
        {
            ownHero = own;
            enemyHero = enemy;
        }


        public Hero OwnHero
        {
            get { return ownHero; }
        }

        public Hero EnemyHero
        {
            get { return enemyHero; }
        }

        public Hero GetHero(HeroType type)
        {
            switch (type)
            {
                case HeroType.Own:
                    return ownHero;
                case HeroType.Enemy:
                    return enemyHero;
                default:
                    throw new UnityException();
            }
        }
    }
}