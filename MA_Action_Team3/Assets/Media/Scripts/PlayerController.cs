using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class PlayerController : MonoBehaviour{
	
	public SkeletonAnimation skeletonAnimation;
	public AnimationReferenceAsset idle, walking, running;
	public string currentState;
	public float speed;
	public float movement;
	private Rigidbody2D rigidbody;
	private Vector3 characterScale;
	public string currentAnimation;
	
    // use this for initialization.
    void Start() {
		rigidbody = GetComponent<Rigidbody2D>();
        currentState = "idle";
		characterScale = transform.localScale;
		SetCharacterState(currentState);
	}

    // Update is called once per frame
    void Update(){
        Move();
    }
	
	// Sets character animation 
	public void SetAnimation(AnimationReferenceAsset animation, bool loop, float timeScale)
	{
		if(animation.name.Equals(currentAnimation))
		{
			return;
		}
		skeletonAnimation.state.SetAnimation(0, animation, loop).TimeScale = timeScale;
		currentAnimation = animation.name;
	}
	
	
	//checks character state and sets the animation accordingly.
	public void SetCharacterState(string state)
	{
		if (state.Equals("Idle"))
		{
			SetAnimation(idle, true, 1f);
		}
		else if (state.Equals("Walking"))
		{
			SetAnimation(walking, true, 2f);
		}
		else if (state.Equals("Running"))
		{
			SetAnimation(running, true, 3f);
		}
	}
	
	public void Move()
	{
			movement = Input.GetAxis("Horizontal");
			rigidbody.velocity = new Vector2(movement * speed, rigidbody.velocity.y);
		if (movement != 0)
		{
			SetCharacterState("Walking");
				if(movement > 0)
				{
					transform.localScale = new Vector2(characterScale.x, characterScale.y);

				}	
				else	
				{
					transform.localScale = new Vector2(-characterScale.x, characterScale.y);

				}
			
			SetCharacterState("Running");
			{
				if(Input.GetButtonDown("Run"));
			}
			SetCharacterState("Idle");
		}		
		
	}
}
