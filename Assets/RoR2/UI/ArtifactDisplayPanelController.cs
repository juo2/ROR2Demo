using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RoR2.UI
{
	// Token: 0x02000CBB RID: 3259
	[RequireComponent(typeof(RectTransform))]
	public class ArtifactDisplayPanelController : MonoBehaviour
	{
		// Token: 0x06004A51 RID: 19025 RVA: 0x00131214 File Offset: 0x0012F414
		public void SetDisplayData<T>(ref T enabledArtifacts) where T : IEnumerator<ArtifactDef>
		{
			if (!this.panelObject)
			{
				return;
			}
			enabledArtifacts.Reset();
			int num = 0;
			while (enabledArtifacts.MoveNext())
			{
				num++;
			}
			this.panelObject.SetActive(num > 0);
			if (this.iconAllocator == null)
			{
				this.iconAllocator = new UIElementAllocator<RawImage>(this.iconContainer, this.iconPrefab, true, false);
			}
			this.iconAllocator.AllocateElements(num);
			int num2 = 0;
			enabledArtifacts.Reset();
			while (enabledArtifacts.MoveNext())
			{
				RawImage rawImage = this.iconAllocator.elements[num2++];
				ArtifactDef artifactDef = enabledArtifacts.Current;
				rawImage.texture = artifactDef.smallIconSelectedSprite.texture;
				TooltipProvider component = rawImage.GetComponent<TooltipProvider>();
				if (component)
				{
					component.titleToken = artifactDef.nameToken;
					component.titleColor = ColorCatalog.GetColor(ColorCatalog.ColorIndex.Artifact);
					component.bodyToken = artifactDef.descriptionToken;
					component.bodyColor = Color.black;
				}
			}
		}

		// Token: 0x04004705 RID: 18181
		[Tooltip("The panel object that this component controls. Should usually a child, as this component manages the enabled/disabled status of the designated gameobject.")]
		public GameObject panelObject;

		// Token: 0x04004706 RID: 18182
		public RectTransform iconContainer;

		// Token: 0x04004707 RID: 18183
		public GameObject iconPrefab;

		// Token: 0x04004708 RID: 18184
		private UIElementAllocator<RawImage> iconAllocator;
	}
}
