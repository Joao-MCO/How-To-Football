using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Initiate : MonoBehaviour
{
    public CharacterDatabase cb;
    public float fator=1f;
    protected GameObject player;
    public Image logo;

    public abstract void Inicio();
}
