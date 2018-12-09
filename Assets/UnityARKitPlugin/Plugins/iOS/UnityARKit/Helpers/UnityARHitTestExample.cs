using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.XR.iOS;
using UnityEngine;

namespace UnityEngine.XR.iOS
{

    public class UnityARHitTestExample : MonoBehaviour
    {
        public Transform m_HitTransform;
        public float maxRayDistance = 30.0f;
        public LayerMask collisionLayer = 1 << 10;  //ARKitPlane layer
        public Vector3 transposition;
        public static Quaternion transorientation;
        public Vector3 deviceposition;

        [SerializeField]
        protected Button GetDeviceLocationButton;

        [SerializeField]
        protected Button SpawnDesk;

        [SerializeField]
        protected Button SpawnChair;

        [SerializeField]
        public LineRenderer m_LineRenderer;

        [SerializeField]
        public TextMesh m_DistanceTextHldr;

        public string objectChosen = "112233";

        public bool showline = true;

        public static Vector3 relativeToolPosition;
        public static Vector3 camera_linePosition;
        public float distance = 0.2f;

        public bool status = false;

        // for recording the position
        public List<Vector3> deskposition = new List<Vector3>();
        public List<Vector3> chairposition = new List<Vector3>();
        public List<Quaternion> deskorientation = new List<Quaternion>();
        public List<Quaternion> chairorientation = new List<Quaternion>();
        public List<Vector3> phoneposition = new List<Vector3>();
        public List<Quaternion> phoneorientation = new List<Quaternion>();

        [SerializeField]
        protected Button showdistanceButton;


        private Button phoneButton;

        private float counter = 0f;

        private float linedistance = 0f;

        public const float r_LineDrawSpeed = 0.005f;


        private void Start()
        {
            phoneButton = GetDeviceLocationButton.GetComponent<Button>();
            phoneButton.onClick.AddListener(UpdateDeviceLocation);
            Button Dbtn = SpawnDesk.GetComponent<Button>();
            Dbtn.onClick.AddListener(spawndesk);
            Button Cbtn = SpawnChair.GetComponent<Button>();
            Cbtn.onClick.AddListener(spawnchair);

            Button linebutton = showdistanceButton.GetComponent<Button>();
            linebutton.onClick.AddListener(ChangeState);

        }


        bool HitTestWithResultType(ARPoint point, ARHitTestResultType resultTypes)
        {
            List<ARHitTestResult> hitResults = UnityARSessionNativeInterface.GetARSessionNativeInterface().HitTest(point, resultTypes);
            if (hitResults.Count > 0)
            {
                foreach (var hitResult in hitResults)
                {
                    Debug.Log("Got hit!");
                    m_HitTransform.position = UnityARMatrixOps.GetPosition(hitResult.worldTransform);
                    m_HitTransform.rotation = UnityARMatrixOps.GetRotation(hitResult.worldTransform);
                    transposition = m_HitTransform.position;
                    transorientation = m_HitTransform.rotation;
                    spawnlogo();

                    //Vector3 relativePosition = transposition - GenerateImageAnchor.GenerateImageAnchorInstance.markerPosition;
                    //Debug.Log (string.Format ("Relative Position: x:{0:0.######} y:{1:0.######} z:{2:0.######}", relativePosition.x, relativePosition.y, relativePosition.z));
                    //Debug.Log (string.Format(("Rotation: x:{0:0.######} y:{1:0.######} z:{2:0.######}"), transorientation.x, transorientation.y, transorientation.z));
                    return true;
                }
            }
            return false;
        }

        private bool IsPointerOverUIObject()
        {
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
            return results.Count > 0;
        }

