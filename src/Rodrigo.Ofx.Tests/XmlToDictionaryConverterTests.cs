using System.Collections.Generic;
using NUnit.Framework;

namespace Rodrigo.Ofx.Tests
{
    [TestFixture]
    public class XmlToDictionaryConverterTests
    {
        [Test]
        public void ShouldConvertAMinimalXMLCorrectly()
        {
            OfxDictionary<string, object> expected = new OfxDictionary<string, object>()
            {
                { "OFX",  
                    new OfxDictionary<string, object>()
                    {
                        { "TEST", "1" }
                    }
                }
            };

            string fileContent = "<OFX><TEST>1</TEST></OFX>";

            var result = XmlToDictionaryConverter.Convert(fileContent);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ShouldConvertAXmlWithTwoLevelsCorrectly()
        {
            OfxDictionary<string, object> expected = new OfxDictionary<string, object>()
            {
                { "OFX",
                    new OfxDictionary<string, object>()
                    {
                        { "SIGNONMSGSRSV1", 
                            new OfxDictionary<string, object>()
                            {
                                {  "CODE", "1" }
                            }
                        }
                    }
                }
            };

            string fileContent = "<OFX><SIGNONMSGSRSV1><CODE>1</CODE></SIGNONMSGSRSV1></OFX>";

            var result = XmlToDictionaryConverter.Convert(fileContent);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ShouldConvertAXmlWithTwoChildrenCorrectly()
        {
            OfxDictionary<string, object> expected = new OfxDictionary<string, object>()
            {
                { "OFX",
                    new OfxDictionary<string, object>()
                    {
                        { "SIGNONMSGSRSV1",
                            new OfxDictionary<string, object>()
                            {
                                {  "CODE", "1" },
                                {  "LANGUAGE", "PORT" }
                            }
                        }
                    }
                }
            };

            string fileContent = "<OFX><SIGNONMSGSRSV1><CODE>1</CODE><LANGUAGE>PORT</LANGUAGE></SIGNONMSGSRSV1></OFX>";

            var result = XmlToDictionaryConverter.Convert(fileContent);

            Assert.AreEqual(expected, result);
        }
    }
}
