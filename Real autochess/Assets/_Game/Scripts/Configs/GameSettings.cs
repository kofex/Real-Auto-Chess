using System.Linq;
using Scripts.Core.View;
using UnityEngine;

namespace Scripts.Configs
{
	[CreateAssetMenu(fileName = "GameSettings", menuName = "Game Tools/Create GameSettings")]
	public class GameSettings : ScriptableObject
	{
		[SerializeField] private AreaSettings _areaSettings;
		[SerializeField] private UnitSettings _unitSettings;
		[SerializeField] private TeamSettings _teamSettings;
		[SerializeField] private CacheBehaviour[] _prefabs;

		public AreaSettings AreaSettings => _areaSettings;
		public UnitSettings UnitSettings => _unitSettings;
		public TeamSettings TeamSettings => _teamSettings;


		public T GetPefab<T>() where T : CacheBehaviour
		{
			return (T) _prefabs.FirstOrDefault(pref => pref.GetType() == typeof(T));
		}

	}
}