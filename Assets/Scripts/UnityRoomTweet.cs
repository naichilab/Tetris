using UnityEngine;

public static class UnityRoomTweet
{
	const string SHAREURL = "http://twitter.com/share?";


	/// <summary>
	///  ツイートします。
	/// </summary>
	/// <param name="text">本文</param>
	public static void Tweet (string text)
	{
		Tweet (text, null);
	}


	/// <summary>
	/// ツイートします。
	/// </summary>
	/// <param name="text">本文</param>
	/// <param name="hashtag">ハッシュタグ(#は不要)</param>
	public static void Tweet (string text, string hashtag)
	{
		if (Application.platform == RuntimePlatform.WebGLPlayer) {

			var sb = new System.Text.StringBuilder ();
			sb.Append (SHAREURL);
			sb.Append ("original_referer=");
			sb.Append ("&text=" + WWW.EscapeURL (text));
			if (!string.IsNullOrEmpty (hashtag))
				sb.Append ("&hashtags=" + WWW.EscapeURL (hashtag));
			Application.ExternalEval ("var F = 0;if (screen.height > 500) {F = Math.round((screen.height / 2) - (250));}window.open('" + sb.ToString () + "','intent','left='+Math.round((screen.width/2)-(250))+',top='+F+',width=500,height=260,personalbar=no,toolbar=no,resizable=no,scrollbars=yes');");
		} else {
			Debug.Log ("WebGL以外では実行できません。");
		}
	}
}