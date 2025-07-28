using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScCampFire : MonoBehaviour
{
    [SerializeField] private GameObject TextObj;
    private GameObject PlayerObj;
    private ScPlayerDM PlayerDM;

    private bool isPlayerEntered = false;
    private bool healState = false;




    //Timer
    private float timer;
    [SerializeField] private float timerMax = 2f;
    [SerializeField] private float healAmount = 10f;



    void Start()
    {
        TextObj.SetActive(false);
        timer = timerMax;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && isPlayerEntered == true)
        {
            if (healState == false) healState = true;
            TextObj.SetActive(false);
        }
        if (healState == true)
        {
            HealPlayer();
        }
    }

    private void HealPlayer()
    {
        timer = timer - Time.deltaTime;
        if (timer <= 0f)
        {
            if (PlayerDM.getPlayerHealth() >= PlayerDM.getPlayerMaxHealth())
            {
                healState = false;
            }
            PlayerDM.hurt(-1 * healAmount);
            timer = timerMax;
            Debug.Log(PlayerDM.getPlayerHealth());
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player Entered Campfire Region");
            PlayerObj = other.gameObject;
            PlayerDM = PlayerObj.GetComponent<ScPlayerDM>();
            TextObj.SetActive(true);
            isPlayerEntered = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == PlayerObj)
        {
            Debug.Log("Player Exited Campfire Region");
            TextObj.SetActive(false);
            isPlayerEntered = false;
            healState=false;
        }

    }
}
     

