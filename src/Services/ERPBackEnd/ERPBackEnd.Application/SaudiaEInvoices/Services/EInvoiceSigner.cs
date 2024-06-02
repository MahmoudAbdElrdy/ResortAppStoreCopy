using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities.Encoders;
using Org.BouncyCastle.X509;
using SaudiEinvoiceService.ApiModels.Requests;
using SaudiEinvoiceService.Constants;
using SaudiEinvoiceService.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Xml;

namespace SaudiEinvoiceService.Services
{
    public class EInvoiceSigner
    {
        public Result SignDocument(string xmlFilePath, string certificateContent, string privateKeyContent, string signedInvoicePath)
        {
            Result result1 = new Result();
            result1.Operation = "Signing E-Invoice Operation";
            result1.IsValid = false;
            try
            {
                if (string.IsNullOrEmpty(certificateContent))
                {
                    result1.ErrorMessage = "Invalid certificate content.";
                    return result1;
                }
                if (string.IsNullOrEmpty(privateKeyContent))
                {
                    result1.ErrorMessage = "Invalid private key content.";
                    return result1;
                }
                XmlDocument xmlDocument1 = new XmlDocument();
                xmlDocument1.PreserveWhitespace = true;
                try
                {
                    xmlDocument1.Load(xmlFilePath);
                }
                catch(Exception ex)
                {
                    string errMsg = ex.Message;
                    if (ex.InnerException != null)
                    {
                        errMsg += ex.InnerException.Message;
                    }
                    result1.ErrorMessage = errMsg+" Can not load XML file " + xmlFilePath;
                    return result1;
                }
                if (string.IsNullOrEmpty(xmlDocument1.InnerText))
                {
                    result1.ErrorMessage = "Invalid invoice XML content";
                    return result1;
                }
                result1.lstSteps = new List<Result>();
                Result result2 = new Result();
                Result einvoiceHashing = GenerateEInvoiceHashing(xmlFilePath);
                string invoiceHash = einvoiceHashing.ResultedValue;
                einvoiceHashing.Operation = "First Step : Generating Hashing";
                if (!einvoiceHashing.IsValid)
                {
                    result1.lstSteps.Add(einvoiceHashing);
                    return result1;
                }
                result1.lstSteps.Add(einvoiceHashing);
                Result result3 = new Result();
                Result digitalSignature = this.GetDigitalSignature(einvoiceHashing.ResultedValue, privateKeyContent);
                digitalSignature.Operation = "Second Step : Generating Digital Signature";
                if (!digitalSignature.IsValid)
                {
                    result1.lstSteps.Add(digitalSignature);
                    return result1;
                }
                result1.lstSteps.Add(digitalSignature);
                Result result4 = new Result();
                result4.IsValid = false;
                result4.Operation = "Third Step : Generating Certificate";
                certificateContent = certificateContent.Replace(" ", "");
                //X509Certificate2 x509Cert = new X509Certificate2((byte[])(object)((IEnumerable<byte>)Encoding.UTF8.GetBytes(certificateContent)).Select<byte, sbyte>((Func<byte, sbyte>)(x => (sbyte)x)).ToArray<sbyte>());
                X509Certificate2 x509Cert = new X509Certificate2((byte[])((IEnumerable<byte>)Encoding.UTF8.GetBytes(certificateContent)));



                Org.BouncyCastle.X509.X509Certificate x509Certificate = DotNetUtilities.FromX509Certificate((System.Security.Cryptography.X509Certificates.X509Certificate)x509Cert);
                sbyte[] array = ((IEnumerable<byte>)SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(x509Certificate.GetPublicKey()).GetEncoded()).Select<byte, sbyte>((Func<byte, sbyte>)(x => (sbyte)x)).ToArray<sbyte>();

                //6f00007ec6e801d35534a77ed7000100007ec6
                //6F00007EC6E801D35534A77ED7000100007EC6
                //var xc = x509Cert.GetSerialNumberString();
                //BigInteger b1 = BigInteger.Parse("0"+xc, NumberStyles.HexNumber);
                BigInteger bigInteger = new BigInteger((byte[])(object)((IEnumerable<byte>)x509Cert.GetSerialNumber()).Select<byte, sbyte>((Func<byte, sbyte>)(x => (sbyte)x)).ToArray<sbyte>());
                
                if (x509Cert != null)
                {
                    result4.IsValid = true;
                    result1.lstSteps.Add(result4);
                    result1.lstSteps.Add(result4);
                    Result result5 = new Result();
                    result5.IsValid = false;
                    result5.Operation = "Forth Step : Generating Certificate Hashing";
                    Result result6 = new Result();
                    try
                    {
                        result5.IsValid = true;
                        result5.ResultedValue = EInvoiceHelper.ToBase64Encode(EInvoiceHelper.Sha256_hashAsString(certificateContent));
                        result1.lstSteps.Add(result5);
                    }
                    catch (Exception ex)
                    {
                        result6.ErrorMessage = ex.Message;
                        result1.lstSteps.Add(result5);
                        return result1;
                    }
                    new Result().IsValid = false;
                    Result result7 = this.TransformXML(xmlDocument1.OuterXml);
                    result7.Operation = "Fifth Step : Transform Xml Result";
                    if (!result7.IsValid)
                    {
                        result1.lstSteps.Add(result7);
                        return result1;
                    }
                    result1.lstSteps.Add(result7);
                    XmlDocument xmlDocument2 = new XmlDocument();
                    xmlDocument2.PreserveWhitespace = true;
                    xmlDocument2.LoadXml(result7.ResultedValue);
                    Dictionary<string, string> nameSpacesMap = this.getNameSpacesMap();
                    Result result8 = new Result();
                    Result result9 = this.PopulateSignedSignatureProperties(xmlDocument2, nameSpacesMap, result5.ResultedValue, this.GetCurrentTimestamp(), x509Cert.IssuerName.Name, bigInteger+"");

                    //Result result9 = this.PopulateSignedSignatureProperties(xmlDocument2, nameSpacesMap, result5.ResultedValue, this.GetCurrentTimestamp(), x509Cert.IssuerName.Name, b1+"");
                    result9.Operation = "Sixth Step : Populate Signed Signature Properties";
                    if (!result9.IsValid)
                    {
                        result1.lstSteps.Add(result9);
                        
                        return result1;
                    }
                    result1.lstSteps.Add(result9);
                    Result result10 = new Result();
                    Result result11 = PopulateUBLExtensions(xmlDocument2, digitalSignature.ResultedValue, result9.ResultedValue, einvoiceHashing.ResultedValue, certificateContent);
                    result11.Operation = "Seventh Step : Populate Populate UBL Extensions";
                    if (!result11.IsValid)
                    {
                        result1.lstSteps.Add(result11);
                        return result1;
                    }
                    result1.lstSteps.Add(result11);
                    Result result12 = new Result();
                    Result result13 = this.PopulateQRCode(xmlDocument2, array, digitalSignature.ResultedValue, einvoiceHashing.ResultedValue, ((IEnumerable<byte>)x509Certificate.GetSignature()).Select<byte, sbyte>((Func<byte, sbyte>)(x => (sbyte)x)).ToArray<sbyte>());
                    result13.Operation = "Eighth Step : Populate QR";
                    if (!result13.IsValid)
                    {
                        result1.lstSteps.Add(result13);
                        return result1;
                    }
                    result1.lstSteps.Add(result13);
                    result1.IsValid = true;
                    result1.ResultedValue = xmlDocument2.OuterXml;
                    foreach (Result lstStep in result1.lstSteps)
                        lstStep.ResultedValue = "";
                    try
                    {
                        result1.InvoiceHash = invoiceHash;
                        result1.Qr = result13.Qr;
                        //xmlDocument2.Save("D:\\SaudiaEInvoiceOutput\\CSR_And_Keys\\NewSigned.xml");
                        xmlDocument2.Save(signedInvoicePath);
                    }
                    catch
                    {
                    }
                    return result1;
                }
                result4.ErrorMessage = "Invalid Certificate";
                result1.lstSteps.Add(result4);
                return result1;
            }
            catch (Exception ex)
            {
                result1.ErrorMessage = ex.Message;
                return result1;
            }
        }


