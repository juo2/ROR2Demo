using System;
using EntityStates;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000536 RID: 1334
	[CreateAssetMenu(menuName = "RoR2/GameEndingDef")]
	public class GameEndingDef : ScriptableObject
	{
		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06001848 RID: 6216 RVA: 0x0006A6C4 File Offset: 0x000688C4
		// (set) Token: 0x06001849 RID: 6217 RVA: 0x0006A6CC File Offset: 0x000688CC
		public GameEndingIndex gameEndingIndex { get; set; }

		// Token: 0x0600184A RID: 6218 RVA: 0x0006A6D5 File Offset: 0x000688D5
		private void Awake()
		{
			this._cachedName = base.name;
		}

		// Token: 0x0600184B RID: 6219 RVA: 0x0006A6D5 File Offset: 0x000688D5
		private void OnValidate()
		{
			this._cachedName = base.name;
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x0600184C RID: 6220 RVA: 0x00062756 File Offset: 0x00060956
		// (set) Token: 0x0600184D RID: 6221 RVA: 0x00062756 File Offset: 0x00060956
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

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x0600184E RID: 6222 RVA: 0x0006A6E3 File Offset: 0x000688E3
		// (set) Token: 0x0600184F RID: 6223 RVA: 0x0006A6EB File Offset: 0x000688EB
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

		// Token: 0x04001DEC RID: 7660
		public string endingTextToken;

		// Token: 0x04001DED RID: 7661
		public Color backgroundColor;

		// Token: 0x04001DEE RID: 7662
		public Color foregroundColor;

		// Token: 0x04001DEF RID: 7663
		public Sprite icon;

		// Token: 0x04001DF0 RID: 7664
		public Material material;

		// Token: 0x04001DF1 RID: 7665
		[Tooltip("The body prefab to use as the killer when this ending is triggered while players are still alive.")]
		public GameObject defaultKillerOverride;

		// Token: 0x04001DF2 RID: 7666
		public bool isWin;

		// Token: 0x04001DF3 RID: 7667
		public bool showCredits;

		// Token: 0x04001DF4 RID: 7668
		public SerializableEntityStateType gameOverControllerState;

		// Token: 0x04001DF5 RID: 7669
		public uint lunarCoinReward;

		// Token: 0x04001DF6 RID: 7670
		private string _cachedName;
	}
}
