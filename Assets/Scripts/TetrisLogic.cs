using UnityEngine;
using System.Collections;


/// <summary>
/// テトリスロジック
/// </summary>
[RequireComponent (typeof(IntervalManager))]
[RequireComponent (typeof(TetriminoGenerator))]
[RequireComponent (typeof(TetrisField))]
[RequireComponent (typeof(ScoreManager))]
public class TetrisLogic : MonoBehaviour
{

	/// <summary>
	/// 現在操作中のテトリミノ
	/// </summary>
	public Tetrimino CurrentMino = null;

	/// <summary>
	/// 自動落下速度を扱うクラス
	/// </summary>
	/// <value>The interval manager.</value>
	private IntervalManager intervalManager { get; set; }

	/// <summary>
	/// テトリミノ生成器
	/// </summary>
	private TetriminoGenerator tetriminoGenerator;

	/// <summary>
	/// テトリスフィールド
	/// </summary>
	private TetrisField tetrisField;


	/// <summary>
	/// スコア管理クラス
	/// </summary>
	public ScoreManager ScoreManager{ get; private set; }

	/// <summary>
	/// テトリミノが自動落下する時間
	/// </summary>
	private float nextAutoDropTime;

	/// <summary>
	/// 操作の入力を行うクラス
	/// </summary>
	[SerializeField]
	private InputBase inputManager;

	private void Awake ()
	{
		//各種コンポーネントを取得
		this.intervalManager = this.gameObject.GetComponent<IntervalManager> ();
		this.tetriminoGenerator = this.gameObject.GetComponent<TetriminoGenerator> ();
		this.tetrisField = this.gameObject.GetComponent<TetrisField> ();
		this.ScoreManager = this.gameObject.GetComponent<ScoreManager> ();
	}

	private void Start ()
	{

		//キー入力イベントハンドラ
		this.inputManager.KeyPressed += (sender, e) => {
			if (this.CanMove (e.Operation)) {
				this.Move (e.Operation);
			}
		};
	}

	private void Update ()
	{
		if (this.CurrentMino != null && !this.IsGameOver && Time.time > this.nextAutoDropTime) {
			//自動落下
			if (this.CanMove (TetriminoOperation.MoveDown)) {
				this.Move (TetriminoOperation.MoveDown);
			} else {
				this.FixMino ();
			}
			this.UpdateNextAutoDropTime ();
		}
	}

	public void Reset ()
	{
		this.ScoreManager.Reset ();
		this.intervalManager.Reset ();
		this.tetrisField.Reset ();
	}

	/// <summary>
	/// 新規ゲーム開始
	/// </summary>
	public void NewGame ()
	{
		this.Reset ();

		this.CreateMino ();
		this.UpdateNextAutoDropTime ();
	}


	public bool CanMove (TetriminoOperation op)
	{
		if (this.CurrentMino == null) {
			return false;
		}
		
		switch (op) {
		case TetriminoOperation.None:
			return this.tetrisField.Placeable (this.CurrentMino.GetMovedAbsolutePoints (Point.Zero));
		case TetriminoOperation.MoveLeft:
			return this.tetrisField.Placeable (this.CurrentMino.GetMovedAbsolutePoints (new Point (-1, 0)));
		case TetriminoOperation.MoveRight:
			return this.tetrisField.Placeable (this.CurrentMino.GetMovedAbsolutePoints (new Point (1, 0)));
		case TetriminoOperation.MoveDown:
			return this.tetrisField.Placeable (this.CurrentMino.GetMovedAbsolutePoints (new Point (0, -1)));
		case TetriminoOperation.RotateClockwise:
			return this.tetrisField.Placeable (this.CurrentMino.GetRotatedAbsolutePoints (RotateDirection.Clockwise));
		case TetriminoOperation.RotateCounterClockwise:
			return this.tetrisField.Placeable (this.CurrentMino.GetRotatedAbsolutePoints (RotateDirection.CounterClockwise));
		case TetriminoOperation.HardDrop:
			int step = 0;
			while (this.tetrisField.Placeable (this.CurrentMino.GetMovedAbsolutePoints (new Point (0, -(step + 1))))) {
				step++;
			}
			return step > 0;
		}

		return false;
	}


	public void Move (TetriminoOperation op, int score = 0)
	{
		if (this.CurrentMino == null) {
			return;
		}

		switch (op) {
		case TetriminoOperation.None:
			break;
		case TetriminoOperation.MoveLeft:
			this.CurrentMino.Move (new Point (-1, 0));
			break;
		case TetriminoOperation.MoveRight:
			this.CurrentMino.Move (new Point (1, 0));
			break;
		case TetriminoOperation.MoveDown:
			this.CurrentMino.Move (new Point (0, -1));
			break;
		case TetriminoOperation.RotateClockwise:
			this.CurrentMino.Rotate (RotateDirection.Clockwise);
			break;
		case TetriminoOperation.RotateCounterClockwise:
			this.CurrentMino.Rotate (RotateDirection.CounterClockwise);
			break;
		case TetriminoOperation.HardDrop:
			while (true) {
				if (this.tetrisField.Placeable (this.CurrentMino.GetMovedAbsolutePoints (new Point (0, -1)))) {
					this.CurrentMino.Move (new Point (0, -1));
					this.ScoreManager.AddHardDropScore (1);
				} else {
					this.FixMino ();
					break;
				}
			}
			break;
		}

	}


	public void CreateMino ()
	{
		if (this.CurrentMino != null) {
			Debug.LogError ("Current Tetrimino is exists");
		}

		this.CurrentMino = this.tetriminoGenerator.Generate ();

		if (this.IsGameOver) {
			GameManager.Instance.GameOver (this);
		}

	}

	public void FixMino ()
	{
		if (this.CurrentMino == null) {
			return;
		}

		this.tetrisField.FixTetrimino (this.CurrentMino);
		this.CurrentMino = null;

		this.ClearLines ();

		this.CreateMino ();
	}

	public void ClearLines ()
	{
		int deletedRowCount = this.tetrisField.ClearLines ();
		this.ScoreManager.AddClearLinesScore (deletedRowCount);
	}

	public bool IsGameOver {
		get { 
			return this.CurrentMino != null && !this.CanMove (TetriminoOperation.None);
		}
	}




	/// <summary>
	/// 自動落下時刻を更新する
	/// </summary>
	private void UpdateNextAutoDropTime ()
	{
		this.intervalManager.Rank = (int)(this.ScoreManager.Score / 10000);
		
		this.nextAutoDropTime = Time.time + this.intervalManager.GetInterval ();
	}

}
