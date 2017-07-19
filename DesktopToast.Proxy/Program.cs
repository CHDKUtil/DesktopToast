﻿using Chimp.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DesktopToast.Proxy
{
	class Program
	{
		static void Main(string[] args)
		{
			var requestString = args.Any() ? args[0] : null;
			if (requestString == null)
			{
#if DEBUG
				requestString = @"{
""ShortcutFileName"":""DesktopToast.Proxy.lnk"",
""ShortcutTargetFilePath"":""C:\\DesktopToast.Proxy.exe"",
""ToastTitle"":""DesktopToast Proxy Sample"",
""ToastBody"":""This is a toast test."",
""AppId"":""DesktopToast.Proxy"",
}";
#endif
#if !DEBUG
				return;
#endif
			}

			new ToastManager(NoOpLoggerFactory.Instance).ShowAsync(requestString)
                .ContinueWith(result => Console.WriteLine(result.Result))
				.Wait();
		}
	}
}