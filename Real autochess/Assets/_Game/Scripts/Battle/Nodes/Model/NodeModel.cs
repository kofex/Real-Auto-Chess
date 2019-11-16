using Scripts.Battle.Nodes.View;
using Scripts.Core;
using Scripts.Core.Interfaces;
using Scripts.Core.Model;
using Scripts.Core.Model.Base;
using UnityEngine;

namespace Scripts.Battle.Nodes.Model
{
	public class NodeModel : ModelBase, IModelWithVIew<NodeView>
	{
		public NodeView View { get; private set; }
		public Vector2 Position { get; private set; }
		public Color Color { get; private set; }
		public int Id { get; private set; }


		private new NodeModel InitModel()
		{
			return this;
		}

		public NodeModel InitModel(Vector2 position, Color color)
		{
			Position = position;
			Color = color;
			return this;
		}

		public NodeView SetView(Component prefab, Transform parent = null)
		{
			View = Object.Instantiate(prefab, Position, Quaternion.identity, parent).GetComponent<NodeView>();
			View.SpriteRenderer.color = Color;
			return View;
		}
	}
}