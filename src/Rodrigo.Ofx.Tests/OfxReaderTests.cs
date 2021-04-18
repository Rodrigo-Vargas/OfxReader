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

            Assert.AreEqual("Banco XXX", ofx.Ofx.SignOnMessage.Fi.Organization);
            Assert.AreEqual("123", ofx.Ofx.SignOnMessage.Fi.Fid);
        }

        [Test]
        public void WhenParseMinimumFileMustReturnBankMessageAttributesCorrectly()
        {
            var reader = new OfxReader();
            OfxModel ofx = reader.Load(TestData.BASIC);

            Assert.AreEqual("1001", ofx.Ofx.BankMessages.Stmttrns.Trnuid);
            Assert.AreEqual("0", ofx.Ofx.BankMessages.Stmttrns.Status.Code);
            Assert.AreEqual("INFO", ofx.Ofx.BankMessages.Stmttrns.Status.Severity);
            Assert.AreEqual("BRL", ofx.Ofx.BankMessages.Stmttrns.CURDEF);
            Assert.AreEqual("077", ofx.Ofx.BankMessages.Stmttrns.BANKACCTFROM.Id);
            Assert.AreEqual("XXXX-X", ofx.Ofx.BankMessages.Stmttrns.BANKACCTFROM.BranchID);
            Assert.AreEqual("12345678", ofx.Ofx.BankMessages.Stmttrns.BANKACCTFROM.AccountID);
            Assert.AreEqual("CHECKING", ofx.Ofx.BankMessages.Stmttrns.BANKACCTFROM.AccountType);
            Assert.AreEqual("20210301", ofx.Ofx.BankMessages.Stmttrns.BANKACCTFROM.BANKTRANLIST.DTSTART);
            Assert.AreEqual("20210331", ofx.Ofx.BankMessages.Stmttrns.BANKACCTFROM.BANKTRANLIST.DTEND);
        }


        [Test]
        public void WhenParseMinimumFileMustReturnTransactionsCorrectly()
        {
            var reader = new OfxReader();
            OfxModel ofx = reader.Load(TestData.BASIC);

            Assert.AreEqual(1, ofx.Ofx.BankMessages.Stmttrns.BANKACCTFROM.BANKTRANLIST.Transactions.Count);

            Assert.AreEqual("PAYMENT", ofx.Ofx.BankMessages.Stmttrns.BANKACCTFROM.BANKTRANLIST.Transactions[0].TRNTYPE);
            Assert.AreEqual("20210305", ofx.Ofx.BankMessages.Stmttrns.BANKACCTFROM.BANKTRANLIST.Transactions[0].DTPOSTED);
            Assert.AreEqual("-1400.77", ofx.Ofx.BankMessages.Stmttrns.BANKACCTFROM.BANKTRANLIST.Transactions[0].TRNAMT);
            Assert.AreEqual("05/03/2021077", ofx.Ofx.BankMessages.Stmttrns.BANKACCTFROM.BANKTRANLIST.Transactions[0].FITID);
            Assert.AreEqual("077", ofx.Ofx.BankMessages.Stmttrns.BANKACCTFROM.BANKTRANLIST.Transactions[0].CHECKNUM);
            Assert.AreEqual("077", ofx.Ofx.BankMessages.Stmttrns.BANKACCTFROM.BANKTRANLIST.Transactions[0].REFNUM);
            Assert.AreEqual("Payment", ofx.Ofx.BankMessages.Stmttrns.BANKACCTFROM.BANKTRANLIST.Transactions[0].MEMO);
        }
    }
}
