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

            xml.Should().Be(@"<?xml version=""1.0"" encoding=""utf-16""?><speak>Hello World</speak>");
        }

        [Fact]
        public async Task ShouldReturnSpeakWithTextForAlias()
        {
            var xml = await new Ssml().Say("Hello")
                .Say("World")
                .AsAlias("Bob")
                .ToStringAsync();

            xml.Should().Be(@"<?xml version=""1.0"" encoding=""utf-16""?><speak>Hello <sub alias=""Bob"">World</sub></speak>");
        }

        [Fact]
        public async Task ShouldReturnEmphasisedWord()
        {
            var xml = await new Ssml().Say("Hello")
                .Say("World")
                .Emphasised()
                .ToStringAsync();

            xml.Should().Be(@"<?xml version=""1.0"" encoding=""utf-16""?><speak>Hello <emphasis>World</emphasis></speak>");

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

            xml.Should().Be($@"<?xml version=""1.0"" encoding=""utf-16""?><speak>Hello <emphasis level=""{expected}"">World</emphasis></speak>");
        }

        [Fact]
        public async Task ShouldReturnBreak()
        {
            var xml = await new Ssml().Say("Take a deep breath")
                .Break()
                .Say("then continue.")
                .ToStringAsync();

            xml.Should().Be(@"<?xml version=""1.0"" encoding=""utf-16""?><speak>Take a deep breath <break /> then continue.</speak>");
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

            xml.Should().Be($@"<?xml version=""1.0"" encoding=""utf-16""?><speak>Take a deep breath <break strength=""{expected}"" /> then continue.</speak>");
        }

        [Fact]
        public async Task ShouldReturnBreakWithTime()
        {
            var xml = await new Ssml().Say("Take a deep breath")
                .Break().For(TimeSpan.FromSeconds(1.5))
                .Say("then continue.")
                .ToStringAsync();

            xml.Should().Be(@"<?xml version=""1.0"" encoding=""utf-16""?><speak>Take a deep breath <break time=""1500ms"" /> then continue.</speak>");
        }

        [Fact]
        public async Task ShouldReturnBreakWithStrengthAndTime()
        {
            var xml = await new Ssml().Say("Take a deep breath")
                .Break().WithStrength(BreakStrength.ExtraStrong).For(TimeSpan.FromMilliseconds(100.1))
                .Say("then continue.")
                .ToStringAsync();

            xml.Should().Be(@"<?xml version=""1.0"" encoding=""utf-16""?><speak>Take a deep breath <break strength=""x-strong"" time=""100ms"" /> then continue.</speak>");
        }

        [Fact]
        public async Task ShouldReturnSayAsWhenSayingADate()
        {
            var date = new DateTime(2017, 09, 13);
            var xml = await new Ssml()
                .Say("This code was written on")
                .Say(date)
                .ToStringAsync();

            xml.Should().Be($@"<?xml version=""1.0"" encoding=""utf-16""?><speak>This code was written on <say-as interpret-as=""date"">{date:dd/MM/yyy}</say-as></speak>");
        }

        [Theory]
        [InlineData(2017, 09, 15, DateFormat.MonthDayYear, "09-15-2017")]
        [InlineData(2017, 09, 15, DateFormat.DayMonthYear, "15-09-2017")]
        [InlineData(2017, 09, 15, DateFormat.YearMonthDay, "2017-09-15")]
        [InlineData(2017, 09, 15, DateFormat.MonthDay, "09-15")]
        [InlineData(2017, 09, 15, DateFormat.DayMonth, "15-09")]
        [InlineData(2017, 09, 15, DateFormat.YearMonth, "2017-09")]
        [InlineData(2017, 09, 15, DateFormat.MonthYear, "09-2017")]
        [InlineData(2017, 09, 15, DateFormat.Day, "15")]
        [InlineData(2017, 09, 15, DateFormat.Month, "09")]
        [InlineData(2017, 09, 15, DateFormat.Year, "2017")]
        public async Task ShouldReturnSayAsWhenSayingADateWithFormat(int year, int month, int day, DateFormat dateFormat, string expectedDate)
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

            xml.Should().Be($@"<?xml version=""1.0"" encoding=""utf-16""?><speak>This code was written on <say-as interpret-as=""date"" format=""{format}"">{expectedDate}</say-as></speak>");
        }
    }
}
