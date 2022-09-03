
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class SunMaker : MonoBehaviour
{
    [SerializeField] GameObject sunPiecePrefab;
    [SerializeField] int pieceCount = 6;
    [SerializeField] int remainPieceCount;

    private bool isInitialized = false;
    private bool isDone = false;
    private int solvedPiece = 0;

    private void Start()
    {
        remainPieceCount = pieceCount;
    }

    public void Update(){
        if(isInitialized)
            return;

        if(Input.touchCount == 0)
            return;

        Touch touch = Input.GetTouch(0);

        if(touch.phase == TouchPhase.Began){
            LayerMask mask = 1 << LayerMask.NameToLayer("Plane");
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask)) {
                InitiateSunPiece(hit.point);
                remainPieceCount--;
                if(remainPieceCount <= 0){
                    isInitialized = true;
                }
            }
        }
    }

    public void CollectPiece()
    {
        if(isDone)
            return;

        solvedPiece++;
        if(solvedPiece >= pieceCount) Done();
    }

    private void InitiateSunPiece(Vector3 _position)
    {
        Instantiate(sunPiecePrefab, _position + new Vector3(0,UnityEngine.Random.Range(0.5f, 1.2f),0), Quaternion.identity).transform.parent = transform;
    }

    private void Done()
    {
        AudioManager.instance.Play(2);
        OSCManager.instance.SendEventToTD(0);
        OSCManager.instance.StartFx(2);
        isDone = true;
    }
}
