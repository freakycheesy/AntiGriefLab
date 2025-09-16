using HarmonyLib;
using LabFusion.Entities;
using LabFusion.Network;
using LabFusion.Player;
using MelonLoader;
using System;

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
                string message = "Dummy Text";
                try
                {
                    var data = received.ReadData<ConnectionRequestData>();
                    message = $"[AntiGrief] Incoming connection attempt: PlatformID={data.PlatformID}, Version={data.Version}";
                    var playerId = PlayerIDManager.GetPlayerID(received.Sender.Value);
                    if (playerId != null) {
                        var metadata = playerId.Metadata;
                        message += $"Username={metadata.Username}, Nickname={metadata.Nickname}, Description={metadata.Description}";
                    }
                    MelonLogger.Msg(message);
                }
                catch (Exception ex)
                {
                    message = $"[AntiGrief] Failed to log connection attempt: {ex}";
                    MelonLogger.Warning(message, ex);
                }
            }
        }

    }


}
