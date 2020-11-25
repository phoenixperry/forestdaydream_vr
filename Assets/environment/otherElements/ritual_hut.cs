using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ritual_hut : MonoBehaviour
{
    int[] button_State;
    AudioSource win_audio;
    AudioSource activate_button_sound;
    // Start is called before the first frame update
    void Start()
    {
        button_State = new int[] {0,0,0,0};
        AudioSource[] sounds = gameObject.GetComponents<AudioSource>();
        win_audio = sounds[0];
        activate_button_sound = sounds[1];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ActivateRandomButton()
    {
        
    }
}
