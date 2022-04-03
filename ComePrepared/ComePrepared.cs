using BepInEx;
using BepInEx.Configuration;
using R2API;
using R2API.Utils;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace ComePrepared
{
    [BepInDependency(R2API.R2API.PluginGUID)]
    [R2API.Utils.R2APISubmoduleDependency("NetworkingAPI")]
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
	
	public class ComePrepared : BaseUnityPlugin
	{
        public const string PluginGUID = PluginAuthor + "." + PluginName;
        public const string PluginAuthor = "Watch_Me_Be_Meh";
        public const string PluginName = "ComePrepared";
        public const string PluginVersion = "0.1.0";

        public static ConfigEntry<int> comePrepared { get; set; }

        public void Awake()
        {
            comePrepared = Config.Bind<int>(
                "ComePrepared",
                "ItemSelection",
                1,
                "What item to start with: 0 (random), 1 (Medkit), 2 (Cautious Slug), 3 (Monster Tooth), 4 (Bungus)"
                );
        }

        public void Start()
        {
            //Initialize our Logging class
            Log.Init(Logger);

            RoR2.Run.onRunStartGlobal += (run) =>
            {
                if (NetworkServer.active)
                {
                    //for (int i = 0; i < RoR2.NetworkUser.readOnlyInstancesList.Count; i++)
                    //{
                    //    PlayerCharacterMasterController.instances[i].master.inventory.GiveItemString("ExtraLife", extraLives.Value);
                    //}

                    int chosenItem = 0;
                    
                    string[] items = { "Medkit", "HealWhileSafe", "Tooth", "Mushroom" };

                    if (comePrepared.Value == 0)
                    {
                        Random rnd = new Random();
                        chosenItem = Random.Range(0, items.Length);
                    }

                    Log.LogInfo(nameof(Awake) + " Config: " + chosenItem); 
                    
                    for ( int i = 0; i < RoR2.NetworkUser.readOnlyInstancesList.Count; i++ )
                    {
                        PlayerCharacterMasterController.instances[i].master.inventory.GiveItemString(items[chosenItem], 1);
                    }
                }
            };
        }
    }
}
