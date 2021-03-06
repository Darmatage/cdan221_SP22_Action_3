using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractableDoor : MonoBehaviour{

        public string NextLevel = "MainMenu";
        public GameObject msgPressE;
		public GameObject msgWisp;
        public bool canPressE = false;
		public bool haveWisp = false;
		
		public bool level1 = true;
		public bool level2 = false;
		public bool level3 = false;
		
	void Start(){
		msgPressE.SetActive(false);
		msgWisp.SetActive(false);
	}

	void Update(){			
			
		if ((level1)&&(GameHandler.haveWisp1)){haveWisp=true; Debug.Log("Door sees wisp1");}	
		if ((level2)&&(GameHandler.haveWisp2)){haveWisp=true;}
		if ((level3)&&(GameHandler.haveWisp3)){haveWisp=true;}
			
		if ((canPressE == true) && (Input.GetKeyDown(KeyCode.E))){
			EnterDoor();
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Player"){
			if (haveWisp){
			msgPressE.SetActive(true);
			canPressE =true;
			} else {msgWisp.SetActive(true);}
		}
	}

        void OnTriggerExit2D(Collider2D other){
              if (other.gameObject.tag == "Player"){
                     msgPressE.SetActive(false);
                     canPressE = false;
					 msgWisp.SetActive(false);
              }
        }

      public void EnterDoor(){
            SceneManager.LoadScene (NextLevel);
      }

}