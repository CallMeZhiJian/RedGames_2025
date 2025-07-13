using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMovement : MonoBehaviour
{
    public enum Direction
    {
        Down,
        LeftDown,
        RightDown
    }

    public float moveSpeed = 5f;
    public Direction currentDirection;
    public Vector3 myPosition;

    void Update()
    {
        myPosition = transform.position;

        switch (currentDirection)
        {
            case Direction.Down:
                transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);
                break;
            case Direction.LeftDown:
                transform.Translate(new Vector2(-1, -1).normalized * moveSpeed * Time.deltaTime);
                break;
            case Direction.RightDown:
                transform.Translate(new Vector2(1, -1).normalized * moveSpeed * Time.deltaTime);
                break;
            default:
                transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);
                break;
        }

        if (Mathf.Abs(myPosition.x) < 0.05f)
        {
            currentDirection = Direction.Down;
        }
    }
}
