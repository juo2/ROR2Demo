using System;
using HG;

namespace RoR2.GamepadVibration
{
	// Token: 0x02000B62 RID: 2914
	public class DefaultGamepadVibrationController : GamepadVibrationController
	{
		// Token: 0x0600423C RID: 16956 RVA: 0x00112708 File Offset: 0x00110908
		protected override void CalculateMotorValues(in VibrationContext vibrationContext, float[] motorValues)
		{
			VibrationContext vibrationContext2 = vibrationContext;
			float num = vibrationContext2.CalcCamDisplacementMagnitude();
			ArrayUtils.SetRange<float>(motorValues, num, 0, motorValues.Length);
		}
	}
}
