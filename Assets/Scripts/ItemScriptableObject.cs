using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Category
{
    None = 0,
    Sport = 1,
    Food = 2,
    Adventure = 3,
    Biggie = 4,
}

[CreateAssetMenu(fileName = "ItemScriptableObject", menuName = "ScriptableObjects/ItemScriptableObject", order = 1)]
public class ItemScriptableObject : ScriptableObject
{
    public Sprite sprite;
    public Category category;
    public float points;
}
