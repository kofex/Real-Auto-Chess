using System;
using UnityEngine;

namespace Scripts.Configs
{
	[Serializable]
	public class TeamSettings
	{
		[SerializeField] private Color[] _teamColors;
		[SerializeField] private Vector2 _unitsRange;
		
		
		public Color[] TeamColors => _teamColors;
		public Vector2 UnitsRange => _unitsRange;

	}
}