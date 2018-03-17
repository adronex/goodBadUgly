using UnityEngine;

namespace Controller
{
    public class Area
    {
        #region Fields
        public static event AimArenaEnterEventHandler AimArenaEnterEvent;
        public static event AimArenaExitEventHandler AimArenaExitEvent;
        public static event HeroArenaEnterEventHandler HeroArenaEnterEvent;
        public static event HeroArenaExitEventHandler HeroArenaExitEvent;
        public static event ShootArenaEnterEventHandler ShootArenaEnterEvent;

        private readonly AreaType type;
        private Vector2 botLeftPoint;
        private Vector2 topRightPoint;
        #endregion
        #region Public Methods
        public Area(AreaType type, RectTransform transformZone)
        {
            this.type = type;

            var pos = transformZone.position;
            var rect = transformZone.rect;

            botLeftPoint = new Vector2(pos.x + rect.xMin, pos.y + rect.yMin);
            topRightPoint = new Vector2(pos.x + rect.xMax, pos.y + rect.yMax);
        }


        public void Invoke(Vector2 mousePos)
        {
            var isCollided = Check(this, mousePos);
            if (isCollided)
            {
                switch (type)
                {
                    case AreaType.AimArea:
                        if (AimArenaEnterEvent != null)
                        {
                            AimArenaEnterEvent();
                        }
                        break;
                    case AreaType.HeroArea:
                        if (HeroArenaEnterEvent != null)
                        {
                            HeroArenaEnterEvent();
                        }
                        break;
                    case AreaType.ShootArea:
                        if (ShootArenaEnterEvent != null)
                        {
                            ShootArenaEnterEvent();
                        }
                        break;
                }
            }
            else
            {
                switch (type)
                {
                    case AreaType.AimArea:
                        if (AimArenaExitEvent != null)
                        {
                            AimArenaExitEvent();
                        }
                        break;
                    case AreaType.HeroArea:
                        if (HeroArenaExitEvent != null)
                        {
                            HeroArenaExitEvent();
                        }
                        break;
                }
            }
        }
        #endregion
        #region Private methods
        private static bool Check(Area area, Vector2 mousePos)
        {
            return mousePos.x >= area.botLeftPoint.x && mousePos.x <= area.topRightPoint.x
                   && mousePos.y >= area.botLeftPoint.y && mousePos.y <= area.topRightPoint.y;
        }
        #endregion

        #region Event handlers
        public delegate void AimArenaEnterEventHandler();
        public delegate void AimArenaExitEventHandler();
        public delegate void HeroArenaEnterEventHandler();
        public delegate void HeroArenaExitEventHandler();
        public delegate void ShootArenaEnterEventHandler();
        #endregion
    }
}