using Scripts.Battle.Model;
using Scripts.Core.Model;
using Scripts.Core.Model.Base;
using UnityEngine;

namespace Scripts.Core
{
	public class GameCore : MonoBehaviour
	{		
		private static GameCore Instance { get; set; }
		private static MainModel _mainModel;
		
		private void Awake()
		{
			if (Instance)
				DestroyImmediate(this);
			
			Instance = this;
			_mainModel = new MainModel();
			_mainModel.InitModel();
			
			DontDestroyOnLoad(this);
		}

		private void Update()
		{
			_mainModel.Update(Time.deltaTime);
		}
		
		public static T GetModel<T>() where T : ModelBase
		{
			return _mainModel.SingletonModels.TryGetSingletonModel<T>() ??
			       GetModel<BattleModel>().BattleSingletons.TryGetSingletonModel<T>();
		}
		
	}
}