using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupsImplementation : MonoBehaviour
{

    public enum FunctionOutputs { moveup = 1, movedown = 2}

    int randomNumber = Random.Range(1, 2);


    // Start is called before the first frame update
    void Start()
    {



    }

    // Update is called once per frame
    void Update()
    {

    }

    public void moveup()
    {
        Debug.Log("Function 1");
    }

    public void movedown()
    {
        Debug.Log("Function 2");
    }

    public void GetPowerup(FunctionOutputs function, int num)
    {
        num = randomNumber;


        switch (function)
        {
            case FunctionOutputs.moveup:
                if(num == 1)
                moveup();
                break;
            case FunctionOutputs.movedown:
                if(num == 2)
                movedown();
                break;
        }
    }

}
