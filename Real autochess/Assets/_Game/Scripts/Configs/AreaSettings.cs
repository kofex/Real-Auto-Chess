using System;
using UnityEngine;

namespace Scripts.Configs
{
	[Serializable]
	public class AreaSettings
	{
		[SerializeField] private int _width;
		[SerializeField] private int _height;
		[SerializeField] private int _straitCost = 10;
		[SerializeField] private int _diagonalCost = 10;
		[SerializeField] private Vector2 _nodeHalfSize;
		[SerializeField] private Vector2 _spawnInxRange;
		[SerializeField] private Color[] _tileColors;
		

		public int Width => _width;
		public int Height => _height;
		public int StraitCost => _straitCost;
		public int DiagonalCost => _diagonalCost;
		public Color[] TileColors => _tileColors;
		public Vector2 SpawnInxRange => _spawnInxRange;
		public Vector2 NodeHalfSize => _nodeHalfSize;
	}
}