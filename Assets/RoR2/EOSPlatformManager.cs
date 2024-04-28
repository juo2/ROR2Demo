using System;
using Epic.OnlineServices;
using Epic.OnlineServices.Logging;
using Epic.OnlineServices.Platform;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020009AA RID: 2474
	public class EOSPlatformManager : PlatformManager
	{
		// Token: 0x0600389C RID: 14492 RVA: 0x000ED7F4 File Offset: 0x000EB9F4
		public EOSPlatformManager()
		{
			this.libManager = new EOSLibraryManager();
			this.InitializePlatformInterface();
			this.SetupLogging();
			this.CreatePlatformInterface();
		}

		// Token: 0x0600389D RID: 14493 RVA: 0x000ED85B File Offset: 0x000EBA5B
		public override void InitializePlatformManager()
		{
			RoR2Application.onShutDown = (Action)Delegate.Combine(RoR2Application.onShutDown, new Action(this.Shutdown));
			RoR2Application.onUpdate += this.UpdatePlatformManager;
		}

		// Token: 0x0600389E RID: 14494 RVA: 0x000ED88F File Offset: 0x000EBA8F
		public static PlatformInterface GetPlatformInterface()
		{
			if (EOSPlatformManager._platformInterface == null)
			{
				throw new Exception("_platformInterface has not been set. Initialize EOSPlatformManager before attempting to access _platformInterface.");
			}
			return EOSPlatformManager._platformInterface;
		}

		// Token: 0x0600389F RID: 14495 RVA: 0x000ED8B0 File Offset: 0x000EBAB0
		private void InitializePlatformInterface()
		{
			Result result = PlatformInterface.Initialize(new InitializeOptions
			{
				ProductName = this._productName,
				ProductVersion = this._productVersion
			});
			if (result != Result.Success && result != Result.AlreadyConfigured)
			{
				throw new Exception("Failed to initialize platform: " + result);
			}
		}

		// Token: 0x060038A0 RID: 14496 RVA: 0x000ED8FE File Offset: 0x000EBAFE
		private void SetupLogging()
		{
			LoggingInterface.SetLogLevel(LogCategory.AllCategories, LogLevel.VeryVerbose);
			LoggingInterface.SetCallback(delegate(LogMessage logMessage)
			{
				Console.WriteLine(logMessage.Message);
			});
		}

		// Token: 0x060038A1 RID: 14497 RVA: 0x000ED938 File Offset: 0x000EBB38
		private void CreatePlatformInterface()
		{
			EOSPlatformManager._platformInterface = PlatformInterface.Create(new Options
			{
				ProductId = this._productId,
				SandboxId = this._sandboxId,
				DeploymentId = this._deploymentId,
				ClientCredentials = new ClientCredentials
				{
					ClientId = "xyza7891fuF5aodDdJ1ITnkYeRbyJbnT",
					ClientSecret = "QMwgx1LJIkRt25iA9YjXldcMD/8aAeQTke7a5FVU3no"
				}
			});
			if (EOSPlatformManager._platformInterface == null)
			{
				throw new Exception("Failed to create platform");
			}
		}

		// Token: 0x060038A2 RID: 14498 RVA: 0x000ED9B1 File Offset: 0x000EBBB1
		private void Shutdown()
		{
			if (EOSPlatformManager._platformInterface != null)
			{
				EOSPlatformManager._platformInterface.Release();
				EOSPlatformManager._platformInterface = null;
				PlatformInterface.Shutdown();
			}
			this.libManager.Shutdown();
		}

		// Token: 0x060038A3 RID: 14499 RVA: 0x000ED9E1 File Offset: 0x000EBBE1
		protected override void UpdatePlatformManager()
		{
			if (EOSPlatformManager._platformInterface != null)
			{
				EOSPlatformManager._platformInterface.Tick();
			}
		}

		// Token: 0x04003860 RID: 14432
		private static PlatformInterface _platformInterface;

		// Token: 0x04003861 RID: 14433
		private string _productName = Application.productName;

		// Token: 0x04003862 RID: 14434
		private string _productVersion = Application.version;

		// Token: 0x04003863 RID: 14435
		private string _productId = "3bf8fc77540f41b5bb253d9563eec679";

		// Token: 0x04003864 RID: 14436
		private string _sandboxId = "0dfcaeede0214b80b597f08fd7d64b1b";

		// Token: 0x04003865 RID: 14437
		private string _deploymentId = "f2ca672e660a4792a17b124291408566";

		// Token: 0x04003866 RID: 14438
		private string _clientId;

		// Token: 0x04003867 RID: 14439
		private string _clientSecret;

		// Token: 0x04003868 RID: 14440
		private EOSLibraryManager libManager;
	}
}
