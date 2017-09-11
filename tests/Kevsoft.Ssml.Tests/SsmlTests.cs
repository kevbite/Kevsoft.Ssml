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
            var ssml = new Ssml();
            var xml = await ssml.Say("Hello")
                .Say("World")
                .ToStringAsync();

            xml.Should().Be(@"<?xml version=""1.0"" encoding=""utf-16""?><speak>Hello World</speak>");
        }

        [Fact]
        public async Task ShouldReturnSpeakWithTextForAlias()
        {
            var ssml = new Ssml();
            var xml = await ssml.Say("Hello")
                .Say("World")
                .AsAlias("Bob")
                .ToStringAsync();

            xml.Should().Be(@"<?xml version=""1.0"" encoding=""utf-16""?><speak>Hello <sub alias=""Bob"">World</sub></speak>");
        }

        [Fact]
        public async Task ShouldReturnEmphasisedWord()
        {
            var ssml = new Ssml();
            var xml = await ssml.Say("Hello")
                .Say("World")
                .Emphasised()
                .ToStringAsync();

            xml.Should().Be(@"<?xml version=""1.0"" encoding=""utf-16""?><speak>Hello <emphasis>World</emphasis></speak>");

        }
    }
}
