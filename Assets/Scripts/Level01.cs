using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level01 : MonoBehaviour
{
	public GameObject big_asteroid, ship;
	private Vector2 screenBounds;

    // Start is called before the first frame update
    void Start()
    {
        createShip();   
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        int big_asteroids_number = Random.Range(3, 7);
        createBigAsteroids(big_asteroids_number);
    }

    private void createBigAsteroids(int number){
    	for (int i = 1; i <= number; i++){
    		// TODO: change position to random inside the scene
        	Vector3 position = new Vector3(Random.Range(-screenBounds.x, screenBounds.x), 0, Random.Range(-screenBounds.y, screenBounds.y));
            GameObject new_big_asteroid = Instantiate(big_asteroid, position, Quaternion.Euler(0,0,0));
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
