using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueJay : MonoBehaviour {
	public int latentMoveStock, currentMoveStock;

	public Monkey m;
	public bool proc = false;


	// Use this for initialization
	void Start () {
		m = GameObject.FindGameObjectWithTag ("Monkey").GetComponent<Monkey> ();
		latentMoveStock = 4;
		currentMoveStock = 2;
	}

	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter(Collider c)
	{
        //hide bird on collision with player
		Debug.Log ("COLLIDED");
		if (c.gameObject.tag == "Monkey" && !proc) {
			MeshRenderer[] mr = GetComponentsInChildren<MeshRenderer>();
			foreach (MeshRenderer r in mr)
			{
				r.enabled = false;
			}

            //unhide bird on top of player
			m.birded = true;
			m.birdMoves += latentMoveStock;
			proc = true;
		}
	}
}

