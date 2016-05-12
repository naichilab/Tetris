using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
	public bool IsDebugMode;


	[SerializeField]
	private TetriminoGenerator Generator;

	[SerializeField]
	private Field Field;

	[SerializeField]
	private TetrisLogic Logic;

	[SerializeField]
	private float Interval = 1.0f;


	/// <summary>
	/// ミノが最後に動いた時間
	/// </summary>
	private float LastUpdated;


	public void Awake ()
	{
		if (this != Instance) {
			Destroy (this);
			return;
		}

		DontDestroyOnLoad (this.gameObject);
	}

	public void Start ()
	{
		this.Logic.SetTetriminoGenerator (this.Generator);
		this.Logic.SetField (this.Field);
		
	}

	public void Update ()
	{
		//ユーザーの操作
		var p = this.transform.position;
		if (Input.GetKey (KeyCode.LeftArrow)) {
			if (this.Logic.CanMove (TetrisLogic.Direction.Left)) {
				this.Logic.Move (TetrisLogic.Direction.Left);
			}
		} else if (Input.GetKey (KeyCode.RightArrow)) {
			if (this.Logic.CanMove (TetrisLogic.Direction.Right)) {
				this.Logic.Move (TetrisLogic.Direction.Right);
			}
		} else if (Input.GetKey (KeyCode.DownArrow)) {
			if (this.Logic.CanMove (TetrisLogic.Direction.Bottom)) {
				this.Logic.Move (TetrisLogic.Direction.Bottom);
			}
		}	

		//自動落下
		if (this.LastUpdated + this.Interval < Time.time) {
			if (!this.Logic.HasCurrentMino) {
				this.Logic.CreateMino ();
				this.LastUpdated = Time.time;
			} else {
				if (this.Logic.CanMove (TetrisLogic.Direction.Bottom)) {
					this.Logic.Move (TetrisLogic.Direction.Bottom);
				}
			}
			this.LastUpdated = Time.time;
		}
	}


}