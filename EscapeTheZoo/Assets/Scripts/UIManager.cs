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

    //Lobby Objects
    public GameObject lobbyCanvas;
    public GameObject addPlayerScreen;
    public Button startGameButton;
    public Button lobbyBackButton;
    public Button addPlayerButton;
    public Button addPlayerScreenCloseButton;
    public Button createPlayerButton;
    public ScrollRect playerAddScroll;

    //Shop Objects



    public void init(GameController controller)
    {
        goToLobbyButton.onClick.AddListener(delegate { GoToLobby(); });
        settingsButton.onClick.AddListener(delegate { GoToSettings(); });
        shopButton.onClick.AddListener(delegate { GoToShop(); });
        quitButton.onClick.AddListener(delegate { Quit(); });

        startGameButton.onClick.AddListener(delegate { StartGame(); });

        //delegate buttons here
        //buttons such as start, settings can be handled in ui 
        //e.g. delegate{ goToLobby();}
        //buttons such as click to roll, click to buy, etc, should be handled by delegating them to a function in controller
        //e.g. delegate{ controller.DiceRolled(); }

        // Lobby Buttons
        lobbyBackButton.onClick.AddListener(delegate { lobbyBack(); });
        addPlayerButton.onClick.AddListener(delegate { addPlayerScreenOpen(); });
        addPlayerScreenCloseButton.onClick.AddListener(delegate { addPlayerScreenClose(); });
        createPlayerButton.onClick.AddListener(delegate { createPlayer(); });
    }

    //TODO: make these button functions agnostic of which canvas is currently loaded,
    //so it can be called at any time(such as going back to lobby AND going from main menu to lobby)
    public void GoToLobby()
    {
        MainMenuCanvas.SetActive(false);
        lobbyCanvas.SetActive(true);
    }

    public void StartGame()
    {
        lobbyCanvas.SetActive(false);
    }

    public void GoToSettings()
    {
        lobbyCanvas.SetActive(false);
        //set settings canvas to true
        Debug.Log("Settings Clicked");
    }

    public void GoToShop()
    {
        lobbyCanvas.SetActive(false);
        //set shop canvas to true
        Debug.Log("Shop Clicked");
    }

    //TODO: there is probably a safer way of doing this, this just prevents having to use alt+f4 on test builds
    public void Quit()
    {
        Debug.Log("Quit Clicked(note: if not in editor, but in build, this will force close the application)");
        Application.Quit();
    }

    // Start of Lobby Button Functions
    public void lobbyBack()
    {
        MainMenuCanvas.SetActive(true);
        lobbyCanvas.SetActive(false);
    }

    public void addPlayerScreenOpen()
    {
        addPlayerScreen.SetActive(true);
    }

    public void addPlayerScreenClose()
    {
        addPlayerScreen.SetActive(false);
    }

    public void createPlayer()
    {
        // Todo
    }
    // End of Lobby Button Functions
}
