using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using Facepunch.Steamworks;
using JetBrains.Annotations;
using Rewired;
using RoR2.Stats;
using UnityEngine;
using Zio;

namespace RoR2
{
	// Token: 0x020009E8 RID: 2536
	public class UserProfile
	{
		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x06003A7F RID: 14975 RVA: 0x000F2C48 File Offset: 0x000F0E48
		// (set) Token: 0x06003A80 RID: 14976 RVA: 0x000F2C50 File Offset: 0x000F0E50
		public bool isCorrupted { get; set; }

		// Token: 0x06003A81 RID: 14977 RVA: 0x000F2C5C File Offset: 0x000F0E5C
		public static void GenerateSaveFieldFunctions()
		{
			UserProfile.nameToSaveFieldMap.Clear();
			foreach (FieldInfo fieldInfo in typeof(UserProfile).GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
			{
				SaveFieldAttribute customAttribute = fieldInfo.GetCustomAttribute<SaveFieldAttribute>();
				if (customAttribute != null)
				{
					customAttribute.Setup(fieldInfo);
					UserProfile.nameToSaveFieldMap[fieldInfo.Name] = customAttribute;
				}
			}
			UserProfile.saveFieldNames = UserProfile.nameToSaveFieldMap.Keys.ToArray<string>();
			Array.Sort<string>(UserProfile.saveFieldNames);
			UserProfile.saveFields = (from name in UserProfile.saveFieldNames
			select UserProfile.nameToSaveFieldMap[name]).ToArray<SaveFieldAttribute>();
		}

		// Token: 0x06003A82 RID: 14978 RVA: 0x000F2D0C File Offset: 0x000F0F0C
		public void SetSaveFieldString([NotNull] string fieldName, [NotNull] string value)
		{
			SaveFieldAttribute saveFieldAttribute;
			if (UserProfile.nameToSaveFieldMap.TryGetValue(fieldName, out saveFieldAttribute))
			{
				saveFieldAttribute.setter(this, value);
				return;
			}
			Debug.LogErrorFormat("Save field {0} is not defined.", new object[]
			{
				fieldName
			});
		}

		// Token: 0x06003A83 RID: 14979 RVA: 0x000F2D4C File Offset: 0x000F0F4C
		public string GetSaveFieldString([NotNull] string fieldName)
		{
			SaveFieldAttribute saveFieldAttribute;
			if (UserProfile.nameToSaveFieldMap.TryGetValue(fieldName, out saveFieldAttribute))
			{
				return saveFieldAttribute.getter(this);
			}
			Debug.LogErrorFormat("Save field {0} is not defined.", new object[]
			{
				fieldName
			});
			return string.Empty;
		}

		// Token: 0x06003A84 RID: 14980 RVA: 0x000F2D8E File Offset: 0x000F0F8E
		public static XDocument ToXml(UserProfile userProfile)
		{
			return XmlUtility.ToXml(userProfile);
		}

		// Token: 0x06003A85 RID: 14981 RVA: 0x000F2D96 File Offset: 0x000F0F96
		public static UserProfile FromXml(XDocument doc)
		{
			return XmlUtility.FromXml(doc);
		}

		// Token: 0x06003A86 RID: 14982 RVA: 0x000F2DA0 File Offset: 0x000F0FA0
		public bool HasUnlockable([NotNull] string unlockableToken)
		{
			UnlockableDef unlockableDef = UnlockableCatalog.GetUnlockableDef(unlockableToken);
			return unlockableDef == null || this.HasUnlockable(unlockableDef);
		}

		// Token: 0x06003A87 RID: 14983 RVA: 0x000F2DC6 File Offset: 0x000F0FC6
		public bool HasUnlockable([CanBeNull] UnlockableDef unlockableDef)
		{
			return this.statSheet.HasUnlockable(unlockableDef);
		}

		// Token: 0x06003A88 RID: 14984 RVA: 0x000F2DD4 File Offset: 0x000F0FD4
		public void AddUnlockToken(string unlockableToken)
		{
			UnlockableDef unlockableDef = UnlockableCatalog.GetUnlockableDef(unlockableToken);
			if (unlockableDef != null)
			{
				this.GrantUnlockable(unlockableDef);
			}
		}

		// Token: 0x06003A89 RID: 14985 RVA: 0x000F2DF8 File Offset: 0x000F0FF8
		public void GrantUnlockable(UnlockableDef unlockableDef)
		{
			if (!this.statSheet.HasUnlockable(unlockableDef))
			{
				this.statSheet.AddUnlockable(unlockableDef);
				Debug.LogFormat("{0} unlocked {1}", new object[]
				{
					this.name,
					unlockableDef.nameToken
				});
				this.RequestEventualSave();
				Action<UserProfile, UnlockableDef> action = UserProfile.onUnlockableGranted;
				if (action == null)
				{
					return;
				}
				action(this, unlockableDef);
			}
		}

		// Token: 0x06003A8A RID: 14986 RVA: 0x000F2E58 File Offset: 0x000F1058
		public void RequestEventualSave()
		{
			if (!this.canSave)
			{
				return;
			}
			this.saveRequestPending = true;
		}

		// Token: 0x06003A8B RID: 14987 RVA: 0x000F2E6A File Offset: 0x000F106A
		public void RevokeUnlockable(UnlockableDef unlockableDef)
		{
			if (this.statSheet.HasUnlockable(unlockableDef))
			{
				this.statSheet.RemoveUnlockable(unlockableDef.index);
			}
		}

		// Token: 0x06003A8C RID: 14988 RVA: 0x000F2E8C File Offset: 0x000F108C
		public bool HasSurvivorUnlocked(SurvivorIndex survivorIndex)
		{
			SurvivorDef survivorDef = SurvivorCatalog.GetSurvivorDef(survivorIndex);
			return !(survivorDef == null) && (!survivorDef.unlockableDef || this.HasUnlockable(survivorDef.unlockableDef));
		}

		// Token: 0x06003A8D RID: 14989 RVA: 0x000F2EC6 File Offset: 0x000F10C6
		public bool HasDiscoveredPickup(PickupIndex pickupIndex)
		{
			return pickupIndex.isValid && this.discoveredPickups[pickupIndex.value];
		}

		// Token: 0x06003A8E RID: 14990 RVA: 0x000F2EE0 File Offset: 0x000F10E0
		public void DiscoverPickup(PickupIndex pickupIndex)
		{
			this.SetPickupDiscovered(pickupIndex, true);
		}

		// Token: 0x06003A8F RID: 14991 RVA: 0x000F2EEC File Offset: 0x000F10EC
		private void SetPickupDiscovered(PickupIndex pickupIndex, bool newDiscovered)
		{
			if (!pickupIndex.isValid)
			{
				return;
			}
			ref bool ptr = ref this.discoveredPickups[pickupIndex.value];
			if (ptr == newDiscovered)
			{
				return;
			}
			ptr = newDiscovered;
			if (newDiscovered)
			{
				Action<PickupIndex> action = this.onPickupDiscovered;
				if (action != null)
				{
					action(pickupIndex);
				}
				this.RequestEventualSave();
			}
		}

		// Token: 0x06003A90 RID: 14992 RVA: 0x000F2F3C File Offset: 0x000F113C
		[ConCommand(commandName = "user_profile_set_pickup_discovered", flags = ConVarFlags.Cheat, helpText = "Sets the pickup discovery state for the sender's profile.")]
		private static void CCUserProfileSetPickupDiscovered(ConCommandArgs args)
		{
			UserProfile userProfile = args.GetSenderLocalUser().userProfile;
			IEnumerable<PickupIndex> enumerable;
			if (args.TryGetArgString(0) == "all")
			{
				enumerable = PickupCatalog.allPickupIndices;
			}
			else
			{
				enumerable = new PickupIndex[]
				{
					args.GetArgPickupIndex(0)
				};
			}
			bool argBool = args.GetArgBool(1);
			foreach (PickupIndex pickupIndex in enumerable)
			{
				userProfile.SetPickupDiscovered(pickupIndex, argBool);
			}
		}

		// Token: 0x06003A91 RID: 14993 RVA: 0x000F2FD8 File Offset: 0x000F11D8
		public bool HasAchievement(string achievementName)
		{
			return this.achievementsList.Contains(achievementName);
		}

		// Token: 0x06003A92 RID: 14994 RVA: 0x000F2FE8 File Offset: 0x000F11E8
		public bool CanSeeAchievement(string achievementName)
		{
			if (this.HasAchievement(achievementName))
			{
				return true;
			}
			AchievementDef achievementDef = AchievementManager.GetAchievementDef(achievementName);
			return achievementDef != null && (string.IsNullOrEmpty(achievementDef.prerequisiteAchievementIdentifier) || this.HasAchievement(achievementDef.prerequisiteAchievementIdentifier));
		}

		// Token: 0x06003A93 RID: 14995 RVA: 0x000F3027 File Offset: 0x000F1227
		public void AddAchievement(string achievementName, bool isExternal)
		{
			this.achievementsList.Add(achievementName);
			this.unviewedAchievementsList.Add(achievementName);
			if (isExternal)
			{
				PlatformSystems.achievementSystem.AddAchievement(achievementName);
			}
			this.RequestEventualSave();
		}

		// Token: 0x06003A94 RID: 14996 RVA: 0x000F3055 File Offset: 0x000F1255
		public void RevokeAchievement(string achievementName)
		{
			this.achievementsList.Remove(achievementName);
			this.unviewedAchievementsList.Remove(achievementName);
			this.RequestEventualSave();
		}

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x06003A95 RID: 14997 RVA: 0x000F3077 File Offset: 0x000F1277
		public bool hasUnviewedAchievement
		{
			get
			{
				return this.unviewedAchievementsList.Count > 0;
			}
		}

		// Token: 0x06003A96 RID: 14998 RVA: 0x000F3087 File Offset: 0x000F1287
		public string PopNextUnviewedAchievementName()
		{
			if (this.unviewedAchievementsList.Count == 0)
			{
				return null;
			}
			string result = this.unviewedAchievementsList[0];
			this.unviewedAchievementsList.RemoveAt(0);
			return result;
		}

		// Token: 0x06003A97 RID: 14999 RVA: 0x000F30B0 File Offset: 0x000F12B0
		public void ApplyDeltaStatSheet(StatSheet deltaStatSheet)
		{
			int i = 0;
			int unlockableCount = deltaStatSheet.GetUnlockableCount();
			while (i < unlockableCount)
			{
				this.GrantUnlockable(deltaStatSheet.GetUnlockable(i));
				i++;
			}
			this.statSheet.ApplyDelta(deltaStatSheet);
			Action action = this.onStatsReceived;
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x06003A98 RID: 15000 RVA: 0x000F30FC File Offset: 0x000F12FC
		[ConCommand(commandName = "user_profile_stats_stress_test", flags = ConVarFlags.Cheat, helpText = "Sets the stats of the sender's user profile to the maximum their datatypes support for stress-testing purposes.")]
		private static void CCUserProfileStatsStressTest(ConCommandArgs args)
		{
			LocalUser senderLocalUser = args.GetSenderLocalUser();
			senderLocalUser.userProfile.statSheet.SetAllFieldsToMaxValue();
			PlatformSystems.saveSystem.Save(senderLocalUser.userProfile, true);
		}

		// Token: 0x06003A99 RID: 15001 RVA: 0x000F3133 File Offset: 0x000F1333
		private void ResetShouldShowTutorial(ref UserProfile.TutorialProgression tutorialProgression)
		{
			tutorialProgression.shouldShow = (tutorialProgression.showCount < 3U);
		}

		// Token: 0x06003A9A RID: 15002 RVA: 0x000F3144 File Offset: 0x000F1344
		private void RebuildTutorialProgressions()
		{
			this.ResetShouldShowTutorial(ref this.tutorialDifficulty);
			this.ResetShouldShowTutorial(ref this.tutorialSprint);
			this.ResetShouldShowTutorial(ref this.tutorialEquipment);
		}

		// Token: 0x06003A9B RID: 15003 RVA: 0x000F316A File Offset: 0x000F136A
		[NotNull]
		public SurvivorDef GetSurvivorPreference()
		{
			return this.survivorPreference;
		}

		// Token: 0x06003A9C RID: 15004 RVA: 0x000F3174 File Offset: 0x000F1374
		public void SetSurvivorPreference([NotNull] SurvivorDef newSurvivorPreference)
		{
			if (!newSurvivorPreference)
			{
				throw new ArgumentException("Provided object is null or invalid", "newSurvivorPreference");
			}
			if (this.survivorPreference == newSurvivorPreference)
			{
				return;
			}
			this.survivorPreference = newSurvivorPreference;
			Action action = this.onSurvivorPreferenceChanged;
			if (action != null)
			{
				action();
			}
			Action<UserProfile> action2 = UserProfile.onSurvivorPreferenceChangedGlobal;
			if (action2 == null)
			{
				return;
			}
			action2(this);
		}

		// Token: 0x140000CA RID: 202
		// (add) Token: 0x06003A9D RID: 15005 RVA: 0x000F31CC File Offset: 0x000F13CC
		// (remove) Token: 0x06003A9E RID: 15006 RVA: 0x000F3204 File Offset: 0x000F1404
		public event Action onSurvivorPreferenceChanged;

		// Token: 0x140000CB RID: 203
		// (add) Token: 0x06003A9F RID: 15007 RVA: 0x000F323C File Offset: 0x000F143C
		// (remove) Token: 0x06003AA0 RID: 15008 RVA: 0x000F3270 File Offset: 0x000F1470
		public static event Action<UserProfile> onSurvivorPreferenceChangedGlobal;

		// Token: 0x06003AA1 RID: 15009 RVA: 0x000F32A3 File Offset: 0x000F14A3
		private void OnLoadoutChanged()
		{
			Action action = this.onLoadoutChanged;
			if (action != null)
			{
				action();
			}
			Action<UserProfile> action2 = UserProfile.onLoadoutChangedGlobal;
			if (action2 != null)
			{
				action2(this);
			}
			this.RequestEventualSave();
		}

		// Token: 0x06003AA2 RID: 15010 RVA: 0x000F32CD File Offset: 0x000F14CD
		public void CopyLoadout(Loadout dest)
		{
			this.loadout.Copy(dest);
		}

		// Token: 0x06003AA3 RID: 15011 RVA: 0x000F32DB File Offset: 0x000F14DB
		public void SetLoadout(Loadout newLoadout)
		{
			if (this.loadout.ValueEquals(newLoadout))
			{
				return;
			}
			newLoadout.Copy(this.loadout);
			this.OnLoadoutChanged();
		}

		// Token: 0x140000CC RID: 204
		// (add) Token: 0x06003AA4 RID: 15012 RVA: 0x000F3300 File Offset: 0x000F1500
		// (remove) Token: 0x06003AA5 RID: 15013 RVA: 0x000F3338 File Offset: 0x000F1538
		public event Action onLoadoutChanged;

		// Token: 0x140000CD RID: 205
		// (add) Token: 0x06003AA6 RID: 15014 RVA: 0x000F3370 File Offset: 0x000F1570
		// (remove) Token: 0x06003AA7 RID: 15015 RVA: 0x000F33A4 File Offset: 0x000F15A4
		public static event Action<UserProfile> onLoadoutChangedGlobal;

		// Token: 0x06003AA8 RID: 15016 RVA: 0x000F33D8 File Offset: 0x000F15D8
		[ConCommand(commandName = "loadout_set_skill_variant", flags = (ConVarFlags.ExecuteOnServer | ConVarFlags.Cheat), helpText = "loadout_set_skill_variant [body_name] [skill_slot_index] [skill_variant_index]\nSets the skill variant for the sender's user profile.")]
		private static void CCLoadoutSetSkillVariant(ConCommandArgs args)
		{
			BodyIndex argBodyIndex = args.GetArgBodyIndex(0);
			int argInt = args.GetArgInt(1);
			int argInt2 = args.GetArgInt(2);
			UserProfile userProfile = args.GetSenderLocalUser().userProfile;
			Loadout loadout = new Loadout();
			userProfile.loadout.Copy(loadout);
			loadout.bodyLoadoutManager.SetSkillVariant(argBodyIndex, argInt, (uint)argInt2);
			userProfile.SetLoadout(loadout);
			if (args.senderMaster)
			{
				args.senderMaster.SetLoadoutServer(loadout);
			}
			if (args.senderBody)
			{
				args.senderBody.SetLoadoutServer(loadout);
			}
		}

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x06003AA9 RID: 15017 RVA: 0x000F3467 File Offset: 0x000F1667
		// (set) Token: 0x06003AAA RID: 15018 RVA: 0x000F346F File Offset: 0x000F166F
		public bool loggedIn { get; set; }

		// Token: 0x06003AAB RID: 15019 RVA: 0x000F3478 File Offset: 0x000F1678
		public void OnLogin()
		{
			if (this.loggedIn)
			{
				Debug.LogErrorFormat("Profile {0} is already logged in!", new object[]
				{
					this.fileName
				});
				return;
			}
			this.loggedIn = true;
			PlatformSystems.saveSystem.loggedInProfiles.Add(this);
			this.RebuildTutorialProgressions();
			foreach (string achievementName in this.achievementsList)
			{
				PlatformSystems.achievementSystem.AddAchievement(achievementName);
			}
			this.loadout.EnforceUnlockables(this);
			this.LoadPortrait();
		}

		// Token: 0x06003AAC RID: 15020 RVA: 0x000F3520 File Offset: 0x000F1720
		public void OnLogout()
		{
			if (!this.loggedIn)
			{
				Debug.LogErrorFormat("Profile {0} is already logged out!", new object[]
				{
					this.fileName
				});
				return;
			}
			this.UnloadPortrait();
			this.loggedIn = false;
			PlatformSystems.saveSystem.Save(this, true);
			PlatformSystems.saveSystem.loggedInProfiles.Remove(this);
		}

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x06003AAD RID: 15021 RVA: 0x000F357A File Offset: 0x000F177A
		// (set) Token: 0x06003AAE RID: 15022 RVA: 0x000F3582 File Offset: 0x000F1782
		public Texture portraitTexture { get; private set; }

		// Token: 0x06003AAF RID: 15023 RVA: 0x000F358C File Offset: 0x000F178C
		private void LoadPortrait()
		{
			UserProfile.<>c__DisplayClass107_0 CS$<>8__locals1 = new UserProfile.<>c__DisplayClass107_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.newPortraitTexture = new Texture2D(184, 184, TextureFormat.ARGB32, false, false);
			this.portraitTexture = CS$<>8__locals1.newPortraitTexture;
			this.ownsPortrait = true;
			Client.Instance.Friends.GetAvatar(Friends.AvatarSize.Large, Client.Instance.SteamId, new Action<Image>(CS$<>8__locals1.<LoadPortrait>g__LoadSteamImage|0));
		}

		// Token: 0x06003AB0 RID: 15024 RVA: 0x000F35F7 File Offset: 0x000F17F7
		private void UnloadPortrait()
		{
			if (this.ownsPortrait)
			{
				UnityEngine.Object.Destroy(this.portraitTexture);
				this.portraitTexture = null;
				this.ownsPortrait = false;
			}
		}

		// Token: 0x140000CE RID: 206
		// (add) Token: 0x06003AB1 RID: 15025 RVA: 0x000F361C File Offset: 0x000F181C
		// (remove) Token: 0x06003AB2 RID: 15026 RVA: 0x000F3650 File Offset: 0x000F1850
		public static event Action<UserProfile, UnlockableDef> onUnlockableGranted;

		// Token: 0x06003AB3 RID: 15027 RVA: 0x000F3683 File Offset: 0x000F1883
		public static void LoadDefaultProfile()
		{
			UserProfile.defaultProfile = XmlUtility.FromXml(XDocument.Parse("<UserProfile>\r\n  <name>Survivor</name>\r\n  <mouseLookSensitivity>0.2</mouseLookSensitivity>\r\n  <mouseLookScaleX>1</mouseLookScaleX>\r\n  <mouseLookScaleY>1</mouseLookScaleY>\r\n  <stickLookSensitivity>5</stickLookSensitivity>\r\n  <stickLookScaleX>1</stickLookScaleX>\r\n  <stickLookScaleY>1</stickLookScaleY>\r\n</UserProfile>"));
			UserProfile.defaultProfile.canSave = false;
		}

		// Token: 0x06003AB4 RID: 15028 RVA: 0x000F36A4 File Offset: 0x000F18A4
		public bool HasViewedViewable(string viewableName)
		{
			return this.viewedViewables.Contains(viewableName);
		}

		// Token: 0x06003AB5 RID: 15029 RVA: 0x000F36B2 File Offset: 0x000F18B2
		public void MarkViewableAsViewed(string viewableName)
		{
			if (this.HasViewedViewable(viewableName))
			{
				return;
			}
			this.viewedViewables.Add(viewableName);
			Action<UserProfile> action = UserProfile.onUserProfileViewedViewablesChanged;
			if (action != null)
			{
				action(this);
			}
			this.RequestEventualSave();
		}

		// Token: 0x06003AB6 RID: 15030 RVA: 0x000F36E1 File Offset: 0x000F18E1
		public void ClearAllViewablesAsViewed()
		{
			this.viewedViewables.Clear();
			Action<UserProfile> action = UserProfile.onUserProfileViewedViewablesChanged;
			if (action != null)
			{
				action(this);
			}
			this.RequestEventualSave();
		}

		// Token: 0x140000CF RID: 207
		// (add) Token: 0x06003AB7 RID: 15031 RVA: 0x000F3708 File Offset: 0x000F1908
		// (remove) Token: 0x06003AB8 RID: 15032 RVA: 0x000F373C File Offset: 0x000F193C
		public static event Action<UserProfile> onUserProfileViewedViewablesChanged;

		// Token: 0x0400394F RID: 14671
		public bool isClaimed;

		// Token: 0x04003950 RID: 14672
		public bool canSave;

		// Token: 0x04003951 RID: 14673
		public string fileName;

		// Token: 0x04003952 RID: 14674
		public IFileSystem fileSystem;

		// Token: 0x04003953 RID: 14675
		public UPath filePath = UPath.Empty;

		// Token: 0x04003954 RID: 14676
		[SaveField]
		public string name;

		// Token: 0x04003955 RID: 14677
		[SaveField]
		public uint coins;

		// Token: 0x04003956 RID: 14678
		[SaveField]
		public uint totalCollectedCoins;

		// Token: 0x04003958 RID: 14680
		[SaveField]
		public string version = "2";

		// Token: 0x04003959 RID: 14681
		[SaveField]
		public float screenShakeScale = 1f;

		// Token: 0x0400395A RID: 14682
		[SaveField(explicitSetupMethod = "SetupKeyboardMap")]
		public KeyboardMap keyboardMap = new KeyboardMap(DefaultControllerMaps.defaultKeyboardMap);

		// Token: 0x0400395B RID: 14683
		[SaveField(explicitSetupMethod = "SetupMouseMap")]
		public MouseMap mouseMap = new MouseMap(DefaultControllerMaps.defaultMouseMap);

		// Token: 0x0400395C RID: 14684
		[SaveField(explicitSetupMethod = "SetupJoystickMap")]
		public JoystickMap joystickMap = new JoystickMap(DefaultControllerMaps.defaultJoystickMap);

		// Token: 0x0400395D RID: 14685
		[SaveField]
		public float mouseLookSensitivity = 0.25f;

		// Token: 0x0400395E RID: 14686
		[SaveField]
		public float mouseLookScaleX = 1f;

		// Token: 0x0400395F RID: 14687
		[SaveField]
		public float mouseLookScaleY = 1f;

		// Token: 0x04003960 RID: 14688
		[SaveField]
		public bool mouseLookInvertX;

		// Token: 0x04003961 RID: 14689
		[SaveField]
		public bool mouseLookInvertY;

		// Token: 0x04003962 RID: 14690
		[SaveField]
		public float stickLookSensitivity = 4f;

		// Token: 0x04003963 RID: 14691
		[SaveField]
		public float stickLookScaleX = 1f;

		// Token: 0x04003964 RID: 14692
		[SaveField]
		public float stickLookScaleY = 1f;

		// Token: 0x04003965 RID: 14693
		[SaveField]
		public bool stickLookInvertX;

		// Token: 0x04003966 RID: 14694
		[SaveField]
		public bool stickLookInvertY;

		// Token: 0x04003967 RID: 14695
		[SaveField]
		public float gamepadVibrationScale = 1f;

		// Token: 0x04003968 RID: 14696
		public bool saveRequestPending;

		// Token: 0x04003969 RID: 14697
		private static string[] saveFieldNames;

		// Token: 0x0400396A RID: 14698
		public static SaveFieldAttribute[] saveFields;

		// Token: 0x0400396B RID: 14699
		private static readonly Dictionary<string, SaveFieldAttribute> nameToSaveFieldMap = new Dictionary<string, SaveFieldAttribute>();

		// Token: 0x0400396C RID: 14700
		public static UserProfile defaultProfile;

		// Token: 0x0400396D RID: 14701
		[SaveField(explicitSetupMethod = "SetupTokenList")]
		public List<string> viewedUnlockablesList = new List<string>();

		// Token: 0x0400396E RID: 14702
		[SaveField(explicitSetupMethod = "SetupPickupsSet")]
		private readonly bool[] discoveredPickups = PickupCatalog.GetPerPickupBuffer<bool>();

		// Token: 0x0400396F RID: 14703
		public Action<PickupIndex> onPickupDiscovered;

		// Token: 0x04003970 RID: 14704
		public Action onStatsReceived;

		// Token: 0x04003971 RID: 14705
		[SaveField(explicitSetupMethod = "SetupTokenList")]
		private List<string> achievementsList = new List<string>();

		// Token: 0x04003972 RID: 14706
		[SaveField(explicitSetupMethod = "SetupTokenList")]
		private List<string> unviewedAchievementsList = new List<string>();

		// Token: 0x04003973 RID: 14707
		public StatSheet statSheet = StatSheet.New();

		// Token: 0x04003974 RID: 14708
		private const uint maxShowCount = 3U;

		// Token: 0x04003975 RID: 14709
		public UserProfile.TutorialProgression tutorialDifficulty;

		// Token: 0x04003976 RID: 14710
		public UserProfile.TutorialProgression tutorialSprint;

		// Token: 0x04003977 RID: 14711
		public UserProfile.TutorialProgression tutorialEquipment;

		// Token: 0x04003978 RID: 14712
		[SaveField]
		private SurvivorDef survivorPreference = SurvivorCatalog.defaultSurvivor;

		// Token: 0x0400397B RID: 14715
		public readonly Loadout loadout = new Loadout();

		// Token: 0x0400397E RID: 14718
		[SaveField]
		public uint totalLoginSeconds;

		// Token: 0x0400397F RID: 14719
		[SaveField]
		public uint totalRunSeconds;

		// Token: 0x04003980 RID: 14720
		[SaveField]
		public uint totalAliveSeconds;

		// Token: 0x04003981 RID: 14721
		[SaveField]
		public uint totalRunCount;

		// Token: 0x04003984 RID: 14724
		private bool ownsPortrait;

		// Token: 0x04003986 RID: 14726
		private const string defaultProfileContents = "<UserProfile>\r\n  <name>Survivor</name>\r\n  <mouseLookSensitivity>0.2</mouseLookSensitivity>\r\n  <mouseLookScaleX>1</mouseLookScaleX>\r\n  <mouseLookScaleY>1</mouseLookScaleY>\r\n  <stickLookSensitivity>5</stickLookSensitivity>\r\n  <stickLookScaleX>1</stickLookScaleX>\r\n  <stickLookScaleY>1</stickLookScaleY>\r\n</UserProfile>";

		// Token: 0x04003987 RID: 14727
		[SaveField(defaultValue = "", explicitSetupMethod = "SetupTokenList", fieldName = "viewedViewables")]
		private readonly List<string> viewedViewables = new List<string>();

		// Token: 0x020009E9 RID: 2537
		public struct TutorialProgression
		{
			// Token: 0x04003989 RID: 14729
			public uint showCount;

			// Token: 0x0400398A RID: 14730
			public bool shouldShow;
		}
	}
}
