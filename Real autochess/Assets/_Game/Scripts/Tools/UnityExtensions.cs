using UnityEngine;

namespace Scripts.Tools
{
	public static class UnityExtensions
	{
		public static float RandomValue(this Vector2 vectorMinMax) => Random.Range(vectorMinMax.x, vectorMinMax.y);
	}
}