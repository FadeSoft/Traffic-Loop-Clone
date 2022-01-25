using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;

    public Transform spawnPositions;
    public Transform splines;

    private void Start()
    {
        StartCoroutine(SpawnOtherCars("Car2"));
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) SpawnCar("Car1");
    }

    private void SpawnCar(string carName)
    {
        GameObject car = Instantiate(Resources.Load<GameObject>("Cars/" + carName));
        Car carSc = car.GetComponent<Car>();
        car.transform.position = spawnPositions.GetChild(carSc.carIndex).position;
        carSc.spline = splines.GetChild(0).GetComponent<SplineComputer>();

    }

    IEnumerator SpawnOtherCars(string carName)
    {
        yield return new WaitForSeconds(.6f);

        GameObject car = Instantiate(Resources.Load<GameObject>("Cars/" + carName));
        Car carSc = car.GetComponent<Car>();
        car.transform.position = spawnPositions.GetChild(carSc.carIndex).position;
        carSc.spline = splines.GetChild(1).GetComponent<SplineComputer>();

        yield return new WaitForSeconds(1.5f);
        StartCoroutine(SpawnOtherCars("Car2"));

    }
}
