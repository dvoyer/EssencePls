using System;
using System.Collections;
using System.Text.RegularExpressions;
using System.Linq;
using GlobalEnums;
using Modding;
using HutongGames.PlayMaker;
using UnityEngine;

namespace EssencePls
{
	public class deathEffects : EnemyDeathEffects
    {
		public GameObject essencePrefab
        {
			get { return dreamEssenceCorpseGetPrefab; }
			set { }
        }
    }

    public class EssencePls : Mod
    {
        public override string GetVersion() => "1.0";
        public override void Initialize()
        {
            Log("EssencePls v." + GetVersion());

            On.EnemyDeathEffects.EmitEssence += alwaysYes;
        }

        public void alwaysYes(On.EnemyDeathEffects.orig_EmitEssence orig, EnemyDeathEffects self)
        {
			PlayerData playerData = GameManager.instance.playerData;
			if (!playerData.GetBool("hasDreamNail"))
			{
				return;
			}
			//deathEffects e = (deathEffects)self;
			//e.essencePrefab.Spawn(self.transform.position + self.effectOrigin);
			PlayerData playerData2 = playerData;
			playerData2.SetInt("dreamOrbs", playerData2.GetInt("dreamOrbs") + 1);
			PlayerData playerData3 = playerData;
			playerData3.SetInt("dreamOrbsSpent", playerData3.GetInt("dreamOrbsSpent") - 1);
			EventRegister.SendEvent("DREAM ORB COLLECT");
		}
	}
}
