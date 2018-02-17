using UnityEngine;
public class HandController
{
    public Transform transform;
    private Vector2 aim;

    public HandController(Transform handAxis)
    {
        transform = handAxis;
    }

    public void LookToMouse()
    {
        //It's works correctly when the HandAxis's Y-component rotated to -90 degree and the Hand's Y- and Z- components rotated to 90
        //You can try to simplify it :) I spent about 2 hours but no effect
        aim = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.rotation = Quaternion.LookRotation(aim);
    }
}
