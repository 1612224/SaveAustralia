using UnityEngine;

public class GameTile : MonoBehaviour
{

	GameTile north, east, south, west;

	GameTileContent content;

	public GameTileContent Content
	{
		get => content;
		set
		{
			Debug.Assert(value != null, "Null assigned to content!");
			if (content != null)
			{
				content.Recycle();
			}
			content = value;
			content.transform.localPosition = transform.localPosition;
		}
	}

	public static void MakeEastWestNeighbors(GameTile east, GameTile west)
	{
		Debug.Assert(
			west.east == null && east.west == null, "Redefined neighbors!"
		);
		west.east = east;
		east.west = west;
	}

	public static void MakeNorthSouthNeighbors(GameTile north, GameTile south)
	{
		Debug.Assert(
			south.north == null && north.south == null, "Redefined neighbors!"
		);
		south.north = north;
		north.south = south;
	}
}