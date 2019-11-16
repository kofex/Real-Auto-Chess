using System;

namespace Scripts.Battle.Components
{
	public class TeamBase : IRestartable
	{
		public static event Action<int> Lose;

		public int ID { get; protected set; }
		public bool HasLoose { get; protected set; }
		protected virtual void TeamLose()
		{
			HasLoose = true;
			Lose?.Invoke(ID);
		}

		public virtual void SetDefault()
		{
			HasLoose = false;
			ID = 0;
		}

		public virtual void Restart()
		{
			SetDefault();
		}
	}
}