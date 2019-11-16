using System.Collections.Generic;
using Scripts.Battle.Units.Model;
using UnityEngine;

namespace Scripts.Battle.Components
{
	public class Team <TUnit> : TeamBase where TUnit : UnitModel, new()
	{
		public List<TUnit> Units { get; } = new List<TUnit>();
		public Color TeamColor { get; }
		
		private int _nextUnitInx;
		public Team(Color color, int inx)
		{
			TeamColor = color;
			ID = inx;
		}
		
		public bool TryAddUnit(TUnit unit)
		{
			unit.SetColor(TeamColor);
			if (Units.Contains(unit))
				return false;

			Units.Add(unit);
			return true;
		}
		
		public void RemoveUnit(UnitModel unit) => RemoveUnit(unit as TUnit);

		private void RemoveUnit(TUnit unit)
		{
			if(!Units.Contains(unit))
				return;
			
			if(Units.Count == 0)
				return;
			
			Units.Remove(unit);
			if(Units.Count == 0)
				TeamLose();
		}

		public TUnit GetNextUnit() => _nextUnitInx < Units.Count ? Units[_nextUnitInx++] : null;

		public override void SetDefault()
		{
			_nextUnitInx = 0;
			
			foreach (var unit in Units)
				unit.Restart();
			
			Units.Clear();
			base.SetDefault();
		}

		public override void Restart()
		{
			SetDefault();
			base.Restart();
		}
	}
}