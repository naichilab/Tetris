using UnityEngine;
using System.Collections;

public class Tetrimino : MonoBehaviour
{

	/// <summary>
	/// テトリミノの形状
	/// 参考：http://livedoor.4.blogimg.jp/mkomiz/imgs/f/f/ff82b30d.gif
	/// </summary>
	public enum Shapes
	{
		None = 0,
		I,
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


	void Awake ()
	{
		if (this.Shape == Shapes.None) {
			Debug.LogError ("Missing Shape");
			return;
		}

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
	
	}
}
