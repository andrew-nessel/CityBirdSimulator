using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudjustMainMenu : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        SoundControl.control.GameBGM.mute = true;
        SoundControl.control.MainMenuBGM.mute = false;
        SoundControl.control.MainMenuBGM.Stop();
        SoundControl.control.MainMenuBGM.PlayOneShot(SoundControl.control.MainMenuBGM.clip);
        SoundControl.control.CutSceneBGM.mute = true;
        SoundControl.control.DeathBGM.mute = true;
        SoundControl.control.VictoryBGM.mute = true;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
