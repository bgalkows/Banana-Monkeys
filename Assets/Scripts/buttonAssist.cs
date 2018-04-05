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

        //  this script is necessary because the object that the buttons need to connect with is not from their actual scene 
        //  (and therefore cannot be connected in editor)
        
		LM = GameObject.FindGameObjectWithTag ("LevelManager").GetComponent<levelManager> ();
		myButton = this.gameObject.GetComponent<Button> ();

        //my first time using delegates  <3 <3 

		myButton.onClick.AddListener (delegate {
			LM.loadLevelByID (level);
		});
	}
}
