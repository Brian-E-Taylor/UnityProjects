//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

using UnityEngine;

public class Block : GLMonoBehaviour
{
	private bool isCurrentlySelected = false;
	private bool isWithinMoveRange = false;
	private bool isPassable = true;
	private Color origColor;

	void OnMouseEnter()
	{
		if (!isCurrentlySelected)
			renderer.material.color = Color.white;
	}

	void OnMouseExit()
	{
		if (!isCurrentlySelected)
		{
			if (isWithinMoveRange)
				renderer.material.color = Color.grey;
			else
				renderer.material.color = origColor;
		}
	}

	public bool GetCurrentlySelected()
	{
		return isCurrentlySelected;
	}
	public void SetCurrentlySelected(bool setCurrentlySelected)
	{
		isCurrentlySelected = setCurrentlySelected;
	}

	public bool GetWithinMoveRange()
	{
		return isWithinMoveRange;
	}
	public void SetWithinMoveRange(bool setMoveRange)
	{
		isWithinMoveRange = setMoveRange;
	}

	public bool IsPassable()
	{
		return isPassable;
	}
	public void SetPassable(bool passable)
	{
		isPassable = passable;
	}

	public Color GetOrigColor()
	{
		return origColor;
	}

	public void SetOrigColor(Color color)
	{
		origColor = color;
		renderer.material.color = color;
	}

	public void SetColor(Color color)
	{
		renderer.material.color = color;
	}

	public float GetHeight()
	{
		return -transform.localScale.z;
	}

	public void SetHeight(float height)
	{
		SetOrigColor(Utils.Blend(height, Utils.colors[0], Utils.colors[1]));
		transform.localScale = new Vector3(1, 1, -height);
	}
}
