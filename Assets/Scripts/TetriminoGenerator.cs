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

	public Tetrimino Generate (Vector2 pos)
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

		var mino = Instantiate (prefab);
		mino.transform.parent = this.transform;
		mino.transform.localPosition = Vector3.zero;
		mino.transform.localRotation = Quaternion.identity;

		mino.transform.position = pos;
		mino.transform.rotation = Quaternion.identity;

//
//		mino.AddComponent<Rigidbody> ();
//		mino.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
//

		return mino.GetComponent<Tetrimino> ();
	}


	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
