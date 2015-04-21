using UnityEngine;
using System.Collections;

public class Utils : MonoBehaviour
{
	public static Color ColorFromInt(int r, int g, int b)
	{
		return new Color(r / 255.0f, g / 255.0f, b / 255.0f);
	}
	
	public static Color ColorFromInt(int r, int g, int b, int a)
	{
		return new Color(r / 255.0f, g / 255.0f, b / 255.0f, a / 255.0f);
	}
	
	public static Color[] colors =
	{	
		ColorFromInt(133, 219, 233),
		ColorFromInt(198, 224, 34),
		ColorFromInt(255, 215, 87),
		ColorFromInt(228, 120, 129),	
		
		ColorFromInt(42, 192, 217),
		ColorFromInt(114, 197, 29),
		ColorFromInt(247, 188, 0),
		ColorFromInt(215, 55, 82),
		
		
		ColorFromInt(205, 240, 246),
		ColorFromInt(229, 242, 154),
		ColorFromInt(255, 241, 153),
		ColorFromInt(240, 182, 187),
		
		ColorFromInt(235, 249, 252),
		ColorFromInt(241, 249, 204),
		ColorFromInt(255, 252, 193),
		ColorFromInt(247, 222, 217)
	};

	public static Color Blend(float t, Color color1, Color color2)
	{
		var r = color1.r * (1 - t) + color2.r * t;
		var g = color1.g * (1 - t) + color2.g * t;
		var b = color1.b * (1 - t) + color2.b * t;
		var a = color1.a * (1 - t) + color2.a * t;
		
		return new Color(r, g, b, a);
	}
}
