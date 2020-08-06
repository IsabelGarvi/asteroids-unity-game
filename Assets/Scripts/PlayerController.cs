using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	Vector3 targetDirection = Vector3.zero;

	private void movePlayer(){
		/* Move the ship towards where the mouse is.
		*/

		targetDirection = get_target_direction();
		if(Input.GetKey(KeyCode.W)){
			transform.position = Vector3.MoveTowards(transform.position, targetDirection, 2 * Time.deltaTime);
		}

	}

	private void shootProjectile(){

	}

	private void lookAtMouse(){
		/* Make the ship look at where the mouse is on the scene
		*/
		targetDirection = get_target_direction();
		float rotate = (Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg) - 90f;
		transform.rotation = Quaternion.Euler(0f, 0f, rotate);
	}

	private Vector3 get_target_direction(){
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
    }
}
