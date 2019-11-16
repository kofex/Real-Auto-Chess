using Scripts.Battle.Model;
using Scripts.Components;
using Scripts.Core.Interfaces;
using Scripts.Core.Model.Base;
using Scripts.UI.Model;

namespace Scripts.Core.Model
{
	public class MainModel : ModelBase, IUpdatable
	{
		public SingletonModelsContainer SingletonModels { get; } = new SingletonModelsContainer();
		
		private IUpdatable[] _updatableModels;
		

		public new MainModel InitModel()
		{
			SingletonModels.TryAddSingletonModel(CreateModel<SettingsModel>()).InitModel();
			var battleModel = SingletonModels.TryAddSingletonModel(CreateModel<BattleModel>()).InitModel();
			SingletonModels.TryAddSingletonModel(CreateModel<MainUIModel>()).InitModel().SetView();

			_updatableModels = SingletonModels.GetUpdatableModels();
			
			battleModel.SpawnUnitOnField();
				
			return this;
		}

		public void Update(float dt)
		{
			foreach (var model in _updatableModels)
				model.Update(dt);
		}
		
	}
}