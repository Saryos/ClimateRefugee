using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameMaster : MonoBehaviour
{
	gameState game;
	myGui gui;
	logicData data;
	
	public gameState giveGamestate(){
		return game;
	}

	void Awake(){
		game = gameObject.AddComponent(typeof(gameState)) as gameState;
		game.setGamemaster(this);
		gui = gameObject.AddComponent(typeof(myGui)) as myGui;
		gui.setGamemaster(this);
		data = new logicData();
	}

    // Start is called before the first frame update
    void Start(){
		data.LoadControlData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
