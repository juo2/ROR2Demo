using System;
using Rewired;
using Rewired.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace RoR2.UI
{
	// Token: 0x02000CAA RID: 3242
	public class MPInput : BaseInput, IMouseInputSource
	{
		// Token: 0x060049E6 RID: 18918 RVA: 0x0012FB1E File Offset: 0x0012DD1E
		protected override void Awake()
		{
			base.Awake();
			this.eventSystem = base.GetComponent<MPEventSystem>();
		}

		// Token: 0x060049E7 RID: 18919 RVA: 0x0012FB32 File Offset: 0x0012DD32
		private static int MouseButtonToAction(int button)
		{
			switch (button)
			{
			case 0:
				return 20;
			case 1:
				return 21;
			case 2:
				return 22;
			default:
				return -1;
			}
		}

		// Token: 0x060049E8 RID: 18920 RVA: 0x0012FB52 File Offset: 0x0012DD52
		public override bool GetMouseButtonDown(int button)
		{
			return this.player.GetButtonDown(MPInput.MouseButtonToAction(button));
		}

		// Token: 0x060049E9 RID: 18921 RVA: 0x0012FB65 File Offset: 0x0012DD65
		public override bool GetMouseButtonUp(int button)
		{
			return this.player.GetButtonUp(MPInput.MouseButtonToAction(button));
		}

		// Token: 0x060049EA RID: 18922 RVA: 0x0012FB78 File Offset: 0x0012DD78
		public override bool GetMouseButton(int button)
		{
			return this.player.GetButton(MPInput.MouseButtonToAction(button));
		}

		// Token: 0x060049EB RID: 18923 RVA: 0x0012FB8B File Offset: 0x0012DD8B
		public void CenterCursor()
		{
			this.internalMousePosition = new Vector2((float)Screen.width * 0.5f, (float)Screen.height * 0.5f);
		}

		// Token: 0x060049EC RID: 18924 RVA: 0x0012FBB0 File Offset: 0x0012DDB0
		public void Update()
		{
			if (!this.eventSystem.isCursorVisible)
			{
				return;
			}
			float num = (float)Screen.width;
			float num2 = (float)Screen.height;
			float num3 = Mathf.Min(num / 1920f, num2 / 1080f);
			this.internalScreenPositionDelta = Vector2.zero;
			if (this.eventSystem.currentInputSource == MPEventSystem.InputSource.MouseAndKeyboard)
			{
				if (Application.isFocused)
				{
					this.internalMousePosition = Input.mousePosition;
				}
			}
			else
			{
				Vector2 a = new Vector2(this.player.GetAxis(23), this.player.GetAxis(24));
				float magnitude = a.magnitude;
				this.stickMagnitude = Mathf.Min(Mathf.MoveTowards(this.stickMagnitude, magnitude, this.cursorAcceleration * Time.unscaledDeltaTime), magnitude);
				float num4 = this.stickMagnitude;
				if (this.eventSystem.isHovering)
				{
					num4 *= this.cursorStickyModifier;
				}
				Vector2 a2 = (magnitude == 0f) ? Vector2.zero : (a * (num4 / magnitude));
				float d = 1920f * this.cursorScreenSpeed * num3;
				this.internalScreenPositionDelta = a2 * Time.unscaledDeltaTime * d;
				this.internalMousePosition += this.internalScreenPositionDelta;
			}
			this.internalMousePosition.x = Mathf.Clamp(this.internalMousePosition.x, 0f, num);
			this.internalMousePosition.y = Mathf.Clamp(this.internalMousePosition.y, 0f, num2);
			this._scrollDelta = new Vector2(0f, this.player.GetAxis(26));
		}

		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x060049ED RID: 18925 RVA: 0x0012FD50 File Offset: 0x0012DF50
		public override Vector2 mousePosition
		{
			get
			{
				return this.internalMousePosition;
			}
		}

		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x060049EE RID: 18926 RVA: 0x0012FD58 File Offset: 0x0012DF58
		public override Vector2 mouseScrollDelta
		{
			get
			{
				return this._scrollDelta;
			}
		}

		// Token: 0x060049EF RID: 18927 RVA: 0x0012FD60 File Offset: 0x0012DF60
		public bool GetButtonDown(int button)
		{
			return this.GetMouseButtonDown(button);
		}

		// Token: 0x060049F0 RID: 18928 RVA: 0x0012FD69 File Offset: 0x0012DF69
		public bool GetButtonUp(int button)
		{
			return this.GetMouseButtonUp(button);
		}

		// Token: 0x060049F1 RID: 18929 RVA: 0x0012FD72 File Offset: 0x0012DF72
		public bool GetButton(int button)
		{
			return this.GetMouseButton(button);
		}

		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x060049F2 RID: 18930 RVA: 0x0012FD7B File Offset: 0x0012DF7B
		public int playerId
		{
			get
			{
				return this.player.id;
			}
		}

		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x060049F3 RID: 18931 RVA: 0x0012FD88 File Offset: 0x0012DF88
		public bool locked
		{
			get
			{
				return !this.eventSystem.isCursorVisible;
			}
		}

		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x060049F4 RID: 18932 RVA: 0x00014F2E File Offset: 0x0001312E
		public int buttonCount
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x060049F5 RID: 18933 RVA: 0x0012FD50 File Offset: 0x0012DF50
		public Vector2 screenPosition
		{
			get
			{
				return this.internalMousePosition;
			}
		}

		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x060049F6 RID: 18934 RVA: 0x0012FD98 File Offset: 0x0012DF98
		public Vector2 screenPositionDelta
		{
			get
			{
				return this.internalScreenPositionDelta;
			}
		}

		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x060049F7 RID: 18935 RVA: 0x0012FD58 File Offset: 0x0012DF58
		public Vector2 wheelDelta
		{
			get
			{
				return this._scrollDelta;
			}
		}

		// Token: 0x060049F9 RID: 18937 RVA: 0x0012FDD0 File Offset: 0x0012DFD0
		bool IMouseInputSource.get_enabled()
		{
			return base.enabled;
		}

		// Token: 0x040046B7 RID: 18103
		public Player player;

		// Token: 0x040046B8 RID: 18104
		private MPEventSystem eventSystem;

		// Token: 0x040046B9 RID: 18105
		[FormerlySerializedAs("useAcceleration")]
		public bool useCursorAcceleration = true;

		// Token: 0x040046BA RID: 18106
		[FormerlySerializedAs("acceleration")]
		public float cursorAcceleration = 8f;

		// Token: 0x040046BB RID: 18107
		public float cursorStickyModifier = 0.33333334f;

		// Token: 0x040046BC RID: 18108
		public float cursorScreenSpeed = 0.75f;

		// Token: 0x040046BD RID: 18109
		private float stickMagnitude;

		// Token: 0x040046BE RID: 18110
		private Vector2 _scrollDelta;

		// Token: 0x040046BF RID: 18111
		private Vector2 internalScreenPositionDelta;

		// Token: 0x040046C0 RID: 18112
		public Vector2 internalMousePosition;
	}
}
