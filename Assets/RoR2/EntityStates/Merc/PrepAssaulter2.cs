using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.Merc
{
	// Token: 0x02000275 RID: 629
	public class PrepAssaulter2 : BaseState
	{
		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000B0C RID: 2828 RVA: 0x0002D376 File Offset: 0x0002B576
		// (set) Token: 0x06000B0D RID: 2829 RVA: 0x0002D37E File Offset: 0x0002B57E
		public int dashIndex { private get; set; }

		// Token: 0x06000B0E RID: 2830 RVA: 0x0002D388 File Offset: 0x0002B588
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = PrepAssaulter2.baseDuration / this.attackSpeedStat;
			base.PlayAnimation("FullBody, Override", "AssaulterPrep", "AssaulterPrep.playbackRate", PrepAssaulter2.baseDuration);
			base.FindModelChild("PreDashEffect").gameObject.SetActive(true);
			Util.PlaySound(PrepAssaulter2.enterSoundString, base.gameObject);
			Ray aimRay = base.GetAimRay();
			base.characterDirection.forward = aimRay.direction;
			base.characterDirection.moveVector = aimRay.direction;
			base.SmallHop(base.characterMotor, PrepAssaulter2.smallHopVelocity);
			if (NetworkServer.active)
			{
				base.characterBody.AddBuff(RoR2Content.Buffs.HiddenInvincibility);
			}
		}

		// Token: 0x06000B0F RID: 2831 RVA: 0x0002D444 File Offset: 0x0002B644
		public override void OnExit()
		{
			if (NetworkServer.active)
			{
				base.characterBody.RemoveBuff(RoR2Content.Buffs.HiddenInvincibility);
				base.characterBody.AddTimedBuff(RoR2Content.Buffs.HiddenInvincibility, 0.2f);
			}
			base.FindModelChild("PreDashEffect").gameObject.SetActive(false);
			base.OnExit();
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x0002D499 File Offset: 0x0002B699
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge > this.duration)
			{
				this.outer.SetNextState(new Assaulter2());
			}
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x0002D4BF File Offset: 0x0002B6BF
		public override void OnSerialize(NetworkWriter writer)
		{
			base.OnSerialize(writer);
			writer.Write((byte)this.dashIndex);
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x0002D4D5 File Offset: 0x0002B6D5
		public override void OnDeserialize(NetworkReader reader)
		{
			base.OnDeserialize(reader);
			this.dashIndex = (int)reader.ReadByte();
		}

		// Token: 0x04000CAD RID: 3245
		public static float baseDuration;

		// Token: 0x04000CAE RID: 3246
		public static float smallHopVelocity;

		// Token: 0x04000CAF RID: 3247
		public static string enterSoundString;

		// Token: 0x04000CB1 RID: 3249
		private float duration;
	}
}
