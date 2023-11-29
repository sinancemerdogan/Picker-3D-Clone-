using System.Collections;
using TMPro;
using UnityEngine;

public class Pool : MonoBehaviour {
    public int numberOfObjectsToComplete = 0;
    private int numberOfObjectsInPool = 0;
    private TextMeshPro scoreText;
    private bool isCompleted = false;

    private void Start() {
        scoreText = GetComponentInChildren<TextMeshPro>();
        if (scoreText == null) {
            Debug.LogError("TextMeshPro component not found in the scene.");
        }
        else {
            scoreText.text = numberOfObjectsInPool + "/" + numberOfObjectsToComplete;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Object")) {
            numberOfObjectsInPool++;
            scoreText.text = numberOfObjectsInPool + "/" + numberOfObjectsToComplete;
            StartCoroutine(DelayedCompletionCheck(2.0f));
        }
    }

    private IEnumerator DelayedCompletionCheck(float delayTime) {
        yield return new WaitForSeconds(delayTime);

        if (numberOfObjectsInPool >= numberOfObjectsToComplete) {
            isCompleted = true;
        }
    }

    public bool GetIsCompleted() {
        return isCompleted;
    }

    public void SetIsCompleted(bool isCompleted) {
        this.isCompleted = isCompleted;
    }

    public void SetNumberOfObjectsToComplete(int numberOfObjectsToComplete) {
        this.numberOfObjectsToComplete = numberOfObjectsToComplete;
    }

    public void ResetPool() {
        numberOfObjectsInPool = 0;
        isCompleted = false;
        scoreText.text = numberOfObjectsInPool + "/" + numberOfObjectsToComplete;
    }
}
