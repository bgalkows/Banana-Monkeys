using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backdrop : MonoBehaviour {

	public Sprite bd2, bd3,bd4, bd5;
	public SpriteRenderer sprites;
	int inc = 1;

	// Use this for initialization
	void Start () {
		sprites = this.GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void shiftBack()
        //shift backdrop sprite 1 spot forward
	{
		inc++;
		if (inc == 2)
			sprites.sprite = bd2;
		else if (inc == 3)
			sprites.sprite = bd3;
		else if (inc == 4)
			sprites.sprite = bd4;
		else
			sprites.sprite = bd5;
			
			
	}
}
