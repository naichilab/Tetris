using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
	public bool IsDebugMode;


	[SerializeField]
	private TetriminoGenerator Generator;

	public void Awake ()
	{
		if (this != Instance) {
			Destroy (this);
			return;
		}

		DontDestroyOnLoad (this.gameObject);
	}


	public void CreateMino ()
	{
		this.Generator.Generate ();
	}

}