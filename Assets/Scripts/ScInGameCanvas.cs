using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScInGameCanvas : Singleton<ScInGameCanvas>
{
    [SerializeField] private GameObject InGameGUIObj;
    [SerializeField] private GameObject InGameMenuObj;
    [SerializeField] private GameObject PlayerGO;
    [SerializeField] private ScPlayerDM PlayerDMSC;
     bool isPause=false;


    // Start is called before the first frame update
    void Start()
    {
        if(InGameGUIObj != null || InGameMenuObj ==null)
        {
            InGameGUIObj=this.gameObject.transform.GetChild(0).gameObject;
            InGameMenuObj= this.gameObject.transform.GetChild(1).gameObject;
        }

        this.SetPlayerGUIObject();



        isPause=false;
        InGameMenuObj.SetActive(false);
        //InGameGUIObj.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel") || Input.GetKeyDown(KeyCode.P))
        {
            button_ContinuePauseToggle();

           
        }
    }

    public void button_ContinuePauseToggle()
    {
        if (isPause == false)
        {
            Time.timeScale = 0f;
            InGameMenuObj.SetActive(true);
            isPause=true;
        }
        else
        {
            Time.timeScale = 1f;
            InGameMenuObj.SetActive(false);
            isPause = false;
        }
    }

    public void button_Save()
    {
        SaveLoad.SaveData(PlayerDMSC.getPlayerData());
    }
    public void button_Load()
    {
        ScPlayerData temp=SaveLoad.LoadData();
        PlayerDMSC.setPlayerData(temp);
    }


    public void button_Exit()
    {
        Application.Quit();
    }
    public void setPlayer(GameObject PlayerObj)
    {
        PlayerGO = PlayerObj;
    }
    public void SetPlayerGUIObject()
    {
        if(PlayerGO != null)
        {
            if (InGameGUIObj != null || InGameMenuObj == null)
            {
                InGameGUIObj = this.gameObject.transform.GetChild(0).gameObject;
                InGameMenuObj = this.gameObject.transform.GetChild(1).gameObject;
            }
            //Assign Interaction Between Player GUI to Player Data
            PlayerDMSC = PlayerGO.GetComponent<ScPlayerDM>();

            //GUI Elements
             GameObject HealthBarObj=InGameGUIObj.transform.GetChild(0).gameObject;
             GameObject HealthBarTextObj= InGameGUIObj.transform.GetChild(2).gameObject;
             GameObject BoneCountObj= InGameGUIObj.transform.GetChild(5).gameObject;
             GameObject ScoreCountObj= InGameGUIObj.transform.GetChild(7).gameObject;

            PlayerDMSC.setGUIObject(HealthBarObj, HealthBarTextObj, BoneCountObj, ScoreCountObj);
            Debug.Log("Gui elements assigned to player1");
        }
        else
        {
            Debug.LogError("InGameCanvas Script Error : Player object cannot be found");
        }
    }




}
