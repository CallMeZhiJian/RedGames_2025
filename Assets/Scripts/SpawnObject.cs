using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    public static SpawnObject Instance;

    public enum Direction
    {
        Down,
        LeftDown,
        RightDown
    }
    [System.Serializable]
    public struct SpawnerTransform
    {
        public Transform Parent;
        public Direction Directions;

        public SpawnerTransform(Transform parent, Direction directions)
        {
            Parent = parent;
            Directions = directions;
        }
    }

    public Direction spawnDirection;
    public float spawnRate = 1f;
    public GameObject itemPrefab;
    public List<ItemScriptableObject> spawnObjects;
    public int cachedRate = 5;
    public List<SpawnerTransform> transforms;
    public float initialDelay = 3f;

    private float timer = 0f;
    private float delayTimer = 0f;
    private bool delayFinished = false;
    private int transformIndex = 0;

    private Dictionary<ItemScriptableObject, int> cachedObjectList = new Dictionary<ItemScriptableObject, int>();

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (!delayFinished)
        {
            delayTimer += Time.deltaTime;
            if (delayTimer >= initialDelay)
            {
                delayFinished = true;
            }
            return;
        }

        timer += Time.deltaTime;

        if (timer >= spawnRate)
        {
            List<ItemScriptableObject> availableObjects = spawnObjects.Where(obj => !cachedObjectList.ContainsKey(obj)).ToList();

            if (availableObjects.Count > 0)
            {
                int randNum = UnityEngine.Random.Range(0, availableObjects.Count);
                ItemScriptableObject objectToSpawn = availableObjects[randNum];
                SpawnAtSelf(objectToSpawn);
            }

            IncreaseCachedObjectRound();
            if (transformIndex >= (transforms.Count - 1))
            {
                transformIndex = 0;
            }
            else
            {
                transformIndex++;
            }
            timer = 0f;
        }
    }

    void SpawnAtSelf(ItemScriptableObject itemData)
    {
        AddObjectIntoList(itemData);
        ItemMovement itemMovement = itemPrefab.GetComponent<ItemMovement>();
        if (itemMovement != null)
        {
            itemMovement.currentDirection = (ItemMovement.Direction)transforms[transformIndex].Directions;
        }
        Item item = itemPrefab.GetComponent<Item>();
        if (item != null)
        {
            item.data = itemData;
        }

        Instantiate(itemPrefab, transforms[transformIndex].Parent.position, Quaternion.identity);
    }

    void AddObjectIntoList(ItemScriptableObject item)
    {
        if (!cachedObjectList.ContainsKey(item))
        {
            cachedObjectList[item] = 0;
        }
    }

    void IncreaseCachedObjectRound()
    {
        List<ItemScriptableObject> toRemove = new List<ItemScriptableObject>();

        foreach (var pair in cachedObjectList.ToList())
        {
            ItemScriptableObject obj = pair.Key;
            int round = pair.Value + 1;

            if (round >= cachedRate)
            {
                toRemove.Add(obj);
            }
            else
            {
                cachedObjectList[obj] = round;
            }
        }

        foreach (var obj in toRemove)
        {
            cachedObjectList.Remove(obj);
        }
    }
}