using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BotoesManager : MonoBehaviour
{
    public static BotoesManager Instance;
    public int escolha1, escolha2;

    private void Awake (){
        escolha1 = -1;
        escolha2 = -1;
        if (Instance == null){
            Instance = this;
        }else{
            Destroy(gameObject, 1f);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
    public void PVP()
    {
        SceneManager.LoadScene(1);
    }
    /*
    public void Facil(){
        escolha = 3;
    }

    public void Medio()
    {
        escolha = 4;
    }

    public void Dificil()
    {
        escolha = 5;
    }
    */
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

    public void Select(int index1, int index2)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            escolha1 = index1;
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            escolha2 = index2 + 12;
        }

        if (escolha1 > -1 && escolha2 > -1)
            SceneManager.LoadScene(2);
        //Debug.Log(escolha1);
        //Debug.Log(escolha2);

    }
}

