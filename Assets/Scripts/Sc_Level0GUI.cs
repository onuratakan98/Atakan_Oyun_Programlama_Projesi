using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sc_Level0GUI : MonoBehaviour
{

    GameObject GameManagerObj;
    ScGameManager GameManagerSc;

    int sceneID;
    // Start is called before the first frame update
    void Start()
    {
        sceneID = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("Current scene Id" + sceneID);

        GameManagerObj = GameObject.FindWithTag("GM");
        GameManagerSc = GameManagerObj.GetComponent<ScGameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void button_NewGame()
    {
        SceneManager.LoadScene(sceneID+1);
        GameManagerSc.MainMenuStart();
    }
    public void button_Load()
    {
        GameManagerSc.MainMenuLoad();
    }

    public void button_Settings()
{
    

}
    public void button_Exit()
    {
        Application.Quit();
    }

}
