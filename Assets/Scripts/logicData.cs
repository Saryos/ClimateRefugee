using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using System;

//data for game

public struct House{
	public int[] pollution;
}

public struct Resource{
	public string name;
	public int amount;
	public int[] pollution;

	public Resource (string a, int b, int[] c){
		name=a;
		amount = b;
		pollution = c;
	}
}

public struct Defence{
	public string name;
	public int level;
	public int[] cost;
	public int[] value;

	public Defence (string a, int b, int[] c, int[] d){
		name = a;
		level = b;
		cost = c;
		value = d;
	}
}

public struct Improvement{
	public string name;
	public int level;
	public int[] cost;
	public int[] value;

	public Improvement (string a, int b, int[] c, int[] d){
		name = a;
		level = b;
		cost = c;
		value = d;
	}
}

public struct Disaster{
	public string name;
	public double value;
	public double state;
	public double treshold;

	public Disaster(string a, double b, double c, double d){
		name = a;
		value = b;
		treshold = c;
		state = d;
	}
}



public class logicData{
	public int noResources = 0;
	public int noDisasters = 0;
	public int noDefences = 0;
	public int noImprovements = 0;

	public Resource[] resources; 
	public Defence[] defences; 
	public Improvement[] improvements; 
	public Disaster[] disasters;
	public House house;

	// advance of disaster on scale 0-1
	public double disasterAdvance(int id){
		return (disasters[id].value/disasters[id].treshold);
	}

	public void addResource(int id){
		resources[id].amount += 1;
		for(int j=0; j<noDisasters; j++){
			disasters[j].value += resources[id].pollution[j];
		}
	}

	public void LoadControlData(){
		FileStream fs = new FileStream("datasettings.txt", FileMode.Open, FileAccess.Read);
//		if(!fs){
//			print("error opening file");
//		}
		StreamReader sr = new StreamReader(fs);
		while(sr.Peek() != -1){
			string txt = sr.ReadLine();
			string [] txtbits = txt.Split(' ');
			// does mono support case with string?
			if(txtbits [0] == "#"){
				if(txtbits[1] == "resources"){
					noResources=int.Parse(txtbits[2]);
					resources = new Resource[noResources];
				}
				if(txtbits[1] == "disasters"){
					noDisasters=int.Parse(txtbits[2]);
					disasters = new Disaster[noDisasters];
					house.pollution = new int[noDisasters];
				}
				if(txtbits[1] == "defences"){
					noDefences=int.Parse(txtbits[2]);
					defences = new Defence[noDefences];
				}
				if(txtbits[1] == "improvements"){
					noImprovements=int.Parse(txtbits[2]);
					improvements = new Improvement[noImprovements];
				}
				continue;
			}

			MonoBehaviour.print(noResources + " " + noDisasters);
			if(noResources==0 || noDisasters==0 || noDefences==0 || noImprovements==0){

				MonoBehaviour.print("error: Amount values must be at the beginning of the file");
			}





			//quick & dirty
			if(txtbits[0]=="resources"){
				for(int i=0; i<noResources; i++){
					txt = sr.ReadLine();
					txtbits = txt.Split(' ');
				
					int[] temp = new int[noDisasters];
					for ( int j=0; j<noDisasters; j++){
						temp[j] = int.Parse(txtbits[j+1]);
					}
					resources[i]=new Resource(txtbits[0], 0, temp);
					MonoBehaviour.print(resources[i].amount);
				}			
			}

			if(txtbits[0]=="disasters"){
				for(int i=0; i<noDisasters; i++){
					txt = sr.ReadLine();
					txtbits = txt.Split(' ');

					disasters[i]=new Disaster(txtbits[0], 0, 100, 0);
				}			
			}

			if(txtbits[0]=="house"){
				txt = sr.ReadLine();
				txtbits = txt.Split(' ');
				for(int i=0; i<noDisasters; i++){
					house.pollution[i]=int.Parse(txtbits[i]);
				}			
			}

			if(txtbits[0]=="defences"){
				for(int i=0; i<noDefences; i++){
					txt = sr.ReadLine();
					txtbits = txt.Split(' ');

					int[] temp = new int[noResources];
					for ( int j=0; j<noResources; j++){
						temp[j] = int.Parse(txtbits[j+1]);
					}

					string name = txtbits[0];

					txt = sr.ReadLine();
					txtbits = txt.Split(' ');

					int[] temp2 = new int[noDisasters];
					for ( int j=0; j<noDisasters; j++){
						temp2[j] = int.Parse(txtbits[j]);
					}

					defences[i]=new Defence(name, 0, temp, temp2);
				}			
			}

			if(txtbits[0]=="improvements"){
				for(int i=0; i<noImprovements; i++){
					txt = sr.ReadLine();
					txtbits = txt.Split(' ');

					int[] temp = new int[noResources];
					for ( int j=0; j<noResources; j++){
						temp[j] = int.Parse(txtbits[j+1]);
					}

					string name = txtbits[0];

					txt = sr.ReadLine();
					txtbits = txt.Split(' ');

					int[] temp2 = new int[noDisasters];
					for ( int j=0; j<noDisasters; j++){
						temp2[j] = int.Parse(txtbits[j]);
					}

					improvements[i]=new Improvement(name, 0, temp, temp2);
				}			
			}
		}
		MonoBehaviour.print("Something loaded from file datasettings.txt");
		MonoBehaviour.print(resources[0].amount);
		sr.Close();
		fs.Close();
	}


}
