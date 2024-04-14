using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class DetectTouch : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _prefabToPlace;

    // List for raycast hits is re-used by raycast manager
    private static readonly List<ARRaycastHit> Hits = new List<ARRaycastHit>();
    Quaternion originRotation = Quaternion.Euler(x:0, y:0, z:0);


    void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Only consider single-finger touches that are beginning
        Touch touch;

        //if (Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began) { return; }

        //Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        if (Input.GetMouseButtonDown(0))
        {
            
        } else
        {
            return;
        }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit raycastHit;
        if (Physics.Raycast(ray, out raycastHit, 1000))
        {
            Debug.Log($"Instantiated on: {raycastHit.transform.tag}");
            Vector3 hitPosition = raycastHit.transform.gameObject.transform.position;
            GameObject btn = raycastHit.transform.gameObject;
            string photoUrl = btn.GetComponent<MediaSource>().photoUrl;
            string videoUrl = btn.GetComponent<MediaSource>().videoUrl;
            string infoText = btn.GetComponent<MediaSource>().infoText;
            switch (raycastHit.transform.tag)
            {
                case "Image":
                    Debug.Log(btn.GetComponent<MediaSource>().isMediaActived);
                    if (btn.GetComponent<MediaSource>().isMediaActived)
                    {
                        Destroy(btn.GetComponent<MediaSource>().screen);
                        btn.GetComponent<MediaSource>().isMediaActived = false;
                    }
                    else
                    {
                        btn.GetComponent<MediaSource>().screen = Instantiate(_prefabToPlace[0], getPosition(pos: hitPosition, rotation: raycastHit.transform.rotation), getRotation(btnRotation: raycastHit.transform.rotation, screenRotation: _prefabToPlace[0].transform.rotation));
                        btn.GetComponent<MediaSource>().screen.AddComponent<ARAnchor>();
                        btn.GetComponent<MediaSource>().screen.GetComponentInChildren<LoadImageFromUrl>().loadImg(url: photoUrl);
                        btn.GetComponent<MediaSource>().isMediaActived = true;
                    }
                    break;
                case "Video":
                    Debug.Log(raycastHit.transform.rotation.eulerAngles);
                    if (btn.GetComponent<MediaSource>().isMediaActived)
                    {
                        Destroy(btn.GetComponent<MediaSource>().screen);
                        btn.GetComponent<MediaSource>().isMediaActived = false;
                    }
                    else
                    {
                        btn.GetComponent<MediaSource>().screen = Instantiate(_prefabToPlace[1], getPosition(pos: hitPosition, rotation: raycastHit.transform.rotation), getRotation(btnRotation: raycastHit.transform.rotation, screenRotation: _prefabToPlace[1].transform.rotation));
                        btn.GetComponent<MediaSource>().screen.AddComponent<ARAnchor>();
                        btn.GetComponent<MediaSource>().screen.GetComponentInChildren<OnlineVideoLoader>().playVideo(videoUrl);
                        Debug.Log(videoUrl);

                        btn.GetComponent<MediaSource>().isMediaActived = true;
                    }
                    break;
                case "Text":
                    Debug.Log(raycastHit.transform.rotation.eulerAngles);
                    if (btn.GetComponent<MediaSource>().isMediaActived)
                    {
                        Destroy(btn.GetComponent<MediaSource>().screen);
                        btn.GetComponent<MediaSource>().isMediaActived = false;
                    }
                    else
                    {
                        btn.GetComponent<MediaSource>().screen = Instantiate(_prefabToPlace[2], getPosition(pos: hitPosition, rotation: raycastHit.transform.rotation), getRotation(btnRotation: raycastHit.transform.rotation, screenRotation: _prefabToPlace[1].transform.rotation));
                        btn.GetComponent<MediaSource>().screen.AddComponent<ARAnchor>();
                        btn.GetComponent<MediaSource>().screen.GetComponentInChildren<InformationLoader>().loadText(infoText);
                        btn.GetComponent<MediaSource>().isMediaActived = true;
                    }
                    break;
                // Add more cases as needed
                default:
                    // Code to execute if variable doesn't match any case
                    break;
            }

        }

        Quaternion getRotation(Quaternion btnRotation, Quaternion screenRotation)
        {
            Quaternion rotationDifference = btnRotation * Quaternion.Inverse(originRotation);

            // Combine the rotations
            Quaternion finalRotation = rotationDifference * screenRotation;
            // Return the combined rotation
            return finalRotation;
        }

        Vector3 getPosition(Vector3 pos, Quaternion rotation)
        {

            // Define the right vector (positive X-axis)
            Vector3 rightVector = Vector3.right;

            // Rotate the right vector by the origin rotation
            Vector3 rotatedRightVector = rotation * rightVector;

            // Define the distance to the right
            float distanceToRight = 0.6f; // Adjust as needed

            // Calculate the position to the right
            Vector3 positionToRight = pos + (rotatedRightVector * distanceToRight);

            return positionToRight;
        }

    }
}
