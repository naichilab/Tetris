using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
	public bool IsDebugMode;


	[SerializeField]
	private TetriminoGenerator Generator;

	[SerializeField]
	private TetrisLogic Logic;

	[SerializeField]
	private float Interval = 1.0f;

	/// <summary>
	/// ミノが最後に動いた時間
	/// </summary>
	private float LastUpdated;


	/// <summary>
	/// 今動かしているミノ
	/// </summary>
	private Tetrimino CurrentMino{ get; set; }

	public void Awake ()
	{
		if (this != Instance) {
			Destroy (this);
			return;
		}

		DontDestroyOnLoad (this.gameObject);
	}

	public void Update ()
	{
		if (this.LastUpdated + this.Interval < Time.time) {
			this.Step ();
		}
	}

	private void Step ()
	{
		if (CurrentMino == null) {
			this.CreateMino ();
		} else {
			
		}
	}

	public void CreateMino ()
	{
		this.CurrentMino = this.Generator.Generate ();
		this.LastUpdated = Time.time;
	}

}