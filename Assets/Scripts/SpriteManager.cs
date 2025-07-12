using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Category parentCategory = Category.None;

    public List<ColorByCategory> colorByCategory;
    Dictionary<Category, Color> colorDict = new Dictionary<Category, Color>();

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        var parent = GetComponentInParent<Box>();
        if(parent != null )
        {
            parentCategory = parent.GetCategory();
        }

        foreach (var category in colorByCategory)
        {
            colorDict.Add(category.category, category.color);
        }

        AssignSprite();
    }

    public void AssignSprite()
    {
        if (spriteRenderer != null && parentCategory != Category.None)
        {
            spriteRenderer.color = colorDict[parentCategory];
        }

    }
}

[System.Serializable]
public class ColorByCategory
{
    public Category category;
    public Color color;
}
