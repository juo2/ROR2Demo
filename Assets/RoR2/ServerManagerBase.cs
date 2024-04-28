using System;
using System.Collections.Generic;
using System.Text;
using HG;
using RoR2.Networking;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020009C7 RID: 2503
	internal class ServerManagerBase<T> : TagManager where T : ServerManagerBase<T>, IDisposable, new()
	{
		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x06003944 RID: 14660 RVA: 0x000EF57C File Offset: 0x000ED77C
		// (set) Token: 0x06003945 RID: 14661 RVA: 0x000EF583 File Offset: 0x000ED783
		public static T instance { get; private set; }

		// Token: 0x140000C8 RID: 200
		// (add) Token: 0x06003946 RID: 14662 RVA: 0x000EF58C File Offset: 0x000ED78C
		// (remove) Token: 0x06003947 RID: 14663 RVA: 0x000EF5C4 File Offset: 0x000ED7C4
		public event Action<T, List<string>> collectAdditionalTags;

		// Token: 0x06003948 RID: 14664 RVA: 0x000EF5F9 File Offset: 0x000ED7F9
		public static string GetVersionGameDataString()
		{
			string result;
			if ((result = ServerManagerBase<T>.versionGameDataString) == null)
			{
				result = (ServerManagerBase<T>.versionGameDataString = "gameData=" + RoR2Application.GetBuildId());
			}
			return result;
		}

		// Token: 0x06003949 RID: 14665 RVA: 0x000EF619 File Offset: 0x000ED819
		protected void OnPreGameControllerSetRuleBookServerGlobal(PreGameController preGameController, RuleBook ruleBook)
		{
			this.UpdateServerRuleBook();
		}

		// Token: 0x0600394A RID: 14666 RVA: 0x000EF619 File Offset: 0x000ED819
		protected void OnServerRunSetRuleBookGlobal(Run run, RuleBook ruleBook)
		{
			this.UpdateServerRuleBook();
		}

		// Token: 0x0600394B RID: 14667 RVA: 0x000EF624 File Offset: 0x000ED824
		protected void UpdateServerRuleBook()
		{
			if (Run.instance)
			{
				this.SetServerRuleBook(Run.instance.ruleBook);
			}
			else if (PreGameController.instance)
			{
				this.SetServerRuleBook(PreGameController.instance.readOnlyRuleBook);
			}
			this.UpdateTags();
		}

		// Token: 0x0600394C RID: 14668 RVA: 0x000EF674 File Offset: 0x000ED874
		protected void SetServerRuleBook(RuleBook ruleBook)
		{
			this.currentRuleBook.Copy(ruleBook);
			StringBuilder stringBuilder = HG.StringBuilderPool.RentStringBuilder();
			RuleBook.WriteBase64ToStringBuilder(ruleBook, stringBuilder);
			this.ruleBookKvHelper.SetValue(stringBuilder.ToString());
			HG.StringBuilderPool.ReturnStringBuilder(stringBuilder);
		}

		// Token: 0x0600394D RID: 14669 RVA: 0x000EF6B4 File Offset: 0x000ED8B4
		protected void UpdateTags()
		{
			this.tags.Clear();
			if (RoR2Application.isModded)
			{
				this.tags.Add("mod");
			}
			this.tags.Add(PreGameController.cvSvAllowRuleVoting.value ? "rv1" : "rv0");
			StringBuilder stringBuilder = HG.StringBuilderPool.RentStringBuilder();
			stringBuilder.Append("a=");
			foreach (RuleDef ruleDef in RuleCatalog.artifactRuleCategory.children)
			{
				RuleChoiceDef ruleChoice = this.currentRuleBook.GetRuleChoice(ruleDef);
				if (ruleChoice.localName == "On")
				{
					int artifactIndex = (int)((ArtifactDef)ruleChoice.extraData).artifactIndex;
					char value = (char)(48 + artifactIndex);
					stringBuilder.Append(value);
				}
			}
			this.tags.Add(stringBuilder.ToString());
			HG.StringBuilderPool.ReturnStringBuilder(stringBuilder);
			foreach (RuleChoiceDef ruleChoiceDef in this.currentRuleBook.choices)
			{
				if (ruleChoiceDef.serverTag != null)
				{
					this.tags.Add(ruleChoiceDef.serverTag);
				}
			}
			Action<T, List<string>> action = this.collectAdditionalTags;
			if (action != null)
			{
				action(ServerManagerBase<T>.instance, this.tags);
			}
			this.tags.Add(NetworkManagerSystem.cvSvCustomTags.value);
			string text = string.Join(",", this.tags);
			if (!base.tagsString.Equals(text, StringComparison.Ordinal))
			{
				base.tagsString = text;
				this.TagsStringUpdated();
				Action<string> onTagsStringUpdated = this.onTagsStringUpdated;
				if (onTagsStringUpdated == null)
				{
					return;
				}
				onTagsStringUpdated(base.tagsString);
			}
		}

		// Token: 0x0600394E RID: 14670 RVA: 0x000026ED File Offset: 0x000008ED
		protected virtual void TagsStringUpdated()
		{
		}

		// Token: 0x0600394F RID: 14671 RVA: 0x000EF890 File Offset: 0x000EDA90
		public virtual void Dispose()
		{
			if (this.disposed)
			{
				return;
			}
			this.disposed = true;
			RoR2Application.onUpdate -= this.Update;
			Run.onServerRunSetRuleBookGlobal -= this.OnServerRunSetRuleBookGlobal;
			PreGameController.onPreGameControllerSetRuleBookServerGlobal -= this.OnPreGameControllerSetRuleBookServerGlobal;
		}

		// Token: 0x06003950 RID: 14672 RVA: 0x000EF8E1 File Offset: 0x000EDAE1
		protected virtual void Update()
		{
			this.playerUpdateTimer -= Time.unscaledDeltaTime;
			if (this.playerUpdateTimer <= 0f)
			{
				this.playerUpdateTimer = this.playerUpdateInterval;
			}
		}

		// Token: 0x06003951 RID: 14673 RVA: 0x000EF910 File Offset: 0x000EDB10
		public static void StartServer()
		{
			T t = ServerManagerBase<T>.instance;
			if (t != null)
			{
				t.Dispose();
			}
			ServerManagerBase<T>.instance = default(T);
			if (NetworkServer.dontListen)
			{
				return;
			}
			ServerManagerBase<T>.instance = Activator.CreateInstance<T>();
			if (ServerManagerBase<T>.instance.disposed)
			{
				ServerManagerBase<T>.instance = default(T);
			}
		}

		// Token: 0x06003952 RID: 14674 RVA: 0x000EF974 File Offset: 0x000EDB74
		public static void StopServer()
		{
			T t = ServerManagerBase<T>.instance;
			if (t != null)
			{
				t.Dispose();
			}
			ServerManagerBase<T>.instance = default(T);
		}

		// Token: 0x040038E7 RID: 14567
		protected List<string> tags = new List<string>();

		// Token: 0x040038E8 RID: 14568
		protected RuleBook currentRuleBook = new RuleBook();

		// Token: 0x040038E9 RID: 14569
		protected KeyValueSplitter ruleBookKvHelper;

		// Token: 0x040038EA RID: 14570
		protected KeyValueSplitter modListKvHelper;

		// Token: 0x040038EB RID: 14571
		protected bool disposed;

		// Token: 0x040038EC RID: 14572
		protected float playerUpdateTimer;

		// Token: 0x040038ED RID: 14573
		protected float playerUpdateInterval = 5f;

		// Token: 0x040038EE RID: 14574
		private static string versionGameDataString;

		// Token: 0x040038EF RID: 14575
		protected const int k_cbMaxGameServerGameData = 2048;
	}
}
