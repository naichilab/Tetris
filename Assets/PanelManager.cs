using UnityEngine;
using System.Collections;

public class PanelManager : MonoBehaviour
{

	[SerializeField]
	private RectTransform TitlePanel;

	[SerializeField]
	private RectTransform ScorePanel;

	[SerializeField]
	private RectTransform GameOverPanel;

	private void Awake ()
	{
		TitlePanel.localPosition = new Vector3 (0, 0, 0);
		ScorePanel.localPosition = new Vector3 (0, 0, 0);
		GameOverPanel.localPosition = new Vector3 (0, 0, 0);
	}

	public bool TitlePanelVisible {
		get{ return this.TitlePanel.gameObject.activeInHierarchy; }
		set{ this.TitlePanel.gameObject.SetActive (value); }
	}

	public bool ScorePanelVisible {
		get{ return this.ScorePanel.gameObject.activeInHierarchy; }
		set{ this.ScorePanel.gameObject.SetActive (value); }
	}

	public bool GameOverPanelVisible {
		get{ return this.GameOverPanel.gameObject.activeInHierarchy; }
		set{ this.GameOverPanel.gameObject.SetActive (value); }
	}


	public void ShowTitle ()
	{
		this.TitlePanelVisible = true;
		this.ScorePanelVisible = true;
		this.GameOverPanelVisible = false;
	}

	public void ShowGameOver ()
	{
		this.TitlePanelVisible = false;
		this.ScorePanelVisible = true;
		this.GameOverPanelVisible = true;
	}
}
