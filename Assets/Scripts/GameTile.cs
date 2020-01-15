using UnityEngine;

public class GameTile : MonoBehaviour
{
	[SerializeField]
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
}