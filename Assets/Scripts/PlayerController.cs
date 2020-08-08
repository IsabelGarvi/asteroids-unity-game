using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class PlayerController : MonoBehaviour
{
	private Vector3 targetDirection;
	private Level01 level_controller;

	/* Set variables on Unity GUI*/
	public GameObject projectile;
	public Text lifeText;

	private int life = 3 /* number of lives the player has */;

    void Start(){
    	level_controller = GameObject.Find("SceneManager").GetComponent<Level01>();
		transform.position = Vector3.zero;
    	showLife(life);
    }

	private void controlPlayer(){
		/* If the player presses key W, the ship moves.
		If the player presses key Space, the ship fires a projectile.
		*/
		if(Input.GetKey(KeyCode.W)){
			movePlayer();
			checkPosition();
		}
		if(Input.GetKeyDown(KeyCode.Space)){
			shootProjectile();
		}
	}

	private void movePlayer(){
		/* Move the ship towards where the mouse is on the screen.
		*/
		targetDirection = getMousePosition();
		transform.position = Vector3.MoveTowards(transform.position, new Vector2(targetDirection.x, targetDirection.y), 5 * Time.deltaTime);			
	}

	private void checkPosition(){
		/* If the position of the player is outside the boundaries of
		the scene, the player is transported to the other side of it.
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

	// TODO: fix bug here.
	private void shootProjectile(){
		/* Shoot a projectile towards where the ship is pointing at when
		Space Key is pressed down.
		*/
		targetDirection = getMousePosition();
		GameObject new_projectile = Instantiate(projectile, transform.position, Quaternion.Euler(0,0,0));
		float rotate = (Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg) - 90f;
		new_projectile.transform.rotation = Quaternion.Euler(0f, 0f, rotate);
		new_projectile.transform.GetComponent<Rigidbody2D>().velocity = targetDirection * 2f;
	}

	private void lookAtMouse(){
		/* Make the ship look at where the mouse is on the scene
		*/
		targetDirection = getMousePosition();
		float rotate = (Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg) - 90f;
		transform.rotation = Quaternion.Euler(0f, 0f, rotate);
	}

	private Vector3 getMousePosition(){
		/* Get the mouse position on the screen
		*/
		return Camera.main.ScreenToWorldPoint(Input.mousePosition);
	}

	private void loseLife(){
		/* Lose 1 life. If the player still has lives left
		reset its position to the center of the screen, otherwise, game over.
		*/
		life--;
        if (life > 0) {
            transform.position = new Vector3(0f, 0f, 0f);
            showLife(life);
        } else {
            SceneManager.LoadScene(1);
        }
	}

	private void showLife(int life){
		/* Update the LIFE text on screen.
		*/
    	lifeText.text = "LIFE: " + life.ToString();
    }

    void Update(){
        lookAtMouse();
        controlPlayer();
    }

    void OnTriggerEnter2D(Collider2D other){
		/* When colliding with an asteroid the player loses one life.
		The asteroid is destroyed.
		*/
		switch(other.tag){
			case "Asteroid":
				level_controller.total_asteroids--;
				Destroy(other.gameObject);
				loseLife();
				break;
		}
	}
}
