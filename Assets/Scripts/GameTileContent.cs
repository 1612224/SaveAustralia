using UnityEngine;

public class GameTileContent : MonoBehaviour
{

	[SerializeField]
	GameTileContentType type = default;

	public GameTileContentType Type => type;

	[SerializeField]
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
		if (originFactory)
		{
			originFactory.Reclaim(this);
		} else
		{
			CustomDestroy.SafeDestroy(gameObject);
		}
	}

	public virtual void GameUpdate()
	{

	}
}