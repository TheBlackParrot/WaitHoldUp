using HarmonyLib;
using UnityEngine;

namespace WaitHoldUp.Patches;

[HarmonyPatch]
internal class AudioTimeSyncControllerPatch
{
    private static PauseController? _pauseController;
    
    [HarmonyPatch(typeof(AudioTimeSyncController), "StartSong")]
    // ReSharper disable once InconsistentNaming
    internal static void Postfix()
    {
        if (GameObject.Find("ReplayerCore") != null)
        {
            Plugin.Log.Info("ReplayerCore found, we're probably in a BeatLeader replay");
            return;
        }

        // i refuse to install ScoreSaber on any of my machines, so i'm just following what Camera2 checks for
        // https://github.com/kinsi55/CS_BeatSaber_Camera2/blob/6b6807d60da8e7c922bba0c74800f9095d93c247/Utils/Scoresaber.cs#L38
        if (GameObject.Find("RecorderCamera(Clone)") != null)
        {
            Plugin.Log.Info("RecorderCamera(Clone) found, we're probably in a ScoreSaber replay");
            return;
        }
        
        if (_pauseController == null)
        {
            _pauseController = Object.FindObjectOfType<PauseController>();
        }

        _pauseController?.Pause();
    }
}