using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class camera_line : MonoBehaviour {

    [SerializeField]
    protected GameObject line;
    [SerializeField]
    protected GameObject sphereEnd;



    float cylinderSize;


    public static Vector3 cubeposition;
    public static Vector3 cuberelativeposition;

    // Use this for initialization
    void Start () {


    }
	
	// Update is called once per frame
	void Update () {


        cubeposition = sphereEnd.transform.position;
        //Debug.Log("cube" + cubeposition);
        //Debug.Log("Camera Position:"+Camera.main.transform.position);
        //Debug.Log("Line Position:" + line.transform.position);
        cylinderSize = line.GetComponent<MeshFilter>().mesh.bounds.size.y * line.transform.localScale.y;
        //Debug.Log("the distance between phone and object is " + cylinderSize);
        //Debug.Log("cubeposition" + cubeposition);
        //Debug.Log("end"+sphereEnd.transform.position);
        //Debug.Log("cy" + line.transform.position);

    }
}
