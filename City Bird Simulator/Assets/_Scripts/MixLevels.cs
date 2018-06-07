using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MixLevels : MonoBehaviour {

    public AudioMixer masterMixer;
	// Use this for initialization
	public void setSfxVolume(float sfxvolume)
    {
        masterMixer.SetFloat("SFXVolume", sfxvolume);
    }
    public void setBGMVolume(float bgmvolume)
    {
        masterMixer.SetFloat("BGMVolume", bgmvolume);
    }
    public void setMasterVolume(float mastervolume)
    {
        masterMixer.SetFloat("MasterVolume", mastervolume);
    }
}
