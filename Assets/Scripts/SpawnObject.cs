using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;

public class SpawnObject : MonoBehaviour
{
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
    public List<GameObject> spawnObjects;
    public int cachedRate = 5;
    public List<SpawnerTransform> transforms;
    public float initialDelay = 3f;

    private float timer = 0f;
    private float delayTimer = 0f;
    private bool delayFinished = false;
    private int transformIndex = 0;

    private Dictionary<GameObject, int> cachedObjectList = new Dictionary<GameObject, int>();

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
            List<GameObject> availableObjects = spawnObjects.Where(obj => !cachedObjectList.ContainsKey(obj)).ToList();

            if (availableObjects.Count > 0)
            {
                int randNum = UnityEngine.Random.Range(0, availableObjects.Count);
                GameObject objectToSpawn = availableObjects[randNum];
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

    void SpawnAtSelf(GameObject item)
    {
        AddObjectIntoList(item);
        MovingObject movingObject = item.GetComponent<MovingObject>();
        if (movingObject != null)
        {
            movingObject.currentDirection = (MovingObject.Direction)transforms[transformIndex].Directions;
        }
        Instantiate(item, transforms[transformIndex].Parent.position, Quaternion.identity);
    }

    void AddObjectIntoList(GameObject item)
    {
        if (!cachedObjectList.ContainsKey(item))
        {
            cachedObjectList[item] = 0;
        }
    }

    void IncreaseCachedObjectRound()
    {
        List<GameObject> toRemove = new List<GameObject>();

        foreach (var pair in cachedObjectList.ToList())
        {
            GameObject obj = pair.Key;
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