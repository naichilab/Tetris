using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
	public bool IsDebugMode;

	public void Awake ()
	{
		if (this != Instance) {
			Destroy (this);
			return;
		}

		DontDestroyOnLoad (this.gameObject);
	}

}