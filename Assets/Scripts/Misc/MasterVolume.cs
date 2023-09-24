using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MasterVolume : MonoBehaviour
{
    [SerializeField] Slider slider;
    public void ChangeVolume(float sliderValue)
    {
        AudioListener.volume = sliderValue;
    }
}
