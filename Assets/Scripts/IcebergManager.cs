using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class IcebergManager : MonoBehaviour
{
	public float slideSpeed = 10f;

	[System.Serializable]
	public struct IcebergLayer
	{
		public GameObject obj;

		// A melt layer of 1 means the iceberg melts and reappears at level 1. Greater numbers melt sooner and reappear later
		public int meltLayer;
	}

	public IcebergLayer[] icebergLayers;

	private GameObject player;

	void Awake()
	{
		player = GameObject.FindGameObjectWithTag("Player");
	}

	void Update()
	{

	}

	// Negative levels are melting, positive are freezing
	public void UpdateIcebergs(int level)
	{
		// Debug.Log(level);
		foreach (IcebergLayer icebergLayer in icebergLayers)
		{
			// Check which icebergs to process
			if (icebergLayer.meltLayer == Mathf.Abs(level))
			{
				// Check if player sinks
				if (player.transform.IsChildOf(icebergLayer.obj.transform))
				{
					GetComponent<LevelManager>().GameOver();
				}

				// if level > 0, melting turns
				icebergLayer.obj.GetComponent<Iceberg>().Melt(level < 0);
			}
			else if (level < 0 && icebergLayer.meltLayer + level == -1)
			{
				icebergLayer.obj.GetComponent<Iceberg>().Pre(true); // Pre-melt
			}
			else if (level > 0 && icebergLayer.meltLayer - level == 1)
			{
				icebergLayer.obj.GetComponent<Iceberg>().Pre(false); // Pre-freeze
			}
		}
	}
}
