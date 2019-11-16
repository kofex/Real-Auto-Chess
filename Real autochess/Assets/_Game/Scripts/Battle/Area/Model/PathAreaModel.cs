using Scripts.Battle.Nodes.Model;

namespace Scripts.Battle.Area.Model
{
	public class PathAreaModel : AreaModel<PathNodeModel>
	{
		public new PathAreaModel InitModel()
		{
			base.InitModel();
			return this;
		}
	}
}