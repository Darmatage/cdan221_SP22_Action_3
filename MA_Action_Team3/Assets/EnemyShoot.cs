using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class EnemyShoot : MonoBehaviour {

	public Animator anim;
	private float timeBtwShots;
	public float startTimeBtwShots = 2;
	public GameObject projectile;

	private Transform player;
	private Vector2 PlayerVect;

	private Renderer rend;

	public float attackRange = 5;
	public bool isAttacking = false;
	private float scaleX;

	void Start () {
		Physics2D.queriesStartInColliders = false;
		scaleX = gameObject.transform.localScale.x;

		player = GameObject.FindGameObjectWithTag("Player").transform;
		PlayerVect = player.transform.position;

		timeBtwShots = startTimeBtwShots;

		rend = GetComponentInChildren<Renderer> ();
		anim = GetComponentInChildren<Animator> ();
	}

	void Update () {
		float DistToPlayer = Vector3.Distance(transform.position, player.position);
		
		if ((player != null) && (DistToPlayer <= attackRange)) {
			
			//Flip enemy to face player direction. Wrong direction? Swap the * -1.
			if (player.position.x > gameObject.transform.position.x){
				gameObject.transform.localScale = new Vector2(scaleX, gameObject.transform.localScale.y);
			} else {
				gameObject.transform.localScale = new Vector2(scaleX * -1, gameObject.transform.localScale.y);
			}

			//Timer for shooting projectiles
			if (timeBtwShots <= 0) {
				isAttacking = true;
				//anim.SetBool("IdleAttention", false);
				anim.SetTrigger("Attack");
				Instantiate (projectile, transform.position, Quaternion.identity);
				timeBtwShots = startTimeBtwShots;
			} else {
				timeBtwShots -= Time.deltaTime;
				isAttacking = false;
				//anim.SetBool("IdleAttention", true);
			}
		}
	}


	//DISPLAY the range of enemy's attack when selected in the Editor
	void OnDrawGizmosSelected(){
		Gizmos.DrawWireSphere(transform.position, attackRange);
	}
}