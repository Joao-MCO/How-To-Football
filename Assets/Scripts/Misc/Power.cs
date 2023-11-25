using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
            if(other.gameObject.layer == 8){  
                GameManager.Instance.Poder(other.gameObject, gameObject);
            }
        }
}