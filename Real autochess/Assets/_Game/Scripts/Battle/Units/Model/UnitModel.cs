using System;
using Scripts.Battle.Nodes.Model;
using Scripts.Battle.Units.View;
using Scripts.Core.Interfaces;
using UnityEngine;
using LogType = Scripts.Tools.LogType;
using Object = UnityEngine.Object;

namespace Scripts.Battle.Units.Model
{
	public class UnitModel : UnitModelBase, IModelWithVIew<UnitView>, IUpdatable, IRestartable
	{
		public static event Action<UnitModel> UnitDeath;
		protected static int ID;

		public UnitView View { get; private set; }
		
		protected bool IsDead { get; set; }
		
		private Color Color { get; set; }
		private int UnitId { get; set; }
		
		private NodeModel _currentNode;
		public NodeModel CurrentNode => _currentNode;
		

		public virtual UnitModel InitModel()
		{
			UnitId = ID++;
			return this;
		}
		
		public virtual UnitView SetView(Component prefab, Transform parent = null)
		{
			View = Object.Instantiate(prefab, parent).GetComponent<UnitView>();
			View.transform.position = StartPosition;
			View.SpriteRenderer.color = Color;
			return View;
		}

		public static void RestId()=> ID = 0;
		
		public void SetColor(Color color) => Color = color;

		public virtual void Update(float dt)
		{
			View.transform.Translate(CurrentSpeed * dt);
		}
		
		protected virtual void OnDeath()
		{
			IsDead = true;
			UnitDeath?.Invoke(this);
			Object.Destroy(View.gameObject);
		}

		protected virtual bool CheckIsDead()
		{
			Debug.Log($"Check is dead base call [not implemented]", LogType.MissCall);
			return false;
		}
		
		public void SetCurrentNode(NodeModel currentNode)
		{
			_currentNode = currentNode;
		}

		public void SetDefault()
		{
			_currentNode = null;
			ID = 0;
			UnitId = 0;
		}

		public virtual void Restart()
		{
			SetDefault();
			Object.Destroy(View.gameObject);	
		}
	}
}