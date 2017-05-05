namespace DesktopToast
{
	/// <summary>
	/// Logger interface.
	/// </summary>
	public interface ILogger
	{
		/// <summary>
		/// Writes an object to the log.
		/// </summary>
		/// <param name="level">Log level.</param>
		/// <param name="obj">Object to write.</param>
		void Log(LogLevel level, object obj);

		/// <summary>
		/// Writes a string to the log.
		/// </summary>
		/// <param name="level">Log level.</param>
		/// <param name="str">String to write.</param>
		void Log(LogLevel level, string str);

		/// <summary>
		/// Writes a formatted string to the log.
		/// </summary>
		/// <param name="level">Log level.</param>
		/// <param name="format">Format string.</param>
		/// <param name="args">Arguments.</param>
		void Log(LogLevel level, string format, params object[] args);
	}
}
