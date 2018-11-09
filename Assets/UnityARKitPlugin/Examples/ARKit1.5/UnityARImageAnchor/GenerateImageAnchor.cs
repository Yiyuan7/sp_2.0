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

    private Vector3 markerPosition;
    private Quaternion markerRotation;

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
		if (arImageAnchor.referenceImageName == referenceImage.imageName) {
            if (arImageAnchor.isTracked)
            {
                if (!imageAnchorGO.activeSelf)
                {
                    imageAnchorGO.SetActive(true);
                }
                imageAnchorGO.transform.position = UnityARMatrixOps.GetPosition(arImageAnchor.transform);
                imageAnchorGO.transform.rotation = UnityARMatrixOps.GetRotation(arImageAnchor.transform);
            }
            else if (imageAnchorGO.activeSelf)
            {
                imageAnchorGO.SetActive(false);
            }
        }

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
