using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using HG;
using UnityEngine;

namespace RoR2.ContentManagement
{
	// Token: 0x02000E0E RID: 3598
	[CreateAssetMenu(menuName = "RoR2/ContentAssetRefResolver")]
	public class ContentAssetRefResolver : ScriptableObject
	{
		// Token: 0x06005273 RID: 21107 RVA: 0x001563AC File Offset: 0x001545AC
		[ContextMenu("Run Test")]
		public void Apply()
		{
			for (int i = 0; i < this.fieldAssignmentInfos.Length; i++)
			{
				this.ApplyFieldAssignmentInfo(this.fieldAssignmentInfos[i]);
			}
		}

		// Token: 0x06005274 RID: 21108 RVA: 0x001563E0 File Offset: 0x001545E0
		private unsafe object FindContentAsset(in ContentAssetRefResolver.ContentAssetPath contentAssetPath)
		{
			for (int i = 0; i < ContentAssetRefResolver.loadedContentPacks.Length; i++)
			{
				if (ContentAssetRefResolver.loadedContentPacks[i].identifier.Equals(contentAssetPath.contentPackIdentifier, StringComparison.Ordinal))
				{
					ReadOnlyContentPack readOnlyContentPack = *ContentAssetRefResolver.loadedContentPacks[i];
					object result;
					readOnlyContentPack.FindAsset(contentAssetPath.collectionName, contentAssetPath.assetName, out result);
					return result;
				}
			}
			return null;
		}

		// Token: 0x06005275 RID: 21109 RVA: 0x0015644A File Offset: 0x0015464A
		static ContentAssetRefResolver()
		{
			ContentManager.onContentPacksAssigned += ContentAssetRefResolver.OnContentPacksLoaded;
		}

		// Token: 0x06005276 RID: 21110 RVA: 0x0015647C File Offset: 0x0015467C
		private static void OnContentPacksLoaded(ReadOnlyArray<ReadOnlyContentPack> newLoadedContentPacks)
		{
			ContentAssetRefResolver.loadedContentPacks = newLoadedContentPacks;
			ContentAssetRefResolver.contentPacksLoaded = true;
			ContentAssetRefResolver.ApplyQueued();
		}

		// Token: 0x06005277 RID: 21111 RVA: 0x00156490 File Offset: 0x00154690
		private static void ApplyQueued()
		{
			while (ContentAssetRefResolver.pendingResolutions.Count > 0)
			{
				ContentAssetRefResolver contentAssetRefResolver = ContentAssetRefResolver.pendingResolutions.Dequeue();
				try
				{
					contentAssetRefResolver.Apply();
				}
				catch (Exception message)
				{
					Debug.LogError(message);
				}
			}
		}

		// Token: 0x06005278 RID: 21112 RVA: 0x001564D8 File Offset: 0x001546D8
		private void OnEnable()
		{
			if (this.applyOnEnable)
			{
				if (ContentAssetRefResolver.contentPacksLoaded)
				{
					this.Apply();
					return;
				}
				ContentAssetRefResolver.pendingResolutions.Enqueue(this);
			}
		}

		// Token: 0x06005279 RID: 21113 RVA: 0x001564FC File Offset: 0x001546FC
		private bool ApplyFieldAssignmentInfo(in ContentAssetRefResolver.FieldAssignmentInfo fieldAssignmentInfo)
		{
			object targetObject = fieldAssignmentInfo.targetObject;
			ContentAssetRefResolver.<>c__DisplayClass13_0 CS$<>8__locals1;
			CS$<>8__locals1.fieldPath = fieldAssignmentInfo.fieldPath;
			CS$<>8__locals1.valueToAssign = (this.FindContentAsset(fieldAssignmentInfo.contentAssetPath) as UnityEngine.Object);
			CS$<>8__locals1.currentReadPos = 0;
			bool result;
			try
			{
				if (targetObject == null)
				{
					throw new NullReferenceException("targetObject is null");
				}
				ContentAssetRefResolver.<ApplyFieldAssignmentInfo>g__ProcessObject|13_0(ref targetObject, ref CS$<>8__locals1);
				result = true;
			}
			catch (Exception ex)
			{
				Debug.LogError(string.Format("Could not assign {0}.{1}={2}: {3}", new object[]
				{
					targetObject,
					CS$<>8__locals1.fieldPath,
					CS$<>8__locals1.valueToAssign,
					ex
				}));
				result = false;
			}
			return result;
		}

