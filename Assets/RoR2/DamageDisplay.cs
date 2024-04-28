using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TMPro;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000689 RID: 1673
	public class DamageDisplay : MonoBehaviour
	{
		// Token: 0x060020A9 RID: 8361 RVA: 0x0008C4AF File Offset: 0x0008A6AF
		static DamageDisplay()
		{
			UICamera.onUICameraPreCull += DamageDisplay.OnUICameraPreCull;
			RoR2Application.onUpdate += DamageDisplay.UpdateAll;
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x060020AA RID: 8362 RVA: 0x0008C4EC File Offset: 0x0008A6EC
		public static ReadOnlyCollection<DamageDisplay> readOnlyInstancesList
		{
			get
			{
				return DamageDisplay._readOnlyInstancesList;
			}
		}

		// Token: 0x060020AB RID: 8363 RVA: 0x0008C4F4 File Offset: 0x0008A6F4
		private void Start()
		{
			this.velocity = Vector3.Normalize(Vector3.up + new Vector3(UnityEngine.Random.Range(-this.offset, this.offset), 0f, UnityEngine.Random.Range(-this.offset, this.offset))) * this.magnitude;
			DamageDisplay.instancesList.Add(this);
			this.internalPosition = base.transform.position;
		}

		// Token: 0x060020AC RID: 8364 RVA: 0x0008C56B File Offset: 0x0008A76B
		private void OnDestroy()
		{
			DamageDisplay.instancesList.Remove(this);
		}

		// Token: 0x060020AD RID: 8365 RVA: 0x0008C57C File Offset: 0x0008A77C
		public void SetValues(GameObject victim, GameObject attacker, float damage, bool crit, DamageColorIndex damageColorIndex)
		{
			this.victimTeam = TeamIndex.Neutral;
			this.attackerTeam = TeamIndex.Neutral;
			this.scale = 1f;
			this.victim = victim;
			this.attacker = attacker;
			this.crit = crit;
			this.baseColor = DamageColor.FindColor(damageColorIndex);
			string text = Mathf.CeilToInt(Mathf.Abs(damage)).ToString();
			this.heal = (damage < 0f);
			if (this.heal)
			{
				damage = -damage;
				base.transform.parent = victim.transform;
				text = "+" + text;
				this.baseColor = DamageColor.FindColor(DamageColorIndex.Heal);
				this.baseOutlineColor = this.baseColor * Color.gray;
			}
			if (victim)
			{
				TeamComponent component = victim.GetComponent<TeamComponent>();
				if (component)
				{
					this.victimTeam = component.teamIndex;
				}
			}
			if (attacker)
			{
				TeamComponent component2 = attacker.GetComponent<TeamComponent>();
				if (component2)
				{
					this.attackerTeam = component2.teamIndex;
				}
			}
			if (crit)
			{
				text += "!";
				this.baseOutlineColor = Color.red;
			}
			this.textMeshComponent.text = text;
			this.UpdateMagnitude();
		}

		// Token: 0x060020AE RID: 8366 RVA: 0x0008C6A8 File Offset: 0x0008A8A8
		private void UpdateMagnitude()
		{
			float fontSize = this.magnitudeCurve.Evaluate(this.life / this.maxLife) * this.textMagnitude * this.scale;
			this.textMeshComponent.fontSize = fontSize;
		}

		// Token: 0x060020AF RID: 8367 RVA: 0x0008C6E8 File Offset: 0x0008A8E8
		private static void UpdateAll()
		{
			for (int i = DamageDisplay.instancesList.Count - 1; i >= 0; i--)
			{
				DamageDisplay.instancesList[i].DoUpdate();
			}
		}

		// Token: 0x060020B0 RID: 8368 RVA: 0x0008C71C File Offset: 0x0008A91C
		private void DoUpdate()
		{
			this.UpdateMagnitude();
			this.life += Time.deltaTime;
			if (this.life >= this.maxLife)
			{
				UnityEngine.Object.Destroy(base.gameObject);
				return;
			}
			this.velocity += this.gravity * Vector3.down * Time.deltaTime;
			this.internalPosition += this.velocity * Time.deltaTime;
		}

		// Token: 0x060020B1 RID: 8369 RVA: 0x0008C7A8 File Offset: 0x0008A9A8
		private static void OnUICameraPreCull(UICamera uiCamera)
		{
			Camera camera = uiCamera.camera;
			Camera sceneCam = uiCamera.cameraRigController.sceneCam;
			GameObject target = uiCamera.cameraRigController.target;
			TeamIndex targetTeamIndex = uiCamera.cameraRigController.targetTeamIndex;
			for (int i = 0; i < DamageDisplay.instancesList.Count; i++)
			{
				DamageDisplay damageDisplay = DamageDisplay.instancesList[i];
				Color b = Color.white;
				if (!damageDisplay.heal)
				{
					if (targetTeamIndex == damageDisplay.victimTeam)
					{
						b = new Color(0.5568628f, 0.29411766f, 0.6039216f);
					}
					else if (targetTeamIndex == damageDisplay.attackerTeam && target != damageDisplay.attacker)
					{
						b = Color.gray;
					}
				}
				damageDisplay.textMeshComponent.color = Color.Lerp(Color.white, damageDisplay.baseColor * b, damageDisplay.life / 0.2f);
				damageDisplay.textMeshComponent.outlineColor = Color.Lerp(Color.white, damageDisplay.baseOutlineColor * b, damageDisplay.life / 0.2f);
				Vector3 position = damageDisplay.internalPosition;
				Vector3 vector = sceneCam.WorldToScreenPoint(position);
				vector.z = ((vector.z > 0f) ? 1f : -1f);
				Vector3 position2 = camera.ScreenToWorldPoint(vector);
				damageDisplay.transform.position = position2;
			}
		}

		// Token: 0x040025E6 RID: 9702
		private static List<DamageDisplay> instancesList = new List<DamageDisplay>();

		// Token: 0x040025E7 RID: 9703
		private static ReadOnlyCollection<DamageDisplay> _readOnlyInstancesList = new ReadOnlyCollection<DamageDisplay>(DamageDisplay.instancesList);

		// Token: 0x040025E8 RID: 9704
		public TextMeshPro textMeshComponent;

		// Token: 0x040025E9 RID: 9705
		public AnimationCurve magnitudeCurve;

		// Token: 0x040025EA RID: 9706
		public float maxLife = 3f;

		// Token: 0x040025EB RID: 9707
		public float gravity = 9.81f;

		// Token: 0x040025EC RID: 9708
		public float magnitude = 3f;

		// Token: 0x040025ED RID: 9709
		public float offset = 20f;

		// Token: 0x040025EE RID: 9710
		private Vector3 velocity;

		// Token: 0x040025EF RID: 9711
		public float textMagnitude = 0.01f;

		// Token: 0x040025F0 RID: 9712
		private float vel;

		// Token: 0x040025F1 RID: 9713
		private float life;

		// Token: 0x040025F2 RID: 9714
		private float scale = 1f;

		// Token: 0x040025F3 RID: 9715
		[HideInInspector]
		public Color baseColor = Color.white;

		// Token: 0x040025F4 RID: 9716
		[HideInInspector]
		public Color baseOutlineColor = Color.gray;

		// Token: 0x040025F5 RID: 9717
		private GameObject victim;

		// Token: 0x040025F6 RID: 9718
		private GameObject attacker;

		// Token: 0x040025F7 RID: 9719
		private TeamIndex victimTeam;

		// Token: 0x040025F8 RID: 9720
		private TeamIndex attackerTeam;

		// Token: 0x040025F9 RID: 9721
		private bool crit;

		// Token: 0x040025FA RID: 9722
		private bool heal;

		// Token: 0x040025FB RID: 9723
		private Vector3 internalPosition;
	}
}
