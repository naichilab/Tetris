using UnityEngine;
using System.Collections;

public class SaveData
{
	private const string HIGHSCORE = "HIGHSCORE";

	public static int HighScore {
		get {
			return PlayerPrefs.GetInt (HIGHSCORE);
		}
		set {
			PlayerPrefs.SetInt (HIGHSCORE, value);
		}
	}
}
