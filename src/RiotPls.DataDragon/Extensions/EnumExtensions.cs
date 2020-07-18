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

        public static Language GetLanguage(this string code)
        {
            return code.ToLower() switch
            {
                "en_us" => Language.AmericanEnglish,
                "cs_cz" => Language.Czech,
                "de_de" => Language.German,
                "el_gr" => Language.Greek,
                "en_au" => Language.AustralianEnglish,
                "en_gb" => Language.EnglandEnglish,
                "en_ph" => Language.PhilippinesEnglish,
                "en_sg" => Language.SingaporeEnglish,
                "es_ar" => Language.ArgentinianSpanish,
                "es_es" => Language.SpainSpanish,
                "es_mx" => Language.MexicanSpanish,
                "fr_fr" => Language.French,
                "hu_hu" => Language.Hungarian,
                "id_id" => Language.Indonesian,
                "it_it" => Language.Italian,
                "ja_jp" => Language.Japanese,
                "ko_kr" => Language.Korea,
                "pl_pl" => Language.Polish,
                "pt_br" => Language.BrazilianPortuguese,
                "ro_ro" => Language.Romanian,
                "ru_ru" => Language.Russian,
                "th_th" => Language.Thai,
                "tr_tr" => Language.Turkish,
                "vn_vn" => Language.Vietnamese,
                "zh_cn" => Language.ChinaChinese,
                "zh_my" => Language.MalaysianChinese,
                "zh_tw" => Language.TaiwanChinese,
                _ => Language.AmericanEnglish
            };
        }

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