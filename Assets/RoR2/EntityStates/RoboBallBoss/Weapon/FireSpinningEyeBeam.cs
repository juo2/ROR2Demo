using System;
using EntityStates.RoboBallMini.Weapon;
using UnityEngine;

namespace EntityStates.RoboBallBoss.Weapon
{
	// Token: 0x020001EB RID: 491
	public class FireSpinningEyeBeam : FireEyeBeam
	{
		// Token: 0x060008BD RID: 2237 RVA: 0x00025004 File Offset: 0x00023204
		public override void OnEnter()
		{
			string customName = this.outer.customName;
			this.eyeBeamOriginTransform = base.FindModelChild(customName);
			this.muzzleString = customName;
			base.OnEnter();
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x00025038 File Offset: 0x00023238
		public override Ray GetLaserRay()
		{
			Ray result = default(Ray);
			if (this.eyeBeamOriginTransform)
			{
				result.origin = this.eyeBeamOriginTransform.position;
				result.direction = this.eyeBeamOriginTransform.forward;
			}
			return result;
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override bool ShouldFireLaser()
		{
			return true;
		}

		// Token: 0x04000A50 RID: 2640
		private Transform eyeBeamOriginTransform;
	}
}
