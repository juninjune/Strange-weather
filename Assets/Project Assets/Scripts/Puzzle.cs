using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    public GameObject[] pieceHolders;
    [SerializeField] GameObject particlePrefab;
    
    private bool[] isFits;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        isFits = new bool[pieceHolders.Length];
        for(int i=0; i<isFits.Length; i++){
            isFits[i] = false;
        }
    }

    public Vector3 GetHolderPosition(int i){
        return pieceHolders[i].transform.position;
    }

    public void AttachPiece(GameObject piece, int i){
        piece.transform.parent = pieceHolders[i].transform;
        piece.transform.SetPositionAndRotation(pieceHolders[i].transform.position, pieceHolders[i].transform.rotation);
        isFits[i] = true;
        AudioManager.instance.Play(1);
        Handheld.Vibrate();

        if(CheckDone()){
            Done();
        }
    }

    private bool CheckDone(){
        bool isDone = true;

        for(int i=0; i<isFits.Length; i++){
            if(!isFits[i]){
                isDone = false;
            }
        }

        return isDone;
    }

    private void Done(){
        AudioManager.instance.Play(2);
        Instantiate(particlePrefab, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
        OSCManager.instance.StartFx(1);
        gameObject.SetActive(false);
    }
}
