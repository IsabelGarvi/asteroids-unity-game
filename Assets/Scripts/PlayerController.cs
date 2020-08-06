using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
	private Vector3 targetDirection = Vector3.zero;
	/* Set the projectile on Unity UI*/
	public GameObject projectile;

	public static int life = 3 /* number of lives the player has */;

	// Start is called before the first frame update
    void Start()
    {
        Debug.Log(life);
    }

	private void controlPlayer(){
		if(Input.GetKey(KeyCode.W)){
			movePlayer();
		}else if(Input.GetKeyDown(KeyCode.Space)){
			shootProjectile();
		}
	}

	private void movePlayer(){
		/* Move the ship towards where the mouse is on the screen.
		*/
		// TODO: fix when ship arrives at target disappearing
		targetDirection = getTargetDirection();
		if(transform.position != targetDirection){
			transform.position = Vector3.MoveTowards(transform.position, targetDirection, 5 * Time.deltaTime);			
		}
	}

	// TODO: fix bug here.
	private void shootProjectile(){
		/* Shoot a projectile towards where the ship is pointing at when
		Space Key is pressed down.
		*/
		targetDirection = getTargetDirection();
		GameObject new_projectile = Instantiate(projectile, transform.position, Quaternion.Euler(0,0,0));
		new_projectile.transform.GetComponent<Rigidbody2D>().velocity = targetDirection * 1f;
	}

	private void lookAtMouse(){
		/* Make the ship look at where the mouse is on the scene
		*/
		targetDirection = getTargetDirection();
		float rotate = (Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg) - 90f;
		transform.rotation = Quaternion.Euler(0f, 0f, rotate);
	}

	private Vector3 getTargetDirection(){
		/* Get the direction towards where the ship will be pointing
		*/
		return Camera.main.ScreenToWorldPoint(Input.mousePosition); //- transform.position;
	}

	private void loseLife(){
		life--;
        if (life >= 1) {
            // respawn to center of the screen with 1 less life.
        } else {
            // game over
        }
        Debug.Log(life);
	}

    // Update is called once per frame
    void Update()
    {
        lookAtMouse();
        controlPlayer();
    }

    void OnCollisionEnter2D(Collision2D other){
		/* When colliding with an asteroid, lose life.
		*/
		switch(other.gameObject.tag){
			case "Asteroid":
				loseLife();
				break;
		}
	}
}
