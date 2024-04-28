using System;
using Rewired;

namespace RoR2.GamepadVibration
{
	// Token: 0x02000B66 RID: 2918
	public class Xbox360GamepadVibrationController : GamepadVibrationController
	{
		// Token: 0x0600424F RID: 16975 RVA: 0x00112A6C File Offset: 0x00110C6C
		protected override void CalculateMotorValues(in VibrationContext vibrationContext, float[] motorValues)
		{
			VibrationContext vibrationContext2 = vibrationContext;
			float num = vibrationContext2.CalcCamDisplacementMagnitude();
			float num2 = num / Xbox360GamepadVibrationController.deepRumbleFactor;
			float num3 = num;
			motorValues[0] = num2;
			motorValues[1] = num3;
		}

		// Token: 0x06004250 RID: 16976 RVA: 0x00112A98 File Offset: 0x00110C98
		[GamepadVibrationControllerResolver(typeof(Xbox360GamepadVibrationController))]
		private static bool Resolve(Joystick joystick)
		{
			return joystick.vibrationMotorCount >= 2 && (joystick.name.Contains("Xbox") || joystick.name.Contains("XInput Gamepad "));
		}

		// Token: 0x0400405B RID: 16475
		protected static readonly float deepRumbleFactor = 5f;
	}
}
