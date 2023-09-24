using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BotoesManager : MonoBehaviour
{
    private void Awake (){
        if (Instance == null){
            Instance = this;
        }else{
            Destroy(gameObject, 1f);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
    public static BotoesManager Instance;
    public void PVP(){
        SceneManager.LoadScene(2);
    }

    public void PVC(){
        SceneManager.LoadScene(1);
    }

    public void Voltar(){
        SceneManager.LoadScene(0);
    }

    public void Pause(){
        UiManager.Instance.pause.SetActive(true);
        Time.timeScale = 0f;
    }


    public void UnPause(){
        UiManager.Instance.pause.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Quit(){
        Application.Quit();
    }
}

