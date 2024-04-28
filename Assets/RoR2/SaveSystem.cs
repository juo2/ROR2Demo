using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Facepunch.Steamworks;
using JetBrains.Annotations;
using RoR2.Stats;
using UnityEngine;
using Zio;
using Zio.FileSystems;

namespace RoR2
{
	// Token: 0x020009C4 RID: 2500
	public abstract class SaveSystem : SaveSystemBase
	{
		// Token: 0x06003920 RID: 14624 RVA: 0x000EE764 File Offset: 0x000EC964
		public static void SkipBOM(Stream stream)
		{
			long position = stream.Position;
			if (stream.Length - position < 3L)
			{
				return;
			}
			int num = stream.ReadByte();
			int num2 = stream.ReadByte();
			if (num == 255 && num2 == 254)
			{
				Debug.Log("Skipping UTF-8 BOM");
				return;
			}
			int num3 = stream.ReadByte();
			if (num == 239 && num2 == 187 && num3 == 191)
			{
				Debug.Log("Skipping UTF-16 BOM");
				return;
			}
			stream.Position = position;
		}

		// Token: 0x06003921 RID: 14625 RVA: 0x000EE7DF File Offset: 0x000EC9DF
		public virtual void InitializeSaveSystem()
		{
			Debug.Log("Should call child class initialize");
		}

		// Token: 0x06003922 RID: 14626 RVA: 0x000EE7EC File Offset: 0x000EC9EC
		protected bool CanWrite(FileOutput fileOutput)
		{
			if (fileOutput.contents.Length == 0)
			{
				Debug.LogErrorFormat("Cannot write UserProfile \"{0}\" with zero-length contents. This would erase the file.", Array.Empty<object>());
				return false;
			}
			DateTime t;
			return !this.latestWrittenRequestTimesByFile.TryGetValue(fileOutput.fileReference, out t) || t < fileOutput.requestTime;
		}

		// Token: 0x06003923 RID: 14627 RVA: 0x000EE838 File Offset: 0x000ECA38
		protected UserProfile AttemptToRecoverUserData(string profileXML)
		{
			string value = "<a";
			string value2 = "<c";
			int num = profileXML.IndexOf(value);
			int num2 = profileXML.IndexOf(value2);
			string[] array = null;
			string text = null;
			if (num != -1 && num + 1 < profileXML.Length)
			{
				int num3 = profileXML.IndexOf("<", num + 1);
				int num4 = profileXML.IndexOf(">", num);
				if (num4 != -1)
				{
					if (num3 == -1)
					{
						num3 = profileXML.Length - 1;
					}
					else
					{
						num3--;
					}
					string text2 = profileXML.Substring(num4 + 1, num3 - num4);
					array = text2.Split(new char[]
					{
						' '
					}, StringSplitOptions.RemoveEmptyEntries);
					Debug.LogError("Recovered achievements: " + text2);
				}
			}
			if (num2 != -1 && num2 + 1 < profileXML.Length)
			{
				int num5 = profileXML.IndexOf("<", num2 + 1);
				int num6 = profileXML.IndexOf(">", num2);
				if (num6 != -1)
				{
					if (num5 == -1)
					{
						num5 = profileXML.Length - 1;
					}
					else
					{
						num5--;
					}
					text = profileXML.Substring(num6 + 1, num5 - num6);
					Debug.LogError("Recovered coins: " + text);
				}
			}
			UserProfile userProfile = this.CreateGuestProfile();
			if (array != null && array.Length != 0)
			{
				foreach (string achievementName in array)
				{
					userProfile.AddAchievement(achievementName, true);
				}
			}
			else
			{
				Debug.LogError("XML didn't contain achievements!");
			}
			uint coins = 0U;
			if (text != null && uint.TryParse(text, out coins))
			{
				userProfile.coins = coins;
			}
			else
			{
				Debug.LogError("XML didn't contain coins!");
			}
			return userProfile;
		}

		// Token: 0x140000C7 RID: 199
		// (add) Token: 0x06003924 RID: 14628 RVA: 0x000EE9C0 File Offset: 0x000ECBC0
		// (remove) Token: 0x06003925 RID: 14629 RVA: 0x000EE9F4 File Offset: 0x000ECBF4
		public static event Action onAvailableUserProfilesChanged;

