using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] private Category category;

    public Category GetCategory() { return category; }

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }
}
