using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020007CE RID: 1998
	[DefaultExecutionOrder(-1)]
	public class NetworkRuleBook : NetworkBehaviour
	{
		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06002A66 RID: 10854 RVA: 0x000B6F82 File Offset: 0x000B5182
		// (set) Token: 0x06002A67 RID: 10855 RVA: 0x000B6F8A File Offset: 0x000B518A
		public RuleBook ruleBook { get; private set; }

		// Token: 0x14000074 RID: 116
		// (add) Token: 0x06002A68 RID: 10856 RVA: 0x000B6F94 File Offset: 0x000B5194
		// (remove) Token: 0x06002A69 RID: 10857 RVA: 0x000B6FCC File Offset: 0x000B51CC
		public event Action<NetworkRuleBook> onRuleBookUpdated;

		// Token: 0x06002A6A RID: 10858 RVA: 0x000B7001 File Offset: 0x000B5201
		private void Awake()
		{
			this.ruleBook = new RuleBook();
		}

		// Token: 0x06002A6B RID: 10859 RVA: 0x000B7010 File Offset: 0x000B5210
		[Server]
		public void SetRuleBook([NotNull] RuleBook newRuleBook)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.NetworkRuleBook::SetRuleBook(RoR2.RuleBook)' called on client");
				return;
			}
			if (this.ruleBook.Equals(newRuleBook))
			{
				return;
			}
			base.SetDirtyBit(1U);
			this.ruleBook.Copy(newRuleBook);
			Action<NetworkRuleBook> action = this.onRuleBookUpdated;
			if (action == null)
			{
				return;
			}
			action(this);
		}

		// Token: 0x06002A6C RID: 10860 RVA: 0x000B7068 File Offset: 0x000B5268
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
				writer.Write(this.ruleBook);
			}
			return !initialState && num > 0U;
		}

		// Token: 0x06002A6D RID: 10861 RVA: 0x000B70A8 File Offset: 0x000B52A8
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			if ((reader.ReadByte() & 1) != 0)
			{
				reader.ReadRuleBook(this.ruleBook);
				try
				{
					Action<NetworkRuleBook> action = this.onRuleBookUpdated;
					if (action != null)
					{
						action(this);
					}
				}
				catch (Exception message)
				{
					Debug.LogError(message);
				}
			}
		}

		// Token: 0x06002A6F RID: 10863 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x06002A70 RID: 10864 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x04002D98 RID: 11672
		private const uint ruleBookDirtyBit = 1U;
	}
}
