using Saving;

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressionManager : MonoBehaviour
{
	public static ProgressionManager theProgressionManager;
	
	public int currentCheckpoint;
	private List<string> questsCompleted;

	private void Awake()
	{
		theProgressionManager = this;
	}

	public void ApplyProgressionData(ProgressData _data)
	{
		currentCheckpoint = _data.currentCheckpoint;
		questsCompleted = _data.questsCompleted;
	}
}
