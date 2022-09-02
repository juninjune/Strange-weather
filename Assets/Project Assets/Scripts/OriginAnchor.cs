using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OriginAnchor : MonoBehaviour
{
    public GameObject arCamera;

    private GameObject arCamTracker;
    private bool isSet = false;

    private void Update(){
        if(!isSet)
            return;

        arCamTracker.transform.position = arCamera.transform.position;
        OSCManager.instance.SendPosition(arCamTracker.transform.localPosition);
    }

    public void ResetAnchor()
    {
        int layerMask = 1 << LayerMask.NameToLayer("Plane");

        Ray ray = new Ray(arCamera.transform.position, Vector3.down);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask)){
            transform.SetPositionAndRotation(hit.point, Quaternion.Euler(0, arCamera.transform.eulerAngles.y, 0));
            if(arCamTracker){
                Destroy(arCamTracker);
            }
            arCamTracker = new GameObject("arCamTracker");
            arCamTracker.transform.parent = this.gameObject.transform;
            isSet = true;
        }
    }
}