		// Token: 0x06003926 RID: 14630 RVA: 0x000EEA28 File Offset: 0x000ECC28
		protected void AddActiveTask(Task task)
		{
			List<Task> obj = this.activeTasks;
			lock (obj)
			{
				this.activeTasks.Add(task);
			}
		}

		// Token: 0x06003927 RID: 14631 RVA: 0x000EEA70 File Offset: 0x000ECC70
		protected void RemoveActiveTask(Task task)
		{
			List<Task> obj = this.activeTasks;
			lock (obj)
			{
				this.activeTasks.Remove(task);
			}
		}

		// Token: 0x06003928 RID: 14632 RVA: 0x000EEAB8 File Offset: 0x000ECCB8
		public void StaticUpdate()
		{
			SaveSystem.secondAccumulator += Time.unscaledDeltaTime;
			if (SaveSystem.secondAccumulator > 1f)
			{
				SaveSystem.secondAccumulator -= 1f;
				foreach (UserProfile userProfile in this.loggedInProfiles)
				{
					userProfile.totalLoginSeconds += 1U;
				}
			}
			foreach (UserProfile userProfile2 in this.loggedInProfiles)
			{
				if (userProfile2.saveRequestPending && this.Save(userProfile2, false))
				{
					userProfile2.saveRequestPending = false;
				}
			}
			this.ProcessFileOutputQueue();
		}

		// Token: 0x06003929 RID: 14633
		public abstract void SaveHistory(byte[] data, string fileName);

		// Token: 0x0600392A RID: 14634
		public abstract Dictionary<string, byte[]> LoadHistory();

		// Token: 0x0600392B RID: 14635 RVA: 0x000EEB98 File Offset: 0x000ECD98
		public void RequestSave(UserProfile profile, bool immediate = false)
		{
			if (!profile.canSave)
			{
				return;
			}
			if (immediate)
			{
				this.Save(profile, true);
				return;
			}
			profile.saveRequestPending = true;
		}

