﻿using UnityEngine;
using System.Collections;

public class Team : MonoBehaviour 
{
	// PULL THESE FROM PREFS WHEN WE CHANGE SCENES
	public int ENFORCER_LVL = 1;
	public int RIFLEMAN_LVL = 1;
	public int MEDIC_LVL = 1;
	public int ENGINEER_LVL = 1;

	// AND THESE
	public int ENFORCER_RANK = 0;
	public int RIFLEMAN_RANK = 1;
	public int MEDIC_RANK = 2;
	public int ENGINEER_RANK = 3;

	// AAAAND THESE
	public int ENFORCER_HEALTH = 1;
	public int RIFLEMAN_HEALTH = 1;
	public int MEDIC_HEALTH = 1;
	public int ENGINEER_HEALTH = 1;

	private Unit[] myTeam;

	// Use this for initialization
	void Start () 
	{
		myTeam = new Unit[] {new Enforcer(), new Rifleman(), new Medic(), new Engineer()};
		myTeam [0].SetStats (ENFORCER_LVL, ENFORCER_RANK);
		myTeam [1].SetStats (RIFLEMAN_LVL, RIFLEMAN_RANK);
		myTeam [2].SetStats (MEDIC_LVL, MEDIC_RANK);
		myTeam [3].SetStats (ENGINEER_LVL, ENGINEER_RANK);
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void MoveUnit (int Magnitude, Unit[] Team)
	{
		if (Magnitude > 3 || Magnitude < -3)
		{
			return;
		}


	}
}
