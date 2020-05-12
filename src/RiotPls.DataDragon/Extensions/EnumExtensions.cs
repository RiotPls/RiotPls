using System;
using System.Runtime.CompilerServices;
using RiotPls.DataDragon.Enums;

namespace RiotPls.DataDragon.Extensions
{
    public static class EnumExtensions
    {
        public static string GetCode(this Language language)
            => language switch
            {
                Language.AmericanEnglish => "en_US",
                Language.Czech => "cs_CZ",
                Language.German => "de_DE",
                Language.Greek => "el_GR",
                Language.AustralianEnglish => "en_AU",
                Language.EnglandEnglish => "en_GB",
                Language.PhilippinesEnglish => "en_PH",
                Language.SingaporeEnglish => "en_SG",
                Language.ArgentinianSpanish => "es_AR",
                Language.SpainSpanish => "es_ES",
                Language.MexicanSpanish => "es_MX",
                Language.French => "fr_FR",
                Language.Hungarian => "hu_HU",
                Language.Indonesian => "id_ID",
                Language.Italian => "it_IT",
                Language.Japanese => "ja_JP",
                Language.Korea => "ko_KR",
                Language.Polish => "pl_PL",
                Language.BrazilianPortuguese => "pt_BR",
                Language.Romanian => "ro_RO",
                Language.Russian => "ru_RU",
                Language.Thai => "th_TH",
                Language.Turkish => "tr_TR",
                Language.Vietnamese => "vn_VN",
                Language.ChinaChinese => "zh_CN",
                Language.MalaysianChinese => "zh_MY",
                Language.TaiwanChinese => "zh_TW",
                _ => throw new InvalidOperationException($"Attempted to write an invalid value. Value: {language}")
            };

        // I just want to ensure the JIT optimizes this so ability.ToLower() 
        // is replaced directly with the respective constant string
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static string ToLower(this ChampionAbility ability)
            => ability switch
            {
                ChampionAbility.Passive => "passive",
                ChampionAbility.Q => "q",
                ChampionAbility.W => "w",
                ChampionAbility.E => "e",
                ChampionAbility.R => "r",
                _ => string.Empty 
            };
    }
}