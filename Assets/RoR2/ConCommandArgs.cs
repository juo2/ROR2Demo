using System;
using System.Collections.Generic;
using RoR2.Networking;
using UnityEngine;

namespace RoR2
{
	// Token: 0x0200066F RID: 1647
	public struct ConCommandArgs
	{
		// Token: 0x06002006 RID: 8198 RVA: 0x00089D8B File Offset: 0x00087F8B
		public void CheckArgumentCount(int count)
		{
			ConCommandException.CheckArgumentCount(this.userArgs, count);
		}

		// Token: 0x17000285 RID: 645
		public string this[int i]
		{
			get
			{
				return this.userArgs[i];
			}
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06002008 RID: 8200 RVA: 0x00089DA7 File Offset: 0x00087FA7
		public int Count
		{
			get
			{
				return this.userArgs.Count;
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06002009 RID: 8201 RVA: 0x00089DB4 File Offset: 0x00087FB4
		public GameObject senderMasterObject
		{
			get
			{
				if (!this.sender)
				{
					return null;
				}
				return this.sender.masterObject;
			}
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x0600200A RID: 8202 RVA: 0x00089DD0 File Offset: 0x00087FD0
		public CharacterMaster senderMaster
		{
			get
			{
				if (!this.sender)
				{
					return null;
				}
				return this.sender.master;
			}
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x0600200B RID: 8203 RVA: 0x00089DEC File Offset: 0x00087FEC
		public CharacterBody senderBody
		{
			get
			{
				if (!this.sender)
				{
					return null;
				}
				return this.sender.GetCurrentBody();
			}
		}

		// Token: 0x0600200C RID: 8204 RVA: 0x00089E08 File Offset: 0x00088008
		public string TryGetArgString(int index)
		{
			if (index < this.userArgs.Count)
			{
				return this.userArgs[index];
			}
			return null;
		}

		// Token: 0x0600200D RID: 8205 RVA: 0x00089E26 File Offset: 0x00088026
		public string GetArgString(int index)
		{
			string text = this.TryGetArgString(index);
			if (text == null)
			{
				throw new ConCommandException(string.Format("Argument {0} must be a string.", index));
			}
			return text;
		}

		// Token: 0x0600200E RID: 8206 RVA: 0x00089E48 File Offset: 0x00088048
		public ulong? TryGetArgUlong(int index)
		{
			ulong value;
			if (index < this.userArgs.Count && TextSerialization.TryParseInvariant(this.userArgs[index], out value))
			{
				return new ulong?(value);
			}
			return null;
		}

		// Token: 0x0600200F RID: 8207 RVA: 0x00089E88 File Offset: 0x00088088
		public ulong GetArgULong(int index)
		{
			ulong? num = this.TryGetArgUlong(index);
			if (num == null)
			{
				throw new ConCommandException(string.Format("Argument {0} must be an unsigned integer.", index));
			}
			return num.Value;
		}

		// Token: 0x06002010 RID: 8208 RVA: 0x00089EC4 File Offset: 0x000880C4
		public int? TryGetArgInt(int index)
		{
			int value;
			if (index < this.userArgs.Count && TextSerialization.TryParseInvariant(this.userArgs[index], out value))
			{
				return new int?(value);
			}
			return null;
		}

		// Token: 0x06002011 RID: 8209 RVA: 0x00089F04 File Offset: 0x00088104
		public int GetArgInt(int index)
		{
			int? num = this.TryGetArgInt(index);
			if (num == null)
			{
				throw new ConCommandException(string.Format("Argument {0} must be an integer.", index));
			}
			return num.Value;
		}

		// Token: 0x06002012 RID: 8210 RVA: 0x00089F40 File Offset: 0x00088140
		public bool? TryGetArgBool(int index)
		{
			int? num = this.TryGetArgInt(index);
			if (num != null)
			{
				int? num2 = num;
				int num3 = 0;
				return new bool?(num2.GetValueOrDefault() > num3 & num2 != null);
			}
			return null;
		}

		// Token: 0x06002013 RID: 8211 RVA: 0x00089F84 File Offset: 0x00088184
		public bool GetArgBool(int index)
		{
			int? num = this.TryGetArgInt(index);
			if (num == null)
			{
				throw new ConCommandException(string.Format("Argument {0} must be a boolean.", index));
			}
			return num.Value > 0;
		}

		// Token: 0x06002014 RID: 8212 RVA: 0x00089FC4 File Offset: 0x000881C4
		public float? TryGetArgFloat(int index)
		{
			float value;
			if (index < this.userArgs.Count && TextSerialization.TryParseInvariant(this.userArgs[index], out value))
			{
				return new float?(value);
			}
			return null;
		}

		// Token: 0x06002015 RID: 8213 RVA: 0x0008A004 File Offset: 0x00088204
		public float GetArgFloat(int index)
		{
			float? num = this.TryGetArgFloat(index);
			if (num == null)
			{
				throw new ConCommandException(string.Format("Argument {0} must be a number.", index));
			}
			return num.Value;
		}

		// Token: 0x06002016 RID: 8214 RVA: 0x0008A040 File Offset: 0x00088240
		public double? TryGetArgDouble(int index)
		{
			double value;
			if (index < this.userArgs.Count && TextSerialization.TryParseInvariant(this.userArgs[index], out value))
			{
				return new double?(value);
			}
			return null;
		}

		// Token: 0x06002017 RID: 8215 RVA: 0x0008A080 File Offset: 0x00088280
		public double GetArgDouble(int index)
		{
			double? num = this.TryGetArgDouble(index);
			if (num == null)
			{
				throw new ConCommandException(string.Format("Argument {0} must be a number.", index));
			}
			return num.Value;
		}

		// Token: 0x06002018 RID: 8216 RVA: 0x0008A0BC File Offset: 0x000882BC
		public T? TryGetArgEnum<T>(int index) where T : struct
		{
			T value;
			if (index < this.userArgs.Count && Enum.TryParse<T>(this.userArgs[index], true, out value))
			{
				return new T?(value);
			}
			return null;
		}

		// Token: 0x06002019 RID: 8217 RVA: 0x0008A100 File Offset: 0x00088300
		public T GetArgEnum<T>(int index) where T : struct
		{
			T? t = this.TryGetArgEnum<T>(index);
			if (t == null)
			{
				throw new ConCommandException(string.Format("Argument {0} must be one of the values of {1}.", index, typeof(T).Name));
			}
			return t.Value;
		}

		// Token: 0x0600201A RID: 8218 RVA: 0x0008A14C File Offset: 0x0008834C
		public CSteamID? TryGetArgSteamID(int index)
		{
			CSteamID value;
			if (index < this.userArgs.Count && CSteamID.TryParse(this.userArgs[index], out value))
			{
				return new CSteamID?(value);
			}
			return null;
		}

		// Token: 0x0600201B RID: 8219 RVA: 0x0008A18C File Offset: 0x0008838C
		public CSteamID GetArgSteamID(int index)
		{
			CSteamID? csteamID = this.TryGetArgSteamID(index);
			if (csteamID == null)
			{
				throw new ConCommandException(string.Format("Argument {0} must be a valid Steam ID.", index));
			}
			return csteamID.Value;
		}

		// Token: 0x0600201C RID: 8220 RVA: 0x0008A1C8 File Offset: 0x000883C8
		public AddressPortPair? TryGetArgAddressPortPair(int index)
		{
			AddressPortPair value;
			if (index < this.userArgs.Count && AddressPortPair.TryParse(this.userArgs[index], out value))
			{
				return new AddressPortPair?(value);
			}
			return null;
		}

		// Token: 0x0600201D RID: 8221 RVA: 0x0008A208 File Offset: 0x00088408
		public AddressPortPair GetArgAddressPortPair(int index)
		{
			AddressPortPair? addressPortPair = this.TryGetArgAddressPortPair(index);
			if (addressPortPair == null)
			{
				throw new ConCommandException(string.Format("Argument {0} must be a valid address and port pair in the format \"address:port\" (e.g. \"127.0.0.1:27015\"). Given value: {1}", index, this.TryGetArgString(index)));
			}
			return addressPortPair.Value;
		}

		// Token: 0x0600201E RID: 8222 RVA: 0x0008A24A File Offset: 0x0008844A
		public PickupIndex GetArgPickupIndex(int index)
		{
			PickupIndex pickupIndex = PickupCatalog.FindPickupIndex(this.TryGetArgString(index) ?? string.Empty);
			if (pickupIndex == PickupIndex.none)
			{
				throw new ConCommandException(string.Format("Argument {0} must be a valid pickup name.", index));
			}
			return pickupIndex;
		}

		// Token: 0x0600201F RID: 8223 RVA: 0x0008A284 File Offset: 0x00088484
		public LocalUser GetSenderLocalUser()
		{
			LocalUser localUser = this.localUserSender;
			if (localUser == null)
			{
				throw new ConCommandException(string.Format("Command requires a local user that is not available.", Array.Empty<object>()));
			}
			return localUser;
		}

		// Token: 0x06002020 RID: 8224 RVA: 0x00089DEC File Offset: 0x00087FEC
		public CharacterBody TryGetSenderBody()
		{
			if (!this.sender)
			{
				return null;
			}
			return this.sender.GetCurrentBody();
		}

		// Token: 0x06002021 RID: 8225 RVA: 0x0008A2A8 File Offset: 0x000884A8
		public CharacterBody GetSenderBody()
		{
			CharacterBody characterBody = this.TryGetSenderBody();
			if (characterBody)
			{
				return characterBody;
			}
			throw new ConCommandException("Command requires the sender to have a body.");
		}

		// Token: 0x06002022 RID: 8226 RVA: 0x0008A2D0 File Offset: 0x000884D0
		public CharacterMaster GetSenderMaster()
		{
			if (!this.senderMaster)
			{
				throw new ConCommandException("Command requires the sender to have a CharacterMaster. The game must be in an active run and the sender must be a participating player.");
			}
			return this.senderMaster;
		}

		// Token: 0x06002023 RID: 8227 RVA: 0x0006AA15 File Offset: 0x00068C15
		public void Log(string message)
		{
			Debug.Log(message);
		}

		// Token: 0x04002589 RID: 9609
		public List<string> userArgs;

		// Token: 0x0400258A RID: 9610
		public NetworkUser sender;

		// Token: 0x0400258B RID: 9611
		public LocalUser localUserSender;

		// Token: 0x0400258C RID: 9612
		public string commandName;
	}
}
