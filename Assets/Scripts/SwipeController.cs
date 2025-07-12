using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class SwipeController : MonoBehaviour
{
    [SerializeField] private float swipeSpeed;

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

    public void SnapToCenter()
    {

    }
}
