using Scripts.Battle.Units.Model;
using Scripts.Core.View;
using UnityEngine;

namespace Scripts.Battle.Units.View
{
	public class UnitView : ViewBase<UnitModel>
	{
		private SpriteRenderer _spriteRenderer;
		public SpriteRenderer SpriteRenderer => _spriteRenderer != null
			? _spriteRenderer
			: (_spriteRenderer = GetComponent<SpriteRenderer>());

	}
}