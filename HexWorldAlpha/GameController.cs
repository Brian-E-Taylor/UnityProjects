using UnityEngine;
using Gamelogic.Grids;
using System.Collections;

public class GameController : MonoBehaviour
{
	private PlayerCharacter player1;
	private EnemyCharacter enemy1;
	private HexWorld hexWorld1, hexWorld2, hexWorld3;
	private FlatHexGrid<Block> grid1, grid2, grid3;
	private IMap3D<FlatHexPoint> map1, map2, map3;
	// private Transform lookPosition;

	private bool playerTurnTaken;
	private bool enemyTurnTaken;
	private bool playerTurn;
	private int turnNum;

	public void Start()
	{
		FlatHexPoint startPoint;

		player1 = GameObject.FindGameObjectWithTag(Tags.player1).GetComponent<PlayerCharacter>();
		enemy1 = GameObject.FindGameObjectWithTag(Tags.enemy1).GetComponent<EnemyCharacter>();
		hexWorld1 = GameObject.FindGameObjectWithTag(Tags.hexWorld1).GetComponent<HexWorld>();
		grid1 = hexWorld1.GetGrid();
		map1 = hexWorld1.GetMap();

		// Position enemy
		startPoint = new FlatHexPoint(enemy1.startPositionX, enemy1.startPositionY);
		MoveCharacter(enemy1, startPoint, enemy1.startHexWorld);

		// Position player
		startPoint = new FlatHexPoint(player1.startPositionX, player1.startPositionY);
		MoveCharacter(player1, startPoint, player1.startHexWorld);

		grid1[startPoint].SetCurrentlySelected(true);
		var movementHexPoints = GetAvailableMoves(player1);
		HighlightMoves(movementHexPoints, player1);

		// lookPosition = GameObject.FindGameObjectWithTag(Tags.player1).gameObject.transform;

		playerTurnTaken = false;
		enemyTurnTaken = false;
		playerTurn = true;
		turnNum = 1;
	}

	public void Update()
	{
		// Check for Game Over

		// Check for end of player and enemy turns
		if (playerTurnTaken && enemyTurnTaken)
		{
			// Advance turn
			Debug.Log("Finished Turn #" + turnNum);
			turnNum++;
			playerTurnTaken = false;
			enemyTurnTaken = false;
		}

		// Check whether Player or Enemy turn
		if (playerTurn && !playerTurnTaken)
		{
			// Player Turn :

			// Check for mouse click
			if (Input.GetMouseButtonDown(0))
			{
				// Get the grid point clicked
				FlatHexPoint clickedPoint = ClickedGridPoint(Input.mousePosition);
				if (grid1.Contains(clickedPoint))
				{
					var movementHexPoints = GetAvailableMoves(player1);
					if (movementHexPoints.Contains(clickedPoint) &&
					    grid1[clickedPoint].IsPassable())
					{
						// Change block color back
						HighlightMoves(movementHexPoints, player1, false);
						grid1[player1.GetLocation()].SetCurrentlySelected(false);
						grid1[player1.GetLocation()].SetColor(grid1[player1.GetLocation()].GetOrigColor());

						MoveCharacter(player1, clickedPoint, hexWorld1);
						grid1[clickedPoint].SetCurrentlySelected(true);

						// Update list of possible moves
						movementHexPoints = GetAvailableMoves(player1);
						HighlightMoves(movementHexPoints, player1);

						// Player has taken their turn
						playerTurnTaken = true;
						playerTurn = false;					
					}
				}
			}
		}

		// Enemy Turn :
		if (!playerTurn && !enemyTurnTaken)
		{
			enemyTurnTaken = true;
			playerTurn = true;
		}
	}

	private FlatHexPoint ClickedGridPoint(Vector3 mousePosition)
	{
		Vector3 worldPosition;
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(mousePosition);
		if (Physics.Raycast(ray, out hit))
		{
			if (hit.transform.tag == "HexWorld1")
			{
				worldPosition = hit.transform.position;
				return map1[worldPosition];
			}
		}

		return new FlatHexPoint(-10, -10);
	}
	
	private PointList<FlatHexPoint> GetAvailableMoves(Character character)
	{
		if (character.movement < 1)
			return null;
		FlatHexPoint playerPosition = character.GetLocation();

		// Get the first 6 neighbors
		PointList<FlatHexPoint> pointlist_temp = grid1.GetNeighbors(playerPosition).ToPointList();
		PointList<FlatHexPoint> pointlist = pointlist_temp;
		
		for (int i = 1; i < character.movement; i++)
		{
			foreach (FlatHexPoint point in pointlist_temp.ToPointList())
			{
				foreach (FlatHexPoint innerpoint in grid1.GetNeighbors(point).ToPointList())
				{
					if (!grid1[innerpoint].GetCurrentlySelected())
						pointlist.Add(innerpoint);
				}
			}
			pointlist_temp = new PointList<FlatHexPoint>(pointlist);
		}
		
		return pointlist;
	}

	private void HighlightMoves(PointList<FlatHexPoint> pointlist, Character character, bool highlight = true)
	{
		foreach (FlatHexPoint point in pointlist)
		{
			if (grid1[point].IsPassable())
			{
				if (highlight)
					grid1[point].SetColor(Color.grey);
				else
					grid1[point].SetColor(grid1[point].GetOrigColor());
				grid1[point].SetWithinMoveRange(highlight);
			}
		}
	}

	private void MoveCharacter(Character character, FlatHexPoint destination, HexWorld hexWorld)
	{
		grid1[character.GetLocation()].SetPassable(true);

		// Position character
		character.SetLocation(destination, hexWorld);

		grid1[destination].SetPassable(false);
	}
}
