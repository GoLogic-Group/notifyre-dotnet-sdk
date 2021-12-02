using FluentAssertions;
using Notifyre.Infrastructure.Annotations;
using Notifyre.Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xunit;

namespace NotifyreTests.Infrastructure.Utils
{
    public class JsonUtilTests
    {
        [Fact]
        public async Task CreateBody_CheckDatatypes()
        {
            // Arrange
            string stringType = "xyz";
            double doubleType = 1;
            int intType = 1;
            DateTime dateTimeType = DateTime.MinValue;
            DateTime dateFormattedTimeType = new DateTime(2021, 07, 01, 12, 43, 52);
            DateTime? dateTimeNullableType = null;
            Guid guidType = Guid.Empty;
            Guid? guidNullableType = null;
            var request = new BodyFake()
            {
                StringType = stringType,
                DoubleType = doubleType,
                IntType = intType,
                DateTimeType = dateTimeType,
                DateFormattedTimeType = dateFormattedTimeType,
                DateTimeNullableType = dateTimeNullableType,
                GuidType = guidType,
                GuidNullableType = guidNullableType,
                IgnoreGuidType = Guid.NewGuid(),
                ListGuidType = new List<Guid>() { guidType },
                EnumFakeType = EnumFake.Val1,
                LiteralEnumFakeType = EnumFake.Val1
            };

            // Act
            var body = JsonUtil.CreateBody(request);
            var bodyStr = await body.ReadAsStringAsync();
            var normalised = new Regex(@"\s+").Replace(bodyStr, "")
                .Replace("\r", "")
                .Replace("\n", "");

            // Assert
            string expected = "{\"StringType\":\"xyz\",\"DoubleType\":1.0,\"IntType\":1,\"DateTimeType\":\"0001-01-01T00:00:00\",\"DateFormattedTimeType\":\"2021-07-0112:43:52\",\"DateTimeNullableType\":null,\"GuidType\":\"00000000-0000-0000-0000-000000000000\",\"GuidNullableType\":null,\"ListGuidType\":[\"00000000-0000-0000-0000-000000000000\"],\"NullListGuidType\":null,\"EnumFakeType\":\"val_1\",\"LiteralEnumFakeType\":\"Val1\"}";

            normalised.Should().Be(expected);
        }

        private class BodyFake
        {
            [BodyParam]
            public string StringType { get; set; }
            [BodyParam]
            public double DoubleType { get; set; }
            [BodyParam]
            public int IntType { get; set; }
            [BodyParam]
            public DateTime DateTimeType { get; set; }
            [BodyParam]
            [DateTimeFormat("yyyy-MM-dd HH:mm:ss")]
            public DateTime DateFormattedTimeType { get; set; }
            [BodyParam]
            public DateTime? DateTimeNullableType { get; set; }
            [BodyParam]
            public Guid GuidType { get; set; }
            [BodyParam]
            public Guid? GuidNullableType { get; set; }
            [QueryParam]
            public Guid IgnoreGuidType { get; set; }
            [BodyParam]
            public List<Guid> ListGuidType { get; set; }
            [BodyParam]
            public List<Guid> NullListGuidType { get; set; }
            [BodyParam(typeof(ConstantFake))]
            public EnumFake EnumFakeType { get; set; }
            [BodyParam()]
            public EnumFake LiteralEnumFakeType { get; set; }
        }

        private enum EnumFake
        {
            Val1,
            Val2
        }

        public static class ConstantFake
        {
            public const string Val2 = "val2";
            public const string Val1 = "val_1";
        }
    }
}
