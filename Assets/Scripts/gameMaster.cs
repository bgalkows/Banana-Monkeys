﻿using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameMaster : MonoBehaviour {
	public string[] gameStates;
	public int currentLevel = 0;
	public gridRoot generator;
	public BananaGenerator bananaGen;
	public Monkey player;

    public bool transposed = false;

	// Use this for initialization
	void Start() {

        //string array holding all games states:    row / col / tiles / start row+col / end row+col / bananas
		gameStates = new string[] {
			"3 3 11111c11c 3 1 1 3 xxxxxxxxxxxx",
			"4 4 11111c1111c1c11c 4 3 3 4 xxxxxxxxxxxxxxxxxxxxxxxx",
			"3 3 11111c11c 3 1 1 3 xxxxxbxxxxxx",
			"3 3 11111c11c 3 1 1 3 bxxxxbxxxxxx",
			"3 3 111111111 3 1 1 1 xbxbxbxxxxxx",
			"4 4 1111111111111111 4 3 2 3 bxxbxxxxbxxbxxxxxxxxxxxx",
			"5 5 1111111111111111111111111 3 5 3 3 xxxbbbxxbxxxxxxxxxxxxxxbxxxxxxbbxxxbbxxx",
			"3 3 c1c12111c 3 1 2 3 xxxxxbxxbxxx",
			"4 3 111121121c11 3 1 4 2 bbxxbbbxxxxxxxxxx",
			"4 4 c111121121211c11 1 4 2 4 xxxxbxxbxxxxbxbxbxxxxxxb",
			"4 4 11111211111ccc11 1 1 4 4 xxbxbxxxxxxbxxxbxxxbxxxx",
			"3 4 c1111211c121 3 2 2 1 xbxxxxbbbxxxbxxxb",
			"4 4 1111112112111111 3 4 1 4 xbxxxbxxbbxbbxbbxxxxxxxx",
			"1 5 1111b 1 2 1 1 xbbb",
			"3 3 11b11c11c 1 2 1 1 xxxxxbxxbxbx",
			"4 4 1111bc11111c111n 3 1 1 4 xbxxxxxxxxxxxxxxxxbxxxbx"
		};



		generator = GameObject.FindGameObjectWithTag ("GameController").GetComponent<gridRoot> ();
		player = GameObject.FindGameObjectWithTag ("Monkey").GetComponent<Monkey> ();
		bananaGen = GameObject.FindGameObjectWithTag("GameController").GetComponent<BananaGenerator>();



		levelDefeat ();
		player.GetComponent<Monkey>().setGrid(generator.objectGrid);

	}

	// Update is called once per frame
	void Update () {

	}

	public void levelDefeat()
	{ 
        //parse level string corresponding to current level
		string[] splitState = gameStates [currentLevel].Split (' ');
		int rows = Int32.Parse(splitState [0]);
		int cols = Int32.Parse (splitState [1]);
		string state = splitState [2];
		int startRow = Int32.Parse (splitState [3]);
		int startCol = Int32.Parse (splitState [4]);
		int endRow = Int32.Parse (splitState [5]);
		int endCol = Int32.Parse (splitState [6]);
		string bananaString = splitState[7];

        //destroy current gameobjects
		foreach (List<GameObject> r in generator.objectGrid) {
			foreach (GameObject g in r) {
				Destroy (g);
			}
			r.Clear();
		}
		generator.objectGrid.Clear();

		foreach (GameObject b in bananaGen.bananaList)
		{
			Destroy(b);
		}

        //generate the actual grid -  also based on whether transposition is set
        if (!transposed)
        {
            generator.actualGenerateGrid(rows, cols, state, startRow, startCol, endRow, endCol);
            bananaGen.GenerateBananas(rows, cols, bananaString);
        }
        else
        {
            generator.generateTransposedGrid(rows, cols, state, startRow, startCol, endRow, endCol);
            bananaGen.generateTBananas(rows, cols, bananaString);
        }
            
		//set necessary values on player
		GameObject obj = generator.objectGrid [startRow - 1] [startCol - 1];
		player.StopMovement();
		player.birded = false;
		player.birdMoves = 0;


		// Move to new start
		player.transform.position = new Vector3 (obj.transform.position.x, player.transform.position.y, obj.transform.position.z);
		//obj.transform.position.z
		player.bananaCount = 0;


	}

	public void levelWin()
	{
        //increment level counter
		++currentLevel;

        //interact with music/backdrop scripts to shift forward
		backdrop resp = GameObject.FindGameObjectWithTag ("Respawn").GetComponent<backdrop> ();
		mShift mb = GameObject.FindGameObjectWithTag ("music").GetComponent<mShift> ();
		if (currentLevel % 3 == 0) {
			resp.shiftBack ();
			mb.songShift ();
		}


        //parse current level string
		string[] splitState = gameStates [currentLevel].Split (' ');
		int rows = Int32.Parse(splitState [0]);
		int cols = Int32.Parse (splitState [1]);
		string state = splitState [2];
		int startRow = Int32.Parse (splitState [3]);
		int startCol = Int32.Parse (splitState [4]);
		int endRow = Int32.Parse (splitState [5]);
		int endCol = Int32.Parse (splitState [6]);
		string bananaString = splitState[7];

		player.bananaCount = 0;

        //destroy old gameobjects
		foreach (List<GameObject> r in generator.objectGrid) {
			foreach (GameObject g in r) {
				Destroy (g);
			}
			r.Clear();
		}
		generator.objectGrid.Clear();

		foreach (GameObject b in bananaGen.bananaList)
		{
			Destroy(b);
		}
        
        //flip a coin for if the next level should be transposed
        System.Random rand = new System.Random();
        
        int roll = rand.Next(1, 3);
        Debug.Log("RAND =   " + Convert.ToString(roll));
        if (roll == 1)
        {
            transposed = false;
            generator.actualGenerateGrid(rows, cols, state, startRow, startCol, endRow, endCol);
            bananaGen.GenerateBananas(rows, cols, bananaString);
        }
        else
        {
            transposed = true;
            generator.generateTransposedGrid(rows, cols, state, startRow, startCol, endRow, endCol);
            bananaGen.generateTBananas(rows, cols, bananaString);
        }

		
		GameObject obj = generator.objectGrid [startRow - 1] [startCol - 1];

        //zoom camera + resize backdrop based on size of grid
		if (rows == 3) {
			GameObject.FindGameObjectWithTag ("MainCamera").transform.position = new Vector3 (.77f, 8.12f, -5.49f);
            resp.transform.localScale = new Vector3(3.528242f, 1.96204f, 1f);

        } else if (rows == 4) {
			GameObject.FindGameObjectWithTag ("MainCamera").transform.position = new Vector3 (.77f, 9.6f, -6.6f);
            resp.transform.localScale = new Vector3(4.040634f, 2.349884f, 1f);
        } else {
			GameObject.FindGameObjectWithTag ("MainCamera").transform.position = new Vector3 (.77f, 11.01f, -7.67f);
            resp.transform.localScale = new Vector3(4.571733f, 2.356324f, 1f);
        }

		//Stop motion
		player.StopMovement();
		player.birded = false;
		player.birdMoves = 0;

		//reset monkey rotation system.
		player.canMove = false;
		player.lastDirection = "up";
		player.transform.rotation = new Quaternion(0f,0f,0f,1f);

		// Move to new start
		player.transform.position = new Vector3(obj.transform.position.x, player.transform.position.y, obj.transform.position.z);
		//obj.transform.position.z
		player.bananaCount = 0;
	}

}
