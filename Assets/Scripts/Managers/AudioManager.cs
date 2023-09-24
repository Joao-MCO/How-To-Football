using System;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public Sound[] sounds;
    
    // Start is called before the first frame update
    private void Awake (){
        if (Instance == null){
            Instance = this;
        }else{
            Destroy(gameObject, 1f);
            return;
        }
        DontDestroyOnLoad(gameObject);
        foreach(Sound s in sounds){
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play (string name){
        Sound s = Array.Find(sounds, sound => sound.nome == name);
        s.source.Play();
    }

    public void Pause (string name){
        Sound s = Array.Find(sounds, sound => sound.nome == name);
        s.source.Stop();
    }
}
