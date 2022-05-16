 using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StopMusic : MonoBehaviour {

        void Start() {
                BGSoundScript.Instance.gameObject.GetComponent<AudioSource>().Pause();
        }
}
