using Scripts.Tools;
using UnityEngine;

namespace Scripts.Battle.Nodes.Model
{
	public class PathNodeModel : NodeModel, IRestartable
	{
		public float GCost { get; private set; } = 0;
		public float HCost { get; private set; } = 0;
		public float FCost => GCost + HCost;
		
		public PathNodeModel PrevNode { get; private set; }
		
		private bool _canReach = true;
		public bool CanReach => _canReach;

		public void SetCosts(float gCost, float hCost)
		{
			GCost = gCost;
			HCost = hCost;
		}

		public void GCostCheckToUpdate(float gCost, PathNodeModel form)
		{
			if(gCost > GCost && GCost != 0)
				return;

			GCost = gCost;
			PrevNode = form;
		}

		public void SetHCost(float hCost) => HCost = hCost;

		public void SetPrevNode(PathNodeModel node)
		{
			PrevNode = node;
			View.gameObject.name = $"ID {Id} gCost {GCost} hCost {HCost} fCost {FCost}"; //TODO: delete it
		}
		
		public void SetDefault()
		{
			GCost = 0;
			HCost = 0;
			PrevNode = null;
		}

		public void Restart()
		{
			throw new System.NotImplementedException();
		}

		public void SetColor()
		{
			View.SpriteRenderer.color = Color.green;
		}
	}
}