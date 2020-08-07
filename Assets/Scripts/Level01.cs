using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level01 : MonoBehaviour
{
	public GameObject big_asteroid;

    // Start is called before the first frame update
    void Start()
    {
        // create player at the center of the screen
        // create random number of big asteroids (between 5 and 10)
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
