using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public LayerMask standingLayer;
	public LayerMask borderLayer;
	public LevelManager levelManager;

	void Start()
	{

	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.A) && Physics2D.OverlapCircle(transform.position + new Vector3(-32, 0, 0), 0.1f, standingLayer))
		{
			MovePlayer(-32, 0);
		}
		if (Input.GetKeyDown(KeyCode.S) && Physics2D.OverlapCircle(transform.position + new Vector3(0, -32, 0), 0.1f, standingLayer))
		{
			MovePlayer(0, -32);
		}
		if (Input.GetKeyDown(KeyCode.D) && Physics2D.OverlapCircle(transform.position + new Vector3(32, 0, 0), 0.1f, standingLayer))
		{
			MovePlayer(32, 0);
		}
		if (Input.GetKeyDown(KeyCode.W) && Physics2D.OverlapCircle(transform.position + new Vector3(0, 32, 0), 0.1f, standingLayer))
		{
			MovePlayer(0, 32);
		}
	}

	private void MovePlayer(float x, float y)
	{
		Vector3 dir = new Vector3(x, y);

		try
		{
			GameObject newIceberg = Physics2D.OverlapCircle(transform.position + dir, 0.1f, standingLayer).gameObject;
			if (newIceberg.GetComponent<Iceberg>().isMelted)
			{
				Debug.Log("iceberg melted, can't move");
			}
			else
			{
				levelManager.Move(newIceberg, dir);
				levelManager.DecreaseTurnsLeft();

				if (newIceberg.CompareTag("Goal"))
					levelManager.GameWin();
			}
		}
		catch (NullReferenceException e)
		{
			// Bear falls in water
			Debug.Log("Bear fell in water");
			Debug.Log(e);
		}
	}
}
