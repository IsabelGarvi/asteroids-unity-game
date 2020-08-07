using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerController : MonoBehaviour
{
	private Vector3 targetDirection;
	private Vector2 screenBounds;

	/* Set the projectile on Unity UI*/
	public GameObject projectile;
	public Text lifeText;

	private int life = 3 /* number of lives the player has */;

	// Start is called before the first frame update
    void Start(){
    	screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
		transform.position = Vector3.zero;
    	showLife(life);
    }

	private void controlPlayer(){
		if(Input.GetKey(KeyCode.W)){
			movePlayer();
			checkPosition();
		}else if(Input.GetKeyDown(KeyCode.Space)){
			shootProjectile();
		}
	}

	private void movePlayer(){
		/* Move the ship towards where the mouse is on the screen.
		*/
		// TODO: fix when ship arrives at target disappearing
		targetDirection = getMousePosition();
		if(transform.position != targetDirection){
			transform.position = Vector3.MoveTowards(transform.position, targetDirection, 5 * Time.deltaTime);			
		}
	}

	private void checkPosition(){
    	if(transform.position.x > screenBounds.x || transform.position.x < -screenBounds.x){
    		transform.position = new Vector2(-transform.position.x, transform.position.y);
    	}else if(transform.position.y > screenBounds.y || transform.position.y < -screenBounds.y){
    		transform.position = new Vector2(transform.position.x, -transform.position.y);
    	}
    }

	// TODO: fix bug here.
	private void shootProjectile(){
		/* Shoot a projectile towards where the ship is pointing at when
		Space Key is pressed down.
		*/
		targetDirection = getMousePosition();
		GameObject new_projectile = Instantiate(projectile, transform.position, Quaternion.Euler(0,0,0));
		new_projectile.transform.GetComponent<Rigidbody2D>().velocity = targetDirection * 1f;
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
		return Camera.main.ScreenToWorldPoint(Input.mousePosition); //- transform.position;
	}

	private void loseLife(){
		life--;
		Debug.Log(life);
        if (life > 0) {
            transform.position = Vector3.zero;
            showLife(life);
        } else {
            // game over
            // SceneManager.LoadScene(3);
        }
	}

	private void showLife(int life){
    	lifeText.text = "LIFE: " + life.ToString();
    }

    // Update is called once per frame
    void Update(){
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
