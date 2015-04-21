using UnityEngine;
using System.Collections;
using Gamelogic.Grids;

public class MovementPerimeter : MonoBehaviour
{
	public void GenerateMesh(PointList<FlatHexPoint> pointlist)
	{
		gameObject.AddComponent("MeshFilter");
		gameObject.AddComponent("MeshRenderer");

		// Mesh mesh = GetComponent<MeshFilter>().mesh;

		// Vector3[] vertices = new Vector3[12 * pointlist.Count];
	}
}
