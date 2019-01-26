using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;


public class myGui : MonoBehaviour
{
	gameMaster myMaster;
	logicData data = null;
	gameState game = null;

	public void setGamemaster(gameMaster master){
		myMaster = master;
	}

	void OnGUI(){
		if(data==null){
			data = myMaster.giveData();
		}
		if(game==null){
			game = myMaster.giveGamestate();
		}





		for(int i=0; i<data.noResources; i++){
			if(GUI.Button(new Rect(200,20+30*i,100,30), data.resources[i].name + " " + data.resources[i].amount)){
				// totally wrong place for logic!
				data.resources[i].amount += 1;
				for(int j=0; j<data.noDisasters; j++){
					data.disasters[j].value += data.resources[i].pollution[j];
				}
			}
		}
		for(int i=0; i<data.noDisasters; i++){
			GUI.Box(new Rect(300,20+30*i,100,30), data.disasters[i].name + " " + data.disasters[i].value);
		}

		for(int i=0; i<data.noDefences; i++){
			if(GUI.Button(new Rect(10,20+30*i,100,30), data.defences[i].name + " " + data.defences[i].level)){
				myMaster.buildDefence(i);
			}
		}


		for(int i=0; i<data.noDisasters; i++){
			if (data.disasters [i].value > data.disasters [i].treshold) {
				GUIStyle myButtonStyle = new GUIStyle (GUI.skin.button);
				myButtonStyle.fontSize = 50;
				// Load and set Font
				Font myFont = (Font)Resources.Load ("Fonts/comic", typeof(Font));
				myButtonStyle.font = myFont;
				// Set color for selected and unselected buttons
				myButtonStyle.normal.textColor = Color.red;
				myButtonStyle.hover.textColor = Color.red;

				// use style in button
				GUI.Box (new Rect (50, 10, 350, 250), "Death by " + data.disasters[i].name, myButtonStyle);
			}
		}
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