        //public Result SignDocument(string xmlFilePath, string certificateContent, string privateKeyContent, string signedInvoicePath, DateTime signerTimeStamp)
        //{
        //    Result result1 = new Result();
        //    result1.Operation = "Signing E-Invoice Operation";
        //    result1.IsValid = false;
        //    try
        //    {
        //        if (string.IsNullOrEmpty(certificateContent))
        //        {
        //            result1.ErrorMessage = "Invalid certificate content.";
        //            return result1;
        //        }
        //        if (string.IsNullOrEmpty(privateKeyContent))
        //        {
        //            result1.ErrorMessage = "Invalid private key content.";
        //            return result1;
        //        }
        //        XmlDocument xmlDocument1 = new XmlDocument();
        //        xmlDocument1.PreserveWhitespace = true;
        //        try
        //        {
        //            xmlDocument1.Load(xmlFilePath);
        //        }
        //        catch (Exception ex)
        //        {
        //            string errMsg = ex.Message;
        //            if (ex.InnerException != null)
        //            {
        //                errMsg += ex.InnerException.Message;
        //            }
        //            result1.ErrorMessage = errMsg + " Can not load XML file " + xmlFilePath;
        //            return result1;
        //        }
        //        if (string.IsNullOrEmpty(xmlDocument1.InnerText))
        //        {
        //            result1.ErrorMessage = "Invalid invoice XML content";
        //            return result1;
        //        }
        //        result1.lstSteps = new List<Result>();
        //        Result result2 = new Result();
        //        Result einvoiceHashing = GenerateEInvoiceHashing(xmlFilePath);
        //        string invoiceHash = einvoiceHashing.ResultedValue;
        //        einvoiceHashing.Operation = "First Step : Generating Hashing";
        //        if (!einvoiceHashing.IsValid)
        //        {
        //            result1.lstSteps.Add(einvoiceHashing);
        //            return result1;
        //        }
        //        result1.lstSteps.Add(einvoiceHashing);
        //        Result result3 = new Result();
        //        Result digitalSignature = this.GetDigitalSignature(einvoiceHashing.ResultedValue, privateKeyContent);
        //        digitalSignature.Operation = "Second Step : Generating Digital Signature";
        //        if (!digitalSignature.IsValid)
        //        {
        //            result1.lstSteps.Add(digitalSignature);
        //            return result1;
        //        }
        //        result1.lstSteps.Add(digitalSignature);
        //        Result result4 = new Result();
        //        result4.IsValid = false;
        //        result4.Operation = "Third Step : Generating Certificate";

