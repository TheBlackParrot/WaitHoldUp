using System.Reflection;
using HarmonyLib;
using IPA;
using JetBrains.Annotations;
using SiraUtil.Zenject;
using IPALogger = IPA.Logging.Logger;

namespace WaitHoldUp;

[Plugin(RuntimeOptions.DynamicInit), NoEnableDisable]
[UsedImplicitly]
internal class Plugin
{
    private static Harmony _harmony = null!;
    internal static IPALogger Log { get; set; } = null!;

    [Init]
    public Plugin(IPALogger ipaLogger, Zenjector zenjector)
    {
        Log = ipaLogger;
        zenjector.UseLogger(Log);
        
        Log.Info("Plugin loaded");
    }

    [OnEnable]
    public void OnEnable()
    {
        _harmony = new Harmony("TheBlackParrot.WaitHoldUp");
        _harmony.PatchAll(Assembly.GetExecutingAssembly());
        
        Log.Info("Patches applied");
    }
    
    [OnDisable]
    public void OnDisable()
    {
        _harmony.UnpatchSelf();
    }
}