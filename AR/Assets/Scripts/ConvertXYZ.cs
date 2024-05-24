using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ConvertXYZ : MonoBehaviour
{
    private void Start()
    {
        Vector3 rotation = new Vector3(0, 180, 0);
        var position = new Vector3(5, 0, -7);
        //Debug.Log(BvhToUnityRotation(rotation));
    }

    public static Vector3 convertPos(Vector3 rightHandPos)
    {
        Vector3 leftHandPos = new Vector3(rightHandPos.x, rightHandPos.y, -rightHandPos.z);
        return leftHandPos;
    }

    public static Vector3 convertRot(Vector3 rightHandRot)
    {
        Vector3 leftHandRot = new Vector3(rightHandRot.x,180 - rightHandRot.y,-rightHandRot.z);
        return leftHandRot;
    }

}
