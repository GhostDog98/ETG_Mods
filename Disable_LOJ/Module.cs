using BepInEx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using UnityEngine;
using Dungeonator;
using Gungeon;
using MonoMod.RuntimeDetour;
using System.IO;
using System.Collections;
using System.Collections.ObjectModel;

namespace Mod
{
    [BepInDependency(ETGModMainBehaviour.GUID)]
    [BepInPlugin(GUID, NAME, VERSION)]
    public class Module : BaseUnityPlugin
    {
        public const string GUID = "ghostdog.etg.removeloj";
        public const string NAME = "Remove_LOJ";
        public const string VERSION = "0.0.0";
        public const string TEXT_COLOR = "#00FFFF";

        public void Start()
        {
            ETGModMainBehaviour.WaitForGameManagerStart(GMStart);
        }

        public void GMStart(GameManager g)
        {
            Log($"{NAME} v{VERSION} started successfully.", TEXT_COLOR);
            try
            {
                Hook mainhook = new Hook(typeof(SuperReaperController).GetMethod("Start", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance),
                typeof(JammerHook).GetMethod("StartHook", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance), typeof(SuperReaperController));
            }
            catch (Exception exception)
            {
                Log("[Remove_LOJ] Error installing hooks, contact the dev");
                Debug.LogException(exception);
            }
        }

        public static void Log(string text, string color = "#FFFFFF")
        {
            ETGModConsole.Log($"<color={color}>{text}</color>");
        }


        public class JammerHook : MonoBehaviour
        {
            private object superreapear;

            public void StartHook(SuperReaperController self)
            {
                Log("[Remove_LOJ] Starting hook...");
                SuperReaperController superreaper = UnityEngine.Object.FindObjectOfType<SuperReaperController>();

                if (superreapear != null)
                {
                    Log("[Remove_LOG] Destroyed death itself");
                    UnityEngine.Object.Destroy(superreaper.gameObject);
                }
            }
        }


    }
}
