using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

	void movePlayer(){

	}

	void shootProjectile(){

	}

	void lookAtMouse(){
		Vector3 targetDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
		float rotate = (Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg) - 90f;
		transform.rotation = Quaternion.Euler(0f, 0f, rotate);
	}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lookAtMouse();
    }
}