		// Token: 0x0600527B RID: 21115 RVA: 0x001565B0 File Offset: 0x001547B0
		[CompilerGenerated]
		internal static void <ApplyFieldAssignmentInfo>g__ProcessObject|13_0(ref object obj, ref ContentAssetRefResolver.<>c__DisplayClass13_0 A_1)
		{
			Type type = obj.GetType();
			IList list = obj as IList;
			if (list != null)
			{
				int index = ContentAssetRefResolver.<ApplyFieldAssignmentInfo>g__TakeArrayIndex|13_3(ref A_1);
				object value = list[index];
				ContentAssetRefResolver.<ApplyFieldAssignmentInfo>g__HandleInnerValue|13_5(ref value, ref A_1);
				list[index] = value;
				return;
			}
			string text = ContentAssetRefResolver.<ApplyFieldAssignmentInfo>g__TakeIdentifier|13_2(ref A_1);
			FieldInfo field = type.GetField(text, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			if (field == null)
			{
				throw new Exception(string.Concat(new string[]
				{
					"Field \"",
					type.Name,
					".",
					text,
					"\" could not be found."
				}));
			}
			object value2 = field.GetValue(obj);
			ContentAssetRefResolver.<ApplyFieldAssignmentInfo>g__HandleInnerValue|13_5(ref value2, ref A_1);
			field.SetValue(obj, value2);
		}

		// Token: 0x0600527C RID: 21116 RVA: 0x00156664 File Offset: 0x00154864
		[CompilerGenerated]
		internal static void <ApplyFieldAssignmentInfo>g__HandleInnerValue|13_5(ref object innerValue, ref ContentAssetRefResolver.<>c__DisplayClass13_0 A_1)
		{
			if (A_1.currentReadPos >= A_1.fieldPath.Length)
			{
				innerValue = A_1.valueToAssign;
				return;
			}
			if (innerValue == null)
			{
				throw new Exception(A_1.fieldPath.Substring(0, A_1.currentReadPos) + " is null.");
			}
			if (innerValue is UnityEngine.Object)
			{
				throw new Exception("Assignment cannot propagate through UnityEngine.Object.");
			}
			ContentAssetRefResolver.<ApplyFieldAssignmentInfo>g__ProcessObject|13_0(ref innerValue, ref A_1);
		}

		// Token: 0x0600527D RID: 21117 RVA: 0x001566CE File Offset: 0x001548CE
		[CompilerGenerated]
		internal static bool <ApplyFieldAssignmentInfo>g__IsIdentifierChar|13_1(char c)
		{
			return char.IsLetterOrDigit(c) || c == '_';
		}

		// Token: 0x0600527E RID: 21118 RVA: 0x001566E0 File Offset: 0x001548E0
		[CompilerGenerated]
		internal static string <ApplyFieldAssignmentInfo>g__TakeIdentifier|13_2(ref ContentAssetRefResolver.<>c__DisplayClass13_0 A_0)
		{
			int num = A_0.currentReadPos;
			char c = A_0.fieldPath[A_0.currentReadPos];
			if (c == '.')
			{
				num++;
				int currentReadPos = A_0.currentReadPos + 1;
				A_0.currentReadPos = currentReadPos;
			}
			else if (A_0.currentReadPos != 0)
			{
				throw new Exception(string.Format("Expected '.' at {0}, but encountered '{1}'", num, c));
			}
			while (A_0.currentReadPos < A_0.fieldPath.Length && ContentAssetRefResolver.<ApplyFieldAssignmentInfo>g__IsIdentifierChar|13_1(A_0.fieldPath[A_0.currentReadPos]))
			{
				int currentReadPos = A_0.currentReadPos + 1;
				A_0.currentReadPos = currentReadPos;
			}
			string text = A_0.fieldPath.Substring(num, A_0.currentReadPos - num);
			if (text == null)
			{
				throw new FormatException(string.Format("Expected identifier at {0}, but encountered end of string.", num));
			}
			if (text.Length == 0)
			{
				throw new FormatException(string.Format("Expected identifier at {0}, but encountered no valid characters (a-zA-Z0-9_).", num));
			}
			if (char.IsDigit(text[0]))
			{
				throw new FormatException(string.Format("Expected identifier at {0}, but an identifier cannot begin with a digit. digit={1} substring={2}", num, text[0], text));
			}
			return text;
		}

		// Token: 0x0600527F RID: 21119 RVA: 0x00156800 File Offset: 0x00154A00
		[CompilerGenerated]
		internal static int <ApplyFieldAssignmentInfo>g__TakeArrayIndex|13_3(ref ContentAssetRefResolver.<>c__DisplayClass13_0 A_0)
		{
			char c = A_0.fieldPath[A_0.currentReadPos];
			if (c != '[')
			{
				throw new FormatException(string.Format("Expected char '[' but got '{0}'.", c));
			}
			int currentReadPos = A_0.currentReadPos + 1;
			A_0.currentReadPos = currentReadPos;
			int num = ContentAssetRefResolver.<ApplyFieldAssignmentInfo>g__TakeInt|13_4(ref A_0);
			if (num < 0)
			{
				throw new FormatException(string.Format("Array index {0} cannot be negative.", num));
			}
			char c2 = A_0.fieldPath[A_0.currentReadPos];
			if (c2 != ']')
			{
				throw new FormatException(string.Format("Expected char ']' but got '{0}'.", c2));
			}
			currentReadPos = A_0.currentReadPos + 1;
			A_0.currentReadPos = currentReadPos;
			return num;
		}

		// Token: 0x06005280 RID: 21120 RVA: 0x001568AC File Offset: 0x00154AAC
		[CompilerGenerated]
		internal static int <ApplyFieldAssignmentInfo>g__TakeInt|13_4(ref ContentAssetRefResolver.<>c__DisplayClass13_0 A_0)
		{
			int currentReadPos = A_0.currentReadPos;
			if (currentReadPos >= A_0.fieldPath.Length)
			{
				throw new FormatException(string.Format("Expected integer at {0}, but encountered end of string.", currentReadPos));
			}
			char c = A_0.fieldPath[A_0.currentReadPos];
			if (c != '-' && !char.IsDigit(c))
			{
				throw new FormatException(string.Format("Expected integer at {0}, but an integer cannot begin with character '{1}'", currentReadPos, c));
			}
			if (c == '-')
			{
				int currentReadPos2 = A_0.currentReadPos + 1;
				A_0.currentReadPos = currentReadPos2;
			}
			while (A_0.currentReadPos < A_0.fieldPath.Length)
			{
				if (!char.IsDigit(A_0.fieldPath[A_0.currentReadPos]) || A_0.currentReadPos == A_0.fieldPath.Length - 1)
				{
					A_0.fieldPath.Substring(currentReadPos, A_0.currentReadPos + 1 - currentReadPos);
					break;
				}
				int currentReadPos2 = A_0.currentReadPos + 1;
				A_0.currentReadPos = currentReadPos2;
			}
			string text = A_0.fieldPath.Substring(currentReadPos, A_0.currentReadPos - currentReadPos);
			if (text == null)
			{
				throw new FormatException(string.Format("Expected integer at {0}, but encountered end of string.", currentReadPos));
			}
			if (text.Length == 0)
			{
				throw new FormatException(string.Format("Expected integer at {0}, but encountered no valid characters (-0-9).", currentReadPos));
			}
			return int.Parse(text);
		}

		// Token: 0x04004EC8 RID: 20168
		public bool applyOnEnable = true;

		// Token: 0x04004EC9 RID: 20169
		public ContentAssetRefResolver.FieldAssignmentInfo[] fieldAssignmentInfos;

		// Token: 0x04004ECA RID: 20170
		private static bool contentPacksLoaded = false;

		// Token: 0x04004ECB RID: 20171
		private static Queue<ContentAssetRefResolver> pendingResolutions = new Queue<ContentAssetRefResolver>();

		// Token: 0x04004ECC RID: 20172
		private static ReadOnlyArray<ReadOnlyContentPack> loadedContentPacks = Array.Empty<ReadOnlyContentPack>();

		// Token: 0x02000E0F RID: 3599
		[Serializable]
		public struct FieldAssignmentInfo
		{
			// Token: 0x04004ECD RID: 20173
			public UnityEngine.Object targetObject;

			// Token: 0x04004ECE RID: 20174
			public string fieldPath;

			// Token: 0x04004ECF RID: 20175
			public ContentAssetRefResolver.ContentAssetPath contentAssetPath;
		}

		// Token: 0x02000E10 RID: 3600
		[Serializable]
		public struct ContentAssetPath
		{
			// Token: 0x04004ED0 RID: 20176
			public string contentPackIdentifier;

			// Token: 0x04004ED1 RID: 20177
			public string collectionName;

			// Token: 0x04004ED2 RID: 20178
			public string assetName;
		}
	}
}
