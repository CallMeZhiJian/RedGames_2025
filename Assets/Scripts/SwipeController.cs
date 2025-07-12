using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class SwipeController : MonoBehaviour
{
    [SerializeField] private float swipeSpeed;
    [SerializeField] private float snapSpeed = 5.0f;

    public Box[] childBoxes;

    [SerializeField] private SwipeController leftGroup;
    [SerializeField] private SwipeController rightGroup;

    private void Start()
    {
        childBoxes = GetComponentsInChildren<Box>();
    }

    void Update()
    {
        if(Input.anyKey)
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

            TeleportForLoop(pos);
        }
        else
        {
            SnapToCenter();
            TransformHolder.Instance.SnapParent(snapSpeed);
        }
    }

    public void TeleportForLoop(Vector3 myPosition)
    {
        var instance = TransformHolder.Instance;

        Vector3 rightTelePos = instance.GetRightTeleportPos();
        Vector3 leftTelePos = instance.GetLeftTeleportPos();
        Vector3 rightStartPos = instance.GetRightStartPos();
        Vector3 leftStartPos = instance.GetLeftStartPos();

        if (rightTelePos == Vector3.zero) return;
        if (leftTelePos == Vector3.zero) return;

        if (myPosition.x < leftTelePos.x)
        {
            transform.position = rightStartPos;
        }
        else if (myPosition.x > rightTelePos.x)
        {
            transform.position = leftStartPos;
        }
    }

    public float DistanceToCenter()
    {
        var distance = Mathf.Abs(transform.position.x) - TransformHolder.Instance.GetCenterPos().x;

        return distance;
    }

    public bool SnapToCenter()
    {
        float leftGroupDistance = leftGroup.DistanceToCenter();
        float rightGourpDistance = rightGroup.DistanceToCenter();
        float myDistance = DistanceToCenter();

        // Left Group is closer
        if (leftGroupDistance < myDistance)
        {
            return false;
        }
        // Right Group is closer
        else if (rightGourpDistance < myDistance)
        {
            return false;
        }
        // I am closer
        else
        {
            float distanceToSnap = GetClosestChildDistance();

            var instance = TransformHolder.Instance;
            Vector3 snapPos = instance.snapPosition;

            // Move toward left if positive, right if negative
            Vector3 newPos = instance.GetBoxGrandParentPos() - new Vector3(distanceToSnap, 0.0f);
            instance.snapPosition = newPos;

            return true;
        }
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

                if(absDistance < nearestDistance)
                {
                    if(distance > 0)
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
