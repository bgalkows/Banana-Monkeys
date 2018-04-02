using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class twoBranch : MonoBehaviour {
	public int touchCount;
	public bool end = false, start = false;

	public Avatar backup;
	public Controller
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
			this.GetComponent<SpriteRenderer> ().enabled = false;
			GameObject player = GameObject.FindGameObjectWithTag ("Monkey");
			player.GetComponent<Monkey> ().Die ();
		} else {
			touchCount++;
			Animator anim = this.GetComponent<Animator> ();
			anim.avatar 
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

}
