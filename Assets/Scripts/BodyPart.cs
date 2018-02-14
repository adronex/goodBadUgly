using UnityEngine;

public class BodyPart
{
    public static event BodyPartDamagedEventHandler BodyPartDamagedEvent;

    private float width;
    private float height;

    private int minDamage;
    private int maxDamage;

    private Transform bodyPart;
    

    public bool Check(Vector2 bulletPos)
    {
        Vector2 bodyPos = bodyPart.position;

        if (bulletPos.x >= bodyPos.x - width && bulletPos.x <= bodyPos.x + width)
        {
            if (bulletPos.y >= bodyPos.y - height && bulletPos.y <= bodyPos.y + height)
            {
                return true;
            }
        }

        return false;
    }


    public void GetDamage()
    {
        if (BodyPartDamagedEvent != null)
        {
            BodyPartDamagedEvent(Random.Range(minDamage, maxDamage));
        }
    }


    public BodyPart(Transform bodyPart)
    {
        this.bodyPart = bodyPart;

        var spriteRender = bodyPart.GetComponent<SpriteRenderer>();
        width = spriteRender.size.x * bodyPart.lossyScale.x / 2;
        height = spriteRender.size.y * bodyPart.lossyScale.y / 2;

        var info = bodyPart.GetComponent<BodyPartInfo>();
        minDamage = info.MinDamage;
        maxDamage = info.MaxDamage;
    }


    public delegate void BodyPartDamagedEventHandler(int damage);
}
