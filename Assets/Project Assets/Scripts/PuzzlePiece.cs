using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    [SerializeField] float correctDistance = 0.3f;
    [SerializeField] Material[] materials;
    private Puzzle puzzle;
    private int index;
    private bool isHolded = false;
    private bool isFit = false;

    public void Initiate(int i){
        puzzle = FindObjectOfType<Puzzle>();
        GetComponent<Renderer>().material = materials[i];
        index = i;
    }

    private void Update(){
        if(isFit)
            return;
        if(Input.touchCount == 0){
            return;
        }

        Touch touch = Input.touches[0];

        switch(touch.phase)
        {
            case TouchPhase.Began:
                HoldStart(touch);
                break;

            case TouchPhase.Moved:
                Move(touch);
                break;

            case TouchPhase.Stationary:
                Move(touch);
                break;
            
            case TouchPhase.Ended:
                HoldEnd();
                break;

            case TouchPhase.Canceled:
                HoldEnd();
                break;
        }
    }

    private void HoldStart(Touch _touch){
        Ray ray = Camera.main.ScreenPointToRay(_touch.position);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit)){
            if(hit.collider.gameObject == this.gameObject){
                isHolded = true;
            }
        }
    }

    private void Move(Touch _touch){
        if(!isHolded)
            return;

        int layerMask = 1 << LayerMask.NameToLayer("Plane");

        Ray ray = Camera.main.ScreenPointToRay(_touch.position);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask)){
            transform.position = hit.point + new Vector3(0, 0.15f, 0);
            if(CheckCorrect(hit.point)){
                puzzle.AttachPiece(this.gameObject, index);
                isFit = true;
            }
        }
    }

    private void HoldEnd(){
        isHolded = false;
    }

    private bool CheckCorrect(Vector3 position){
        Vector3 target = puzzle.GetHolderPosition(index);

        if(Vector3.Distance(position, target) < correctDistance){
            return true;
        }else
        {
            return false;
        }
    }
}
