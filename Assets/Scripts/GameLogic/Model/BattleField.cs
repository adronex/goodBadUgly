using UnityEngine;

namespace GameCore.Model
{
    public class BattleField
    {
        private Hero ownHero;
        private Hero enemyHero;
//        private Vector2Int ownHeroPosition;
//        private Vector2Int enemyHeroPosition;

        public BattleField()
        {
            ownHero = new Hero(HeroType.Own, 100, 6);
            enemyHero = new Hero(HeroType.Enemy, 100, 6);
        }

        public Hero OwnHero
        {
            get { return ownHero; }
        }

        public Hero EnemyHero
        {
            get { return enemyHero; }
        }

//        public Vector2Int OwnHeroPosition
//        {
//            get { return ownHeroPosition; }
//        }
//
//        public Vector2Int EnemyHeroPositionX
//        {
//            get { return enemyHeroPosition; }
//        }
    }
}