using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameMaster : MonoBehaviour{

	public GameObject[] defencePrefabs; // Fill in editor!
	public GameObject[] improvementPrefabs; // Fill in editor!

	public AudioSource background_music; // Fill in editor!
	public AudioSource disaster_music;
	public AudioSource victory_music;
	public AudioSource new_day_music;
	public AudioSource defeat_music;

	gameState game;
	myGui gui;
	logicData data;

	bool victory = false;

    public GameObject generatorPF;
    private GameObject generator = null;


    public int levelWidth_ = 40;
    public int levelHeight_ = 40;

	public double gameSpeed = 1.0;
	double oldGameSpeed = 0; // save speed durig pause

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

	public logicData giveData(){
		return data;
	}

	public void pauseGame(){
		if (gameSpeed>0){
			oldGameSpeed = gameSpeed;
			gameSpeed=0;
		}
	}

	public void resumeGame(){
		gameSpeed = oldGameSpeed;
	}

	public void addResource(int id){
		data.addResource(id);
	}

	public bool buildDefence(int id){
	// can we afford building?
		for(int j=0; j<data.noResources; j++){
			if(data.resources[j].amount < data.defences[id].cost[j]){
				return false;
			}
		}

		//build
		data.defences[id].level += 1;
		for(int j=0; j<data.noResources; j++){
			data.resources[j].amount -= data.defences[id].cost[j];
		}

		// instantiate graphics
		if(defencePrefabs.Length < id){
			MonoBehaviour.print("ERROR: Missing prefab from defence building array (did you fill it in editor?)");
			return false;
		}
		Vector3 pos = new Vector3(3,3,0);
		Instantiate(defencePrefabs[id],pos, Quaternion.identity);
		return true;
	}

	public bool buildImprovement(int id){
		// can we afford building?
		for(int j=0; j<data.noResources; j++){
			if(data.resources[j].amount < data.improvements[id].cost[j]){
				return false;
			}
		}

		//build
		data.improvements[id].level += 1;
		for(int j=0; j<data.noResources; j++){
			data.resources[j].amount -= data.improvements[id].cost[j];
		}

		//improve house
		for(int j=0; j<data.noDisasters; j++){
			data.house.pollution[j] -= data.improvements[id].value[j];
		}

		// check for victory
		bool tester = true;
		for(int j=0; j<data.noDisasters; j++){
			if(data.house.pollution[j]>0){
				tester = false;
			}
		}
		victory = tester;
		if(victory){
			background_music.Pause();
			victory_music.Play();
			gui.winGame();
		}

		// instantiate graphics
		if(improvementPrefabs.Length < id){
			MonoBehaviour.print("ERROR: Missing prefab from defence building array (did you fill it in editor?)");
			return false;
		}
		Vector3 pos = new Vector3(3,3,0);
		Instantiate(improvementPrefabs[id],pos, Quaternion.identity);
	
		return true;
	}

    // Start is called before the first frame update
    void Start(){

		data.LoadControlData();

        createWorld();
    }


    void createWorld()
    {
        Debug.Log("Creating world");
        generator = Instantiate(generatorPF, transform.position, Quaternion.identity);

        Generator g = generator.GetComponent<Generator>();
        g.generateScenario(levelWidth_, levelHeight_, 0.0f, 0.0f);
    }

    void destroyWorld()
    {
        Destroy(generator);
    }


    // Update is called once per frame
    void Update()
    {
		if(!background_music.isPlaying
		&& !disaster_music.isPlaying
		&& !victory_music.isPlaying
		&& !new_day_music.isPlaying
		&& !defeat_music.isPlaying){
			background_music.Play();
		}
		
		// advance disasters
		for(int i=0; i<data.noDisasters; i++){
			data.disasters[i].value += Time.deltaTime*gameSpeed*(data.house.pollution[i]/100.0) + (data.disasters[i].state/100.0);
		}

		// check for disaster
		for(int i=0; i<data.noDisasters; i++){
			if(data.disasters [i].value > data.disasters [i].treshold){
				if(disaster(i)){
					pauseGame();
					gui.surviveDisaster(i);
					background_music.Pause();
					new_day_music.Play();
					nextDay();
				} else {
					gameSpeed=0;
					if(!defeat_music.isPlaying){
						background_music.Pause();
						defeat_music.Play();
					}
					gui.loseGame();
				}
			}
		}
    }

	//disaster strikes
	bool disaster(int id){
		int total_defence=0;
		//check for game lost
		for(int i=0; i<data.noDefences; i++){
			total_defence += data.defences[i].level*data.defences[i].value[id];
		}
		if(total_defence < 1){ // change value 1 to something sensible
			// lose game
			return false;
		}

		//lose resources
		for(int i=0; i<data.noResources; i++){
			data.resources[i].amount -= Mathf.Min(5-total_defence, data.resources[i].amount);
		}

		//destroy house
		for(int i=0; i<data.noDefences; i++){
			data.defences[i].level=0;
		}

		//Zero disaster counter
		data.disasters[id].value = 0;
		return true;
	}

    void nextDay()
    {
        destroyWorld();
        createWorld();
    }

}
