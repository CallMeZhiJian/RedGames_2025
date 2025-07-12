using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SwipeController : MonoBehaviour
{
    [SerializeField] private float swipeSpeed = 10.0f;
    [SerializeField] private float snapSpeed = 5.0f;

    public static Vector3 snapPosition = Vector3.zero;
    public static Vector3 newPosition = Vector3.zero;

    public Box[] childBoxes;

    void Update()
    {
#if PLATFORM_WEBGL
        if (Input.anyKey)
        {
            var pos = transform.position;
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                pos.x -= swipeSpeed * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                pos.x += swipeSpeed * Time.deltaTime;
            }

            transform.position = pos;
        }
#else
        if (Input.touchCount > 0)
        {
            TouchScreenInput();
        }
#endif
        else
        {
            SnapToCenter();
            transform.position = Vector3.MoveTowards(transform.position, snapPosition, snapSpeed);
        }
    }

    private Vector3 startTouchPos;
    private float offset;

    public void TouchScreenInput()
    {
        Touch touch = Input.GetTouch(0);
        var worldTouchPos = Camera.main.ScreenToWorldPoint(touch.position);

        if (touch.phase == TouchPhase.Began)
        {
            startTouchPos = worldTouchPos;
            offset = worldTouchPos.x - transform.position.x;
        }

        if (touch.phase == TouchPhase.Moved)
        {
            float movePos = worldTouchPos.x - startTouchPos.x;

            float newXPosition = 0.0f;
            float absOffset = Mathf.Abs(offset);

            if (offset < 0)
            {
                newXPosition = worldTouchPos.x + absOffset;
            }
            else
            {
                newXPosition = worldTouchPos.x - absOffset;
            }

            newPosition = new Vector3(newXPosition, transform.position.y, transform.position.z);
            transform.position = newPosition;//Vector3.MoveTowards(transform.position, newPosition, snapSpeed);
        }
    }

    public void SnapToCenter()
    {
        float distanceToSnap = GetClosestChildDistance();

        var instance = TransformHolder.Instance;
        Vector3 snapPos = SwipeController.snapPosition;

        // Move toward left if positive, right if negative
        Vector3 newPos = instance.GetBoxGrandParentPos() - new Vector3(distanceToSnap, 0.0f);
        SwipeController.snapPosition = newPos;
    }

    public float GetClosestChildDistance()
    {
        if (childBoxes != null)
        {
            float nearestDistance = 100.0f;
            bool isPositiveValue = false;

            // Comparing 2 boxes in one round
            for (int i = 0; i < childBoxes.Length; i++)
            {
                float distance = childBoxes[i].DistanceToCenter();
                float absDistance = Mathf.Abs(distance);

                if (absDistance < nearestDistance)
                {
                    if (distance > 0)
                    {
                        isPositiveValue = true;
                    }
                    else
                    {
                        isPositiveValue = false;
                    }

                    nearestDistance = absDistance;
                }
            }

            return isPositiveValue ? nearestDistance : -nearestDistance;
        }

        return 0.0f;
    }
}
