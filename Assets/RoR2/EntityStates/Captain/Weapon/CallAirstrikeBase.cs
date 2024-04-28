using System;
using RoR2;
using RoR2.Projectile;
using UnityEngine;

namespace EntityStates.Captain.Weapon
{
	// Token: 0x02000424 RID: 1060
	public class CallAirstrikeBase : AimThrowableBase
	{
		// Token: 0x0600130C RID: 4876 RVA: 0x00054D8E File Offset: 0x00052F8E
		public override void OnEnter()
		{
			base.OnEnter();
			base.characterBody.SetSpreadBloom(this.bloom, true);
		}

		// Token: 0x0600130D RID: 4877 RVA: 0x00054DA8 File Offset: 0x00052FA8
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			base.characterBody.SetAimTimer(4f);
		}

		// Token: 0x0600130E RID: 4878 RVA: 0x00054DC0 File Offset: 0x00052FC0
		public override void OnExit()
		{
			Util.PlaySound(CallAirstrikeBase.fireAirstrikeSoundString, base.gameObject);
			base.OnExit();
		}

		// Token: 0x0600130F RID: 4879 RVA: 0x00054DDC File Offset: 0x00052FDC
		protected override void ModifyProjectile(ref FireProjectileInfo fireProjectileInfo)
		{
			base.ModifyProjectile(ref fireProjectileInfo);
			fireProjectileInfo.position = this.currentTrajectoryInfo.hitPoint;
			fireProjectileInfo.rotation = Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f);
			fireProjectileInfo.speedOverride = 0f;
		}

		// Token: 0x06001310 RID: 4880 RVA: 0x00054E30 File Offset: 0x00053030
		protected override bool KeyIsDown()
		{
			return base.inputBank.skill1.down;
		}

		// Token: 0x06001311 RID: 4881 RVA: 0x00054E42 File Offset: 0x00053042
		protected override EntityState PickNextState()
		{
			return new Idle();
		}

		// Token: 0x06001312 RID: 4882 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x0400186C RID: 6252
		[SerializeField]
		public float airstrikeRadius;

		// Token: 0x0400186D RID: 6253
		[SerializeField]
		public float bloom;

		// Token: 0x0400186E RID: 6254
		public static GameObject muzzleFlashEffect;

		// Token: 0x0400186F RID: 6255
		public static string muzzleString;

		// Token: 0x04001870 RID: 6256
		public static string fireAirstrikeSoundString;
	}
}
