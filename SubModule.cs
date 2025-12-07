using HarmonyLib;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace MinimumExperienceGain
{
    public class SubModule : MBSubModuleBase
    {
        public static readonly string ModuleName = "Minimum Experience Gain";
        private Harmony _harmony = null;

        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();

            try
            {
                _harmony = new Harmony("minimum_experience_gain");
                _harmony.PatchAll();
                InformationManager.DisplayMessage(new InformationMessage($"{ModuleName}: Harmony patches applied", Colors.Green));
            }
            catch (System.Exception ex)
            {
                InformationManager.DisplayMessage(new InformationMessage($"{ModuleName}: Harmony patching failed - {ex.Message}", Colors.Red));
            }
        }

        protected override void OnSubModuleUnloaded()
        {
            base.OnSubModuleUnloaded();

            try
            {
                if (_harmony != null)
                {
                    _harmony.UnpatchAll("minimum_experience_gain");
                    InformationManager.DisplayMessage(new InformationMessage($"{ModuleName}: Harmony patches removed", Colors.Yellow));
                    _harmony = null;
                }
            }
            catch (System.Exception ex)
            {
                InformationManager.DisplayMessage(new InformationMessage($"{ModuleName}: Unpatch failed - {ex.Message}", Colors.Red));
            }
        }

        protected override void OnBeforeInitialModuleScreenSetAsRoot()
        {
            InformationManager.DisplayMessage(new InformationMessage(string.Format("{0} loaded", ModuleName), Colors.Green));

            ConfigCache.Refresh(Settings.Instance);

            base.OnBeforeInitialModuleScreenSetAsRoot();
        }
    }
}