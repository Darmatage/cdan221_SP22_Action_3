using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wisp_Behavior : MonoBehaviour{

	public bool level1wisp = true;
	public bool level2wisp = false;
	public bool level3wisp = false;

	//public Animator anim;
	private Renderer rend;

	public float speed = 2f;
	public float stoppingDistance = 4f; // when wisp stops moving towards player
	public float retreatDistance = 3f; // when wisp moves away from approaching player

	private Rigidbody2D rb;
	public Transform player;

	private float scaleX;
	public Color captureColor;
	public bool isCaptured = false;

	private GameObject gameHandler;

	void Start () {
		scaleX = gameObject.transform.localScale.x;

		rb = GetComponent<Rigidbody2D> ();
		player = GameObject.FindGameObjectWithTag("Player").transform;

		//anim = GetComponentInChildren<Animator> ();
		rend = GetComponentInChildren<Renderer> ();

		gameHandler = GameObject.FindWithTag("GameHandler");

	}

	void Update () {
		float DistToPlayer = Vector3.Distance(transform.position, player.position);
		if ((player != null) && (isCaptured)) {
			GetComponent<Pickup_Bob>().enabled=false;
			// approach player
			if (Vector2.Distance (transform.position, player.position) > stoppingDistance) {
				transform.position = Vector2.MoveTowards (transform.position, player.position, speed * Time.deltaTime);
				//anim.SetBool("Fly", true);
			}

			// stop moving
			else if (Vector2.Distance (transform.position, player.position) < stoppingDistance && Vector2.Distance (transform.position, player.position) > retreatDistance) {
				transform.position = this.transform.position;
				//anim.SetBool("Fly", false);
			}

			// retreat from player
			else if (Vector2.Distance (transform.position, player.position) < retreatDistance) {
				transform.position = Vector2.MoveTowards (transform.position, player.position, -speed * Time.deltaTime);
				//anim.SetBool("Fly", true);
			}

			//Flip enemy to face player direction. Wrong direction? Swap the * -1.
			if (player.position.x < gameObject.transform.position.x){
				gameObject.transform.localScale = new Vector2(scaleX, gameObject.transform.localScale.y);
			} else {
				gameObject.transform.localScale = new Vector2(scaleX * -1, gameObject.transform.localScale.y);
			}

		}
		//allow bobbing again if wisp is lost
		GetComponent<Pickup_Bob>().enabled=true;
	}

	void OnTriggerEnter2D (Collider2D other){
		if(other.gameObject.tag=="Player"){
			if (level1wisp){GameHandler.haveWisp1 = true; isCaptured=true;}
			if (level2wisp){GameHandler.haveWisp2 = true; isCaptured=true;}
			if (level3wisp){GameHandler.haveWisp3 = true; isCaptured=true;}
			StartCoroutine(ColorChange());

			//activate timer and wisp release functions
			gameHandler.GetComponent<TimerWisp>().GotTheWisp(gameObject);
		}
	}

	IEnumerator ColorChange(){
		// color values are R, G, B, and alpha, each divided by 100
		rend.material.color = captureColor;
		yield return new WaitForSeconds(1f);
		rend.material.color = Color.white;
	}

}
