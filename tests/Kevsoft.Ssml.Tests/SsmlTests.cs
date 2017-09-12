using System;
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
    }
}
