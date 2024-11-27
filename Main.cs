using DDSS_MelonSettings.Components;
using DDSS_MelonSettings.GUI;
using HarmonyLib;
using MelonLoader;
using Semver;
using System;
using System.Reflection;

namespace DDSS_MelonSettings
{
    internal class MelonMain : MelonMod
    {
        private static bool _isDisabled;
        internal static MelonLogger.Instance _logger;

        public override void OnInitializeMelon()
        {
            // Static Cache Logger
            _logger = LoggerInstance;

            // Check for ModHelper
            MelonMod modHelper = FindModHelper();
            if (modHelper != null)
            {
                MakeModHelperAware(modHelper);

                // Check for Legacy ModHelpers
                if (IsUsingLegacyModHelper(modHelper, out SemVersion modHelperVersion))
                {
                    _logger.Msg($"ModHelper {modHelperVersion.ToString()} includes a Mod Settings GUI already!");
                    _logger.Msg($"Temporarily disabling {Properties.BuildInfo.Name}...");
                    _isDisabled = true;
                    return;
                }
            }

            // Register Custom Types
            ManagedEnumerator.Register();

            // Apply Patches
            ApplyPatches();

            // Log Success
            _logger.Msg("Initialized!");
        }

        public override void OnSceneWasInitialized(int buildIndex, string sceneName)
        {
            if (_isDisabled)
                return;

            if (sceneName == "MainMenuScene") // Main Menu
            {
                ModSettingsManager.MainMenuInit();
            }
            else if (sceneName != "LobbyScene") // In-Game
            {
                ModSettingsManager.GameInit();
            }
        }

        private void ApplyPatches()
        {
            Assembly melonAssembly = typeof(MelonMain).Assembly;
            foreach (Type type in melonAssembly.GetValidTypes())
            {
                // Check Type for any Harmony Attribute
                if (type.GetCustomAttribute<HarmonyPatch>() == null)
                    continue;

                // Apply
                try
                {
                    if (MelonDebug.IsEnabled())
                        LoggerInstance.Msg($"Applying {type.Name}");

                    HarmonyInstance.PatchAll(type);
                }
                catch (Exception e)
                {
                    LoggerInstance.Error($"Exception while attempting to apply {type.Name}: {e}");
                }
            }
        }

        private MelonMod FindModHelper()
        {
            MelonMod modHelper = null;
            foreach (var mod in RegisteredMelons)
                if (mod.Info.Name == "ModHelper")
                {
                    modHelper = mod;
                    break;
                }
            return modHelper;
        }

        private bool IsUsingLegacyModHelper(MelonMod modHelper, out SemVersion ver)
        {
            ver = modHelper.Info.SemanticVersion;
            return ver <= "1.2.0";
        }

        private void MakeModHelperAware(MelonMod modHelper)
        {
            Type modFilterType = modHelper.MelonAssembly.Assembly.GetType("DDSS_ModHelper.Utils.RequirementFilter");
            if (modFilterType == null)
                return;

            MethodInfo method = modFilterType.GetMethod("AddOptionalMelon", BindingFlags.Public | BindingFlags.Static);
            if (method == null)
                return;

            method.Invoke(null, [this]);
        }
    }   
}
