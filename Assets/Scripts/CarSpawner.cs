using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    private enum Direction { ToSun, ToNihility }

    [SerializeField] private GameObject carPrefab;
    [SerializeField] private int[] lanes = new int[0];
    [SerializeField] private Direction direction;
    [SerializeField] private bool allowDoubleSpawn = false;
    [SerializeField] private float spawnY = 0.28f;
    [SerializeField] private float spawnIntervalMin;
    [SerializeField] private float spawnIntervalMax;
    [SerializeField] private float spawnDistance;
    [SerializeField] private float noVanishDistance;

    private float spawnTimer;

    private PlayerController player;
    private List<Car> activeCars = new List<Car>();
    private List<Car> disabledCars = new List<Car>();

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
    }

    private void FixedUpdate()
    {
        DisableGoneCars();

        spawnTimer -= Time.fixedDeltaTime;
        if (spawnTimer <= 0) {
            SpawnCar();
            spawnTimer = Random.Range(spawnIntervalMin, spawnIntervalMax);
        }
    }

    private void SpawnCar()
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
        float z = player.distance + spawnDistance + Random.Range(-2, 2);
        Vector3 position = new Vector3(LaneManager.GetX(lane), spawnY, z);
        Quaternion rotation = direction == Direction.ToNihility
                                ? Quaternion.Euler(new Vector3(0, 180, 0))
                                : Quaternion.identity;
        Car car = TakeCar();
        car.transform.position = position;
        car.transform.rotation = rotation;
    }

    private Car TakeCar()
    {
        Car car;
        if (disabledCars.Count == 0)
        {
            car = Instantiate(carPrefab, transform).GetComponent<Car>();
        }
        else
        {
            car = disabledCars[0];
            car.gameObject.SetActive(true);
            disabledCars.Remove(car);
        }
        activeCars.Add(car);
        return car;
    }

    private void DisableGoneCars()
    {
        var removeList = new List<Car>();
        foreach (Car car in activeCars)
        {
            if (car.transform.position.z + noVanishDistance < player.distance)
            {
                car.gameObject.SetActive(false);
                disabledCars.Add(car);
                removeList.Add(car);
            }
        }
        foreach (Car car in removeList)
        {
            activeCars.Remove(car);
        }
    }
}
