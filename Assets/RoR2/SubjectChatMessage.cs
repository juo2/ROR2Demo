using System;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x02000509 RID: 1289
	public class SubjectChatMessage : ChatMessageBase
	{
		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06001786 RID: 6022 RVA: 0x000674E0 File Offset: 0x000656E0
		// (set) Token: 0x06001785 RID: 6021 RVA: 0x00067494 File Offset: 0x00065694
		public NetworkUser subjectAsNetworkUser
		{
			get
			{
				return this.subjectNetworkUserGetComponent.Get(this.subjectNetworkUserObject);
			}
			set
			{
				this.subjectNetworkUserObject = (value ? value.gameObject : null);
				CharacterBody characterBody = null;
				if (value)
				{
					characterBody = value.GetCurrentBody();
				}
				this.subjectCharacterBodyGameObject = (characterBody ? characterBody.gameObject : null);
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06001788 RID: 6024 RVA: 0x00067524 File Offset: 0x00065724
		// (set) Token: 0x06001787 RID: 6023 RVA: 0x000674F3 File Offset: 0x000656F3
		public CharacterBody subjectAsCharacterBody
		{
			get
			{
				return this.subjectCharacterBodyGetComponent.Get(this.subjectCharacterBodyGameObject);
			}
			set
			{
				this.subjectCharacterBodyGameObject = (value ? value.gameObject : null);
				NetworkUser networkUser = Util.LookUpBodyNetworkUser(value);
				this.subjectNetworkUserObject = ((networkUser != null) ? networkUser.gameObject : null);
			}
		}

		// Token: 0x06001789 RID: 6025 RVA: 0x00067537 File Offset: 0x00065737
		protected string GetSubjectName()
		{
			if (this.subjectAsNetworkUser)
			{
				return Util.EscapeRichTextForTextMeshPro(this.subjectAsNetworkUser.userName);
			}
			if (this.subjectAsCharacterBody)
			{
				return this.subjectAsCharacterBody.GetDisplayName();
			}
			return "???";
		}

		// Token: 0x0600178A RID: 6026 RVA: 0x00067575 File Offset: 0x00065775
		protected bool IsSecondPerson()
		{
			return LocalUserManager.readOnlyLocalUsersList.Count == 1 && this.subjectAsNetworkUser && this.subjectAsNetworkUser.localUser != null;
		}

		// Token: 0x0600178B RID: 6027 RVA: 0x000675A1 File Offset: 0x000657A1
		protected string GetResolvedToken()
		{
			if (!this.IsSecondPerson())
			{
				return this.baseToken;
			}
			return this.baseToken + "_2P";
		}

		// Token: 0x0600178C RID: 6028 RVA: 0x000675C2 File Offset: 0x000657C2
		public override string ConstructChatString()
		{
			return string.Format(Language.GetString(this.GetResolvedToken()), this.GetSubjectName());
		}

		// Token: 0x0600178D RID: 6029 RVA: 0x000675DA File Offset: 0x000657DA
		public override void Serialize(NetworkWriter writer)
		{
			base.Serialize(writer);
			writer.Write(this.subjectNetworkUserObject);
			writer.Write(this.subjectCharacterBodyGameObject);
			writer.Write(this.baseToken);
		}

		// Token: 0x0600178E RID: 6030 RVA: 0x00067607 File Offset: 0x00065807
		public override void Deserialize(NetworkReader reader)
		{
			base.Deserialize(reader);
			this.subjectNetworkUserObject = reader.ReadGameObject();
			this.subjectCharacterBodyGameObject = reader.ReadGameObject();
			this.baseToken = reader.ReadString();
		}

		// Token: 0x04001D0E RID: 7438
		private GameObject subjectNetworkUserObject;

		// Token: 0x04001D0F RID: 7439
		private GameObject subjectCharacterBodyGameObject;

		// Token: 0x04001D10 RID: 7440
		public string baseToken;

		// Token: 0x04001D11 RID: 7441
		private MemoizedGetComponent<NetworkUser> subjectNetworkUserGetComponent;

		// Token: 0x04001D12 RID: 7442
		private MemoizedGetComponent<CharacterBody> subjectCharacterBodyGetComponent;
	}
}
