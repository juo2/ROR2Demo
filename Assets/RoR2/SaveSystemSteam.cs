using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Facepunch.Steamworks;
using UnityEngine;
using Zio;

namespace RoR2
{
	// Token: 0x020009CD RID: 2509
	public class SaveSystemSteam : SaveSystem
	{
		// Token: 0x06003985 RID: 14725 RVA: 0x000EFD34 File Offset: 0x000EDF34
		protected override LoadUserProfileOperationResult LoadUserProfileFromDisk(IFileSystem fileSystem, UPath path)
		{
			Debug.LogFormat("Attempting to load user profile {0}", new object[]
			{
				path
			});
			LoadUserProfileOperationResult loadUserProfileOperationResult = new LoadUserProfileOperationResult
			{
				fileName = path.FullName,
				fileLength = 0L,
				userProfile = null,
				exception = null,
				failureContents = null
			};
			LoadUserProfileOperationResult result = loadUserProfileOperationResult;
			try
			{
				using (Stream stream = fileSystem.OpenFile(path, FileMode.Open, FileAccess.Read, FileShare.Read))
				{
					SaveSystem.SkipBOM(stream);
					result.fileLength = stream.Length;
					using (TextReader textReader = new StreamReader(stream, Encoding.UTF8))
					{
						Debug.LogFormat("stream.Length={0}", new object[]
						{
							stream.Length
						});
						try
						{
							UserProfile userProfile = XmlUtility.FromXml(XDocument.Load(textReader));
							userProfile.fileName = path.GetNameWithoutExtension();
							userProfile.canSave = true;
							userProfile.fileSystem = fileSystem;
							userProfile.filePath = path;
							result.userProfile = userProfile;
							return result;
						}
						catch (XmlException)
						{
							stream.Position = 0L;
							byte[] array = new byte[stream.Length];
							stream.Read(array, 0, (int)stream.Length);
							result.failureContents = Convert.ToBase64String(array);
							UserProfile userProfile2 = base.CreateGuestProfile();
							userProfile2.fileSystem = fileSystem;
							userProfile2.filePath = path;
							userProfile2.fileName = path.GetNameWithoutExtension();
							userProfile2.name = string.Format("<color=#FF7F7FFF>Corrupted Profile: {0}</color>", userProfile2.fileName);
							userProfile2.canSave = false;
							userProfile2.isCorrupted = true;
							result.userProfile = userProfile2;
							throw;
						}
					}
				}
			}
			catch (Exception ex)
			{
				Debug.LogFormat("Failed to load user profile {0}: {1}\nStack Trace:\n{2}", new object[]
				{
					path,
					ex.Message,
					ex.StackTrace
				});
				result.exception = ex;
			}
			return result;
		}

		// Token: 0x06003986 RID: 14726 RVA: 0x000EFF60 File Offset: 0x000EE160
		protected override void ProcessFileOutputQueue()
		{
			Queue<FileOutput> pendingOutputQueue = this.pendingOutputQueue;
			lock (pendingOutputQueue)
			{
				while (this.pendingOutputQueue.Count > 0)
				{
					FileOutput fileOutput = this.pendingOutputQueue.Dequeue();
					if (this.CanWrite(fileOutput))
					{
						this.WriteToDisk(fileOutput);
					}
				}
			}
		}

		// Token: 0x06003987 RID: 14727 RVA: 0x000EFFC8 File Offset: 0x000EE1C8
		protected override void StartSave(UserProfile userProfile, bool blocking)
		{
			SaveSystemSteam.<>c__DisplayClass3_0 CS$<>8__locals1 = new SaveSystemSteam.<>c__DisplayClass3_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.tempCopy = new UserProfile();
			SaveSystem.Copy(userProfile, CS$<>8__locals1.tempCopy);
			CS$<>8__locals1.fileOutput = new FileOutput
			{
				fileReference = new FileReference
				{
					path = CS$<>8__locals1.tempCopy.filePath,
					fileSystem = CS$<>8__locals1.tempCopy.fileSystem
				},
				requestTime = DateTime.UtcNow,
				contents = Array.Empty<byte>()
			};
			CS$<>8__locals1.task = null;
			CS$<>8__locals1.task = new Task(new Action(CS$<>8__locals1.<StartSave>g__PayloadGeneratorAction|0));
			base.AddActiveTask(CS$<>8__locals1.task);
			CS$<>8__locals1.task.Start(TaskScheduler.Default);
			if (blocking)
			{
				CS$<>8__locals1.task.Wait();
				this.ProcessFileOutputQueue();
			}
		}

		// Token: 0x06003988 RID: 14728 RVA: 0x000F009C File Offset: 0x000EE29C
		private new bool CanWrite(FileOutput fileOutput)
		{
			if (fileOutput.contents.Length == 0)
			{
				Debug.LogErrorFormat("Cannot write UserProfile \"{0}\" with zero-length contents. This would erase the file.", Array.Empty<object>());
				return false;
			}
			DateTime t;
			return !this.latestWrittenRequestTimesByFile.TryGetValue(fileOutput.fileReference, out t) || t < fileOutput.requestTime;
		}

		// Token: 0x06003989 RID: 14729 RVA: 0x000F00E8 File Offset: 0x000EE2E8
		private void WriteToDisk(FileOutput fileOutput)
		{
			FileIoIndicatorManager.IncrementActiveWriteCount();
			try
			{
				using (Stream stream = fileOutput.fileReference.fileSystem.OpenFile(fileOutput.fileReference.path, FileMode.Create, FileAccess.Write, FileShare.None))
				{
					stream.Write(fileOutput.contents, 0, fileOutput.contents.Length);
					stream.Flush();
					stream.Close();
					Debug.LogFormat("Saved file \"{0}\" ({1} bytes)", new object[]
					{
						fileOutput.fileReference.path.GetName(),
						fileOutput.contents.Length
					});
				}
				this.latestWrittenRequestTimesByFile[fileOutput.fileReference] = fileOutput.requestTime;
			}
			catch (Exception message)
			{
				Debug.Log(message);
			}
			finally
			{
				FileIoIndicatorManager.DecrementActiveWriteCount();
			}
		}

		// Token: 0x0600398A RID: 14730 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public override UserProfile LoadPrimaryProfile()
		{
			return null;
		}

		// Token: 0x0600398B RID: 14731 RVA: 0x000F01C8 File Offset: 0x000EE3C8
		public override string GetPlatformUsernameOrDefault(string defaultName)
		{
			Client instance = Client.Instance;
			string text = (instance != null) ? instance.Username : null;
			if (!string.IsNullOrEmpty(text))
			{
				return text;
			}
			return defaultName;
		}

		// Token: 0x0600398C RID: 14732 RVA: 0x000026ED File Offset: 0x000008ED
		public override void SaveHistory(byte[] data, string fileName)
		{
		}

		// Token: 0x0600398D RID: 14733 RVA: 0x000F01F2 File Offset: 0x000EE3F2
		public override Dictionary<string, byte[]> LoadHistory()
		{
			return new Dictionary<string, byte[]>();
		}

		// Token: 0x040038F3 RID: 14579
		private new readonly Dictionary<FileReference, DateTime> latestWrittenRequestTimesByFile = new Dictionary<FileReference, DateTime>();
	}
}
