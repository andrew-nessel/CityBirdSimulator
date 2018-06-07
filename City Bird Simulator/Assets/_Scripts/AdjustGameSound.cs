﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustGameSound : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        SoundControl.control.GameBGM.mute = false;
        SoundControl.control.GameBGM.Stop();
        SoundControl.control.GameBGM.PlayOneShot(SoundControl.control.GameBGM.clip);
        SoundControl.control.MainMenuBGM.mute = true;
        SoundControl.control.CutSceneBGM.mute = true;
        SoundControl.control.DeathBGM.mute = true;
    }
    // Update is called once per frame
    void Update () {
		
	}
}
