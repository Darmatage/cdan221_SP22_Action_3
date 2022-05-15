using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PickUp : MonoBehaviour{

	public GameHandler gameHandler;
	//public playerVFX playerPowerupVFX;
	public bool isHealthPickUp = true;
	public bool increaseHealthMax =false;
	public bool isSpeedBoostPickUp = false;
	public bool isOrb = false;


	public int healthBoost = 10;
	public float speedBoost = 2f;
	public float speedTime = 2f;

	public int healthMaxIncrease = 10;

	void Start(){
            gameHandler = GameObject.FindWithTag("GameHandler").GetComponent<GameHandler>();
            //playerPowerupVFX = GameObject.FindWithTag("Player").GetComponent<playerVFX>();
	}

	public void OnTriggerEnter2D (Collider2D other){
		if (other.gameObject.tag == "Player"){
			//Debug.Log("Hit Player");
			GetComponent<Collider2D>().enabled = false;
			//GetComponent<AudioSource>().Play();
			StartCoroutine(DestroyThis());

			if (isHealthPickUp == true) {
				if (increaseHealthMax){
					gameHandler.StartPlayerHealth += healthMaxIncrease;
				}
				gameHandler.playerGetHit(healthBoost * -1);
				//playerPowerupVFX.powerup();
			}

			if (isSpeedBoostPickUp == true) {
				other.gameObject.GetComponent<PlayerMove>().speedBoost(speedBoost, speedTime);
				//playerPowerupVFX.powerup();
			}
		
			if (isOrb == true) {
				gameHandler.playerGetTokens(1);
				//playerPowerupVFX.powerup();
			}
			
		}
	}

	IEnumerator DestroyThis(){
		yield return new WaitForSeconds(0.3f);
		Destroy(gameObject);
	}

}