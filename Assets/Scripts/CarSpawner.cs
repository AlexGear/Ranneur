using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : Spawner<Car>
{
    private enum Direction { ToSun, ToNihility }

    [SerializeField] private int[] lanes = new int[0];
    [SerializeField] private Direction direction;
    [SerializeField] private bool allowDoubleSpawn = false;

    protected override void Spawn()
    {
        int lane = GetRandomLane();
        SpawnCar(lane);
        if (lanes.Length > 1 && allowDoubleSpawn && Random.value < 0.15f)
        {
            int otherLane = GetRandomLaneExcept(lane);
            SpawnCar(otherLane);
        }
    }

    private int GetRandomLane() => lanes[Random.Range(0, lanes.Length)];

    private int GetRandomLaneExcept(int lane)
    {
        int otherLane;
        do
            otherLane = GetRandomLane();
        while (otherLane == lane);
        return otherLane;
    }

    private void SpawnCar(int lane)
    {
        float z = GetSpawnZ() + Random.Range(-2, 2);
        Vector3 position = new Vector3(LaneManager.GetX(lane), spawnY, z);
        Quaternion rotation = direction == Direction.ToNihility
                                ? Quaternion.Euler(new Vector3(0, 180, 0))
                                : Quaternion.identity;
        Car car = TakeObject();
        car.transform.position = position;
        car.transform.rotation = rotation;
    }
}
