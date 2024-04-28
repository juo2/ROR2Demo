using System;
using System.Runtime.InteropServices;

namespace RoR2
{
	// Token: 0x0200099E RID: 2462
	public class EOSLibraryManager
	{
		// Token: 0x060037E5 RID: 14309
		[DllImport("Kernel32.dll")]
		private static extern IntPtr LoadLibrary(string lpLibFileName);

		// Token: 0x060037E6 RID: 14310
		[DllImport("Kernel32.dll")]
		private static extern int FreeLibrary(IntPtr hLibModule);

		// Token: 0x060037E7 RID: 14311
		[DllImport("Kernel32.dll")]
		private static extern IntPtr GetProcAddress(IntPtr hModule, string lpProcName);

		// Token: 0x060037E8 RID: 14312 RVA: 0x000EAE23 File Offset: 0x000E9023
		public EOSLibraryManager()
		{
			this.Initialize();
		}

		// Token: 0x060037E9 RID: 14313 RVA: 0x000EAE31 File Offset: 0x000E9031
		private void Initialize()
		{
			if (this._initialized)
			{
				return;
			}
			this._initialized = true;
		}

		// Token: 0x060037EA RID: 14314 RVA: 0x000EAE43 File Offset: 0x000E9043
		public void Shutdown()
		{
			this._initialized = false;
		}

		// Token: 0x04003807 RID: 14343
		private IntPtr _libraryPointer;

		// Token: 0x04003808 RID: 14344
		private bool _initialized;
	}
}
