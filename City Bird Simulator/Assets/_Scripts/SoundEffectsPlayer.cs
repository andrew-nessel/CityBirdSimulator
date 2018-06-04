using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectsPlayer : MonoBehaviour {
    public AudioClip splat;
    public AudioClip squish;

    private AudioSource source;
    private float volLowRange = .5f;
    private float volHighRange = 1.0f;
    private float lowPitchRange = .75F;
    private float highPitchRange = 1.5F;

    public void PlaySplat()
    {
        source.pitch = Random.Range(lowPitchRange, highPitchRange);
        float vol = Random.Range(volLowRange, volHighRange);
        float rand = Random.Range(0, 2);
        if (rand < 1f)
        {
            source.PlayOneShot(splat, vol);
        }
        else if (rand < 2f)
        {
            source.PlayOneShot(squish, vol);
        }
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
