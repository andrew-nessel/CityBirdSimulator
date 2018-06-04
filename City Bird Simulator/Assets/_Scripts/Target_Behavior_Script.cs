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
    void Start(){
		m_Material = GetComponent<Renderer>().material;
	}
	
	void OnCollisionEnter(Collision other){
		if (other.gameObject.CompareTag("Bomb")){
			this.m_Material.color = Color.white;
            int rand = Random.Range(0, 8);
            if (rand < 1f)
            {
                sound.PlayOneShot(Damn);
            }
            else if (rand < 2f)
            {
                sound.PlayOneShot(GodDammit);
            }
            else if (rand < 3f)
            {
                sound.PlayOneShot(No);
            }
            else if (rand < 4f)
            {
                sound.PlayOneShot(Sigh);
            }
            else if (rand < 5f)
            {
                sound.PlayOneShot(Great);
            }
            else if (rand < 6f)
            {
                sound.PlayOneShot(Why);
            }
            else if (rand < 7f)
            {
                sound.PlayOneShot(Yo);
            }
            else if (rand < 8f)
            {
                sound.PlayOneShot(Kidding);
            }

        }
	}
}
