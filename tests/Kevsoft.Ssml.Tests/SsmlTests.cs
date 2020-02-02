using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Kevsoft.Ssml.Tests
{
    public class SsmlTests
    {
        [Fact]
        public async Task ShouldReturnSpeakWithText()
        {
            var xml = await new Ssml().Say("Hello")
                .Say("World")
                .ToStringAsync();

            xml.Should().Be(@"<?xml version=""1.0"" encoding=""utf-16""?><speak version=""1.0"" xml:lang=""en-US"" xmlns=""http://www.w3.org/2001/10/synthesis"">Hello World</speak>");
        }

        [Fact]
        public async Task ShouldReturnSpeakWithTextForAlias()
        {
            var xml = await new Ssml().Say("Hello")
                .Say("World")
                .AsAlias("Bob")
                .ToStringAsync();

            xml.Should().Be(@"<?xml version=""1.0"" encoding=""utf-16""?><speak version=""1.0"" xml:lang=""en-US"" xmlns=""http://www.w3.org/2001/10/synthesis"">Hello <sub alias=""Bob"">World</sub></speak>");
        }

        [Fact]
        public async Task ShouldReturnSpeakWithTextForVoice()
        {
            var xml = await new Ssml().Say("Hello")
                .Say("World")
                .AsVoice("en-US-Jessa24kRUS")
                .ToStringAsync();

            xml.Should().Be(@"<?xml version=""1.0"" encoding=""utf-16""?><speak version=""1.0"" xml:lang=""en-US"" xmlns=""http://www.w3.org/2001/10/synthesis"">Hello <voice name=""en-US-Jessa24kRUS"">World</voice></speak>");
        }

        [Fact]
        public async Task ShouldReturnEmphasisedSpeakWithTextForVoice()
        {
            var xml = await new Ssml().Say("Hello")
                .Say("World")
                .Emphasised()
                .AsVoice("en-US-Jessa24kRUS")
                .ToStringAsync();

            xml.Should().Be(@"<?xml version=""1.0"" encoding=""utf-16""?><speak version=""1.0"" xml:lang=""en-US"" xmlns=""http://www.w3.org/2001/10/synthesis"">Hello <voice name=""en-US-Jessa24kRUS""><emphasis>World</emphasis></voice></speak>");
        }

        [Fact]
        public async Task ShouldReturnChineseLanguageSsml()
        {
            var xml = await new Ssml(lang: "zh-CN").Say("这样做吗")
                                             .Break().WithStrength(BreakStrength.ExtraStrong).For(TimeSpan.FromMilliseconds(100.1))
                                             .Say("是的，它确实！")
                                             .ToStringAsync();

            xml.Should().Be(@"<?xml version=""1.0"" encoding=""utf-16""?><speak version=""1.0"" xml:lang=""zh-CN"" xmlns=""http://www.w3.org/2001/10/synthesis"">这样做吗 <break strength=""x-strong"" time=""100ms"" /> 是的，它确实！</speak>");
        }

        [Fact]
        public async Task ShouldReturnEmphasisedWord()
        {
            var xml = await new Ssml().Say("Hello")
                .Say("World")
                .Emphasised()
                .ToStringAsync();

            xml.Should().Be(@"<?xml version=""1.0"" encoding=""utf-16""?><speak version=""1.0"" xml:lang=""en-US"" xmlns=""http://www.w3.org/2001/10/synthesis"">Hello <emphasis>World</emphasis></speak>");

        }

        [Theory]
        [InlineData(EmphasiseLevel.Strong, "strong")]
        [InlineData(EmphasiseLevel.Moderate, "moderate")]
        [InlineData(EmphasiseLevel.None, "none")]
        [InlineData(EmphasiseLevel.Reduced, "reduced")]
        public async Task ShouldReturnEmphasisedWordWithLevel(EmphasiseLevel level, string expected)
        {
            var xml = await new Ssml().Say("Hello")
                .Say("World")
                .Emphasised(level)
                .ToStringAsync();

            xml.Should().Be($@"<?xml version=""1.0"" encoding=""utf-16""?><speak version=""1.0"" xml:lang=""en-US"" xmlns=""http://www.w3.org/2001/10/synthesis"">Hello <emphasis level=""{expected}"">World</emphasis></speak>");
        }

        [Fact]
        public async Task ShouldReturnBreak()
        {
            var xml = await new Ssml().Say("Take a deep breath")
                .Break()
                .Say("then continue.")
                .ToStringAsync();

            xml.Should().Be(@"<?xml version=""1.0"" encoding=""utf-16""?><speak version=""1.0"" xml:lang=""en-US"" xmlns=""http://www.w3.org/2001/10/synthesis"">Take a deep breath <break /> then continue.</speak>");
        }

        [Theory]
        [InlineData(BreakStrength.None, "none")]
        [InlineData(BreakStrength.ExtraWeak, "x-weak")]
        [InlineData(BreakStrength.Weak, "weak")]
        [InlineData(BreakStrength.Medium, "medium")]
        [InlineData(BreakStrength.Strong, "strong")]
        [InlineData(BreakStrength.ExtraStrong, "x-strong")]
        public async Task ShouldReturnBreakWithStrength(BreakStrength strength, string expected)
        {
            var xml = await new Ssml().Say("Take a deep breath")
                .Break().WithStrength(strength)
                .Say("then continue.")
                .ToStringAsync();

            xml.Should().Be($@"<?xml version=""1.0"" encoding=""utf-16""?><speak version=""1.0"" xml:lang=""en-US"" xmlns=""http://www.w3.org/2001/10/synthesis"">Take a deep breath <break strength=""{expected}"" /> then continue.</speak>");
        }

        [Fact]
        public async Task ShouldReturnBreakWithTime()
        {
            var xml = await new Ssml().Say("Take a deep breath")
                .Break().For(TimeSpan.FromSeconds(1.5))
                .Say("then continue.")
                .ToStringAsync();

            xml.Should().Be(@"<?xml version=""1.0"" encoding=""utf-16""?><speak version=""1.0"" xml:lang=""en-US"" xmlns=""http://www.w3.org/2001/10/synthesis"">Take a deep breath <break time=""1500ms"" /> then continue.</speak>");
        }

        [Fact]
        public async Task ShouldReturnBreakWithStrengthAndTime()
        {
            var xml = await new Ssml().Say("Take a deep breath")
                .Break().WithStrength(BreakStrength.ExtraStrong).For(TimeSpan.FromMilliseconds(100.1))
                .Say("then continue.")
                .ToStringAsync();

            xml.Should().Be(@"<?xml version=""1.0"" encoding=""utf-16""?><speak version=""1.0"" xml:lang=""en-US"" xmlns=""http://www.w3.org/2001/10/synthesis"">Take a deep breath <break strength=""x-strong"" time=""100ms"" /> then continue.</speak>");        }

        [Fact]
        public async Task ShouldReturnSayAsWhenSayingADate()
        {
            var date = new DateTime(2017, 09, 13);
            var xml = await new Ssml()
                .Say("This code was written on")
                .Say(date)
                .ToStringAsync();

            xml.Should()
                .Be(
                    $@"<?xml version=""1.0"" encoding=""utf-16""?><speak version=""1.0"" xml:lang=""en-US"" xmlns=""http://www.w3.org/2001/10/synthesis"">This code was written on <say-as interpret-as=""date"">{
                            date
                        :yyyyMMdd}</say-as></speak>");
        }

        [Theory]
        [InlineData(2017, 09, 15, DateFormat.MonthDayYear, "09152017")]
        [InlineData(2017, 09, 15, DateFormat.DayMonthYear, "15092017")]
        [InlineData(2017, 09, 15, DateFormat.YearMonthDay, "20170915")]
        [InlineData(2017, 09, 15, DateFormat.MonthDay, "0915")]
        [InlineData(2017, 09, 15, DateFormat.DayMonth, "1509")]
        [InlineData(2017, 09, 15, DateFormat.YearMonth, "201709")]
        [InlineData(2017, 09, 15, DateFormat.MonthYear, "092017")]
        [InlineData(2017, 09, 15, DateFormat.Day, "15")]
        [InlineData(2017, 09, 15, DateFormat.Month, "09")]
        [InlineData(2017, 09, 15, DateFormat.Year, "2017")]
        public async Task ShouldReturnSayAsWhenSayingADateWithFormat(int year, int month, int day,
            DateFormat dateFormat, string expectedDate)
        {
            var formatMaps = new Dictionary<DateFormat, string>
            {
                {DateFormat.MonthDayYear, "mdy"},
                {DateFormat.DayMonthYear, "dmy"},
                {DateFormat.YearMonthDay, "ymd"},
                {DateFormat.MonthDay, "md"},
                {DateFormat.DayMonth, "dm"},
                {DateFormat.YearMonth, "ym"},
                {DateFormat.MonthYear, "my"},
                {DateFormat.Day, "d"},
                {DateFormat.Month, "m"},
                {DateFormat.Year, "y"}
            };

            var date = new DateTime(year, month, day);
            var format = formatMaps[dateFormat];

            var xml = await new Ssml()
                .Say("This code was written on")
                .Say(date).As(dateFormat)
                .ToStringAsync();

            xml.Should()
                .Be(
                    $@"<?xml version=""1.0"" encoding=""utf-16""?><speak version=""1.0"" xml:lang=""en-US"" xmlns=""http://www.w3.org/2001/10/synthesis"">This code was written on <say-as interpret-as=""date"" format=""{
                            format
                        }"">{expectedDate}</say-as></speak>");
        }

        [Fact]
        public void ShouldThrowExceptionIfTimeSpanIsNegative()
        {
            var time = new TimeSpan(-1);

            Assert.Throws<ArgumentException>("value", () => new Ssml()
                .Say(time));
        }

        [Fact]
        public void ShouldThrowExceptionIfTimeSpanIsOver()
        {
            var time = new TimeSpan(24, 0, 0);

            Assert.Throws<ArgumentException>("value", () => new Ssml()
                .Say(time));
        }

        [Fact]
        public async Task ShouldReturnSayAsWhenSayingATimeIn24Hours()
        {
            var time = new TimeSpan(20, 05, 33);

            var xml = await new Ssml().Say("Bedtime is")
                .Say(time).In(TimeFormat.TwentyFourHour)
                .ToStringAsync();

            xml.Should()
                .Be(
                    @"<?xml version=""1.0"" encoding=""utf-16""?><speak version=""1.0"" xml:lang=""en-US"" xmlns=""http://www.w3.org/2001/10/synthesis"">Bedtime is <say-as interpret-as=""time"" format=""hms24"">20:05:33</say-as></speak>");
        }


        [Fact]
        public async Task ShouldReturnSayAsWhenSayingATimeIn12Hours()
        {
            var time = new TimeSpan(20, 05, 33);

            var xml = await new Ssml().Say("Bedtime is")
                .Say(time).In(TimeFormat.TwelveHour)
                .ToStringAsync();

            xml.Should()
                .Be(
                    @"<?xml version=""1.0"" encoding=""utf-16""?><speak version=""1.0"" xml:lang=""en-US"" xmlns=""http://www.w3.org/2001/10/synthesis"">Bedtime is <say-as interpret-as=""time"" format=""hms12"">08:05:33PM</say-as></speak>");
        }

        [Fact]
        public async Task ShouldReturnSayAsWhenSayingTextAsTelephone()
        {
            var xml = await new Ssml()
                .Say("If you require a new job, please phone")
                .Say("+44 (0)114 273 0281").AsTelephone()
                .ToStringAsync();

            xml.Should()
                .Be(
                    @"<?xml version=""1.0"" encoding=""utf-16""?><speak version=""1.0"" xml:lang=""en-US"" xmlns=""http://www.w3.org/2001/10/synthesis"">If you require a new job, please phone <say-as interpret-as=""telephone"">+44 (0)114 273 0281</say-as></speak>");
        }

        [Fact]
        public async Task ShouldReturnSayAsWhenSayingTextAsCharacters()
        {
            var xml = await new Ssml()
                .Say("It's as easy as")
                .Say("abc").AsCharacters()
                .ToStringAsync();

            xml.Should()
                .Be(
                    @"<?xml version=""1.0"" encoding=""utf-16""?><speak version=""1.0"" xml:lang=""en-US"" xmlns=""http://www.w3.org/2001/10/synthesis"">It's as easy as <say-as interpret-as=""characters"" format=""characters"">abc</say-as></speak>");
        }

        [Fact]
        public async Task ShouldReturnSayAsWhenSayingTextAsCharactersWithGlyphInformation()
        {
            var xml = await new Ssml()
                .Say("It's as easy as")
                .Say("abc").AsCharacters().WithGlyphInformation()
                .ToStringAsync();

            xml.Should()
                .Be(
                    @"<?xml version=""1.0"" encoding=""utf-16""?><speak version=""1.0"" xml:lang=""en-US"" xmlns=""http://www.w3.org/2001/10/synthesis"">It's as easy as <say-as interpret-as=""characters"" format=""glyph"">abc</say-as></speak>");
        }

        [Fact]
        public async Task ShouldReturnSayAsWhenSayingIntAsCardinalNumber()
        {
            var xml = await new Ssml()
                .Say("We only have")
                .Say(512).AsCardinalNumber()
                .ToStringAsync();

            xml.Should()
                .Be(
                    @"<?xml version=""1.0"" encoding=""utf-16""?><speak version=""1.0"" xml:lang=""en-US"" xmlns=""http://www.w3.org/2001/10/synthesis"">We only have <say-as interpret-as=""cardinal"">512</say-as></speak>");
        }

        [Fact]
        public async Task ShouldReturnSayAsWhenSayingIntAsOrdinalNumber()
        {
            var xml = await new Ssml()
                .Say("We only have")
                .Say(512).AsOrdinalNumber()
                .ToStringAsync();

            xml.Should()
                .Be(
                    @"<?xml version=""1.0"" encoding=""utf-16""?><speak version=""1.0"" xml:lang=""en-US"" xmlns=""http://www.w3.org/2001/10/synthesis"">We only have <say-as interpret-as=""ordinal"">512</say-as></speak>");
        }

        [Fact]
        public async Task ShouldNotIncludeVersion()
        {
            var xml = await new Ssml()
                .WithConfiguration(new SsmlConfiguration(true))
                .Say("Hello")
                .Say("World")
                .ToStringAsync();

            xml.Should().Be(@"<?xml version=""1.0"" encoding=""utf-16""?><speak xml:lang=""en-US"" xmlns=""http://www.w3.org/2001/10/synthesis"">Hello World</speak>");
        }
    }
}