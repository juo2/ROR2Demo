using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.CaptainSupplyDrop
{
	// Token: 0x0200041B RID: 1051
	public class UnlockTargetState : BaseMainState
	{
		// Token: 0x060012EE RID: 4846 RVA: 0x000545B0 File Offset: 0x000527B0
		public override void OnEnter()
		{
			base.OnEnter();
			if (NetworkServer.active && this.target && this.target.available)
			{
				this.target.Networkcost = 0;
				GameObject ownerObject = base.GetComponent<GenericOwnership>().ownerObject;
				if (ownerObject)
				{
					Interactor component = ownerObject.GetComponent<Interactor>();
					if (component)
					{
						component.AttemptInteraction(this.target.gameObject);
					}
				}
				EffectManager.SpawnEffect(UnlockTargetState.unlockEffectPrefab, new EffectData
				{
					origin = this.target.transform.position
				}, true);
			}
			Util.PlaySound(UnlockTargetState.soundString, base.gameObject);
			this.duration = UnlockTargetState.baseDuration;
		}

		// Token: 0x060012EF RID: 4847 RVA: 0x00054669 File Offset: 0x00052869
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x060012F0 RID: 4848 RVA: 0x00054692 File Offset: 0x00052892
		public override void OnSerialize(NetworkWriter writer)
		{
			base.OnSerialize(writer);
			writer.Write(this.target ? this.target.gameObject : null);
		}

		// Token: 0x060012F1 RID: 4849 RVA: 0x000546BC File Offset: 0x000528BC
		public override void OnDeserialize(NetworkReader reader)
		{
			base.OnDeserialize(reader);
			GameObject gameObject = reader.ReadGameObject();
			this.target = (gameObject ? gameObject.GetComponent<PurchaseInteraction>() : null);
		}

		// Token: 0x0400184C RID: 6220
		public static GameObject unlockEffectPrefab;

		// Token: 0x0400184D RID: 6221
		public static float baseDuration;

		// Token: 0x0400184E RID: 6222
		public static string soundString;

		// Token: 0x0400184F RID: 6223
		public PurchaseInteraction target;

		// Token: 0x04001850 RID: 6224
		private float duration;
	}
}
