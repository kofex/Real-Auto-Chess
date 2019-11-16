using Scripts.Core.Interfaces;

namespace Scripts.Core.Model.Base
{
	public abstract class ModelBase : IModel, IInitiable<ModelBase>
	{
		protected static T CreateModel<T>() where T : IModel, new()
		{
			return new T();
		}

		public virtual ModelBase InitModel()
		{
			return this;
		}
	}
}