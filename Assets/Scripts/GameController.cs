using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.R)) {
            if (Time.timeScale == 0) {
                Time.timeScale = 1;
                StartCoroutine(restartGame());
            }
        }
	}

    IEnumerator restartGame() {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(0);
        while (!asyncLoad.isDone) {
            yield return null;
        }
    }
}
