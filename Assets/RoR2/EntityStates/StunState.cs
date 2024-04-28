using System;
using RoR2;
using UnityEngine;

namespace EntityStates
{
	// Token: 0x020000D3 RID: 211
	public class StunState : BaseState
	{
		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060003D5 RID: 981 RVA: 0x0000FB87 File Offset: 0x0000DD87
		public float timeRemaining
		{
			get
			{
				return Math.Max(this.duration - base.fixedAge, 0f);
			}
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x0000FBA0 File Offset: 0x0000DDA0
		public void ExtendStun(float durationDelta)
		{
			this.duration += durationDelta;
			this.PlayStunAnimation();
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x0000FBB8 File Offset: 0x0000DDB8
		public override void OnEnter()
		{
			base.OnEnter();
			if (base.sfxLocator && base.sfxLocator.barkSound != "")
			{
				Util.PlaySound(base.sfxLocator.barkSound, base.gameObject);
			}
			this.PlayStunAnimation();
			if (base.characterBody)
			{
				base.characterBody.isSprinting = false;
			}
			if (base.characterDirection)
			{
				base.characterDirection.moveVector = base.characterDirection.forward;
			}
			if (base.rigidbodyMotor)
			{
				base.rigidbodyMotor.moveVector = Vector3.zero;
			}
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x0000FC68 File Offset: 0x0000DE68
		private void PlayStunAnimation()
		{
			Animator modelAnimator = base.GetModelAnimator();
			if (modelAnimator)
			{
				int layerIndex = modelAnimator.GetLayerIndex("Body");
				modelAnimator.CrossFadeInFixedTime((UnityEngine.Random.Range(0, 2) == 0) ? "Hurt1" : "Hurt2", 0.1f);
				modelAnimator.Update(0f);
				AnimatorStateInfo nextAnimatorStateInfo = modelAnimator.GetNextAnimatorStateInfo(layerIndex);
				this.duration = Mathf.Max(this.stunDuration, nextAnimatorStateInfo.length);
				if (this.stunDuration >= 0f)
				{
					this.stunVfxInstance = UnityEngine.Object.Instantiate<GameObject>(StunState.stunVfxPrefab, base.transform);
					this.stunVfxInstance.GetComponent<ScaleParticleSystemDuration>().newDuration = this.duration;
				}
			}
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x0000FD17 File Offset: 0x0000DF17
		public override void OnExit()
		{
			if (this.stunVfxInstance)
			{
				EntityState.Destroy(this.stunVfxInstance);
			}
			base.OnExit();
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0000FD37 File Offset: 0x0000DF37
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x040003D3 RID: 979
		private float duration;

		// Token: 0x040003D4 RID: 980
		private GameObject stunVfxInstance;

		// Token: 0x040003D5 RID: 981
		public float stunDuration = 0.35f;

		// Token: 0x040003D6 RID: 982
		public static GameObject stunVfxPrefab;
	}
}
