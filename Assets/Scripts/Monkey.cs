using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monkey : MonoBehaviour {

	public int bananaCount = 0, bananaGoal;
	public float offset = 0.2f;
	public float jumpSpeed = 8;
	public int birdMoves = 0;
	public bool birded = false;

	public bool canMove = true;
	private Animator anim;
	public string lastDirection = "up";
	private Vector3 m_destination;
	private float origHeight;

	public List<List<GameObject>> currentObjectGrid; 
	public int maxRow, maxCol;
	public int currentRow = 4, currentCol = 1;

	void Start()
	{
		anim = GetComponent<Animator>();
		//setCurrentPosition (4, 1);
		origHeight = transform.position.y;
	}

	// Update is called once per frame
	void Update () {
        //reveal bird if birded, otherwise hide it
		if (birded) {
			GameObject myBird = GameObject.FindGameObjectWithTag ("Finish");
			MeshRenderer[] m = GetComponentsInChildren<MeshRenderer>();
			foreach (MeshRenderer r in m)
			{
				r.enabled = true;
			}
		}
		else{
			GameObject myBird = GameObject.FindGameObjectWithTag ("Finish");
			MeshRenderer[] m = GetComponentsInChildren<MeshRenderer>();
			foreach (MeshRenderer r in m)
			{
				r.enabled = false;
			}
		}

        //if the player cannot move, perform in-transit animation
		if (!canMove)
		{
			Vector3 directionOfTravel = m_destination - transform.position;
			if (directionOfTravel.magnitude <= 0.05f)
				RestoreMovement();
			//now normalize the direction, since we only want the direction information
			directionOfTravel.Normalize();
			//scale the movement on each axis by the directionOfTravel vector components

			this.transform.Translate(
				(directionOfTravel.x * jumpSpeed * Time.deltaTime),
				(directionOfTravel.y * jumpSpeed * Time.deltaTime),
				(directionOfTravel.z * jumpSpeed * Time.deltaTime),
				Space.World);
			//transform.position = m_destination;

			return;
		}
		//Debug.Log(currentObjectGrid[currentRow-1][currentCol-1].name);
		if ((Input.GetKeyDown (KeyCode.W) || Input.GetKeyDown (KeyCode.UpArrow)) && (currentRow - 1) >= 1) {
			Debug.Log ("start touch");

            //clean up previous tile
			if (!birded)
				LeaveTile (currentRow - 1, currentCol - 1);
			else {
				birdMoves--;
			}
			currentRow--;

            //rotate monkey based on direction it is currently facing
			if (lastDirection == "down")
				this.transform.Rotate (new Vector3 (0, 0, 180));
			else if (lastDirection == "left")
				this.transform.Rotate (new Vector3 (0, 90, 0));
			else if (lastDirection == "right")
				this.transform.Rotate (new Vector3 (0, 270, 0));

			canMove = false;
			float newZ = this.transform.position.z + offset;

			/////
			anim.SetTrigger ("Jump");
			m_destination = new Vector3 (this.transform.position.x, this.transform.position.y, newZ);
			/////

			lastDirection = "up";

			Debug.Log ("leave touch");
            //touch new tile
			if (!birded)
				TouchTile (currentRow - 1, currentCol - 1);

			//canMove = true;
		} else if ((Input.GetKeyDown (KeyCode.A) || Input.GetKeyDown (KeyCode.LeftArrow)) && (currentCol - 1) >= 1) {
			Debug.Log ("start touch");
			if(!birded)
				LeaveTile (currentRow - 1, currentCol - 1);
			else {
				birdMoves--;
			}
			currentCol--;

			if (lastDirection == "right")
				this.transform.Rotate (new Vector3 (0, 180, 0));
			else if (lastDirection == "down")
				this.transform.Rotate (new Vector3 (0, 90, 0));
			else if (lastDirection == "up")
				this.transform.Rotate (new Vector3 (0, 270, 0));

			canMove = false;
			float newX = this.transform.position.x - offset;

			////
			anim.SetTrigger ("Jump");
			m_destination = new Vector3 (newX, this.transform.position.y, this.transform.position.z);
			////

			lastDirection = "left";
			Debug.Log ("leave touch");
			if (!birded)
				TouchTile(currentRow - 1, currentCol - 1);
		}

		//canMove = true;

		else if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && (currentRow + 1) <= maxRow)
		{
			Debug.Log ("start touch");
			if(!birded)
				LeaveTile(currentRow - 1, currentCol - 1);
			else {
				birdMoves--;
			}
			currentRow++;

			if (lastDirection == "up")
				this.transform.Rotate (new Vector3 (0, 180, 0));
			else if (lastDirection == "right")
				this.transform.Rotate (new Vector3 (0, 90, 0));
			else if (lastDirection == "left")
				this.transform.Rotate (new Vector3 (0, 270, 0));

			canMove = false;
			float newZ = this.transform.position.z - offset;

			////
			anim.SetTrigger("Jump");
			m_destination = new Vector3 (this.transform.position.x, this.transform.position.y, newZ);
			////

			lastDirection = "down";
			Debug.Log ("leave touch");
			if (!birded)
				TouchTile(currentRow - 1, currentCol - 1);

			//canMove = true;
		}
		else if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && (currentCol + 1) <= maxCol)
		{
			Debug.Log ("start touch");
			if(!birded)
				LeaveTile(currentRow - 1, currentCol - 1);
			else {
				birdMoves--;
			}
			currentCol++;

			if (lastDirection == "left")
				this.transform.Rotate (new Vector3 (0, 180, 0));
			else if (lastDirection == "up")
				this.transform.Rotate (new Vector3 (0, 90, 0));
			else if (lastDirection == "down")
				this.transform.Rotate (new Vector3 (0, 270, 0));

			canMove = false;
			float newX = this.transform.position.x + offset;

			////
			anim.SetTrigger("Jump");
			m_destination = new Vector3 (newX, this.transform.position.y, this.transform.position.z);      
			////

			lastDirection = "right";
			Debug.Log ("leave touch");
			if (!birded)
				TouchTile (currentRow - 1, currentCol - 1);

			//canMove = true;
		}
	}

	void TouchTile(int x, int y)
	{
        //call touch method on the game object in current square

		if (currentObjectGrid[x][y].name.Contains("oneB"))
		{
			currentObjectGrid[x][y].GetComponent<oneBranch>().Touch();
		}
		else if (currentObjectGrid[x][y].name.Contains("twoB"))
		{
			currentObjectGrid[x][y].GetComponent<twoBranch>().Touch();
		}
		else if (currentObjectGrid[x][y].name.Contains("fourB"))
		{

		}
		else if (currentObjectGrid[x][y].name.Contains("Cliff"))
		{
			currentObjectGrid[x][y].GetComponent<Cliff>().Touch();
		}
	}

	void LeaveTile(int x, int y)
	{
        //erase old trees
		if (currentObjectGrid[x][y].name.Contains("oneB"))
		{
			currentObjectGrid[x][y].GetComponent<oneBranch>().KillTree();
		}
		else if (currentObjectGrid[x][y].name.Contains("twoB"))
		{
			if (currentObjectGrid[x][y].gameObject.GetComponent<twoBranch>().touchCount >= 2)
				currentObjectGrid[x][y].GetComponent<twoBranch>().KillTree();
			else
				currentObjectGrid[x][y].GetComponent<twoBranch>().switchTree();
		}
		else if (currentObjectGrid[x][y].name.Contains("fourB"))
		{

		}

	}

	void OnTriggerEnter(Collider c)
	{
		Debug.Log("Collision!");

        //detect collision with bananas
		if (c.gameObject.tag == "Banana" && !birded)
		{
			Debug.Log("banana acquired");

			bananaCount++;
			Destroy(c.gameObject);
		} 
	}

	void OnGUI()
	{
        //create banana count box? not really necessary anymore
		GUI.Label(new Rect(10,10,30,30), bananaCount.ToString());
	}

	public void Die()
	{
        //reconfigure variables and then call gameMaster to reset level
		canMove = false;
		lastDirection = "up";
		this.transform.rotation = new Quaternion(0f,0f,0f,1f);
		Debug.Log ("DEAD");
		GameObject.FindGameObjectWithTag("GameMaster").GetComponent<gameMaster>().levelDefeat();
		//GameObject.FindGameObjectWithTag ("EditorOnly").GetComponent<Text> ().enabled = true;
		//anim.SetTrigger("Die");
	}

	public void GameOver()
	{
        //this is pointless
		Debug.Log("GameOver");
	}
	public void RestoreMovement()
	{
		// Called by the end of the animation clip
		transform.position = new Vector3(transform.position.x, origHeight, transform.position.z);
        if (birded && birdMoves <= 0)
        {
            birded = false;
            TouchTile(currentRow - 1, currentCol - 1);
        }
        canMove = true;
	}
	public void StopMovement()
	{
		canMove = true;
	}

	public void setGrid(List<List<GameObject>> grid)
	{
        //align monkey's internal grid variables with the grid created
		currentObjectGrid = grid;
		maxRow = grid.Count;
		maxCol = grid [0].Count;
	}

	public void setCurrentPosition(int row, int col)
	{
        //align monkey's personal grid variables + touch beginning tile
		currentRow = row;
		currentCol = col;
		if (currentObjectGrid[row -1][col - 1].name.Contains("oneB"))
		{
			currentObjectGrid[row - 1][col - 1].GetComponent<oneBranch>().Touch();
		}
		else if (currentObjectGrid[row - 1][col - 1].name.Contains("twoB"))
		{
			currentObjectGrid[row - 1][col - 1].GetComponent<twoBranch>().Touch();
		}
	}
}
