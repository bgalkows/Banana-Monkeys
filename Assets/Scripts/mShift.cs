using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mShift : MonoBehaviour {

	public AudioClip m2, m3, m4, m5;
	public AudioSource audi;

	int inc = 1;

	// Use this for initialization
	void Start () {
		audi = this.GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void songShift()
	{
		inc++;
		if (inc == 2) {
			audi.clip = m2;
			audi.Play ();
		} else if (inc == 3) {
			audi.clip = m3;
			audi.Play ();
		} else if (inc == 4) {
			audi.clip = m4;
			audi.Play ();
		}
		else
		{
			audi.clip = m5;
			audi.Play();
		}
	}
}
