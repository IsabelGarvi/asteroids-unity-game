using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	private Vector3 targetDirection = Vector3.zero;
	public GameObject projectile;

	private void movePlayer(){
		/* Move the ship towards where the mouse is.
		*/

		targetDirection = getTargetDirection();
		if(Input.GetKey(KeyCode.W)){
			transform.position = Vector3.MoveTowards(transform.position, targetDirection, 5 * Time.deltaTime);
		}

	}

	private void shootProjectile(){
		targetDirection = getTargetDirection();
		if(Input.GetKeyDown(KeyCode.Space)){
			GameObject new_projectile = Instantiate(projectile, transform.position, Quaternion.Euler(0,0,0));
			// new_projectile.transform.parent = gameObject.transform;
			// new_projectile.transform.position = Vector3.MoveTowards(transform.position, targetDirection, 2 * Time.deltaTime);
			new_projectile.transform.GetComponent<Rigidbody2D>().velocity = targetDirection * 1f;

		}
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
		return Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
	}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    	movePlayer();
        lookAtMouse();
        shootProjectile();
    }
}
