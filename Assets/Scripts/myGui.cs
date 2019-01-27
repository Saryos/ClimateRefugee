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
	bool gameLost = false;
	bool survived = false;
	int disaster; // current disaster
	bool victory = false;

	public void setGamemaster(gameMaster master){
		myMaster = master;
	}

	public void loseGame(){
		gameLost=true;
	}

	public void winGame(){
		victory=true;
	}

	public void surviveDisaster(int id){
		MonoBehaviour.print("survive!");
		survived = true;
		disaster = id;
	}

	void OnGUI(){
		if(data==null){
			data = myMaster.giveData();
		}
		if(game==null){
			game = myMaster.giveGamestate();
		}




		// resources
		for(int i=0; i<data.noResources; i++){
			if(GUI.Button(new Rect(Screen.width-200,30*data.noDisasters+20+30*i,180,30), data.resources[i].name + " " + data.resources[i].amount)){
				//data.addResource(i);
			}
		}

		// disasters
		for(int i=0; i<data.noDisasters; i++){
			GUI.Box(new Rect(Screen.width-200,20+30*i,180,30), data.disasters[i].name + " " + (int)(data.disasterAdvance(i)*100) + "%");
		}



		//defences
		for(int i=0; i<data.noDefences; i++){
			string defence_values = "";
			for(int j=0; j<data.noDisasters; j++){
				defence_values += data.defences[i].value[j] + " ";
			}
			if(GUI.Button(new Rect(10,20+40*i,100,40), data.defences[i].name + " " + data.defences[i].level + " cost: " + data.defences[i].cost[0] + " " + data.defences[i].cost[1] + "\n"
				+ "def: " + defence_values)){
				myMaster.buildDefence(i);
			}
		}

		//improvements
		for(int i=0; i<data.noImprovements; i++){
			string defence_values = "";
			for(int j=0; j<data.noDisasters; j++){
				defence_values += data.improvements[i].value[j] + " ";
			}
			if(GUI.Button(new Rect(10,Screen.height-50-55*i,100,55), data.improvements[i].name + " " + data.improvements[i].level+ "\n"
				+ "cost: " + data.improvements[i].cost[0] + " " + data.improvements[i].cost[1] +"\n"
				+ "value: " + defence_values)){
				myMaster.buildImprovement(i);
			}
		}

		// house
		string house_values = "";
		for(int j=0; j<data.noDisasters; j++){
			house_values += data.house.pollution[j] + " ";
		}
			if(GUI.Button(new Rect(120,Screen.height-50,120,40), "house" + " " + house_values)){
			}
	

		/*
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
		*/
		if (gameLost){
			GUIStyle myButtonStyle = new GUIStyle (GUI.skin.button);
			myButtonStyle.fontSize = 50;
			// Load and set Font
			Font myFont = (Font)Resources.Load ("Fonts/comic", typeof(Font));
			myButtonStyle.font = myFont;
			// Set color for selected and unselected buttons
			myButtonStyle.normal.textColor = Color.red;
			myButtonStyle.hover.textColor = Color.red;

			// use style in button
			GUI.Box (new Rect (50, 10, 350, 250), "Death", myButtonStyle);
			GUI.Box (new Rect (30,270,390,50), "Sakari jäätyi ilman villapaitaa\nWihout a sweater, Sakari froze to death");
		}
		if (survived){
			GUIStyle myButtonStyle = new GUIStyle (GUI.skin.button);
			myButtonStyle.fontSize = 50;
			// Load and set Font
			Font myFont = (Font)Resources.Load ("Fonts/comic", typeof(Font));
			myButtonStyle.font = myFont;
			// Set color for selected and unselected buttons
			myButtonStyle.normal.textColor = Color.blue;
			myButtonStyle.hover.textColor = Color.blue;

			// use style in button
			if (GUI.Button (new Rect (50, 10, 350, 250), data.disasters[disaster].name, myButtonStyle)){
				survived = false;
				myMaster.resumeGame();
			}
			GUI.Box (new Rect (30,270,390,50), "You have survived, but your house is lost\n" +
				"and you have moved to new place. Click to continue.");
		}
		if (victory){
			GUIStyle myButtonStyle = new GUIStyle (GUI.skin.button);
			myButtonStyle.fontSize = 50;
			// Load and set Font
			Font myFont = (Font)Resources.Load ("Fonts/comic", typeof(Font));
			myButtonStyle.font = myFont;
			// Set color for selected and unselected buttons
			myButtonStyle.normal.textColor = Color.blue;
			myButtonStyle.hover.textColor = Color.blue;

			// use style in button
			if (GUI.Button (new Rect (50, 10, 350, 250), "VICTORY!", myButtonStyle)){
				//survived = false;
				//myMaster.resumeGame();
			}
			GUI.Box (new Rect (30,270,390,50), "You have succeeded building a house\n" +
				"that is no longer a mortal threat to your environment. Congratulations!");
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
