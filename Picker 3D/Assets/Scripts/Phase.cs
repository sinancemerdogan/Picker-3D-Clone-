using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Phase : MonoBehaviour {
    private Road road;
    private FinishLine finishLine;
    private Pool pool;
    private ObjectsContainer objectsContainer;
    private PhaseConfigSO phaseConfigSO;
    private bool isCompleted = false;

    private void Start() {
        road = GetComponentInChildren<Road>();
        finishLine = GetComponentInChildren<FinishLine>();
        pool = GetComponentInChildren<Pool>();
        objectsContainer = GetComponentInChildren<ObjectsContainer>();
    }

    private void Update() {
        CheckForCompletion();
    }

    private void CheckForCompletion() {
        if (pool != null && pool.GetIsCompleted()) {
            isCompleted = true;
        }
    }

    public bool GetIsCompleted() {
        return isCompleted;
    }

    public void SetIsCompleted(bool isCompleted) {
        this.isCompleted = isCompleted;
    }

    public void TransitionToNextPhase() {
        road.PlayRoadAnimation();
        finishLine.PlayFinishLineAnimation();
        road.DeactivatePhaseEnd();
    }

    public void TransitionToNormal() {
        road.ResetAnimation();
        finishLine.ResetAnimation();
        road.ActivatePhaseEnd();
    }

    public void InstantiateObjectsInPhase(int levelNumber, int phaseNumber) {
        if(levelNumber > 3) {
            levelNumber = 1;
        }
        phaseConfigSO = Resources.Load<PhaseConfigSO>("ScriptableObjects/Level" + levelNumber + "/Phase" + phaseNumber);
        pool.SetNumberOfObjectsToComplete(phaseConfigSO.GetNumberOfObjectToComplete());
        objectsContainer.InstantiateObjects(phaseConfigSO.GetObjectPrefab(), phaseConfigSO.GetObjectPositionsList());
    }

    public void ResetPoolInPhase() {
        pool.ResetPool();
    }
    public void ResetObjectsInPhase() {
        objectsContainer.ResetObjectContainer();
    }
}
