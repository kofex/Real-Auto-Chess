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
			 
			for (var i = 0; i < _inxWidth; i++)
			{
				for (var j = 0; j < _inxHeight; j++)
				{
					var pos = GetPosFromIndexes(i, j);
					var node = CreateModel<TNode>().InitModel(pos, colors[(i + j) % 2 == 0 ? 0 : 1])
						.SetView(_prefab, _nodesRoot)
						.Model;
					_nodes[inx++] = node as TNode;
				}
			}
		}

		private Vector2 GetPosFromIndexes(int xInx, int yInx)
		{
			return new Vector2(_areaSettings.NodeHalfSize.x * (1 + 2 * xInx),
				_areaSettings.NodeHalfSize.y * (1 + 2 * yInx));
		}

		public void SpawnUnit(UnitModel unit, Component prefab, int teamInx)
		{
			if (unit == null)
				return;
			
			unit.SetStartPosition(GetRandomPosWithinBorder(teamInx));
			unit.SetView(prefab, _unitsRoot);
		}
			
		
		private Vector2 GetRandomPosWithinBorder(int teamInx)
		{
			// -1 cos' it's float Vec2
			var exclusiveWidth = _inxWidth - 1;
			var xIndex = Mathf.RoundToInt(teamInx == 1
				? new Vector2(exclusiveWidth - _areaSettings.SpawnInxRange.y, exclusiveWidth).RandomValue()
				: _areaSettings.SpawnInxRange.RandomValue());
			
			var spawnIndexes = GetPosFromIndexes(xIndex,
				Mathf.RoundToInt(new Vector2(0f, _areaSettings.Height-1).RandomValue()));
			
			if (_bannedIndexes.Contains(spawnIndexes))
				return GetRandomPosWithinBorder(teamInx);
			
			_bannedIndexes.Add(spawnIndexes);

			 return spawnIndexes;
		}

		public void SetDefault()
		{
			_nodes = null;
		}

		public void Restart()
		{
			SetDefault();
		}
	}
}