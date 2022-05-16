using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class TeleportBack : MonoBehaviour {

	public Transform lastCheckpoint;
	private Transform player;


	void Start(){
		player = GameObject.FindWithTag("Player").transform;
	
	}


	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Player"){
			player.position = lastCheckpoint.position;
		}
	}

}
