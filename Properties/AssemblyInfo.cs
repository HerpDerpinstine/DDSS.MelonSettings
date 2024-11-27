using System.Reflection;
using MelonLoader;

[assembly: AssemblyTitle(DDSS_MelonSettings.Properties.BuildInfo.Description)]
[assembly: AssemblyDescription(DDSS_MelonSettings.Properties.BuildInfo.Description)]
[assembly: AssemblyCompany(DDSS_MelonSettings.Properties.BuildInfo.Company)]
[assembly: AssemblyProduct(DDSS_MelonSettings.Properties.BuildInfo.Name)]
[assembly: AssemblyCopyright("Created by " + DDSS_MelonSettings.Properties.BuildInfo.Author)]
[assembly: AssemblyTrademark(DDSS_MelonSettings.Properties.BuildInfo.Company)]
[assembly: AssemblyVersion(DDSS_MelonSettings.Properties.BuildInfo.Version)]
[assembly: AssemblyFileVersion(DDSS_MelonSettings.Properties.BuildInfo.Version)]
[assembly: MelonInfo(typeof(DDSS_MelonSettings.MelonMain), 
    DDSS_MelonSettings.Properties.BuildInfo.Name, 
    DDSS_MelonSettings.Properties.BuildInfo.Version,
    DDSS_MelonSettings.Properties.BuildInfo.Author,
    DDSS_MelonSettings.Properties.BuildInfo.DownloadLink)]
[assembly: MelonGame("StripedPandaStudios", "DDSS")]
[assembly: VerifyLoaderVersion("0.6.5", true)]
[assembly: HarmonyDontPatchAll]