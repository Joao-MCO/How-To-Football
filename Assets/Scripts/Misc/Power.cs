using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HTF;

public class Power : MonoBehaviour
{

    private GameObject _player;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.layer == 8){  
            gameObject.SetActive(false);
            _player = other.gameObject;
            GameManager.Instance.Poder(_player, gameObject);
            Destroy(gameObject, 1f);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.layer == 8){  
            Destroy(gameObject, 1f);
        }
    }
}