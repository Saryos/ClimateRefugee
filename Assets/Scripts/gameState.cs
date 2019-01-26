using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameState : MonoBehaviour{

	gameMaster myMaster;
	logicData data = null;

	public void setGamemaster(gameMaster master){
		myMaster = master;
	}



	void nightfall (){
		// check survival
		// adjust dangerLevels if multilevel?
		// destroy resources
		// lose house objects? If defense not enought?
	}

	// Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
