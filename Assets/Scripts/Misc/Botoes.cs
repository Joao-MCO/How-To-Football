using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HTF{
    public class Botoes : MonoBehaviour
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
        public static Botoes Instance;
        public void Jogar(){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public void Voltar(){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }

        public void Pause(){
            UiManager.Instance.pause.SetActive(true);
            Time.timeScale = 0f;
        }

        public void UnPause(){
            UiManager.Instance.pause.SetActive(false);
            Time.timeScale = 1f;
        }

        public void Quit(){
            Application.Quit();
        }
    }
}
