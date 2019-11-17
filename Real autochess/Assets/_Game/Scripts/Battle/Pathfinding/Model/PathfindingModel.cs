using System.Collections.Generic;
using Scripts.Battle.Area.Model;
using Scripts.Battle.Nodes.Model;
using Scripts.Battle.Units.Model;
using Scripts.Core;
using Scripts.Core.Model.Base;

namespace Scripts.Battle.Pathfinding.Model
{
	public class PathfindingModel : ModelBase
	{
		private PathAreaModel _area;
		private List<PathNodeModel> _openNodesList = new List<PathNodeModel>();
		private List<PathNodeModel> _closeNodesList = new List<PathNodeModel>();
		private bool isFirst;


		public new PathfindingModel InitModel()
		{
			_area = GameCore.GetModel<PathAreaModel>();
			return this;
		}

		public void GetPathForTeam(ChessUnitModel[] teamSeekers, ChessUnitModel[] teamTargets)
		{
			_openNodesList.Clear();
			_closeNodesList.Clear();
			SetNodesToDefault();

			foreach (var seeker in teamSeekers)
			{
				foreach (var target in teamTargets)
				{
					GetPathForUnit(seeker, target);
				}
			}
		}

		private PathNodeModel[] GetPathForUnit(ChessUnitModel from, ChessUnitModel to)
		{
			_openNodesList.Add(from.CurrentNode);

			while (_openNodesList.Count > 0)
			{
				var node = PopAvailableOpenNode();

				if (to.CurrentNode == node)
				{
					return RetracePath(to.CurrentNode);
				}

				var neighbours = _area.GetNeighbours(node);
				foreach (var neighbour in neighbours)
				{
					if (!neighbour.CanReach || _closeNodesList.Contains(neighbour))
						continue;

					var gCost = neighbour.GCost + _area.GetPathCost(node, neighbour);
					if (neighbour.HCost == 0)
						neighbour.SetHCost(_area.GetPathCost(neighbour, to.CurrentNode));

					neighbour.GCostCheckToUpdate(gCost, node);
					neighbour.SetPrevNode(node);

					if (!_openNodesList.Contains(neighbour))
						_openNodesList.Add(neighbour);
				}
				
			}

			return null;
		}

		private PathNodeModel PopAvailableOpenNode()
		{
			var node = _openNodesList[0];
			var count = _openNodesList.Count;
			for (var i = 1; i < count; i++)
			{
				if (_openNodesList[i].FCost <= node.FCost && _openNodesList[i].HCost < node.HCost)
					node = _openNodesList[i];
			}

			_openNodesList.Remove(node);
			_closeNodesList.Add(node);
			
			return node;
		}


		private PathNodeModel[] RetracePath(PathNodeModel node)
		{
			var path = new List<PathNodeModel>();
			var currentNode = node;
			while (currentNode.PrevNode !=null )
			{
				path.Add(currentNode.PrevNode);
				currentNode = currentNode.PrevNode;
			}
			
			path.Reverse();
			path.RemoveAt(0);
			foreach (var model in path)
			{
				model.SetColor();
			}
			return path.ToArray();
		}


		private void SetNodesToDefault()
		{
			foreach (var node in _area.Nodes)
				node.SetDefault();
		}
		
	}
}