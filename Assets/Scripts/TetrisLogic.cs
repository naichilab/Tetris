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
	private IntervalManager IntervalManager { get; set; }


	/// <summary>
	/// テトリミノ生成器
	/// </summary>
	private TetriminoGenerator TetriminoGenerator;

	/// <summary>
	/// テトリスフィールド
	/// </summary>
	private TetrisField TetrisField;


	/// <summary>
	/// スコア管理クラス
	/// </summary>
	private ScoreManager ScoreManager;

	/// <summary>
	/// テトリミノが自動落下する時間
	/// </summary>
	private float NextAutoDropTime;


	private void Start ()
	{
		//各種コンポーネントを取得
		this.IntervalManager = this.gameObject.GetComponent<IntervalManager> ();
		this.TetriminoGenerator = this.gameObject.GetComponent<TetriminoGenerator> ();
		this.TetrisField = this.gameObject.GetComponent<TetrisField> ();
		this.ScoreManager = this.gameObject.GetComponent<ScoreManager> ();

		this.NewGame ();
	}

	private void Update ()
	{
		if (!this.IsGameOver && Time.time > this.NextAutoDropTime) {
			//自動落下
			if (this.CanMove (TetriminoOperation.MoveDown)) {
				this.Move (TetriminoOperation.MoveDown);
			} else {
				this.FixMino ();
			}
			this.UpdateNextAutoDropTime ();
		}
	}


	/// <summary>
	/// 新規ゲーム開始
	/// </summary>
	public void NewGame ()
	{
		this.ScoreManager.Reset ();
		this.IntervalManager.Reset ();
		this.TetrisField.Reset ();

		this.CreateMino ();
		this.UpdateNextAutoDropTime ();
	}



	public bool HasCurrentMino {
		get{ return this.CurrentMino != null; }
	}


	public bool CanMove (TetriminoOperation op)
	{
		if (this.CurrentMino == null) {
			return false;
		}
		
		switch (op) {
		case TetriminoOperation.MoveLeft:
			return this.TetrisField.Placeable (this.CurrentMino.GetMovedAbsolutePoints (new Point (-1, 0)));
		case TetriminoOperation.MoveRight:
			return this.TetrisField.Placeable (this.CurrentMino.GetMovedAbsolutePoints (new Point (1, 0)));
		case TetriminoOperation.MoveDown:
			return this.TetrisField.Placeable (this.CurrentMino.GetMovedAbsolutePoints (new Point (0, -1)));
		case TetriminoOperation.RotateClockwise:
			return this.TetrisField.Placeable (this.CurrentMino.GetRotatedAbsolutePoints (RotateDirection.Clockwise));
		case TetriminoOperation.RotateCounterClockwise:
			return this.TetrisField.Placeable (this.CurrentMino.GetRotatedAbsolutePoints (RotateDirection.CounterClockwise));
		}

		return false;
	}


	public void Move (TetriminoOperation op, int score = 0)
	{
		if (this.CurrentMino == null) {
			return;
		}

		switch (op) {
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
		}
		this.ScoreManager.AddHardDropScore (score);
	}


	public void CreateMino ()
	{
		if (this.CurrentMino != null) {
			Debug.LogError ("Current Tetrimino is exists");
		}

		this.CurrentMino = this.TetriminoGenerator.Generate ();
	}

	public void FixMino ()
	{
		if (this.CurrentMino == null) {
			return;
		}

		this.TetrisField.FixTetrimino (this.CurrentMino);
		this.CurrentMino = null;

		this.ClearLines ();

		if (this.IsGameOver) {
			Debug.Log ("Game Over!!!!!");
		} else {
			this.CreateMino ();
		}

	}

	public void ClearLines ()
	{
		int deletedRowCount = this.TetrisField.ClearLines ();
		this.ScoreManager.AddClearLinesScore (deletedRowCount);
	}

	public bool IsGameOver {
		get { 
			return this.TetrisField.CeilReached;
		}
	}




	/// <summary>
	/// 自動落下時刻を更新する
	/// </summary>
	private void UpdateNextAutoDropTime ()
	{
		this.NextAutoDropTime = Time.time + this.IntervalManager.GetInterval ();
	}

}
