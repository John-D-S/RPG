using Saving;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressionManager : MonoBehaviour
{
	private int currentCheckpoint;
	private List<string> questsCompleted;
	
	public void ApplyProgressionData(ProgressData _data)
	{
		currentCheckpoint = _data.currentCheckpoint;
		questsCompleted = _data.questsCompleted;
	}
}
