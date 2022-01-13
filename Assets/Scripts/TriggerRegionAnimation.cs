using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerRegionAnimation : MonoBehaviour
{
    [SerializeField]
    private Animator regionAnimator;
    
    [SerializeField]
    private string animationName = "regioncanvasscale";
    void OnTriggerEnter(Collider other){
        regionAnimator.Play(animationName,0,0.0f);
    }
}
