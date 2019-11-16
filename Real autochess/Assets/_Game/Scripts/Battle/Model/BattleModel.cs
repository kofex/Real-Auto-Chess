using System;
using Scripts.Battle.Area.Model;
using Scripts.Battle.Camera.Model;
using Scripts.Battle.Components;
using Scripts.Battle.Units.Model;
using Scripts.Battle.Units.View;
using Scripts.Components;
using Scripts.Core;
using Scripts.Core.Interfaces;
using Scripts.Core.Model;
using Scripts.Core.Model.Base;
using Scripts.UI.Model;
using UnityEngine;

namespace Scripts.Battle.Model
{
	public class BattleModel : ModelBase, IUpdatable, IRestartable
	{
		public static Action BattleRestartBegin;
		
		public static bool IsGameOver => _isGameOver;
		private static bool _isGameOver;
		
		public SingletonModelsContainer BattleSingletons { get; } = new SingletonModelsContainer();
		
		private PathAreaModel _areaModel;
		private TeamsModel<ChessUnitModel> _teamsModel;
		private UnitView _unitPrefab;
		private IUpdatable[] _updatableModels;
		private bool _isSpawned;
		private float _timer;
		
		
		public new BattleModel InitModel()
		{
			var settings = GameCore.GetModel<SettingsModel>().Settings;
			_areaModel = BattleSingletons.TryAddSingletonModel(CreateModel<PathAreaModel>())
				.InitModel();
			
			_teamsModel = BattleSingletons.TryAddSingletonModel(CreateModel<TeamsModel<ChessUnitModel>>())
				.InitModel();

			BattleSingletons.TryAddSingletonModel(CreateModel<CameraModel>())
				.InitModel(settings.AreaSettings.Width * settings.AreaSettings.NodeHalfSize.x * 2,
					settings.AreaSettings.Height * settings.AreaSettings.NodeHalfSize.y * 2).SetView();

			_updatableModels = BattleSingletons.GetUpdatableModels();
			
			
			TeamsModelBase.UnitsSpawned += OnSpawnComplete;
			TeamBase.Lose += inx =>
			{
				if(_isGameOver)
					return;
				
				_isGameOver = true;
			};

			PopUpModel.NewButtonClick += Restart;

			_unitPrefab = settings.GetPefab<UnitView>();
			
			return this;
		}

		public void Update(float dt)
		{
			if (_isGameOver)
				return;
			
			if (!_isSpawned)
				return;
			
			foreach (var model in _updatableModels)
			{
				model.Update((dt));
			}
		}


		public void SpawnUnitOnField()
		{
			var count = _teamsModel.MaxUnits;
			for (var i = 0; i < count; i++)
				_areaModel.SpawnUnit(_teamsModel.GetNextUnit(out var teamInx), _unitPrefab, teamInx);
			
		}

		private void OnSpawnComplete() => _isSpawned = true;

		public void Restart()
		{
			if(!_isSpawned)
				return;
			
			BattleRestartBegin?.Invoke();

			SetDefault();

			foreach (var model in BattleSingletons.GetRestartableModels())
				model.Restart();
		}
		
		public void SetDefault()
		{
			UnitModel.RestId();
			
			_isSpawned = false;
			_isGameOver = false;
		}
	}
}