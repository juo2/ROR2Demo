using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using HG;
using RoR2.Projectile;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000A64 RID: 2660
	public class SphereSearch
	{
		// Token: 0x06003CF1 RID: 15601 RVA: 0x000FBC26 File Offset: 0x000F9E26
		public SphereSearch ClearCandidates()
		{
			this.searchData = SphereSearch.SearchData.empty;
			return this;
		}

		// Token: 0x06003CF2 RID: 15602 RVA: 0x000FBC34 File Offset: 0x000F9E34
		public SphereSearch RefreshCandidates()
		{
			Collider[] array = Physics.OverlapSphere(this.origin, this.radius, this.mask, this.queryTriggerInteraction);
			SphereSearch.Candidate[] array2 = new SphereSearch.Candidate[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				Collider collider = array[i];
				ref SphereSearch.Candidate ptr = ref array2[i];
				ptr.collider = collider;
				MeshCollider meshCollider;
				if ((meshCollider = (collider as MeshCollider)) != null && !meshCollider.convex)
				{
					ptr.position = collider.ClosestPointOnBounds(this.origin);
				}
				else
				{
					ptr.position = collider.ClosestPoint(this.origin);
				}
				ptr.difference = ptr.position - this.origin;
				ptr.distanceSqr = ptr.difference.sqrMagnitude;
			}
			this.searchData = new SphereSearch.SearchData(array2);
			return this;
		}

		// Token: 0x06003CF3 RID: 15603 RVA: 0x000FBD0B File Offset: 0x000F9F0B
		public SphereSearch OrderCandidatesByDistance()
		{
			this.searchData.OrderByDistance();
			return this;
		}

		// Token: 0x06003CF4 RID: 15604 RVA: 0x000FBD19 File Offset: 0x000F9F19
		public SphereSearch FilterCandidatesByHurtBoxTeam(TeamMask mask)
		{
			this.searchData.FilterByHurtBoxTeam(mask);
			return this;
		}

		// Token: 0x06003CF5 RID: 15605 RVA: 0x000FBD28 File Offset: 0x000F9F28
		public SphereSearch FilterCandidatesByColliderEntities()
		{
			this.searchData.FilterByColliderEntities();
			return this;
		}

		// Token: 0x06003CF6 RID: 15606 RVA: 0x000FBD36 File Offset: 0x000F9F36
		public SphereSearch FilterCandidatesByDistinctColliderEntities()
		{
			this.searchData.FilterByColliderEntitiesDistinct();
			return this;
		}

		// Token: 0x06003CF7 RID: 15607 RVA: 0x000FBD44 File Offset: 0x000F9F44
		public SphereSearch FilterCandidatesByDistinctHurtBoxEntities()
		{
			this.searchData.FilterByHurtBoxEntitiesDistinct();
			return this;
		}

		// Token: 0x06003CF8 RID: 15608 RVA: 0x000FBD52 File Offset: 0x000F9F52
		public SphereSearch FilterCandidatesByProjectileControllers()
		{
			this.searchData.FilterByProjectileControllers();
			return this;
		}

		// Token: 0x06003CF9 RID: 15609 RVA: 0x000FBD60 File Offset: 0x000F9F60
		public HurtBox[] GetHurtBoxes()
		{
			return this.searchData.GetHurtBoxes();
		}

		// Token: 0x06003CFA RID: 15610 RVA: 0x000FBD6D File Offset: 0x000F9F6D
		public void GetHurtBoxes(List<HurtBox> dest)
		{
			this.searchData.GetHurtBoxes(dest);
		}

		// Token: 0x06003CFB RID: 15611 RVA: 0x000FBD7B File Offset: 0x000F9F7B
		public void GetProjectileControllers(List<ProjectileController> dest)
		{
			this.searchData.GetProjectileControllers(dest);
		}

		// Token: 0x06003CFC RID: 15612 RVA: 0x000FBD89 File Offset: 0x000F9F89
		public void GetColliders(List<Collider> dest)
		{
			this.searchData.GetColliders(dest);
		}

		// Token: 0x04003C22 RID: 15394
		public float radius;

		// Token: 0x04003C23 RID: 15395
		public Vector3 origin;

		// Token: 0x04003C24 RID: 15396
		public LayerMask mask;

		// Token: 0x04003C25 RID: 15397
		public QueryTriggerInteraction queryTriggerInteraction;

		// Token: 0x04003C26 RID: 15398
		private SphereSearch.SearchData searchData = SphereSearch.SearchData.empty;

		// Token: 0x02000A65 RID: 2661
		private struct Candidate
		{
			// Token: 0x06003CFE RID: 15614 RVA: 0x000FBDAA File Offset: 0x000F9FAA
			public static bool HurtBoxHealthComponentIsValid(SphereSearch.Candidate candidate)
			{
				return candidate.hurtBox.healthComponent;
			}

			// Token: 0x04003C27 RID: 15399
			public Collider collider;

			// Token: 0x04003C28 RID: 15400
			public HurtBox hurtBox;

			// Token: 0x04003C29 RID: 15401
			public Vector3 position;

			// Token: 0x04003C2A RID: 15402
			public Vector3 difference;

			// Token: 0x04003C2B RID: 15403
			public float distanceSqr;

			// Token: 0x04003C2C RID: 15404
			public Transform root;

			// Token: 0x04003C2D RID: 15405
			public ProjectileController projectileController;

			// Token: 0x04003C2E RID: 15406
			public EntityLocator entityLocator;
		}

		// Token: 0x02000A66 RID: 2662
		private struct SearchData
		{
			// Token: 0x06003CFF RID: 15615 RVA: 0x000FBDBC File Offset: 0x000F9FBC
			public SearchData(SphereSearch.Candidate[] candidatesBuffer)
			{
				this.candidatesBuffer = candidatesBuffer;
				this.candidatesMapping = new int[candidatesBuffer.Length];
				this.candidatesCount = candidatesBuffer.Length;
				for (int i = 0; i < candidatesBuffer.Length; i++)
				{
					this.candidatesMapping[i] = i;
				}
				this.hurtBoxesLoaded = false;
				this.rootsLoaded = false;
				this.projectileControllersLoaded = false;
				this.entityLocatorsLoaded = false;
				this.filteredByHurtBoxes = false;
				this.filteredByHurtBoxHealthComponents = false;
				this.filteredByProjectileControllers = false;
				this.filteredByEntityLocators = false;
			}

			// Token: 0x06003D00 RID: 15616 RVA: 0x000FBE36 File Offset: 0x000FA036
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private ref SphereSearch.Candidate GetCandidate(int i)
			{
				return ref this.candidatesBuffer[this.candidatesMapping[i]];
			}

			// Token: 0x06003D01 RID: 15617 RVA: 0x000FBE4B File Offset: 0x000FA04B
			private void RemoveCandidate(int i)
			{
				ArrayUtils.ArrayRemoveAt<int>(this.candidatesMapping, ref this.candidatesCount, i, 1);
			}

			// Token: 0x06003D02 RID: 15618 RVA: 0x000FBE60 File Offset: 0x000FA060
			public void LoadHurtBoxes()
			{
				if (this.hurtBoxesLoaded)
				{
					return;
				}
				for (int i = 0; i < this.candidatesCount; i++)
				{
					ref SphereSearch.Candidate candidate = ref this.GetCandidate(i);
					candidate.hurtBox = candidate.collider.GetComponent<HurtBox>();
				}
				this.hurtBoxesLoaded = true;
			}

			// Token: 0x06003D03 RID: 15619 RVA: 0x000FBEA8 File Offset: 0x000FA0A8
			public void LoadRoots()
			{
				if (this.rootsLoaded)
				{
					return;
				}
				for (int i = 0; i < this.candidatesCount; i++)
				{
					ref SphereSearch.Candidate candidate = ref this.GetCandidate(i);
					candidate.root = candidate.collider.transform.root;
				}
				this.rootsLoaded = true;
			}

			// Token: 0x06003D04 RID: 15620 RVA: 0x000FBEF4 File Offset: 0x000FA0F4
			public void LoadProjectileControllers()
			{
				if (this.projectileControllersLoaded)
				{
					return;
				}
				this.LoadRoots();
				for (int i = 0; i < this.candidatesCount; i++)
				{
					ref SphereSearch.Candidate candidate = ref this.GetCandidate(i);
					candidate.projectileController = (candidate.root ? candidate.root.GetComponent<ProjectileController>() : null);
				}
				this.projectileControllersLoaded = true;
			}

			// Token: 0x06003D05 RID: 15621 RVA: 0x000FBF54 File Offset: 0x000FA154
			public void LoadColliderEntityLocators()
			{
				if (this.entityLocatorsLoaded)
				{
					return;
				}
				for (int i = 0; i < this.candidatesCount; i++)
				{
					ref SphereSearch.Candidate candidate = ref this.GetCandidate(i);
					candidate.entityLocator = candidate.collider.GetComponent<EntityLocator>();
				}
				this.entityLocatorsLoaded = true;
			}

			// Token: 0x06003D06 RID: 15622 RVA: 0x000FBF9C File Offset: 0x000FA19C
			public void FilterByProjectileControllers()
			{
				if (this.filteredByProjectileControllers)
				{
					return;
				}
				this.LoadProjectileControllers();
				for (int i = this.candidatesCount - 1; i >= 0; i--)
				{
					if (!this.GetCandidate(i).projectileController)
					{
						this.RemoveCandidate(i);
					}
				}
				this.filteredByProjectileControllers = true;
			}

			// Token: 0x06003D07 RID: 15623 RVA: 0x000FBFEC File Offset: 0x000FA1EC
			public void FilterByHurtBoxes()
			{
				if (this.filteredByHurtBoxes)
				{
					return;
				}
				this.LoadHurtBoxes();
				for (int i = this.candidatesCount - 1; i >= 0; i--)
				{
					if (!this.GetCandidate(i).hurtBox)
					{
						this.RemoveCandidate(i);
					}
				}
				this.filteredByHurtBoxes = true;
			}

			// Token: 0x06003D08 RID: 15624 RVA: 0x000FC03C File Offset: 0x000FA23C
			public void FilterByHurtBoxHealthComponents()
			{
				if (this.filteredByHurtBoxHealthComponents)
				{
					return;
				}
				this.FilterByHurtBoxes();
				for (int i = this.candidatesCount - 1; i >= 0; i--)
				{
					if (!this.GetCandidate(i).hurtBox.healthComponent)
					{
						this.RemoveCandidate(i);
					}
				}
				this.filteredByHurtBoxHealthComponents = true;
			}

			// Token: 0x06003D09 RID: 15625 RVA: 0x000FC094 File Offset: 0x000FA294
			public void FilterByHurtBoxTeam(TeamMask teamMask)
			{
				this.FilterByHurtBoxes();
				for (int i = this.candidatesCount - 1; i >= 0; i--)
				{
					ref SphereSearch.Candidate candidate = ref this.GetCandidate(i);
					if (!teamMask.HasTeam(candidate.hurtBox.teamIndex))
					{
						this.RemoveCandidate(i);
					}
				}
			}

			// Token: 0x06003D0A RID: 15626 RVA: 0x000FC0E0 File Offset: 0x000FA2E0
			public void FilterByHurtBoxEntitiesDistinct()
			{
				this.FilterByHurtBoxHealthComponents();
				for (int i = this.candidatesCount - 1; i >= 0; i--)
				{
					ref SphereSearch.Candidate candidate = ref this.GetCandidate(i);
					for (int j = i - 1; j >= 0; j--)
					{
						ref SphereSearch.Candidate candidate2 = ref this.GetCandidate(j);
						if (candidate.hurtBox.healthComponent == candidate2.hurtBox.healthComponent)
						{
							this.RemoveCandidate(i);
							break;
						}
					}
				}
			}

			// Token: 0x06003D0B RID: 15627 RVA: 0x000FC148 File Offset: 0x000FA348
			public void FilterByColliderEntities()
			{
				if (this.filteredByEntityLocators)
				{
					return;
				}
				this.LoadColliderEntityLocators();
				for (int i = this.candidatesCount - 1; i >= 0; i--)
				{
					ref SphereSearch.Candidate candidate = ref this.GetCandidate(i);
					if (!candidate.entityLocator || !candidate.entityLocator.entity)
					{
						this.RemoveCandidate(i);
					}
				}
				this.filteredByEntityLocators = true;
			}

			// Token: 0x06003D0C RID: 15628 RVA: 0x000FC1AC File Offset: 0x000FA3AC
			public void FilterByColliderEntitiesDistinct()
			{
				this.FilterByColliderEntities();
				for (int i = this.candidatesCount - 1; i >= 0; i--)
				{
					ref SphereSearch.Candidate candidate = ref this.GetCandidate(i);
					for (int j = i - 1; j >= 0; j--)
					{
						ref SphereSearch.Candidate candidate2 = ref this.GetCandidate(j);
						if (candidate.entityLocator.entity == candidate2.entityLocator.entity)
						{
							this.RemoveCandidate(i);
							break;
						}
					}
				}
			}

			// Token: 0x06003D0D RID: 15629 RVA: 0x000FC214 File Offset: 0x000FA414
			public void OrderByDistance()
			{
				if (this.candidatesCount == 0)
				{
					return;
				}
				bool flag = true;
				while (flag)
				{
					flag = false;
					ref SphereSearch.Candidate ptr = ref this.GetCandidate(0);
					int i = 1;
					int num = this.candidatesCount - 1;
					while (i < num)
					{
						ref SphereSearch.Candidate candidate = ref this.GetCandidate(i);
						if (ptr.distanceSqr > candidate.distanceSqr)
						{
							Util.Swap<int>(ref this.candidatesMapping[i - 1], ref this.candidatesMapping[i]);
							flag = true;
						}
						else
						{
							ptr = ref candidate;
						}
						i++;
					}
				}
			}

			// Token: 0x06003D0E RID: 15630 RVA: 0x000FC290 File Offset: 0x000FA490
			public HurtBox[] GetHurtBoxes()
			{
				this.FilterByHurtBoxes();
				HurtBox[] array = new HurtBox[this.candidatesCount];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = this.GetCandidate(i).hurtBox;
				}
				return array;
			}

			// Token: 0x06003D0F RID: 15631 RVA: 0x000FC2D0 File Offset: 0x000FA4D0
			public void GetHurtBoxes(List<HurtBox> dest)
			{
				int num = dest.Count + this.candidatesCount;
				if (dest.Capacity < num)
				{
					dest.Capacity = num;
				}
				for (int i = 0; i < this.candidatesCount; i++)
				{
					dest.Add(this.GetCandidate(i).hurtBox);
				}
			}

			// Token: 0x06003D10 RID: 15632 RVA: 0x000FC320 File Offset: 0x000FA520
			public void GetProjectileControllers(List<ProjectileController> dest)
			{
				int num = dest.Count + this.candidatesCount;
				if (dest.Capacity < num)
				{
					dest.Capacity = num;
				}
				for (int i = 0; i < this.candidatesCount; i++)
				{
					dest.Add(this.GetCandidate(i).projectileController);
				}
			}

			// Token: 0x06003D11 RID: 15633 RVA: 0x000FC370 File Offset: 0x000FA570
			public void GetColliders(List<Collider> dest)
			{
				int num = dest.Count + this.candidatesCount;
				if (dest.Capacity < num)
				{
					dest.Capacity = num;
				}
				for (int i = 0; i < this.candidatesCount; i++)
				{
					dest.Add(this.GetCandidate(i).collider);
				}
			}

			// Token: 0x04003C2F RID: 15407
			private SphereSearch.Candidate[] candidatesBuffer;

			// Token: 0x04003C30 RID: 15408
			private int[] candidatesMapping;

			// Token: 0x04003C31 RID: 15409
			private int candidatesCount;

			// Token: 0x04003C32 RID: 15410
			private bool hurtBoxesLoaded;

			// Token: 0x04003C33 RID: 15411
			private bool rootsLoaded;

			// Token: 0x04003C34 RID: 15412
			private bool projectileControllersLoaded;

			// Token: 0x04003C35 RID: 15413
			private bool entityLocatorsLoaded;

			// Token: 0x04003C36 RID: 15414
			private bool filteredByHurtBoxes;

			// Token: 0x04003C37 RID: 15415
			private bool filteredByHurtBoxHealthComponents;

			// Token: 0x04003C38 RID: 15416
			private bool filteredByProjectileControllers;

			// Token: 0x04003C39 RID: 15417
			private bool filteredByEntityLocators;

			// Token: 0x04003C3A RID: 15418
			public static readonly SphereSearch.SearchData empty = new SphereSearch.SearchData
			{
				candidatesBuffer = Array.Empty<SphereSearch.Candidate>(),
				candidatesMapping = Array.Empty<int>(),
				candidatesCount = 0,
				hurtBoxesLoaded = false
			};
		}
	}
}
