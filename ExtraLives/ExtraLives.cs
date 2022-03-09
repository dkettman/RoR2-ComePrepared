using BepInEx;
using BepInEx.Configuration;
using R2API;
using R2API.Utils;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace ExtraLives
{
    [BepInDependency(R2API.R2API.PluginGUID)]
    [R2API.Utils.R2APISubmoduleDependency("NetworkingAPI")]
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
	
	public class ExtraLives : BaseUnityPlugin
	{
        public const string PluginGUID = PluginAuthor + "." + PluginName;
        public const string PluginAuthor = "Ozzymops";
        public const string PluginName = "ExtraLives";
        public const string PluginVersion = "1.0.0";

        public static ConfigEntry<int> extraLives { get; set; }

        public void Awake()
        {
            extraLives = Config.Bind<int>(
                "Extra Lives",
                "Extra Lives",
                3,
                "Amount of extra lives to grant at the start of a run"
                );
        }

        public void Start()
        {
            RoR2.Run.onRunStartGlobal += (run) =>
            {
                if (NetworkServer.active)
                {
                    for (int i = 0; i < RoR2.NetworkUser.readOnlyInstancesList.Count; i++)
                    {
                        PlayerCharacterMasterController.instances[i].master.inventory.GiveItemString("ExtraLife", extraLives.Value);
                    }
                }
            };
        }
    }
}
