using FluentAssertions;
using RandomData.Api.Services.StringServices.Enums;
using RandomData.Api.Services.StringServices.ExtensionMethods;
using Xunit;

namespace RandomData.Api.Tests.Services.StringServices
{
    public class StringEncoderExtensionMethodsTests
    {
        [Theory]
        [InlineData(Encoding.Base64, "There can never be too many cherries on an ice cream sundae.",
            "VGhlcmUgY2FuIG5ldmVyIGJlIHRvbyBtYW55IGNoZXJyaWVzIG9uIGFuIGljZSBjcmVhbSBzdW5kYWUu")]
        [InlineData(Encoding.None, "Nobody questions who built the pyramids in Mexico.",
            "Nobody questions who built the pyramids in Mexico.")]
        public void EncodeToExtensionTests(Encoding encoding, string input, string output)
        {
            input.EncodeTo(encoding).Should().Be(output);
        }
        
        [Theory]
        [InlineData("Nobody questions who built the pyramids in Mexico.",
            "Tm9ib2R5IHF1ZXN0aW9ucyB3aG8gYnVpbHQgdGhlIHB5cmFtaWRzIGluIE1leGljby4=")]
        [InlineData("His mind was blown that there was nothing in space except space itself.",
            "SGlzIG1pbmQgd2FzIGJsb3duIHRoYXQgdGhlcmUgd2FzIG5vdGhpbmcgaW4gc3BhY2UgZXhjZXB0IHNwYWNlIGl0c2VsZi4=")]
        [InlineData(
            "The urgent care center was flooded with patients after the news of a new deadly virus was made public.",
            "VGhlIHVyZ2VudCBjYXJlIGNlbnRlciB3YXMgZmxvb2RlZCB3aXRoIHBhdGllbnRzIGFmdGVyIHRoZSBuZXdzIG9mIGEgbmV3IGRlYWRseSB2aXJ1cyB3YXMgbWFkZSBwdWJsaWMu")]
        [InlineData("There can never be too many cherries on an ice cream sundae.",
            "VGhlcmUgY2FuIG5ldmVyIGJlIHRvbyBtYW55IGNoZXJyaWVzIG9uIGFuIGljZSBjcmVhbSBzdW5kYWUu")]
        [InlineData("Courage and stupidity were all he had.", "Q291cmFnZSBhbmQgc3R1cGlkaXR5IHdlcmUgYWxsIGhlIGhhZC4=")]
        [InlineData("She can live her life however she wants as long as she listens to what I have to say.",
            "U2hlIGNhbiBsaXZlIGhlciBsaWZlIGhvd2V2ZXIgc2hlIHdhbnRzIGFzIGxvbmcgYXMgc2hlIGxpc3RlbnMgdG8gd2hhdCBJIGhhdmUgdG8gc2F5Lg==")]
        public void ToBase64ExtensionTests(string input, string output)
        {
            input.ToBase64().Should().Be(output);
        }
    }
}