		// Token: 0x0600392C RID: 14636 RVA: 0x000EEBB8 File Offset: 0x000ECDB8
		public bool Save(UserProfile data, bool blocking)
		{
			bool result;
			try
			{
				this.StartSave(data, blocking);
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x0600392D RID: 14637 RVA: 0x000EEBE8 File Offset: 0x000ECDE8
		public void AddLoadedUserProfile(string name, UserProfile profile)
		{
			if (profile == null)
			{
				return;
			}
			if (!this.loadedUserProfiles.ContainsKey(name))
			{
				this.loadedUserProfiles.Add(name, profile);
			}
		}

		// Token: 0x0600392E RID: 14638 RVA: 0x000EEC0C File Offset: 0x000ECE0C
		public List<string> GetAvailableProfileNames()
		{
			List<string> list = new List<string>();
			foreach (KeyValuePair<string, UserProfile> keyValuePair in this.loadedUserProfiles)
			{
				if (!keyValuePair.Value.isClaimed)
				{
					list.Add(keyValuePair.Key);
				}
			}
			list.Sort();
			return list;
		}

		// Token: 0x0600392F RID: 14639 RVA: 0x000EEC80 File Offset: 0x000ECE80
		public UserProfile GetProfile(string profileName)
		{
			profileName = profileName.ToLower(CultureInfo.InvariantCulture);
			UserProfile result;
			if (this.loadedUserProfiles.TryGetValue(profileName, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06003930 RID: 14640 RVA: 0x000EECB0 File Offset: 0x000ECEB0
		public virtual UserProfile CreateProfile(IFileSystem fileSystem, string name)
		{
			UserProfile userProfile = UserProfile.FromXml(UserProfile.ToXml(UserProfile.defaultProfile));
			this.PlatformInitProfile(ref userProfile, fileSystem, name);
			this.loadedUserProfiles.Add(userProfile.fileName, userProfile);
			this.Save(userProfile, true);
			Action action = SaveSystem.onAvailableUserProfilesChanged;
			if (action != null)
			{
				action();
			}
			return userProfile;
		}

		// Token: 0x06003931 RID: 14641 RVA: 0x000EED04 File Offset: 0x000ECF04
		protected virtual void PlatformInitProfile(ref UserProfile newProfile, IFileSystem fileSystem, string name)
		{
			newProfile.fileName = Guid.NewGuid().ToString();
			newProfile.fileSystem = fileSystem;
			newProfile.filePath = "/UserProfiles/" + newProfile.fileName + ".xml";
			newProfile.name = name;
			newProfile.canSave = true;
		}

		// Token: 0x06003932 RID: 14642 RVA: 0x000EED68 File Offset: 0x000ECF68
		public UserProfile CreateGuestProfile()
		{
			UserProfile userProfile = new UserProfile();
			SaveSystem.Copy(UserProfile.defaultProfile, userProfile);
			userProfile.name = "Guest";
			return userProfile;
		}

		// Token: 0x06003933 RID: 14643 RVA: 0x000EED94 File Offset: 0x000ECF94
		public void LoadUserProfiles()
		{
			this.badFileResults.Clear();
			this.loadedUserProfiles.Clear();
			UserProfile.LoadDefaultProfile();
			FileSystem cloudStorage = RoR2Application.cloudStorage;
			if (cloudStorage == null)
			{
				Debug.LogError("cloud storage is null");
				return;
			}
			if (!cloudStorage.DirectoryExists("/UserProfiles"))
			{
				cloudStorage.CreateDirectory("/UserProfiles");
			}
			foreach (UPath path in cloudStorage.EnumeratePaths("/UserProfiles"))
			{
				if (cloudStorage.FileExists(path) && string.CompareOrdinal(path.GetExtensionWithDot(), ".xml") == 0)
				{
					LoadUserProfileOperationResult loadUserProfileOperationResult = this.LoadUserProfileFromDisk(cloudStorage, path);
					UserProfile userProfile = loadUserProfileOperationResult.userProfile;
					if (userProfile != null)
					{
						this.loadedUserProfiles[userProfile.fileName] = userProfile;
					}
					if (loadUserProfileOperationResult.exception != null)
					{
						this.badFileResults.Add(loadUserProfileOperationResult);
					}
				}
			}
			this.OutputBadFileResults();
			Action action = SaveSystem.onAvailableUserProfilesChanged;
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x06003934 RID: 14644 RVA: 0x000EEEA8 File Offset: 0x000ED0A8
		private void OutputBadFileResults()
		{
			if (this.badFileResults.Count == 0)
			{
				return;
			}
			try
			{
				using (Stream stream = RoR2Application.fileSystem.CreateFile(new UPath("/bad_profiles.log")))
				{
					using (TextWriter textWriter = new StreamWriter(stream))
					{
						foreach (LoadUserProfileOperationResult loadUserProfileOperationResult in this.badFileResults)
						{
							textWriter.WriteLine("Failed to load file \"{0}\" ({1}B)", loadUserProfileOperationResult.fileName, loadUserProfileOperationResult.fileLength);
							textWriter.WriteLine("Exception: {0}", loadUserProfileOperationResult.exception);
							textWriter.Write("Base64 Contents: ");
							textWriter.WriteLine(loadUserProfileOperationResult.failureContents ?? string.Empty);
							textWriter.WriteLine(string.Empty);
						}
					}
				}
			}
			catch (Exception ex)
			{
				Debug.LogFormat("Could not write bad UserProfile load log! Reason: {0}", new object[]
				{
					ex.Message
				});
			}
		}

		// Token: 0x06003935 RID: 14645 RVA: 0x000EEFD4 File Offset: 0x000ED1D4
		public virtual void HandleShutDown()
		{
			foreach (UserProfile profile in this.loggedInProfiles)
			{
				this.RequestSave(profile, true);
			}
		}

		// Token: 0x06003936 RID: 14646 RVA: 0x000EF028 File Offset: 0x000ED228
		public static void Copy(UserProfile src, UserProfile dest)
		{
			dest.fileSystem = src.fileSystem;
			dest.filePath = src.filePath;
			StatSheet.Copy(src.statSheet, dest.statSheet);
			src.loadout.Copy(dest.loadout);
			dest.tutorialSprint = src.tutorialSprint;
			dest.tutorialDifficulty = src.tutorialDifficulty;
			dest.tutorialEquipment = src.tutorialEquipment;
			SaveFieldAttribute[] saveFields = UserProfile.saveFields;
			for (int i = 0; i < saveFields.Length; i++)
			{
				saveFields[i].copier(src, dest);
			}
			dest.isClaimed = false;
			dest.canSave = false;
			dest.fileName = src.fileName;
			dest.onPickupDiscovered = null;
			dest.onStatsReceived = null;
			dest.loggedIn = false;
		}

		// Token: 0x06003937 RID: 14647 RVA: 0x000EF0E8 File Offset: 0x000ED2E8
		protected void EnqueueFileOutput(FileOutput fileOutput)
		{
			Queue<FileOutput> obj = this.pendingOutputQueue;
			lock (obj)
			{
				this.pendingOutputQueue.Enqueue(fileOutput);
			}
		}

		// Token: 0x06003938 RID: 14648 RVA: 0x000EF130 File Offset: 0x000ED330
		private static bool ProfileNameIsReserved([NotNull] string profileName)
		{
			return string.Equals("default", profileName, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06003939 RID: 14649 RVA: 0x000EF140 File Offset: 0x000ED340
		[ConCommand(commandName = "user_profile_save", flags = ConVarFlags.None, helpText = "Saves the named profile to disk, if it exists.")]
		private static void CCUserProfileSave(ConCommandArgs args)
		{
			args.CheckArgumentCount(1);
			string text = args[0];
			if (SaveSystem.ProfileNameIsReserved(text))
			{
				Debug.LogFormat("Cannot save profile \"{0}\", it is a reserved profile.", new object[]
				{
					text
				});
				return;
			}
			UserProfile profile = PlatformSystems.saveSystem.GetProfile(text);
			if (profile == null)
			{
				Debug.LogFormat("Could not find profile \"{0}\" to save.", new object[]
				{
					text
				});
				return;
			}
			profile.RequestEventualSave();
		}

		// Token: 0x0600393A RID: 14650 RVA: 0x000EF1A8 File Offset: 0x000ED3A8
		[ConCommand(commandName = "user_profile_copy", flags = ConVarFlags.None, helpText = "Copies the profile named by the first argument to a new profile named by the second argument. This does not save the profile.")]
		private static void CCUserProfileCopy(ConCommandArgs args)
		{
			args.CheckArgumentCount(2);
			string text = args[0].ToLower(CultureInfo.InvariantCulture);
			string text2 = args[1].ToLower(CultureInfo.InvariantCulture);
			UserProfile profile = PlatformSystems.saveSystem.GetProfile(text);
			if (profile == null)
			{
				Debug.LogFormat("Profile {0} does not exist, so it cannot be copied.", new object[]
				{
					text
				});
				return;
			}
			if (PlatformSystems.saveSystem.GetProfile(text2) != null)
			{
				Debug.LogFormat("Profile {0} already exists, and cannot be copied to.", new object[]
				{
					text2
				});
				return;
			}
			UserProfile userProfile = new UserProfile();
			SaveSystem.Copy(profile, userProfile);
			userProfile.fileSystem = (profile.fileSystem ?? RoR2Application.cloudStorage);
			userProfile.filePath = "/UserProfiles/" + text2 + ".xml";
			userProfile.fileName = text2;
			userProfile.canSave = true;
			PlatformSystems.saveSystem.loadedUserProfiles[text2] = userProfile;
			Action action = SaveSystem.onAvailableUserProfilesChanged;
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x0600393B RID: 14651 RVA: 0x000EF294 File Offset: 0x000ED494
		[ConCommand(commandName = "user_profile_delete", flags = ConVarFlags.None, helpText = "Unloads the named user profile and deletes it from the disk if it exists.")]
		private static void CCUserProfileDelete(ConCommandArgs args)
		{
			args.CheckArgumentCount(1);
			string text = args[0];
			if (SaveSystem.ProfileNameIsReserved(text))
			{
				Debug.LogFormat("Cannot delete profile \"{0}\", it is a reserved profile.", new object[]
				{
					text
				});
				return;
			}
			SaveSystem.DeleteUserProfile(text);
		}

		// Token: 0x0600393C RID: 14652 RVA: 0x000EF2D8 File Offset: 0x000ED4D8
		private static void DeleteUserProfile(string fileName)
		{
			fileName = fileName.ToLower(CultureInfo.InvariantCulture);
			UserProfile profile = PlatformSystems.saveSystem.GetProfile(fileName);
			if (PlatformSystems.saveSystem.loadedUserProfiles.ContainsKey(fileName))
			{
				PlatformSystems.saveSystem.loadedUserProfiles.Remove(fileName);
			}
			if (profile != null && profile.fileSystem != null)
			{
				profile.fileSystem.DeleteFile(profile.filePath);
			}
			Action action = SaveSystem.onAvailableUserProfilesChanged;
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x0600393D RID: 14653 RVA: 0x000EF34C File Offset: 0x000ED54C
		[ConCommand(commandName = "create_corrupted_profiles", flags = ConVarFlags.None, helpText = "Creates corrupted user profiles.")]
		private static void CCCreateCorruptedProfiles(ConCommandArgs args)
		{
			SaveSystem.<>c__DisplayClass39_0 CS$<>8__locals1;
			CS$<>8__locals1.fileSystem = RoR2Application.cloudStorage;
			SaveSystem.<CCCreateCorruptedProfiles>g__WriteFile|39_0("empty", "", ref CS$<>8__locals1);
			SaveSystem.<CCCreateCorruptedProfiles>g__WriteFile|39_0("truncated", "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<UserProfile>\r\n", ref CS$<>8__locals1);
			SaveSystem.<CCCreateCorruptedProfiles>g__WriteFile|39_0("multiroot", "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<UserProfile>\r\n</UserProfile>\r\n<UserProfile>\r\n</UserProfile>", ref CS$<>8__locals1);
			SaveSystem.<CCCreateCorruptedProfiles>g__WriteFile|39_0("outoforder", "<?xml version=\"1.0\" encodi=\"utf-8\"ng?>\r\n<Userrofile>\r\n<UserProfile>\r\n</UserProfileProfile>\r\n</UserP>", ref CS$<>8__locals1);
		}

		// Token: 0x0600393E RID: 14654 RVA: 0x000EF3AC File Offset: 0x000ED5AC
		[ConCommand(commandName = "userprofile_test_buffer_overflow", flags = ConVarFlags.None, helpText = "")]
		private static void CCUserProfileTestBufferOverflow(ConCommandArgs args)
		{
			args.CheckArgumentCount(1);
			int num = 128;
			FileSystem cloudStorage = RoR2Application.cloudStorage;
			RemoteFile remoteFile = Client.Instance.RemoteStorage.OpenFile(args[0]);
			int sizeInBytes = remoteFile.SizeInBytes;
			FieldInfo field = remoteFile.GetType().GetField("_sizeInBytes", BindingFlags.Instance | BindingFlags.NonPublic);
			int num2 = (int)field.GetValue(remoteFile);
			field.SetValue(remoteFile, num2 + num);
			byte[] array = remoteFile.ReadAllBytes();
			byte[] array2 = new byte[num];
			for (int i = 0; i < num; i++)
			{
				Debug.Log(array[num2 + i]);
				array2[i] = array[num2 + i];
			}
			GUIUtility.systemCopyBuffer = Encoding.UTF8.GetString(array2);
			field.SetValue(remoteFile, num2);
		}

		// Token: 0x06003940 RID: 14656 RVA: 0x000EF4D4 File Offset: 0x000ED6D4
		[CompilerGenerated]
		internal static void <CCCreateCorruptedProfiles>g__WriteFile|39_0(string fileName, string contents, ref SaveSystem.<>c__DisplayClass39_0 A_2)
		{
			using (Stream stream = A_2.fileSystem.OpenFile("/UserProfiles/" + fileName + ".xml", FileMode.Create, FileAccess.Write, FileShare.None))
			{
				using (TextWriter textWriter = new StreamWriter(stream))
				{
					textWriter.Write(contents.ToCharArray());
					textWriter.Flush();
				}
				stream.Flush();
			}
		}

		// Token: 0x040038D8 RID: 14552
		public bool isXmlReady;

		// Token: 0x040038D9 RID: 14553
		protected readonly Dictionary<FileReference, DateTime> latestWrittenRequestTimesByFile = new Dictionary<FileReference, DateTime>();

		// Token: 0x040038DA RID: 14554
		public readonly Dictionary<string, UserProfile> loadedUserProfiles = new Dictionary<string, UserProfile>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x040038DB RID: 14555
		public readonly List<LoadUserProfileOperationResult> badFileResults = new List<LoadUserProfileOperationResult>();

		// Token: 0x040038DC RID: 14556
		public readonly List<UserProfile> loggedInProfiles = new List<UserProfile>();

		// Token: 0x040038DD RID: 14557
		private static float secondAccumulator;

		// Token: 0x040038DF RID: 14559
		private const string userProfilesFolder = "/UserProfiles";

		// Token: 0x040038E0 RID: 14560
		protected readonly Queue<FileOutput> pendingOutputQueue = new Queue<FileOutput>();

		// Token: 0x040038E1 RID: 14561
		private readonly List<Task> activeTasks = new List<Task>();
	}
}
