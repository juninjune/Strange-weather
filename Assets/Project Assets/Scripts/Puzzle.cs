using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    public GameObject[] pieceHolders;
    
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
        piece.transform.SetPositionAndRotation(pieceHolders[i].transform.position, pieceHolders[i].transform.rotation);
        isFits[i] = true;

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
        Debug.Log("Puzzle Solved!!!");
    }
}
