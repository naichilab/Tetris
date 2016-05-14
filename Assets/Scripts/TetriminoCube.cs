using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
#endif


public class TetriminoCube : MonoBehaviour
{
	/// <summary>
	/// 座標（テトリミノ中心座標からの距離）
	/// </summary>
	public Point Point;

	/// <summary>
	/// 回転移動する
	/// </summary>
	public void Rotate (RotationDirection rotationDirection)
	{
		switch (rotationDirection) {
		case RotationDirection.Clockwise:
			this.Point.RotateClockwise (Point.Zero);
			break;
		case RotationDirection.CounterClockwise:
			this.Point.RotateCounterClockwise (Point.Zero);
			break;
		}

		this.transform.localPosition = this.Point.Vector2;
	}

	#if UNITY_EDITOR
	[CustomEditor (typeof(TetriminoCube))]
	public class TetriminoCubeEditor : Editor
	{
		public override void OnInspectorGUI ()
		{
			base.OnInspectorGUI ();
			TetriminoCube tetriminoCube = target as TetriminoCube;
			EditorGUILayout.LabelField ("座標", tetriminoCube.Point.ToString ());
		}
	}
	#endif
}
