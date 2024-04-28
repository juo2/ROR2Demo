using System;
using RoR2;
using RoR2.Projectile;
using UnityEngine;

namespace EntityStates.Engi.SpiderMine
{
	// Token: 0x0200038C RID: 908
	public class BaseSpiderMineState : BaseState
	{
		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06001046 RID: 4166 RVA: 0x00047A6E File Offset: 0x00045C6E
		// (set) Token: 0x06001047 RID: 4167 RVA: 0x00047A76 File Offset: 0x00045C76
		private protected ProjectileStickOnImpact projectileStickOnImpact { protected get; private set; }

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06001048 RID: 4168 RVA: 0x00047A7F File Offset: 0x00045C7F
		// (set) Token: 0x06001049 RID: 4169 RVA: 0x00047A87 File Offset: 0x00045C87
		private protected ProjectileTargetComponent projectileTargetComponent { protected get; private set; }

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x0600104A RID: 4170 RVA: 0x00047A90 File Offset: 0x00045C90
		// (set) Token: 0x0600104B RID: 4171 RVA: 0x00047A98 File Offset: 0x00045C98
		private protected ProjectileGhostController projectileGhostController { protected get; private set; }

		// Token: 0x0600104C RID: 4172 RVA: 0x00047AA4 File Offset: 0x00045CA4
		public override void OnEnter()
		{
			base.OnEnter();
			this.projectileStickOnImpact = base.GetComponent<ProjectileStickOnImpact>();
			this.projectileTargetComponent = base.GetComponent<ProjectileTargetComponent>();
			this.projectileGhostController = base.projectileController.ghost;
			if (base.modelLocator && this.projectileGhostController)
			{
				base.modelLocator.modelBaseTransform = this.projectileGhostController.transform;
				base.modelLocator.modelTransform = base.modelLocator.modelBaseTransform.Find("mdlEngiSpiderMine");
			}
			if (this.projectileStickOnImpact.enabled != this.shouldStick)
			{
				this.projectileStickOnImpact.enabled = this.shouldStick;
			}
			Transform transform = base.FindModelChild(this.childLocatorStringToEnable);
			if (transform)
			{
				transform.gameObject.SetActive(true);
			}
			Util.PlaySound(this.enterSoundString, base.gameObject);
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x0600104D RID: 4173 RVA: 0x0000CF8A File Offset: 0x0000B18A
		protected virtual bool shouldStick
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600104E RID: 4174 RVA: 0x00047B87 File Offset: 0x00045D87
		protected void EmitDustEffect()
		{
			if (this.projectileGhostController)
			{
				this.projectileGhostController.transform.Find("Ring").GetComponent<ParticleSystem>().Play();
			}
		}

		// Token: 0x040014C4 RID: 5316
		[SerializeField]
		public string enterSoundString;

		// Token: 0x040014C5 RID: 5317
		[SerializeField]
		public string childLocatorStringToEnable;
	}
}
