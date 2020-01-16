using UnityEngine;

[SelectionBase]
public class GameTileContent : MonoBehaviour
{

	[SerializeField]
	GameTileContentType type = default;

	public Canvas canvas;

	public GameTileContentType Type => type;

	GameTileContentFactory originFactory;

	public GameTileContentFactory OriginFactory
	{
		get => originFactory;
		set
		{
			Debug.Assert(originFactory == null, "Redefined origin factory!");
			originFactory = value;
		}
	}

	public void Recycle()
	{
		originFactory.Reclaim(this);
	}

	public virtual void OnTouch()
	{

	}

	public virtual void GameUpdate()
	{
		
	}
}