using System;
using Rewired;
using RoR2.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RoR2
{
	// Token: 0x020008D5 RID: 2261
	[RequireComponent(typeof(MPEventSystemLocator))]
	public class InputStickVisualizer : MonoBehaviour
	{
		// Token: 0x060032A0 RID: 12960 RVA: 0x000D5AF4 File Offset: 0x000D3CF4
		private void Awake()
		{
			this.eventSystemLocator = base.GetComponent<MPEventSystemLocator>();
		}

		// Token: 0x060032A1 RID: 12961 RVA: 0x000D5B02 File Offset: 0x000D3D02
		private Player GetPlayer()
		{
			MPEventSystem eventSystem = this.eventSystemLocator.eventSystem;
			if (eventSystem == null)
			{
				return null;
			}
			return eventSystem.player;
		}

		// Token: 0x060032A2 RID: 12962 RVA: 0x000D5B1A File Offset: 0x000D3D1A
		private CameraRigController GetCameraRigController()
		{
			if (CameraRigController.readOnlyInstancesList.Count <= 0)
			{
				return null;
			}
			return CameraRigController.readOnlyInstancesList[0];
		}

		// Token: 0x060032A3 RID: 12963 RVA: 0x000D5B38 File Offset: 0x000D3D38
		private void SetBarValues(Vector2 vector, Scrollbar scrollbarX, Scrollbar scrollbarY)
		{
			if (scrollbarX)
			{
				scrollbarX.value = Util.Remap(vector.x, -1f, 1f, 0f, 1f);
			}
			if (scrollbarY)
			{
				scrollbarY.value = Util.Remap(vector.y, -1f, 1f, 0f, 1f);
			}
		}

		// Token: 0x060032A4 RID: 12964 RVA: 0x000D5BA0 File Offset: 0x000D3DA0
		private void Update()
		{
			Player player = this.GetPlayer();
			if (!this.GetCameraRigController() || player == null)
			{
				return;
			}
			Vector2 vector = new Vector2(player.GetAxis(0), player.GetAxis(1));
			Vector2 vector2 = new Vector2(player.GetAxis(16), player.GetAxis(17));
			this.SetBarValues(vector, this.moveXBar, this.moveYBar);
			this.SetBarValues(vector2, this.aimXBar, this.aimYBar);
			this.moveXLabel.text = string.Format("move.x={0:0.0000}", vector.x);
			this.moveYLabel.text = string.Format("move.y={0:0.0000}", vector.y);
			this.aimXLabel.text = string.Format("aim.x={0:0.0000}", vector2.x);
			this.aimYLabel.text = string.Format("aim.y={0:0.0000}", vector2.y);
		}

		// Token: 0x040033C8 RID: 13256
		[Header("Move")]
		public Scrollbar moveXBar;

		// Token: 0x040033C9 RID: 13257
		public Scrollbar moveYBar;

		// Token: 0x040033CA RID: 13258
		public TextMeshProUGUI moveXLabel;

		// Token: 0x040033CB RID: 13259
		public TextMeshProUGUI moveYLabel;

		// Token: 0x040033CC RID: 13260
		[Header("Aim")]
		public Scrollbar aimXBar;

		// Token: 0x040033CD RID: 13261
		public Scrollbar aimYBar;

		// Token: 0x040033CE RID: 13262
		public TextMeshProUGUI aimXLabel;

		// Token: 0x040033CF RID: 13263
		public TextMeshProUGUI aimYLabel;

		// Token: 0x040033D0 RID: 13264
		public Scrollbar aimStickPostSmoothingXBar;

		// Token: 0x040033D1 RID: 13265
		public Scrollbar aimStickPostSmoothingYBar;

		// Token: 0x040033D2 RID: 13266
		public Scrollbar aimStickPostDualZoneXBar;

		// Token: 0x040033D3 RID: 13267
		public Scrollbar aimStickPostDualZoneYBar;

		// Token: 0x040033D4 RID: 13268
		public Scrollbar aimStickPostExponentXBar;

		// Token: 0x040033D5 RID: 13269
		public Scrollbar aimStickPostExponentYBar;

		// Token: 0x040033D6 RID: 13270
		private MPEventSystemLocator eventSystemLocator;
	}
}
