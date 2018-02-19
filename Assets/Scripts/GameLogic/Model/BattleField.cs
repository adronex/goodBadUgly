namespace GameCore.Model
{
    public class BattleField
    {
        private Hero ownHero;
        private Hero enemyHero;
        private int ownHeroPositionX;
        private int ownHeroPositionY;
        private int enemyHeroPositionX;
        private int enemyHeroPositionY;

        public Hero OwnHero
        {
            get { return ownHero; }
        }

        public Hero EnemyHero
        {
            get { return enemyHero; }
        }

        public int OwnHeroPositionX
        {
            get { return ownHeroPositionX; }
        }

        public int OwnHeroPositionY
        {
            get { return ownHeroPositionY; }
        }

        public int EnemyHeroPositionX
        {
            get { return enemyHeroPositionX; }
        }

        public int EnemyHeroPositionY
        {
            get { return enemyHeroPositionY; }
        }
    }
}