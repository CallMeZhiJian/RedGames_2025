using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] private Category category;
    [SerializeField] private ParticleSystem scoreVFX;

    [SerializeField] private int stage_2_Count;
    [SerializeField] private int stage_3_Count;
    private int counter;

    private SpriteManager spriteManager;

    private void Start()
    {
        spriteManager = GetComponentInChildren<SpriteManager>();
        counter = 0;
    }

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

    public void AddCounter()
    {
        counter++;

        if (counter >= stage_3_Count)
        {
            spriteManager.AssignSprite(2);
        }
        else if (counter >= stage_2_Count)
        {
            spriteManager.AssignSprite(1);
        }
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
                        scoreVFX.Play();
                        AddCounter();
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
                        scoreVFX.Play();
                        AddCounter();
                    }
                    else
                    {
                        ScoreManager.Instance.OnWrongSort(3);
                        Handheld.Vibrate();
                    }
                }

                Destroy(collision.gameObject); 
            }
        }
    }
}
