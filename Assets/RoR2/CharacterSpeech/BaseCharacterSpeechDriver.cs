using System;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2.CharacterSpeech
{
	// Token: 0x02000C26 RID: 3110
	[RequireComponent(typeof(CharacterSpeechController))]
	public class BaseCharacterSpeechDriver : MonoBehaviour
	{
		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x06004638 RID: 17976 RVA: 0x00122BAF File Offset: 0x00120DAF
		// (set) Token: 0x06004639 RID: 17977 RVA: 0x00122BB7 File Offset: 0x00120DB7
		private protected CharacterSpeechController characterSpeechController { protected get; private set; }

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x0600463A RID: 17978 RVA: 0x00122BC0 File Offset: 0x00120DC0
		// (set) Token: 0x0600463B RID: 17979 RVA: 0x00122BC8 File Offset: 0x00120DC8
		private protected CharacterBody currentCharacterBody { protected get; private set; }

		// Token: 0x0600463C RID: 17980 RVA: 0x00122BD1 File Offset: 0x00120DD1
		protected void Awake()
		{
			if (!NetworkServer.active)
			{
				base.enabled = false;
				return;
			}
			this.characterSpeechController = base.GetComponent<CharacterSpeechController>();
		}

		// Token: 0x0600463D RID: 17981 RVA: 0x00122BF0 File Offset: 0x00120DF0
		protected void OnEnable()
		{
			if (!NetworkServer.active)
			{
				return;
			}
			this.characterSpeechController.onCharacterBodyDiscovered += this.OnCharacterBodyDiscovered;
			this.characterSpeechController.onCharacterBodyLost += this.OnCharacterBodyLost;
			if (this.characterSpeechController.currentCharacterBody != null)
			{
				this.OnCharacterBodyDiscovered(this.characterSpeechController.currentCharacterBody);
			}
		}

		// Token: 0x0600463E RID: 17982 RVA: 0x00122C54 File Offset: 0x00120E54
		protected void OnDisable()
		{
			if (!NetworkServer.active)
			{
				return;
			}
			if (this.currentCharacterBody != null)
			{
				this.OnCharacterBodyLost(this.currentCharacterBody);
			}
			this.characterSpeechController.onCharacterBodyLost -= this.OnCharacterBodyLost;
			this.characterSpeechController.onCharacterBodyDiscovered -= this.OnCharacterBodyDiscovered;
		}

		// Token: 0x0600463F RID: 17983 RVA: 0x00122CAD File Offset: 0x00120EAD
		protected void OnDestroy()
		{
			bool active = NetworkServer.active;
		}

		// Token: 0x06004640 RID: 17984 RVA: 0x00122CB5 File Offset: 0x00120EB5
		protected virtual void OnCharacterBodyDiscovered(CharacterBody characterBody)
		{
			this.currentCharacterBody = characterBody;
		}

		// Token: 0x06004641 RID: 17985 RVA: 0x00122CBE File Offset: 0x00120EBE
		protected virtual void OnCharacterBodyLost(CharacterBody characterBody)
		{
			this.currentCharacterBody = null;
		}
	}
}
