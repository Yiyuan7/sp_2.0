using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class showlog2 : MonoBehaviour {
    public Text displayText;

    public void DisplayText()
    {
        displayText.text = "What ever you want to display";
    }
    // Use this for initialization
    void Start () {
        displayText.text = "";

    }

    // Update is called once per frame
    void Update () {
		
	}
}
