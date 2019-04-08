using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner<T> : MonoBehaviour where T : Component
{
    [SerializeField] protected GameObject prefab;
    [SerializeField] protected float spawnY = 0.28f;
    [SerializeField] protected float spawnIntervalMin;
    [SerializeField] protected float spawnIntervalMax;
    [SerializeField] protected float spawnDistance;
    [SerializeField] protected float noVanishDistance;

    protected PlayerController player { get; private set; }
    protected float spawnTimer { get; private set; }

    private List<T> activeObjs = new List<T>();
    private List<T> disabledObjs = new List<T>();

    protected virtual float GetSpawnInterval() => Random.Range(spawnIntervalMin, spawnIntervalMax);

    protected virtual void Awake()
    {
        player = FindObjectOfType<PlayerController>();
    }

    protected virtual void Update()
    {
        DisableGoneObjects();

        spawnTimer -= Time.fixedDeltaTime;
        if (spawnTimer <= 0)
        {
            Spawn();
            spawnTimer = GetSpawnInterval();
        }
    }

    protected abstract void Spawn();

    protected T TakeObject()
    {
        T obj;
        if (disabledObjs.Count == 0)
        {
            obj = Instantiate(prefab, transform).GetComponent<T>();
        }
        else
        {
            obj = disabledObjs[0];
            obj.gameObject.SetActive(true);
            disabledObjs.Remove(obj);
        }
        activeObjs.Add(obj);
        return obj;
    }

    protected float GetSpawnZ() => player.distance + spawnDistance;
    
    private void DisableGoneObjects()
    {
        var removeList = new List<T>();
        foreach (T obj in activeObjs)
        {
            if (obj.transform.position.z + noVanishDistance < player.distance)
            {
                obj.gameObject.SetActive(false);
                disabledObjs.Add(obj);
                removeList.Add(obj);
            }
        }
        foreach (T obj in removeList)
        {
            activeObjs.Remove(obj);
        }
    }
}
