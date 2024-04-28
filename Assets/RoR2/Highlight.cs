using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000729 RID: 1833
	public class Highlight : MonoBehaviour
	{
		// Token: 0x1700034C RID: 844
		// (get) Token: 0x0600261C RID: 9756 RVA: 0x000A659D File Offset: 0x000A479D
		public static ReadOnlyCollection<Highlight> readonlyHighlightList
		{
			get
			{
				return Highlight._readonlyHighlightList;
			}
		}

		// Token: 0x0600261D RID: 9757 RVA: 0x000A65A4 File Offset: 0x000A47A4
		private void Awake()
		{
			this.displayNameProvider = base.GetComponent<IDisplayNameProvider>();
		}

		// Token: 0x0600261E RID: 9758 RVA: 0x000A65B2 File Offset: 0x000A47B2
		public void OnEnable()
		{
			Highlight.highlightList.Add(this);
		}

		// Token: 0x0600261F RID: 9759 RVA: 0x000A65BF File Offset: 0x000A47BF
		public void OnDisable()
		{
			Highlight.highlightList.Remove(this);
		}

		// Token: 0x06002620 RID: 9760 RVA: 0x000A65D0 File Offset: 0x000A47D0
		public Color GetColor()
		{
			switch (this.highlightColor)
			{
			case Highlight.HighlightColor.interactive:
				return ColorCatalog.GetColor(ColorCatalog.ColorIndex.Interactable);
			case Highlight.HighlightColor.teleporter:
				return ColorCatalog.GetColor(ColorCatalog.ColorIndex.Teleporter);
			case Highlight.HighlightColor.pickup:
			{
				PickupDef pickupDef = PickupCatalog.GetPickupDef(this.pickupIndex);
				if (pickupDef == null)
				{
					return PickupCatalog.invalidPickupColor;
				}
				return pickupDef.baseColor;
			}
			default:
				return Color.magenta;
			}
		}

		// Token: 0x040029F5 RID: 10741
		private static List<Highlight> highlightList = new List<Highlight>();

		// Token: 0x040029F6 RID: 10742
		private static ReadOnlyCollection<Highlight> _readonlyHighlightList = new ReadOnlyCollection<Highlight>(Highlight.highlightList);

		// Token: 0x040029F7 RID: 10743
		private IDisplayNameProvider displayNameProvider;

		// Token: 0x040029F8 RID: 10744
		[HideInInspector]
		public PickupIndex pickupIndex;

		// Token: 0x040029F9 RID: 10745
		public Renderer targetRenderer;

		// Token: 0x040029FA RID: 10746
		public float strength = 1f;

		// Token: 0x040029FB RID: 10747
		public Highlight.HighlightColor highlightColor;

		// Token: 0x040029FC RID: 10748
		public bool isOn;

		// Token: 0x0200072A RID: 1834
		public enum HighlightColor
		{
			// Token: 0x040029FE RID: 10750
			interactive,
			// Token: 0x040029FF RID: 10751
			teleporter,
			// Token: 0x04002A00 RID: 10752
			pickup,
			// Token: 0x04002A01 RID: 10753
			unavailable
		}
	}
}
