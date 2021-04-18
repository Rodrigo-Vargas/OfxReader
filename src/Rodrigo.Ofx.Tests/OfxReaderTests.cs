using NUnit.Framework;
using Rodrigo.Ofx.Models;

namespace Rodrigo.Ofx.Tests
{
    [TestFixture]
    public class OfxReaderTests
    {
        [Test]
        public void WhenParseMinimumFileMustReturnHeaderAttributesCorrectly()
        {
            var reader = new OfxReader();
            OfxModel ofx = reader.Load(TestData.BASIC);

            Assert.AreEqual("100", ofx.Header);
            Assert.AreEqual("OFXSGML", ofx.Data);
            Assert.AreEqual("102", ofx.Version);
            Assert.AreEqual("NONE", ofx.Security);
            Assert.AreEqual("1252", ofx.Charset);
            Assert.AreEqual("NONE", ofx.Compression);
            Assert.AreEqual("NONE", ofx.OldFileUID);
            Assert.AreEqual("NONE", ofx.NewFileUID);
        }
    }
}
