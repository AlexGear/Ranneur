using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    [SerializeField] private GameObject carPrefab;
    [SerializeField] private float spawnY = 0.28f;
    [SerializeField] private float spawnIntervalSeconds;
    [SerializeField] private float spawnDistance;
    [SerializeField] private float noVanishDistance;

    private float lastSpawnTime;

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

        float currentTime = Time.time;
        if (currentTime - lastSpawnTime > spawnIntervalSeconds) {
            SpawnCar();
            lastSpawnTime = currentTime;
        }
    }

    private void SpawnCar()
    {
        int lane = Random.Range(0, 4);
        Vector3 position = new Vector3(LaneManager.GetX(lane), spawnY, player.distance + spawnDistance);
        Quaternion rotation = (lane < 2) ? Quaternion.Euler(new Vector3(0, 180, 0)) : Quaternion.identity;
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
