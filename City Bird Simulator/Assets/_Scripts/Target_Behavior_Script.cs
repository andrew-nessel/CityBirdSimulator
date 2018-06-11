using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target_Behavior_Script : MonoBehaviour {
	
	Material m_Material;
    public AudioClip Damn;
    public AudioClip GodDammit;
    public AudioClip No;
    public AudioClip Sigh;
    public AudioClip Great;
    public AudioClip Why;
    public AudioClip Yo;
    public AudioClip Kidding;
    private AudioSource sound;

    public Vector3 hitPosition;
    public Vector3 hitRotation;

    //Target Hit Sounds
    public AudioClip forTheBooks;
    public AudioClip bullseye;
    public AudioClip killConfirmed;
    public AudioClip oneDown;
    void Start(){
		m_Material = GetComponent<Renderer>().material;
        sound = gameObject.GetComponent<AudioSource>();
        sound.volume = 0.5f;
	}
	
	void OnCollisionEnter(Collision other){
		if (other.gameObject.CompareTag("Bomb")){
			this.m_Material.color = Color.white;
            float rand = Random.Range(0, 8);
            StartCoroutine(playEngineSound());

            transform.eulerAngles = hitRotation;
            transform.localPosition = hitPosition;

        }
	}

    IEnumerator playEngineSound()
    {
        float rand = Random.Range(0, 8);
        if (rand < 1f)
        {
            sound.clip = Damn;
        }
        else if (rand < 2f)
        {
            sound.clip = (GodDammit);
        }
        else if (rand < 3f)
        {
            sound.clip = (No);
        }
        else if (rand < 4f)
        {
            sound.clip = (Sigh);
        }
        else if (rand < 5f)
        {
            sound.clip = (Great);
        }
        else if (rand < 6f)
        {
            sound.clip = (Why);
        }
        else if (rand < 7f)
        {
            sound.clip = (Yo);
        }
        else if (rand < 8f)
        {
            sound.clip = Kidding;
        }
        sound.Play();

        yield return new WaitForSeconds(sound.clip.length);
        sound.volume = 1f;
        if (rand < 2f)
        {
            sound.clip = forTheBooks;
        }
        else if (rand < 4f)
        {
            sound.clip = bullseye;
        }
        else if (rand < 6f)
        {
            sound.clip = killConfirmed;
        }
        else if (rand < 8f)
        {
            sound.clip = oneDown;
        }
        sound.Play();
    }
}
