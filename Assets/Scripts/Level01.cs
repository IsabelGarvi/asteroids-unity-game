using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level01 : MonoBehaviour
{
	public GameObject big_asteroid, ship;
	public int total_asteroids = 0; 
	public Vector2 screenBounds;

    // Start is called before the first frame update
    void Start()
    {
    	screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        createShip();   
        createBigAsteroids();
    }

    private void createBigAsteroids(){
    	/* Instantiate a random number of big asteroids inside the screen. 
    	*/
    	int number = Random.Range(3, 7);
    	for (int i = 1; i <= number; i++){
        	Vector3 position = new Vector3(Random.Range(-screenBounds.x, screenBounds.x), 0, Random.Range(-screenBounds.y, screenBounds.y));
            GameObject new_big_asteroid = Instantiate(big_asteroid, position, Quaternion.Euler(0,0,0));
            total_asteroids++;
        }
    }

    private void createShip(){
    	/* Instantiate the ship in the center of the scene.
    	*/
    	GameObject new_ship = Instantiate(ship, Vector3.zero, transform.rotation);
    }

    private void reset_level(){
    	/* If there are no more asteroids, the level is clear and 
    	more big asteroids are created.
    	*/ 
    	if(total_asteroids == 0){
    		createBigAsteroids();
    	}
    }

    void Update()
    {
        reset_level();
    }
}
