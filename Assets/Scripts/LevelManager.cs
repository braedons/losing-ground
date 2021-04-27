using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
	public int meltTurnsInitial;
	private int meltTurnsLeft;
	public int freezeTurnsInitial;
	private int freezeTurnsLeft;

	public HUD hud;

	private TutorialTextUpdater tutorialTextUpdater;
	public IcebergManager icebergManager;
	private GameObject player;

	public GameObject gameOver;
	public GameObject gameWin;

	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");

		meltTurnsLeft = meltTurnsInitial;
		freezeTurnsLeft = freezeTurnsInitial;

		hud.UpdateTurnsLeft(meltTurnsLeft, freezeTurnsLeft);
		icebergManager.UpdateIcebergs(-meltTurnsInitial);

		tutorialTextUpdater = GetComponent<TutorialTextUpdater>();
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.R))
			Restart();
	}

	public void DecreaseTurnsLeft()
	{
		if (meltTurnsLeft > 0)
		{
			meltTurnsLeft--;
			icebergManager.UpdateIcebergs(-meltTurnsLeft);
		}
		else if (freezeTurnsLeft > 0)
		{
			freezeTurnsLeft--;
			icebergManager.UpdateIcebergs(freezeTurnsInitial - freezeTurnsLeft);
		}
		else
			Debug.Log("Out of turns");

		hud.UpdateTurnsLeft(meltTurnsLeft, freezeTurnsLeft);
	}

	public void Move(GameObject newIceberg, Vector3 dir)
	{
		player.transform.Translate(dir);
		player.transform.parent = newIceberg.transform;
		tutorialTextUpdater.NewParent(newIceberg.transform);
		StartCoroutine(newIceberg.GetComponent<Iceberg>().Slide(dir));
	}

	public void GameOver()
	{
		Debug.Log("game over");
		gameOver.SetActive(true);
	}

	public void GameWin()
	{
		Debug.Log("game win");
		gameWin.SetActive(true);
	}

	public void Restart()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void NextLevel()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
}
