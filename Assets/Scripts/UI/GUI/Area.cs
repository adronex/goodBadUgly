using UnityEngine;

namespace UI.GUI
{
    internal class Area
    {
        #region Fields
        private Vector2 botLeftPoint;
        private Vector2 topRightPoint;

        private bool IsClickedOnThisFrame;
        #endregion
        #region Public Methods
        internal Area(RectTransform transformZone)
        {
            var pos = transformZone.position;
            var rect = transformZone.rect;

            botLeftPoint = new Vector2(pos.x + rect.xMin, pos.y + rect.yMin);
            topRightPoint = new Vector2(pos.x + rect.xMax, pos.y + rect.yMax);
        }


        internal bool IsTouched(Vector2 mousePos)
        {
            if (IsClickedOnThisFrame)
            {
                return false;
            }

            var isCollided = Check(mousePos);
            if (isCollided)
            {
                return true;
            }

            return false;
        }
        #endregion
        #region Private methods
        private bool Check(Vector2 mousePos)
        {
            return mousePos.x >= botLeftPoint.x && mousePos.x <= topRightPoint.x
                   && mousePos.y >= botLeftPoint.y && mousePos.y <= topRightPoint.y;
        }


        internal void ResetTouch()
        {
            IsClickedOnThisFrame = false;
        }
        #endregion
    }
}