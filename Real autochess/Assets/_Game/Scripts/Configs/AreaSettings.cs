using System;
using UnityEngine;

namespace Scripts.Configs
{
	[Serializable]
	public class AreaSettings
	{
		[SerializeField] private int _width;
		[SerializeField] private int _height;
		[SerializeField] private float _straitCost = 10f;
		[SerializeField] private float _diagonalCost = 10f;
		[SerializeField] private Vector2 _nodeHalfSize;
		[SerializeField] private Vector2 _spawnInxRange;
		[SerializeField] private Color[] _tileColors;
		

		public int Width => _width;
		public int Height => _height;
		public float StraitCost => _straitCost;
		public float DiagonalCost => _diagonalCost;
		public Color[] TileColors => _tileColors;
		public Vector2 SpawnInxRange => _spawnInxRange;
		public Vector2 NodeHalfSize => _nodeHalfSize;
	}
}