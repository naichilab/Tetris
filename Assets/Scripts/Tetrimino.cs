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
		if(this.Shape == Shapes.None){
			Debug.LogError("Missing Shape")
			return;
		}

		var centerCube = Instantiate(this.CenterCubePrefab) as GameObject;
		centerCube.transform.parent = this.transform;
		centerCube.transform.localPosition = new Vector3(0,0,0) ;
		centerCube.transform.localRotation = Quaternion.identity;

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
