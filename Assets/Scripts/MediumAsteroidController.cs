using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediumAsteroidController : MonoBehaviour
{
	private Rigidbody2D rb;
	private float time;
	private Vector2 screenBounds;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        changeRotation();
        moveAsteroid();        
    }

    private void moveAsteroid(){
    	rb.AddForce(transform.up * 300f);
    }

    private void changeRotation(){
    	transform.Rotate(new Vector3 (0, 0, Random.Range(0f, 359f)));
    }

    private void checkPosition(){
    	if(transform.position.x > screenBounds.x || transform.position.x < -screenBounds.x){
    		transform.position = new Vector2(-transform.position.x, transform.position.y);
    	}else if(transform.position.y > screenBounds.y || transform.position.y < -screenBounds.y){
    		transform.position = new Vector2(transform.position.x, -transform.position.y);
    	}
    }

    void Update()
    {
    	checkPosition();
    	
    		
    }

    void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Projectile"){
			//createMediumAsteroids();
			Destroy(gameObject);
		}
	}
}
