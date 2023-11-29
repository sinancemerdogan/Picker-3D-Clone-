using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UICanvas : MonoBehaviour {
    private TextMeshProUGUI currentLevelText;
    private TextMeshProUGUI nextLevelText; 
    private TextMeshProUGUI startText; 

    private List<Image> indicatorImages = new List<Image>();

    private Button proceedButton;
    private Button replayButton;

    void Start() {
        currentLevelText = transform.Find("CurrentLevelIndicator").Find("CurrentLevelText").GetComponent<TextMeshProUGUI>();
        nextLevelText = transform.Find("NextLevelIndicator").Find("NextLevelText").GetComponent<TextMeshProUGUI>();
        startText = transform.Find("StartText").GetComponent<TextMeshProUGUI>();

        indicatorImages.Add(transform.Find("Phase1Indicator").GetComponent<Image>());
        indicatorImages.Add(transform.Find("Phase2Indicator").GetComponent<Image>());
        indicatorImages.Add(transform.Find("Phase3Indicator").GetComponent<Image>());

        proceedButton = transform.Find("ProceedButton").GetComponent<Button>();
        replayButton = transform.Find("ReplayButton").GetComponent<Button>();
    }



    public void OnPhaseCompletion(int index) {
        Sprite newSprite = Resources.Load<Sprite>("Sprites/Square_Orange_Button_Normal");

        if (index >= 0 && index < indicatorImages.Count) {
            indicatorImages[index].sprite = newSprite;
        }
    }

    public void ResetIndicators() {
        Sprite newSprite = Resources.Load<Sprite>("Sprites/Square_White_Button_Normal");

        foreach(Image indicatorImage in indicatorImages) {
            indicatorImage.sprite = newSprite;
        }
    }

    public void OnFail() {
        replayButton.gameObject.SetActive(true);
    }

    public void OnLevelComplete() {
        proceedButton.gameObject.SetActive(true);
    }

    public void SetLevelTexts(int currentLevel) {
        int nextLevel = currentLevel + 1;

        currentLevelText.text = currentLevel.ToString();
        nextLevelText.text = nextLevel.ToString();
    }

    public void DeactivateStartText() {
        startText.gameObject.SetActive(false);
    }
}
