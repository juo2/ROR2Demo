using System;
using HG.Reflection;

namespace RoR2.GamepadVibration
{
	// Token: 0x02000B64 RID: 2916
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	public class GamepadVibrationControllerResolverAttribute : SearchableAttribute
	{
		// Token: 0x0600424D RID: 16973 RVA: 0x00112A26 File Offset: 0x00110C26
		public GamepadVibrationControllerResolverAttribute(Type vibrationControllerType)
		{
			this.vibrationControllerType = vibrationControllerType;
		}

		// Token: 0x04004057 RID: 16471
		public readonly Type vibrationControllerType;
	}
}
