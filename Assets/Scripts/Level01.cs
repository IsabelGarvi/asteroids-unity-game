using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level01 : MonoBehaviour
{
	public GameObject big_asteroid, ship;

    // Start is called before the first frame update
    void Start()
    {
        // create player at the center of the screen
        createShip();
        int big_asteroids_number = Random.Range(5, 10);
        createBigAsteroids(big_asteroids_number);
        // initialise GUI
    }

    private void createBigAsteroids(int number){
    	for (int i = 1; i <= number; i++)
        {
        	Vector3 position = new Vector3(Random.Range(-10.0f, 10.0f), 0, Random.Range(-10.0f, 10.0f));
            GameObject new_big_asteroid = Instantiate(big_asteroid, position, transform.rotation);
        }
    }

    private void createShip(){
    	GameObject new_ship = Instantiate(ship, Vector3.zero, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
