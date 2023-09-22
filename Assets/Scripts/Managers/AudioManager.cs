using System;
using UnityEngine.Audio;
using UnityEngine;

namespace HTF{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;
        public Sound[] sounds;
        // Start is called before the first frame update
        private void Awake (){
            if (Instance == null){
                Instance = this;
            }
            foreach(Sound s in sounds){
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;

                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
                s.source.loop = s.loop;
            }
            DontDestroyOnLoad(gameObject);
        }

        public void Play (string name){
            Sound s = Array.Find(sounds, sound => sound.nome == name);
            s.source.Play();
        }
    }
}
