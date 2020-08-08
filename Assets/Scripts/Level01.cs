using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level01 : MonoBehaviour
{
	public GameObject big_asteroid, ship;
	public int total_small_asteroids = 0, total_asteroids = 0; 
	private Vector2 screenBounds;

    // Start is called before the first frame update
    void Start()
    {
        createShip();   
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        createBigAsteroids();
    }

    private void createBigAsteroids(){
    	int number = Random.Range(3, 7);
    	for (int i = 1; i <= number; i++){
    		// TODO: change position to random inside the scene
        	Vector3 position = new Vector3(Random.Range(-screenBounds.x, screenBounds.x), 0, Random.Range(-screenBounds.y, screenBounds.y));
            GameObject new_big_asteroid = Instantiate(big_asteroid, position, Quaternion.Euler(0,0,0));
            total_asteroids++;
        }
        Debug.Log("total_asteroids = " + total_asteroids);
    }

    private void createShip(){
    	GameObject new_ship = Instantiate(ship, Vector3.zero, transform.rotation);
    }

    private void reset_level(){
    	if(total_small_asteroids == 0 && total_asteroids == 0){
    		createBigAsteroids();
    	}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
