using UnityEngine;

public class HandRotatation : MonoBehaviour
{
    private Transform handAxis;
    private bool isTrace;

    Vector2 tempAim;

    private void Awake()
    {
        handAxis = GetComponent<Transform>();
    }

    private void OnEnable()
    {
        AimArea.AimAreaPressedEvent += OnAimAreaPressed;
        AimArea.AimAreaDepressedEvent += OnAimAreaDepressed;
    }

    private void OnDisable()
    {
        AimArea.AimAreaPressedEvent -= OnAimAreaPressed;
        AimArea.AimAreaDepressedEvent -= OnAimAreaDepressed;
    }


    void Update()
    {
        if (isTrace)
        {
            //It's works correctly when the HandAxis's Y-component rotated to -90 degree and the Hand's Y- and Z- components rotated to 90
            //You can try to simplify it :) I spent about 2 hours but no effect
            tempAim = handAxis.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            handAxis.rotation = Quaternion.LookRotation(tempAim);
        }

    }


    private void OnAimAreaPressed()
    {
        if (!isTrace)
        {
            isTrace = true;
        }
    }


    private void OnAimAreaDepressed()
    {
        if (isTrace)
        {
            isTrace = false;
        }
    }
}
