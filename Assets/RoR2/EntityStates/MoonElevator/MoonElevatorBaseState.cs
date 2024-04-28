using System;
using RoR2;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace EntityStates.MoonElevator
{
	// Token: 0x02000238 RID: 568
	public abstract class MoonElevatorBaseState : BaseState
	{
		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000A0F RID: 2575 RVA: 0x00029B2B File Offset: 0x00027D2B
		public virtual EntityState nextState
		{
			get
			{
				return new Uninitialized();
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000A10 RID: 2576 RVA: 0x0000CF8A File Offset: 0x0000B18A
		public virtual Interactability interactability
		{
			get
			{
				return Interactability.Disabled;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000A11 RID: 2577 RVA: 0x0000CF8A File Offset: 0x0000B18A
		public virtual bool goToNextStateAutomatically
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000A12 RID: 2578 RVA: 0x0000CF8A File Offset: 0x0000B18A
		public virtual bool showBaseEffects
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x00029B34 File Offset: 0x00027D34
		public override void OnEnter()
		{
			base.OnEnter();
			this.genericInteraction = base.GetComponent<GenericInteraction>();
			Util.PlaySound(this.enterSfxString, base.gameObject);
			if (NetworkServer.active)
			{
				this.genericInteraction.Networkinteractability = this.interactability;
				if (this.interactability == Interactability.Available)
				{
					GenericInteraction.InteractorUnityEvent onActivation = this.genericInteraction.onActivation;
					if (onActivation != null)
					{
						onActivation.AddListener(new UnityAction<Interactor>(this.OnInteractionBegin));
					}
				}
			}
			base.FindModelChild("EffectBase").gameObject.SetActive(this.showBaseEffects);
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x00029BC4 File Offset: 0x00027DC4
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge > this.duration && base.isAuthority && this.goToNextStateAutomatically)
			{
				this.outer.SetNextState(this.nextState);
			}
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x000026ED File Offset: 0x000008ED
		protected virtual void OnInteractionBegin(Interactor activator)
		{
		}

		// Token: 0x04000BBB RID: 3003
		[SerializeField]
		public float duration;

		// Token: 0x04000BBC RID: 3004
		[SerializeField]
		public string enterSfxString;

		// Token: 0x04000BBD RID: 3005
		protected GenericInteraction genericInteraction;
	}
}
