using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.AffixVoid
{
	// Token: 0x0200049F RID: 1183
	public class SelfDestruct : BaseState
	{
		// Token: 0x0600153F RID: 5439 RVA: 0x0005E4C8 File Offset: 0x0005C6C8
		public override void OnEnter()
		{
			base.OnEnter();
			base.PlayCrossfade(this.animationLayerName, this.animationStateName, this.duration);
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
			Util.PlaySound(this.enterSoundString, base.gameObject);
		}

		// Token: 0x06001540 RID: 5440 RVA: 0x0005E55E File Offset: 0x0005C75E
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (NetworkServer.active && base.fixedAge >= this.duration)
			{
				EntityState.Destroy(base.gameObject);
			}
		}

		// Token: 0x04001B13 RID: 6931
		[SerializeField]
		public float duration;

		// Token: 0x04001B14 RID: 6932
		[SerializeField]
		public string animationLayerName;

		// Token: 0x04001B15 RID: 6933
		[SerializeField]
		public string animationStateName;

		// Token: 0x04001B16 RID: 6934
		[SerializeField]
		public string enterSoundString;
	}
}
