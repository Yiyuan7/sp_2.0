using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chooselogo : MonoBehaviour {
    public GameObject desk;
    public GameObject chair;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Desk(){

        desk.GetComponent<UnityEngine.XR.iOS.UnityARHitTestExample>().enabled = true;
        chair.GetComponent<UnityEngine.XR.iOS.UnityARHitTestExample>().enabled = false;

    }

    public void Chair()
    {
        chair.GetComponent<UnityEngine.XR.iOS.UnityARHitTestExample>().enabled = true;
        desk.GetComponent<UnityEngine.XR.iOS.UnityARHitTestExample>().enabled = false;
    }
     

}
