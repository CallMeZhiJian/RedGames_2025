using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemScriptableObject data;
    public Category category;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        Init();
    }

    private void OnEnable()
    {
        Init();
    }

    private void Init()
    {
        if (data == null) return;

        if(spriteRenderer != null)
            spriteRenderer.sprite = data.sprite;

        string tag = string.Empty;

        switch (data.category)
        {
            case Category.None:
                break;
            case Category.Sport:
                tag = "Sport";
                break;
            case Category.Food:
                tag = "Food";
                break;
            case Category.Adventure:
                tag = "Adventure";
                break;
            case Category.Biggie:
                tag = "Biggie";
                break;
        }
        gameObject.tag = tag;

        category = data.category;
    }
}
