using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigAsteroidController : MonoBehaviour
{

	private Rigidbody2D rb;
	public GameObject medium_asteroid;
	private Vector2 screenBounds;

    // Start is called before the first frame update
    void Start()
    {
    	screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    	rb = GetComponent<Rigidbody2D>();
    	transform.Rotate(new Vector3 (0, 0, Random.Range(0f, 359f))); 
    	moveAsteroid();
            
    }

    private void moveAsteroid(){
    	rb.AddForce(transform.up * 100f);
    }

    private void checkPosition(){
    	if(transform.position.x > screenBounds.x || transform.position.x < -screenBounds.x){
    		transform.position = new Vector2(-transform.position.x, transform.position.y);
    	}else if(transform.position.y > screenBounds.y || transform.position.y < -screenBounds.y){
    		transform.position = new Vector2(transform.position.x, -transform.position.y);
    	}
    }

    private void createMediumAsteroids(){
    	for(int i = 0; i < 2; i++){
    		Vector3 position = transform.position;
    		GameObject new_medium_asteroid = Instantiate(medium_asteroid, position, Quaternion.Euler(0,0,0));
    	}
    }

    // Update is called once per frame
    void Update()
    {
         
        checkPosition();
    }

    void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Projectile"){
			createMediumAsteroids();
			Destroy(gameObject);
		}
	}
}
