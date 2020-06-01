using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using RiotPls.DataDragon.Enums;
using RiotPls.DataDragon.Extensions;

namespace RiotPls.DataDragon.Converters
{
    internal sealed class LanguageJsonConverter : JsonConverter<Language>
    {
        public static LanguageJsonConverter Instance { get; } = new LanguageJsonConverter();

        public override Language Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var language = reader.GetString();
            return language switch
            {
                "en_US" => Language.AmericanEnglish,
                "cs_CZ" => Language.Czech,
                "de_DE" => Language.German,
                "el_GR" => Language.Greek,
                "en_AU" => Language.AustralianEnglish,
                "en_GB" => Language.EnglandEnglish,
                "en_PH" => Language.PhilippinesEnglish,
                "en_SG" => Language.SingaporeEnglish,
                "es_AR" => Language.ArgentinianSpanish,
                "es_ES" => Language.SpainSpanish,
                "es_MX" => Language.MexicanSpanish,
                "fr_FR" => Language.French,
                "hu_HU" => Language.Hungarian,
                "id_ID" => Language.Indonesian,
                "it_IT" => Language.Italian,
                "ja_JP" => Language.Japanese,
                "ko_KR" => Language.Korea,
                "pl_PL" => Language.Polish,
                "pt_BR" => Language.BrazilianPortuguese,
                "ro_RO" => Language.Romanian,
                "ru_RU" => Language.Russian,
                "th_TH" => Language.Thai,
                "tr_TR" => Language.Turkish,
                "vn_VN" => Language.Vietnamese,
                "zh_CN" => Language.ChinaChinese,
                "zh_MY" => Language.MalaysianChinese,
                "zh_TW" => Language.TaiwanChinese,
                _ => throw new NotImplementedException($"The language \"{language}\" has not been implemented")
            };
        }

        public override void Write(Utf8JsonWriter writer, Language value, JsonSerializerOptions options)
            => writer.WriteStringValue(value.GetCode());
    }
}