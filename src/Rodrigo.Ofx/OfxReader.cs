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

            OfxDictionary<string, object> root = XmlToDictionaryConverter.Convert(ofxBodyContent);

            model.Ofx.SignOnMessage = ParseSignOnMessages(root["OFX"]["SIGNONMSGSRSV1"]["SONRS"]);

            model.Ofx.BankMessages = ParseBankMessages(root["OFX"]["BANKMSGSRSV1"]);

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

            return model;
        }

        private static dynamic ParseSignOnMessages(dynamic fileContent)
        {
            var messages = new SignOnMessage();

            messages.Status.Code = fileContent["STATUS"]["CODE"];
            messages.Status.Severity = fileContent["STATUS"]["SEVERITY"];
            messages.DateServer = fileContent["DTSERVER"];
            messages.Language = fileContent["LANGUAGE"];
            messages.Fi.Organization = fileContent["FI"]["ORG"];
            messages.Fi.Fid = fileContent["FI"]["FID"];

            return messages;
        }

        private BankMessages ParseBankMessages(dynamic fileContent)
        {
            var messages = new BankMessages();

            messages.Stmttrns.Trnuid = fileContent["STMTTRNRS"]["TRNUID"];
            messages.Stmttrns.Status.Code = fileContent["STMTTRNRS"]["STATUS"]["CODE"];
            messages.Stmttrns.Status.Severity = fileContent["STMTTRNRS"]["STATUS"]["SEVERITY"];
            messages.Stmttrns.CURDEF = fileContent["STMTTRNRS"]["STMTRS"]["CURDEF"];
            messages.Stmttrns.BANKACCTFROM.Id = fileContent["STMTTRNRS"]["STMTRS"]["BANKACCTFROM"]["BANKID"];
            messages.Stmttrns.BANKACCTFROM.BranchID = fileContent["STMTTRNRS"]["STMTRS"]["BANKACCTFROM"]["BRANCHID"];
            messages.Stmttrns.BANKACCTFROM.AccountID = fileContent["STMTTRNRS"]["STMTRS"]["BANKACCTFROM"]["ACCTID"];
            messages.Stmttrns.BANKACCTFROM.AccountType = fileContent["STMTTRNRS"]["STMTRS"]["BANKACCTFROM"]["ACCTTYPE"];
            messages.Stmttrns.BANKACCTFROM.BANKTRANLIST.DTSTART = fileContent["STMTTRNRS"]["STMTRS"]["BANKTRANLIST"]["DTSTART"];
            messages.Stmttrns.BANKACCTFROM.BANKTRANLIST.DTEND = fileContent["STMTTRNRS"]["STMTRS"]["BANKTRANLIST"]["DTEND"];


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
