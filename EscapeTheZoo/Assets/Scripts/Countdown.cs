using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    [SerializeField] GameObject countdownCanvas;
    TextMeshProUGUI countdownText;

    const int COUNTDOWN_FROM = 3;

    // Start is called before the first frame update
    void Start()
    {
        countdownCanvas.SetActive(true);
        countdownText = countdownCanvas.GetComponentInChildren<TextMeshProUGUI>();
        countdownText.text = "";
        StartCoroutine(StartCountdownFromThree());
    }
    
    IEnumerator StartCountdownFromThree()
    {
        int dots = 3;

        PauseGame();

        yield return new WaitForSecondsRealtime(0.2f);
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
        countdownCanvas.SetActive(false);
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
