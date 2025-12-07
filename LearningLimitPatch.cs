using HarmonyLib;
using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.GameComponents;
using TaleWorlds.Core;

namespace MinimumExperienceGain
{
    public static class ConfigCache
    {
        public static float MinLearningRate { get; private set; }
        public static int MaxSkillValue { get; private set; }

        public static void Refresh(Settings settings)
        {
            MinLearningRate = settings.MinLearningRate;
            MaxSkillValue = settings.MaxSkillValue;
        }
    }
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
            if (skillValue >= ConfigCache.MaxSkillValue)
            {
                __result.LimitMin(0f);
            } 
            else
            {
                __result.LimitMin(ConfigCache.MinLearningRate);
            }
        }
    }
}