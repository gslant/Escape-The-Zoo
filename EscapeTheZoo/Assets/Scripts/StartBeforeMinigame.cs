using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartBeforeMinigame : MonoBehaviour
{
    [SerializeField] GameObject startCanvas;
    [SerializeField] GameObject tooltipObject;

    [SerializeField] TextMeshProUGUI tooltipText;
    [SerializeField] string tooltipMessage;

    [SerializeField] TextMeshProUGUI helpfulHintText;
    [SerializeField] string helpfulHintMessage;

    [SerializeField] TextMeshProUGUI countdownText;

    const float TOOLTIP_WAIT_TIME = 4f;
    const int COUNTDOWN_FROM = 3;

    // Start is called before the first frame update
    void Start()
    {
        startCanvas.SetActive(true);
        tooltipObject.SetActive(true);

        countdownText.text = "";

        PauseGame();
        StartCoroutine(ShowTooltipMessage());
    }

    IEnumerator ShowTooltipMessage()
    {
        tooltipText.text = tooltipMessage;
        helpfulHintText.text = helpfulHintMessage;

        yield return new WaitForSecondsRealtime(TOOLTIP_WAIT_TIME);
        tooltipObject.SetActive(false);

        StartCoroutine(StartCountdownFromThree());
    }

    IEnumerator StartCountdownFromThree()
    {
        int dots = 3;
        for (int i = COUNTDOWN_FROM; i > 0; i--)
        {
            countdownText.text = i.ToString();
            for (int j = dots; j > 0; j--)
            {
                yield return new WaitForSecondsRealtime(0.1f);
                countdownText.text += ".";
            }
            yield return new WaitForSecondsRealtime(0.4f);
        }
        countdownText.text = "GO!";
        yield return new WaitForSecondsRealtime(1f);

        ResumeGame();
        startCanvas.SetActive(false);
    }

    void PauseGame()
    {
        Time.timeScale = 0;
    }

    void ResumeGame()
    {
        Time.timeScale = 1;
    }
}
