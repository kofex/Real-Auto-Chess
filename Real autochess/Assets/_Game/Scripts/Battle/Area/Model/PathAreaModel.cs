using Scripts.Battle.Nodes.Model;
using Scripts.Core;
using Scripts.Core.Model;
using UnityEngine;

namespace Scripts.Battle.Area.Model
{
	public class PathAreaModel : AreaModel<PathNodeModel>
	{
		private float _diagonalCost;
		private float _straitCost;
		
		public new PathAreaModel InitModel()
		{
			var settings = GameCore.GetModel<SettingsModel>().Settings.AreaSettings;
			_diagonalCost = settings.DiagonalCost;
			_straitCost = settings.StraitCost;
			
			base.InitModel();
			return this;
		}

		public float GetPathCost(PathNodeModel from, PathNodeModel to)
		{
			var vecFrom = GetInxPosFromNodeInx(from.Id);
			var vecTo = GetInxPosFromNodeInx(to.Id);

			var distX = Mathf.Abs(vecFrom.x - vecTo.x);
			var distY = Mathf.Abs(vecFrom.y - vecTo.y);

			if (distX > distY)
				return _diagonalCost * distY + _straitCost * (distX - distY);
			
			return _diagonalCost * distX + _straitCost*(distY - distX);

		}
	}
}