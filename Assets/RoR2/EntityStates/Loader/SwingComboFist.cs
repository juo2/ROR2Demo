using System;
using RoR2.Skills;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.Loader
{
	// Token: 0x020002CC RID: 716
	public class SwingComboFist : LoaderMeleeAttack, SteppedSkillDef.IStepSetter
	{
		// Token: 0x06000CB4 RID: 3252 RVA: 0x0003584E File Offset: 0x00033A4E
		void SteppedSkillDef.IStepSetter.SetStep(int i)
		{
			this.gauntlet = i;
		}

		// Token: 0x06000CB5 RID: 3253 RVA: 0x00035858 File Offset: 0x00033A58
		protected override void PlayAnimation()
		{
			string animationStateName = (this.gauntlet == 0) ? "SwingFistRight" : "SwingFistLeft";
			float duration = Mathf.Max(this.duration, 0.2f);
			base.PlayCrossfade("Gesture, Additive", animationStateName, "SwingFist.playbackRate", duration, 0.1f);
			base.PlayCrossfade("Gesture, Override", animationStateName, "SwingFist.playbackRate", duration, 0.1f);
		}

		// Token: 0x06000CB6 RID: 3254 RVA: 0x000358B9 File Offset: 0x00033AB9
		protected override void BeginMeleeAttackEffect()
		{
			this.swingEffectMuzzleString = ((this.gauntlet == 0) ? "SwingRight" : "SwingLeft");
			base.BeginMeleeAttackEffect();
		}

		// Token: 0x06000CB7 RID: 3255 RVA: 0x000358DB File Offset: 0x00033ADB
		public override void OnSerialize(NetworkWriter writer)
		{
			base.OnSerialize(writer);
			writer.Write((byte)this.gauntlet);
		}

		// Token: 0x06000CB8 RID: 3256 RVA: 0x000358F1 File Offset: 0x00033AF1
		public override void OnDeserialize(NetworkReader reader)
		{
			base.OnDeserialize(reader);
			this.gauntlet = (int)reader.ReadByte();
		}

		// Token: 0x06000CB9 RID: 3257 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x04000F82 RID: 3970
		public int gauntlet;
	}
}
