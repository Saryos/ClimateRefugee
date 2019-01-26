using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameState : MonoBehaviour{

	enum DISASTERS {HURRICANE, FIRE, FLOOD, QUAKE, ARMAGEDDON};
	enum RESOURCES {WOOD, ROCK};
	// defenses: against single disaster or multiple?
	enum DEFENCES {WALL, ROOF, DAM, CLEARANCE}
	enum IMPROVEMENTS {INSULATION, ENERGY, OVEN}


	double[] dangerLevels = new double[ System.Enum.GetNames(typeof(DISASTERS)).Length];
	int[] resourceLevels = new int[System.Enum.GetNames(typeof(RESOURCES)).Length];
	int[] defenceLevels = new int[System.Enum.GetNames(typeof(DEFENCES)).Length];
	int[] improvementLevels = new int[System.Enum.GetNames(typeof(IMPROVEMENTS)).Length];

	void addResource (RESOURCES resurssi, int amount){
		resourceLevels[(int)resurssi] += amount;
		// adjust dangerLevels
	}

	void buildDefence(DEFENCES building){
		//adjust resources
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
