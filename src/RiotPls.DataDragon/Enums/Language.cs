using System.Text.Json.Serialization;
using RiotPls.DataDragon.Converters;

namespace RiotPls.DataDragon.Enums
{
    [JsonConverter(typeof(LanguageJsonConverter))]
    public enum Language
    {
        /// <summary>
        ///     English language (United States of America). Code: en_US
        /// </summary>
        AmericanEnglish,
        /// <summary>
        ///     Czech language. Code: cs_CZ
        /// </summary>
        Czech,
        /// <summary>
        ///     German language. Code: de_DE
        /// </summary>
        German,
        /// <summary>
        ///     Greek language. Code: el_GR
        /// </summary>
        Greek,
        /// <summary>
        ///     English language (Australia). Code: en_AU
        /// </summary>
        AustralianEnglish,
        /// <summary>
        ///     English language (United Kingdom). Code: en_GB
        /// </summary>
        EnglandEnglish,
        /// <summary>
        ///     English language (Philippines). Code: en_PH
        /// </summary>
        PhilippinesEnglish,
        /// <summary>
        ///     English language (Singapore). Code: en_SG
        /// </summary>
        SingaporeEnglish,
        /// <summary>
        ///     Spanish language (Argentina). Code: es_AR
        /// </summary>
        ArgentinianSpanish,
        /// <summary>
        ///     Spanish language (Spain). Code: es_ES
        /// </summary>
        SpainSpanish,
        /// <summary>
        ///     Spanish language (Mexico). Code: es_MX
        /// </summary>
        MexicanSpanish,
        /// <summary>
        ///     French language. Code: fr_FR
        /// </summary>
        French,
        /// <summary>
        ///     Hungarian language. Code: hu_HU
        /// </summary>
        Hungarian,
        /// <summary>
        ///     Indonesian language. Code: id_ID
        /// </summary>
        Indonesian,
        /// <summary>
        ///     Italian language. Code: it_IT
        /// </summary>
        Italian,
        /// <summary>
        ///     Japanese language. Code: ja_JP
        /// </summary>
        Japanese,
        /// <summary>
        ///     Korean language. Code: ko_KR
        /// </summary>
        Korea,
        /// <summary>
        ///     Polish language. Code: pl_PL
        /// </summary>
        Polish,
        /// <summary>
        ///     Portuguese language (Brazil). Code: pt_BR
        /// </summary>
        BrazilianPortuguese,
        /// <summary>
        ///     Romanian language. Code: ro_RO
        /// </summary>
        Romanian,
        /// <summary>
        ///     Russian language. Code: ru_RU
        /// </summary>
        Russian,
        /// <summary>
        ///     Thai language. Code th_TH
        /// </summary>
        Thai,
        /// <summary>
        ///     Turkish language. Code tr_TR
        /// </summary>
        Turkish,
        /// <summary>
        ///     Vietnamese language. Code vn_VN
        /// </summary>
        Vietnamese,
        /// <summary>
        ///     Chinese language (China). Code zh_CN
        /// </summary>
        ChinaChinese,
        /// <summary>
        ///     Chinese language (Malaysia). Code zh_CN
        /// </summary>
        MalaysianChinese,
        /// <summary>
        ///     Chinese language (Taiwan). Code zh_CN
        /// </summary>
        TaiwanChinese,
    }
}
