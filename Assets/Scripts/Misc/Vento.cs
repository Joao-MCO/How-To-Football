using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vento : MonoBehaviour
{
   public float strength = 1f;
   private float _time;

   void Awake(){
        _time = GameManager.Instance.cooldownPower;
   }

   void Update(){
       _time -= Time.deltaTime;
       if(_time < 0) _time = GameManager.Instance.cooldownPower;
   }

   private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.layer == 9 && _time > 0){
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(strength*GameManager.Instance.playerController,0f));
        }
    }
}
