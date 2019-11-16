using Scripts.Core.View;
using UnityEngine;

namespace Scripts.Core.Interfaces
{
	public interface IModelWithVIew<out TView> where TView : CacheBehaviour
	{
		TView View { get; }
		TView SetView(Component prefab = null, Transform parent = null);

	}
}