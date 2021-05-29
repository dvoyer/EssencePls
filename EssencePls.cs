using System;
using Modding;
using UnityEngine;

namespace EssencePls
{
	public class deathEffects : EnemyDeathEffects
	{
		public GameObject essencePrefab;
    }

    public class EssencePls : Mod
    {
 		public GameObject _essencePrefab;
        public deathEffects deathEffects;
        public override string GetVersion() => "1.0.0";
        public override void Initialize()
        {
            Log("EssencePls v." + GetVersion());
			getPrefab();
			On.EnemyDeathEffects.EmitEssence += alwaysYes;
        }
		public int getPrefab()
		{
			// this is still very bad but it will work
			Resources.LoadAll<GameObject>("");
			foreach (GameObject i in Resources.FindObjectsOfTypeAll<GameObject>())
			{
				if (i.name == "dream_essence_corpse_get")
				{
					_essencePrefab = i;
					return 0;
				}
				//Log(i.name);
			}
			return -1;
		}

		public void alwaysYes(On.EnemyDeathEffects.orig_EmitEssence orig, EnemyDeathEffects self)
        {
			PlayerData playerData = GameManager.instance.playerData;
			if (!playerData.GetBool("hasDreamNail"))
			{
				return;
			}
			if (playerData.dreamOrbs < 907 && playerData.dreamOrbs > 1800)
            {
				orig(self);
            }
			else
            {
				_essencePrefab.Spawn(self.transform.position + self.effectOrigin);
				PlayerData playerData2 = playerData;
				playerData2.SetInt("dreamOrbs", playerData2.GetInt("dreamOrbs") + 1);
				PlayerData playerData3 = playerData;
				playerData3.SetInt("dreamOrbsSpent", playerData3.GetInt("dreamOrbsSpent") - 1);
				EventRegister.SendEvent("DREAM ORB COLLECT");
            }
		}
	}
}
