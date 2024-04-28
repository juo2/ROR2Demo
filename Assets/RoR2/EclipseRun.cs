using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Text;
using HG;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020006E9 RID: 1769
	public class EclipseRun : Run
	{
		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x060022EA RID: 8938 RVA: 0x00096A67 File Offset: 0x00094C67
		public static int minUnlockableEclipseLevel
		{
			get
			{
				return EclipseRun.minEclipseLevel + 1;
			}
		}

		// Token: 0x060022EB RID: 8939 RVA: 0x00096A70 File Offset: 0x00094C70
		public static DifficultyIndex GetEclipseDifficultyIndex(int eclipseLevel)
		{
			return (DifficultyIndex)(DifficultyCatalog.standardDifficultyCount - 1 + eclipseLevel);
		}

		// Token: 0x060022EC RID: 8940 RVA: 0x00096A7C File Offset: 0x00094C7C
		private static List<UnlockableDef> GetEclipseLevelUnlockablesForSurvivor(SurvivorDef survivorDef)
		{
			List<UnlockableDef> list;
			if (!EclipseRun.survivorToEclipseUnlockables.TryGetValue(survivorDef, out list))
			{
				list = new List<UnlockableDef>();
				EclipseRun.survivorToEclipseUnlockables[survivorDef] = list;
				if (BodyCatalog.GetBodyName(BodyCatalog.FindBodyIndex(survivorDef.bodyPrefab)) != null)
				{
					int num = EclipseRun.minUnlockableEclipseLevel;
					StringBuilder stringBuilder = HG.StringBuilderPool.RentStringBuilder();
					for (;;)
					{
						stringBuilder.Clear();
						stringBuilder.Append("Eclipse.").Append(survivorDef.cachedName).Append(".").AppendInt(num, 1U, uint.MaxValue);
						UnlockableDef unlockableDef = UnlockableCatalog.GetUnlockableDef(stringBuilder.ToString());
						if (!unlockableDef)
						{
							break;
						}
						list.Add(unlockableDef);
						num++;
					}
					HG.StringBuilderPool.ReturnStringBuilder(stringBuilder);
				}
			}
			return list;
		}

		// Token: 0x060022ED RID: 8941 RVA: 0x00096B24 File Offset: 0x00094D24
		public static int GetLocalUserSurvivorCompletedEclipseLevel(LocalUser localUser, SurvivorDef survivorDef)
		{
			List<UnlockableDef> eclipseLevelUnlockablesForSurvivor = EclipseRun.GetEclipseLevelUnlockablesForSurvivor(survivorDef);
			int num = 1;
			int num2 = 0;
			while (num2 < eclipseLevelUnlockablesForSurvivor.Count && localUser.userProfile.HasUnlockable(eclipseLevelUnlockablesForSurvivor[num2]))
			{
				num = EclipseRun.minUnlockableEclipseLevel + num2;
				num2++;
			}
			return Mathf.Clamp(num - 1, 0, EclipseRun.maxEclipseLevel);
		}

		// Token: 0x060022EE RID: 8942 RVA: 0x00096B78 File Offset: 0x00094D78
		public static int GetNetworkUserSurvivorCompletedEclipseLevel(NetworkUser networkUser, SurvivorDef survivorDef)
		{
			List<UnlockableDef> eclipseLevelUnlockablesForSurvivor = EclipseRun.GetEclipseLevelUnlockablesForSurvivor(survivorDef);
			int num = 1;
			int num2 = 0;
			while (num2 < eclipseLevelUnlockablesForSurvivor.Count && networkUser.unlockables.Contains(eclipseLevelUnlockablesForSurvivor[num2]))
			{
				num = EclipseRun.minUnlockableEclipseLevel + num2;
				num2++;
			}
			return Mathf.Clamp(num - 1, 0, EclipseRun.maxEclipseLevel);
		}

		// Token: 0x060022EF RID: 8943 RVA: 0x00096BC9 File Offset: 0x00094DC9
		public static int GetEclipseLevelFromRuleBook(RuleBook ruleBook)
		{
			return ruleBook.FindDifficulty() - (DifficultyIndex)DifficultyCatalog.standardDifficultyCount + 1;
		}

		// Token: 0x060022F0 RID: 8944 RVA: 0x00096BDC File Offset: 0x00094DDC
		public override void OnClientGameOver(RunReport runReport)
		{
			base.OnClientGameOver(runReport);
			if (runReport.gameEnding.isWin)
			{
				int num = EclipseRun.GetEclipseLevelFromRuleBook(base.ruleBook) + 1;
				ReadOnlyCollection<PlayerCharacterMasterController> instances = PlayerCharacterMasterController.instances;
				for (int i = 0; i < instances.Count; i++)
				{
					PlayerCharacterMasterController playerCharacterMasterController = instances[i];
					NetworkUser networkUser = instances[i].networkUser;
					if (networkUser)
					{
						LocalUser localUser = networkUser.localUser;
						if (localUser != null)
						{
							SurvivorDef survivorPreference = networkUser.GetSurvivorPreference();
							if (survivorPreference)
							{
								UnlockableDef safe = ListUtils.GetSafe<UnlockableDef>(EclipseRun.GetEclipseLevelUnlockablesForSurvivor(survivorPreference), num - EclipseRun.minUnlockableEclipseLevel);
								if (safe)
								{
									localUser.userProfile.GrantUnlockable(safe);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060022F1 RID: 8945 RVA: 0x00096C8C File Offset: 0x00094E8C
		public override void OverrideRuleChoices(RuleChoiceMask mustInclude, RuleChoiceMask mustExclude, ulong runSeed)
		{
			base.OverrideRuleChoices(mustInclude, mustExclude, base.seed);
			int num = 0;
			ReadOnlyCollection<NetworkUser> readOnlyInstancesList = NetworkUser.readOnlyInstancesList;
			for (int i = 0; i < readOnlyInstancesList.Count; i++)
			{
				NetworkUser networkUser = readOnlyInstancesList[i];
				SurvivorDef survivorPreference = networkUser.GetSurvivorPreference();
				if (survivorPreference)
				{
					int num2 = EclipseRun.GetNetworkUserSurvivorCompletedEclipseLevel(networkUser, survivorPreference) + 1;
					num = ((num > 0) ? Math.Min(num, num2) : num2);
				}
			}
			num = Math.Min(num, EclipseRun.maxEclipseLevel);
			base.ForceChoice(mustInclude, mustExclude, string.Format("Difficulty.{0}", EclipseRun.GetEclipseDifficultyIndex(num).ToString()));
			base.ForceChoice(mustInclude, mustExclude, "Items." + RoR2Content.Items.LunarTrinket.name + ".Off");
			for (int j = 0; j < ArtifactCatalog.artifactCount; j++)
			{
				base.ForceChoice(mustInclude, mustExclude, EclipseRun.<OverrideRuleChoices>g__FindRuleForArtifact|11_0((ArtifactIndex)j).FindChoice("Off"));
			}
		}

		// Token: 0x060022F2 RID: 8946 RVA: 0x00096D77 File Offset: 0x00094F77
		protected override void HandlePostRunDestination()
		{
			Console.instance.SubmitCmd(null, "transition_command \"disconnect\";", false);
		}

		// Token: 0x060022F3 RID: 8947 RVA: 0x00096D8A File Offset: 0x00094F8A
		protected new void Start()
		{
			base.Start();
			if (NetworkServer.active)
			{
				base.SetEventFlag("NoArtifactWorld");
				base.SetEventFlag("NoMysterySpace");
				base.SetEventFlag("NoVoidStage");
			}
		}

		// Token: 0x060022F6 RID: 8950 RVA: 0x00096DD4 File Offset: 0x00094FD4
		[CompilerGenerated]
		internal static RuleDef <OverrideRuleChoices>g__FindRuleForArtifact|11_0(ArtifactIndex artifactIndex)
		{
			ArtifactDef artifactDef = ArtifactCatalog.GetArtifactDef(artifactIndex);
			return RuleCatalog.FindRuleDef("Artifacts." + artifactDef.cachedName);
		}

		// Token: 0x060022F7 RID: 8951 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x060022F8 RID: 8952 RVA: 0x00096E00 File Offset: 0x00095000
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			bool flag = base.OnSerialize(writer, forceAll);
			bool flag2;
			return flag2 || flag;
		}

		// Token: 0x060022F9 RID: 8953 RVA: 0x00075971 File Offset: 0x00073B71
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			base.OnDeserialize(reader, initialState);
		}

		// Token: 0x060022FA RID: 8954 RVA: 0x0007597B File Offset: 0x00073B7B
		public override void PreStartClient()
		{
			base.PreStartClient();
		}

		// Token: 0x040027FA RID: 10234
		public static int minEclipseLevel = 1;

		// Token: 0x040027FB RID: 10235
		public static int maxEclipseLevel = 8;

		// Token: 0x040027FC RID: 10236
		private static Dictionary<SurvivorDef, List<UnlockableDef>> survivorToEclipseUnlockables = new Dictionary<SurvivorDef, List<UnlockableDef>>();
	}
}
