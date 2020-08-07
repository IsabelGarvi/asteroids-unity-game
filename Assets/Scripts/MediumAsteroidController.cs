using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediumAsteroidController : MonoBehaviour
{
	private Rigidbody2D rb;
	private float time;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        changeRotation();
        moveAsteroid();        
    }

    private void moveAsteroid(){
    	rb.AddForce(transform.up * 300f);
    }

    private void changeRotation(){
    	transform.Rotate(new Vector3 (0, 0, Random.Range(0f, 359f)));
    }

    void Update()
    {
    	time += Time.deltaTime;
    	if(time >= 1){
    		changeRotation();
        	moveAsteroid();
        	time = 0;
    	}
    		
    }

    void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Projectile"){
			//createMediumAsteroids();
			Destroy(gameObject);
		}
	}
}
