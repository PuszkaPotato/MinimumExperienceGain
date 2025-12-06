using HarmonyLib;
using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.GameComponents;
using TaleWorlds.Core;
using TaleWorlds.Localization;

namespace MinimumExperienceGain
{
    [HarmonyPatch(typeof(DefaultCharacterDevelopmentModel), "CalculateLearningRate")]
    [HarmonyPatch(new Type[] {
        typeof(IReadOnlyPropertyOwner<CharacterAttribute>),
        typeof(int),     // focusValue
        typeof(int),     // skillValue
        typeof(SkillObject),
        typeof(bool)     // includeDescriptions
    })]
    public class LearningLimitPatch
    {
        static void Postfix(ref ExplainedNumber __result, int skillValue)
        {
            if (skillValue >= Settings.Instance.MaxSkillValue)
            {
                __result.LimitMin(0f);
            } 
            else
            {
                __result.LimitMin(Settings.Instance.MinLearningRate);
            }
        }
    }
}