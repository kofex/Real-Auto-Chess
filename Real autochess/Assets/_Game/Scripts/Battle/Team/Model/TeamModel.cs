using System.Collections.Generic;
using System.Linq;
using Scripts.Battle.Components;
using Scripts.Battle.Units.Model;
using Scripts.Core;
using Scripts.Core.Model;
using Scripts.Tools;
using Scripts.UI.Model;
using LogType = Scripts.Tools.LogType;

namespace Scripts.Battle.Team.Model
{
	public class TeamsModel<TUnit> : TeamsModelBase where TUnit : UnitModel, new()
	{
		protected List<Team<TUnit>> Teams { get; } = new List<Team<TUnit>>();
		public int MaxUnits { get; private set; }
		
		private Team<TUnit> _theVictoriousTeam;
		private SettingsModel _gameSettings;
		private int _nextTeamInx;
		private int _unitsGot;
		private bool _canMove;

		protected new TeamsModel<TUnit> InitModel()
		{
			_gameSettings = GameCore.GetModel<SettingsModel>();

			CreateTeams();
			
			TeamBase.Lose += OnTeamLose;
			UnitModel.UnitDeath += RemoveUnit;
			return this;
		}

		private void CreateTeams()
		{
			var colors = _gameSettings.Settings.TeamSettings.TeamColors;
			var inx = 0;
			Teams.Add(new Team<TUnit>(colors[inx], inx++));
			Teams.Add(new Team<TUnit>(colors[inx], inx));

			var count = Teams.Count;
			for (inx = 0; inx < count; inx++)
				PrepareUnits(Teams[inx]);
		}
		
		private void PrepareUnits(Team<TUnit> team)
		{
			var unitCont = _gameSettings.Settings.TeamSettings.UnitsRange.RandomValue();
			
			for (var i = 0; i < unitCont; i++)
				team.TryAddUnit(CreateModel<TUnit>().InitModel() as TUnit);

			MaxUnits += team.Units.Count;
		}
		

		public UnitModel GetNextUnit(out int teamInx)
		{
			teamInx = GetTeamIndex(_nextTeamInx++);
			if (_unitsGot++ >= MaxUnits)
				OnSpawnCompleted();
			
			return Teams[teamInx].GetNextUnit();;
		}

		private int GetTeamIndex(int totalUnitInx) => totalUnitInx >= Teams[0].Units.Count ? 1 : 0;

		public override void Update(float dt)
		{
			if(!_canMove)
				return;
			
			foreach (var team in Teams)
				team.Units.ForEach(un => un.Update(dt));
			
			base.Update(dt);
		}

		private void RemoveUnit(UnitModel unit)
		{
			var team = Teams.Find(entry => entry.Units.Contains(unit));
			team?.RemoveUnit(unit);
		}

		protected override void OnSpawnCompleted()
		{
			base.OnSpawnCompleted();
			_canMove = true;
		}

		private void OnTeamLose(int id)
		{
			_theVictoriousTeam = Teams.FirstOrDefault(team => !team.HasLoose);
			if (_theVictoriousTeam == default)
			{
				Debug.Log($"There is no victorious team!", LogType.Warning);	
				return;
			}
			
			Debug.Log($"Victory to {_theVictoriousTeam?.ID}", LogType.Info);
			GameCore.GetModel<MainUIModel>().PopUpRestart();
		}

		public override void SetDefault()
		{
			foreach (var team in Teams)
				team.SetDefault();
			
			Teams.Clear();

			_canMove = false;
			_nextTeamInx = 0;
			_unitsGot = 0;
			MaxUnits = 0;
		}

		public override void Restart()
		{
			SetDefault();
			CreateTeams();
		}
		
	}
}