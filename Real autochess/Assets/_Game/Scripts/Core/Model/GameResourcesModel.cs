using Scripts.Configs;
using Scripts.Core.Model.Base;
using UnityEngine;


namespace Scripts.Core.Model
{
	public class SettingsModel : ModelBase
	{
		public GameSettings Settings{ get; private set; }

		public new SettingsModel InitModel()
		{
			LoadSettings();
			return this;
		}

		private void LoadSettings()
		{
			Settings = Resources.Load<GameSettings>(typeof(GameSettings).Name);
		}
		
		
	}
}