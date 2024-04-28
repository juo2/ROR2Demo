using System;
using System.Collections.Generic;
using System.Linq;
using Rewired;
using UnityEngine;

namespace RoR2.UI
{
	// Token: 0x02000D23 RID: 3363
	[RequireComponent(typeof(MPEventSystemLocator))]
	public class InputBindingControl : MonoBehaviour
	{
		// Token: 0x06004C98 RID: 19608 RVA: 0x0013C47C File Offset: 0x0013A67C
		public void Awake()
		{
			this.eventSystemLocator = base.GetComponent<MPEventSystemLocator>();
			this.bindingDisplay.actionName = this.actionName;
			this.bindingDisplay.useExplicitInputSource = true;
			this.bindingDisplay.explicitInputSource = this.inputSource;
			this.bindingDisplay.axisRange = this.axisRange;
			this.nameLabel.token = InputCatalog.GetActionNameToken(this.actionName, this.axisRange);
			this.action = ReInput.mapping.GetAction(this.actionName);
		}

		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x06004C99 RID: 19609 RVA: 0x0013C506 File Offset: 0x0013A706
		private bool isListening
		{
			get
			{
				return this.inputMapperHelper.isListening;
			}
		}

		// Token: 0x06004C9A RID: 19610 RVA: 0x0013C513 File Offset: 0x0013A713
		public void ToggleListening()
		{
			if (!this.isListening)
			{
				this.StartListening();
				return;
			}
			this.StopListening();
		}

		// Token: 0x06004C9B RID: 19611 RVA: 0x0013C52C File Offset: 0x0013A72C
		public void StartListening()
		{
			if (!this.button.IsInteractable())
			{
				return;
			}
			this.inputMapperHelper.Stop();
			MPEventSystem eventSystem = this.eventSystemLocator.eventSystem;
			Player player;
			if (eventSystem == null)
			{
				player = null;
			}
			else
			{
				LocalUser localUser = eventSystem.localUser;
				player = ((localUser != null) ? localUser.inputPlayer : null);
			}
			this.currentPlayer = player;
			if (this.currentPlayer == null)
			{
				return;
			}
			IList<Controller> controllers = null;
			MPEventSystem.InputSource inputSource = this.inputSource;
			if (inputSource != MPEventSystem.InputSource.MouseAndKeyboard)
			{
				if (inputSource == MPEventSystem.InputSource.Gamepad)
				{
					controllers = this.currentPlayer.controllers.Joysticks.ToArray<Joystick>();
				}
			}
			else
			{
				controllers = new Controller[]
				{
					this.currentPlayer.controllers.Keyboard,
					this.currentPlayer.controllers.Mouse
				};
			}
			this.inputMapperHelper.Start(this.currentPlayer, controllers, this.action, this.axisRange);
			if (this.button)
			{
				this.button.interactable = false;
			}
		}

		// Token: 0x06004C9C RID: 19612 RVA: 0x0013C612 File Offset: 0x0013A812
		private void StopListening()
		{
			if (this.currentPlayer == null)
			{
				return;
			}
			this.currentPlayer = null;
			this.inputMapperHelper.Stop();
		}

		// Token: 0x06004C9D RID: 19613 RVA: 0x0013C62F File Offset: 0x0013A82F
		private void OnEnable()
		{
			if (!this.eventSystemLocator.eventSystem)
			{
				base.enabled = false;
				return;
			}
			this.inputMapperHelper = this.eventSystemLocator.eventSystem.inputMapperHelper;
		}

		// Token: 0x06004C9E RID: 19614 RVA: 0x0013C661 File Offset: 0x0013A861
		private void OnDisable()
		{
			this.StopListening();
		}

		// Token: 0x06004C9F RID: 19615 RVA: 0x0013C66C File Offset: 0x0013A86C
		private void Update()
		{
			if (this.button)
			{
				MPEventSystemLocator mpeventSystemLocator = this.eventSystemLocator;
				if (!((mpeventSystemLocator != null) ? mpeventSystemLocator.eventSystem : null))
				{
					Debug.LogError("MPEventSystem is invalid.");
					return;
				}
				bool flag = !this.eventSystemLocator.eventSystem.inputMapperHelper.isListening;
				if (!flag)
				{
					this.buttonReactivationTime = Time.unscaledTime + 0.25f;
				}
				this.button.interactable = (flag && this.buttonReactivationTime <= Time.unscaledTime);
			}
		}

		// Token: 0x040049A2 RID: 18850
		public string actionName;

		// Token: 0x040049A3 RID: 18851
		public AxisRange axisRange;

		// Token: 0x040049A4 RID: 18852
		public LanguageTextMeshController nameLabel;

		// Token: 0x040049A5 RID: 18853
		public InputBindingDisplayController bindingDisplay;

		// Token: 0x040049A6 RID: 18854
		public MPEventSystem.InputSource inputSource;

		// Token: 0x040049A7 RID: 18855
		public MPButton button;

		// Token: 0x040049A8 RID: 18856
		private MPEventSystemLocator eventSystemLocator;

		// Token: 0x040049A9 RID: 18857
		private InputAction action;

		// Token: 0x040049AA RID: 18858
		private InputMapperHelper inputMapperHelper;

		// Token: 0x040049AB RID: 18859
		private Player currentPlayer;

		// Token: 0x040049AC RID: 18860
		private float buttonReactivationTime;
	}
}
