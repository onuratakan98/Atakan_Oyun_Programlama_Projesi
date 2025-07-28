using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ScSoundManager : MonoBehaviour
{

    [SerializeField] public AudioSource AudioSrc;
    [SerializeField] private AudioClip Level0Music;
    [SerializeField] private AudioClip Level1Music;

    Scene currentScene;

    // Start is called before the first frame update
    void Start()
    {
        //GetCurrentScene
        currentScene = SceneManager.GetActiveScene();
        this.AudioSrc.clip = Level1Music;
        AudioSrc.Play();
        AudioSrc.loop = true;


        //Debug.Log("Screen Width: "+Screen.width);
        //Debug.Log("Screen Height: "+Screen.height);
        changeMusicOnLevel();
        

    }

    public void changeMusicOnLevel()
    {
        //GetCurrentScene
        currentScene = SceneManager.GetActiveScene();

        if (currentScene.buildIndex == 0)
        {
            this.AudioSrc.clip = Level0Music;
            AudioSrc.Play();
            AudioSrc.loop = true;
        }

        else if (currentScene.buildIndex == 1)
        {
            this.AudioSrc.clip = Level1Music;
            AudioSrc.Play();
            AudioSrc.loop = true;
        }

        }
    

    // Update is called once per frame
    void Update()
    {
        //this.AudioSrc.clip = Level1Music;
        //AudioSrc.Play();
       // AudioSrc.loop = true;

    }
}
