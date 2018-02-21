using UnityEngine;
public class HandController
{
    private Transform transform;

    private Vector2 aim;

    public HandController(Transform handAxis)
    {
        transform = handAxis;
    }

    public void LookToMouse()
    {
        aim = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.rotation = Quaternion.LookRotation(aim);
    }
}
