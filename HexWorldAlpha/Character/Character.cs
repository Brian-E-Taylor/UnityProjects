using UnityEngine;
using System.Collections;
using Gamelogic.Grids;

public class Character : MonoBehaviour
{
	Inventory inventory;
	// Skills
	// Skill tree
	
	// Class
	// Class tree

	public int startPositionX;
	public int startPositionY;
	public HexWorld startHexWorld;
	public int movement;
	
	private FlatHexPoint myLocationPoint;

	public FlatHexPoint GetLocation()
	{
		return myLocationPoint;
	}
	
	public void SetLocation(int x, int y, HexWorld hexWorld)
	{
		SetLocation(new FlatHexPoint(x, y), hexWorld);
	}
	
	public void SetLocation(FlatHexPoint point, HexWorld hexWorld)
	{
		IMap3D<FlatHexPoint> map = hexWorld.GetMap();
		FlatHexGrid<Block> grid = hexWorld.GetGrid();
		if (grid.Contains(point))
		{
			Vector3 worldPoint = new Vector3(map[point].x, map[point].y + 20.0f, map[point].z);
			int layerMask = 1 << 8;
			RaycastHit hit;
			if (Physics.Raycast(worldPoint, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, layerMask))
			{
				worldPoint.y = hit.point.y + 0.8f;
				transform.position = worldPoint;
				myLocationPoint = point;
				grid[point].SetColor(Color.blue);
				grid[point].SetPassable(false);
			}
		}
	}
}
