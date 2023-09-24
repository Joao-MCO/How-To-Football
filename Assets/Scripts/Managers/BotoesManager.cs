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
        SceneManager.LoadScene(1);
    }

    public void Facil(){
        SceneManager.LoadScene(2);
    }

    public void Medio(){
        SceneManager.LoadScene(3);
    }

    public void Dificil(){
        SceneManager.LoadScene(4);
    }

    public void Voltar(){
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void Pause(){
        GameManager.Instance.gol.SetActive(false);
        UiManager.Instance.pause.SetActive(true);
        AudioManager.Instance.Pause("Torcida");
        Time.timeScale = 0f;
    }


    public void UnPause(){
        UiManager.Instance.pause.SetActive(false);
        AudioManager.Instance.Play("Torcida");
        AudioManager.Instance.Play("Apito");
        Time.timeScale = 1f;
    }

    public void Quit(){
        Application.Quit();
    }
}

