using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sc_Teleport : MonoBehaviour
{
    //int sceneID;

    private GameObject GameManagerObj;
    void Start()
    {
        GameManagerObj = GameObject.FindWithTag("GM");
        //sceneID = SceneManager.GetActiveScene().buildIndex;
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            //DontDestroyOnLoad(col.gameObject);
            //SceneManager.LoadScene(sceneID + 1);
            GameManagerObj.GetComponent<ScGameManager>().LoadNextScene();
        }
    }
}
