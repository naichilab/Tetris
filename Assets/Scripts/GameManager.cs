using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{

	public void Awake ()
	{
		if (this != Instance) {
			Destroy (this);
			return;
		}

		DontDestroyOnLoad (this.gameObject);
	}

}