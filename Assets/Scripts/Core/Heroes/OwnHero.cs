using UI;

namespace Core.Heroes
{
    public class OwnHero : Hero
    {
        #region Fields
        public static HeroType heroType;
        #endregion
        #region Public methods
        public OwnHero(HeroInfo heroInfo) : base(heroInfo)
        {
        }
        #endregion
    }
}