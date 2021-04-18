using NUnit.Framework;

namespace Rodrigo.Ofx.Tests
{
    [TestFixture]
    public class FileParserTests
    {
        [Test]
        public void WhenParseMinimumFileMustReturnOfxBodyCorrectly()
        {
            var body = @"OFXHEADER:100
DATA:OFXSGML
<OFX>
<TEST>1</TEST>
</OFX>";

            var result = FileParser.GetOfxBody(body);
            var expected = "<OFX><TEST>1</TEST></OFX>";


            Assert.AreEqual(expected, result);
        }
    }
}
