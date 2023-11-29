using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    private LevelPlatform levelPlatformPrefab;
    private LevelPlatform currentLevelPlatform;
    private LevelPlatform previousLevelPlatform;
    private int currentLevel = 1;
    private int numberOfLevelsPlayedInASession = 0;
    private bool hasGameStarted = false;

    private ObjectPool<LevelPlatform> levelPlatformPool;

    private void Start() {
        levelPlatformPrefab = Resources.Load<LevelPlatform>("Prefabs/Level Platform");
        levelPlatformPool = new ObjectPool<LevelPlatform>(levelPlatformPrefab, transform);
        currentLevelPlatform = levelPlatformPool.GetPooledObject();
        PlayerPrefs.DeleteAll();
        if (PlayerPrefs.HasKey("Level")) {
            currentLevel = PlayerPrefs.GetInt("Level");
        }
        currentLevelPlatform.SetLevelNumber(currentLevel);
        Time.timeScale = 0;
    }

    private void Update() {
        if (!hasGameStarted) {
            if (Input.GetMouseButtonDown(0)) {
                Time.timeScale = 1;
                hasGameStarted = true;
            }
        }
        CheckLevelCompletion();
    }

    private void CheckLevelCompletion() {
        if (currentLevelPlatform.GetIsCompleted()) {
            currentLevel++;
            if (currentLevel > 3) {
                currentLevel = 1;
            }
            numberOfLevelsPlayedInASession++;
            previousLevelPlatform = currentLevelPlatform;
            previousLevelPlatform.SetLevelNumber(currentLevel);
            currentLevelPlatform = GetNextLevelPlatform();
            currentLevelPlatform.SetLevelNumber(currentLevel);
            PlayerPrefs.SetInt("Level", currentLevel);
            StartCoroutine(DestroyWithDelay(previousLevelPlatform, 6f));
        }
    }

    private LevelPlatform GetNextLevelPlatform() {
        LevelPlatform platform = levelPlatformPool.GetPooledObject();
        platform.transform.position = new Vector3(numberOfLevelsPlayedInASession * platform.GetPlatformLength(), 0f, 0f);
        platform.gameObject.SetActive(true);
        return platform;
    }

    private IEnumerator DestroyWithDelay(LevelPlatform platform, float delay) {
        levelPlatformPool.ReturnToPool(platform);
        platform.SetLevelNumber(currentLevel + 1);
        yield return new WaitForSeconds(delay);
        platform.ResetPlatform();
        yield return new WaitForSeconds(1f);
        platform.gameObject.SetActive(false);

    }

    public void ProceedToNextLevel() {
        Time.timeScale = 1;
    }

    public void ReplayLevel() {
        SceneManager.LoadScene(0);
    }
}