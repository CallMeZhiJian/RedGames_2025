using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxHolder : MonoBehaviour
{
    public Box[] childBoxes;

    public BoxHolder leftGroup;
    public BoxHolder rightGroup;

    private void Start()
    {
        childBoxes = GetComponentsInChildren<Box>();
    }

    private void Update()
    {
        if(Input.anyKey || Input.touchCount > 0)
        {
            TeleportForLoop(transform.position);
        }
    }

    const float BOX_HOLDER_GAP = 8.0f;

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
            //SwipeController.SetRighBoxtHolder(this);

            Vector3 boxHolderPos = transform.position;
            float leftGroup_X = leftGroup.transform.position.x;
            var boxGap = boxHolderPos.x - leftGroup_X;

            if (boxGap != BOX_HOLDER_GAP)
            {
               transform.position = new Vector3(leftGroup_X + BOX_HOLDER_GAP, boxHolderPos.y, boxHolderPos.z);
            }
        }
        else if (myPosition.x > rightTelePos.x)
        {
            transform.position = leftStartPos;
            //SwipeController.SetLeftBoxHolder(this);

            Vector3 boxHolderPos = transform.position;
            float rightGroup_X = rightGroup.transform.position.x;
            var boxGap = rightGroup_X - boxHolderPos.x;

            if (boxGap != BOX_HOLDER_GAP)
            {
                transform.position = new Vector3(rightGroup_X - BOX_HOLDER_GAP, boxHolderPos.y, boxHolderPos.z);
            }
        }
    }

}
