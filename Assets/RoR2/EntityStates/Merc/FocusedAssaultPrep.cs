using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.Merc
{
	// Token: 0x0200027A RID: 634
	public class FocusedAssaultPrep : BaseState
	{
		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000B35 RID: 2869 RVA: 0x0002E86D File Offset: 0x0002CA6D
		// (set) Token: 0x06000B36 RID: 2870 RVA: 0x0002E875 File Offset: 0x0002CA75
		public int dashIndex { private get; set; }

		// Token: 0x06000B37 RID: 2871 RVA: 0x0002E880 File Offset: 0x0002CA80
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = this.baseDuration / this.attackSpeedStat;
			base.PlayAnimation("FullBody, Override", "AssaulterPrep", "AssaulterPrep.playbackRate", this.baseDuration);
			GameObject gameObject = base.FindModelChildGameObject("PreDashEffect");
			if (gameObject != null)
			{
				gameObject.SetActive(true);
			}
			Util.PlaySound(this.enterSoundString, base.gameObject);
			Ray aimRay = base.GetAimRay();
			base.characterDirection.forward = aimRay.direction;
			base.characterDirection.moveVector = aimRay.direction;
			base.SmallHop(base.characterMotor, this.smallHopVelocity);
			if (NetworkServer.active)
			{
				base.characterBody.AddBuff(RoR2Content.Buffs.HiddenInvincibility);
			}
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x0002E940 File Offset: 0x0002CB40
		public override void OnExit()
		{
			if (NetworkServer.active)
			{
				base.characterBody.RemoveBuff(RoR2Content.Buffs.HiddenInvincibility);
				base.characterBody.AddTimedBuff(RoR2Content.Buffs.HiddenInvincibility, 0.2f);
			}
			GameObject gameObject = base.FindModelChildGameObject("PreDashEffect");
			if (gameObject != null)
			{
				gameObject.SetActive(false);
			}
			base.OnExit();
		}

		// Token: 0x06000B39 RID: 2873 RVA: 0x0002E996 File Offset: 0x0002CB96
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge > this.duration)
			{
				this.outer.SetNextState(new FocusedAssaultDash());
			}
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x0002E9BC File Offset: 0x0002CBBC
		public override void OnSerialize(NetworkWriter writer)
		{
			base.OnSerialize(writer);
			writer.Write((byte)this.dashIndex);
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x0002E9D2 File Offset: 0x0002CBD2
		public override void OnDeserialize(NetworkReader reader)
		{
			base.OnDeserialize(reader);
			this.dashIndex = (int)reader.ReadByte();
		}

		// Token: 0x04000CF8 RID: 3320
		[SerializeField]
		public float baseDuration;

		// Token: 0x04000CF9 RID: 3321
		[SerializeField]
		public float smallHopVelocity;

		// Token: 0x04000CFA RID: 3322
		[SerializeField]
		public string enterSoundString;

		// Token: 0x04000CFC RID: 3324
		private float duration;
	}
}
