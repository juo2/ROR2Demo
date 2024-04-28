using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020006FA RID: 1786
	public sealed class GenericInteraction : NetworkBehaviour, IInteractable
	{
		// Token: 0x06002476 RID: 9334 RVA: 0x0009BD55 File Offset: 0x00099F55
		[Server]
		public void SetInteractabilityAvailable()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.GenericInteraction::SetInteractabilityAvailable()' called on client");
				return;
			}
			this.Networkinteractability = Interactability.Available;
		}

		// Token: 0x06002477 RID: 9335 RVA: 0x0009BD73 File Offset: 0x00099F73
		[Server]
		public void SetInteractabilityConditionsNotMet()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.GenericInteraction::SetInteractabilityConditionsNotMet()' called on client");
				return;
			}
			this.Networkinteractability = Interactability.ConditionsNotMet;
		}

		// Token: 0x06002478 RID: 9336 RVA: 0x0009BD91 File Offset: 0x00099F91
		[Server]
		public void SetInteractabilityDisabled()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.GenericInteraction::SetInteractabilityDisabled()' called on client");
				return;
			}
			this.Networkinteractability = Interactability.Disabled;
		}

		// Token: 0x06002479 RID: 9337 RVA: 0x0009BDAF File Offset: 0x00099FAF
		string IInteractable.GetContextString(Interactor activator)
		{
			if (this.contextToken == "")
			{
				return null;
			}
			return Language.GetString(this.contextToken);
		}

		// Token: 0x0600247A RID: 9338 RVA: 0x0009BDD0 File Offset: 0x00099FD0
		public bool ShouldIgnoreSpherecastForInteractibility(Interactor activator)
		{
			return this.shouldIgnoreSpherecastForInteractibility;
		}

		// Token: 0x0600247B RID: 9339 RVA: 0x0009BDD8 File Offset: 0x00099FD8
		Interactability IInteractable.GetInteractability(Interactor activator)
		{
			return this.interactability;
		}

		// Token: 0x0600247C RID: 9340 RVA: 0x0009BDE0 File Offset: 0x00099FE0
		void IInteractable.OnInteractionBegin(Interactor activator)
		{
			this.onActivation.Invoke(activator);
		}

		// Token: 0x0600247D RID: 9341 RVA: 0x0009BDEE File Offset: 0x00099FEE
		private void OnEnable()
		{
			InstanceTracker.Add<GenericInteraction>(this);
		}

		// Token: 0x0600247E RID: 9342 RVA: 0x0009BDF6 File Offset: 0x00099FF6
		private void OnDisable()
		{
			InstanceTracker.Remove<GenericInteraction>(this);
		}

		// Token: 0x0600247F RID: 9343 RVA: 0x0009BDFE File Offset: 0x00099FFE
		public bool ShouldShowOnScanner()
		{
			return this.shouldShowOnScanner && this.interactability > Interactability.Disabled;
		}

		// Token: 0x06002481 RID: 9345 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06002482 RID: 9346 RVA: 0x0009BE2C File Offset: 0x0009A02C
		// (set) Token: 0x06002483 RID: 9347 RVA: 0x0009BE40 File Offset: 0x0009A040
		public Interactability Networkinteractability
		{
			get
			{
				return this.interactability;
			}
			[param: In]
			set
			{
				ulong newValueAsUlong = (ulong)((long)value);
				ulong fieldValueAsUlong = (ulong)((long)this.interactability);
				base.SetSyncVarEnum<Interactability>(value, newValueAsUlong, ref this.interactability, fieldValueAsUlong, 1U);
			}
		}

		// Token: 0x06002484 RID: 9348 RVA: 0x0009BE70 File Offset: 0x0009A070
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			if (forceAll)
			{
				writer.Write((int)this.interactability);
				return true;
			}
			bool flag = false;
			if ((base.syncVarDirtyBits & 1U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				writer.Write((int)this.interactability);
			}
			if (!flag)
			{
				writer.WritePackedUInt32(base.syncVarDirtyBits);
			}
			return flag;
		}

		// Token: 0x06002485 RID: 9349 RVA: 0x0009BEDC File Offset: 0x0009A0DC
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			if (initialState)
			{
				this.interactability = (Interactability)reader.ReadInt32();
				return;
			}
			int num = (int)reader.ReadPackedUInt32();
			if ((num & 1) != 0)
			{
				this.interactability = (Interactability)reader.ReadInt32();
			}
		}

		// Token: 0x06002486 RID: 9350 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x040028A9 RID: 10409
		[SyncVar]
		public Interactability interactability = Interactability.Available;

		// Token: 0x040028AA RID: 10410
		public bool shouldIgnoreSpherecastForInteractibility;

		// Token: 0x040028AB RID: 10411
		public string contextToken;

		// Token: 0x040028AC RID: 10412
		public GenericInteraction.InteractorUnityEvent onActivation;

		// Token: 0x040028AD RID: 10413
		public bool shouldShowOnScanner = true;

		// Token: 0x020006FB RID: 1787
		[Serializable]
		public class InteractorUnityEvent : UnityEvent<Interactor>
		{
		}
	}
}
