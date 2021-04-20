using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using Rodrigo.Ofx.Attributes;
using Rodrigo.Ofx.Models;

namespace Rodrigo.Ofx
{
    public class OfxReader
    {
        public OfxModel Load(string fileContent)
        {
            var model = new OfxModel();

            var lines = fileContent.Split("\r\n");

            fileContent = string.Join("", lines);

            string ofxBodyContent = FileParser.GetOfxBody(fileContent);

            ofxBodyContent = "<xml>" + ofxBodyContent + "</xml>";

            using (var stringReader = new StringReader(ofxBodyContent))
            {
                XmlReader reader = XmlReader.Create(stringReader);
                XmlDocument doc = new XmlDocument();
                doc.Load(reader);

                XmlNode node = doc.DocumentElement.FirstChild;

                model.Ofx.SignOnMessage = ParseSignOnMessages(node["SIGNONMSGSRSV1"]["SONRS"]);

                model.Ofx.BankMessages = ParseBankMessages(node["BANKMSGSRSV1"]);

                foreach (var line in lines)
                {
                    string value = "";
                    string propName = "";

                    foreach (var property in model.GetType().GetProperties())
                    {
                        object[] attrs = property.GetCustomAttributes(true);
                        foreach (object attr in attrs)
                        {
                            OfxProperty authAttr = attr as OfxProperty;
                            if (authAttr != null)
                            {
                                propName = property.Name;
                                value = authAttr.FieldName;
                            }
                        }

                        var keyValue = line.Split(":");

                        if (keyValue.Length > 1 && value == keyValue[0])
                        {
                            model[propName] = keyValue[1];
                        }
                    }
                }

            }
            return model;
        }

        private static dynamic ParseSignOnMessages(XmlNode fileContent)
        {
            var messages = new SignOnMessage();

            messages.Status.Code = fileContent["STATUS"]["CODE"].InnerText;
            messages.Status.Severity = fileContent["STATUS"]["SEVERITY"].InnerText;
            messages.DateServer = fileContent["DTSERVER"].InnerText;
            messages.Language = fileContent["LANGUAGE"].InnerText;
            messages.Fi.Organization = fileContent["FI"]["ORG"].InnerText;
            messages.Fi.Fid = fileContent["FI"]["FID"].InnerText;

            return messages;
        }

        private BankMessages ParseBankMessages(XmlNode fileContent)
        {
            var messages = new BankMessages();

            messages.Stmttrns.Trnuid = fileContent["STMTTRNRS"]["TRNUID"].InnerText;
            messages.Stmttrns.Status.Code = fileContent["STMTTRNRS"]["STATUS"]["CODE"].InnerText;
            messages.Stmttrns.Status.Severity = fileContent["STMTTRNRS"]["STATUS"]["SEVERITY"].InnerText;
            messages.Stmttrns.Currency = fileContent["STMTTRNRS"]["STMTRS"]["CURDEF"].InnerText;
            messages.Stmttrns.AccountFrom.Id = fileContent["STMTTRNRS"]["STMTRS"]["BANKACCTFROM"]["BANKID"].InnerText;
            messages.Stmttrns.AccountFrom.BranchID = fileContent["STMTTRNRS"]["STMTRS"]["BANKACCTFROM"]["BRANCHID"].InnerText;
            messages.Stmttrns.AccountFrom.AccountID = fileContent["STMTTRNRS"]["STMTRS"]["BANKACCTFROM"]["ACCTID"].InnerText;
            messages.Stmttrns.AccountFrom.AccountType = fileContent["STMTTRNRS"]["STMTRS"]["BANKACCTFROM"]["ACCTTYPE"].InnerText;
            messages.Stmttrns.AccountFrom.TransacionList.StartDate = fileContent["STMTTRNRS"]["STMTRS"]["BANKTRANLIST"]["DTSTART"].InnerText;
            messages.Stmttrns.AccountFrom.TransacionList.EndDate = fileContent["STMTTRNRS"]["STMTRS"]["BANKTRANLIST"]["DTEND"].InnerText;

            foreach (XmlNode item in fileContent["STMTTRNRS"]["STMTRS"]["BANKTRANLIST"])
            {
                if (item.Name == "STMTTRN")
                {
                    messages.Stmttrns.AccountFrom.TransacionList.Transactions.Add(new Stmttrn()
                    {
                        Type = item["TRNTYPE"].InnerText,
                        Date = item["DTPOSTED"].InnerText,
                        Ammount = item["TRNAMT"].InnerText,
                        FITID = item["FITID"].InnerText,
                        CHECKNUM = item["CHECKNUM"].InnerText,
                        REFNUM = item["REFNUM"].InnerText,
                        MEMO = item["MEMO"].InnerText,
                    });
                }
            }


            return messages;
        }

        private string GetXmlValue (XmlReader reader)
        {
            XElement element = XNode.ReadFrom(reader) as XElement;
            if (element == null)
                return "";

            return element.Value;
        }
    }
}
