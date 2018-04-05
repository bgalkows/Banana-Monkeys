using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oneBranch : MonoBehaviour {
	public int touchCount = 0;
	public bool end = false, start = false;

	private bool proc = false;

	public int myRow, myCol;
	GameObject player; 


	// Use this for initialization
	void Awake () {
		//touchCount = 0;
		player = GameObject.FindGameObjectWithTag ("Monkey");
	}
	
	// Update is called once per frame
	void Update () {
        //reveal star only on end tree
		if (end) {
			//Debug.Log (gameObject.transform.GetChild (0).gameObject.name + gameObject.transform.GetChild (1).gameObject.name + gameObject.transform.GetChild (2).gameObject.name);
			this.gameObject.transform.GetChild (1).gameObject.SetActive (true);
		}

        //trigger end of level effects if all conditions are met
		if (!proc && myRow == player.GetComponent<Monkey>().currentRow && myCol == player.GetComponent<Monkey>().currentCol && end && player.GetComponent<Monkey> ().bananaCount >= player.GetComponent<Monkey> ().bananaGoal)
		{
			proc = true;
			StartCoroutine(Waiting());
			proc = false;

		}
	}

	public void Touch()
	{
        if (touchCount >= 1) {
			this.GetComponent<SpriteRenderer> ().enabled = false;
			player.GetComponent<Monkey> ().Die ();
		} else {
			touchCount++;
			Debug.Log("current ban " + player.GetComponent<Monkey>().bananaCount + " goal bananas: " + player.GetComponent<Monkey>().bananaGoal + " bool val: " + (player.GetComponent<Monkey>().bananaCount >= player.GetComponent<Monkey>().bananaGoal));

           // if (end && player.GetComponent<Monkey> ().bananaCount >= player.GetComponent<Monkey> ().bananaGoal)
           // {
              //  StartCoroutine(Waiting());

            //}
        }
	}

    public void KillTree()
    {
        MeshRenderer[] m = GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer r in m)
        {
            r.enabled = false;
        }
    }

    IEnumerator Waiting()
    {
        //establish brief waiting period before calling gameMaster to move to next level
        player.GetComponent<Monkey>().canMove = false;
        yield return new WaitForSeconds(1f);
        GameObject.FindGameObjectWithTag("GameMaster").GetComponent<gameMaster>().levelWin();
        player.GetComponent<Monkey>().canMove = true;

    }
}
