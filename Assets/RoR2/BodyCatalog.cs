using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using HG;
using JetBrains.Annotations;
using RoR2.ContentManagement;
using RoR2.Modding;
using RoR2.UI;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020004E2 RID: 1250
	public static class BodyCatalog
	{
		// Token: 0x17000156 RID: 342
		// (get) Token: 0x0600169B RID: 5787 RVA: 0x00064061 File Offset: 0x00062261
		// (set) Token: 0x0600169C RID: 5788 RVA: 0x00064068 File Offset: 0x00062268
		public static int bodyCount { get; private set; }

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x0600169D RID: 5789 RVA: 0x00064070 File Offset: 0x00062270
		public static IEnumerable<GameObject> allBodyPrefabs
		{
			get
			{
				return BodyCatalog.bodyPrefabs;
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x0600169E RID: 5790 RVA: 0x00064077 File Offset: 0x00062277
		public static IEnumerable<CharacterBody> allBodyPrefabBodyBodyComponents
		{
			get
			{
				return BodyCatalog.bodyPrefabBodyComponents;
			}
		}

		// Token: 0x0600169F RID: 5791 RVA: 0x0006407E File Offset: 0x0006227E
		public static GameObject GetBodyPrefab(BodyIndex bodyIndex)
		{
			return ArrayUtils.GetSafe<GameObject>(BodyCatalog.bodyPrefabs, (int)bodyIndex);
		}

		// Token: 0x060016A0 RID: 5792 RVA: 0x0006408B File Offset: 0x0006228B
		[CanBeNull]
		public static CharacterBody GetBodyPrefabBodyComponent(BodyIndex bodyIndex)
		{
			return ArrayUtils.GetSafe<CharacterBody>(BodyCatalog.bodyPrefabBodyComponents, (int)bodyIndex);
		}

		// Token: 0x060016A1 RID: 5793 RVA: 0x00064098 File Offset: 0x00062298
		public static string GetBodyName(BodyIndex bodyIndex)
		{
			return ArrayUtils.GetSafe<string>(BodyCatalog.bodyNames, (int)bodyIndex);
		}

		// Token: 0x060016A2 RID: 5794 RVA: 0x000640A8 File Offset: 0x000622A8
		public static BodyIndex FindBodyIndex([NotNull] string bodyName)
		{
			BodyIndex result;
			if (BodyCatalog.nameToIndexMap.TryGetValue(bodyName, out result))
			{
				return result;
			}
			return BodyIndex.None;
		}

		// Token: 0x060016A3 RID: 5795 RVA: 0x000640C8 File Offset: 0x000622C8
		public static BodyIndex FindBodyIndexCaseInsensitive([NotNull] string bodyName)
		{
			for (BodyIndex bodyIndex = (BodyIndex)0; bodyIndex < (BodyIndex)BodyCatalog.bodyPrefabs.Length; bodyIndex++)
			{
				if (string.Compare(BodyCatalog.bodyPrefabs[(int)bodyIndex].name, bodyName, StringComparison.OrdinalIgnoreCase) == 0)
				{
					return bodyIndex;
				}
			}
			return BodyIndex.None;
		}

		// Token: 0x060016A4 RID: 5796 RVA: 0x000640FF File Offset: 0x000622FF
		public static BodyIndex FindBodyIndex(GameObject bodyObject)
		{
			return BodyCatalog.FindBodyIndex(bodyObject ? bodyObject.GetComponent<CharacterBody>() : null);
		}

		// Token: 0x060016A5 RID: 5797 RVA: 0x00064117 File Offset: 0x00062317
		public static BodyIndex FindBodyIndex(CharacterBody characterBody)
		{
			if (characterBody == null)
			{
				return BodyIndex.None;
			}
			return characterBody.bodyIndex;
		}

		// Token: 0x060016A6 RID: 5798 RVA: 0x00064124 File Offset: 0x00062324
		[CanBeNull]
		public static GameObject FindBodyPrefab([NotNull] string bodyName)
		{
			BodyIndex bodyIndex = BodyCatalog.FindBodyIndex(bodyName);
			if (bodyIndex != BodyIndex.None)
			{
				return BodyCatalog.GetBodyPrefab(bodyIndex);
			}
			return null;
		}

		// Token: 0x060016A7 RID: 5799 RVA: 0x00064144 File Offset: 0x00062344
		[CanBeNull]
		public static GameObject FindBodyPrefab(CharacterBody characterBody)
		{
			return BodyCatalog.GetBodyPrefab(BodyCatalog.FindBodyIndex(characterBody));
		}

		// Token: 0x060016A8 RID: 5800 RVA: 0x00064151 File Offset: 0x00062351
		[CanBeNull]
		public static GameObject FindBodyPrefab(GameObject characterBodyObject)
		{
			return BodyCatalog.GetBodyPrefab(BodyCatalog.FindBodyIndex(characterBodyObject));
		}

		// Token: 0x060016A9 RID: 5801 RVA: 0x0006415E File Offset: 0x0006235E
		[CanBeNull]
		public static GenericSkill[] GetBodyPrefabSkillSlots(BodyIndex bodyIndex)
		{
			return ArrayUtils.GetSafe<GenericSkill[]>(BodyCatalog.skillSlots, (int)bodyIndex);
		}

		// Token: 0x060016AA RID: 5802 RVA: 0x0006416C File Offset: 0x0006236C
		public static SkinDef[] GetBodySkins(BodyIndex bodyIndex)
		{
			SkinDef[][] array = BodyCatalog.skins;
			SkinDef[] array2 = Array.Empty<SkinDef>();
			return ArrayUtils.GetSafe<SkinDef[]>(array, (int)bodyIndex, array2);
		}

		// Token: 0x060016AB RID: 5803 RVA: 0x0006418C File Offset: 0x0006238C
		[SystemInitializer(new Type[]
		{

		})]
		private static void Init()
		{
			BodyCatalog.SetBodyPrefabs(ContentManager.bodyPrefabs);
			BodyCatalog.availability.MakeAvailable();
		}

		// Token: 0x060016AC RID: 5804 RVA: 0x000641A4 File Offset: 0x000623A4
		private static void SetBodyPrefabs([NotNull] GameObject[] newBodyPrefabs)
		{
			BodyCatalog.bodyPrefabs = ArrayUtils.Clone<GameObject>(newBodyPrefabs);
			Array.Sort<GameObject>(BodyCatalog.bodyPrefabs, (GameObject a, GameObject b) => string.CompareOrdinal(a.name, b.name));
			BodyCatalog.bodyPrefabBodyComponents = new CharacterBody[BodyCatalog.bodyPrefabs.Length];
			BodyCatalog.bodyNames = new string[BodyCatalog.bodyPrefabs.Length];
			BodyCatalog.bodyComponents = new Component[BodyCatalog.bodyPrefabs.Length][];
			BodyCatalog.skillSlots = new GenericSkill[BodyCatalog.bodyPrefabs.Length][];
			BodyCatalog.skins = new SkinDef[BodyCatalog.bodyPrefabs.Length][];
			BodyCatalog.nameToIndexMap.Clear();
			for (BodyIndex bodyIndex = (BodyIndex)0; bodyIndex < (BodyIndex)BodyCatalog.bodyPrefabs.Length; bodyIndex++)
			{
				GameObject gameObject = BodyCatalog.bodyPrefabs[(int)bodyIndex];
				string name = gameObject.name;
				BodyCatalog.bodyNames[(int)bodyIndex] = name;
				BodyCatalog.bodyComponents[(int)bodyIndex] = gameObject.GetComponents<Component>();
				BodyCatalog.skillSlots[(int)bodyIndex] = gameObject.GetComponents<GenericSkill>();
				BodyCatalog.nameToIndexMap.Add(name, bodyIndex);
				BodyCatalog.nameToIndexMap.Add(name + "(Clone)", bodyIndex);
				CharacterBody characterBody = BodyCatalog.bodyPrefabBodyComponents[(int)bodyIndex] = gameObject.GetComponent<CharacterBody>();
				characterBody.bodyIndex = bodyIndex;
				Texture2D texture2D = LegacyResourcesAPI.Load<Texture2D>("Textures/BodyIcons/" + name);
				SkinDef[][] array = BodyCatalog.skins;
				int num = (int)bodyIndex;
				ModelLocator component = gameObject.GetComponent<ModelLocator>();
				SkinDef[] array2;
				if (component == null)
				{
					array2 = null;
				}
				else
				{
					Transform modelTransform = component.modelTransform;
					if (modelTransform == null)
					{
						array2 = null;
					}
					else
					{
						ModelSkinController component2 = modelTransform.GetComponent<ModelSkinController>();
						array2 = ((component2 != null) ? component2.skins : null);
					}
				}
				array[num] = (array2 ?? Array.Empty<SkinDef>());
				if (texture2D)
				{
					characterBody.portraitIcon = texture2D;
				}
				else if (characterBody.portraitIcon == null)
				{
					characterBody.portraitIcon = LegacyResourcesAPI.Load<Texture2D>("Textures/MiscIcons/texMysteryIcon");
				}
				if (string.IsNullOrEmpty(characterBody.baseNameToken))
				{
					characterBody.baseNameToken = "UNIDENTIFIED";
				}
			}
			BodyCatalog.bodyCount = BodyCatalog.bodyPrefabs.Length;
		}

		// Token: 0x060016AD RID: 5805 RVA: 0x0006436A File Offset: 0x0006256A
		private static IEnumerator GeneratePortraits(bool forceRegeneration)
		{
			BodyCatalog.<>c__DisplayClass30_0 CS$<>8__locals1 = new BodyCatalog.<>c__DisplayClass30_0();
			Debug.Log("Starting portrait generation.");
			CS$<>8__locals1.modelPanel = UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/UI/IconGenerator")).GetComponentInChildren<ModelPanel>();
			yield return new WaitForEndOfFrame();
			int num2;
			for (int i = 0; i < BodyCatalog.bodyPrefabs.Length; i = num2)
			{
				CharacterBody characterBody = BodyCatalog.bodyPrefabBodyComponents[i];
				if (characterBody && (forceRegeneration || BodyCatalog.<GeneratePortraits>g__IconIsNotSuitable|30_0(characterBody.portraitIcon)))
				{
					float num = 1f;
					try
					{
						Debug.LogFormat("Generating portrait for {0}", new object[]
						{
							BodyCatalog.bodyPrefabs[i].name
						});
						ModelPanel modelPanel = CS$<>8__locals1.modelPanel;
						ModelLocator component = BodyCatalog.bodyPrefabs[i].GetComponent<ModelLocator>();
						modelPanel.modelPrefab = ((component != null) ? component.modelTransform.gameObject : null);
						CS$<>8__locals1.modelPanel.SetAnglesForCharacterThumbnail(true);
						GameObject modelPrefab = CS$<>8__locals1.modelPanel.modelPrefab;
						PrintController printController;
						if ((printController = ((modelPrefab != null) ? modelPrefab.GetComponentInChildren<PrintController>() : null)) != null)
						{
							num = Mathf.Max(num, printController.printTime + 1f);
						}
						GameObject modelPrefab2 = CS$<>8__locals1.modelPanel.modelPrefab;
						TemporaryOverlay temporaryOverlay;
						if ((temporaryOverlay = ((modelPrefab2 != null) ? modelPrefab2.GetComponentInChildren<TemporaryOverlay>() : null)) != null)
						{
							num = Mathf.Max(num, temporaryOverlay.duration + 1f);
						}
					}
					catch (Exception message)
					{
						Debug.Log(message);
					}
					RoR2Application.onLateUpdate += CS$<>8__locals1.<GeneratePortraits>g__UpdateCamera|1;
					yield return new WaitForSeconds(num);
					CS$<>8__locals1.modelPanel.SetAnglesForCharacterThumbnail(true);
					yield return new WaitForEndOfFrame();
					yield return new WaitForEndOfFrame();
					try
					{
						Texture2D texture2D = new Texture2D(CS$<>8__locals1.modelPanel.renderTexture.width, CS$<>8__locals1.modelPanel.renderTexture.height, TextureFormat.ARGB32, false, false);
						RenderTexture active = RenderTexture.active;
						RenderTexture.active = CS$<>8__locals1.modelPanel.renderTexture;
						texture2D.ReadPixels(new Rect(0f, 0f, (float)CS$<>8__locals1.modelPanel.renderTexture.width, (float)CS$<>8__locals1.modelPanel.renderTexture.height), 0, 0, false);
						RenderTexture.active = active;
						byte[] array = texture2D.EncodeToPNG();
						using (FileStream fileStream = new FileStream("Assets/RoR2/GeneratedPortraits/" + BodyCatalog.bodyPrefabs[i].name + ".png", FileMode.Create, FileAccess.Write))
						{
							fileStream.Write(array, 0, array.Length);
						}
					}
					catch (Exception message2)
					{
						Debug.Log(message2);
					}
					RoR2Application.onLateUpdate -= CS$<>8__locals1.<GeneratePortraits>g__UpdateCamera|1;
					yield return new WaitForEndOfFrame();
				}
				num2 = i + 1;
			}
			UnityEngine.Object.Destroy(CS$<>8__locals1.modelPanel.transform.root.gameObject);
			Debug.Log("Portrait generation complete.");
			yield break;
		}

		// Token: 0x060016AE RID: 5806 RVA: 0x0006437C File Offset: 0x0006257C
		[ConCommand(commandName = "body_generate_portraits", flags = ConVarFlags.None, helpText = "Generates portraits for all bodies that are currently using the default.")]
		private static void CCBodyGeneratePortraits(ConCommandArgs args)
		{
			RoR2Application.instance.StartCoroutine(BodyCatalog.GeneratePortraits(args.TryGetArgBool(0) ?? false));
		}

		// Token: 0x060016AF RID: 5807 RVA: 0x000643B8 File Offset: 0x000625B8
		[ConCommand(commandName = "body_list", flags = ConVarFlags.None, helpText = "Prints a list of all character bodies in the game.")]
		private static void CCBodyList(ConCommandArgs args)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < BodyCatalog.bodyComponents.Length; i++)
			{
				stringBuilder.Append("[").Append(i).Append("]=").Append(BodyCatalog.bodyNames[i]).AppendLine();
			}
			Debug.Log(stringBuilder);
		}

		// Token: 0x060016B0 RID: 5808 RVA: 0x00064410 File Offset: 0x00062610
		[ConCommand(commandName = "body_reload_all", flags = ConVarFlags.Cheat, helpText = "Reloads all bodies and repopulates the BodyCatalog.")]
		private static void CCBodyReloadAll(ConCommandArgs args)
		{
			BodyCatalog.SetBodyPrefabs(Resources.LoadAll<GameObject>("Prefabs/CharacterBodies/"));
		}

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x060016B1 RID: 5809 RVA: 0x00064421 File Offset: 0x00062621
		// (remove) Token: 0x060016B2 RID: 5810 RVA: 0x000026ED File Offset: 0x000008ED
		[Obsolete("Use IContentPackProvider instead.")]
		public static event Action<List<GameObject>> getAdditionalEntries
		{
			add
			{
				LegacyModContentPackProvider.instance.HandleLegacyGetAdditionalEntries<GameObject>("RoR2.BodyCatalog.getAdditionalEntries", value, LegacyModContentPackProvider.instance.registrationContentPack.bodyPrefabs);
			}
			remove
			{
			}
		}

		// Token: 0x060016B4 RID: 5812 RVA: 0x0006445C File Offset: 0x0006265C
		[CompilerGenerated]
		internal static bool <GeneratePortraits>g__IconIsNotSuitable|30_0(Texture texture)
		{
			if (!texture)
			{
				return true;
			}
			string name = texture.name;
			return name == "texMysteryIcon" || name == "texNullIcon";
		}

		// Token: 0x04001C78 RID: 7288
		public static ResourceAvailability availability = default(ResourceAvailability);

		// Token: 0x04001C79 RID: 7289
		private static string[] bodyNames;

		// Token: 0x04001C7A RID: 7290
		private static GameObject[] bodyPrefabs;

		// Token: 0x04001C7B RID: 7291
		private static CharacterBody[] bodyPrefabBodyComponents;

		// Token: 0x04001C7C RID: 7292
		private static Component[][] bodyComponents;

		// Token: 0x04001C7D RID: 7293
		private static GenericSkill[][] skillSlots;

		// Token: 0x04001C7E RID: 7294
		private static SkinDef[][] skins;

		// Token: 0x04001C80 RID: 7296
		private static readonly Dictionary<string, BodyIndex> nameToIndexMap = new Dictionary<string, BodyIndex>();
	}
}
