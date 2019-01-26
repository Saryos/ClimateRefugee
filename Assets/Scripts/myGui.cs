using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;


public class myGui : MonoBehaviour
{
	gameMaster myMaster;

	public void setGamemaster(gameMaster master){
		myMaster = master;
	}

	void OnGUI(){
		GUI.Box(new Rect(200,20,100,30), "Wood: " + 0);
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
