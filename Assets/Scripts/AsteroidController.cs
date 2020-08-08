using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{

	private Rigidbody2D rb;	
	private Vector2 screenBounds;
	private float time;
	private Level01 level_controller;

	public GameObject medium_asteroid, small_asteroid;

    void Start()
    {
    	screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    	level_controller = GameObject.Find("SceneManager").GetComponent<Level01>();
    	rb = GetComponent<Rigidbody2D>();
    	// changeRotation();
    	switch(gameObject.name){
    		case "Big_Asteroid(Clone)":
    			changeRotation();
    			moveBigAsteroid();
    			break;
			case "Medium_Asteroid(Clone)":
        		changeAsteroidDirection(150f);
				break;
			case "Small_Asteroid(Clone)":
				changeAsteroidDirection(300f);
				break;
		}         
    }

    private void moveBigAsteroid(){
    	rb.AddForce(transform.up * 100f);
    }

    private void moveAsteroid(){
        time += Time.deltaTime;    	
    	switch(gameObject.name){
			case "Medium_Asteroid(Clone)":				
		    	if(time >= 1f){
		    		changeAsteroidDirection(0f);
		        	time = 0;
		    	}		
				break;
			case "Small_Asteroid(Clone)":
		    	if(time >= 0.5f){
		    		changeAsteroidDirection(0f);
		        	time = 0;
		    	}				
		    	break;
		}
    }

    private void changeRotation(){
    	transform.Rotate(new Vector3 (0, 0, Random.Range(0f, 359f)));
    }

    private void checkPosition(){
    	if(transform.position.x > screenBounds.x){
    		transform.position = new Vector3(-screenBounds.x, transform.position.y, transform.position.z);
    	}else if(transform.position.x < -screenBounds.x){
    	    transform.position = new Vector3(screenBounds.x, transform.position.y, transform.position.z);
    	}else if(transform.position.y > screenBounds.y){
    		transform.position = new Vector3(transform.position.x, -screenBounds.y, transform.position.z);
    	}else if(transform.position.y < -screenBounds.y){
    		transform.position = new Vector3(transform.position.x, screenBounds.y, transform.position.z);
    	}
    }

    private void changeAsteroidDirection(float speed){
    	Vector3 position = new Vector3(Random.Range(-screenBounds.x, screenBounds.x), Random.Range(-screenBounds.y, screenBounds.y), 0);
		float rotate = (Mathf.Atan2(position.y, position.x) * Mathf.Rad2Deg) - 90f;
		transform.rotation = Quaternion.Euler(0f, 0f, rotate);
		rb.AddForce(transform.up * speed);
    }

    private void createMediumAsteroids(){
    	for(int i = 0; i < 2; i++){
    		Vector3 position = new Vector3(Random.Range(transform.position.x-0.5f, transform.position.x-0.5f), Random.Range(transform.position.y-0.5f, transform.position.y-0.5f), transform.position.z);
    		GameObject new_medium_asteroid = Instantiate(medium_asteroid, position, Quaternion.Euler(0,0,0));
    		level_controller.total_asteroids++;
    	}
    	Debug.Log("total_asteroids = " + level_controller.total_asteroids);
	}

    private void createSmallAsteroids(){
    	for(int i = 0; i < 2; i++){
    		Vector3 position = new Vector3(Random.Range(transform.position.x-0.5f, transform.position.x-0.5f), Random.Range(transform.position.y-0.5f, transform.position.y-0.5f), transform.position.z);
    		GameObject new_small_asteroid = Instantiate(small_asteroid, position, Quaternion.Euler(0,0,0));
    		level_controller.total_small_asteroids++;
    		level_controller.total_asteroids++;
    	}
    	Debug.Log("total_asteroids = " + level_controller.total_asteroids);
    }

    // Update is called once per frame
    void Update()
    {         
        checkPosition();
        moveAsteroid();
    }

    void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Projectile"){
			Destroy(other.gameObject);
			switch(gameObject.name){
				case "Big_Asteroid(Clone)":
					level_controller.total_asteroids--;
					createMediumAsteroids();
					Destroy(gameObject);
					break;
				case "Medium_Asteroid(Clone)":
					level_controller.total_asteroids--;
					createSmallAsteroids();
					Destroy(gameObject);
					break;
				case "Small_Asteroid(Clone)":
					level_controller.total_small_asteroids--;
					Destroy(gameObject);
					break;
			}			
		}
	}
}
