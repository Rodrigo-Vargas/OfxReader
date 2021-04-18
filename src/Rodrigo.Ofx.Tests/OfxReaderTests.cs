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

        [Test]
        public void WhenParseMinimumFileMustReturnSignOnMessageAttributesCorrectly()
        {
            var reader = new OfxReader();
            OfxModel ofx = reader.Load(TestData.BASIC);

            Assert.AreEqual("0", ofx.Ofx.SignOnMessage.Status.Code);
            Assert.AreEqual("INFO", ofx.Ofx.SignOnMessage.Status.Severity);
            Assert.AreEqual("20210417", ofx.Ofx.SignOnMessage.DateServer);
            Assert.AreEqual("POR", ofx.Ofx.SignOnMessage.Language);

            Assert.AreEqual("Banco Inter S/A", ofx.Ofx.SignOnMessage.Fi.Organization);
            Assert.AreEqual("077", ofx.Ofx.SignOnMessage.Fi.Fid);
        }

        [Test]
        public void WhenParseMinimumFileMustReturnBankMessageAttributesCorrectly()
        {
            var reader = new OfxReader();
            OfxModel ofx = reader.Load(TestData.BASIC);

            Assert.AreEqual("0", ofx.Ofx.SignOnMessage.Status.Code);
        }
    }
}
