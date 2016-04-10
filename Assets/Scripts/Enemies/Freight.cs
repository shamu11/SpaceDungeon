﻿using UnityEngine;
using System.Collections;

public class Freight : Unit
{
	/*==================================
			   Ability Indexes
	===================================*/
	private readonly int SLAM 			= 0;
	private readonly int CHARGE		 	= 1;
	private readonly int SELF_DESTRUCT	= 2;

	/*==================================
			Character stat values
	===================================*/
	private readonly int[,] LVL_DMG = new int[,] { {5, 10}, {6, 12}, {7, 13}, {8, 15}, {9, 16} };
	private readonly int[] LVL_HEALTH = new int[] {22, 27, 32, 37, 42};
	private readonly int[] LVL_DODGE = new int[] {0, 5, 10, 15, 20};
	private readonly int[] LVL_SPEED = new int[] {1, 2, 2, 3, 3};
	private readonly int[] LVL_CRIT = new int[] {5, 5, 6, 6, 7};

	public Freight () : base ()
	{
		int NewLevel = 0;
		BaseHealth = LVL_HEALTH[NewLevel];
		BaseSpeed = LVL_SPEED[NewLevel];
		BaseDodge = LVL_DODGE[NewLevel];
		BaseCrit = LVL_CRIT[NewLevel];
		BaseDmg = new int[] {LVL_DMG[NewLevel, 0], LVL_DMG[NewLevel, 1]};
		BaseArmor = 0;

		CritMods = new int[] {0, 0, 0};
		DmgMods = new float[] {0f, -0.20f, 0.5f};
		AccMods = new int[] {75, 85, 85};
		DebuffMods = new float[] {0f, 0f, 0f, -0.15f, 0.10f};
		ValidRanks = new bool[][] {
			new bool [] { false, true, true, true, false, false, false },	// Rifle	1-4
			new bool [] { true, true, false, false, false, false, false },	// Bayonet	1-2
			new bool [] { true, true, true, false, false, false, false },	// Shotgun	1-3 all
			new bool [] { true, true, true, true, false, false, false },	// Net Gun	1-4
			new bool [] { false, false, false, false, true, false, false }	// Reload	self
		};
		IsMultiHit = new bool[] { false, false, true, false, false };

		CurrHealth = BaseHealth;
		Level = 1;
		Rank = 1;
		Category = "Freight";
		IsMech = true;
		IsFriendly = false;
		HasPlayed = false;
	}

	public override void SetStats (int NewLevel, int NewRank, int NewHealth)
	{
		NewLevel--;
		this.BaseHealth = this.LVL_HEALTH[NewLevel];
		this.BaseSpeed = this.LVL_SPEED[NewLevel];
		this.BaseDodge = this.LVL_DODGE[NewLevel];
		this.BaseCrit = this.LVL_CRIT[NewLevel];
		this.BaseDmg = new int[] {this.LVL_DMG[NewLevel, 0], this.LVL_DMG[NewLevel, 1]};
		this.BaseArmor = 0;

		this.CurrHealth = NewHealth;
		this.Level = NewLevel;
		this.Rank = NewRank;
	}

	public override bool MakeMove (int MoveID, Unit[] Allies, Unit[] Enemies, Unit Target)
	{
		if (MoveID == CHARGE) 
		{
			MoveUnit (Allies, this, 1);
		}

		if (MoveID == SLAM || MoveID == CHARGE) 
		{
			if (!this.CheckHit (MoveID, Target)) 
			{
				return FAILURE;
			}
		}

		if (MoveID == SLAM) 
		{
			Target.RemoveHealth (RollDamage (MoveID, this.BaseDmg, Target));
			return SUCCESS;
		}
		else if (MoveID == CHARGE)
		{
			Target.MoveUnit (Enemies, Target, -1);
			Target.RemoveHealth (RollDamage (MoveID, this.BaseDmg, Target));
		}

		return FAILURE;
	}
}

