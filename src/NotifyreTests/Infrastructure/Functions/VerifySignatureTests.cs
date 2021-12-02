using FluentAssertions;
using Notifyre;
using Notifyre.Infrastructure.Functions;
using System;
using Xunit;

namespace NotifyreTests.Infrastructure.Functions
{
    public class VerifySignatureTests
    {
        [Fact]
        public void InvalidTimestampSchemaShouldThrowException()
        {
            // Arrange
            long timestamp = ConvertDateTime.ConvertToUnixTime(DateTime.UtcNow);

            string signatureHeader = $"t1={timestamp},v=7b39f80b8535b7c71ee6850a01567632a323a1ba71365ff11db44010e31d9f89";
            string signingSecret = "ma4l4gtluqltoops28eh61tx4jgyturdwnie";
            string payload = "{\"Event\":\"fax_sent\",\"Timestamp\":1634001867,\"Payload\":{\"ID\":\"cbf16f4b-d6b9-478c-afa2-c9a1d6ca41e1\",\"RecipientID\":\"f4da6acf-9024-4599-ab50-192ca6f6c98a\",\"FromNumber\":\"+61771111111\",\"To\":\"+61422677014\",\"Reference\":\"testing 1644\",\"CreatedDateUtc\":1632617277,\"QueuedDateUtc\":null,\"LastModifiedDateUtc\":null,\"HighQuality\":false,\"Pages\":3,\"Status\":\"completed\"}}";

            // Act
            var exception = Assert.Throws<NotifyreException>(() => VerifySignature.Execute(signatureHeader, payload, signingSecret));

            // Assert
            exception.Message.Should().Be($"Empty signature timestamp");
        }

        [Fact]
        public void MissingTimestampSchemaShouldThrowException()
        {
            // Arrange
            long timestamp = ConvertDateTime.ConvertToUnixTime(DateTime.UtcNow);

            string signatureHeader = $"v=7b39f80b8535b7c71ee6850a01567632a323a1ba71365ff11db44010e31d9f89";
            string signingSecret = "ma4l4gtluqltoops28eh61tx4jgyturdwnie";
            string payload = "{\"Event\":\"fax_sent\",\"Timestamp\":1634001867,\"Payload\":{\"ID\":\"cbf16f4b-d6b9-478c-afa2-c9a1d6ca41e1\",\"RecipientID\":\"f4da6acf-9024-4599-ab50-192ca6f6c98a\",\"FromNumber\":\"+61771111111\",\"To\":\"+61422677014\",\"Reference\":\"testing 1644\",\"CreatedDateUtc\":1632617277,\"QueuedDateUtc\":null,\"LastModifiedDateUtc\":null,\"HighQuality\":false,\"Pages\":3,\"Status\":\"completed\"}}";

            // Act
            var exception = Assert.Throws<NotifyreException>(() => VerifySignature.Execute(signatureHeader, payload, signingSecret));

            // Assert
            exception.Message.Should().Be($"Empty signature timestamp");
        }

        [Fact]
        public void ExpiredTimestampShouldThrowException()
        {
            // Arrange
            long timestamp = ConvertDateTime.ConvertToUnixTime(DateTime.UtcNow.AddDays(-1));

            string signatureHeader = $"t={timestamp},v=7b39f80b8535b7c71ee6850a01567632a323a1ba71365ff11db44010e31d9f89";
            string signingSecret = "ma4l4gtluqltoops28eh61tx4jgyturdwnie";
            string payload = "{\"Event\":\"fax_sent\",\"Timestamp\":1634001867,\"Payload\":{\"ID\":\"cbf16f4b-d6b9-478c-afa2-c9a1d6ca41e1\",\"RecipientID\":\"f4da6acf-9024-4599-ab50-192ca6f6c98a\",\"FromNumber\":\"+61771111111\",\"To\":\"+61422677014\",\"Reference\":\"testing 1644\",\"CreatedDateUtc\":1632617277,\"QueuedDateUtc\":null,\"LastModifiedDateUtc\":null,\"HighQuality\":false,\"Pages\":3,\"Status\":\"completed\"}}";

            // Act
            var exception = Assert.Throws<NotifyreException>(() => VerifySignature.Execute(signatureHeader, payload, signingSecret));

            // Assert
            exception.Message.Should().Be($"Signature timestamp expired");
        }