        //        //X509Certificate2 x509Cert = new X509Certificate2((byte[])(object)((IEnumerable<byte>)Encoding.UTF8.GetBytes(certificateContent)).Select<byte, sbyte>((Func<byte, sbyte>)(x => (sbyte)x)).ToArray<sbyte>());
        //        X509Certificate2 x509Cert = new X509Certificate2((byte[])((IEnumerable<byte>)Encoding.UTF8.GetBytes(certificateContent)));



        //        Org.BouncyCastle.X509.X509Certificate x509Certificate = DotNetUtilities.FromX509Certificate((System.Security.Cryptography.X509Certificates.X509Certificate)x509Cert);
        //        sbyte[] array = ((IEnumerable<byte>)SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(x509Certificate.GetPublicKey()).GetEncoded()).Select<byte, sbyte>((Func<byte, sbyte>)(x => (sbyte)x)).ToArray<sbyte>();

        //        //6f00007ec6e801d35534a77ed7000100007ec6
        //        //6F00007EC6E801D35534A77ED7000100007EC6
        //        //var xc = x509Cert.GetSerialNumberString();
        //        //BigInteger b1 = BigInteger.Parse("0"+xc, NumberStyles.HexNumber);
        //        BigInteger bigInteger = new BigInteger((byte[])(object)((IEnumerable<byte>)x509Cert.GetSerialNumber()).Select<byte, sbyte>((Func<byte, sbyte>)(x => (sbyte)x)).ToArray<sbyte>());

        //        if (x509Cert != null)
        //        {
        //            result4.IsValid = true;
        //            result1.lstSteps.Add(result4);
        //            result1.lstSteps.Add(result4);
        //            Result result5 = new Result();
        //            result5.IsValid = false;
        //            result5.Operation = "Forth Step : Generating Certificate Hashing";
        //            Result result6 = new Result();
        //            try
        //            {
        //                result5.IsValid = true;
        //                result5.ResultedValue = EInvoiceHelper.ToBase64Encode(EInvoiceHelper.Sha256_hashAsString(certificateContent));
        //                result1.lstSteps.Add(result5);
        //            }
        //            catch (Exception ex)
        //            {
        //                result6.ErrorMessage = ex.Message;
        //                result1.lstSteps.Add(result5);
        //                return result1;
        //            }
        //            new Result().IsValid = false;
        //            Result result7 = this.TransformXML(xmlDocument1.OuterXml);
        //            result7.Operation = "Fifth Step : Transform Xml Result";
        //            if (!result7.IsValid)
        //            {
        //                result1.lstSteps.Add(result7);
        //                return result1;
        //            }
        //            result1.lstSteps.Add(result7);
        //            XmlDocument xmlDocument2 = new XmlDocument();
        //            xmlDocument2.PreserveWhitespace = true;
        //            xmlDocument2.LoadXml(result7.ResultedValue);
        //            Dictionary<string, string> nameSpacesMap = this.getNameSpacesMap();
        //            Result result8 = new Result();
        //            Result result9 = this.PopulateSignedSignatureProperties(xmlDocument2, nameSpacesMap, result5.ResultedValue, this.GetCurrentTimestamp(signerTimeStamp), x509Cert.IssuerName.Name, bigInteger + "");

        //            //Result result9 = this.PopulateSignedSignatureProperties(xmlDocument2, nameSpacesMap, result5.ResultedValue, this.GetCurrentTimestamp(), x509Cert.IssuerName.Name, b1+"");
        //            result9.Operation = "Sixth Step : Populate Signed Signature Properties";
        //            if (!result9.IsValid)
        //            {
        //                result1.lstSteps.Add(result9);

