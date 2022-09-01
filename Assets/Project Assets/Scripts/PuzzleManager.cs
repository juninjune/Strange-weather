#if AR_FOUNDATION_5_0_OR_NEWER
    using ARSessionOrigin = Unity.XR.CoreUtils.XROrigin;
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField] ARRaycastManager raycastManager = null;
    [SerializeField] TrackableType raycastMask = TrackableType.PlaneWithinPolygon;

    [SerializeField] GameObject puzzleHolder;
    [SerializeField] GameObject puzzlePiecePrefab;
    [SerializeField] int remainPiece = 4;

    private bool isPuzzleHolderInitialized = false;

    public void Update(){
        if(Input.touchCount == 0)
            return;

        Touch touch = Input.GetTouch(0);

        if(touch.phase == TouchPhase.Began){
            var ray = Camera.main.ScreenPointToRay(touch.position);
            var arHits = new List<ARRaycastHit>();
            var arHasHit = raycastManager.Raycast(ray, arHits, raycastMask);
            RaycastHit hit;

            if (arHasHit) {
                Physics.Raycast(ray, out hit);

                if(!isPuzzleHolderInitialized){
                    SetupPuzzleHolder(hit.point);
                }else{
                    SetupPuzzlePiece(hit.point + new Vector3(0, 0.3f, 0));
                }
            }
        }
    }

    public void SetupPuzzleHolder(Vector3 position){
        Instantiate(puzzleHolder, position, Quaternion.identity);
        isPuzzleHolderInitialized = true;
    }

    public void SetupPuzzlePiece(Vector3 position){
        PuzzlePiece piece = Instantiate(puzzlePiecePrefab, position, Quaternion.identity).GetComponent<PuzzlePiece>();
        remainPiece--;

        piece.Initiate(remainPiece);

        if(remainPiece == 0){
            Destroy(this.gameObject);
        }
    }
}
