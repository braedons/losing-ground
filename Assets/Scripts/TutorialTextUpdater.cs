using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialTextUpdater : MonoBehaviour
{
	[System.Serializable]
	public struct TutorialTextPair
	{
		public string newText;
		public Transform newParent;
	}

	public TutorialTextPair[] tutorialTextPairs;
	private Dictionary<Transform, string> textPairsMap;

	public TextMeshProUGUI text;

	void Start()
	{
		if (text == null)
		{
			Debug.LogWarning("tutorial text to update is null");
		}

		textPairsMap = new Dictionary<Transform, string>();
		foreach (var pair in tutorialTextPairs)
		{
			textPairsMap[pair.newParent] = pair.newText;
		}
	}

	void Update()
	{

	}

	public void NewParent(Transform newIceberg)
	{
		if (textPairsMap.ContainsKey(newIceberg))
		{
			text.text = textPairsMap[newIceberg];
		}
	}
}
