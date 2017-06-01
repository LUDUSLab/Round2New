using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {
    public Button newGame;
    public Button Continue;
    public Button Quit;
    public Canvas canvas;
    Animator anim;
    public GameObject DinoMouth;
    bool canGo;

    public float waitTime;
    // Use this for initialization
    void Start () {
        newGame.Select();
        Object.DontDestroyOnLoad(gameObject);
        anim = DinoMouth.GetComponent<Animator>();
        canGo = true;
    }
	
    void StartGame()
    {
        anim.SetTrigger("Close");
        newGame.enabled = false;
        Continue.enabled = false;
        Quit.enabled = false;
        canvas.enabled = false;
    }


    public void LoadLevel(string scene)
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(scene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }


    public IEnumerator Load(string scene)
    {
        if (canGo)
        {
            canGo = false;
            StartGame();
            yield return new WaitForSeconds(waitTime);
            LoadLevel(scene);
        }
        
    }

    public void Go(string scene)
    {
        StartCoroutine(Load(scene));
    }

    public void ExitGame()
    {
        if (canGo)
        {
            canGo = false;
            StartGame();
            Invoke("QuitGame", 1.5f);
        }
    }
}
