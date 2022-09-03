using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scope : MonoBehaviour
{
    void Update()
    {
        
    }

    /// <summary>
    /// OnTriggerStay is called once per frame for every Collider other
    /// that is touching the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "SunParticle"){
            Debug.Log("Scoping...");
        }
    }
}
