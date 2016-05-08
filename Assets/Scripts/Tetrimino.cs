using UnityEngine;
using System;
using System.Collections;

public class Tetrimino : MonoBehaviour
{
	/// <summary>
	/// テトリミノの形状
	/// 参考：http://livedoor.4.blogimg.jp/mkomiz/imgs/f/f/ff82b30d.gif
	/// </summary>
	public enum Shapes
	{
		I = 0,
		O,
		T,
		J,
		L,
		S,
		Z
	}

	/// <summary>
	/// 形状
	/// </summary>
	[SerializeField]
	private Shapes Shape;

	[SerializeField]
	private GameObject CenterCubePrefab;

	[SerializeField]
	private GameObject CubePrefab;

	public static Shapes GetRandomShape ()
	{
		var shapeId = UnityEngine.Random.Range (0, Enum.GetValues (typeof(Tetrimino.Shapes)).Length);
		return (Shapes)(shapeId);
	}

	void Awake ()
	{
		//中心キューブ
		var centerCubePrefab = GameManager.Instance.IsDebugMode ? this.CenterCubePrefab : this.CubePrefab;
		this.AddCube (centerCubePrefab, Vector3.zero);

		switch (this.Shape) {
		case Shapes.I:
			this.AddCube (this.CubePrefab, new Vector3 (-1, 0, 0));
			this.AddCube (this.CubePrefab, new Vector3 (1, 0, 0));
			this.AddCube (this.CubePrefab, new Vector3 (2, 0, 0));
			break;
		case Shapes.O:
			this.AddCube (this.CubePrefab, new Vector3 (1, 0, 0));
			this.AddCube (this.CubePrefab, new Vector3 (0, 1, 0));
			this.AddCube (this.CubePrefab, new Vector3 (1, 1, 0));
			break;
		case Shapes.T:
			this.AddCube (this.CubePrefab, new Vector3 (0, 1, 0));
			this.AddCube (this.CubePrefab, new Vector3 (-1, 0, 0));
			this.AddCube (this.CubePrefab, new Vector3 (1, 0, 0));
			break;
		case Shapes.J:
			this.AddCube (this.CubePrefab, new Vector3 (-1, 1, 0));
			this.AddCube (this.CubePrefab, new Vector3 (-1, 0, 0));
			this.AddCube (this.CubePrefab, new Vector3 (1, 0, 0));
			break;
		case Shapes.L:
			this.AddCube (this.CubePrefab, new Vector3 (1, 1, 0));
			this.AddCube (this.CubePrefab, new Vector3 (-1, 0, 0));
			this.AddCube (this.CubePrefab, new Vector3 (1, 0, 0));
			break;
		case Shapes.S:
			this.AddCube (this.CubePrefab, new Vector3 (-1, 0, 0));
			this.AddCube (this.CubePrefab, new Vector3 (0, 1, 0));
			this.AddCube (this.CubePrefab, new Vector3 (1, 1, 0));
			break;
		case Shapes.Z:
			this.AddCube (this.CubePrefab, new Vector3 (-1, 1, 0));
			this.AddCube (this.CubePrefab, new Vector3 (0, 1, 0));
			this.AddCube (this.CubePrefab, new Vector3 (1, 0, 0));
			break;
		}
	}

	void AddCube (GameObject prefab, Vector3 pos)
	{
		var cube = Instantiate (prefab) as GameObject;
		cube.transform.parent = this.transform;
		cube.transform.localPosition = pos;
		cube.transform.localRotation = Quaternion.identity;
	}


	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		var p = this.transform.position;
		if (Input.GetKey (KeyCode.LeftArrow)) {
			transform.position = new Vector3 (p.x - 1, p.y, p.z);
		} else if (Input.GetKey (KeyCode.RightArrow)) {
			transform.position = new Vector3 (p.x + 1, p.y, p.z);
		} else if (Input.GetKey (KeyCode.DownArrow)) {
			transform.position = new Vector3 (p.x, p.y - 1, p.z);
		}	
	}
}
