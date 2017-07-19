using System;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

using Chimp.Logging;

namespace DesktopToast.Helper
{
	/// <summary>
	/// Toast helper class
	/// </summary>
	internal class Toast
	{
		private readonly ILogger logger;
		private readonly TaskCompletionSource<ToastResult> tcs;

		/// <summary>
		/// Initializes a new instance of the <see cref="Toast"/> class.
		/// </summary>
		/// <param name="loggerFactory">Logger factory</param>
		public Toast(ILoggerFactory loggerFactory)
		{
            logger = loggerFactory.CreateLogger<Toast>();
			tcs = new TaskCompletionSource<ToastResult>();
		}

		/// <summary>
		/// Shows a toast.
		/// </summary>
		/// <param name="document">Toast document</param>
		/// <param name="appId">AppUserModelID</param>
		/// <param name="maximumDuration">Optional maximum duration</param>
		/// <returns>Result of showing a toast</returns>
		public async Task<ToastResult> ShowAsync(XmlDocument document, string appId, TimeSpan maximumDuration)
		{
			var notifier = ToastNotificationManager.CreateToastNotifier(appId);
			if (notifier.Setting != NotificationSetting.Enabled)
				return GetResult(notifier);

			// Create a toast and prepare to handle toast events.
			var toast = new ToastNotification(document);
			if (maximumDuration != default(TimeSpan))
			{
				toast.ExpirationTime = DateTime.Now + maximumDuration;
			}

			toast.Activated += Toast_Activated;
			toast.Dismissed += Toast_Dismissed;
			toast.Failed += Toast_Failed;

			// Show a toast.
			notifier.Show(toast);

			// Wait for the result.
			var result = await tcs.Task;

			logger.Log(LogLevel.Debug, "Toast result: {0}", result);

			toast.Activated -= Toast_Activated;
			toast.Dismissed -= Toast_Dismissed;
			toast.Failed -= Toast_Failed;

			return result;
		}

		private void Toast_Activated(ToastNotification sender, object e)
		{
			tcs.SetResult(ToastResult.Activated);
		}

		private void Toast_Dismissed(ToastNotification sender, ToastDismissedEventArgs e)
		{
			switch (e.Reason)
			{
				case ToastDismissalReason.ApplicationHidden:
					tcs.SetResult(ToastResult.ApplicationHidden);
					break;
				case ToastDismissalReason.UserCanceled:
					tcs.SetResult(ToastResult.UserCanceled);
					break;
				case ToastDismissalReason.TimedOut:
					tcs.SetResult(ToastResult.TimedOut);
					break;
			}
		}

		private void Toast_Failed(ToastNotification sender, ToastFailedEventArgs e)
		{
			tcs.SetResult(ToastResult.Failed);
		}

		#region Helper

		private static ToastResult GetResult(ToastNotifier notifier)
		{
			switch (notifier.Setting)
			{
				default:
					return ToastResult.Invalid;
				case NotificationSetting.DisabledForApplication:
					return ToastResult.DisabledForApplication;
				case NotificationSetting.DisabledForUser:
					return ToastResult.DisabledForUser;
				case NotificationSetting.DisabledByGroupPolicy:
					return ToastResult.DisabledByGroupPolicy;
				case NotificationSetting.DisabledByManifest:
					return ToastResult.DisabledByManifest;
			}
		}

		#endregion
	}
}
