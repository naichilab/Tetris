using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

[RequireComponent (typeof(PanelManager))]
public class GameManager : SingletonMonoBehaviour<GameManager>
{
	public bool IsDebugMode;

	[SerializeField]
	private TetrisLogic Logic;


	private PanelManager panelManager;


	private void Awake ()
	{
		if (this != Instance) {
			Destroy (this);
			return;
		}
		DontDestroyOnLoad (this.gameObject);

		//各種コンポーネントを取得
		this.panelManager = this.gameObject.GetComponent<PanelManager> ();
	}

	private void Start ()
	{
		this.ShowTitle ();
	}

	public void GameStart ()
	{
		Debug.Log ("GameStart");


		//タイトル非表示
		this.panelManager.TitlePanelVisible = false;

		//ゲーム開始
		this.Logic.NewGame ();
	}

	public void GameOver (TetrisLogic tl)
	{
		this.panelManager.ShowGameOver ();
	}

	public void ShowTitle ()
	{
		//GameOver時のCurrentMinoを消す
		if (this.Logic.CurrentMino != null) {
			Destroy (this.Logic.CurrentMino.gameObject);
		}
		
		this.Logic.Reset ();
		this.panelManager.ShowTitle ();
	}

	public void TweetScore ()
	{
		string msg = string.Format ("京ゆにテトリス(難しめ) {0}ライン消して{1}点でした。", this.Logic.ScoreManager.Lines, this.Logic.ScoreManager.Score);

		UnityRoomTweet.Tweet (msg, "com-naichilab-tetris", "kyounitetris");
	}

}