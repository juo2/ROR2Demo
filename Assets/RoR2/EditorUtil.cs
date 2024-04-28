using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using JetBrains.Annotations;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000590 RID: 1424
	public static class EditorUtil
	{
		// Token: 0x06001994 RID: 6548 RVA: 0x0006F08B File Offset: 0x0006D28B
		private static void StaticUpdate()
		{
			Action onNextUpdate = EditorUtil._onNextUpdate;
			EditorUtil._onNextUpdate = null;
			if (onNextUpdate == null)
			{
				return;
			}
			onNextUpdate();
		}

		// Token: 0x06001995 RID: 6549 RVA: 0x0000CF8A File Offset: 0x0000B18A
		public static bool IsPrefabVariant(UnityEngine.Object obj)
		{
			return false;
		}

		// Token: 0x06001996 RID: 6550 RVA: 0x000026ED File Offset: 0x000008ED
		public static void SetDirty(UnityEngine.Object obj)
		{
		}

		// Token: 0x06001997 RID: 6551 RVA: 0x000026ED File Offset: 0x000008ED
		public static void NonSerializedObjectGUI<T>(ref T obj)
		{
		}

		// Token: 0x06001998 RID: 6552 RVA: 0x0006F0A4 File Offset: 0x0006D2A4
		[UsedImplicitly]
		private static void SetFieldGeneric<T>(FieldInfo fieldInfo, object instance, T value)
		{
			if (fieldInfo.FieldType.IsAssignableFrom(typeof(T)))
			{
				fieldInfo.SetValue(instance, value);
				return;
			}
			Debug.LogErrorFormat("Cannot assign value {0} of type {1} to field {2} of type {3}", new object[]
			{
				value,
				value.GetType().Name,
				fieldInfo.Name,
				fieldInfo.FieldType.Name
			});
		}

		// Token: 0x06001999 RID: 6553 RVA: 0x00062756 File Offset: 0x00060956
		public static UnityEngine.Object PrefabUtilityGetNearestPrefabInstanceRoot(UnityEngine.Object obj)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600199A RID: 6554 RVA: 0x00062756 File Offset: 0x00060956
		public static UnityEngine.Object PrefabUtilityInstantiatePrefab(UnityEngine.Object prefab, Transform parent)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600199B RID: 6555 RVA: 0x0006F11C File Offset: 0x0006D31C
		public static void CopyToScriptableObject<TSrc, TDest>(TSrc src, string folder) where TDest : ScriptableObject
		{
			TDest tdest = ScriptableObject.CreateInstance<TDest>();
			Dictionary<string, ValueTuple<Type, Func<TSrc, object>>> dictionary = new Dictionary<string, ValueTuple<Type, Func<TSrc, object>>>();
			Dictionary<string, ValueTuple<Type, Action<TDest, object>>> dictionary2 = new Dictionary<string, ValueTuple<Type, Action<TDest, object>>>();
			FieldInfo[] fields = typeof(TSrc).GetFields();
			for (int i = 0; i < fields.Length; i++)
			{
				FieldInfo srcField = fields[i];
				dictionary[srcField.Name] = new ValueTuple<Type, Func<TSrc, object>>(srcField.FieldType, (TSrc s) => srcField.GetValue(s));
			}
			PropertyInfo[] properties = typeof(TSrc).GetProperties();
			for (int i = 0; i < properties.Length; i++)
			{
				PropertyInfo srcProperty = properties[i];
				if (!(srcProperty.GetMethod == null))
				{
					dictionary[srcProperty.Name] = new ValueTuple<Type, Func<TSrc, object>>(srcProperty.PropertyType, (TSrc s) => srcProperty.GetValue(s));
				}
			}
			fields = typeof(TDest).GetFields();
			for (int i = 0; i < fields.Length; i++)
			{
				FieldInfo destField = fields[i];
				dictionary2[destField.Name] = new ValueTuple<Type, Action<TDest, object>>(destField.FieldType, delegate(TDest d, object v)
				{
					destField.SetValue(d, v);
				});
			}
			properties = typeof(TDest).GetProperties();
			for (int i = 0; i < properties.Length; i++)
			{
				PropertyInfo destProperty = properties[i];
				if (!(destProperty.SetMethod == null))
				{
					dictionary2[destProperty.Name] = new ValueTuple<Type, Action<TDest, object>>(destProperty.PropertyType, delegate(TDest d, object v)
					{
						destProperty.SetValue(d, v);
					});
				}
			}
			foreach (KeyValuePair<string, ValueTuple<Type, Func<TSrc, object>>> keyValuePair in dictionary)
			{
				string key = keyValuePair.Key;
				ValueTuple<Type, Func<TSrc, object>> value = keyValuePair.Value;
				Type item = value.Item1;
				Func<TSrc, object> item2 = value.Item2;
				ValueTuple<Type, Action<TDest, object>> valueTuple;
				if (dictionary2.TryGetValue(key, out valueTuple))
				{
					Type item3 = valueTuple.Item1;
					Action<TDest, object> item4 = valueTuple.Item2;
					if (item3.IsAssignableFrom(item))
					{
						item4(tdest, item2(src));
					}
				}
			}
			EditorUtil.CreateAsset(tdest, folder + tdest.name + ".asset");
		}

		// Token: 0x0600199C RID: 6556 RVA: 0x000026ED File Offset: 0x000008ED
		private static void CreateAsset(UnityEngine.Object asset, string path)
		{
		}

		// Token: 0x0600199D RID: 6557 RVA: 0x000026ED File Offset: 0x000008ED
		private static void AssetDatabaseSaveAssets()
		{
		}

		// Token: 0x0600199E RID: 6558 RVA: 0x000026ED File Offset: 0x000008ED
		private static void AssetDatabaseRefresh()
		{
		}

		// Token: 0x0600199F RID: 6559 RVA: 0x000026ED File Offset: 0x000008ED
		private static void AssetDatabaseMoveAsset(string oldPath, string newPath)
		{
		}

		// Token: 0x060019A0 RID: 6560 RVA: 0x000026ED File Offset: 0x000008ED
		private static void AssetDatabaseDeleteAsset(string path)
		{
		}

		// Token: 0x060019A1 RID: 6561 RVA: 0x000026ED File Offset: 0x000008ED
		private static void AssetDatabaseCreateFolder(string parentFolder, string newFolderName)
		{
		}

		// Token: 0x060019A2 RID: 6562 RVA: 0x0006F3AC File Offset: 0x0006D5AC
		public static void EnsureDirectoryExists(string directoryPath)
		{
			string[] array = directoryPath.Split(EditorUtil.directorySeparators);
			string text = array[0].ToString();
			int i = 1;
			int num = array.Length;
			while (i < num)
			{
				string text2 = array[i].ToString();
				string text3 = string.Format("{0}{1}{2}", text, '/', text2);
				if (!Directory.Exists(text3))
				{
					Debug.Log("Creating directory " + text + "/" + text2);
					EditorUtil.AssetDatabaseCreateFolder(text, text2);
				}
				text = text3;
				i++;
			}
			if (!Directory.Exists(directoryPath))
			{
				Debug.LogWarning("Failed to ensure path \"" + directoryPath + "\"");
			}
		}

		// Token: 0x060019A3 RID: 6563 RVA: 0x0006F444 File Offset: 0x0006D644
		public static void MoveAsset(string oldPath, string newPath, bool deleteEmpty = true)
		{
			string directoryName = Path.GetDirectoryName(oldPath);
			EditorUtil.EnsureDirectoryExists(Path.GetDirectoryName(newPath));
			EditorUtil.AssetDatabaseMoveAsset(oldPath, newPath);
			if (deleteEmpty)
			{
				EditorUtil.DeleteDirectoryEmptyChildren(directoryName);
			}
		}

		// Token: 0x060019A4 RID: 6564 RVA: 0x0006F474 File Offset: 0x0006D674
		public static void DeleteDirectoryIfEmpty(string directory)
		{
			EditorUtil.DeleteDirectoryEmptyChildren(directory);
			EditorUtil.DeleteDirectoryEmptyParents(directory);
		}

		// Token: 0x060019A5 RID: 6565 RVA: 0x0006F484 File Offset: 0x0006D684
		private static bool DeleteDirectoryEmptyChildren(string directory)
		{
			bool flag = true;
			try
			{
				string[] directories = Directory.GetDirectories(directory);
				for (int i = 0; i < directories.Length; i++)
				{
					string fileName = Path.GetFileName(directories[i]);
					if (!EditorUtil.DeleteDirectoryEmptyChildren(directory + "/" + fileName))
					{
						flag = false;
					}
				}
				flag &= (Directory.GetFiles(directory).Length == 0);
				if (flag)
				{
					EditorUtil.AssetDatabaseDeleteAsset(directory);
				}
			}
			catch (Exception ex)
			{
				throw new Exception(string.Format("DeleteDirectoryEmptyChildren(\"{0}\") error: {1}", directory, ex), ex);
			}
			return flag;
		}

		// Token: 0x060019A6 RID: 6566 RVA: 0x0006F508 File Offset: 0x0006D708
		private static void DeleteDirectoryEmptyParents(string directory)
		{
			string directoryName = Path.GetDirectoryName(directory);
			bool flag = Directory.GetDirectories(directoryName).Length != 0;
			bool flag2 = Directory.GetFiles(directoryName).Length != 0;
			if (!flag && !flag2)
			{
				EditorUtil.AssetDatabaseDeleteAsset(directory);
				EditorUtil.DeleteDirectoryEmptyParents(directoryName);
			}
		}

		// Token: 0x060019A7 RID: 6567 RVA: 0x0006F544 File Offset: 0x0006D744
		public static IEnumerable<string> GetAllAssetsInDirectory(string directory, bool includeSubdirectories)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(directory);
			FileSystemInfo[] array = directoryInfo.GetFileSystemInfos();
			for (int i = 0; i < array.Length; i++)
			{
				string name = array[i].Name;
				if (!name.EndsWith(".meta"))
				{
					yield return directory + "/" + name;
				}
			}
			array = null;
			if (includeSubdirectories)
			{
				foreach (DirectoryInfo directoryInfo2 in directoryInfo.GetDirectories())
				{
					string directory2 = directory + "/" + directoryInfo2.Name;
					foreach (string text in EditorUtil.GetAllAssetsInDirectory(directory2, includeSubdirectories))
					{
						yield return text;
					}
					IEnumerator<string> enumerator = null;
				}
				DirectoryInfo[] array2 = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x1400001B RID: 27
		// (add) Token: 0x060019A8 RID: 6568 RVA: 0x0006F55C File Offset: 0x0006D75C
		// (remove) Token: 0x060019A9 RID: 6569 RVA: 0x0006F590 File Offset: 0x0006D790
		private static event Action _onNextUpdate;

		// Token: 0x1400001C RID: 28
		// (add) Token: 0x060019AA RID: 6570 RVA: 0x000026ED File Offset: 0x000008ED
		// (remove) Token: 0x060019AB RID: 6571 RVA: 0x000026ED File Offset: 0x000008ED
		public static event Action onNextUpdate
		{
			add
			{
			}
			remove
			{
			}
		}

		// Token: 0x04001FFF RID: 8191
		private static int recursionLimit = 3;

		// Token: 0x04002000 RID: 8192
		private static int recursionStep = 0;

		// Token: 0x04002001 RID: 8193
		private static readonly char[] directorySeparators = new char[]
		{
			'/',
			'\\'
		};
	}
}
