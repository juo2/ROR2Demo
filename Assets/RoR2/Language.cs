using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Facepunch.Steamworks;
using JetBrains.Annotations;
using RoR2.ConVar;
using SimpleJSON;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000945 RID: 2373
	public class Language
	{
		// Token: 0x0600359F RID: 13727 RVA: 0x000E2898 File Offset: 0x000E0A98
		private Language()
		{
		}

		// Token: 0x060035A0 RID: 13728 RVA: 0x000E28C1 File Offset: 0x000E0AC1
		private Language(string name)
		{
			this.name = name;
		}

		// Token: 0x060035A1 RID: 13729 RVA: 0x000E28F4 File Offset: 0x000E0AF4
		private void SetFolders([NotNull] IEnumerable<string> newFolders)
		{
			this.folders = newFolders.ToArray<string>();
			this.foundManifest = false;
			for (int i = this.folders.Length - 1; i >= 0; i--)
			{
				string path = this.folders[i];
				if (Directory.Exists(path))
				{
					string text = Directory.EnumerateFiles(path, "language.json").FirstOrDefault<string>();
					this.foundManifest |= (text != null);
					if (text != null)
					{
						this.LoadManifest(text);
					}
					string text2 = Directory.EnumerateFiles(path, "icon.png").FirstOrDefault<string>();
					if (text2 != null)
					{
						this.SetIconSprite(Language.BuildSpriteFromTextureFile(text2));
						return;
					}
				}
			}
		}

		// Token: 0x060035A2 RID: 13730 RVA: 0x000E2987 File Offset: 0x000E0B87
		private void SetIconSprite(Sprite newIconSprite)
		{
			if (this.iconSprite)
			{
				UnityEngine.Object.Destroy(this.iconSprite.texture);
				UnityEngine.Object.Destroy(this.iconSprite);
			}
			this.iconSprite = newIconSprite;
		}

		// Token: 0x060035A3 RID: 13731 RVA: 0x000E29B8 File Offset: 0x000E0BB8
		private void LoadManifest(string file)
		{
			using (Stream stream = File.Open(file, FileMode.Open, FileAccess.Read))
			{
				using (StreamReader streamReader = new StreamReader(stream, Encoding.UTF8))
				{
					JSONNode jsonnode = JSON.Parse(streamReader.ReadToEnd());
					if (jsonnode != null)
					{
						JSONNode jsonnode2 = jsonnode["language"];
						if (jsonnode2 != null)
						{
							string text = jsonnode2["selfname"];
							if (text != null)
							{
								this.selfName = text;
							}
						}
					}
				}
			}
		}

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x060035A4 RID: 13732 RVA: 0x000E2A54 File Offset: 0x000E0C54
		// (set) Token: 0x060035A5 RID: 13733 RVA: 0x000E2A5C File Offset: 0x000E0C5C
		public string selfName { get; private set; } = string.Empty;

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x060035A6 RID: 13734 RVA: 0x000E2A65 File Offset: 0x000E0C65
		// (set) Token: 0x060035A7 RID: 13735 RVA: 0x000E2A6D File Offset: 0x000E0C6D
		public Sprite iconSprite { get; private set; }

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x060035A8 RID: 13736 RVA: 0x000E2A76 File Offset: 0x000E0C76
		// (set) Token: 0x060035A9 RID: 13737 RVA: 0x000E2A7E File Offset: 0x000E0C7E
		public bool stringsLoaded { get; private set; }

		// Token: 0x060035AA RID: 13738 RVA: 0x000E2A88 File Offset: 0x000E0C88
		[NotNull]
		public string GetLocalizedStringByToken([NotNull] string token)
		{
			if (Language.isDummyStringOverrideEnabled)
			{
				return Language.dummyString;
			}
			string result;
			if (this.stringsByToken.TryGetValue(token, out result))
			{
				return result;
			}
			if (this.fallbackLanguage != null)
			{
				return this.fallbackLanguage.GetLocalizedStringByToken(token);
			}
			return token;
		}

		// Token: 0x060035AB RID: 13739 RVA: 0x000E2ACA File Offset: 0x000E0CCA
		[NotNull]
		public string GetLocalizedFormattedStringByToken([NotNull] string token, params object[] args)
		{
			return string.Format(this.GetLocalizedStringByToken(token), args);
		}

		// Token: 0x060035AC RID: 13740 RVA: 0x000E2AD9 File Offset: 0x000E0CD9
		public void SetStringByToken([NotNull] string token, [NotNull] string localizedString)
		{
			this.stringsByToken[token] = localizedString;
		}

		// Token: 0x060035AD RID: 13741 RVA: 0x000E2AE8 File Offset: 0x000E0CE8
		public void SetStringsByTokens([NotNull] IEnumerable<KeyValuePair<string, string>> tokenPairs)
		{
			foreach (KeyValuePair<string, string> keyValuePair in tokenPairs)
			{
				this.SetStringByToken(keyValuePair.Key, keyValuePair.Value);
			}
		}

		// Token: 0x060035AE RID: 13742 RVA: 0x000E2B40 File Offset: 0x000E0D40
		public void LoadStrings()
		{
			if (this.stringsLoaded)
			{
				return;
			}
			this.stringsLoaded = true;
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			Language.LoadAllTokensFromFolders(this.folders, list);
			this.SetStringsByTokens(list);
		}

		// Token: 0x060035AF RID: 13743 RVA: 0x000E2B76 File Offset: 0x000E0D76
		public void UnloadStrings()
		{
			if (!this.stringsLoaded)
			{
				return;
			}
			this.stringsLoaded = false;
			this.stringsByToken.Clear();
		}

		// Token: 0x060035B0 RID: 13744 RVA: 0x000E2B93 File Offset: 0x000E0D93
		public bool TokenIsRegistered([NotNull] string token)
		{
			return this.stringsByToken.ContainsKey(token);
		}

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x060035B1 RID: 13745 RVA: 0x000E2BA1 File Offset: 0x000E0DA1
		public bool hasEntries
		{
			get
			{
				return this.stringsByToken.Count > 0;
			}
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x060035B2 RID: 13746 RVA: 0x000E2BB1 File Offset: 0x000E0DB1
		// (set) Token: 0x060035B3 RID: 13747 RVA: 0x000E2BB8 File Offset: 0x000E0DB8
		public static string currentLanguageName { get; private set; } = "";

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x060035B4 RID: 13748 RVA: 0x000E2BC0 File Offset: 0x000E0DC0
		// (set) Token: 0x060035B5 RID: 13749 RVA: 0x000E2BC7 File Offset: 0x000E0DC7
		public static Language currentLanguage { get; private set; } = null;

		// Token: 0x140000B6 RID: 182
		// (add) Token: 0x060035B6 RID: 13750 RVA: 0x000E2BD0 File Offset: 0x000E0DD0
		// (remove) Token: 0x060035B7 RID: 13751 RVA: 0x000E2C04 File Offset: 0x000E0E04
		public static event Action onCurrentLanguageChanged;

		// Token: 0x140000B7 RID: 183
		// (add) Token: 0x060035B8 RID: 13752 RVA: 0x000E2C38 File Offset: 0x000E0E38
		// (remove) Token: 0x060035B9 RID: 13753 RVA: 0x000E2C6C File Offset: 0x000E0E6C
		public static event Action<List<string>> collectLanguageRootFolders;

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x060035BA RID: 13754 RVA: 0x000E2C9F File Offset: 0x000E0E9F
		// (set) Token: 0x060035BB RID: 13755 RVA: 0x000E2CA6 File Offset: 0x000E0EA6
		public static Language english { get; private set; }

		// Token: 0x060035BC RID: 13756 RVA: 0x000E2CB0 File Offset: 0x000E0EB0
		[CanBeNull]
		public static Language FindLanguageByName([NotNull] string languageName)
		{
			Language result;
			if (Language.languagesByName.TryGetValue(languageName, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x060035BD RID: 13757 RVA: 0x000E2CCF File Offset: 0x000E0ECF
		[NotNull]
		public static string GetString([NotNull] string token, [NotNull] string language)
		{
			Language language2 = Language.FindLanguageByName(language);
			return ((language2 != null) ? language2.GetLocalizedStringByToken(token) : null) ?? token;
		}

		// Token: 0x060035BE RID: 13758 RVA: 0x000E2CE9 File Offset: 0x000E0EE9
		[NotNull]
		public static string GetString([NotNull] string token)
		{
			Language currentLanguage = Language.currentLanguage;
			return ((currentLanguage != null) ? currentLanguage.GetLocalizedStringByToken(token) : null) ?? token;
		}

		// Token: 0x060035BF RID: 13759 RVA: 0x000E2D02 File Offset: 0x000E0F02
		[NotNull]
		public static string GetStringFormatted([NotNull] string token, params object[] args)
		{
			Language currentLanguage = Language.currentLanguage;
			return ((currentLanguage != null) ? currentLanguage.GetLocalizedFormattedStringByToken(token, args) : null) ?? string.Format(token, args);
		}

		// Token: 0x060035C0 RID: 13760 RVA: 0x000E2D22 File Offset: 0x000E0F22
		public static bool IsTokenInvalid([NotNull] string token)
		{
			Language currentLanguage = Language.currentLanguage;
			return currentLanguage == null || !currentLanguage.TokenIsRegistered(token);
		}

		// Token: 0x060035C1 RID: 13761 RVA: 0x000E2D39 File Offset: 0x000E0F39
		public static IEnumerable<Language> GetAllLanguages()
		{
			return Language.languagesByName.Values;
		}

		// Token: 0x060035C2 RID: 13762 RVA: 0x000E2D48 File Offset: 0x000E0F48
		[NotNull]
		private static Language GetOrCreateLanguage([NotNull] string languageName)
		{
			Language result;
			if (!Language.languagesByName.TryGetValue(languageName, out result))
			{
				result = (Language.languagesByName[languageName] = new Language(languageName));
			}
			return result;
		}

		// Token: 0x060035C3 RID: 13763 RVA: 0x000E2D78 File Offset: 0x000E0F78
		private static void LoadAllTokensFromFolders([NotNull] IEnumerable<string> folders, [NotNull] List<KeyValuePair<string, string>> output)
		{
			foreach (string folder in folders)
			{
				Language.LoadAllTokensFromFolder(folder, output);
			}
		}

		// Token: 0x060035C4 RID: 13764 RVA: 0x000E2DC0 File Offset: 0x000E0FC0
		private static void LoadAllTokensFromFolder([NotNull] string folder, [NotNull] List<KeyValuePair<string, string>> output)
		{
			PlatformSystems.textDataManager.GetLocFiles(folder, delegate(string[] contents)
			{
				int num = contents.Length;
				for (int i = 0; i < num; i++)
				{
					Language.LoadTokensFromData(contents[i], output);
				}
			});
		}

		// Token: 0x060035C5 RID: 13765 RVA: 0x000E2DF4 File Offset: 0x000E0FF4
		private static void LoadTokensFromData([NotNull] string contents, [NotNull] List<KeyValuePair<string, string>> output)
		{
			JSONNode jsonnode = JSON.Parse(contents);
			if (jsonnode != null)
			{
				JSONNode jsonnode2 = jsonnode["strings"];
				if (jsonnode2 != null)
				{
					foreach (string text in jsonnode2.Keys)
					{
						output.Add(new KeyValuePair<string, string>(text, jsonnode2[text].Value));
					}
				}
			}
		}

		// Token: 0x060035C6 RID: 13766 RVA: 0x000E2E78 File Offset: 0x000E1078
		private static void LoadTokensFromFile([NotNull] string file, [NotNull] List<KeyValuePair<string, string>> output)
		{
			using (Stream stream = File.Open(file, FileMode.Open, FileAccess.Read))
			{
				using (StreamReader streamReader = new StreamReader(stream, Encoding.UTF8))
				{
					JSONNode jsonnode = JSON.Parse(streamReader.ReadToEnd());
					if (jsonnode != null)
					{
						JSONNode jsonnode2 = jsonnode["strings"];
						if (jsonnode2 != null)
						{
							foreach (string text in jsonnode2.Keys)
							{
								output.Add(new KeyValuePair<string, string>(text, jsonnode2[text].Value));
							}
						}
					}
				}
			}
		}

		// Token: 0x060035C7 RID: 13767 RVA: 0x000E2F4C File Offset: 0x000E114C
		[NotNull]
		private static List<string> GetLanguageRootFolders()
		{
			List<string> list = new List<string>();
			try
			{
				Action<List<string>> action = Language.collectLanguageRootFolders;
				if (action != null)
				{
					action(list);
				}
			}
			catch (Exception ex)
			{
				Debug.LogErrorFormat("Encountered error loading language folders: {0}", new object[]
				{
					ex
				});
			}
			return list;
		}

		// Token: 0x060035C8 RID: 13768 RVA: 0x000E2F9C File Offset: 0x000E119C
		private static void BuildLanguagesFromFolders()
		{
			foreach (IGrouping<string, string> grouping in Language.GetLanguageRootFolders().SelectMany((string languageRootFolder) => Directory.EnumerateDirectories(languageRootFolder)).GroupBy((string languageRootFolder) => new DirectoryInfo(languageRootFolder).Name, StringComparer.OrdinalIgnoreCase).ToArray<IGrouping<string, string>>())
			{
				Language orCreateLanguage = Language.GetOrCreateLanguage(grouping.Key);
				orCreateLanguage.SetFolders(grouping);
				if (!orCreateLanguage.foundManifest)
				{
					Language.languagesByName.Remove(grouping.Key);
				}
			}
		}

		// Token: 0x060035C9 RID: 13769 RVA: 0x000E303D File Offset: 0x000E123D
		private static bool IsAnyLanguageLoaded()
		{
			return Language.languagesByName.Count > 0;
		}

		// Token: 0x060035CA RID: 13770 RVA: 0x000E304C File Offset: 0x000E124C
		private static void SetCurrentLanguage([NotNull] string newCurrentLanguageName)
		{
			Debug.LogFormat("Setting current language to \"{0}\"", new object[]
			{
				newCurrentLanguageName
			});
			if (Language.currentLanguage != Language.english)
			{
				Language currentLanguage = Language.currentLanguage;
				if (currentLanguage != null)
				{
					currentLanguage.UnloadStrings();
				}
			}
			Language.currentLanguageName = newCurrentLanguageName;
			Language.currentLanguage = Language.FindLanguageByName(Language.currentLanguageName);
			if (Language.currentLanguage == null && string.Compare(Language.currentLanguageName, "en", StringComparison.OrdinalIgnoreCase) != 0)
			{
				Debug.LogFormat("Could not load files for language \"{0}\". Falling back to \"en\".", new object[]
				{
					newCurrentLanguageName
				});
				Language.currentLanguageName = "en";
				Language.currentLanguage = Language.FindLanguageByName(Language.currentLanguageName);
			}
			Language currentLanguage2 = Language.currentLanguage;
			if (currentLanguage2 != null)
			{
				currentLanguage2.LoadStrings();
			}
			Action action = Language.onCurrentLanguageChanged;
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x060035CB RID: 13771 RVA: 0x000E3104 File Offset: 0x000E1304
		public static void Init()
		{
			Language.BuildLanguagesFromFolders();
			if (Language.LanguageConVar.instance != null)
			{
				Language.LanguageConVar.instance.SetString(Language.LanguageConVar.instance.GetString());
			}
			Language.english = Language.GetOrCreateLanguage("en");
			Language.english.LoadStrings();
			foreach (Language language in Language.GetAllLanguages())
			{
				if (language != Language.english)
				{
					language.fallbackLanguage = Language.english;
				}
			}
		}

		// Token: 0x060035CC RID: 13772 RVA: 0x000E3198 File Offset: 0x000E1398
		private static Sprite BuildSpriteFromTextureFile(string file)
		{
			Texture2D texture2D = new Texture2D(2, 2, TextureFormat.ARGB32, false, false);
			texture2D.wrapMode = TextureWrapMode.Clamp;
			texture2D.LoadImage(File.ReadAllBytes(file), true);
			return Sprite.Create(texture2D, new Rect(0f, 0f, (float)texture2D.width, (float)texture2D.height), new Vector2(0.5f, 0.5f), 1f, 1U, SpriteMeshType.FullRect, Vector4.zero);
		}

		// Token: 0x060035CD RID: 13773 RVA: 0x000E3204 File Offset: 0x000E1404
		[CanBeNull]
		public static string GetPlatformLanguageName()
		{
			Client instance = Client.Instance;
			string text = (instance != null) ? instance.CurrentLanguage : null;
			if (text == null)
			{
				return null;
			}
			Language.SteamLanguageDef steamLanguageDef;
			if (Language.steamLanguageTable.TryGetValue(text, out steamLanguageDef))
			{
				return steamLanguageDef.webApiName;
			}
			return null;
		}

		// Token: 0x060035CE RID: 13774 RVA: 0x000E323F File Offset: 0x000E143F
		[ConCommand(commandName = "language_reload", flags = ConVarFlags.None, helpText = "Reloads the current language.")]
		public static void CCLanguageReload(ConCommandArgs args)
		{
			Language.SetCurrentLanguage(Language.currentLanguageName);
		}

		// Token: 0x060035CF RID: 13775 RVA: 0x000E324C File Offset: 0x000E144C
		[ConCommand(commandName = "language_dump_to_json", flags = ConVarFlags.None, helpText = "Combines all files for the given language into a single JSON file.")]
		private static void CCLanguageDumpToJson(ConCommandArgs args)
		{
			string argString = args.GetArgString(0);
			Language language = Language.FindLanguageByName(argString);
			if (language == null)
			{
				throw new ConCommandException(string.Format("'{0}' is not a valid language name.", argString));
			}
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			Language.LoadAllTokensFromFolders(language.folders, list);
			new StringBuilder();
			JSONNode jsonnode = new JSONObject();
			JSONNode jsonnode2 = jsonnode["strings"] = new JSONObject();
			foreach (KeyValuePair<string, string> keyValuePair in list)
			{
				jsonnode2[keyValuePair.Key] = keyValuePair.Value;
			}
			File.WriteAllText("output.json", jsonnode.ToString(1), Encoding.UTF8);
		}

		// Token: 0x060035D0 RID: 13776 RVA: 0x000E3318 File Offset: 0x000E1518
		[ConCommand(commandName = "language_dummy_strings", flags = ConVarFlags.None, helpText = "Toggles use of a dummy string for all text")]
		private static void CCLanguageDummyStringsToggle(ConCommandArgs args)
		{
			Language.isDummyStringOverrideEnabled = !Language.isDummyStringOverrideEnabled;
			Debug.Log(string.Format("isDummyStringOverrideEnabled={0}", Language.isDummyStringOverrideEnabled));
		}

		// Token: 0x060035D1 RID: 13777 RVA: 0x000E3340 File Offset: 0x000E1540
		// Note: this type is marked as 'beforefieldinit'.
		static Language()
		{
			Dictionary<string, Language.SteamLanguageDef> dictionary = new Dictionary<string, Language.SteamLanguageDef>(StringComparer.OrdinalIgnoreCase);
			dictionary["arabic"] = new Language.SteamLanguageDef("Arabic", "العربية", "arabic", "ar");
			dictionary["bulgarian"] = new Language.SteamLanguageDef("Bulgarian", "български език", "bulgarian", "bg");
			dictionary["schinese"] = new Language.SteamLanguageDef("Chinese (Simplified)", "简体中文", "schinese", "zh-CN");
			dictionary["tchinese"] = new Language.SteamLanguageDef("Chinese (Traditional)", "繁體中文", "tchinese", "zh-TW");
			dictionary["czech"] = new Language.SteamLanguageDef("Czech", "čeština", "czech", "cs");
			dictionary["danish"] = new Language.SteamLanguageDef("Danish", "Dansk", "danish", "da");
			dictionary["dutch"] = new Language.SteamLanguageDef("Dutch", "Nederlands", "dutch", "nl");
			dictionary["english"] = new Language.SteamLanguageDef("English", "English", "english", "en");
			dictionary["finnish"] = new Language.SteamLanguageDef("Finnish", "Suomi", "finnish", "fi");
			dictionary["french"] = new Language.SteamLanguageDef("French", "Français", "french", "fr");
			dictionary["german"] = new Language.SteamLanguageDef("German", "Deutsch", "german", "de");
			dictionary["greek"] = new Language.SteamLanguageDef("Greek", "Ελληνικά", "greek", "el");
			dictionary["hungarian"] = new Language.SteamLanguageDef("Hungarian", "Magyar", "hungarian", "hu");
			dictionary["italian"] = new Language.SteamLanguageDef("Italian", "Italiano", "italian", "it");
			dictionary["japanese"] = new Language.SteamLanguageDef("Japanese", "日本語", "japanese", "ja");
			dictionary["koreana"] = new Language.SteamLanguageDef("Korean", "한국어", "koreana", "ko");
			dictionary["korean"] = new Language.SteamLanguageDef("Korean", "한국어", "korean", "ko");
			dictionary["norwegian"] = new Language.SteamLanguageDef("Norwegian", "Norsk", "norwegian", "no");
			dictionary["polish"] = new Language.SteamLanguageDef("Polish", "Polski", "polish", "pl");
			dictionary["portuguese"] = new Language.SteamLanguageDef("Portuguese", "Português", "portuguese", "pt");
			dictionary["brazilian"] = new Language.SteamLanguageDef("Portuguese-Brazil", "Português-Brasil", "brazilian", "pt-BR");
			dictionary["romanian"] = new Language.SteamLanguageDef("Romanian", "Română", "romanian", "ro");
			dictionary["russian"] = new Language.SteamLanguageDef("Russian", "Русский", "russian", "ru");
			dictionary["spanish"] = new Language.SteamLanguageDef("Spanish-Spain", "Español-España", "spanish", "es");
			dictionary["latam"] = new Language.SteamLanguageDef("Spanish-Latin America", "Español-Latinoamérica", "latam", "es-419");
			dictionary["swedish"] = new Language.SteamLanguageDef("Swedish", "Svenska", "swedish", "sv");
			dictionary["thai"] = new Language.SteamLanguageDef("Thai", "ไทย", "thai", "th");
			dictionary["turkish"] = new Language.SteamLanguageDef("Turkish", "Türkçe", "turkish", "tr");
			dictionary["ukrainian"] = new Language.SteamLanguageDef("Ukrainian", "Українська", "ukrainian", "uk");
			dictionary["vietnamese"] = new Language.SteamLanguageDef("Vietnamese", "Tiếng Việt", "vietnamese", "vn");
			Language.steamLanguageTable = dictionary;
		}

		// Token: 0x04003664 RID: 13924
		private static readonly Dictionary<string, Language> languagesByName = new Dictionary<string, Language>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x04003665 RID: 13925
		private static bool isDummyStringOverrideEnabled = false;

		// Token: 0x04003666 RID: 13926
		private static string dummyString = ".•.";

		// Token: 0x04003667 RID: 13927
		private readonly Dictionary<string, string> stringsByToken = new Dictionary<string, string>();

		// Token: 0x04003668 RID: 13928
		private Language fallbackLanguage;

		// Token: 0x04003669 RID: 13929
		private string[] folders = Array.Empty<string>();

		// Token: 0x0400366A RID: 13930
		private bool foundManifest;

		// Token: 0x0400366B RID: 13931
		public readonly string name;

		// Token: 0x04003674 RID: 13940
		private static readonly Dictionary<string, Language.SteamLanguageDef> steamLanguageTable;

		// Token: 0x02000946 RID: 2374
		private struct SteamLanguageDef
		{
			// Token: 0x060035D2 RID: 13778 RVA: 0x000E37C3 File Offset: 0x000E19C3
			public SteamLanguageDef(string englishName, string nativeName, string apiName, string webApiName)
			{
				this.englishName = englishName;
				this.nativeName = nativeName;
				this.apiName = apiName;
				this.webApiName = webApiName;
			}

			// Token: 0x04003675 RID: 13941
			public readonly string englishName;

			// Token: 0x04003676 RID: 13942
			public readonly string nativeName;

			// Token: 0x04003677 RID: 13943
			public readonly string apiName;

			// Token: 0x04003678 RID: 13944
			public readonly string webApiName;
		}

		// Token: 0x02000947 RID: 2375
		public class LanguageConVar : BaseConVar
		{
			// Token: 0x060035D3 RID: 13779 RVA: 0x000E37E2 File Offset: 0x000E19E2
			public LanguageConVar(string name, ConVarFlags flags, string defaultValue, string helpText) : base(name, flags, defaultValue, helpText)
			{
			}

			// Token: 0x060035D4 RID: 13780 RVA: 0x000E37FC File Offset: 0x000E19FC
			public override void SetString(string newValue)
			{
				if (!Language.IsAnyLanguageLoaded())
				{
					this.internalValue = newValue;
					return;
				}
				if (string.Equals(newValue, "EN_US", StringComparison.Ordinal) || Language.FindLanguageByName(newValue) == null)
				{
					newValue = Language.LanguageConVar.platformString;
				}
				this.internalValue = newValue;
				if (string.Equals(this.internalValue, Language.LanguageConVar.platformString, StringComparison.Ordinal))
				{
					newValue = (Language.GetPlatformLanguageName() ?? "en");
				}
				Language.SetCurrentLanguage(newValue);
			}

			// Token: 0x060035D5 RID: 13781 RVA: 0x000E3865 File Offset: 0x000E1A65
			public override string GetString()
			{
				return this.internalValue;
			}

			// Token: 0x04003679 RID: 13945
			private static readonly string platformString = "platform";

			// Token: 0x0400367A RID: 13946
			public static Language.LanguageConVar instance = new Language.LanguageConVar("language", ConVarFlags.Archive, Language.LanguageConVar.platformString, "Which language to use.");

			// Token: 0x0400367B RID: 13947
			private string internalValue = Language.LanguageConVar.platformString;
		}
	}
}
