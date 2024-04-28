using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using HG;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x02000861 RID: 2145
	[RequireComponent(typeof(Run))]
	[DisallowMultipleComponent]
	public class RunArtifactManager : NetworkBehaviour
	{
		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06002EF1 RID: 12017 RVA: 0x000C85DC File Offset: 0x000C67DC
		// (set) Token: 0x06002EF2 RID: 12018 RVA: 0x000C85E3 File Offset: 0x000C67E3
		public static RunArtifactManager instance { get; private set; }

		// Token: 0x06002EF3 RID: 12019 RVA: 0x000C85EB File Offset: 0x000C67EB
		private void Awake()
		{
			this._enabledArtifacts = RunArtifactManager.enabledArtifactMaskPool.Request();
			this.run = base.GetComponent<Run>();
			Run.onServerRunSetRuleBookGlobal += this.OnServerRunSetRuleBook;
		}

		// Token: 0x06002EF4 RID: 12020 RVA: 0x000C861C File Offset: 0x000C681C
		private void OnDestroy()
		{
			int i = 0;
			int artifactCount = ArtifactCatalog.artifactCount;
			while (i < artifactCount)
			{
				this.SetArtifactEnabled(ArtifactCatalog.GetArtifactDef((ArtifactIndex)i), false);
				i++;
			}
			Run.onServerRunSetRuleBookGlobal -= this.OnServerRunSetRuleBook;
			if (this._enabledArtifacts != null)
			{
				RunArtifactManager.enabledArtifactMaskPool.Return(this._enabledArtifacts);
				this._enabledArtifacts = null;
			}
		}

		// Token: 0x06002EF5 RID: 12021 RVA: 0x000C8678 File Offset: 0x000C6878
		private void OnEnable()
		{
			RunArtifactManager.instance = SingletonHelper.Assign<RunArtifactManager>(RunArtifactManager.instance, this);
		}

		// Token: 0x06002EF6 RID: 12022 RVA: 0x000C868A File Offset: 0x000C688A
		private void OnDisable()
		{
			RunArtifactManager.instance = SingletonHelper.Unassign<RunArtifactManager>(RunArtifactManager.instance, this);
		}

		// Token: 0x06002EF7 RID: 12023 RVA: 0x000C869C File Offset: 0x000C689C
		public override bool OnSerialize(NetworkWriter writer, bool initialState)
		{
			uint num = initialState ? RunArtifactManager.allDirtyBits : base.syncVarDirtyBits;
			bool flag = (num & RunArtifactManager.enabledArtifactsDirtyBit) > 0U;
			writer.WritePackedUInt32(num);
			if (flag)
			{
				writer.WriteBitArray(this._enabledArtifacts);
			}
			return !initialState && num > 0U;
		}

		// Token: 0x06002EF8 RID: 12024 RVA: 0x000C86E4 File Offset: 0x000C68E4
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			if ((reader.ReadPackedUInt32() & RunArtifactManager.enabledArtifactsDirtyBit) > 0U)
			{
				bool[] array = RunArtifactManager.enabledArtifactMaskPool.Request();
				reader.ReadBitArray(array);
				int i = 0;
				int artifactCount = ArtifactCatalog.artifactCount;
				while (i < artifactCount)
				{
					this.SetArtifactEnabled(ArtifactCatalog.GetArtifactDef((ArtifactIndex)i), array[i]);
					i++;
				}
			}
		}

		// Token: 0x06002EF9 RID: 12025 RVA: 0x000C8738 File Offset: 0x000C6938
		private void OnServerRunSetRuleBook([NotNull] Run run, [NotNull] RuleBook newRuleBook)
		{
			bool[] array = RunArtifactManager.enabledArtifactMaskPool.Request();
			int i = 0;
			int ruleCount = RuleCatalog.ruleCount;
			while (i < ruleCount)
			{
				RuleChoiceDef ruleChoice = newRuleBook.GetRuleChoice(i);
				if (ruleChoice.artifactIndex != ArtifactIndex.None)
				{
					array[(int)ruleChoice.artifactIndex] = true;
				}
				i++;
			}
			int j = 0;
			int artifactCount = ArtifactCatalog.artifactCount;
			while (j < artifactCount)
			{
				this.SetArtifactEnabled(ArtifactCatalog.GetArtifactDef((ArtifactIndex)j), array[j]);
				j++;
			}
			RunArtifactManager.enabledArtifactMaskPool.Return(array);
		}

		// Token: 0x14000091 RID: 145
		// (add) Token: 0x06002EFA RID: 12026 RVA: 0x000C87B0 File Offset: 0x000C69B0
		// (remove) Token: 0x06002EFB RID: 12027 RVA: 0x000C87E4 File Offset: 0x000C69E4
		public static event RunArtifactManager.ArtifactStateChangeDelegate onArtifactEnabledGlobal;

		// Token: 0x14000092 RID: 146
		// (add) Token: 0x06002EFC RID: 12028 RVA: 0x000C8818 File Offset: 0x000C6A18
		// (remove) Token: 0x06002EFD RID: 12029 RVA: 0x000C884C File Offset: 0x000C6A4C
		public static event RunArtifactManager.ArtifactStateChangeDelegate onArtifactDisabledGlobal;

		// Token: 0x06002EFE RID: 12030 RVA: 0x000C8880 File Offset: 0x000C6A80
		private void SetArtifactEnabled([NotNull] ArtifactDef artifactDef, bool newEnabled)
		{
			ref bool ptr = ref this._enabledArtifacts[(int)artifactDef.artifactIndex];
			if (ptr == newEnabled)
			{
				return;
			}
			if (NetworkServer.active)
			{
				base.SetDirtyBit(RunArtifactManager.enabledArtifactsDirtyBit);
			}
			ptr = newEnabled;
			RunArtifactManager.ArtifactStateChangeDelegate artifactStateChangeDelegate = ptr ? RunArtifactManager.onArtifactEnabledGlobal : RunArtifactManager.onArtifactDisabledGlobal;
			if (artifactStateChangeDelegate == null)
			{
				return;
			}
			artifactStateChangeDelegate(this, artifactDef);
		}

		// Token: 0x06002EFF RID: 12031 RVA: 0x000C88D6 File Offset: 0x000C6AD6
		[SystemInitializer(new Type[]
		{
			typeof(ArtifactCatalog)
		})]
		private static void Init()
		{
			RunArtifactManager.enabledArtifactMaskPool.lengthOfArrays = ArtifactCatalog.artifactCount;
		}

		// Token: 0x06002F00 RID: 12032 RVA: 0x000C88E7 File Offset: 0x000C6AE7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool IsArtifactEnabled([NotNull] ArtifactDef artifactDef)
		{
			return this.IsArtifactEnabled(artifactDef.artifactIndex);
		}

		// Token: 0x06002F01 RID: 12033 RVA: 0x000C88F8 File Offset: 0x000C6AF8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool IsArtifactEnabled(ArtifactIndex artifactIndex)
		{
			bool[] enabledArtifacts = this._enabledArtifacts;
			bool flag = false;
			return ArrayUtils.GetSafe<bool>(enabledArtifacts, (int)artifactIndex, flag);
		}

		// Token: 0x06002F02 RID: 12034 RVA: 0x000C8915 File Offset: 0x000C6B15
		[Server]
		public void SetArtifactEnabledServer(ArtifactDef artifactDef, bool newEnabled)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.RunArtifactManager::SetArtifactEnabledServer(RoR2.ArtifactDef,System.Boolean)' called on client");
				return;
			}
			this.SetArtifactEnabled(artifactDef, newEnabled);
		}

		// Token: 0x06002F05 RID: 12037 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x06002F06 RID: 12038 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x040030F3 RID: 12531
		private Run run;

		// Token: 0x040030F4 RID: 12532
		private static readonly uint enabledArtifactsDirtyBit = 1U;

		// Token: 0x040030F5 RID: 12533
		private static readonly uint allDirtyBits = RunArtifactManager.enabledArtifactsDirtyBit;

		// Token: 0x040030F8 RID: 12536
		private bool[] _enabledArtifacts;

		// Token: 0x040030F9 RID: 12537
		private static readonly FixedSizeArrayPool<bool> enabledArtifactMaskPool = new FixedSizeArrayPool<bool>(0);

		// Token: 0x040030FA RID: 12538
		public static readonly GenericStaticEnumerable<ArtifactDef, RunArtifactManager.RunEnabledArtifacts> enabledArtifactsEnumerable = default(GenericStaticEnumerable<ArtifactDef, RunArtifactManager.RunEnabledArtifacts>);

		// Token: 0x02000862 RID: 2146
		// (Invoke) Token: 0x06002F08 RID: 12040
		public delegate void ArtifactStateChangeDelegate([NotNull] RunArtifactManager runArtifactManager, [NotNull] ArtifactDef artifactDef);

		// Token: 0x02000863 RID: 2147
		public struct RunEnabledArtifacts : IEnumerator<ArtifactDef>, IEnumerator, IDisposable
		{
			// Token: 0x06002F0B RID: 12043 RVA: 0x000C895C File Offset: 0x000C6B5C
			public bool MoveNext()
			{
				RunArtifactManager instance = RunArtifactManager.instance;
				if (instance == null)
				{
					return false;
				}
				do
				{
					ArtifactIndex artifactIndex = this.artifactIndex + 1;
					this.artifactIndex = artifactIndex;
					if (artifactIndex >= (ArtifactIndex)ArtifactCatalog.artifactCount)
					{
						return false;
					}
				}
				while (!instance.IsArtifactEnabled(this.artifactIndex));
				return true;
			}

			// Token: 0x06002F0C RID: 12044 RVA: 0x000C899D File Offset: 0x000C6B9D
			public void Reset()
			{
				this.artifactIndex = ArtifactIndex.None;
			}

			// Token: 0x17000441 RID: 1089
			// (get) Token: 0x06002F0D RID: 12045 RVA: 0x000C89A6 File Offset: 0x000C6BA6
			public ArtifactDef Current
			{
				get
				{
					return ArtifactCatalog.GetArtifactDef(this.artifactIndex);
				}
			}

			// Token: 0x17000442 RID: 1090
			// (get) Token: 0x06002F0E RID: 12046 RVA: 0x000C89B3 File Offset: 0x000C6BB3
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06002F0F RID: 12047 RVA: 0x000026ED File Offset: 0x000008ED
			public void Dispose()
			{
			}

			// Token: 0x040030FB RID: 12539
			private ArtifactIndex artifactIndex;
		}
	}
}
