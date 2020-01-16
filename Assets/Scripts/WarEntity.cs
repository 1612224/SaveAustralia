using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WarEntity : GameBehavior
{
	// Start is called before the first frame update
	WarFactory originFactory;

	public WarFactory OriginFactory
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



	public override bool GameUpdate()
	{
		return base.GameUpdate();
	}
}
