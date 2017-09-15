using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Xunit;

namespace Kevsoft.Ssml.Tests
{
    public class TimeSpanToTwelveHourTimeConvertorTests
    {
        [Theory]
        [InlineData("00:00:00", "12:00:00AM")]
        [InlineData("00:01:01", "12:01:01AM")]
        [InlineData("12:00:00", "12:00:00PM")]
        [InlineData("12:00:00", "12:00:00PM")]
        [InlineData("12:01:01", "12:01:01PM")]
        [InlineData("23:59:59", "11:59:59PM")]
        public void ShouldReturnCorrectTime(string ts, string expected)
        {
            var timespan = TimeSpan.Parse(ts);

            var convertor = new TimeSpanToTwelveHourTimeConvertor();

            var value = convertor.Convert(timespan);

            value.Should().Be(expected);
        }
    }
}
