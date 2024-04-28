using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;

namespace RoR2.UI
{
	// Token: 0x02000CDF RID: 3295
	public class CreditsStripGroupBuilder : MonoBehaviour
	{
		// Token: 0x06004B24 RID: 19236 RVA: 0x00134B10 File Offset: 0x00132D10
		public static string EnglishRoleToToken(string englishRoleString)
		{
			StringBuilder stringBuilder = new StringBuilder(englishRoleString);
			stringBuilder.Replace("&", "AND");
			stringBuilder.Replace(",", "");
			stringBuilder.Replace("(", "");
			stringBuilder.Replace(")", "");
			for (int i = stringBuilder.Length - 1; i >= 0; i--)
			{
				if (char.IsWhiteSpace(stringBuilder[i]))
				{
					stringBuilder[i] = '_';
				}
			}
			for (int j = stringBuilder.Length - 1; j >= 0; j--)
			{
				char c = stringBuilder[j];
				if (!char.IsLetterOrDigit(c) && c != '_' && c != '/')
				{
					stringBuilder.Remove(j, 1);
				}
			}
			stringBuilder.Insert(0, "CREDITS_ROLE_");
			return stringBuilder.ToString().ToUpper(CultureInfo.InvariantCulture);
		}

		// Token: 0x06004B25 RID: 19237 RVA: 0x00134BE8 File Offset: 0x00132DE8
		[return: TupleElementNames(new string[]
		{
			"name",
			"englishRoleName"
		})]
		public List<ValueTuple<string, string>> GetNamesAndEnglishRoles()
		{
			string text = this.source.Replace("\r", "").Replace("\n", "\t");
			List<ValueTuple<string, string>> list = new List<ValueTuple<string, string>>();
			string[] array = text.Split(new char[]
			{
				'\t'
			});
			for (int i = 0; i < array.Length; i += 2)
			{
				list.Add(new ValueTuple<string, string>(array[i], array[i + 1]));
			}
			return list;
		}

		// Token: 0x06004B26 RID: 19238 RVA: 0x00134C54 File Offset: 0x00132E54
		[ContextMenu("Build")]
		private void Build()
		{
			List<ValueTuple<string, string>> namesAndEnglishRoles = this.GetNamesAndEnglishRoles();
			List<Transform> list = new List<Transform>(base.transform.childCount);
			int i = 0;
			int childCount = base.transform.childCount;
			while (i < childCount)
			{
				list.Add(base.transform.GetChild(i));
				i++;
			}
			for (int j = list.Count - 1; j >= 0; j--)
			{
				Transform transform = list[j];
				if (EditorUtil.PrefabUtilityGetNearestPrefabInstanceRoot(transform.gameObject) != this.stripPrefab)
				{
					UnityEngine.Object.DestroyImmediate(transform.gameObject);
					list.RemoveAt(j);
				}
			}
			while (list.Count < namesAndEnglishRoles.Count)
			{
				GameObject gameObject = (GameObject)EditorUtil.PrefabUtilityInstantiatePrefab(this.stripPrefab, base.transform);
				list.Add(gameObject.transform);
			}
			while (list.Count > namesAndEnglishRoles.Count)
			{
				int index = list.Count - 1;
				UnityEngine.Object.DestroyImmediate(list[index].gameObject);
				list.RemoveAt(index);
			}
			int k = 0;
			int num = Math.Min(namesAndEnglishRoles.Count, list.Count);
			while (k < num)
			{
				ValueTuple<string, string> valueTuple = namesAndEnglishRoles[k];
				CreditsStripGroupBuilder.<Build>g__SetStripValues|4_0(list[k].gameObject, valueTuple.Item1, valueTuple.Item2);
				k++;
			}
		}

		// Token: 0x06004B28 RID: 19240 RVA: 0x00134DA8 File Offset: 0x00132FA8
		[CompilerGenerated]
		internal static void <Build>g__SetStripValues|4_0(GameObject stripInstance, string name, string rawRoleText)
		{
			stripInstance.transform.Find("CreditNameLabel").GetComponent<HGTextMeshProUGUI>().SetText(name, true);
			stripInstance.transform.Find("CreditRoleLabel").GetComponent<LanguageTextMeshController>().token = CreditsStripGroupBuilder.EnglishRoleToToken(rawRoleText);
			stripInstance.transform.Find("CreditRoleLabel").GetComponent<HGTextMeshProUGUI>().SetText(rawRoleText, true);
		}

		// Token: 0x040047D8 RID: 18392
		[Multiline]
		public string source;

		// Token: 0x040047D9 RID: 18393
		public GameObject stripPrefab;
	}
}
