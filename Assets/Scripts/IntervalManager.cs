using UnityEngine;
using System.Collections;


/// <summary>
/// 自動落下の間隔を制御するクラス
/// </summary>
public class IntervalManager : MonoBehaviour
{
	/*
	 * 経過時間でだんだん難しくなるようにする
	 * 参考：https://twitter.com/abagames/status/471998089402646528
     * [難度] = sqrt([経過フレーム数] * 0.0001) + 1
	*/

	private int frameCount = 0;

	/// <summary>
	/// 難度
	/// 最低1.0〜いくらでも
	/// </summary>
	/// <value>The difficulty.</value>
	private float difficulty {
		get {
			return Mathf.Sqrt (this.frameCount * 0.0002f) + 1f;
		}
	}

	private void Update ()
	{
		this.frameCount++;
	}


	public void Reset ()
	{
		this.frameCount = 0;
	}


	/// <summary>
	/// 自動落下の間隔
	/// </summary>
	public float GetInterval ()
	{
		//最初は１秒間隔、時間経過で早くなっていく。
		return 1f / this.difficulty;
	}


}
