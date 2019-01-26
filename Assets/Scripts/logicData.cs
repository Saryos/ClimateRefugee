using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using System;

//data for game

public struct Resource{
	string name;
	int amount;
	int[] pollution;

	public Resource (string a, int b, int[] c){
		name=a;
		amount = b;
		pollution = c;
	}
}

public struct Defence{
	string name;
	int level;
	int[] cost;
	int[] value;

	public Defence (string a, int b, int[] c, int[] d){
		name = a;
		level = b;
		cost = c;
		value = d;
	}

}

public struct Improvement{
	string name;
	int level;
	int[] cost;
	int[] value;

	public Improvement (string a, int b, int[] c, int[] d){
		name = a;
		level = b;
		cost = c;
		value = d;
	}
}
	



public class logicData{
	int noResources = 0;
	int noDisasters = 0;
	int noDefences = 0;
	int noImprovements = 0;

	public Resource[] resources; 
	public Defence[] defences; 
	public Improvement[] improvements; 

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
				}
				if(txtbits[1] == "disasters"){
					noDisasters=int.Parse(txtbits[2]);
				}
				if(txtbits[1] == "defences"){
					noDefences=int.Parse(txtbits[2]);
				}
				if(txtbits[1] == "improvements"){
					noImprovements=int.Parse(txtbits[2]);
				}
				continue;
			}

			MonoBehaviour.print(noResources + " " + noDisasters);
			if(noResources==0 || noDisasters==0 || noDefences==0 || noImprovements==0){

				MonoBehaviour.print("error: Amount values must be at the beginning of the file");
			}

			resources = new Resource[noResources];
			defences = new Defence[noDefences];
			improvements = new Improvement[noImprovements];

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

				/*
			bool found = false;
			for(int k=0; k<3 ; k++){
				for(int j=0; j<2 && gelements[k][j]!=null; j++){
					for(int i=0; i<gelements[k][j].Count; i++){
						if(gelements[k][j][i].gtexta==txtbits[0]){
							if (found==true){
								print("WARNING: multiple values with same idenfier" + txtbits[0]);
							}
							found = true;
							print("found "+txtbits[0]+" "+txtbits[3]);
							gelements[k][j][i].val.v=float.Parse(txtbits[3]);
						}
					}
				}
			}
*/
		}
		MonoBehaviour.print("Something loaded from file datasettings.txt");

		sr.Close();
		fs.Close();
	}


}
