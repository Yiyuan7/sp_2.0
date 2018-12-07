using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.XR.iOS;




public class MyLog : MonoBehaviour
{
    string myLog;
    Queue myLogQueue = new Queue();
    bool state;
    [SerializeField]
    protected Button GetLogButton;

    void Start()
    {

        state = false;
        Button logbutton = GetLogButton.GetComponent<Button>();
        logbutton.onClick.AddListener(ChangeState);
    }

    void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        myLog = logString;
        string newString = "\n [" + type + "] : " + myLog;
        myLogQueue.Enqueue(newString);
        while (myLogQueue.Count > 16)
        {
            myLogQueue.Dequeue();
        }

            if (type == LogType.Exception)
        {
            newString = "\n" + stackTrace;
            myLogQueue.Enqueue(newString);
        }
        myLog = string.Empty;
        foreach (string mylog in myLogQueue)
        {
            myLog += mylog;
        }
    }

    void OnGUI()
    {
        //GUILayout.Label(myLog);
        if (state){
            GUI.Label(new Rect(10, 10, 300, 300), myLog);
        }

    }

    void ChangeState(){

        state = !state;
    }
}