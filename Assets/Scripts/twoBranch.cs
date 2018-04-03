using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class twoBranch : MonoBehaviour {
	public int touchCount;
	public bool end = false, start = false;

	public Avatar backup;
	// Use this for initialization
	void Start () {
		touchCount = 0;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Touch()
	{
		
		if (touchCount >= 2) {
			//this.GetComponent<SpriteRenderer> ().enabled = false;
			GameObject player = GameObject.FindGameObjectWithTag ("Monkey");
			player.GetComponent<Monkey> ().Die ();
		} else {
			touchCount++;
			Animator anim = this.GetComponent<Animator> ();
        }
	}
    public void KillTree()
    {
        transform.Find("palmtree").gameObject.SetActive(false);
    }

    public void switchTree()
    {
        transform.Find("twotree").gameObject.SetActive(false);
        transform.Find("palmtree").gameObject.SetActive(true);
    }
}
