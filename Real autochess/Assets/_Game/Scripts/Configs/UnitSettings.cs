using System;
using UnityEngine;

namespace Scripts.Configs
{
	[Serializable]
	public class UnitSettings
	{
		[SerializeField] private float _health = 100f;
		[SerializeField] private float _radius = 0.5f;
		[SerializeField] private float _damage = 10f;
		[SerializeField] private float _speed = 1f;

		public float Health => _health;
		public float Radius => _radius;
		public float Damage => _damage;
		public float Speed => _speed;
	}
}