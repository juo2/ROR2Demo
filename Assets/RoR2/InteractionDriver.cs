using System;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x02000774 RID: 1908
	[RequireComponent(typeof(Interactor))]
	[RequireComponent(typeof(InputBankTest))]
	public class InteractionDriver : MonoBehaviour, ILifeBehavior
	{
		// Token: 0x17000385 RID: 901
		// (get) Token: 0x060027A2 RID: 10146 RVA: 0x000AC1C2 File Offset: 0x000AA3C2
		// (set) Token: 0x060027A3 RID: 10147 RVA: 0x000AC1CA File Offset: 0x000AA3CA
		public Interactor interactor { get; private set; }

		// Token: 0x060027A4 RID: 10148 RVA: 0x000AC1D4 File Offset: 0x000AA3D4
		private void Awake()
		{
			this.networkIdentity = base.GetComponent<NetworkIdentity>();
			this.interactor = base.GetComponent<Interactor>();
			this.inputBank = base.GetComponent<InputBankTest>();
			this.characterBody = base.GetComponent<CharacterBody>();
			this.equipmentSlot = (this.characterBody ? this.characterBody.equipmentSlot : null);
		}

		// Token: 0x060027A5 RID: 10149 RVA: 0x000AC234 File Offset: 0x000AA434
		private void FixedUpdate()
		{
			if (this.networkIdentity.hasAuthority)
			{
				this.interactableCooldown -= Time.fixedDeltaTime;
				this.inputReceived = (this.inputBank.interact.justPressed || (this.inputBank.interact.down && this.interactableCooldown <= 0f));
				if (this.inputBank.interact.justReleased)
				{
					this.inputReceived = false;
					this.interactableCooldown = 0f;
				}
			}
			if (this.inputReceived)
			{
				GameObject gameObject = this.FindBestInteractableObject();
				if (gameObject)
				{
					this.interactor.AttemptInteraction(gameObject);
					this.interactableCooldown = 0.25f;
				}
			}
		}

		// Token: 0x060027A6 RID: 10150 RVA: 0x000AC2F4 File Offset: 0x000AA4F4
		public GameObject FindBestInteractableObject()
		{
			if (this.interactableOverride)
			{
				return this.interactableOverride;
			}
			float num = 0f;
			Ray originalAimRay = new Ray(this.inputBank.aimOrigin, this.inputBank.aimDirection);
			Ray raycastRay = CameraRigController.ModifyAimRayIfApplicable(originalAimRay, base.gameObject, out num);
			float num2 = this.interactor.maxInteractionDistance;
			if (this.equipmentSlot && this.equipmentSlot.equipmentIndex == RoR2Content.Equipment.Recycle.equipmentIndex)
			{
				num2 *= 2f;
			}
			return this.interactor.FindBestInteractableObject(raycastRay, num2 + num, originalAimRay.origin, num2);
		}

		// Token: 0x060027A7 RID: 10151 RVA: 0x000AC396 File Offset: 0x000AA596
		static InteractionDriver()
		{
			OutlineHighlight.onPreRenderOutlineHighlight = (Action<OutlineHighlight>)Delegate.Combine(OutlineHighlight.onPreRenderOutlineHighlight, new Action<OutlineHighlight>(InteractionDriver.OnPreRenderOutlineHighlight));
		}

		// Token: 0x060027A8 RID: 10152 RVA: 0x000AC3B8 File Offset: 0x000AA5B8
		private static void OnPreRenderOutlineHighlight(OutlineHighlight outlineHighlight)
		{
			if (!outlineHighlight.sceneCamera)
			{
				return;
			}
			if (!outlineHighlight.sceneCamera.cameraRigController)
			{
				return;
			}
			GameObject target = outlineHighlight.sceneCamera.cameraRigController.target;
			if (!target)
			{
				return;
			}
			InteractionDriver component = target.GetComponent<InteractionDriver>();
			if (!component)
			{
				return;
			}
			GameObject gameObject = component.FindBestInteractableObject();
			if (!gameObject)
			{
				return;
			}
			IInteractable component2 = gameObject.GetComponent<IInteractable>();
			Highlight component3 = gameObject.GetComponent<Highlight>();
			if (!component3)
			{
				return;
			}
			Color a = component3.GetColor();
			if (component2 != null && ((MonoBehaviour)component2).isActiveAndEnabled && component2.GetInteractability(component.interactor) == Interactability.ConditionsNotMet)
			{
				a = ColorCatalog.GetColor(ColorCatalog.ColorIndex.Unaffordable);
			}
			outlineHighlight.highlightQueue.Enqueue(new OutlineHighlight.HighlightInfo
			{
				renderer = component3.targetRenderer,
				color = a * component3.strength
			});
		}

		// Token: 0x060027A9 RID: 10153 RVA: 0x0007FEC8 File Offset: 0x0007E0C8
		public void OnDeathStart()
		{
			base.enabled = false;
		}

		// Token: 0x04002B93 RID: 11155
		public bool highlightInteractor;

		// Token: 0x04002B94 RID: 11156
		private bool inputReceived;

		// Token: 0x04002B95 RID: 11157
		private NetworkIdentity networkIdentity;

		// Token: 0x04002B97 RID: 11159
		private InputBankTest inputBank;

		// Token: 0x04002B98 RID: 11160
		private CharacterBody characterBody;

		// Token: 0x04002B99 RID: 11161
		private EquipmentSlot equipmentSlot;

		// Token: 0x04002B9A RID: 11162
		[NonSerialized]
		public GameObject interactableOverride;

		// Token: 0x04002B9B RID: 11163
		private const float interactableCooldownDuration = 0.25f;

		// Token: 0x04002B9C RID: 11164
		private float interactableCooldown;
	}
}