        //                return result1;
        //            }
        //            result1.lstSteps.Add(result9);
        //            Result result10 = new Result();
        //            Result result11 = PopulateUBLExtensions(xmlDocument2, digitalSignature.ResultedValue, result9.ResultedValue, einvoiceHashing.ResultedValue, certificateContent);
        //            result11.Operation = "Seventh Step : Populate Populate UBL Extensions";
        //            if (!result11.IsValid)
        //            {
        //                result1.lstSteps.Add(result11);
        //                return result1;
        //            }
        //            result1.lstSteps.Add(result11);
        //            Result result12 = new Result();
        //            Result result13 = this.PopulateQRCode(xmlDocument2, array, digitalSignature.ResultedValue, einvoiceHashing.ResultedValue, ((IEnumerable<byte>)x509Certificate.GetSignature()).Select<byte, sbyte>((Func<byte, sbyte>)(x => (sbyte)x)).ToArray<sbyte>());
        //            result13.Operation = "Eighth Step : Populate QR";
        //            if (!result13.IsValid)
        //            {
        //                result1.lstSteps.Add(result13);
        //                return result1;
        //            }
        //            result1.lstSteps.Add(result13);
        //            result1.IsValid = true;
        //            result1.ResultedValue = xmlDocument2.OuterXml;
        //            foreach (Result lstStep in result1.lstSteps)
        //                lstStep.ResultedValue = "";
        //            try
        //            {
        //                result1.InvoiceHash = invoiceHash;
        //                result1.Qr = result13.Qr;
        //                //xmlDocument2.Save("D:\\SaudiaEInvoiceOutput\\CSR_And_Keys\\NewSigned.xml");
        //                xmlDocument2.Save(signedInvoicePath);
        //            }
        //            catch
        //            {
        //            }
        //            return result1;
        //        }
        //        result4.ErrorMessage = "Invalid Certificate";
        //        result1.lstSteps.Add(result4);
        //        return result1;
        //    }
        //    catch (Exception ex)
        //    {
        //        result1.ErrorMessage = ex.Message;
        //        return result1;
        //    }
        //}

