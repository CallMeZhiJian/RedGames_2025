using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiggieEvent : MonoBehaviour
{
    public enum BiggieItem
    {
        Banana = 0,
        Braap = 1,
        FunnnyFace = 2,
    }

    [SerializeField] private float interval;
    private float timePassed = 0.0f;

    [SerializeField] private Animator animator;
    [SerializeField] private AnimationClip flyInAnimation;
    [SerializeField] private AnimationClip flyOutAnimation;

    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private ItemScriptableObject[] itemData;
    private int spawnNum = 0;

    [Header("Audio")]
    public AudioClip biggieLaughClip;
    public AudioClip bananaClip;

    private void Update()
    {
        timePassed += Time.deltaTime;

        if(timePassed > interval)
        {
            timePassed = 0.0f;

            TriggerEvent(BiggieItem.Banana);
        }
    }

    public void TriggerEvent(BiggieItem item)
    {
        AudioManager.Instance.PlaySFX(biggieLaughClip);

        spawnNum = (int)item;

        StartCoroutine(FlyIn());
    }

    public void SpawnItem()
    {
        if(itemData == null) return;
        if (itemPrefab == null) return;

        var currData = itemData[spawnNum];

        Item item = itemPrefab.GetComponent<Item>();
        if(item) item.data = currData;

        ItemMovement itemMovement = itemPrefab.GetComponent<ItemMovement>();
        if (itemMovement != null)
        {
            itemMovement.currentDirection = ItemMovement.Direction.Down;
        }

        Instantiate(itemPrefab, transform.position, Quaternion.identity);

        //SpawnObject.Instance.spawnObjects.Add(currData);

        StartCoroutine(FlyOut());
    }

    public IEnumerator FlyIn()
    {
        animator.SetTrigger("trigger_in");

        animator.ResetTrigger("trigger_idle");

        yield return new WaitForSeconds(flyInAnimation.length);

        SpawnItem();
    }

    public IEnumerator FlyOut()
    {
        animator.SetTrigger("trigger_out");

        animator.ResetTrigger("trigger_in");

        yield return new WaitForSeconds(flyOutAnimation.length);

        animator.SetTrigger("trigger_idle");

        animator.ResetTrigger("trigger_out");
    }
}
