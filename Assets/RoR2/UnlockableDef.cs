using System;
using JetBrains.Annotations;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000569 RID: 1385
	[CreateAssetMenu(menuName = "RoR2/UnlockableDef")]
	public class UnlockableDef : ScriptableObject
	{
		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06001903 RID: 6403 RVA: 0x0006C6DD File Offset: 0x0006A8DD
		// (set) Token: 0x06001904 RID: 6404 RVA: 0x0006C6E5 File Offset: 0x0006A8E5
		public UnlockableIndex index { get; set; } = UnlockableIndex.None;

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06001905 RID: 6405 RVA: 0x00062756 File Offset: 0x00060956
		// (set) Token: 0x06001906 RID: 6406 RVA: 0x00062756 File Offset: 0x00060956
		[Obsolete(".name should not be used. Use .cachedName instead. If retrieving the value from the engine is absolutely necessary, cast to ScriptableObject first.", true)]
		public new string name
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06001907 RID: 6407 RVA: 0x0006C6EE File Offset: 0x0006A8EE
		// (set) Token: 0x06001908 RID: 6408 RVA: 0x0006C6F6 File Offset: 0x0006A8F6
		public string cachedName
		{
			get
			{
				return this._cachedName;
			}
			set
			{
				base.name = value;
				this._cachedName = value;
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06001909 RID: 6409 RVA: 0x0006C706 File Offset: 0x0006A906
		// (set) Token: 0x0600190A RID: 6410 RVA: 0x0006C70E File Offset: 0x0006A90E
		[NotNull]
		public Func<string> getHowToUnlockString { get; set; } = () => "???";

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x0600190B RID: 6411 RVA: 0x0006C717 File Offset: 0x0006A917
		// (set) Token: 0x0600190C RID: 6412 RVA: 0x0006C71F File Offset: 0x0006A91F
		[NotNull]
		public Func<string> getUnlockedString { get; set; } = () => "???";

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x0600190D RID: 6413 RVA: 0x0006C728 File Offset: 0x0006A928
		// (set) Token: 0x0600190E RID: 6414 RVA: 0x0006C730 File Offset: 0x0006A930
		public int sortScore { get; set; }

		// Token: 0x0600190F RID: 6415 RVA: 0x0006C739 File Offset: 0x0006A939
		private void Awake()
		{
			this._cachedName = base.name;
		}

		// Token: 0x06001910 RID: 6416 RVA: 0x0006C739 File Offset: 0x0006A939
		private void OnValidate()
		{
			this._cachedName = base.name;
		}

		// Token: 0x06001911 RID: 6417 RVA: 0x0006C748 File Offset: 0x0006A948
		[ContextMenu("Update displayModelPath to displayModelPrefab")]
		private void ReplaceDisplayModelPrefabPathWithDirectReference()
		{
			string text = this.displayModelPath;
			if (!string.IsNullOrEmpty(text))
			{
				GameObject exists = LegacyResourcesAPI.Load<GameObject>(text);
				if (exists)
				{
					this.displayModelPrefab = exists;
					this.displayModelPath = string.Empty;
					EditorUtil.SetDirty(this);
				}
			}
		}

		// Token: 0x06001912 RID: 6418 RVA: 0x0006C78C File Offset: 0x0006A98C
		[ConCommand(commandName = "unlockable_migrate", flags = ConVarFlags.None, helpText = "Generates UnlockableDef assets from the existing catalog entries.")]
		private static void CCUnlockableMigrate(ConCommandArgs args)
		{
			for (UnlockableIndex unlockableIndex = (UnlockableIndex)0; unlockableIndex < (UnlockableIndex)UnlockableCatalog.unlockableCount; unlockableIndex++)
			{
				EditorUtil.CopyToScriptableObject<UnlockableDef, UnlockableDef>(UnlockableCatalog.GetUnlockableDef(unlockableIndex), "Assets/RoR2/Resources/UnlockableDefs/");
			}
		}

		// Token: 0x04001EDF RID: 7903
		private string _cachedName;

		// Token: 0x04001EE0 RID: 7904
		public string nameToken = "???";

		// Token: 0x04001EE1 RID: 7905
		[Obsolete("UnlockableDef.displayModelPath is obsolete. Use .displayModel instead.", false)]
		public string displayModelPath = "Prefabs/NullModel";

		// Token: 0x04001EE2 RID: 7906
		public GameObject displayModelPrefab;

		// Token: 0x04001EE3 RID: 7907
		public bool hidden;

		// Token: 0x04001EE4 RID: 7908
		public Sprite achievementIcon;
	}
}
