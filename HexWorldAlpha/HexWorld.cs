using Gamelogic.Grids;
using UnityEngine;

public class HexWorld : GLMonoBehaviour
{
	public Block blockPrefab;
	public GameObject gridRoot;
	public Texture2D heightMap;

	private FlatHexGrid<Block> grid;
	private IMap3D<FlatHexPoint> map;

	public void Awake()
	{
		BuildGrid();
	}

	public void Update()
	{

	}

	private void BuildGrid()
	{
		grid = FlatHexGrid<Block>.FatRectangle(20, 30);

		map = new FlatHexMap(new Vector2(.80f, .69f) * 6.0f)
			.To3DXZ();

		var map2D = new FlatHexMap(new Vector2(80, 60) * 0.05f);

		foreach (var point in grid)
		{
			var block = Instantiate(blockPrefab);
			block.gameObject.isStatic = true;
			block.transform.parent = gridRoot.transform;
			block.gameObject.tag = transform.gameObject.tag;
			block.gameObject.layer = LayerMask.NameToLayer("HexWorld");

			// This wipes out scale for some reason
			block.transform.rotation = Quaternion.LookRotation(gridRoot.transform.up);
			// block.transform.rotation = Quaternion.Euler(new Vector3(transform.localRotation.z + 90, 90, 90));

			block.transform.localPosition = map[point];

			int x = Mathf.FloorToInt(map2D[point].x);
			int y = Mathf.FloorToInt(map2D[point].y);
			float height = heightMap.GetPixel(x, y).r * 30 - 10;

			Debug.Log("Height: " + height);
			if (height <= 0)
			{
				height = 0.01f;
			}

			block.SetOrigColor(Utils.Blend(height, Utils.colors[0], Utils.colors[1]));
			block.transform.localScale = new Vector3(3.5f, 3.5f, -height);

			grid[point] = block;
		}
	}

	public FlatHexGrid<Block> GetGrid()
	{
		return grid;
	}
	public IMap3D<FlatHexPoint> GetMap()
	{
		return map;
	}
}
