using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Category parentCategory = Category.None;

    public int spriteToUse = 0;

    public List<SpriteByCategory> spriteByCategory;
    Dictionary<Category, List<Sprite>> spriteDict = new Dictionary<Category, List<Sprite>>();

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        var parent = GetComponentInParent<Box>();
        if(parent != null )
        {
            parentCategory = parent.GetCategory();
        }

        foreach (var category in spriteByCategory)
        {
            spriteDict.Add(category.category, category.sprite);
        }

        AssignSprite(spriteToUse);
    }

    public void AssignSprite(int index)
    {
        var sprites = spriteDict[parentCategory];
        if (index > sprites.Count - 1) return;

        if (spriteRenderer != null && parentCategory != Category.None)
        {  
            if (sprites[index] != spriteRenderer.sprite)
            {
                spriteRenderer.sprite = sprites[index];
            }
        }

    }
}

[System.Serializable]
public class SpriteByCategory
{
    public Category category;
    public List<Sprite> sprite;
}
