using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OriginAnchor : MonoBehaviour
{

    public GameObject arCamera;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.SetPositionAndRotation(arCamera.transform.position, Quaternion.identity);
    }

    public void ResetAnchor()
    {
        transform.SetPositionAndRotation(arCamera.transform.position, Quaternion.Euler(0, arCamera.transform.eulerAngles.y, 0));
    }
}
