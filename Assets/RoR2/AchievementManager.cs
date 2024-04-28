using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;
using RoR2.Achievements;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020004BB RID: 1211
	public static class AchievementManager
	{
		// Token: 0x060015EE RID: 5614 RVA: 0x00061120 File Offset: 0x0005F320
		public static UserAchievementManager GetUserAchievementManager([NotNull] LocalUser user)
		{
			UserAchievementManager result;
			AchievementManager.userToManagerMap.TryGetValue(user, out result);
			return result;
		}

		// Token: 0x060015EF RID: 5615 RVA: 0x0006113C File Offset: 0x0005F33C
		[SystemInitializer(new Type[]
		{
			typeof(UnlockableCatalog)
		})]
		private static void DoInit()
		{
			AchievementManager.CollectAchievementDefs(AchievementManager.achievementNamesToDefs);
			LocalUserManager.onUserSignIn += delegate(LocalUser localUser)
			{
				if (!localUser.userProfile.canSave)
				{
					return;
				}
				UserAchievementManager userAchievementManager = new UserAchievementManager();
				userAchievementManager.OnInstall(localUser);
				AchievementManager.userToManagerMap[localUser] = userAchievementManager;
			};
			LocalUserManager.onUserSignOut += delegate(LocalUser localUser)
			{
				UserAchievementManager userAchievementManager;
				if (AchievementManager.userToManagerMap.TryGetValue(localUser, out userAchievementManager))
				{
					userAchievementManager.OnUninstall();
					AchievementManager.userToManagerMap.Remove(localUser);
				}
			};
			RoR2Application.onUpdate += delegate()
			{
				foreach (KeyValuePair<LocalUser, UserAchievementManager> keyValuePair in AchievementManager.userToManagerMap)
				{
					keyValuePair.Value.Update();
				}
			};
			AchievementManager.availability.MakeAvailable();
		}

		// Token: 0x060015F0 RID: 5616 RVA: 0x000611C9 File Offset: 0x0005F3C9
		public static void AddTask(Action action)
		{
			AchievementManager.taskQueue.Enqueue(action);
		}

		// Token: 0x060015F1 RID: 5617 RVA: 0x000611D6 File Offset: 0x0005F3D6
		public static void ProcessTasks()
		{
			while (AchievementManager.taskQueue.Count > 0)
			{
				AchievementManager.taskQueue.Dequeue()();
			}
		}

		// Token: 0x060015F2 RID: 5618 RVA: 0x000611F8 File Offset: 0x0005F3F8
		public static AchievementDef GetAchievementDef(string achievementIdentifier)
		{
			AchievementDef result;
			if (AchievementManager.achievementNamesToDefs.TryGetValue(achievementIdentifier, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x060015F3 RID: 5619 RVA: 0x00061217 File Offset: 0x0005F417
		public static AchievementDef GetAchievementDef(AchievementIndex index)
		{
			if (index.intValue >= 0 && index.intValue < AchievementManager.achievementDefs.Length)
			{
				return AchievementManager.achievementDefs[index.intValue];
			}
			return null;
		}

		// Token: 0x060015F4 RID: 5620 RVA: 0x0006123F File Offset: 0x0005F43F
		public static AchievementDef GetAchievementDef(ServerAchievementIndex index)
		{
			if (index.intValue >= 0 && index.intValue < AchievementManager.serverAchievementDefs.Length)
			{
				return AchievementManager.serverAchievementDefs[index.intValue];
			}
			return null;
		}

		// Token: 0x060015F5 RID: 5621 RVA: 0x00061268 File Offset: 0x0005F468
		public static AchievementDef GetAchievementDefFromUnlockable(string unlockableRewardIdentifier)
		{
			for (int i = 0; i < AchievementManager.achievementDefs.Length; i++)
			{
				if (AchievementManager.achievementDefs[i].unlockableRewardIdentifier == unlockableRewardIdentifier)
				{
					return AchievementManager.achievementDefs[i];
				}
			}
			return null;
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060015F6 RID: 5622 RVA: 0x000612A4 File Offset: 0x0005F4A4
		public static int achievementCount
		{
			get
			{
				return AchievementManager.achievementDefs.Length;
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060015F7 RID: 5623 RVA: 0x000612AD File Offset: 0x0005F4AD
		public static int serverAchievementCount
		{
			get
			{
				return AchievementManager.serverAchievementDefs.Length;
			}
		}

		// Token: 0x060015F8 RID: 5624 RVA: 0x000612B8 File Offset: 0x0005F4B8
		public static void CollectAchievementDefs(Dictionary<string, AchievementDef> map)
		{
			List<AchievementDef> list = new List<AchievementDef>();
			map.Clear();
			List<Assembly> list2 = new List<Assembly>();
			if (RoR2Application.isModded)
			{
				foreach (Assembly item in AppDomain.CurrentDomain.GetAssemblies())
				{
					list2.Add(item);
				}
			}
			else
			{
				list2.Add(typeof(BaseAchievement).Assembly);
			}
			foreach (Assembly assembly in list2)
			{
				Type[] types;
				try
				{
					types = assembly.GetTypes();
				}
				catch (ReflectionTypeLoadException ex)
				{
					Debug.LogError(string.Format("CollectAchievementDefs:  {0}", ex));
					types = ex.Types;
					if (types == null)
					{
						continue;
					}
				}
				catch (Exception arg)
				{
					Debug.LogError(string.Format("CollectAchievementDefs:  {0}", arg));
					continue;
				}
				foreach (Type type2 in from type in types
				where type != null && type.IsSubclassOf(typeof(BaseAchievement))
				orderby type.Name
				select type)
				{
					RegisterAchievementAttribute registerAchievementAttribute = (RegisterAchievementAttribute)type2.GetCustomAttributes(false).FirstOrDefault((object v) => v is RegisterAchievementAttribute);
					if (registerAchievementAttribute != null)
					{
						if (map.ContainsKey(registerAchievementAttribute.identifier))
						{
							Debug.LogErrorFormat("Class {0} attempted to register as achievement {1}, but class {2} has already registered as that achievement.", new object[]
							{
								type2.FullName,
								registerAchievementAttribute.identifier,
								AchievementManager.achievementNamesToDefs[registerAchievementAttribute.identifier].type.FullName
							});
						}
						else
						{
							UnlockableDef unlockableDef = UnlockableCatalog.GetUnlockableDef(registerAchievementAttribute.unlockableRewardIdentifier);
							AchievementDef achievementDef = new AchievementDef
							{
								identifier = registerAchievementAttribute.identifier,
								unlockableRewardIdentifier = registerAchievementAttribute.unlockableRewardIdentifier,
								prerequisiteAchievementIdentifier = registerAchievementAttribute.prerequisiteAchievementIdentifier,
								nameToken = "ACHIEVEMENT_" + registerAchievementAttribute.identifier.ToUpper(CultureInfo.InvariantCulture) + "_NAME",
								descriptionToken = "ACHIEVEMENT_" + registerAchievementAttribute.identifier.ToUpper(CultureInfo.InvariantCulture) + "_DESCRIPTION",
								type = type2,
								serverTrackerType = registerAchievementAttribute.serverTrackerType
							};
							if (unlockableDef && unlockableDef.achievementIcon)
							{
								achievementDef.SetAchievedIcon(unlockableDef.achievementIcon);
							}
							else
							{
								achievementDef.iconPath = "Textures/AchievementIcons/tex" + registerAchievementAttribute.identifier + "Icon";
							}
							AchievementManager.achievementIdentifiers.Add(registerAchievementAttribute.identifier);
							map.Add(registerAchievementAttribute.identifier, achievementDef);
							list.Add(achievementDef);
							if (unlockableDef != null)
							{
								unlockableDef.getHowToUnlockString = (() => Language.GetStringFormatted("UNLOCK_VIA_ACHIEVEMENT_FORMAT", new object[]
								{
									Language.GetString(achievementDef.nameToken),
									Language.GetString(achievementDef.descriptionToken)
								}));
								unlockableDef.getUnlockedString = (() => Language.GetStringFormatted("UNLOCKED_FORMAT", new object[]
								{
									Language.GetString(achievementDef.nameToken),
									Language.GetString(achievementDef.descriptionToken)
								}));
							}
						}
					}
				}
			}
			AchievementManager.achievementDefs = list.ToArray();
			AchievementManager.SortAchievements(AchievementManager.achievementDefs);
			AchievementManager.serverAchievementDefs = (from achievementDef in AchievementManager.achievementDefs
			where achievementDef.serverTrackerType != null
			select achievementDef).ToArray<AchievementDef>();
			for (int j = 0; j < AchievementManager.achievementDefs.Length; j++)
			{
				AchievementManager.achievementDefs[j].index = new AchievementIndex
				{
					intValue = j
				};
			}
			for (int k = 0; k < AchievementManager.serverAchievementDefs.Length; k++)
			{
				AchievementManager.serverAchievementDefs[k].serverIndex = new ServerAchievementIndex
				{
					intValue = k
				};
			}
			for (int l = 0; l < AchievementManager.achievementIdentifiers.Count; l++)
			{
				string currentAchievementIdentifier = AchievementManager.achievementIdentifiers[l];
				map[currentAchievementIdentifier].childAchievementIdentifiers = (from v in AchievementManager.achievementIdentifiers
				where map[v].prerequisiteAchievementIdentifier == currentAchievementIdentifier
				select v).ToArray<string>();
			}
			Action action = AchievementManager.onAchievementsRegistered;
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x060015F9 RID: 5625 RVA: 0x000617B8 File Offset: 0x0005F9B8
		private static void SortAchievements(AchievementDef[] achievementDefsArray)
		{
			AchievementManager.AchievementSortPair[] array = new AchievementManager.AchievementSortPair[achievementDefsArray.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new AchievementManager.AchievementSortPair
				{
					score = UnlockableCatalog.GetUnlockableSortScore(achievementDefsArray[i].unlockableRewardIdentifier),
					achievementDef = achievementDefsArray[i]
				};
			}
			Array.Sort<AchievementManager.AchievementSortPair>(array, (AchievementManager.AchievementSortPair a, AchievementManager.AchievementSortPair b) => a.score - b.score);
			for (int j = 0; j < array.Length; j++)
			{
				achievementDefsArray[j] = array[j].achievementDef;
			}
		}

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x060015FA RID: 5626 RVA: 0x0006184C File Offset: 0x0005FA4C
		// (remove) Token: 0x060015FB RID: 5627 RVA: 0x00061880 File Offset: 0x0005FA80
		public static event Action onAchievementsRegistered;

		// Token: 0x060015FC RID: 5628 RVA: 0x000618B4 File Offset: 0x0005FAB4
		public static AchievementManager.Enumerator GetEnumerator()
		{
			return default(AchievementManager.Enumerator);
		}

		// Token: 0x04001BB0 RID: 7088
		private static readonly Dictionary<LocalUser, UserAchievementManager> userToManagerMap = new Dictionary<LocalUser, UserAchievementManager>();

		// Token: 0x04001BB1 RID: 7089
		public static ResourceAvailability availability;

		// Token: 0x04001BB2 RID: 7090
		private static readonly Queue<Action> taskQueue = new Queue<Action>();

		// Token: 0x04001BB3 RID: 7091
		private static readonly Dictionary<string, AchievementDef> achievementNamesToDefs = new Dictionary<string, AchievementDef>();

		// Token: 0x04001BB4 RID: 7092
		private static readonly List<string> achievementIdentifiers = new List<string>();

		// Token: 0x04001BB5 RID: 7093
		public static readonly ReadOnlyCollection<string> readOnlyAchievementIdentifiers = AchievementManager.achievementIdentifiers.AsReadOnly();

		// Token: 0x04001BB6 RID: 7094
		private static AchievementDef[] achievementDefs;

		// Token: 0x04001BB7 RID: 7095
		private static AchievementDef[] serverAchievementDefs;

		// Token: 0x04001BB9 RID: 7097
		public static readonly GenericStaticEnumerable<AchievementDef, AchievementManager.Enumerator> allAchievementDefs;

		// Token: 0x020004BC RID: 1212
		private struct AchievementSortPair
		{
			// Token: 0x04001BBA RID: 7098
			public int score;

			// Token: 0x04001BBB RID: 7099
			public AchievementDef achievementDef;
		}

		// Token: 0x020004BD RID: 1213
		public struct Enumerator : IEnumerator<AchievementDef>, IEnumerator, IDisposable
		{
			// Token: 0x060015FE RID: 5630 RVA: 0x00061903 File Offset: 0x0005FB03
			public bool MoveNext()
			{
				this.position++;
				return this.position < AchievementManager.achievementDefs.Length;
			}

			// Token: 0x060015FF RID: 5631 RVA: 0x00061922 File Offset: 0x0005FB22
			public void Reset()
			{
				this.position = -1;
			}

			// Token: 0x1700014D RID: 333
			// (get) Token: 0x06001600 RID: 5632 RVA: 0x0006192B File Offset: 0x0005FB2B
			public AchievementDef Current
			{
				get
				{
					return AchievementManager.achievementDefs[this.position];
				}
			}

			// Token: 0x1700014E RID: 334
			// (get) Token: 0x06001601 RID: 5633 RVA: 0x00061939 File Offset: 0x0005FB39
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06001602 RID: 5634 RVA: 0x000026ED File Offset: 0x000008ED
			void IDisposable.Dispose()
			{
			}

			// Token: 0x04001BBC RID: 7100
			private int position;
		}
	}
}
