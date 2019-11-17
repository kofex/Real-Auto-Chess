using Scripts.Tools;
using UnityEngine;

namespace Scripts
{
	internal static class Debug
	{
		private static CustomLogger _logger;
		static Debug()
		{
			_logger = new CustomLogger();
		}

		public static void Log(string message) => _logger.Log(message);
		public static void Log(string message, Tools.LogType type) => _logger.Log(message, type);
		public static void Log(string message, Object context, Tools.LogType type) => _logger.Log(message, context, type);
	}
}