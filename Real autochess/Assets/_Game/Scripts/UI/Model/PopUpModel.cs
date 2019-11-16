using System;
using Scripts.Battle.Model;
using Scripts.Core;
using Scripts.UI.View;
using UnityEngine;

namespace Scripts.UI.Model
{
	public class PopUpModel : UIModel<PopUpView>
	{
		public static Action NewButtonClick;
		private const string NEW_BTN = "NEW BATTLE";
		

		private MainUIModel _mainUIModel;

		public new PopUpView SetView(Transform parent = null)
		{
			ThisView = _mainUIModel.View.PopUp;
			View.NewGameBtn.AddListener(()=>NewButtonClick?.Invoke());
			View.SetModel(this);
			View.NewGameBtn.SetTest(NEW_BTN);
			UnPop();
			
			return View;
		}

		public PopUpModel InitModel(MainUIModel mainUIModel)
		{
			NewButtonClick += () =>
			{
				UnPop();
				GameCore.GetModel<BattleModel>().Restart();
			};
			_mainUIModel = mainUIModel;
			
			return this;
		}

		public void Pop()
		{
			View.Canvas.enabled = true;
		}

		public void UnPop() => SetActive(false);
		private void SetActive(bool isActive) => View.Canvas.enabled = isActive;
		

	}
}