using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelManager : MonoBehaviour {
	public gameMaster GM;
	private static bool created = false;

	private int level;

	// Use this for initialization
	void Awake () {

        //establish singleton structure
		if (!created) {
			DontDestroyOnLoad (this.gameObject);
			created = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void loadScene(string s)
	{
		SceneManager.LoadScene (s);
	}

	public void loadLevelByID( int levelNum )
	{
        //load DJ scene and then call coroutine to override default level
		SceneManager.LoadScene ("DJ");
		level = levelNum;

		StartCoroutine ("generate");


	}


	IEnumerator generate()
	{
        //wait for first map to be generated (otherwise this will be overrided
		yield return new WaitForSeconds(1f);
		GM = GameObject.FindGameObjectWithTag ("GameMaster").GetComponent<gameMaster> ();
		BananaGenerator bananaGen = GameObject.FindGameObjectWithTag ("GameController").GetComponent<BananaGenerator> ();
		Monkey player = GameObject.FindGameObjectWithTag ("Monkey").GetComponent<Monkey> ();


        //follow standard level generation steps - see GameMaster script


		string[] splitState = GM.gameStates [level].Split (' ');
		int rows = Int32.Parse(splitState [0]);
		int cols = Int32.Parse (splitState [1]);
		string state = splitState [2];
		int startRow = Int32.Parse (splitState [3]);
		int startCol = Int32.Parse (splitState [4]);
		int endRow = Int32.Parse (splitState [5]);
		int endCol = Int32.Parse (splitState [6]);
		string bananaString = splitState[7];

		gridRoot generator = GameObject.FindGameObjectWithTag ("GameController").GetComponent<gridRoot> ();

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



        generator.actualGenerateGrid (rows, cols, state, startRow, startCol, endRow, endCol);
        //generator.generateTransposedGrid(rows, cols, state, startRow, startCol, endRow, endCol);

        bananaGen.GenerateBananas(rows, cols, bananaString);
		GameObject obj = generator.objectGrid [startRow - 1] [startCol - 1];
		//Stop motion
		player.StopMovement();
		player.birded = false;
		player.birdMoves = 0;


		// Move to new start
		player.transform.position = new Vector3 (obj.transform.position.x, player.transform.position.y, obj.transform.position.z);
		//obj.transform.position.z
		player.bananaCount = 0;

		GM.currentLevel = level;
	}
}
