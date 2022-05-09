using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerWisp : MonoBehaviour{

	private float returnWispTimer;       //set the number of seconds here
	public float WispTimerMax = 10f;
	public bool gotWisp = false;

	public Image timerCircleDisplay;

	//failure variables
	private GameObject theWisp;
	public Vector2 wispStartingPos;
	public GameObject failMessage;

	void Start(){
		timerCircleDisplay.gameObject.SetActive(false);
		returnWispTimer = WispTimerMax;
		failMessage.SetActive(false);
	}

	void Update(){
		//test functionality
		if (Input.GetKeyDown("8")){ 
			gotWisp = true;
		}
	}

	void FixedUpdate(){
		if (gotWisp == true){
			returnWispTimer -= 0.01f;
			Debug.Log("time: " + returnWispTimer);
			timerCircleDisplay.gameObject.SetActive(true);
			timerCircleDisplay.fillAmount = returnWispTimer / WispTimerMax; 
			
			// Color oldCol = timerCircleDisplay.GetComponent<Image>().color;
			// if (oldCol.a > 0.01f){
				// float r = oldCol.r - 0.005f;
				// float g = oldCol.g - 0.005f;
				// float b = oldCol.b - 0.005f;
				// float a = oldCol.a - 0.005f;
				// timerCircleDisplay.GetComponent<Image>().color = new Color(r,g,b,a);
				//float x = display.localScale.x/1.005f;
				//float y = display.localScale.y/1.005f;
				//float z = display.localScale.z;
				//display.localScale = new Vector3(x, y, z);
			// }
			
			if (returnWispTimer <= 0){
				returnWispTimer = WispTimerMax;
				Debug.Log("failed to get the Wisp back in time");       //can be replaced with the desired commands
				StartCoroutine(LoseWisp());
				gotWisp = false;
			}
		}
	}
	
	
	public void GotTheWisp(GameObject wisp){
		if (gotWisp == false){
			gotWisp = true;
			theWisp = wisp;
			wispStartingPos = new Vector2(wisp.transform.position.x, wisp.transform.position.y);
		}
	}
	
	IEnumerator LoseWisp(){
		//make the wisp dissolve? 
		//display a fail message
		theWisp.transform.position = wispStartingPos;
		theWisp.GetComponent<Wisp_Behavior>().isCaptured = false;
		failMessage.SetActive(true);
		yield return new WaitForSeconds(4f);
		failMessage.SetActive(false);
	}
	
	
}