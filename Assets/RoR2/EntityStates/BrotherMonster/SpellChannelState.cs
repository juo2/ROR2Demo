using System;
using RoR2;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace EntityStates.BrotherMonster
{
	// Token: 0x02000444 RID: 1092
	public class SpellChannelState : SpellBaseState
	{
		// Token: 0x17000126 RID: 294
		// (get) Token: 0x0600138C RID: 5004 RVA: 0x0000CF8A File Offset: 0x0000B18A
		protected override bool DisplayWeapon
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600138D RID: 5005 RVA: 0x000570FC File Offset: 0x000552FC
		public override void OnEnter()
		{
			base.OnEnter();
			this.PlayAnimation("Body", "SpellChannel");
			Util.PlaySound("Play_moonBrother_phase4_itemSuck_start", base.gameObject);
			this.spellChannelChildTransform = base.FindModelChild("SpellChannel");
			if (this.spellChannelChildTransform)
			{
				this.channelEffectInstance = UnityEngine.Object.Instantiate<GameObject>(SpellChannelState.channelEffectPrefab, this.spellChannelChildTransform.position, Quaternion.identity, this.spellChannelChildTransform);
			}
			base.characterBody.AddBuff(RoR2Content.Buffs.Immune);
		}

		// Token: 0x0600138E RID: 5006 RVA: 0x00057184 File Offset: 0x00055384
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (!this.itemStealController)
			{
				return;
			}
			if (!this.hasSubscribedToStealFinish && base.isAuthority)
			{
				this.hasSubscribedToStealFinish = true;
				if (NetworkServer.active)
				{
					this.itemStealController.onStealFinishServer.AddListener(new UnityAction(this.OnStealEndAuthority));
				}
				else
				{
					this.itemStealController.onStealFinishClient += this.OnStealEndAuthority;
				}
			}
			if (NetworkServer.active && base.fixedAge > SpellChannelState.delayBeforeBeginningSteal && !this.hasBegunSteal)
			{
				this.hasBegunSteal = true;
				this.itemStealController.stealInterval = SpellChannelState.stealInterval;
				TeamIndex teamIndex = base.GetTeam();
				this.itemStealController.StartSteal((CharacterMaster characterMaster) => characterMaster.teamIndex != teamIndex && characterMaster.hasBody);
			}
			if (base.isAuthority && base.fixedAge > SpellChannelState.delayBeforeBeginningSteal + SpellChannelState.maxDuration)
			{
				this.outer.SetNextState(new SpellChannelExitState());
			}
			if (this.spellChannelChildTransform)
			{
				this.itemStealController.transform.position = this.spellChannelChildTransform.position;
			}
		}

		// Token: 0x0600138F RID: 5007 RVA: 0x000572A8 File Offset: 0x000554A8
		public override void OnExit()
		{
			if (this.itemStealController && this.hasSubscribedToStealFinish)
			{
				this.itemStealController.onStealFinishServer.RemoveListener(new UnityAction(this.OnStealEndAuthority));
				this.itemStealController.onStealFinishClient -= this.OnStealEndAuthority;
			}
			if (this.channelEffectInstance)
			{
				EntityState.Destroy(this.channelEffectInstance);
			}
			Util.PlaySound("Play_moonBrother_phase4_itemSuck_end", base.gameObject);
			base.characterBody.RemoveBuff(RoR2Content.Buffs.Immune);
			base.OnExit();
		}

		// Token: 0x06001390 RID: 5008 RVA: 0x0005733C File Offset: 0x0005553C
		private void OnStealEndAuthority()
		{
			this.outer.SetNextState(new SpellChannelExitState());
		}

		// Token: 0x040018FD RID: 6397
		public static float stealInterval;

		// Token: 0x040018FE RID: 6398
		public static float delayBeforeBeginningSteal;

		// Token: 0x040018FF RID: 6399
		public static float maxDuration;

		// Token: 0x04001900 RID: 6400
		public static GameObject channelEffectPrefab;

		// Token: 0x04001901 RID: 6401
		private bool hasBegunSteal;

		// Token: 0x04001902 RID: 6402
		private GameObject channelEffectInstance;

		// Token: 0x04001903 RID: 6403
		private Transform spellChannelChildTransform;

		// Token: 0x04001904 RID: 6404
		private bool hasSubscribedToStealFinish;
	}
}
