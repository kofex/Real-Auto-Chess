using Scripts.Battle.Units.View;
using Scripts.Core;
using Scripts.Core.Model;
using UnityEngine;

namespace Scripts.Battle.Units.Model
{
	public class ChessUnitModel : UnitModel
	{
		public override UnitModel InitModel()
		{
			base.InitModel();
			Width = Height = GameCore.GetModel<SettingsModel>().Settings.UnitSettings.Radius * 2;
			return this;
		}
		
		public override UnitView SetView(Component prefab, Transform parent = null)
		{
			base.SetView(prefab, parent);
			View.transform.localScale = new Vector3(Width, Height, View.transform.localScale.z);
			return View;
		}
	}
}