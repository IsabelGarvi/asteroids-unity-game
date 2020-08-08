using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{

	private Rigidbody2D rb;	
	private float time_medium, time_small;
	private Level01 level_controller;

	public GameObject medium_asteroid, small_asteroid;

    void Start()
    {
    	level_controller = GameObject.Find("SceneManager").GetComponent<Level01>();
    	rb = GetComponent<Rigidbody2D>();
    	
        // Move the asteroid
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
        /* Move the asteroid towards the direction it's looking at.
        */
    	rb.AddForce(transform.up * 100f);
    }

    private void moveAsteroid(){
        /* Calculate time for medium and small asteroid and 
        change the direction after a certain amount of time 
        has passed.
        */
        time_medium += Time.deltaTime;
        time_small += Time.deltaTime; 	
    	switch(gameObject.name){
			case "Medium_Asteroid(Clone)":				
		    	if(time_medium >= 0.8f){
		    		changeAsteroidDirection(150f);
		        	time_medium = 0;
		    	}		
				break;
			case "Small_Asteroid(Clone)":
		    	if(time_small >= 0.2f){
		    		changeAsteroidDirection(300f);
		        	time_small = 0;
		    	}				
		    	break;
		}
    }

    private void changeRotation(){
        /* Change the rotation of the asteroid.
        */
    	transform.Rotate(new Vector3 (0, 0, Random.Range(0f, 359f)));
    }

    private void checkPosition(){
        /* If the position of the asteroid is outside the boundaries of
        the scene, the asteroid is transported to the other side of it.
        */
    	if(transform.position.x > level_controller.screenBounds.x){
    		transform.position = new Vector3(-level_controller.screenBounds.x, transform.position.y, transform.position.z);
    	}else if(transform.position.x < -level_controller.screenBounds.x){
    	    transform.position = new Vector3(level_controller.screenBounds.x, transform.position.y, transform.position.z);
    	}else if(transform.position.y > level_controller.screenBounds.y){
    		transform.position = new Vector3(transform.position.x, -level_controller.screenBounds.y, transform.position.z);
    	}else if(transform.position.y < -level_controller.screenBounds.y){
    		transform.position = new Vector3(transform.position.x, level_controller.screenBounds.y, transform.position.z);
    	}
    }

    private void changeAsteroidDirection(float speed){
    	Vector3 position = new Vector3(Random.Range(-level_controller.screenBounds.x, level_controller.screenBounds.x), Random.Range(-level_controller.screenBounds.y, level_controller.screenBounds.y), 0);
		float rotate = (Mathf.Atan2(position.y, position.x) * Mathf.Rad2Deg) - 90f;
		transform.rotation = Quaternion.Euler(0f, 0f, rotate);
        rb.velocity = Vector3.zero; // Reset velocity of the rigibody so the speed is not incremented. 
		rb.AddForce(transform.up * speed);
    }

    private void createMediumAsteroids(){
        /* Instantiate 2 medium asteroids on screen at the position of the 
        big asteroid.
        */
    	for(int i = 0; i < 2; i++){
    		Vector3 position = new Vector3(Random.Range(transform.position.x-0.5f, transform.position.x-0.5f), Random.Range(transform.position.y-0.5f, transform.position.y-0.5f), transform.position.z);
    		GameObject new_medium_asteroid = Instantiate(medium_asteroid, position, Quaternion.Euler(0,0,0));
    		level_controller.total_asteroids++;
    	}
	}

    private void createSmallAsteroids(){
        /* Instantiate 2 small asteroids on screen at the position of the 
        medium asteroid.
        */
    	for(int i = 0; i < 2; i++){
    		Vector3 position = new Vector3(Random.Range(transform.position.x-0.5f, transform.position.x-0.5f), Random.Range(transform.position.y-0.5f, transform.position.y-0.5f), transform.position.z);
    		GameObject new_small_asteroid = Instantiate(small_asteroid, position, Quaternion.Euler(0,0,0));
    		level_controller.total_asteroids++;
    	}
    }

    void Update()
    {         
        checkPosition();
        moveAsteroid();
    }

    void OnTriggerEnter2D(Collider2D other){
        /* If the asteroid is hit a projectile, it is destroyed and so is 
        the projectile. 
        If it's a big asteroid, medium size ones are created, 
        if it's a medium size, small size asteroids are created. 
        Decrease variable of total_asteroids to know when a level is clear.
        */
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
                    level_controller.total_asteroids--;
					Destroy(gameObject);
					break;
			}			
		}
	}
}
