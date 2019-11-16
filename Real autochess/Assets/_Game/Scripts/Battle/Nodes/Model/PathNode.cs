using Scripts.Tools;

namespace Scripts.Battle.Nodes.Model
{
	public class PathNodeModel : NodeModel
	{
		public int GCost { get; private set; } = 0;
		public int HCost { get; private set; } = 0;
		public int FCost => GCost + HCost;
		
		public PathNodeModel PrefNode { get; private set; }


		public void GCostCheckToUpdate(int gCost, PathNodeModel form)
		{
			if(gCost > GCost)
				return;

			GCost = gCost;
			PrefNode = form;
		}

		public void SetFCost(int fCost) => HCost = fCost;
	}
}