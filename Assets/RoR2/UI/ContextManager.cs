using System;
using System.Globalization;
using TMPro;
using UnityEngine;

namespace RoR2.UI
{
	// Token: 0x02000CD7 RID: 3287
	[RequireComponent(typeof(MPEventSystemLocator))]
	public class ContextManager : MonoBehaviour
	{
		// Token: 0x06004B01 RID: 19201 RVA: 0x001344C7 File Offset: 0x001326C7
		private void Awake()
		{
			this.eventSystemLocator = base.GetComponent<MPEventSystemLocator>();
		}

		// Token: 0x06004B02 RID: 19202 RVA: 0x001344D5 File Offset: 0x001326D5
		private void Start()
		{
			this.Update();
		}

		// Token: 0x06004B03 RID: 19203 RVA: 0x001344E0 File Offset: 0x001326E0
		private void Update()
		{
			string text = "";
			string text2 = "";
			bool active = false;
			if (this.hud && this.hud.targetBodyObject)
			{
				InteractionDriver component = this.hud.targetBodyObject.GetComponent<InteractionDriver>();
				if (component)
				{
					GameObject gameObject = component.FindBestInteractableObject();
					if (gameObject)
					{
						PlayerCharacterMasterController component2 = this.hud.targetMaster.GetComponent<PlayerCharacterMasterController>();
						if (component2 && component2.networkUser && component2.networkUser.localUser != null)
						{
							IInteractable component3 = gameObject.GetComponent<IInteractable>();
							if (component3 != null && ((MonoBehaviour)component3).isActiveAndEnabled)
							{
								string text3 = (component3.GetInteractability(component.interactor) == Interactability.Available) ? component3.GetContextString(component.interactor) : null;
								if (text3 != null)
								{
									text2 = text3;
									text = string.Format(CultureInfo.InvariantCulture, "<style=cKeyBinding>{0}</style>", Glyphs.GetGlyphString(this.eventSystemLocator, "Interact"));
									active = true;
								}
							}
						}
					}
				}
			}
			this.glyphTMP.text = text;
			this.descriptionTMP.text = text2;
			this.contextDisplay.SetActive(active);
		}

		// Token: 0x040047C3 RID: 18371
		public TextMeshProUGUI glyphTMP;

		// Token: 0x040047C4 RID: 18372
		public TextMeshProUGUI descriptionTMP;

		// Token: 0x040047C5 RID: 18373
		public GameObject contextDisplay;

		// Token: 0x040047C6 RID: 18374
		public HUD hud;

		// Token: 0x040047C7 RID: 18375
		private MPEventSystemLocator eventSystemLocator;
	}
}
