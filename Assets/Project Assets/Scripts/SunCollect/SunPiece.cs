using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunPiece : MonoBehaviour
{
    [SerializeField] ParticleSystem sphere;
    [SerializeField] ParticleSystem inside;
    [SerializeField] ParticleSystem outside;
    [SerializeField] GameObject pop;


    private Scope scope;
    Collider scopeCollider;

    private SunMaker sunMaker;

    private bool isScoped = false;
    private bool isSolved = false;

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    private void OnEnable()
    {
        scope = FindObjectOfType<Scope>();
        scopeCollider = scope.GetComponent<Collider>();

        sunMaker = FindObjectOfType<SunMaker>();
    }

    private void FixedUpdate()
    {
        if(isSolved)
            return;

        CheckScoped();
    }

    private void OnScopeEnter()
    {
        Solve();
    }

    private void OnScopeExit()
    {
    }

    private void OnScopeStay()
    {
    }

    private void CheckScoped()
    {
        if(scopeCollider.bounds.Contains(transform.position)){
            if(isScoped)
            {
                OnScopeStay();    
            }else
            {
                OnScopeEnter();
                isScoped = true;
            }
        }else
        {
            if(isScoped)
            {
                OnScopeExit();
                isScoped = false;
            }
        }
    }

    private void Solve()
    {
        AudioManager.instance.Play(0);
        Handheld.Vibrate();
        sunMaker.CollectPiece();
        sphere.Play();
        outside.Play();

        isSolved = true;
        Invoke("SolveDone", 3f);
    }

    private void SolveDone(){
        AudioManager.instance.Play(1);
        Instantiate(pop, transform.position, Quaternion.identity);
        sphere.Stop();
        outside.Stop();
        inside.Stop();
    }
}
