using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;


#if UNITY_EDITOR
using UnityEditor;
#endif


/// <summary>
/// テトリスフィールド
/// </summary>
public class Field:MonoBehaviour
{

	/// <summary>
	/// 可用範囲の幅
	/// </summary>
	const int FIELD_WIDTH = 10;
	/// <summary>
	/// 可用範囲の高さ
	/// </summary>
	const int FIELD_HEIGHT = 20;


	/// <summary>
	/// フィールド
	/// </summary>
	Cell[,] field = null;


	/// <summary>
	/// 全セルを初期化
	/// </summary>
	public void Reset ()
	{
		//左右の壁、床、天井を含めたサイズで初期化
		this.field = new Cell[FIELD_WIDTH + 2, FIELD_HEIGHT + 2];

		for (int col = 0; col < FIELD_WIDTH + 2; col++) {
			for (int row = 0; row < FIELD_HEIGHT + 2; row++) {
				bool leftWall = col == 0;
				bool rightWall = col == FIELD_WIDTH + 1;
				bool floor = row == 0;

				Cell cell = null;
				if (leftWall || rightWall || floor) {
					cell = new Cell (Cell.Contents.Wall);
				} else {
					cell = new Cell (Cell.Contents.Empty);
				}
				this.field [col, row] = cell;
			}
		}
	}

	/// <summary>
	/// 指定座標に配置可能かどうか
	/// </summary>
	/// <param name="absolutePoints">Absolute points.</param>
	public bool Placeable (IEnumerable<Point> absolutePoints)
	{
		return absolutePoints.All (p => this.field [p.X, p.Y].IsEmpty);
	}


	/// <summary>
	/// テトリミノを固定する
	/// </summary>
	public void FixTetrimino (Tetrimino mino)
	{
		mino.GetAbsolutePoints ()
			.ToList ()
			.ForEach (p => this.field [p.X, p.Y].SetCube ());
	}

	#if UNITY_EDITOR
	[CustomEditor (typeof(Field))]
	public class FieldEditor : Editor
	{
		public override void OnInspectorGUI ()
		{
			base.OnInspectorGUI ();
			Field field = target as Field;

			if (field.field == null) {
				return;
			}


			for (int row = FIELD_HEIGHT + 1; row >= 0; row--) {
				EditorGUILayout.BeginHorizontal ();

				for (int col = 0; col < FIELD_WIDTH + 2; col++) {
					EditorGUILayout.BeginVertical ();

					EditorGUILayout.Toggle (!field.field [col, row].IsEmpty);
//					EditorGUILayout.LabelField (((int)field.field [col, row].Contents).ToString ());

					EditorGUILayout.EndVertical ();

				}
				EditorGUILayout.EndHorizontal ();
			}
		}
	}
	#endif


}
