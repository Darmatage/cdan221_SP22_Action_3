using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow2DLERP : MonoBehaviour{
    public GameObject target;
    public float camSpeed = 4.0f;


	private float vertMove;
	public float CameraDownAmt = 0.5f;
	private bool CameraDown = false;

      void Start(){
            target = GameObject.FindWithTag("Player");
      }

	void Update(){
		vertMove = Input.GetAxis("Vertical");
		if (vertMove < 0){
			CameraDown = true;
		} else {
			CameraDown = false;
		}
	}


    void FixedUpdate () {
        Vector2 pos = Vector2.Lerp ((Vector2)transform.position, (Vector2)target.transform.position, camSpeed * Time.fixedDeltaTime);
 
		if (CameraDown){
			transform.position = new Vector3 (pos.x, pos.y - CameraDownAmt, transform.position.z);
		} else {
			transform.position = new Vector3 (pos.x, pos.y, transform.position.z);
		}	
    }
}