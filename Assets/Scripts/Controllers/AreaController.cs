using UnityEngine;
public class AreaController
{
    private Notification notification;

    public void FindClickedAreas(Vector2 mousePos)
    {
        foreach (var area in App.Model.Areas)
        {
            if (Check(area, mousePos))
            {
                switch (area.Type)
                {
                    case Area.AimArea:
                        notification = Notification.AimAreaEnter;
                        break;
                    case Area.HeroArea:
                        notification = Notification.HeroAreaEnter;
                        break;
                    case Area.ShootArea:
                        notification = Notification.ShootAreaEnter;
                        break;
                }
            }
            else
            {
                switch (area.Type)
                {
                    case Area.HeroArea:
                        notification = Notification.HeroAreaExit;
                        break;
                }
            }

            App.Notify(notification);
        }
    }


    private static bool Check(AreaModel area, Vector2 mousePos)
    {
        if (mousePos.x >= area.BotLeftPoint.x && mousePos.x <= area.TopRightPoint.x
            && mousePos.y >= area.BotLeftPoint.y && mousePos.y <= area.TopRightPoint.y)
        {
            return true;
        }

        return false;
    }
}
