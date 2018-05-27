using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target_Behavior_Script : MonoBehaviour {
	
	Material m_Material;
	
	void Start(){
		m_Material = GetComponent<Renderer>().material;
	}
	
	void OnCollisionEnter(Collision other){
		if (other.gameObject.CompareTag("Bomb")){
			this.m_Material.color = Color.white;
		}
	}
}
