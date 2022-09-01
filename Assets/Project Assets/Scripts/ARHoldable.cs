using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARHoldable : MonoBehaviour
{
    private bool isHolded = false;

    private void Update(){
        if(Input.touchCount == 0)
            return;

        Touch touch = Input.touches[0];

        if(touch.phase == TouchPhase.Began){
            HoldStart(touch);
        }else if(touch.phase == TouchPhase.Moved){
            Move(touch);
        }else if(touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled){
            HoldEnd(touch);
        }
    }

    private void HoldStart(Touch _touch){
        Ray ray = Camera.main.ScreenPointToRay(_touch.position);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit)){
            if(hit.collider.tag == "Holdable"){
                GameObject cam = Camera.main.gameObject;
                hit.collider.gameObject.transform.parent = cam.transform;
            }
        }
    }

    private void Move(Touch _touch){

    }

    private void HoldEnd(Touch _touch){
        
    }
}
