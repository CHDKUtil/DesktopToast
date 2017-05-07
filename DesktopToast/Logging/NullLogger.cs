namespace DesktopToast.Logging
{
	/// <summary>
	/// No-op logger implementation.
	/// </summary>
	public sealed class NullLogger : ILogger
	{
		/// <summary>
		/// No-op logger instance.
		/// </summary>
		public static readonly NullLogger Instance = new NullLogger();

		private NullLogger()
		{
		}

		void ILogger.Log(LogLevel level, string str)
		{
			//Do nothing
		}

		void ILogger.Log(LogLevel level, object obj)
		{
			//Do nothing
		}

		void ILogger.Log(LogLevel level, string format, params object[] args)
		{
			//Do nothing
		}
	}
}
