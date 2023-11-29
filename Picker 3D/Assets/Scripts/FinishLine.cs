using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour {
    private Animator animator;
   
    private void Start() {
        animator = GetComponent<Animator>();
    }
    private void OnDisable() {
        animator.SetBool("IsPhaseCompleted", false);
    }

    public void PlayFinishLineAnimation() {
        animator.SetBool("IsPhaseCompleted", true);
    }

    public void ResetAnimation() {
        animator.SetBool("IsPhaseCompleted", false);
    }
}
