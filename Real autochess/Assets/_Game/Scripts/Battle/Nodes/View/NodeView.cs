using Scripts.Battle.Nodes.Model;
using Scripts.Core.View;
using UnityEngine;

namespace Scripts.Battle.Nodes.View
{
	public class NodeView : ViewBase<NodeModel>
	{
		private SpriteRenderer _spriteRenderer;
		public SpriteRenderer SpriteRenderer => _spriteRenderer != null
			? _spriteRenderer
			: (_spriteRenderer = GetComponent<SpriteRenderer>());
	}
}