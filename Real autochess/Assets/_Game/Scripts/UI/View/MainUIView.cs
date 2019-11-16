using Scripts.UI.Components;
using Scripts.UI.Model;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.View
{
	public class MainUIView : UIView<MainUIModel>
	{
		[SerializeField] private PopUpView _popUpView;
		public PopUpView PopUp => _popUpView;
		 
	}
}