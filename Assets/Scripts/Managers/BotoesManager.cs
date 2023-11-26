using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BotoesManager : MonoBehaviour
{
    public static BotoesManager Instance;
    public int escolha1, escolha2;    
    public bool _selectedHome, _selectedAway, _confirmation;
    public GameObject panel;
    public GameObject[] textos;
    public MenuController mc;

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
        _selectedAway = false;
        _selectedHome = false;
        _confirmation = false;
    }
    public void PVP()
    {
        mc.Fechar();
        Destroy(mc.jogar, 1f);
        Destroy(mc.instrucoes, 1f);
        Destroy(mc.creditos, 1f);
        Destroy(mc.config, 1f);
        Destroy(mc,1f);
        Debug.Log("Feito");
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
        if(_selectedAway && _selectedHome){
            panel.SetActive(true);
            if (Input.GetKeyUp(KeyCode.U) || Input.GetKeyUp(KeyCode.E)) SceneManager.LoadScene(2);
            else if(Input.GetKeyUp(KeyCode.Z) || Input.GetKeyUp(KeyCode.M)){
                panel.SetActive(false);
                _selectedAway = false;
                _selectedHome = false;
                escolha1 = -1;
                escolha2 = -1;
                textos[0].SetActive(true);
                textos[2].SetActive(false);
                textos[1].SetActive(true);
                textos[3].SetActive(false);
            }
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            textos[0].SetActive(false);
            textos[2].SetActive(true);
            _selectedHome = true;
            escolha1 = index1;
        }

        if (Input.GetKeyUp(KeyCode.U))
        {
            textos[1].SetActive(false);
            textos[3].SetActive(true);
            _selectedAway = true;
            escolha2 = index2 + 12;
        }

        if (Input.GetKeyUp(KeyCode.Z))
        {
            textos[0].SetActive(true);
            textos[2].SetActive(false);
            _selectedHome = false;
            escolha1 = -1;
        }

        if (Input.GetKeyUp(KeyCode.M))
        {
            textos[1].SetActive(true);
            textos[3].SetActive(false);
            _selectedAway = false;
            escolha2 = -1;
        }        
    }
}

