using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoopParticleScript : MonoBehaviour {
    private AudioSource source;
    private float volLowRange = .5f;
    private float volHighRange = 1.0f;
    private float lowPitchRange = .75F;
    private float highPitchRange = 1.5F;

    public AudioClip splat;
    public AudioClip squish;
    void Start(){
        source.Stop();
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
        Destroy(this.gameObject, 5f);
	}
}
