namespace DesktopToast
{
	/// <summary>
	/// Log level.
	/// </summary>
	public enum LogLevel
	{
		/// <summary>
		/// Trace.
		/// </summary>
		Trace = 0,

		/// <summary>
		/// Debug.
		/// </summary>
		Debug,

		/// <summary>
		/// Information.
		/// </summary>
		Information,

		/// <summary>
		/// Warning.
		/// </summary>
		Warning,

		/// <summary>
		/// Error.
		/// </summary>
		Error,

		/// <summary>
		/// Critical.
		/// </summary>
		Critical,

		/// <summary>
		/// None.
		/// </summary>
		None = int.MaxValue
	}
}
