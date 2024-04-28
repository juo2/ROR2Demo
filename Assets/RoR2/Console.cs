using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using HG;
using HG.Reflection;
using RoR2.ConVar;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x02000671 RID: 1649
	public class Console : MonoBehaviour
	{
		// Token: 0x1700028A RID: 650
		// (get) Token: 0x0600202A RID: 8234 RVA: 0x0008A355 File Offset: 0x00088555
		// (set) Token: 0x0600202B RID: 8235 RVA: 0x0008A35C File Offset: 0x0008855C
		public static Console instance { get; private set; }

		// Token: 0x0600202C RID: 8236 RVA: 0x0008A364 File Offset: 0x00088564
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void RegisterLogHandler()
		{
			Application.logMessageReceived += Console.HandleLog;
		}

		// Token: 0x0600202D RID: 8237 RVA: 0x0008A378 File Offset: 0x00088578
		private static void HandleLog(string message, string stackTrace, LogType logType)
		{
			if (logType == LogType.Error)
			{
				message = string.Format(CultureInfo.InvariantCulture, "<color=#FF0000>{0}</color>", message);
			}
			else if (logType == LogType.Warning)
			{
				message = string.Format(CultureInfo.InvariantCulture, "<color=#FFFF00>{0}</color>", message);
			}
			Console.Log log = new Console.Log
			{
				message = message,
				stackTrace = stackTrace,
				logType = logType
			};
			Console.logs.Add(log);
			if (Console.maxMessages.value > 0)
			{
				while (Console.logs.Count > Console.maxMessages.value)
				{
					Console.logs.RemoveAt(0);
				}
			}
			if (Console.onLogReceived != null)
			{
				Console.onLogReceived(log);
			}
		}

		// Token: 0x1400003F RID: 63
		// (add) Token: 0x0600202E RID: 8238 RVA: 0x0008A424 File Offset: 0x00088624
		// (remove) Token: 0x0600202F RID: 8239 RVA: 0x0008A458 File Offset: 0x00088658
		public static event Console.LogReceivedDelegate onLogReceived;

		// Token: 0x14000040 RID: 64
		// (add) Token: 0x06002030 RID: 8240 RVA: 0x0008A48C File Offset: 0x0008868C
		// (remove) Token: 0x06002031 RID: 8241 RVA: 0x0008A4C0 File Offset: 0x000886C0
		public static event Action onClear;

		// Token: 0x06002032 RID: 8242 RVA: 0x0008A4F4 File Offset: 0x000886F4
		private string GetVstrValue(LocalUser user, string identifier)
		{
			string result;
			if (user != null)
			{
				result = "";
				return result;
			}
			if (this.vstrs.TryGetValue(identifier, out result))
			{
				return result;
			}
			return "";
		}

		// Token: 0x06002033 RID: 8243 RVA: 0x0008A524 File Offset: 0x00088724
		private void InitConVars()
		{
			this.allConVars = new Dictionary<string, BaseConVar>();
			this.archiveConVars = new List<BaseConVar>();
			foreach (Type type in typeof(BaseConVar).Assembly.GetTypes())
			{
				foreach (FieldInfo fieldInfo in type.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
				{
					if (fieldInfo.FieldType.IsSubclassOf(typeof(BaseConVar)))
					{
						if (fieldInfo.IsStatic)
						{
							BaseConVar conVar = (BaseConVar)fieldInfo.GetValue(null);
							this.RegisterConVarInternal(conVar);
						}
						else if (type.GetCustomAttribute<CompilerGeneratedAttribute>() == null)
						{
							Debug.LogErrorFormat("ConVar defined as {0}.{1} could not be registered. ConVars must be static fields.", new object[]
							{
								type.Name,
								fieldInfo.Name
							});
						}
					}
				}
				foreach (MethodInfo methodInfo in type.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
				{
					if (methodInfo.GetCustomAttribute<ConVarProviderAttribute>() != null)
					{
						if (methodInfo.ReturnType != typeof(IEnumerable<BaseConVar>) || methodInfo.GetParameters().Length != 0)
						{
							Debug.LogErrorFormat("ConVar provider {0}.{1} does not match the signature \"static IEnumerable<ConVar.BaseConVar>()\".", new object[]
							{
								type.Name,
								methodInfo.Name
							});
						}
						else if (!methodInfo.IsStatic)
						{
							Debug.LogErrorFormat("ConVar provider {0}.{1} could not be invoked. Methods marked with the ConVarProvider attribute must be static.", new object[]
							{
								type.Name,
								methodInfo.Name
							});
						}
						else
						{
							foreach (BaseConVar conVar2 in ((IEnumerable<BaseConVar>)methodInfo.Invoke(null, Array.Empty<object>())))
							{
								this.RegisterConVarInternal(conVar2);
							}
						}
					}
				}
			}
			foreach (KeyValuePair<string, BaseConVar> keyValuePair in this.allConVars)
			{
				try
				{
					BaseConVar value = keyValuePair.Value;
					if ((value.flags & ConVarFlags.Engine) != ConVarFlags.None)
					{
						value.defaultValue = value.GetString();
					}
					else if (value.defaultValue != null)
					{
						value.AttemptSetString(value.defaultValue);
					}
				}
				catch (Exception message)
				{
					Debug.LogError(message);
				}
			}
		}

		// Token: 0x06002034 RID: 8244 RVA: 0x0008A788 File Offset: 0x00088988
		private void RegisterConVarInternal(BaseConVar conVar)
		{
			if (conVar == null)
			{
				Debug.LogWarning("Attempted to register null ConVar");
				return;
			}
			this.allConVars[conVar.name] = conVar;
			if ((conVar.flags & ConVarFlags.Archive) != ConVarFlags.None)
			{
				this.archiveConVars.Add(conVar);
			}
		}

		// Token: 0x06002035 RID: 8245 RVA: 0x0008A7C0 File Offset: 0x000889C0
		public BaseConVar FindConVar(string name)
		{
			BaseConVar result;
			if (this.allConVars.TryGetValue(name, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06002036 RID: 8246 RVA: 0x0008A7E0 File Offset: 0x000889E0
		public void SubmitCmd(NetworkUser sender, string cmd, bool recordSubmit = false)
		{
			this.SubmitCmd(sender, cmd, recordSubmit);
		}

		// Token: 0x06002037 RID: 8247 RVA: 0x0008A7F0 File Offset: 0x000889F0
		public void SubmitCmd(Console.CmdSender sender, string cmd, bool recordSubmit = false)
		{
			if (recordSubmit)
			{
				Console.Log log = new Console.Log
				{
					message = string.Format(CultureInfo.InvariantCulture, "<color=#C0C0C0>] {0}</color>", cmd),
					stackTrace = "",
					logType = LogType.Log
				};
				Console.logs.Add(log);
				if (Console.onLogReceived != null)
				{
					Console.onLogReceived(log);
				}
				Console.userCmdHistory.Add(cmd);
			}
			Queue<string> tokens = new Console.Lexer(cmd).GetTokens();
			List<string> list = new List<string>();
			bool flag = false;
			while (tokens.Count != 0)
			{
				string text = tokens.Dequeue();
				if (text == ";")
				{
					flag = false;
					if (list.Count > 0)
					{
						string concommandName = list[0].ToLower(CultureInfo.InvariantCulture);
						list.RemoveAt(0);
						this.RunCmd(sender, concommandName, list);
						list.Clear();
					}
				}
				else
				{
					if (flag)
					{
						text = this.GetVstrValue(sender.localUser, text);
						flag = false;
					}
					if (text == "vstr")
					{
						flag = true;
					}
					else
					{
						list.Add(text);
					}
				}
			}
		}

		// Token: 0x06002038 RID: 8248 RVA: 0x0008A8FB File Offset: 0x00088AFB
		private void ForwardCmdToServer(ConCommandArgs args)
		{
			if (!args.sender)
			{
				return;
			}
			args.sender.CallCmdSendConsoleCommand(args.commandName, args.userArgs.ToArray());
		}

		// Token: 0x06002039 RID: 8249 RVA: 0x0008A927 File Offset: 0x00088B27
		public void RunClientCmd(NetworkUser sender, string concommandName, string[] args)
		{
			this.RunCmd(sender, concommandName, new List<string>(args));
		}

		// Token: 0x0600203A RID: 8250 RVA: 0x0008A93C File Offset: 0x00088B3C
		private void RunCmd(Console.CmdSender sender, string concommandName, List<string> userArgs)
		{
			NetworkUser networkUser = sender.networkUser;
			bool flag = networkUser != null && !networkUser.isLocalPlayer;
			Console.ConCommand conCommand = null;
			BaseConVar baseConVar = null;
			ConVarFlags flags;
			if (this.concommandCatalog.TryGetValue(concommandName, out conCommand))
			{
				flags = conCommand.flags;
			}
			else
			{
				baseConVar = this.FindConVar(concommandName);
				if (baseConVar == null)
				{
					Debug.LogFormat("\"{0}\" is not a recognized ConCommand or ConVar.", new object[]
					{
						concommandName
					});
					return;
				}
				flags = baseConVar.flags;
			}
			bool flag2 = (flags & ConVarFlags.ExecuteOnServer) > ConVarFlags.None;
			if (!NetworkServer.active && flag2)
			{
				this.ForwardCmdToServer(new ConCommandArgs
				{
					sender = sender.networkUser,
					commandName = concommandName,
					userArgs = userArgs
				});
				return;
			}
			if (flag && (flags & ConVarFlags.SenderMustBeServer) != ConVarFlags.None)
			{
				Debug.LogFormat("Blocked server-only command {0} from remote user {1}.", new object[]
				{
					concommandName,
					sender.networkUser.userName
				});
				return;
			}
			if (flag && !flag2)
			{
				Debug.LogFormat("Blocked non-transmittable command {0} from remote user {1}.", new object[]
				{
					concommandName,
					sender.networkUser.userName
				});
				return;
			}
			if ((flags & ConVarFlags.Cheat) != ConVarFlags.None && !Console.CheatsConVar.instance.boolValue)
			{
				Debug.LogFormat("Command \"{0}\" cannot be used while cheats are disabled.", new object[]
				{
					concommandName
				});
				return;
			}
			if (conCommand != null)
			{
				try
				{
					conCommand.action(new ConCommandArgs
					{
						sender = sender.networkUser,
						localUserSender = sender.localUser,
						commandName = concommandName,
						userArgs = userArgs
					});
				}
				catch (ConCommandException ex)
				{
					Debug.LogFormat("Command \"{0}\" failed: {1}", new object[]
					{
						concommandName,
						ex.Message
					});
				}
				return;
			}
			if (baseConVar == null)
			{
				return;
			}
			if (userArgs.Count > 0)
			{
				baseConVar.AttemptSetString(userArgs[0]);
				return;
			}
			Debug.LogFormat("\"{0}\" = \"{1}\"\n{2}", new object[]
			{
				concommandName,
				baseConVar.GetString(),
				baseConVar.helpText
			});
		}

		// Token: 0x0600203B RID: 8251
		[DllImport("kernel32.dll")]
		private static extern bool AllocConsole();

		// Token: 0x0600203C RID: 8252
		[DllImport("kernel32.dll")]
		private static extern bool FreeConsole();

		// Token: 0x0600203D RID: 8253
		[DllImport("kernel32.dll")]
		private static extern bool AttachConsole(int processId);

		// Token: 0x0600203E RID: 8254
		[DllImport("user32.dll")]
		private static extern bool PostMessage(IntPtr hWnd, uint msg, int wParam, int lParam);

		// Token: 0x0600203F RID: 8255
		[DllImport("kernel32.dll")]
		private static extern IntPtr GetConsoleWindow();

		// Token: 0x06002040 RID: 8256 RVA: 0x0008AB20 File Offset: 0x00088D20
		private static string ReadInputStream()
		{
			if (Console.stdInQueue.Count > 0)
			{
				return Console.stdInQueue.Dequeue();
			}
			return null;
		}

		// Token: 0x06002041 RID: 8257 RVA: 0x0008AB3C File Offset: 0x00088D3C
		private static void ThreadedInputQueue()
		{
			string item;
			while (Console.systemConsoleType != Console.SystemConsoleType.None && (item = Console.ReadLine()) != null)
			{
				Console.stdInQueue.Enqueue(item);
			}
		}

		// Token: 0x06002042 RID: 8258 RVA: 0x0008AB68 File Offset: 0x00088D68
		private static void SetupSystemConsole()
		{
			bool flag = false;
			bool flag2 = false;
			string[] commandLineArgs = Environment.GetCommandLineArgs();
			for (int i = 0; i < commandLineArgs.Length; i++)
			{
				if (commandLineArgs[i] == "-console")
				{
					flag |= true;
				}
				if (commandLineArgs[i] == "-console_detach")
				{
					flag2 |= true;
				}
			}
			if (flag)
			{
				Console.systemConsoleType = Console.SystemConsoleType.Attach;
				if (flag2)
				{
					Console.systemConsoleType = Console.SystemConsoleType.Alloc;
				}
			}
			switch (Console.systemConsoleType)
			{
			case Console.SystemConsoleType.Attach:
				Console.AttachConsole(-1);
				break;
			case Console.SystemConsoleType.Alloc:
				Console.AllocConsole();
				break;
			}
			if (Console.systemConsoleType != Console.SystemConsoleType.None)
			{
				Console.SetIn(new StreamReader(Console.OpenStandardInput()));
				Console.stdInReaderThread = new Thread(new ThreadStart(Console.ThreadedInputQueue));
				Console.stdInReaderThread.Start();
			}
		}

		// Token: 0x06002043 RID: 8259 RVA: 0x0008AC28 File Offset: 0x00088E28
		private void Awake()
		{
			Console.instance = this;
			Console.SetupSystemConsole();
			this.InitConVars();
			foreach (SearchableAttribute searchableAttribute in SearchableAttribute.GetInstances<ConCommandAttribute>())
			{
				ConCommandAttribute conCommandAttribute = (ConCommandAttribute)searchableAttribute;
				this.concommandCatalog[conCommandAttribute.commandName.ToLower(CultureInfo.InvariantCulture)] = new Console.ConCommand
				{
					flags = conCommandAttribute.flags,
					action = (Console.ConCommandDelegate)Delegate.CreateDelegate(typeof(Console.ConCommandDelegate), conCommandAttribute.target as MethodInfo),
					helpText = conCommandAttribute.helpText
				};
			}
			string[] commandLineArgs = Environment.GetCommandLineArgs();
			StringBuilder stringBuilder = HG.StringBuilderPool.RentStringBuilder();
			stringBuilder.AppendLine("Launch Parameters: ");
			for (int i = 0; i < commandLineArgs.Length; i++)
			{
				stringBuilder.Append("  arg[").AppendInt(i, 1U, uint.MaxValue).Append("]=\"").Append(commandLineArgs[i]).Append("\"").AppendLine();
			}
			Debug.Log(stringBuilder);
			stringBuilder = HG.StringBuilderPool.ReturnStringBuilder(stringBuilder);
			MPEventSystemManager.availability.CallWhenAvailable(new Action(this.LoadStartupConfigs));
		}

		// Token: 0x06002044 RID: 8260 RVA: 0x0008AD6C File Offset: 0x00088F6C
		private void LoadStartupConfigs()
		{
			try
			{
				this.SubmitCmd(null, "exec config", false);
				this.SubmitCmd(null, "exec autoexec", false);
			}
			catch (Exception message)
			{
				Debug.LogError(message);
			}
		}

		// Token: 0x06002045 RID: 8261 RVA: 0x0008ADAC File Offset: 0x00088FAC
		private void Update()
		{
			string cmd;
			while ((cmd = Console.ReadInputStream()) != null)
			{
				this.SubmitCmd(null, cmd, true);
			}
		}

		// Token: 0x06002046 RID: 8262 RVA: 0x0008ADD0 File Offset: 0x00088FD0
		private void OnDestroy()
		{
			if (Console.stdInReaderThread != null)
			{
				Console.stdInReaderThread = null;
			}
			if (Console.systemConsoleType != Console.SystemConsoleType.None)
			{
				Console.systemConsoleType = Console.SystemConsoleType.None;
				IntPtr consoleWindow = Console.GetConsoleWindow();
				if (consoleWindow != IntPtr.Zero)
				{
					Console.PostMessage(consoleWindow, 256U, 13, 0);
				}
				if (Console.stdInReaderThread != null)
				{
					Console.stdInReaderThread.Join();
					Console.stdInReaderThread = null;
				}
				Console.FreeConsole();
			}
		}

		// Token: 0x06002047 RID: 8263 RVA: 0x0008AE38 File Offset: 0x00089038
		private static string LoadConfig(string fileName)
		{
			string text = Console.sharedStringBuilder.Clear().Append("/Config/").Append(fileName).Append(".cfg").ToString();
			try
			{
				return PlatformSystems.textDataManager.GetConfFile(fileName, text);
			}
			catch (IOException ex)
			{
				Debug.LogFormat("Could not load config {0}: {1}", new object[]
				{
					text,
					ex.Message
				});
			}
			return null;
		}

		// Token: 0x06002048 RID: 8264 RVA: 0x0008AEB4 File Offset: 0x000890B4
		public void SaveArchiveConVars()
		{
			Debug.Log("in save archive convars");
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (TextWriter textWriter = new StreamWriter(memoryStream, Encoding.UTF8))
				{
					for (int i = 0; i < this.archiveConVars.Count; i++)
					{
						BaseConVar baseConVar = this.archiveConVars[i];
						textWriter.Write(baseConVar.name);
						textWriter.Write(" ");
						textWriter.Write(baseConVar.GetString());
						textWriter.Write(";\r\n");
					}
					textWriter.Write("echo \"Loaded archived convars.\";");
					textWriter.Flush();
					RoR2Application.fileSystem.CreateDirectory("/Config/");
					try
					{
						using (Stream stream = RoR2Application.fileSystem.OpenFile("/Config/config.cfg", FileMode.Create, FileAccess.Write, FileShare.None))
						{
							if (stream != null)
							{
								stream.Write(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
								stream.Close();
							}
						}
					}
					catch (IOException ex)
					{
						Debug.LogFormat("Failed to write archived convars: {0}", new object[]
						{
							ex.Message
						});
					}
				}
			}
		}

		// Token: 0x06002049 RID: 8265 RVA: 0x0008B004 File Offset: 0x00089204
		[ConCommand(commandName = "set_vstr", flags = ConVarFlags.None, helpText = "Sets the specified vstr to the specified value.")]
		private static void CCSetVstr(ConCommandArgs args)
		{
			args.CheckArgumentCount(2);
			Console.instance.vstrs.Add(args[0], args[1]);
		}

		// Token: 0x0600204A RID: 8266 RVA: 0x0008B030 File Offset: 0x00089230
		[ConCommand(commandName = "exec", flags = ConVarFlags.None, helpText = "Executes a named config from the \"Config/\" folder.")]
		private static void CCExec(ConCommandArgs args)
		{
			if (args.Count > 0)
			{
				string text = Console.LoadConfig(args[0]);
				if (text != null)
				{
					Console.instance.SubmitCmd(args.sender, text, false);
				}
			}
		}

		// Token: 0x0600204B RID: 8267 RVA: 0x0008B06A File Offset: 0x0008926A
		[ConCommand(commandName = "echo", flags = ConVarFlags.None, helpText = "Echoes the given text to the console.")]
		private static void CCEcho(ConCommandArgs args)
		{
			if (args.Count > 0)
			{
				Debug.Log(args[0]);
				return;
			}
			Console.ShowHelpText(args.commandName);
		}

		// Token: 0x0600204C RID: 8268 RVA: 0x0008B090 File Offset: 0x00089290
		[ConCommand(commandName = "cvarlist", flags = ConVarFlags.None, helpText = "Print all available convars and concommands.")]
		private static void CCCvarList(ConCommandArgs args)
		{
			List<string> list = new List<string>();
			foreach (KeyValuePair<string, BaseConVar> keyValuePair in Console.instance.allConVars)
			{
				list.Add(keyValuePair.Key);
			}
			foreach (KeyValuePair<string, Console.ConCommand> keyValuePair2 in Console.instance.concommandCatalog)
			{
				list.Add(keyValuePair2.Key);
			}
			list.Sort();
			Debug.Log(string.Join("\n", list.ToArray()));
		}

		// Token: 0x0600204D RID: 8269 RVA: 0x0008B15C File Offset: 0x0008935C
		[ConCommand(commandName = "help", flags = ConVarFlags.None, helpText = "Show help text for the named convar or concommand.")]
		private static void CCHelp(ConCommandArgs args)
		{
			if (args.Count == 0)
			{
				Console.instance.SubmitCmd(args.sender, "find \"*\"", false);
				return;
			}
			Console.ShowHelpText(args[0]);
		}

		// Token: 0x0600204E RID: 8270 RVA: 0x0008B18C File Offset: 0x0008938C
		[ConCommand(commandName = "find", flags = ConVarFlags.None, helpText = "Find all concommands and convars with the specified substring.")]
		private static void CCFind(ConCommandArgs args)
		{
			if (args.Count == 0)
			{
				Console.ShowHelpText("find");
				return;
			}
			string text = args[0].ToLower(CultureInfo.InvariantCulture);
			bool flag = text == "*";
			List<string> list = new List<string>();
			foreach (KeyValuePair<string, BaseConVar> keyValuePair in Console.instance.allConVars)
			{
				if (flag || keyValuePair.Key.ToLower(CultureInfo.InvariantCulture).Contains(text) || keyValuePair.Value.helpText.ToLower(CultureInfo.InvariantCulture).Contains(text))
				{
					list.Add(keyValuePair.Key);
				}
			}
			foreach (KeyValuePair<string, Console.ConCommand> keyValuePair2 in Console.instance.concommandCatalog)
			{
				if (flag || keyValuePair2.Key.ToLower(CultureInfo.InvariantCulture).Contains(text) || keyValuePair2.Value.helpText.ToLower(CultureInfo.InvariantCulture).Contains(text))
				{
					list.Add(keyValuePair2.Key);
				}
			}
			list.Sort();
			string[] array = new string[list.Count];
			for (int i = 0; i < list.Count; i++)
			{
				array[i] = Console.GetHelpText(list[i]);
			}
			Debug.Log(string.Join("\n", array));
		}

		// Token: 0x0600204F RID: 8271 RVA: 0x0008B330 File Offset: 0x00089530
		[ConCommand(commandName = "clear", flags = ConVarFlags.None, helpText = "Clears the console output.")]
		private static void CCClear(ConCommandArgs args)
		{
			Console.logs.Clear();
			Action action = Console.onClear;
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x06002050 RID: 8272 RVA: 0x0008B34C File Offset: 0x0008954C
		private static string GetHelpText(string commandName)
		{
			Console.ConCommand conCommand;
			if (Console.instance.concommandCatalog.TryGetValue(commandName, out conCommand))
			{
				return string.Format(CultureInfo.InvariantCulture, "<color=#FF7F7F>\"{0}\"</color>\n- {1}", commandName, conCommand.helpText);
			}
			BaseConVar baseConVar = Console.instance.FindConVar(commandName);
			if (baseConVar != null)
			{
				return string.Format(CultureInfo.InvariantCulture, "<color=#FF7F7F>\"{0}\" = \"{1}\"</color>\n - {2}", commandName, baseConVar.GetString(), baseConVar.helpText);
			}
			return "";
		}

		// Token: 0x06002051 RID: 8273 RVA: 0x0008B3B5 File Offset: 0x000895B5
		public static void ShowHelpText(string commandName)
		{
			Debug.Log(Console.GetHelpText(commandName));
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06002052 RID: 8274 RVA: 0x0008B3C2 File Offset: 0x000895C2
		// (set) Token: 0x06002053 RID: 8275 RVA: 0x0008B3C9 File Offset: 0x000895C9
		public static bool sessionCheatsEnabled { get; private set; } = false;

		// Token: 0x0400258E RID: 9614
		public static List<Console.Log> logs = new List<Console.Log>();

		// Token: 0x04002591 RID: 9617
		private Dictionary<string, string> vstrs = new Dictionary<string, string>();

		// Token: 0x04002592 RID: 9618
		private Dictionary<string, Console.ConCommand> concommandCatalog = new Dictionary<string, Console.ConCommand>();

		// Token: 0x04002593 RID: 9619
		private Dictionary<string, BaseConVar> allConVars;

		// Token: 0x04002594 RID: 9620
		private List<BaseConVar> archiveConVars;

		// Token: 0x04002595 RID: 9621
		public static List<string> userCmdHistory = new List<string>();

		// Token: 0x04002596 RID: 9622
		private const int VK_RETURN = 13;

		// Token: 0x04002597 RID: 9623
		private const int WM_KEYDOWN = 256;

		// Token: 0x04002598 RID: 9624
		private static byte[] inputStreamBuffer = new byte[256];

		// Token: 0x04002599 RID: 9625
		private static Queue<string> stdInQueue = new Queue<string>();

		// Token: 0x0400259A RID: 9626
		private static Thread stdInReaderThread = null;

		// Token: 0x0400259B RID: 9627
		private static Console.SystemConsoleType systemConsoleType = Console.SystemConsoleType.None;

		// Token: 0x0400259C RID: 9628
		private static readonly StringBuilder sharedStringBuilder = new StringBuilder();

		// Token: 0x0400259D RID: 9629
		private const string configFolder = "/Config/";

		// Token: 0x0400259E RID: 9630
		private const string archiveConVarsPath = "/Config/config.cfg";

		// Token: 0x0400259F RID: 9631
		private static IntConVar maxMessages = new IntConVar("max_messages", ConVarFlags.Archive, "25", "Maximum number of messages that can be held in the console log.");

		// Token: 0x02000672 RID: 1650
		public struct Log
		{
			// Token: 0x040025A1 RID: 9633
			public string message;

			// Token: 0x040025A2 RID: 9634
			public string stackTrace;

			// Token: 0x040025A3 RID: 9635
			public LogType logType;
		}

		// Token: 0x02000673 RID: 1651
		// (Invoke) Token: 0x06002057 RID: 8279
		public delegate void LogReceivedDelegate(Console.Log log);

		// Token: 0x02000674 RID: 1652
		private class Lexer
		{
			// Token: 0x0600205A RID: 8282 RVA: 0x0008B460 File Offset: 0x00089660
			public Lexer(string srcString)
			{
				this.srcString = srcString;
				this.readIndex = 0;
			}

			// Token: 0x0600205B RID: 8283 RVA: 0x0008B481 File Offset: 0x00089681
			private static bool IsIgnorableCharacter(char character)
			{
				return !Console.Lexer.IsSeparatorCharacter(character) && !Console.Lexer.IsQuoteCharacter(character) && !Console.Lexer.IsIdentifierCharacter(character) && character != '/';
			}

			// Token: 0x0600205C RID: 8284 RVA: 0x0008B4A5 File Offset: 0x000896A5
			private static bool IsSeparatorCharacter(char character)
			{
				return character == ';' || character == '\n';
			}

			// Token: 0x0600205D RID: 8285 RVA: 0x0008B4B3 File Offset: 0x000896B3
			private static bool IsQuoteCharacter(char character)
			{
				return character == '\'' || character == '"';
			}

			// Token: 0x0600205E RID: 8286 RVA: 0x0008B4C1 File Offset: 0x000896C1
			private static bool IsIdentifierCharacter(char character)
			{
				return char.IsLetterOrDigit(character) || character == '_' || character == '.' || character == '-' || character == ':';
			}

			// Token: 0x0600205F RID: 8287 RVA: 0x0008B4E4 File Offset: 0x000896E4
			private bool TrimComment()
			{
				if (this.readIndex >= this.srcString.Length)
				{
					return false;
				}
				if (this.srcString[this.readIndex] == '/')
				{
					if (this.readIndex + 1 < this.srcString.Length)
					{
						char c = this.srcString[this.readIndex + 1];
						if (c == '/')
						{
							while (this.readIndex < this.srcString.Length)
							{
								if (this.srcString[this.readIndex] == '\n')
								{
									this.readIndex++;
									return true;
								}
								this.readIndex++;
							}
							return true;
						}
						if (c == '*')
						{
							while (this.readIndex < this.srcString.Length - 1)
							{
								if (this.srcString[this.readIndex] == '*' && this.srcString[this.readIndex + 1] == '/')
								{
									this.readIndex += 2;
									return true;
								}
								this.readIndex++;
							}
							return true;
						}
					}
					this.readIndex++;
				}
				return false;
			}

			// Token: 0x06002060 RID: 8288 RVA: 0x0008B610 File Offset: 0x00089810
			private void TrimWhitespace()
			{
				while (this.readIndex < this.srcString.Length && Console.Lexer.IsIgnorableCharacter(this.srcString[this.readIndex]))
				{
					this.readIndex++;
				}
			}

			// Token: 0x06002061 RID: 8289 RVA: 0x0008B64D File Offset: 0x0008984D
			private void TrimUnused()
			{
				do
				{
					this.TrimWhitespace();
				}
				while (this.TrimComment());
			}

			// Token: 0x06002062 RID: 8290 RVA: 0x0008B660 File Offset: 0x00089860
			private static int UnescapeNext(string srcString, int startPos, out char result)
			{
				result = '\\';
				int num = startPos + 1;
				if (num < srcString.Length)
				{
					char c = srcString[num];
					if (c <= '\'')
					{
						if (c != '"' && c != '\'')
						{
							return 1;
						}
					}
					else if (c != '\\')
					{
						if (c != 'n')
						{
							return 1;
						}
						result = '\n';
						return 2;
					}
					result = c;
					return 2;
				}
				return 1;
			}

			// Token: 0x06002063 RID: 8291 RVA: 0x0008B6B4 File Offset: 0x000898B4
			public string NextToken()
			{
				this.TrimUnused();
				if (this.readIndex == this.srcString.Length)
				{
					return null;
				}
				Console.Lexer.TokenType tokenType = Console.Lexer.TokenType.Identifier;
				char c = this.srcString[this.readIndex];
				char c2 = '\0';
				if (Console.Lexer.IsQuoteCharacter(c))
				{
					tokenType = Console.Lexer.TokenType.NestedString;
					c2 = c;
					this.readIndex++;
				}
				else if (Console.Lexer.IsSeparatorCharacter(c))
				{
					this.readIndex++;
					return ";";
				}
				while (this.readIndex < this.srcString.Length)
				{
					char c3 = this.srcString[this.readIndex];
					if (tokenType == Console.Lexer.TokenType.Identifier)
					{
						if (!Console.Lexer.IsIdentifierCharacter(c3))
						{
							break;
						}
					}
					else if (tokenType == Console.Lexer.TokenType.NestedString)
					{
						if (c3 == '\\')
						{
							this.readIndex += Console.Lexer.UnescapeNext(this.srcString, this.readIndex, out c3) - 1;
						}
						else if (c3 == c2)
						{
							this.readIndex++;
							break;
						}
					}
					this.stringBuilder.Append(c3);
					this.readIndex++;
				}
				string result = this.stringBuilder.ToString();
				this.stringBuilder.Length = 0;
				return result;
			}

			// Token: 0x06002064 RID: 8292 RVA: 0x0008B7D8 File Offset: 0x000899D8
			public Queue<string> GetTokens()
			{
				Queue<string> queue = new Queue<string>();
				for (string item = this.NextToken(); item != null; item = this.NextToken())
				{
					queue.Enqueue(item);
				}
				queue.Enqueue(";");
				return queue;
			}

			// Token: 0x040025A4 RID: 9636
			private string srcString;

			// Token: 0x040025A5 RID: 9637
			private int readIndex;

			// Token: 0x040025A6 RID: 9638
			private StringBuilder stringBuilder = new StringBuilder();

			// Token: 0x02000675 RID: 1653
			private enum TokenType
			{
				// Token: 0x040025A8 RID: 9640
				Identifier,
				// Token: 0x040025A9 RID: 9641
				NestedString
			}
		}

		// Token: 0x02000676 RID: 1654
		private class Substring
		{
			// Token: 0x1700028C RID: 652
			// (get) Token: 0x06002065 RID: 8293 RVA: 0x0008B811 File Offset: 0x00089A11
			public int endIndex
			{
				get
				{
					return this.startIndex + this.length;
				}
			}

			// Token: 0x1700028D RID: 653
			// (get) Token: 0x06002066 RID: 8294 RVA: 0x0008B820 File Offset: 0x00089A20
			public string str
			{
				get
				{
					return this.srcString.Substring(this.startIndex, this.length);
				}
			}

			// Token: 0x1700028E RID: 654
			// (get) Token: 0x06002067 RID: 8295 RVA: 0x0008B839 File Offset: 0x00089A39
			public Console.Substring nextToken
			{
				get
				{
					return new Console.Substring
					{
						srcString = this.srcString,
						startIndex = this.startIndex + this.length,
						length = 0
					};
				}
			}

			// Token: 0x040025AA RID: 9642
			public string srcString;

			// Token: 0x040025AB RID: 9643
			public int startIndex;

			// Token: 0x040025AC RID: 9644
			public int length;
		}

		// Token: 0x02000677 RID: 1655
		private class ConCommand
		{
			// Token: 0x040025AD RID: 9645
			public ConVarFlags flags;

			// Token: 0x040025AE RID: 9646
			public Console.ConCommandDelegate action;

			// Token: 0x040025AF RID: 9647
			public string helpText;
		}

		// Token: 0x02000678 RID: 1656
		// (Invoke) Token: 0x0600206B RID: 8299
		public delegate void ConCommandDelegate(ConCommandArgs args);

		// Token: 0x02000679 RID: 1657
		public readonly struct CmdSender
		{
			// Token: 0x0600206E RID: 8302 RVA: 0x0008B866 File Offset: 0x00089A66
			public CmdSender(LocalUser localUser)
			{
				this.localUser = localUser;
				this.networkUser = ((localUser != null) ? localUser.currentNetworkUser : null);
			}

			// Token: 0x0600206F RID: 8303 RVA: 0x0008B881 File Offset: 0x00089A81
			public CmdSender(NetworkUser networkUser)
			{
				this.localUser = ((networkUser != null) ? networkUser.localUser : null);
				this.networkUser = networkUser;
			}

			// Token: 0x06002070 RID: 8304 RVA: 0x0008B89C File Offset: 0x00089A9C
			public static implicit operator Console.CmdSender(LocalUser localUser)
			{
				return new Console.CmdSender(localUser);
			}

			// Token: 0x06002071 RID: 8305 RVA: 0x0008B8A4 File Offset: 0x00089AA4
			public static implicit operator Console.CmdSender(NetworkUser networkUser)
			{
				return new Console.CmdSender(networkUser);
			}

			// Token: 0x040025B0 RID: 9648
			public readonly LocalUser localUser;

			// Token: 0x040025B1 RID: 9649
			public readonly NetworkUser networkUser;
		}

		// Token: 0x0200067A RID: 1658
		private enum SystemConsoleType
		{
			// Token: 0x040025B3 RID: 9651
			None,
			// Token: 0x040025B4 RID: 9652
			Attach,
			// Token: 0x040025B5 RID: 9653
			Alloc
		}

		// Token: 0x0200067B RID: 1659
		public class AutoComplete
		{
			// Token: 0x06002072 RID: 8306 RVA: 0x0008B8AC File Offset: 0x00089AAC
			public AutoComplete(Console console)
			{
				HashSet<string> hashSet = new HashSet<string>();
				for (int i = 0; i < Console.userCmdHistory.Count; i++)
				{
					hashSet.Add(Console.userCmdHistory[i]);
				}
				foreach (KeyValuePair<string, BaseConVar> keyValuePair in console.allConVars)
				{
					hashSet.Add(keyValuePair.Key);
				}
				foreach (KeyValuePair<string, Console.ConCommand> keyValuePair2 in console.concommandCatalog)
				{
					hashSet.Add(keyValuePair2.Key);
				}
				foreach (string item in hashSet)
				{
					this.searchableStrings.Add(item);
				}
				this.searchableStrings.Sort();
			}

			// Token: 0x06002073 RID: 8307 RVA: 0x0008B9EC File Offset: 0x00089BEC
			public bool SetSearchString(string newSearchString)
			{
				newSearchString = newSearchString.ToLower(CultureInfo.InvariantCulture);
				if (newSearchString == this.searchString)
				{
					return false;
				}
				this.searchString = newSearchString;
				List<Console.AutoComplete.MatchInfo> list = new List<Console.AutoComplete.MatchInfo>();
				for (int i = 0; i < this.searchableStrings.Count; i++)
				{
					string text = this.searchableStrings[i];
					int num = Math.Min(text.Length, this.searchString.Length);
					int num2 = 0;
					while (num2 < num && char.ToLower(text[num2]) == this.searchString[num2])
					{
						num2++;
					}
					if (num2 > 1)
					{
						list.Add(new Console.AutoComplete.MatchInfo
						{
							str = text,
							similarity = num2
						});
					}
				}
				list.Sort(delegate(Console.AutoComplete.MatchInfo a, Console.AutoComplete.MatchInfo b)
				{
					if (a.similarity == b.similarity)
					{
						return string.CompareOrdinal(a.str, b.str);
					}
					if (a.similarity <= b.similarity)
					{
						return 1;
					}
					return -1;
				});
				this.resultsList = new List<string>();
				for (int j = 0; j < list.Count; j++)
				{
					this.resultsList.Add(list[j].str);
				}
				return true;
			}

			// Token: 0x040025B6 RID: 9654
			private List<string> searchableStrings = new List<string>();

			// Token: 0x040025B7 RID: 9655
			private string searchString;

			// Token: 0x040025B8 RID: 9656
			public List<string> resultsList = new List<string>();

			// Token: 0x0200067C RID: 1660
			private struct MatchInfo
			{
				// Token: 0x040025B9 RID: 9657
				public string str;

				// Token: 0x040025BA RID: 9658
				public int similarity;
			}
		}

		// Token: 0x0200067E RID: 1662
		public class CheatsConVar : BaseConVar
		{
			// Token: 0x06002077 RID: 8311 RVA: 0x00009F73 File Offset: 0x00008173
			public CheatsConVar(string name, ConVarFlags flags, string defaultValue, string helpText) : base(name, flags, defaultValue, helpText)
			{
			}

			// Token: 0x1700028F RID: 655
			// (get) Token: 0x06002078 RID: 8312 RVA: 0x0008BB4F File Offset: 0x00089D4F
			// (set) Token: 0x06002079 RID: 8313 RVA: 0x0008BB57 File Offset: 0x00089D57
			public bool boolValue
			{
				get
				{
					return this._boolValue;
				}
				private set
				{
					if (this._boolValue)
					{
						Console.sessionCheatsEnabled = true;
					}
				}
			}

			// Token: 0x0600207A RID: 8314 RVA: 0x0008BB68 File Offset: 0x00089D68
			public override void SetString(string newValue)
			{
				int num;
				if (TextSerialization.TryParseInvariant(newValue, out num))
				{
					this.boolValue = (num != 0);
				}
			}

			// Token: 0x0600207B RID: 8315 RVA: 0x0008BB89 File Offset: 0x00089D89
			public override string GetString()
			{
				if (!this.boolValue)
				{
					return "0";
				}
				return "1";
			}

			// Token: 0x040025BD RID: 9661
			public static readonly Console.CheatsConVar instance = new Console.CheatsConVar("cheats", ConVarFlags.ExecuteOnServer, "0", "Enable cheats. Achievements, unlock progression, and stat tracking will be disabled until the application is restarted.");

			// Token: 0x040025BE RID: 9662
			private bool _boolValue;
		}
	}
}
