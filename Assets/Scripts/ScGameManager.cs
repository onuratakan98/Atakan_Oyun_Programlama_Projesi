using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class ScGameManager : Singleton<ScGameManager>
{

    [SerializeField] private GameObject PlayerPrefab;
    private GameObject PlayerGO;
    private ScPlayerDM PlayerDMSC;

    // [SerializeField] private GameObject InGameUIprefab;
    // private GameObject InGameUIobj;
    [SerializeField] private GameObject InGameCanvasPrefab;
    private GameObject InGameCanvas;
    private ScInGameCanvas InGameCanvasSc;

    //Sound Manager
    [SerializeField] private GameObject SoundManagerPrefab;
        private GameObject SoundMngr;


    //Player
    ScPlayerData Pxdata;
   // private bool StartAtBeggining = false;
    
    //Scene
    Scene currentScene;
    
    void Start()
    {
        //Get Current Scene
        currentScene = SceneManager.GetActiveScene();

        //Instantiate Player
        PlayerGO = Instantiate(PlayerPrefab, new Vector3(0, 9, 0),Quaternion.identity);
        PlayerDMSC = PlayerGO.GetComponent<ScPlayerDM>();
        //Create Player Data
        Pxdata = new ScPlayerData();
        PlayerDMSC.setPlayerReference(Pxdata);

        //Instantiate InGameCanvas
        //InGameUIobj = Instantiate(InGameUIprefab, new Vector3(0, 0, 0), Quaternion.identity);
        //InGameCanvas = InGameUIObj.transform.GetChild(0).gameObject;
        InGameCanvas= Instantiate(InGameCanvasPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        InGameCanvasSc = InGameCanvas.GetComponent<ScInGameCanvas>();
        InGameCanvasSc.setPlayer(PlayerGO);
        InGameCanvasSc.SetPlayerGUIObject();

        //Sound Manager
        SoundMngr= Instantiate(SoundManagerPrefab,new Vector3(0, 0, 0), Quaternion.identity);


        if (currentScene.buildIndex==0)
        {
            PlayerGO.SetActive(false);
            InGameCanvas.SetActive(false);
        }
        else
        {
            CheckCineMachine();
        }
    }
    
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log(mode);

        CheckCineMachine();

        SoundMngr.GetComponent<ScSoundManager>().changeMusicOnLevel();
    }

    public void CheckCineMachine()
    {
        GameObject cineMachine = GameObject.FindWithTag("Cinemachine");
        if (cineMachine != null)
        {
            CinemachineVirtualCamera vcam = cineMachine.GetComponent<CinemachineVirtualCamera>();

            if (PlayerGO == null) PlayerGO = GameObject.FindWithTag("Player");
            vcam.Follow = PlayerGO.transform; // vcam.LookAt=..transform
        }

    }


    public void MainMenuStart()
    {
        //StartAtBeggining=true;
        
        SceneManager.LoadScene(1);
        
        //Player & GUI active
        PlayerGO.SetActive(true);
        InGameCanvas.SetActive(true);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void MainMenuLoad()
    {
        //Player and GUI active
        PlayerGO.SetActive(true);
        InGameCanvas.SetActive(true);

        ScPlayerData temp = null;
            if (PlayerGO != null && PlayerDMSC != null)
        {
            temp = SaveLoad.LoadData();
            PlayerDMSC.setPlayerData(temp);
        }
        else
        {
            Debug.Log("No Player Object and Script");
        }
            if(temp != null)
        {
            SceneManager.LoadScene(temp.getPlayerSceneId());
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
    }
    
    public void LoadNextScene()
    {
        currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex + 1);
        SceneManager.sceneLoaded += OnSceneLoaded;


    }

}