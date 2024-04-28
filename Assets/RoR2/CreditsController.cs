using System;
using RoR2.UI;
using UnityEngine;
using UnityEngine.Events;

namespace RoR2
{
	// Token: 0x02000687 RID: 1671
	[RequireComponent(typeof(VoteController))]
	public class CreditsController : MonoBehaviour
	{
		// Token: 0x17000290 RID: 656
		// (get) Token: 0x060020A0 RID: 8352 RVA: 0x0008C34A File Offset: 0x0008A54A
		private static GameObject creditsPanelPrefab
		{
			get
			{
				return LegacyResourcesAPI.Load<GameObject>("Prefabs/UI/Credits/CreditsPanel");
			}
		}

		// Token: 0x060020A1 RID: 8353 RVA: 0x0008C356 File Offset: 0x0008A556
		private void Awake()
		{
			this.voteController = base.GetComponent<VoteController>();
		}

		// Token: 0x060020A2 RID: 8354 RVA: 0x0008C364 File Offset: 0x0008A564
		private void OnEnable()
		{
			this.creditsPanelController = UnityEngine.Object.Instantiate<GameObject>(CreditsController.creditsPanelPrefab, RoR2Application.instance.mainCanvas.transform).GetComponent<CreditsPanelController>();
			this.creditsPanelController.voteInfoPanel.voteController = this.voteController;
			this.creditsPanelController.skipButton.onClick.AddListener(new UnityAction(this.SubmitLocalVotesToEnd));
		}

		// Token: 0x060020A3 RID: 8355 RVA: 0x0008C3CC File Offset: 0x0008A5CC
		private void OnDisable()
		{
			if (this.creditsPanelController)
			{
				UnityEngine.Object.Destroy(this.creditsPanelController.gameObject);
			}
		}

		// Token: 0x060020A4 RID: 8356 RVA: 0x0008C3EB File Offset: 0x0008A5EB
		private void Update()
		{
			if (!this.creditsPanelController)
			{
				this.SubmitLocalVotesToEnd();
				base.enabled = false;
			}
		}

		// Token: 0x060020A5 RID: 8357 RVA: 0x0008C408 File Offset: 0x0008A608
		private void SubmitLocalVotesToEnd()
		{
			foreach (NetworkUser networkUser in NetworkUser.readOnlyLocalPlayersList)
			{
				networkUser.CallCmdSubmitVote(base.gameObject, 0);
			}
		}

		// Token: 0x040025E1 RID: 9697
		private CreditsPanelController creditsPanelController;

		// Token: 0x040025E2 RID: 9698
		private VoteController voteController;
	}
}
