using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SwipeController : MonoBehaviour
{
    [SerializeField] private float swipeSpeed = 10.0f;
    [SerializeField] private float snapSpeed = 5.0f;

    public static Vector3 snapPosition = Vector3.zero;

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
            transform.position = Vector3.MoveTowards(transform.position, snapPosition, snapSpeed * Time.deltaTime);
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

            transform.position = new Vector3(newXPosition, transform.position.y, transform.position.z);
        }
    }
}
