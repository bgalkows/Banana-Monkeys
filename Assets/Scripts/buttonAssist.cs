using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonAssist : MonoBehaviour {

	public Button myButton;
	public levelManager LM;

	public int level;

	// Use this for initialization
	void Start () {
		LM = GameObject.FindGameObjectWithTag ("LevelManager").GetComponent<levelManager> ();
		myButton = this.gameObject.GetComponent<Button> ();

		myButton.onClick.AddListener (delegate {
			LM.loadLevelByID (level);
		});
	}
}
