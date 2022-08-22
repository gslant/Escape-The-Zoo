using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public UIManager uiManager;

    // Start is called before the first frame update
    void Start()
    {
        uiManager.init(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
