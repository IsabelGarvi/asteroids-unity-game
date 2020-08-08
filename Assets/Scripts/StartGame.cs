using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    void Start()
    {
        
    }

    public void startGame(){
    	/* Load level 1
    	*/
		SceneManager.LoadScene(1);
	}

    void Update()
    {
        
    }
}
