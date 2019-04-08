using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : Spawner<Coin>
{
    [SerializeField] uint seriesSize = 8;
    [SerializeField] float intervalZ = 10;

    protected override void Spawn()
    {
        float x = LaneManager.GetX(Random.Range(0, 4));
        float z = GetSpawnZ();
        for(int i = 0; i < seriesSize; i++)
        {
            Coin coin = TakeObject();
            coin.transform.position = new Vector3(x, spawnY, z);
            coin.transform.rotation = Quaternion.identity;
            z += intervalZ;
        }
    }
}