        [Fact]
        public void InvalidSignatureSchemaShouldThrowException()
        {
            // Arrange
            long timestamp = ConvertDateTime.ConvertToUnixTime(DateTime.UtcNow);

            string signatureHeader = $"t={timestamp},v2=7b39f80b8535b7c71ee6850a01567632a323a1ba71365ff11db44010e31d9f89";
            string signingSecret = "ma4l4gtluqltoops28eh61tx4jgyturdwnie";
            string payload = "{\"Event\":\"fax_sent\",\"Timestamp\":1634001867,\"Payload\":{\"ID\":\"cbf16f4b-d6b9-478c-afa2-c9a1d6ca41e1\",\"RecipientID\":\"f4da6acf-9024-4599-ab50-192ca6f6c98a\",\"FromNumber\":\"+61771111111\",\"To\":\"+61422677014\",\"Reference\":\"testing 1644\",\"CreatedDateUtc\":1632617277,\"QueuedDateUtc\":null,\"LastModifiedDateUtc\":null,\"HighQuality\":false,\"Pages\":3,\"Status\":\"completed\"}}";

            // Act
            var exception = Assert.Throws<NotifyreException>(() => VerifySignature.Execute(signatureHeader, payload, signingSecret));

            // Assert
            exception.Message.Should().Be($"Empty signature");
        }

        [Fact]
        public void MissingSignatureSchemaShouldThrowException()
        {
            // Arrange
            long timestamp = ConvertDateTime.ConvertToUnixTime(DateTime.UtcNow);

            string signatureHeader = $"t={timestamp}";
            string signingSecret = "ma4l4gtluqltoops28eh61tx4jgyturdwnie";
            string payload = "{\"Event\":\"fax_sent\",\"Timestamp\":1634001867,\"Payload\":{\"ID\":\"cbf16f4b-d6b9-478c-afa2-c9a1d6ca41e1\",\"RecipientID\":\"f4da6acf-9024-4599-ab50-192ca6f6c98a\",\"FromNumber\":\"+61771111111\",\"To\":\"+61422677014\",\"Reference\":\"testing 1644\",\"CreatedDateUtc\":1632617277,\"QueuedDateUtc\":null,\"LastModifiedDateUtc\":null,\"HighQuality\":false,\"Pages\":3,\"Status\":\"completed\"}}";

            // Act
            var exception = Assert.Throws<NotifyreException>(() => VerifySignature.Execute(signatureHeader, payload, signingSecret));

            // Assert
            exception.Message.Should().Be($"Empty signature");
        }

        [Fact]
        public void NonMatchingSignatureShouldThrowException()
        {
            // Arrange
            long timestamp = ConvertDateTime.ConvertToUnixTime(DateTime.UtcNow);

            string signatureHeader = $"t={timestamp},v=7b39f80b8535b7c71ee6850a01567632a323a1ba71365ff11db44010e31d9f89";
            string signingSecret = "ma4l4gtluqltoops28eh61tx4jgyturdwnie";
            string payload = "{\"Event\":\"fax_sent\",\"Timestamp\":1634001867,\"Payload\":{\"ID\":\"cbf16f4b-d6b9-478c-afa2-c9a1d6ca41e1\",\"RecipientID\":\"f4da6acf-9024-4599-ab50-192ca6f6c98a\",\"FromNumber\":\"+61771111111\",\"To\":\"+61422677014\",\"Reference\":\"testing 1644\",\"CreatedDateUtc\":1632617277,\"QueuedDateUtc\":null,\"LastModifiedDateUtc\":null,\"HighQuality\":false,\"Pages\":3,\"Status\":\"completed\"}}";

            // Act
            var exception = Assert.Throws<NotifyreException>(() => VerifySignature.Execute(signatureHeader, payload, signingSecret));

            // Assert
            exception.Message.Should().Be($"Invalid signature");
        }

        [Fact]
        public void MatchingSignatureShouldReturnVerified()
        {
            // Arrange
            string signatureHeader = $"t=1634001867,v=30894d12c909722c603c3e301e0772ee20557d5359f39326ba702c817ca4703e";
            string signingSecret = "ma4l4gtluqltoops28eh61tx4jgyturdwnie";
            string payload = "{\"Event\":\"fax_sent\",\"Timestamp\":1634001867,\"Payload\":{\"ID\":\"cbf16f4b-d6b9-478c-afa2-c9a1d6ca41e1\",\"RecipientID\":\"f4da6acf-9024-4599-ab50-192ca6f6c98a\",\"FromNumber\":\"+61771111111\",\"To\":\"+61422677014\",\"Reference\":\"testing 1644\",\"CreatedDateUtc\":1632617277,\"QueuedDateUtc\":null,\"LastModifiedDateUtc\":null,\"HighQuality\":false,\"Pages\":3,\"Status\":\"completed\"}}";

            // Act
            var result = VerifySignature.Execute(signatureHeader, payload, signingSecret, long.MaxValue);

            // Assert
            result.Should().Be(true);
        }
    }
}
