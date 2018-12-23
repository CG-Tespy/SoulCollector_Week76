using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class LayerMaskExtensions
{
	public static bool Contains(this LayerMask layerMask, int layer)
	{
		// From Unity Answers
		return layerMask == (layerMask | (1 << layer));
	}

	
}
