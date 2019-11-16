using System;
using Scripts.Core;
using Scripts.Core.Model;
using Scripts.UI.View;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Scripts.UI.Model
{
	public class MainUIModel : UIModel<MainUIView>
	{
		private float _maxTeamBalanceWidth;
		private PopUpModel _popUp;
		
		public new MainUIModel InitModel()
		{
			return this;
		}
		
		public new MainUIView SetView(Transform parent = null)
		{
			var prefab = GameCore.GetModel<SettingsModel>().Settings.GetPefab<MainUIView>();
			ThisView = Object.Instantiate(prefab, parent);
			View.SetModel(this);
			_popUp = CreateModel<PopUpModel>().InitModel(this).SetView(View.transform).Model;
			
			return View;
		}

		public void PopUpRestart()
		{
			_popUp.Pop();
		}

	}
}