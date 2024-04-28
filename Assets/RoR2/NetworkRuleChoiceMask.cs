using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020007CF RID: 1999
	[DefaultExecutionOrder(-1)]
	public class NetworkRuleChoiceMask : NetworkBehaviour
	{
		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06002A71 RID: 10865 RVA: 0x000B70F8 File Offset: 0x000B52F8
		// (set) Token: 0x06002A72 RID: 10866 RVA: 0x000B7100 File Offset: 0x000B5300
		public RuleChoiceMask ruleChoiceMask { get; private set; }

		// Token: 0x06002A73 RID: 10867 RVA: 0x000B7109 File Offset: 0x000B5309
		private void Awake()
		{
			this.ruleChoiceMask = new RuleChoiceMask();
		}

		// Token: 0x06002A74 RID: 10868 RVA: 0x000B7116 File Offset: 0x000B5316
		[Server]
		public void SetRuleChoiceMask([NotNull] RuleChoiceMask newRuleChoiceMask)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.NetworkRuleChoiceMask::SetRuleChoiceMask(RoR2.RuleChoiceMask)' called on client");
				return;
			}
			if (this.ruleChoiceMask.Equals(newRuleChoiceMask))
			{
				return;
			}
			base.SetDirtyBit(1U);
			this.ruleChoiceMask.Copy(newRuleChoiceMask);
		}

		// Token: 0x06002A75 RID: 10869 RVA: 0x000B7150 File Offset: 0x000B5350
		public override bool OnSerialize(NetworkWriter writer, bool initialState)
		{
			uint num = base.syncVarDirtyBits;
			if (initialState)
			{
				num = 1U;
			}
			bool flag = (num & 1U) > 0U;
			writer.Write((byte)num);
			if (flag)
			{
				writer.Write(this.ruleChoiceMask);
			}
			return !initialState && num > 0U;
		}

		// Token: 0x06002A76 RID: 10870 RVA: 0x000B718E File Offset: 0x000B538E
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			if ((reader.ReadByte() & 1) != 0)
			{
				reader.ReadRuleChoiceMask(this.ruleChoiceMask);
			}
		}

		// Token: 0x06002A78 RID: 10872 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x06002A79 RID: 10873 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x04002D9A RID: 11674
		private const uint maskDirtyBit = 1U;
	}
}
