using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020006F2 RID: 1778
	public class GameObjectUnlockableFilter : NetworkBehaviour
	{
		// Token: 0x06002420 RID: 9248 RVA: 0x0009B0A2 File Offset: 0x000992A2
		private void Start()
		{
			if (NetworkServer.active)
			{
				this.Networkactive = this.GameObjectIsValid();
			}
		}

		// Token: 0x06002421 RID: 9249 RVA: 0x0009B0B7 File Offset: 0x000992B7
		private void FixedUpdate()
		{
			base.gameObject.SetActive(this.active);
		}

		// Token: 0x06002422 RID: 9250 RVA: 0x0009B0CC File Offset: 0x000992CC
		private bool GameObjectIsValid()
		{
			if (Run.instance)
			{
				ref string ptr = ref this.requiredUnlockable;
				ref string ptr2 = ref this.forbiddenUnlockable;
				if (!this.requiredUnlockableDef && !string.IsNullOrEmpty(ptr))
				{
					this.requiredUnlockableDef = UnlockableCatalog.GetUnlockableDef(ptr);
					ptr = null;
				}
				if (!this.forbiddenUnlockableDef && !string.IsNullOrEmpty(ptr2))
				{
					this.forbiddenUnlockableDef = UnlockableCatalog.GetUnlockableDef(ptr2);
					ptr2 = null;
				}
				bool flag = !this.requiredUnlockableDef || Run.instance.IsUnlockableUnlocked(this.requiredUnlockableDef);
				bool flag2 = !this.forbiddenUnlockableDef || Run.instance.DoesEveryoneHaveThisUnlockableUnlocked(this.forbiddenUnlockableDef);
				return flag && !flag2;
			}
			return true;
		}

		// Token: 0x06002424 RID: 9252 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06002425 RID: 9253 RVA: 0x0009B190 File Offset: 0x00099390
		// (set) Token: 0x06002426 RID: 9254 RVA: 0x0009B1A3 File Offset: 0x000993A3
		public bool Networkactive
		{
			get
			{
				return this.active;
			}
			[param: In]
			set
			{
				base.SetSyncVar<bool>(value, ref this.active, 1U);
			}
		}

		// Token: 0x06002427 RID: 9255 RVA: 0x0009B1B8 File Offset: 0x000993B8
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			if (forceAll)
			{
				writer.Write(this.active);
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
				writer.Write(this.active);
			}
			if (!flag)
			{
				writer.WritePackedUInt32(base.syncVarDirtyBits);
			}
			return flag;
		}

		// Token: 0x06002428 RID: 9256 RVA: 0x0009B224 File Offset: 0x00099424
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			if (initialState)
			{
				this.active = reader.ReadBoolean();
				return;
			}
			int num = (int)reader.ReadPackedUInt32();
			if ((num & 1) != 0)
			{
				this.active = reader.ReadBoolean();
			}
		}

		// Token: 0x06002429 RID: 9257 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x04002885 RID: 10373
		[Tooltip("'requiredUnlockable' will be discontinued. Use 'requiredUnlockableDef' instead.")]
		[Obsolete("'requiredUnlockable' will be discontinued. Use 'requiredUnlockableDef' instead.", false)]
		public string requiredUnlockable;

		// Token: 0x04002886 RID: 10374
		[Tooltip("'forbiddenUnlockable' will be discontinued. Use 'forbiddenUnlockableDef' instead.")]
		[Obsolete("'forbiddenUnlockable' will be discontinued. Use 'forbiddenUnlockableDef' instead.", false)]
		public string forbiddenUnlockable;

		// Token: 0x04002887 RID: 10375
		public UnlockableDef requiredUnlockableDef;

		// Token: 0x04002888 RID: 10376
		public UnlockableDef forbiddenUnlockableDef;

		// Token: 0x04002889 RID: 10377
		[SyncVar]
		private bool active;
	}
}
