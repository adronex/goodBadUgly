using UI;

namespace Core.Heroes
{
    public class EnemyHero : Hero
    {
        #region Fields
        public static HeroType heroType;
        #endregion
        #region Public methods
        public EnemyHero(HeroInfo heroInfo) : base(heroInfo)
        {
        }
        #endregion
    }
}