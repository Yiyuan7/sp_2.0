using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class camera_line : MonoBehaviour {

    [SerializeField]
    protected GameObject line;
    [SerializeField]
    protected GameObject sphereEnd;

    private const float m_MoveSpeed = 0.05f;
    float distance = 1;


    public static Vector3 cubeposition;

    // Use this for initialization
    void Start () {


    }
	
	// Update is called once per frame
	void Update () {

        line.transform.SetParent(Camera.main.gameObject.transform);
        line.transform.localPosition = Vector3.forward * distance;
        line.transform.position = Vector3.Lerp(line.transform.position, line.transform.localPosition, m_MoveSpeed);
        line.transform.rotation = Input.gyro.attitude;
        cubeposition = sphereEnd.transform.position;
        Debug.Log("cube" + cubeposition);
        Debug.Log("Camera Position:"+Camera.main.transform.position);
        Debug.Log("Line Position:" + line.transform.position);

    }
}
