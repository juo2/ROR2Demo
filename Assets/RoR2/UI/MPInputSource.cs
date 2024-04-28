using System;
using Rewired.UI;
using UnityEngine;

namespace RoR2.UI
{
	// Token: 0x02000D44 RID: 3396
	public class MPInputSource : IMouseInputSource
	{
		// Token: 0x06004DA5 RID: 19877 RVA: 0x00062756 File Offset: 0x00060956
		public bool GetButtonDown(int button)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004DA6 RID: 19878 RVA: 0x00062756 File Offset: 0x00060956
		public bool GetButtonUp(int button)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004DA7 RID: 19879 RVA: 0x00062756 File Offset: 0x00060956
		public bool GetButton(int button)
		{
			throw new NotImplementedException();
		}

		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x06004DA8 RID: 19880 RVA: 0x00140595 File Offset: 0x0013E795
		public int playerId { get; }

		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x06004DA9 RID: 19881 RVA: 0x0014059D File Offset: 0x0013E79D
		public bool enabled { get; }

		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x06004DAA RID: 19882 RVA: 0x001405A5 File Offset: 0x0013E7A5
		public bool locked { get; }

		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x06004DAB RID: 19883 RVA: 0x001405AD File Offset: 0x0013E7AD
		public int buttonCount { get; }

		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x06004DAC RID: 19884 RVA: 0x001405B5 File Offset: 0x0013E7B5
		public Vector2 screenPosition { get; }

		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x06004DAD RID: 19885 RVA: 0x001405BD File Offset: 0x0013E7BD
		public Vector2 screenPositionDelta { get; }

		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x06004DAE RID: 19886 RVA: 0x001405C5 File Offset: 0x0013E7C5
		public Vector2 wheelDelta { get; }
	}
}
