using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //Main Menu Objects
    public GameObject MainMenuCanvas;
    public Button goToLobbyButton;
    public Button settingsButton;
    public Button shopButton;
    public Button quitButton;
    public Button helpButton;
    public Button creditsButton;

    // Help Objects
    public GameObject helpPanel;

    // Credits Objects
    public GameObject creditsPanel;

    // Settings Objects
    public GameObject settingsPanel;

    //Lobby Objects
    public GameObject lobbyCanvas;

    //Shop Objects


    public void init(GameController controller)
    {
        goToLobbyButton.onClick.AddListener(delegate { GoToLobby(); });
        settingsButton.onClick.AddListener(delegate { GoToSettings(); });
        shopButton.onClick.AddListener(delegate { GoToShop(); });
        quitButton.onClick.AddListener(delegate { Quit(); });
        helpButton.onClick.AddListener(delegate { GoToHelp(); });
        creditsButton.onClick.AddListener(delegate { GoToCredits(); });

        //delegate buttons here
        //buttons such as start, settings can be handled in ui 
        //e.g. delegate{ goToLobby();}
        //buttons such as click to roll, click to buy, etc, should be handled by delegating them to a function in controller
        //e.g. delegate{ controller.DiceRolled();
    }

    //TODO: make these button functions agnostic of which canvas is currently loaded,
    //so it can be called at any time(such as going back to lobby AND going from main menu to lobby)
    public void GoToLobby()
    {
        MainMenuCanvas.SetActive(false);
        lobbyCanvas.SetActive(true);
    }

    public void GoToSettings()
    {
        settingsPanel.SetActive(true);
        helpButton.gameObject.SetActive(false);
        creditsButton.gameObject.SetActive(false);
        Debug.Log("Settings Clicked");
    }

    public void GoToShop()
    {
        lobbyCanvas.SetActive(false);
    }

    //TODO: there is probably a safer way of doing this, this just prevents having to use alt+f4 on test builds
    public void Quit()
    {
        Debug.Log("Quit Clicked(note: if not in editor, but in build, this will force close the application)");
        Application.Quit();
    }

    public void GoToHelp()
    {
        helpPanel.SetActive(true);
        helpButton.gameObject.SetActive(false);
        creditsButton.gameObject.SetActive(false);
    }

    public void GoToCredits()
    {
        creditsPanel.SetActive(true);
        helpButton.gameObject.SetActive(false);
        creditsButton.gameObject.SetActive(false);
    }
}
