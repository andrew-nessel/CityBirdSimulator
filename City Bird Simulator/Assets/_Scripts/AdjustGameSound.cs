using System.Collections;
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
        SoundControl.control.VictoryBGM.mute = true;
    }
    // Update is called once per frame
    void Update () {

        if (!SoundControl.control.GameBGM.mute)
        {
            if (!SoundControl.control.GameBGM.isPlaying)
            {
                SoundControl.control.GameBGM.PlayOneShot(SoundControl.control.GameBGM.clip);
            }
        }

        if (!SoundControl.control.VictoryBGM.mute)
        {
            if (!SoundControl.control.VictoryBGM.isPlaying)
            {
                SoundControl.control.GameBGM.PlayOneShot(SoundControl.control.VictoryBGM.clip);
            }
        }
    }
}
