using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
	public GameObject ship;
	private Vector3 ship_position;
    // Start is called before the first frame update
    void Start()
    {
     	ship_position =  ship.transform.position;  
    }

    private void destroyProjectile(){
    	/* Destroy the projectile after it has traveled for a distance.
    	*/
    	// Vector3 player_position = player.playerPosition();
    	if (Vector3.Distance(transform.position, ship_position) > 5)
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
    	
        destroyProjectile();
    }
}
