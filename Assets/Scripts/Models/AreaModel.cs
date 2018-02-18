//основные данные игры вроде здоровья или запаса патронов
//сериализуются, де-, конвертируется между типами
//загружает/созраняет данные (по сети или локально)
//Сообщает контроллерам о прогрессе операции
//Хранит состояние конечного автомата игры
//Никогда не вызывает View!
using UnityEngine;

public class AreaModel
{
    public Area Type;
    public Vector2 BotLeftPoint;
    public Vector2 TopRightPoint;

    public AreaModel(Area type, RectTransform transformZone)
    {
        Type = type;

        var pos = transformZone.position;
        var rect = transformZone.rect;
        BotLeftPoint = new Vector2(pos.x + rect.xMin, pos.y + rect.yMin);
        TopRightPoint = new Vector2(pos.x + rect.xMax, pos.y + rect.yMax);
    }
}