using System.Collections.Generic;
using Scripts.Battle.Units.Model;

namespace Scripts.Battle.Team.Model
{
	public class ChessTeamModel : TeamsModel<ChessUnitModel>
	{
		public new ChessTeamModel InitModel()
		{
			return base.InitModel() as ChessTeamModel;
		}

		public ChessUnitModel[] GetTeamUnits(int teamInx)
		{
			return Teams[teamInx].Units.ToArray();
		}
	}
}