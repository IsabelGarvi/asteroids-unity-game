using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
	private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveAsteroid();
    }

    private void moveAsteroid(){
    	rb.AddForce(transform.up * 100f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