        private Result PopulateUBLExtensions(
          XmlDocument xmlDoc,
          string digitalSignature,
          string signedPropertiesHashing,
          string xmlHashing,
          string certificate)
        {
            Result result = new Result();
            try
            {
                EInvoiceHelper.SetNodeValue(xmlDoc, Settings.SIGNATURE_XPATH, digitalSignature);
                EInvoiceHelper.SetNodeValue(xmlDoc, Settings.CERTIFICATE_XPATH, certificate);
                EInvoiceHelper.SetNodeValue(xmlDoc, Settings.SIGNED_Properities_DIGEST_VALUE_XPATH, signedPropertiesHashing);
                EInvoiceHelper.SetNodeValue(xmlDoc, Settings.Hash_XPATH, xmlHashing);
                result.IsValid = true;
            }
            catch (Exception ex)
            {
                result.IsValid = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        private Result PopulateSignedSignatureProperties(
          XmlDocument document,
          Dictionary<string, string> nameSpacesMap,
          string publicKeyHashing,
          string signatureTimestamp,
          string x509IssuerName,
          string serialNumber)
        {
            Result result = new Result();
            try
            {
                EInvoiceHelper.SetNodeValue(document, Settings.PUBLIC_KEY_HASHING_XPATH, publicKeyHashing);
                EInvoiceHelper.SetNodeValue(document, Settings.SIGNING_TIME_XPATH, signatureTimestamp);
                EInvoiceHelper.SetNodeValue(document, Settings.ISSUER_NAME_XPATH, x509IssuerName);
                EInvoiceHelper.SetNodeValue(document, Settings.X509_SERIAL_NUMBER_XPATH, serialNumber);
                string str = EInvoiceHelper.GetNodeInnerXML(document, Settings.SIGNED_PROPERTIES_XPATH).Replace(" />", "/>").Replace("></ds:DigestMethod>", "/>");
                EInvoiceHelper.Sha256_hashAsString(str.Replace("\r", ""));
                result.ResultedValue = EInvoiceHelper.ToBase64Encode(EInvoiceHelper.Sha256_hashAsString(str.Replace("\r", "")));
                result.IsValid = true;
            }
            catch (Exception ex)
            {
                result.IsValid = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        //private string GetCurrentTimestamp() => DateTime.Now.ToString("yyyy-MM-dd'T'HH:mm:ss'Z'");
        private string GetCurrentTimestamp() {
            CultureInfo culture = new CultureInfo("en-US");
            return  DateTime.Now.ToString("yyyy-MM-dd'T'HH:mm:ss", culture);
        } 


        private Dictionary<string, string> getNameSpacesMap() => new Dictionary<string, string>()
    {
      {
        "cac",
        "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2"
      },
      {
        "cbc",
        "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2"
      },
      {
        "ext",
        "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2"
      },
      {
        "sig",
        "urn:oasis:names:specification:ubl:schema:xsd:CommonSignatureComponents-2"
      },
      {
        "sac",
        "urn:oasis:names:specification:ubl:schema:xsd:SignatureAggregateComponents-2"
      },
      {
        "sbc",
        "urn:oasis:names:specification:ubl:schema:xsd:SignatureBasicComponents-2"
      },
      {
        "ds",
        "http://www.w3.org/2000/09/xmldsig#"
      },
      {
        "xades",
        "http://uri.etsi.org/01903/v1.3.2#"
      }
    };

        public Result GetDigitalSignature(string xmlHashing, string privateKeyContent)
        {
            Result digitalSignature = new Result();
            try
            {
                sbyte[] array = ((IEnumerable<byte>)EInvoiceHelper.ToBase64DecodeAsBinary(xmlHashing)).Select<byte, sbyte>((Func<byte, sbyte>)(x => (sbyte)x)).ToArray<sbyte>();
                if (!privateKeyContent.Contains("-----BEGIN EC PRIVATE KEY-----") && !privateKeyContent.Contains("-----END EC PRIVATE KEY-----"))
                    privateKeyContent = "-----BEGIN EC PRIVATE KEY-----\n" + privateKeyContent + "\n-----END EC PRIVATE KEY-----";
                byte[] signature;
                using (TextReader reader = (TextReader)new StringReader(privateKeyContent))
                {
                    AsymmetricKeyParameter parameters = ((AsymmetricCipherKeyPair)new PemReader(reader).ReadObject()).Private;
                    ISigner signer = SignerUtilities.GetSigner("SHA-256withECDSA");
                    signer.Init(true, (ICipherParameters)parameters);
                    signer.BlockUpdate((byte[])(object)array, 0, array.Length);
                    signature = signer.GenerateSignature();
                }
                digitalSignature.IsValid = true;
                digitalSignature.ResultedValue = Convert.ToBase64String(signature);
            }
            catch
            {
                digitalSignature.IsValid = false;
            }
            return digitalSignature;
        }

        public Result TransformXML(string xmlContent)
        {
            string xml = "";
            Result result = new Result();
            result.IsValid = false;
            try
            {
                xml = EInvoiceHelper.ApplyXSLTPassingXML(xmlContent, Settings.Embeded_Remove_Elements_PATH);
            }
            catch (Exception ex)
            {
                result.ErrorMessage = "Error in removing elements.";
            }
            try
            {
                xml = EInvoiceHelper.ApplyXSLTPassingXML(xml, Settings.Embeded_Add_UBL_Element_PATH);
            }
            catch (Exception ex)
            {
                result.ErrorMessage = "Error in adding UBL elements.";
            }
            try
            {
                xml = xml.Replace("UBL-TO-BE-REPLACED", new StreamReader(EInvoiceHelper.ReadInternalEmbededResourceStream(Settings.Embeded_UBL_File_PATH)).ReadToEnd());
            }
            catch (Exception ex)
            {
                result.ErrorMessage = "Error in replacing UBL elements.";
            }
            try
            {
                xml = EInvoiceHelper.ApplyXSLTPassingXML(xml, Settings.Embeded_Add_QR_Element_PATH);
            }
            catch (Exception ex)
            {
                result.ErrorMessage = "Error in adding QR elements.";
            }
            try
            {
                xml = xml.Replace("QR-TO-BE-REPLACED", new StreamReader(EInvoiceHelper.ReadInternalEmbededResourceStream(Settings.Embeded_QR_XML_File_PATH)).ReadToEnd());
            }
            catch (Exception ex)
            {
                result.ErrorMessage = "Error in replacing QR elements.";
            }
            try
            {
                xml = EInvoiceHelper.ApplyXSLTPassingXML(xml, Settings.Embeded_Add_Signature_Element_PATH);
            }
            catch (Exception ex)
            {
                result.ErrorMessage = "Error in adding signature elements.";
            }
            try
            {
                xml = xml.Replace("SIGN-TO-BE-REPLACED", new StreamReader(EInvoiceHelper.ReadInternalEmbededResourceStream(Settings.Embeded_Signature_File_PATH)).ReadToEnd());
            }
            catch (Exception ex)
            {
                result.ErrorMessage = "Error in replacing signature elements.";
            }
            if (xml != null)
            {
                result.ResultedValue = xml;
                result.IsValid = true;
            }
            return result;
        }

        private Result PopulateQRCode(
          XmlDocument xmlDoc,
          sbyte[] publicKeyArr,
          string signature,
          string hashedXml,
          sbyte[] certificateSignatureBytes)
        {
            Result result1 = new Result();
            result1.IsValid = false;
            string nodeInnerText1 = EInvoiceHelper.GetNodeInnerText(xmlDoc, Settings.SELLER_NAME_XPATH);
            if (string.IsNullOrEmpty(nodeInnerText1))
            {
                result1.ErrorMessage = "Unable to get SELLER_NAME value";
                return result1;
            }
            string nodeInnerText2 = EInvoiceHelper.GetNodeInnerText(xmlDoc, Settings.VAT_REGISTERATION_XPATH);
            if (string.IsNullOrEmpty(nodeInnerText2))
            {
                result1.ErrorMessage = "Unable to get VAT_REGISTERATION value";
                return result1;
            }
            string nodeInnerText3 = EInvoiceHelper.GetNodeInnerText(xmlDoc, Settings.ISSUE_DATE_XPATH);
            if (string.IsNullOrEmpty(nodeInnerText3))
            {
                result1.ErrorMessage = "Unable to get ISSUE_DATE value";
                return result1;
            }
            string nodeInnerText4 = EInvoiceHelper.GetNodeInnerText(xmlDoc, Settings.ISSUE_TIME_XPATH);
            if (string.IsNullOrEmpty(nodeInnerText4))
            {
                result1.ErrorMessage = "Unable to get ISSUE_TIME value";
                return result1;
            }
            string nodeInnerText5 = EInvoiceHelper.GetNodeInnerText(xmlDoc, Settings.INVOICE_TOTAL_XPATH);
            if (string.IsNullOrEmpty(nodeInnerText5))
            {
                result1.ErrorMessage = "Unable to get INVOICE_TOTAL value";
                return result1;
            }
            string nodeInnerText6 = EInvoiceHelper.GetNodeInnerText(xmlDoc, Settings.VAT_TOTAL_XPATH);
            if (string.IsNullOrEmpty(nodeInnerText6))
            {
                result1.ErrorMessage = "Unable to get VAT_TOTAL value";
                return result1;
            }
            EInvoiceHelper.GetNodeInnerText(xmlDoc, Settings.QR_CODE_XPATH);
            DateTime result2 = new DateTime();
            string str = nodeInnerText3 + " " + nodeInnerText4;
            DateTime.TryParseExact(nodeInnerText3, Settings.allDatesFormats, (IFormatProvider)CultureInfo.InvariantCulture, DateTimeStyles.None, out result2);
            string[] strArray = nodeInnerText4.Split(':');
            int result3 = 0;
            int result4 = 0;
            int result5 = 0;
            if (!string.IsNullOrEmpty(strArray[0]) && int.TryParse(strArray[0], out result3))
                result2 = result2.AddHours((double)result3);
            if (strArray.Length > 1 && !string.IsNullOrEmpty(strArray[1]) && int.TryParse(strArray[1], out result4))
                result2 = result2.AddMinutes((double)result4);
            if (strArray.Length > 2 && !string.IsNullOrEmpty(strArray[2]) && int.TryParse(strArray[2], out result5))
                result2 = result2.AddSeconds((double)result5);
            //string timeStamp = result2.ToString("yyyy-MM-dd'T'HH:mm:ss'Z'");
            CultureInfo culture = new CultureInfo("en-US");
            string timeStamp = result2.ToString("yyyy-MM-dd'T'HH:mm:ss", culture);
            bool isSimplified = false;
            if (EInvoiceHelper.GetInvoiceType(xmlDoc) == "Simplified")
                isSimplified = true;
            string qrCodeFromValues = GenerateQrCodeFromValues(nodeInnerText1, nodeInnerText2, timeStamp, nodeInnerText5, nodeInnerText6, hashedXml, publicKeyArr, signature, isSimplified, certificateSignatureBytes);
            try
            {
                EInvoiceHelper.SetNodeValue(xmlDoc, Settings.QR_CODE_XPATH, qrCodeFromValues);
            }
            catch
            {
                result1.ErrorMessage = "There is no node for QR in XML file.";
                result1.IsValid = false;
            }
            result1.IsValid = true;
            result1.Qr = qrCodeFromValues;
            return result1;
        }


        public Result GenerateEInvoiceHashing(string xmlFilePath)
        {
            Result einvoiceHashing = new Result();
            einvoiceHashing.Operation = "Generate Invoice Hashing";
            einvoiceHashing.IsValid = false;
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                try
                {
                    xmlDocument.Load(xmlFilePath);
                }
                catch
                {
                    einvoiceHashing.ErrorMessage = "Can not load XML file";
                    return einvoiceHashing;
                }
                if (string.IsNullOrEmpty(xmlDocument.OuterXml))
                {
                    einvoiceHashing.ErrorMessage = "Invalid invoice XML content";
                    return einvoiceHashing;
                }
                string s;
                try
                {
                    s = EInvoiceHelper.ApplyXSLT(xmlFilePath, Settings.Embeded_InvoiceXSLFileForHashing);
                }
                catch
                {
                    einvoiceHashing.ErrorMessage = "Can not apply XSL file";
                    return einvoiceHashing;
                }
                if (string.IsNullOrEmpty(s))
                {
                    einvoiceHashing.ErrorMessage = "Error In applying XSL file";
                    return einvoiceHashing;
                }
                using (MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(s)))
                {
                    XmlDsigC14NTransform dsigC14Ntransform = new XmlDsigC14NTransform(false);
                    dsigC14Ntransform.LoadInput((object)memoryStream);
                    sbyte[] array = ((IEnumerable<byte>)EInvoiceHelper.Sha256_hashAsBytes(Encoding.UTF8.GetString((dsigC14Ntransform.GetOutput() as MemoryStream).ToArray()))).Select<byte, sbyte>((Func<byte, sbyte>)(x => (sbyte)x)).ToArray<sbyte>();
                    einvoiceHashing.ResultedValue = EInvoiceHelper.ToBase64Encode((byte[])(object)array);
                    einvoiceHashing.IsValid = true;
                }
                return einvoiceHashing;
            }
            catch (Exception ex)
            {
                einvoiceHashing.ErrorMessage = ex.Message;
                return einvoiceHashing;
            }
        }


        public string GenerateQrCodeFromValues(
          string sellerName,
          string vatRegistrationNumber,
          string timeStamp,
          string invoiceTotal,
          string vatTotal,
          string hashedXml,
          sbyte[] publicKey,
          string digitalSignature,
          bool isSimplified,
          sbyte[] certificateSignature)
        {
            sbyte[] array1 = ((IEnumerable<byte>)Encoding.UTF8.GetBytes(digitalSignature)).Select<byte, sbyte>((Func<byte, sbyte>)(x => (sbyte)x)).ToArray<sbyte>();
            byte[] array2 = ((IEnumerable<byte>)EInvoiceHelper.WriteTlv(1U, Encoding.UTF8.GetBytes(sellerName)).ToArray()).Concat<byte>((IEnumerable<byte>)EInvoiceHelper.WriteTlv(2U, Encoding.UTF8.GetBytes(vatRegistrationNumber)).ToArray()).Concat<byte>((IEnumerable<byte>)EInvoiceHelper.WriteTlv(3U, Encoding.UTF8.GetBytes(timeStamp)).ToArray()).Concat<byte>((IEnumerable<byte>)EInvoiceHelper.WriteTlv(4U, Encoding.UTF8.GetBytes(invoiceTotal)).ToArray()).Concat<byte>((IEnumerable<byte>)EInvoiceHelper.WriteTlv(5U, Encoding.UTF8.GetBytes(vatTotal)).ToArray()).Concat<byte>((IEnumerable<byte>)EInvoiceHelper.WriteTlv(6U, Encoding.UTF8.GetBytes(hashedXml)).ToArray()).Concat<byte>((IEnumerable<byte>)EInvoiceHelper.WriteTlv(7U, (byte[])(object)array1).ToArray()).Concat<byte>((IEnumerable<byte>)EInvoiceHelper.WriteTlv(8U, (byte[])(object)publicKey).ToArray()).ToArray<byte>();
            if (isSimplified)
                array2 = ((IEnumerable<byte>)array2).Concat<byte>((IEnumerable<byte>)EInvoiceHelper.WriteTlv(9U, (byte[])(object)certificateSignature).ToArray()).ToArray<byte>();
            return EInvoiceHelper.ToBase64Encode(array2);
        }


        public bool ValidateSignature(ref string errorMessage, string xmlFilePath, string certificateContent)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlFilePath);

                string str1 = EInvoiceHelper.GetNodeInnerText(xmlDoc, Settings.CERTIFICATE_XPATH).Trim();
                if (string.IsNullOrEmpty(str1))
                {
                    errorMessage = "Unable to get CERTIFICATE value from E-invoice XML";
                    return false;
                }
                sbyte[] array1 = ((IEnumerable<byte>)EInvoiceHelper.ToBase64DecodeAsBinary(str1)).Select<byte, sbyte>((Func<byte, sbyte>)(x => (sbyte)x)).ToArray<sbyte>();
                if (array1 != null && array1.Length == 0)
                {
                    errorMessage = "Invalid CERTIFICATE";
                    return false;
                }

                Result einvoiceHashing = GenerateEInvoiceHashing(xmlFilePath);
                if (!einvoiceHashing.IsValid)
                {
                    errorMessage = "Invalid Hashing Generation";
                    return false;
                }
                Result result = ValidateEInvoiceHashing(xmlFilePath);
                if (!result.IsValid)
                {
                    errorMessage = result.ErrorMessage;
                    return false;
                }
                sbyte[] array2;
                try
                {
                    array2 = ((IEnumerable<byte>)SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(DotNetUtilities.FromX509Certificate((System.Security.Cryptography.X509Certificates.X509Certificate)new X509Certificate2((byte[])(object)array1)).GetPublicKey()).GetEncoded()).Select<byte, sbyte>((Func<byte, sbyte>)(x => (sbyte)x)).ToArray<sbyte>();
                }
                catch
                {
                    errorMessage = "Invalid CERTIFICATE";
                    return false;
                }
                string nodeInnerText1 = EInvoiceHelper.GetNodeInnerText(xmlDoc, Settings.SIGNATURE_XPATH);
                if (string.IsNullOrEmpty(nodeInnerText1))
                {
                    errorMessage = "Unable to get Signature value in E-invoice XML.";
                    return false;
                }
                sbyte[] array3 = ((IEnumerable<byte>)EInvoiceHelper.ToBase64DecodeAsBinary(einvoiceHashing.ResultedValue)).Select<byte, sbyte>((Func<byte, sbyte>)(x => (sbyte)x)).ToArray<sbyte>();
                AsymmetricKeyParameter key = PublicKeyFactory.CreateKey((byte[])(object)array2);
                ISigner signer = SignerUtilities.GetSigner("SHA-256withECDSA");
                signer.Init(false, (ICipherParameters)key);
                signer.BlockUpdate((byte[])(object)array3, 0, array3.Length);
                sbyte[] array4 = ((IEnumerable<byte>)Convert.FromBase64String(nodeInnerText1)).Select<byte, sbyte>((Func<byte, sbyte>)(x => (sbyte)x)).ToArray<sbyte>();
                bool flag = signer.VerifySignature((byte[])(object)array4);
                if (!flag)
                {
                    errorMessage = "Wrong signature value.";
                    return false;
                }
                //string str2 = EInvoiceHelper.GetNodeInnerXML(xmlDoc, Settings.SIGNED_PROPERTIES_XPATH).Replace(" />", "/>").Replace("></ds:DigestMethod>", "/>");
                string str2 = EInvoiceHelper.GetNodeInnerXML(xmlDoc, Settings.SIGNED_PROPERTIES_XPATH);
                string nodeInnerText2 = EInvoiceHelper.GetNodeInnerText(xmlDoc, Settings.SIGNED_Properities_DIGEST_VALUE_XPATH);
                //if (EInvoiceHelper.ToBase64Encode(EInvoiceHelper.Sha256_hashAsString(str2.Replace("\r", ""))) != nodeInnerText2)
                var signedPropertiesHash = EInvoiceHelper.ToBase64Encode(EInvoiceHelper.Sha256_hashAsString(str2.Replace("\r", "")));
                if (signedPropertiesHash != nodeInnerText2)
                {
                    errorMessage = "Wrong signing properties digest value.";
                    //return false;
                }
                if (EInvoiceHelper.ToBase64Encode(EInvoiceHelper.Sha256_hashAsString(str1)) != EInvoiceHelper.GetNodeInnerText(xmlDoc, Settings.SIGNED_Certificate_DIGEST_VALUE_XPATH))
                {
                    errorMessage = "Wrong signing certificate digest value.";
                    return false;
                }
                X509Certificate2 x509Certificate2 = new X509Certificate2((byte[])(object)((IEnumerable<byte>)Encoding.UTF8.GetBytes(certificateContent)).Select<byte, sbyte>((Func<byte, sbyte>)(x => (sbyte)x)).ToArray<sbyte>());
                var xmlSerial = EInvoiceHelper.GetNodeInnerText(xmlDoc, Settings.X509_SERIAL_NUMBER_XPATH);
                var certificateSerial = new BigInteger((byte[])(object)((IEnumerable<byte>)x509Certificate2.GetSerialNumber()).Select<byte, sbyte>((Func<byte, sbyte>)(x => (sbyte)x)).ToArray<sbyte>()).ToString();
                if (xmlSerial != certificateSerial )
                {
                    errorMessage = "Invalid certificate serial number.";
                    //return false;
                }
                string xmlIssureName = EInvoiceHelper.GetNodeInnerText(xmlDoc, Settings.ISSUER_NAME_XPATH);
                string certificateIssuerName = x509Certificate2.IssuerName.Name;
                if (!(xmlIssureName != certificateIssuerName))
                    return flag;
                errorMessage = "Invalid certificate issuer name.";
                return false;
            }
            catch (Exception ex)
            {
                errorMessage = "Error occurred in validating signature.";
                return false;
            }
        }


        public Result ValidateEInvoiceHashing(string xmlFilePath)
        {
            Result result = new Result();
            result.Operation = "Validating Invoice Hashing";
            result.IsValid = false;
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.Load(xmlFilePath);
            }
            catch
            {
                result.ErrorMessage = "Can not load XML file";
                return result;
            }
            string nodeInnerText = EInvoiceHelper.GetNodeInnerText(doc, Settings.Hash_XPATH);
            if (string.IsNullOrEmpty(nodeInnerText))
            {
                result.ErrorMessage = "There is no Hashing node value in this XML file";
                return result;
            }
            Result einvoiceHashing = this.GenerateEInvoiceHashing(xmlFilePath);
            if (!einvoiceHashing.IsValid)
            {
                result.ErrorMessage = einvoiceHashing.ErrorMessage;
                return result;
            }
            if (nodeInnerText != einvoiceHashing.ResultedValue)
            {
                result.ErrorMessage = "The generated Hashing is different of the one exists in the XML file.";
                return result;
            }
            result.IsValid = true;
            return result;
        }



    }
}




