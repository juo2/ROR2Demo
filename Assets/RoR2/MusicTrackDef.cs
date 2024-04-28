using System;
using AK.Wwise;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000973 RID: 2419
	[CreateAssetMenu(menuName = "RoR2/MusicTrackDef")]
	public class MusicTrackDef : ScriptableObject
	{
		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x060036DE RID: 14046 RVA: 0x000E78B2 File Offset: 0x000E5AB2
		// (set) Token: 0x060036DF RID: 14047 RVA: 0x000E78BA File Offset: 0x000E5ABA
		public MusicTrackIndex catalogIndex { get; set; }

		// Token: 0x060036E0 RID: 14048 RVA: 0x000E78C3 File Offset: 0x000E5AC3
		private void Awake()
		{
			this._cachedName = base.name;
		}

		// Token: 0x060036E1 RID: 14049 RVA: 0x000E78C3 File Offset: 0x000E5AC3
		private void OnValidate()
		{
			this._cachedName = base.name;
		}

		// Token: 0x060036E2 RID: 14050 RVA: 0x000E78D1 File Offset: 0x000E5AD1
		public virtual void Preload()
		{
			Bank bank = this.soundBank;
			if (bank == null)
			{
				return;
			}
			bank.Load(false, false);
		}

		// Token: 0x060036E3 RID: 14051 RVA: 0x000E78E8 File Offset: 0x000E5AE8
		public virtual void Play()
		{
			this.Preload();
			foreach (State state in this.states)
			{
				AkSoundEngine.SetState(state.GroupId, state.Id);
			}
		}

		// Token: 0x060036E4 RID: 14052 RVA: 0x000E7928 File Offset: 0x000E5B28
		public virtual void Stop()
		{
			State[] array = this.states;
			for (int i = 0; i < array.Length; i++)
			{
				AkSoundEngine.SetState(array[i].GroupId, 0U);
			}
		}

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x060036E5 RID: 14053 RVA: 0x00062756 File Offset: 0x00060956
		// (set) Token: 0x060036E6 RID: 14054 RVA: 0x00062756 File Offset: 0x00060956
		[Obsolete(".name should not be used. Use .cachedName instead. If retrieving the value from the engine is absolutely necessary, cast to ScriptableObject first.", true)]
		public new string name
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x060036E7 RID: 14055 RVA: 0x000E7959 File Offset: 0x000E5B59
		// (set) Token: 0x060036E8 RID: 14056 RVA: 0x000E7961 File Offset: 0x000E5B61
		public string cachedName
		{
			get
			{
				return this._cachedName;
			}
			set
			{
				base.name = value;
				this._cachedName = value;
			}
		}

		// Token: 0x04003742 RID: 14146
		public Bank soundBank;

		// Token: 0x04003743 RID: 14147
		public State[] states;

		// Token: 0x04003744 RID: 14148
		[Multiline]
		public string comment;

		// Token: 0x04003745 RID: 14149
		private string _cachedName;
	}
}
