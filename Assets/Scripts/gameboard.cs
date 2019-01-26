using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameboard : MonoBehaviour{

	const int xtiles = 40;
	const int ytiles = 40;

	// catastrophes
	int fire = 0;
	int hurricane = 0;
	int earthquake = 0;


    // Start is called before the first frame update
    void Start()
    {
		int[,] board = new int[xtiles,ytiles];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
