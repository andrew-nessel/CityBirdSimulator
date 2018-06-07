using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundControl : MonoBehaviour {
    public static SoundControl control;

    public AudioMixer MusicMixer;
    public AudioMixerGroup Master;
    public AudioMixerGroup BGM;
    public AudioMixerGroup SFX;
    public AudioSource GameBGM;
    public AudioSource MainMenuBGM;
    public AudioSource DeathBGM;
    public AudioSource CutSceneBGM;

    private void Awake()
    {
        if(control == null)
        {
            DontDestroyOnLoad(gameObject);
            control = this;
        }
        else if(control != this)
        {
            Destroy(gameObject);
        }
        
    }

   
    // Use this for initialization
    void Start () {
		
	}
	// Update is called once per frame
	void Update () {
		
	}
}
