using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] private Category category;

    public Category GetCategory() { return category; }

    public float DistanceToCenter()
    {
        float distance = 0.0f;
        float currentPosition = transform.position.x;
        float centerPosition = TransformHolder.Instance.GetCenterPos().x;

        if(currentPosition > centerPosition)
        {
            distance = currentPosition - centerPosition;
        }
        else
        {
            distance = currentPosition + centerPosition;
        }
            

        return distance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            Item item = collision.gameObject.GetComponent<Item>();

            if (item != null)
            {
                if (category != Category.Biggie)
                {
                    if (item.category == category)
                    {
                        ScoreManager.Instance.OnCorrectSort(5);
                    }
                    else
                    {
                        if(item.category == Category.Biggie)
                        {
                            ScoreManager.Instance.LoseLife();
                        }
                        else
                        {
                            ScoreManager.Instance.OnWrongSort(3);
                        }

                        Handheld.Vibrate();
                    }
                }
                else
                {
                    if (item.category == category)
                    {
                        ScoreManager.Instance.OnCorrectSort(10);
                    }
                }

                Destroy(collision.gameObject);
            }
        }
    }
}
