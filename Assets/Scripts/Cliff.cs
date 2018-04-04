using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cliff : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Touch()
	{
		GameObject player = GameObject.FindGameObjectWithTag ("Monkey");

		Debug.Log (this.gameObject.name);
		player.GetComponent<Monkey> ().Die ();
	}
}
