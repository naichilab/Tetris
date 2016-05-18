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
public class TetrisField:MonoBehaviour
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
	/// 壁の厚み
	/// </summary>
	const int WALL_THICKNESS = 1;

	/// <summary>
	/// 天井の厚み
	/// </summary>
	const int CEIL_THICKNESS = 3;

	/// <summary>
	/// 合計幅
	/// </summary>
	const int TOTAL_WIDTH = WALL_THICKNESS + FIELD_WIDTH + WALL_THICKNESS;

	/// <summary>
	/// 合計高さ
	/// </summary>
	const int TOTAL_HEIGHT = WALL_THICKNESS + FIELD_HEIGHT + CEIL_THICKNESS;

	const int CEIL_HEIGHT = WALL_THICKNESS + FIELD_HEIGHT;


	public List<Row> Rows { get; private set; }


	public Row this [int rowIdx] {
		get {
			return this.Rows [rowIdx];
		}
	}


	/// <summary>
	/// 全セルを初期化
	/// </summary>
	public void Reset ()
	{
		this.Rows = new List<Row> ();

		for (int r = 0; r < TOTAL_HEIGHT; r++) {
			var row = new Row ();
			for (int c = 0; c < TOTAL_WIDTH; c++) {
				bool leftWall = c == 0;
				bool rightWall = c == FIELD_WIDTH + 1;
				bool floor = r == 0;
				bool ceil = r >= CEIL_HEIGHT;

				Cell cell = null;
				if (leftWall || rightWall || floor) {
					cell = new Cell (Cell.Contents.Wall);
				} else if (ceil) {
					cell = new Cell (Cell.Contents.Ceil);
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
		return absolutePoints.All (p => this [p.Y] [p.X].IsEmpty);
	}


	/// <summary>
	/// テトリミノを固定する
	/// </summary>
	public void FixTetrimino (Tetrimino mino)
	{
		foreach (var c in mino.GetCubes()) {
			var abs = mino.AbsoluteCenterPoint + c.DistanceFromTetriminoCenter;
			this [abs.Y] [abs.X].Cube = c;
		}
	}




	/// <summary>
	/// 揃った行を消す
	/// </summary>
	/// <returns>消した行数</returns>
	public int ClearLines ()
	{
		int deletedRowCount = 0;
		while (true) {
			Row filledRow = this.Rows.Where (r => r.IsFilled).FirstOrDefault ();
			if (filledRow == null) {
				break;
			}

			filledRow.Cells
				.Where (c => c.Cube != null)
				.ToList ()
				.ForEach (c => c.Cube.DestroyGameObject ());

			filledRow.Clear ();

			//空いた行を詰める
			int row = this.Rows.IndexOf (filledRow);
			for (int r = row + 1; r < this.Rows.Count; r++) {
				Row lowerRow = this [r - 1];
				Row upperRow = this [r];
				//上から下へ移動
				for (int colIdx = 0; colIdx < upperRow.Cells.Count; colIdx++) {
					if (upperRow [colIdx].HasCube) {
						var cube = upperRow [colIdx].Cube;
						cube.Move (new Point (0, -1));	//座標を移動
						lowerRow [colIdx].Cube = cube;	//下の行に移す
						upperRow [colIdx].Clear ();		//上の行の参照を消す
					}
				}
			}

			deletedRowCount++;
		}

		return deletedRowCount;
	}


	/// <summary>
	/// 天井まで届いたテトリミノがあるか
	/// </summary>
	/// <value>The ceil reached.</value>
	public bool CeilReached {
		get {
			return this.Rows.Any (r => r.Cells.Any (c => c.IsCeil && c.HasCube));
		}
	}


	#if UNITY_EDITOR
	[CustomEditor (typeof(TetrisField))]
	public class FieldEditor : Editor
	{
		public override void OnInspectorGUI ()
		{
			base.OnInspectorGUI ();
			TetrisField tetrisField = target as TetrisField;

			if (tetrisField.Rows == null) {
				return;
			}


			for (int row = FIELD_HEIGHT + 1; row >= 0; row--) {
				EditorGUILayout.BeginHorizontal ();

				for (int col = 0; col < FIELD_WIDTH + 2; col++) {
					EditorGUILayout.BeginVertical ();

					EditorGUILayout.Toggle (!tetrisField [row] [col].IsEmpty);
//					EditorGUILayout.LabelField (((int)field.field [col, row].Contents).ToString ());

					EditorGUILayout.EndVertical ();

				}
				EditorGUILayout.EndHorizontal ();
			}
		}
	}
	#endif



}
