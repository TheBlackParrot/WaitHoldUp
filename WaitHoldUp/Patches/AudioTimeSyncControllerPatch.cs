using HarmonyLib;
using UnityEngine;

namespace WaitHoldUp.Patches;

[HarmonyPatch]
internal class AudioTimeSyncControllerPatch
{
    private static PauseController? _pauseController;
    
    [HarmonyPatch(typeof(AudioTimeSyncController), "StartSong")]
    // ReSharper disable once InconsistentNaming
    internal static void Postfix(AudioTimeSyncController __instance)
    {
        if (_pauseController == null)
        {
            _pauseController = Object.FindObjectOfType<PauseController>();
        }

        _pauseController?.Pause();
    }
}