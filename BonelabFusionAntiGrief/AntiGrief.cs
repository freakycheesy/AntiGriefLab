using HarmonyLib;
using LabFusion;
using LabFusion.Entities;
using LabFusion.Network;
using LabFusion.Network.Serialization;
using LabFusion.Player;
using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UnityEngine;
using static Il2Cpp.Interop;





namespace BonelabFusionAntiGrief 
{
    [HarmonyPatch(typeof(RigNameTag))]
    public class AntiGrief : MelonMod
    {
       
        public override void OnInitializeMelon()
        {
            LoggerInstance.Msg("Initialized.");
            HarmonyInstance.PatchAll();
        }

        [HarmonyPatch(typeof(ConnectionRequestMessage))]
        public static class ConnectionRequestMessagePatch
        {
            [HarmonyPatch("OnHandleMessage")]
            [HarmonyPrefix]
            public static void Prefix(ReceivedMessage received)
            {
                try
                {
                    var data = received.ReadData<ConnectionRequestData>();
                    MelonLogger.Msg($"[AntiGrief] Incoming connection attempt: PlatformID={data.PlatformID}, Version={data.Version}");

                }
                catch (Exception ex)
                {
                    MelonLogger.Warning($"[AntiGrief] Failed to log connection attempt: {ex}");
                }
            }
        }


    }


}
