using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Main menu");
        UnityEngine.Cursor.visible = true;
    }

    public void onClickStartGame()
    {
        UnityEngine.Cursor.visible = true;
        Application.LoadLevel(1);
    }

    public void onClickCredits()
    {
        Debug.Log("Credits");
        Application.LoadLevel(2);
    }

    public void onClickExit()
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