        // Update is called once per frame
        void Update()
        {
#if UNITY_EDITOR   //we will only use this script on the editor side, though there is nothing that would prevent it from working on device
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                //we'll try to hit one of the plane collider gameobjects that were generated by the plugin
                //effectively similar to calling HitTest with ARHitTestResultType.ARHitTestResultTypeExistingPlaneUsingExtent
                if (Physics.Raycast(ray, out hit, maxRayDistance, collisionLayer))
                {
                    //we're going to get the position from the contact point
                    //m_HitTransform.position = hit.point;
                    transposition = hit.point;

                    //and the rotation from the transform of the plane collider
                    m_HitTransform.rotation = hit.transform.rotation;
                }
            }
#else
		if (Input.touchCount > 0 && m_HitTransform != null)
		{
			var touch = Input.GetTouch(0);
            if ((touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved) && !IsPointerOverUIObject())
			{
				var screenPosition = Camera.main.ScreenToViewportPoint(touch.position);
				ARPoint point = new ARPoint {
					x = screenPosition.x,
					y = screenPosition.y
				};

                // prioritize reults types
                ARHitTestResultType[] resultTypes = {
					//ARHitTestResultType.ARHitTestResultTypeExistingPlaneUsingGeometry,
                    ARHitTestResultType.ARHitTestResultTypeExistingPlaneUsingExtent, 
                    // if you want to use infinite planes use this:
                    //ARHitTestResultType.ARHitTestResultTypeExistingPlane,
                    //ARHitTestResultType.ARHitTestResultTypeEstimatedHorizontalPlane, 
					//ARHitTestResultType.ARHitTestResultTypeEstimatedVerticalPlane, 
					//ARHitTestResultType.ARHitTestResultTypeFeaturePoint
                }; 
				
                foreach (ARHitTestResultType resultType in resultTypes)
                {
                    if (HitTestWithResultType (point, resultType))
                    {
                        return;
                    }
                }
			}
		}
#endif
            if (status == true)
            {
                linerender();
            }
        }

        public void spawnchair()
        {
            //Debug.Log("SpawnChair Come!!!!!");
            objectChosen = "chair";
        }

        public void spawndesk()
        {
            //Debug.Log("SpawnDesk Come!!!!!");
            objectChosen = "desk";
        }

        public void spawnlogo()
        {
            // NOTE: temp comment
             relativeToolPosition = GenerateImageAnchor.markerPosition - transposition;

            if (objectChosen == "desk")
            {
                Instantiate(Resources.Load("red") as GameObject, transposition, transorientation);
                deskposition.Add(relativeToolPosition);
                deskorientation.Add(transorientation);
                Debug.Log("Desk position" + relativeToolPosition);
                Debug.Log("Desk rotation" + transorientation);


            }
            else if (objectChosen == "chair")
            {
                Instantiate(Resources.Load("yellow") as GameObject, transposition, transorientation);
                chairposition.Add(relativeToolPosition);
                chairorientation.Add(transorientation);
                Debug.Log("Chair position" + relativeToolPosition);
                Debug.Log("Chair rotation" + transorientation);
            }



        }

        protected void UpdateDeviceLocation()
        {
            Debug.Log("UpdateDeviceLocation");

            camera_linePosition = camera_line.cubeposition;
            // NOTE: temp comment
            Vector3 relativePhonePosition = GenerateImageAnchor.markerPosition - Camera.main.gameObject.transform.position;
            phoneposition.Add(Camera.main.transform.position + Camera.main.transform.forward * distance);
            phoneorientation.Add(Input.gyro.attitude);
            Debug.Log("Blue Cube position" + Camera.main.transform.position + Camera.main.transform.forward * distance);
            Debug.Log("Blue cube rotation" + Camera.main.transform.rotation);
            GameObject bluecube = Resources.Load("blue") as GameObject;
            bluecube.transform.localScale = new Vector3(.1f, .1f, .1f);
            Instantiate(bluecube, Camera.main.transform.position + Camera.main.transform.forward * distance, Camera.main.transform.rotation);
            status = true;
            int lenth = phoneposition.Count;
            m_LineRenderer.GetComponent<LineRenderer>().SetPosition(0, phoneposition[lenth - 1]);
            m_LineRenderer.GetComponent<LineRenderer>().startWidth = .01f;
            m_LineRenderer.GetComponent<LineRenderer>().endWidth = .01f;
            linedistance = Vector3.Distance(phoneposition[lenth - 1], phoneposition[lenth - 2]);
            Debug.Log("distance is " + linedistance);

        }

        void linerender()
        {
            int lenth = phoneposition.Count;


            if (phoneposition.Count >= 2)
            {
                float dis = linedistance;
                if (counter < dis)
                {
                    counter += r_LineDrawSpeed;
                    Vector3 currentdistance = counter * Vector3.Normalize(phoneposition[lenth - 2] - phoneposition[lenth - 1]) + phoneposition[lenth - 1];
                    m_LineRenderer.GetComponent<LineRenderer>().SetPosition(1, currentdistance);

                }
                else
                {
                    m_DistanceTextHldr.transform.position = (phoneposition[lenth - 1] - phoneposition[lenth - 2]) * 0.5f + phoneposition[lenth - 2];
                    m_DistanceTextHldr.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
                    m_DistanceTextHldr.text = dis.ToString("0.00") + "m";

                    status = false;

                    counter = 0;
                }

            }

        }

        void ChangeState()
        {

            showline = !showline;
            m_LineRenderer.GetComponent<Renderer>().enabled = showline;
            m_DistanceTextHldr.gameObject.SetActive(showline);
        }
    }
}



