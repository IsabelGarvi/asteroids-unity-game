using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigAsteroidController : MonoBehaviour
{

	private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
    	rb = GetComponent<Rigidbody2D>();
    	transform.Rotate(new Vector3 (0, 0, Random.Range(0f, 360f)));
        moveAsteroid();        
            
    }

    private void moveAsteroid(){
    	rb.AddForce(transform.up * 100f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Projectile"){
			// Create 2 medium asteroids.
			Destroy(gameObject);
		}
	}
}
