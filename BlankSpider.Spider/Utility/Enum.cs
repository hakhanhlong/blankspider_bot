using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlankSpider.Spider.Utility
{
    public enum ConfigurationType
    {
        Id = 0,
        Attribute,
        Xpath,
        Style,
        Regex,
        Code,
    }

    public enum PatternType
    {
        XPATH,
        STRING_BETWEEN
    }

    public enum LinkType
    {
        HomeLink = 0,
        SubLink,
        DetailLink,
    }

    public enum EncodingType
    {
        ASCII = 0,
        Default,
        Unicode,
        UTF32,
        UTF7,
        UTF8,
    }

    public enum GetContentType
    {
        Text = 0,
        Html,
        Attribute,
        FixValue,
        TextParagraph,
    }

    public enum CodeType
    {
        UseTree = 0,
        UseText,
    }

    public enum CategoryType
    {
        Field = 0,
        Link,
        ParentLink,
    }

    public enum ConfigLinkType
    {
        Fix = 0,
        Regex,
        Code,
        Build,
    }

    public enum ConfigExtendType
    {
        ConfigFixVale = 1,
        ConfigExtendCode,
    }

    public enum FixValueType
    {
        Instore = 0,
        Outstore,
        New,
        Used,
    }

    public enum LimitTimeType
    {
        NoLimit = 0,
        In24h,
        Inday,
        Inweek,
        InMonth,
        InYear,
    }

    public enum ProjectStatus
    {
        Test = 0,
        Run,
        Error,
    }

    public enum ReturnType
    {
        Nothing = 0,
        BuildImage,
        BuildAbslute,
        ConvertToInt,
        RemoveStopWord,
        DateTime,
    }

    public enum StatusLinkType
    {
        New = 0,
        Old,
        Error,
    }

    public enum LogType
    {
        ErrorDownload = 0,
        ErrorParser,
        ErrorUpdate,
        WarrningParser,
        WarrningConfig,
    }
}
