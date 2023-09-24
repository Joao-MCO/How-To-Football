using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject instrucoes;
    public GameObject creditos;
    public GameObject jogar;

    public void Jogar(){
        jogar.SetActive(true);
    }
    public void Instrucoes(){
        instrucoes.SetActive(true);
    }
    
    public void Creditos(){
        creditos.SetActive(true);
    }

    public void Fechar(){
        instrucoes.SetActive(false);
        creditos.SetActive(false);
        jogar.SetActive(false);
    }
}
