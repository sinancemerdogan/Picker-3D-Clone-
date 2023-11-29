using System;
using System.Collections;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class LevelPlatform : MonoBehaviour {
    private Phase[] phases;
    private LevelEndTunnel levelEndTunnel;
    private Player player;
    private UICanvas uiCanvas;
    private bool isCompleted = false;
    private bool isLevelOver = false;
    private int currentPhaseIndex;
    private float platformLength = 377f;
    private bool[] phaseCompletions;
    private int levelNumber = 1;


    private void Start() {
        phases = transform.GetComponentsInChildren<Phase>().OrderBy(p => p.gameObject.name).ToArray();
        levelEndTunnel = GetComponentInChildren<LevelEndTunnel>();
        player = FindObjectOfType<Player>();
        uiCanvas = FindObjectOfType<UICanvas>();
        phaseCompletions = new bool[phases.Length];
        currentPhaseIndex = 0;
        uiCanvas.SetLevelTexts(levelNumber);

        for (int i = 0; i < phaseCompletions.Length; i++) {
            phaseCompletions[i] = phases[i].GetIsCompleted();
        }
        for (int i = 0; i < phases.Length; i++) {
            phases[i].InstantiateObjectsInPhase(levelNumber, i);
        }
    }

    public void ResetPlatform() {
        isCompleted = false;
        isLevelOver = false;
        phaseCompletions = new bool[phases.Length];
        currentPhaseIndex = 0;

        for (int i = 0; i < phases.Length; i++) {
            phaseCompletions[i] = false;
            phases[i].SetIsCompleted(false);
            phases[i].ResetObjectsInPhase();
            phases[i].InstantiateObjectsInPhase(levelNumber, i);
            phases[i].TransitionToNormal();
            phases[i].ResetPoolInPhase();
        }
    }

    private void Update() {
        if (!isLevelOver) {
            if (player.GetIsStopped()) {
                CheckForPhaseCompletion();
                StartCoroutine(WaitForPlayerStopAndCheckForFail());
            }

            if (phaseCompletions.All(value => value)) {
                CheckForLevelCompletion();
            }

            if (!player.GetWaitForTap()) {
                uiCanvas.DeactivateStartText();
            }
        }
    }

    private IEnumerator WaitForPlayerStopAndCheckForFail() {
        yield return new WaitForSeconds(4f);
        if (player.GetIsStopped()) {
            isLevelOver = true;
            uiCanvas.OnFail();
        }
    }

    private void CheckForPhaseCompletion() {
        if (!phaseCompletions.All(value => value) && phases[currentPhaseIndex].GetIsCompleted()) {
            phaseCompletions[currentPhaseIndex] = true;
            uiCanvas.OnPhaseCompletion(currentPhaseIndex);
            currentPhaseIndex++;
            StartCoroutine(TransitionWithDelay(phases[currentPhaseIndex - 1], 1f));
        }
    }

    private IEnumerator TransitionWithDelay(Phase phase, float delayTime) {
        phase.TransitionToNextPhase();
        yield return new WaitForSeconds(delayTime);
        player.SetIsStopped(false);
    }

    private void CheckForLevelCompletion() {
        if (levelEndTunnel.GetHasPlayerArrived()) {
            isCompleted = true;
            isLevelOver = true;
            levelNumber++;
            if(levelNumber > 3) {
                levelNumber = 1;
            }
            uiCanvas.SetLevelTexts(levelNumber);
            uiCanvas.ResetIndicators();
        }
    }

    private void OnTriggerEnter(Collider collision) {
        uiCanvas.OnLevelComplete();
        Time.timeScale = 0;
        GetComponent<Collider>().enabled = false;
    }

    public bool GetIsCompleted() {
        return this.isCompleted;
    }

    public void SetLevelNumber(int levelNumber) {
        this.levelNumber = levelNumber;
    }

    public float GetPlatformLength() {
        return this.platformLength;
    }
}