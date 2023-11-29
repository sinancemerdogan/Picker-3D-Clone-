using System.Collections;
using UnityEngine;

public class Road : MonoBehaviour {
    private Animator animator;
    private GameObject roadEnd;

    private void Start() {
        animator = GetComponent<Animator>();
        roadEnd = transform.Find("Phase End").gameObject;
    }

    public void ResetAnimation() {
        animator.SetBool("IsPhaseCompleted", false);
    }
      
    public void PlayRoadAnimation() {
        animator.SetBool("IsPhaseCompleted", true);
    }

    public void DeactivatePhaseEnd() {
        roadEnd.SetActive(false);
    }

    public void ActivatePhaseEnd() {
        roadEnd.SetActive(true);
    }
}
