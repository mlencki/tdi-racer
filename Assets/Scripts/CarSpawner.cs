using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner: MonoBehaviour
{
    public GameObject[] lanes;
    public List<GameObject> availableCars;

    protected GameStateManager gameStateManager;

    public void Start ()
    {
        this.gameStateManager = GameObject.Find("GameStateManager").GetComponent<GameStateManager>();
        this.lanes = GameObject.FindGameObjectsWithTag("Lane");
        this.LoadCars();

        StartCoroutine("SpawnCar");
    }
	
    public void Update ()
    {
        if(this.gameStateManager.gameOver) {
            Destroy(this);
        }
    }

    protected IEnumerator SpawnCar()
    {
        while(!this.gameStateManager.gameOver) {
            InstantiateRandomCar();
            yield return new WaitForSeconds(8f / (this.gameStateManager.gameSpeed * this.gameStateManager.gameSpeed));
        }
    }

    protected void LoadCars()
    {
        Object[] carList = Resources.LoadAll("Cars", typeof(GameObject));

        foreach (GameObject carResource in carList) {
            GameObject car = (GameObject)carResource;
            this.availableCars.Add(car);
        }
    }

    protected void InstantiateRandomCar()
    {
        int randomLane = Random.Range(0, 4);
        Vector3 lanePosition = new Vector3(this.lanes[randomLane].transform.position.x, -10, 700);

        GameObject randomCar = this.availableCars[Random.Range(0, availableCars.Count)];

        GameObject newCar = Instantiate(randomCar, lanePosition, Quaternion.identity);

        if (randomLane < 2) {
            newCar.transform.rotation = new Quaternion(0f, 180f, 0f, 0f);
        }
    }
}
