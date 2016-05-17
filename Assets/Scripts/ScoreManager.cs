using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
	/// <summary>
	/// 1ライン消した時のスコア
	/// </summary>
	const int LINE_CLEAR_SCORE = 1000;

	/// <summary>
	/// 複数ライン消した時の１ラインあたりの加算倍率
	/// </summary>
	const float MULTI_LINE_BONUS = 0.5f;

	/// <summary>
	/// ハードドロップ時の加算スコア(１マスごと）
	/// </summary>
	const int HARD_DROP_SCORE = 50;

	[SerializeField]
	private Text ScoreLabel;

	[SerializeField]
	private Text LinesLabel;

	private int score;
	private int lines;


	public int Score {
		get{ return this.score; }
		private set {
			this.score = value;
			this.ScoreLabel.text = value.ToString ();
		}
	}

	public int Lines {
		get{ return this.lines; }
		private set {
			this.lines = value;
			this.LinesLabel.text = value.ToString ();
		}
	}

	public void Reset ()
	{
		this.Score = 0;
		this.Lines = 0;
	}


	public void AddClearLinesScore (int lines)
	{
		if (lines <= 0) {
			return;
		}
		
		this.Lines += lines;

		//1000 * 0.5 + 0.5 =  1000
		//2000 * 0.5 + 1.0 =  3000
		//3000 * 0.5 + 1.5 =  6000
		//4000 * 0.5 + 2.0 = 10000
		this.Score += (int)((LINE_CLEAR_SCORE * lines) * (MULTI_LINE_BONUS * (lines + 1)));
	}

	public void AddHardDropScore (int rows)
	{
		this.Score += rows * HARD_DROP_SCORE;
	}



	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
