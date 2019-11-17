using System.Collections.Generic;
using Scripts.Battle.Nodes.Model;
using Scripts.Battle.Nodes.View;
using Scripts.Battle.Units.Model;
using Scripts.Configs;
using Scripts.Core;
using Scripts.Core.Model;
using Scripts.Core.Model.Base;
using Scripts.Tools;
using UnityEngine;
using LogType = Scripts.Tools.LogType;

namespace Scripts.Battle.Area.Model
{
	public class AreaModel<TNode> : ModelBase, IRestartable where TNode : NodeModel, new()
	{
		private const string units_root_name = "Units Root";
		private const string nodes_root_name = "Nodes Root";

		private TNode[] _nodes;
		public TNode[] Nodes => _nodes;


		private List<Vector2> _bannedIndexes = new List<Vector2>();
		private Transform _unitsRoot;
		private Transform _nodesRoot;
		private AreaSettings _areaSettings;
		private NodeView _prefab;
		private int _inxWidth;
		private int _inxHeight;
		private int _retries;
		private int _MaxRetries = 5;
		

		public new AreaModel<TNode> InitModel()
		{
			var settings = GameCore.GetModel<SettingsModel>().Settings;
			_areaSettings = settings.AreaSettings;
			_prefab = settings.GetPefab<NodeView>();
			
			_unitsRoot = new GameObject(units_root_name).transform;
			_nodesRoot = new GameObject(nodes_root_name).transform;

			_inxWidth = _areaSettings.Width;
			_inxHeight = _areaSettings.Height;

			_nodes = new TNode[_inxWidth * _inxHeight];
			
			ConstructArea();
			return this;
		}
		

		private void ConstructArea()
		{
			var colors = _areaSettings.TileColors;

			var inx = 0;
			 
			for (var h = 0; h < _inxHeight; h++)
			{
				for (var w = 0; w < _inxWidth; w++)
				{
					var pos = GetPosFromIndexes(w, h);
					var node = CreateModel<TNode>().InitModel(pos, colors[(h + w) % 2 == 0 ? 0 : 1])
						.SetView(_prefab, _nodesRoot)
						.Model;
					_nodes[inx++] = node as TNode;
				}
			}
		}


		private Vector2 GetPosFromIndexes(Vector2 indexVector) =>
			GetPosFromIndexes(Mathf.FloorToInt(indexVector.x), Mathf.FloorToInt(indexVector.y));
		
		private Vector2 GetPosFromIndexes(int xInx, int yInx)
		{
			return new Vector2(_areaSettings.NodeHalfSize.x * (1 + 2 * xInx),
				_areaSettings.NodeHalfSize.y * (1 + 2 * yInx));
		}

		private Vector2 GetIndexesFromPos(Vector2 pos) => new Vector2(
			Mathf.FloorToInt(pos.x / (_areaSettings.NodeHalfSize.x * 2f)),
			Mathf.FloorToInt(pos.y / (_areaSettings.NodeHalfSize.y * 2f)));
		
		protected Vector2 GetInxPosFromNodeInx(int nodeInx)
		{
			var y = Mathf.FloorToInt((float) nodeInx / _inxWidth);
			return new Vector2(nodeInx - y * _inxWidth, y);
		}

		protected int GetIdByAxisIndexes(Vector2 indexVector)
		{
			return Mathf.CeilToInt(indexVector.x + indexVector.y * _inxWidth);
		}

		private TNode GetNodeByPos(Vector2 pos)
		{
			Debug.Log($"Nodes.length {Nodes.Length} id {GetIdByAxisIndexes(GetIndexesFromPos(pos))}");
			return Nodes[GetIdByAxisIndexes(GetIndexesFromPos(pos))];
		} 

		public void SpawnUnit(UnitModel unit, Component prefab, int teamInx)
		{
			if (unit == null)
				return;
			
			_retries = 0;
			unit.SetStartPosition(GetRandomPosWithinBorder(teamInx));
			unit.SetCurrentNode(GetNodeByPos(unit.StartPosition));
			unit.SetView(prefab, _unitsRoot);
		}

		private Vector2 GetRandomPosWithinBorder(int teamInx)
		{
			if (_retries++ > _MaxRetries)
			{
				Debug.Log($"Can't get random free place!", LogType.Error);
				return Vector2.zero*-1;
			}
			
			// -1 cos' it's float Vec2
			var exclusiveWidth = _inxWidth - 1;
			
			var xIndex = Mathf.RoundToInt(teamInx == 1
				? new Vector2(exclusiveWidth - _areaSettings.SpawnInxRange.y, exclusiveWidth).RandomValue()
				: _areaSettings.SpawnInxRange.RandomValue());
			var yIndex = Mathf.RoundToInt(new Vector2(0f, _areaSettings.Height - 1).RandomValue());

			var spawnIndexes = new Vector2(xIndex, yIndex);
			
			if (_bannedIndexes.Contains(spawnIndexes))
				return GetRandomPosWithinBorder(teamInx);
			
			_bannedIndexes.Add(spawnIndexes);
			
			return GetPosFromIndexes(spawnIndexes);
		}

		public List<TNode> GetNeighbours(TNode node)
		{
			var neighbours = new List<TNode>();
			var targetIndexes =  GetInxPosFromNodeInx(node.Id);
			
			for (var h = -1; h <= 1; h++)
			{
				for (var w = -1; w <= 1; w++)
				{
					var inx = node.Id + w + h*_inxWidth;
					if (inx == node.Id|| inx >= Nodes.Length || inx < 0)
						continue;
					
					var inxPos = GetInxPosFromNodeInx(inx);
					if (Mathf.Abs(inxPos.x - targetIndexes.x) > 3 || Mathf.Abs(inxPos.y - targetIndexes.y) > 3)
						continue;
					
					if (inxPos.x < 0 || inxPos.x > _inxWidth || inxPos.y > _inxHeight || inxPos.y < 0)
						continue;
					
					neighbours.Add(Nodes[inx]);
				}
			}
			return neighbours;
		}

		public void SetDefault()
		{
			_nodes = null;
			_bannedIndexes.Clear();
		}

		public void Restart()
		{
			SetDefault();
		}
	}
}