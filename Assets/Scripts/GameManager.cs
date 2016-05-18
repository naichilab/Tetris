using UnityEngine;
using System.Collections.Generic;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
	public bool IsDebugMode;

	[SerializeField]
	private TetrisLogic Logic;


	public void Awake ()
	{
		if (this != Instance) {
			Destroy (this);
			return;
		}
		DontDestroyOnLoad (this.gameObject);
	}

}