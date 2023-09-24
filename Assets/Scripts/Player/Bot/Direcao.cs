using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HTF_Bot{
    public class Direcao : MonoBehaviour
    {
        private bool _may = false;
        public bool Verify(){
            return _may;
        }

        private void OnTriggerStay2D(Collider2D other) {
            if(other.gameObject.layer == 9  && gameObject.tag == "Bot" && gameObject.layer == 12) _may = true;
        }

        private void OnTriggerExit2D(Collider2D other) {
            if(other.gameObject.layer == 9 && gameObject.tag == "Bot" && gameObject.layer == 12) _may = false;
        }
    }
}
