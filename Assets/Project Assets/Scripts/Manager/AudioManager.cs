using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    private AudioSource audioSource;
    [SerializeField] private AudioClip[] clips;

    private void Start(){
        if(instance == null){
            instance = this;
        }else
        {
            Destroy(this.gameObject);
        }

        audioSource = GetComponent<AudioSource>();
    }

    public void Play(int _index){
        if(_index >= clips.Length)
            return;

        audioSource.clip = clips[_index];
        audioSource.Play();
    }
}
