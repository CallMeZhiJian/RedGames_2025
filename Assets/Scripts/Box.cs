using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] private Category category;
    [SerializeField] private ParticleSystem scoreVFX;

    private SpriteManager spriteManager;
    private Animator animator;

    [SerializeField] private int stage_2_Count = 5;
    [SerializeField] private int stage_3_Count = 10;

    private void Start()
    {
        spriteManager = GetComponentInChildren<SpriteManager>();
        animator = GetComponentInChildren<Animator>();

        InvokeRepeating("TriggerPhases", 1, 1);
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

    //public void AddCounter()
    //{
    //    counter++;

    //    if (counter >= stage_3_Count)
    //    {
    //        //spriteManager.AssignSprite(2);
    //        animator.SetTrigger("phase_3");
    //    }
    //    else if (counter >= stage_2_Count)
    //    {
    //        //spriteManager.AssignSprite(1);
    //        animator.SetTrigger("phase_2");
    //    }
    //}

    public void TriggerPhases()
    {
        int counter = 0;

        switch (category)
        {
            case Category.Sport:
                counter = BoxHolder.oguCounter;
                break;
            case Category.Food:
                counter = BoxHolder.bamCounter;
                break;
            case Category.Adventure:
                counter = BoxHolder.tappyCounter;
                break;
            case Category.Biggie:
                counter = BoxHolder.biggieCounter;
                break;
        }

        if (counter >= stage_3_Count)
        {
            //spriteManager.AssignSprite(2);
            animator.SetTrigger("phase_3");
            CancelInvoke("TriggerPhases");
        }
        else if (counter >= stage_2_Count)
        {
            //spriteManager.AssignSprite(1);
            animator.SetTrigger("phase_2");
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
                        //AddCounter();
                        BoxHolder.AddCounter(category);
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
                        //AddCounter();
                        BoxHolder.AddCounter(category);
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
