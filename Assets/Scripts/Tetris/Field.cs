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

	//	/// <summary>
	//	/// フィールド
	//	/// </summary>
	//	Cell[,] field = null;

	public List<Row> Rows { get; private set; }



	/// <summary>
	/// 全セルを初期化
	/// </summary>
	public void Reset ()
	{
		this.Rows = new List<Row> ();

		for (int r = 0; r < FIELD_HEIGHT + 2; r++) {
			var row = new Row ();
			for (int c = 0; c < FIELD_WIDTH + 2; c++) {
				bool leftWall = c == 0;
				bool rightWall = c == FIELD_WIDTH + 1;
				bool floor = r == 0;

				Cell cell = null;
				if (leftWall || rightWall || floor) {
					cell = new Cell (Cell.Contents.Wall);
				} else {
					cell = new Cell (Cell.Contents.Empty);
				}

				row.Cells.Add (cell);
			}
			this.Rows.Add (row);
		}

	}

	/// <summary>
	/// 指定座標に配置可能かどうか
	/// </summary>
	/// <param name="absolutePoints">Absolute points.</param>
	public bool Placeable (IEnumerable<Point> absolutePoints)
	{
		return absolutePoints.All (p => this.Rows [p.Y].Cells [p.X].IsEmpty);
	}


	/// <summary>
	/// テトリミノを固定する
	/// </summary>
	public void FixTetrimino (Tetrimino mino)
	{
		mino.GetAbsolutePoints ()
			.ToList ()
			.ForEach (p => this.Rows [p.Y].Cells [p.X].SetCube ());
	}

	public void ClearLines ()
	{
		
		
	}


	#if UNITY_EDITOR
	[CustomEditor (typeof(Field))]
	public class FieldEditor : Editor
	{
		public override void OnInspectorGUI ()
		{
			base.OnInspectorGUI ();
			Field field = target as Field;

			if (field.Rows == null) {
				return;
			}


			for (int row = FIELD_HEIGHT + 1; row >= 0; row--) {
				EditorGUILayout.BeginHorizontal ();

				for (int col = 0; col < FIELD_WIDTH + 2; col++) {
					EditorGUILayout.BeginVertical ();

					EditorGUILayout.Toggle (!field.Rows [row].Cells [col].IsEmpty);
//					EditorGUILayout.LabelField (((int)field.field [col, row].Contents).ToString ());

					EditorGUILayout.EndVertical ();

				}
				EditorGUILayout.EndHorizontal ();
			}
		}
	}
	#endif



}
