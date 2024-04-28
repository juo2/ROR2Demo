using System;
using RoR2;
using UnityEngine;

namespace EntityStates.Railgunner.Scope
{
	// Token: 0x02000203 RID: 515
	public class BaseWindDown : BaseScopeState
	{
		// Token: 0x06000919 RID: 2329 RVA: 0x00025CAC File Offset: 0x00023EAC
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = this.baseDuration;
			base.SetScopeAlpha(1f);
			base.RemoveOverlay(this.duration);
			base.StartScopeParamsOverride(0f);
			base.EndScopeParamsOverride(this.duration);
			Util.PlaySound(this.enterSoundString, base.gameObject);
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x00025D0B File Offset: 0x00023F0B
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x00025D13 File Offset: 0x00023F13
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && base.fixedAge >= this.duration)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x00025D3C File Offset: 0x00023F3C
		public override void Update()
		{
			base.Update();
			base.SetScopeAlpha(1f - Mathf.Clamp01(base.age / this.duration));
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x00003BE8 File Offset: 0x00001DE8
		protected override CharacterCameraParams GetCameraParams()
		{
			return null;
		}

		// Token: 0x04000AA3 RID: 2723
		[SerializeField]
		public float baseDuration;

		// Token: 0x04000AA4 RID: 2724
		[SerializeField]
		public string enterSoundString;

		// Token: 0x04000AA5 RID: 2725
		private float duration;
	}
}
