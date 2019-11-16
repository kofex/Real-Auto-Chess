using Scripts.Core.Model.Base;
using Scripts.UI.Components;
using Scripts.UI.Model;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.View
{
	public class PopUpView : UIView<PopUpModel> 
	{
		[SerializeField] private ButtonWithText _newGameBtn;
		public ButtonWithText NewGameBtn => _newGameBtn;
		

		protected override void OnViewDestroy()
		{
			NewGameBtn.RemoveAllListeners();
		}
	}
}