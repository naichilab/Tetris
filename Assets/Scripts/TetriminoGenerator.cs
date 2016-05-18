using UnityEngine;
using System;
using System.Collections;

public class TetriminoGenerator : MonoBehaviour
{

	[SerializeField]
	private GameObject PrefabI;
	[SerializeField]
	private GameObject PrefabO;
	[SerializeField]
	private GameObject PrefabT;
	[SerializeField]
	private GameObject PrefabJ;
	[SerializeField]
	private GameObject PrefabL;
	[SerializeField]
	private GameObject PrefabS;
	[SerializeField]
	private GameObject PrefabZ;

	public Tetrimino Generate ()
	{
		var randomShape = Tetrimino.GetRandomShape ();

		GameObject prefab = null;

		switch (randomShape) {
		case Tetrimino.Shapes.I:
			prefab = this.PrefabI;
			break;
		case Tetrimino.Shapes.O:
			prefab = this.PrefabO;
			break;
		case Tetrimino.Shapes.T:
			prefab = this.PrefabT;
			break;
		case Tetrimino.Shapes.J:
			prefab = this.PrefabJ;
			break;
		case Tetrimino.Shapes.L:
			prefab = this.PrefabL;
			break;
		case Tetrimino.Shapes.S:
			prefab = this.PrefabS;
			break;
		case Tetrimino.Shapes.Z:
			prefab = this.PrefabZ;
			break;
		default:
			Debug.LogError ("Shape Missing");
			break;
		}

		var pos = new Point ((int)this.transform.localPosition.x, (int)this.transform.localPosition.y);

		var mino = Instantiate (prefab);
		mino.transform.parent = this.transform;
		mino.transform.localPosition = Vector3.zero;
		mino.transform.localRotation = Quaternion.identity;

		mino.transform.position = pos.ToVector2;
		mino.transform.rotation = Quaternion.identity;

		var tetrimino = mino.GetComponent<Tetrimino> ();
		tetrimino.SetAbsoluteCenterPoint (pos);

		return tetrimino;
	}
}
