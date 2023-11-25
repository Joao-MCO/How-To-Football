using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiate : MonoBehaviour
{
    public void Inicia(GameObject power){
        Instantiate(power, transform);
    }
}