using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;

public class GenerateImageAnchor : MonoBehaviour {


	[SerializeField]
	private ARReferenceImage referenceImage;

	[SerializeField]
	private GameObject prefabToGenerate;

	private GameObject imageAnchorGO;

    public static Vector3 markerPosition;
    public static Quaternion markerRotation;

    public static GenerateImageAnchor GenerateImageAnchorInstance { get; private set; }

    // Use this for initialization
    void Start()
    {
        UnityARSessionNativeInterface.ARImageAnchorAddedEvent += AddImageAnchor;
        //UnityARSessionNativeInterface.ARImageAnchorUpdatedEvent += UpdateImageAnchor;
        //UnityARSessionNativeInterface.ARImageAnchorRemovedEvent += RemoveImageAnchor;

    }

	void AddImageAnchor(ARImageAnchor arImageAnchor)
	{
		Debug.LogFormat("image anchor added[{0}] : tracked => {1}", arImageAnchor.identifier, arImageAnchor.isTracked);
		if (arImageAnchor.referenceImageName == referenceImage.imageName) {
            markerPosition = UnityARMatrixOps.GetPosition (arImageAnchor.transform);
            markerRotation = UnityARMatrixOps.GetRotation (arImageAnchor.transform);
            Debug.Log("Marker Position: " + markerPosition);
            Debug.Log("Marker Rotation: " + markerRotation);

            imageAnchorGO = Instantiate<GameObject>(prefabToGenerate, markerPosition, markerRotation);

            GameObject markerText = GameObject.Find("markerLocation");
            //GameObject markerFoundPrompt = new GameObject();
            //markerFoundPrompt.AddComponent<TextMesh>();
            //markerFoundPrompt.GetComponent<TextMesh>().text = "Marker position: " + markerPosition + ". MarkerRotation: " + markerRotation ;
            //Vector3 worldPoint = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
            //imageAnchorGO = Instantiate<GameObject> (markerFoundPrompt, worldPoint, Quaternion.identity);
            //Debug.Log(markerFoundPrompt.GetComponent<TextMesh>().text);
            //Debug.Log(markerFoundPrompt.GetComponent<TextMesh>().transform.position);
            //Debug.Log(markerFoundPrompt.GetComponent<TextMesh>().transform.rotation);
        }
	}

	void UpdateImageAnchor(ARImageAnchor arImageAnchor)
	{
		Debug.LogFormat("image anchor updated[{0}] : tracked => {1}", arImageAnchor.identifier, arImageAnchor.isTracked);

        if (arImageAnchor.referenceImageName == referenceImage.imageName)
        {
            imageAnchorGO.transform.position = UnityARMatrixOps.GetPosition(arImageAnchor.transform);
            imageAnchorGO.transform.rotation = UnityARMatrixOps.GetRotation(arImageAnchor.transform);
        }


        //if (arImageAnchor.referenceImageName == referenceImage.imageName) {
        //    if (arImageAnchor.isTracked)
        //    {
        //        if (!imageAnchorGO.activeSelf)
        //        {
        //            imageAnchorGO.SetActive(true);
        //        }
        //        imageAnchorGO.transform.position = UnityARMatrixOps.GetPosition(arImageAnchor.transform);
        //        imageAnchorGO.transform.rotation = UnityARMatrixOps.GetRotation(arImageAnchor.transform);
        //    }
        //    else if (imageAnchorGO.activeSelf)
        //    {
        //        imageAnchorGO.SetActive(false);
        //    }
        //}

	}

	void RemoveImageAnchor(ARImageAnchor arImageAnchor)
	{
		Debug.LogFormat("image anchor removed[{0}] : tracked => {1}", arImageAnchor.identifier, arImageAnchor.isTracked);
		if (imageAnchorGO) {
            GameObject.Destroy (imageAnchorGO);
		}

	}

	void OnDestroy()
	{
		UnityARSessionNativeInterface.ARImageAnchorAddedEvent -= AddImageAnchor;
		UnityARSessionNativeInterface.ARImageAnchorUpdatedEvent -= UpdateImageAnchor;
		UnityARSessionNativeInterface.ARImageAnchorRemovedEvent -= RemoveImageAnchor;

	}

	// Update is called once per frame
	void Update () {

	}
}



//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.XR.iOS;

//public class GenerateImageAnchor : MonoBehaviour
//{


//    [SerializeField]
//    private ARReferenceImage referenceImage;

//    [SerializeField]
//    private GameObject prefabToGenerate;

//    private GameObject imageAnchorGO;

//    // Use this for initialization
//    void Start()
//    {
//        //UnityARSessionNativeInterface.ARImageAnchorAddedEvent += AddImageAnchor;
//        //UnityARSessionNativeInterface.ARImageAnchorUpdatedEvent += UpdateImageAnchor;
//        //UnityARSessionNativeInterface.ARImageAnchorRemovedEvent += RemoveImageAnchor;

//    }

//    void AddImageAnchor(ARImageAnchor arImageAnchor)
//    {
//        Debug.Log("image anchor added");
//        if (arImageAnchor.referenceImageName == referenceImage.imageName)
//        {
//            Vector3 position = UnityARMatrixOps.GetPosition(arImageAnchor.transform);
//            Quaternion rotation = UnityARMatrixOps.GetRotation(arImageAnchor.transform);

//            imageAnchorGO = Instantiate<GameObject>(prefabToGenerate, position, rotation);
//        }
//    }

//    void UpdateImageAnchor(ARImageAnchor arImageAnchor)
//    {
//        Debug.Log("image anchor updated");
//        if (arImageAnchor.referenceImageName == referenceImage.imageName)
//        {
//            imageAnchorGO.transform.position = UnityARMatrixOps.GetPosition(arImageAnchor.transform);
//            imageAnchorGO.transform.rotation = UnityARMatrixOps.GetRotation(arImageAnchor.transform);
//        }

//    }

//    void RemoveImageAnchor(ARImageAnchor arImageAnchor)
//    {
//        Debug.Log("image anchor removed");
//        if (imageAnchorGO)
//        {
//            GameObject.Destroy(imageAnchorGO);
//        }

//    }

//    void OnDestroy()
//    {
//        UnityARSessionNativeInterface.ARImageAnchorAddedEvent -= AddImageAnchor;
//        UnityARSessionNativeInterface.ARImageAnchorUpdatedEvent -= UpdateImageAnchor;
//        UnityARSessionNativeInterface.ARImageAnchorRemovedEvent -= RemoveImageAnchor;

//    }

//    // Update is called once per frame
//    void Update()
//    {

//    }
//}
