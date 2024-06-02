using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using SaudiEinvoiceService.ViewModels;
using SaudiEinvoiceService.IRepos;
using SaudiEinvoiceService.Constants;
using Newtonsoft.Json;
using SaudiEinvoiceService.ApiModels.Requests;
using SaudiEinvoiceService.ApiModels.Responses;
using System.Net.Http.Headers;
using System.Reflection;
using System.Xml;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Org.BouncyCastle.Utilities;
using System.Security;
using System.Runtime.InteropServices;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Utilities.IO.Pem;
using Org.BouncyCastle.Utilities.IO;
using PemWriter = Org.BouncyCastle.OpenSsl.PemWriter;
using PemReader = Org.BouncyCastle.OpenSsl.PemReader;
using System.Net.Sockets;
using Org.BouncyCastle.Asn1.Pkcs;
using System.Security.Cryptography.Xml;
using Org.BouncyCastle.X509;
using System.Globalization;
using System.Xml.Linq;
using System.Numerics;
using System.ComponentModel.DataAnnotations;
using X509Certificate = System.Security.Cryptography.X509Certificates.X509Certificate;
using System.Net.Http;
using Azure.Core;
using System.Net.Mail;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading;
using Microsoft.Extensions.Configuration;

namespace SaudiEinvoiceService.Services
{
    public class EInvoiceManager : IEInvoiceManager
    {
        private ILogger<EInvoiceManager> _logger;
        private HttpClient _httpClient;
        private Uri _baseUri;
        private bool _isProduction;
        private readonly IEInvoiceResponseLogRepos _eInvoiceResponseLogRepos;
        public EInvoiceManager(ILogger<EInvoiceManager> logger, IEInvoiceResponseLogRepos eInvoiceResponseLogRepos, IConfiguration _configuration)
        {
            _logger = logger;
            _httpClient = new HttpClient();
            //IConfigurationRoot configuration = new ConfigurationBuilder()
            //                  .SetBasePath(Directory.GetCurrentDirectory())
            //                  .AddJsonFile("appsettings.json")
            //                  .Build();
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("Accept-version", "V2");
            _httpClient.DefaultRequestHeaders.Add("Accept-Language", "en");
            var p = _configuration.GetSection("IsProduction").Value;
            if (Convert.ToBoolean(_configuration.GetSection("IsProduction").Value))
            {
                _isProduction = true;
                _httpClient.BaseAddress = new Uri(EndPoints.BaseUrlProduction);
                _baseUri = new Uri(EndPoints.BaseUrlProduction);

            }
            else
            {
                _isProduction = false;
                _httpClient.BaseAddress = new Uri(EndPoints.BaseUrlTest);
                _baseUri = new Uri(EndPoints.BaseUrlTest);
            }
            _eInvoiceResponseLogRepos = eInvoiceResponseLogRepos;
        }

        public SignerData GenerateKeyPairsAndCSR(string privateKeyPath, string publicKeyPath, string csrConfigPath, string csrPath)
        {
            SignerData signerData = new SignerData();
            //D:\SaudiaEInvoiceOutput\CSR_And_Keys\PrivateKey.pem
            string openSSLPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\OpenSSL\OpenSSL\bin\openssl.exe";
            //_logger.LogInformation("Start OpenSSL");
            //_logger.LogInformation("OpenSSL Path is :" + openSSLPath);
            //_logger.LogInformation("SSL Code is :" + openSSLPath + " " + @"ecparam -name secp256k1 -genkey -noout -out " + privateKeyPath);
            ProcessStartInfo startInfo = new ProcessStartInfo("\"" + openSSLPath + "\"", @"ecparam -name secp256k1 -genkey -noout -out " + "\"" + privateKeyPath + "\"");
            if (!File.Exists(privateKeyPath))
            {
                //Generate Privcate Key
                Process.Start(startInfo);
                Thread.Sleep(2000);
            }

            if (!File.Exists(publicKeyPath))
            {
                //Generate Public Key
                //D:\SaudiaEInvoiceOutput\CSR_And_Keys\publicKey.pem
                //D:\SaudiaEInvoiceOutput\CSR_And_Keys\PrivateKey.pem
                startInfo.Arguments = @"ec -in " + "\"" + privateKeyPath + "\"" + " -pubout -conv_form compressed -out \"" + publicKeyPath + "\"";
                Process.Start(startInfo);
                Thread.Sleep(2000);
            }

            var publicKeyPathBin = publicKeyPath.Replace(".pem", "") + ".bin";
            if (!File.Exists(publicKeyPathBin))
            {

                //base64 -d -in publickey.pem -out publickey.bin
                startInfo.Arguments = @"base64 -d -in " + "\"" + publicKeyPath + "\"" + " -out " + "\"" + publicKeyPathBin + "\"";
                Process.Start(startInfo);
                Thread.Sleep(2000);
            }



            if (!File.Exists(csrPath))
            {
                //Generate CSR
                //D:\SaudiaEInvoiceOutput\CSR_And_Keys\crs-config.cnf
                //D:\SaudiaEInvoiceOutput\CSR_And_Keys\taxPayer.csr
                startInfo.Arguments = @"req -new -sha256 -key " + "\"" + privateKeyPath + "\"" + " -extensions v3_req -config \"" + csrConfigPath + "\"" + " -out " + "\"" + csrPath + "\"";
                Process.Start(startInfo);
                Thread.Sleep(2000);
            }

            var base64Crs = csrPath + "-base64Encode.txt";
            if (!File.Exists(base64Crs))
            {
                // base64 -in taxpayer.csr -out taxpayerCSRbase64Encoded.txt
                startInfo.Arguments = @"base64 -in " + "\"" + csrPath + "\"" + " -out " + "\"" + base64Crs + "\"";
                Process.Start(startInfo);
                Thread.Sleep(2000);
            }






            //@"D:\SaudiaEInvoiceOutput\CSR_And_Keys\taxPayer.csr"
            //var reader = new StreamReader(csrPath);
            var reader = new StreamReader(base64Crs);
            var data = reader.ReadToEnd();
            data = data.Replace("\n", "");
            data = data.Replace("\r", "");
            reader.Close();
            //Thread.Sleep(1000);
            //@"D:\SaudiaEInvoiceOutput\CSR_And_Keys\PrivateKey.pem"
            reader = new StreamReader(privateKeyPath);
            var privateKeyString = reader.ReadToEnd();
            reader.Close();
            //Thread.Sleep(1000);
            //signerData.CSR = ConvertStringToBase64(data);

            signerData.CSR = data;
            signerData.EnCodedPrivateKey = ConvertStringToBase64(privateKeyString);
            signerData.PrivateKey = privateKeyString;
            SaveFile(base64Crs, data);
            return signerData;
        }


        public string ReadFile(string path)
        {
            if (File.Exists(path))
            {
                var reader = new StreamReader(path);
                var data = reader.ReadToEnd(); ;
                reader.Close();
                return data;
            }
            return "";

        }

        public bool SaveFile(string path, string data)
        {
            try
            {
                //var writer = new StreamWriter(path, true);
                //writer.Write(data);
                //writer.Close();
                File.WriteAllText(path, data);
                return true;
            }
            catch
            {
                return false;
            }


        }


        public string GetQrValueFromClearedInvoice(string xmlDataBase64)
        {
            var xmlString = Encoding.UTF8.GetString(Convert.FromBase64String(xmlDataBase64));
            XmlDocument invoiceXml = new XmlDocument();
            invoiceXml.LoadXml(xmlString);
            XmlNode qrNode = invoiceXml.SelectSingleNode(Settings.QR_CODE_XPATH);


            //IEnumerable<XElement> direclty = invoiceXml.Elements("Settings").Elements("directory");
            //var rosterUserIds = direclty.Select(r => r.Attribute("value").Value);
            return qrNode.InnerText;
        }

        public SignerData GenerateKeyPairsAndCSRUsingCode()
        {
            SignerData signerData = new SignerData();

            //ProcessStartInfo startInfo = new ProcessStartInfo(@"D:\OpenSSL\OpenSSL\bin\openssl.exe", @"ecparam -name secp256k1 -genkey -noout -out D:\SaudiaEInvoiceOutput\CSR_And_Keys\PrivateKey.pem");
            //Process.Start(startInfo);
            //_logger.LogInformation("====================================================");
            //Thread.Sleep(1000);
            //startInfo.Arguments = @"ec -in D:\SaudiaEInvoiceOutput\CSR_And_Keys\PrivateKey.pem -pubout -conv_form compressed -out D:\SaudiaEInvoiceOutput\CSR_And_Keys\publicKey.pem";
            //Process.Start(startInfo);
            //Thread.Sleep(1000);

            //_logger.LogInformation("====================================================");
            var ecDsaKeypair = ECDsa.Create(ECCurve.NamedCurves.nistP256);
            StringWriter stringWriter = new StringWriter();
            PemWriter pemWriter = new PemWriter(stringWriter);
            pemWriter.WriteObject((PemObjectGenerator)new PemObject("PRIVATE KEY", ecDsaKeypair.ExportPkcs8PrivateKey())); // SEC1: EC PRIVATE KEY, X.509: PUBLIC KEY
            Console.WriteLine(stringWriter.ToString());


            SaveToFile(stringWriter.ToString(), @"D:\SaudiaEInvoiceOutput\CSR_And_Keys\NewPrivateKey.pem");



            ProcessStartInfo startInfo = new ProcessStartInfo(@"D:\OpenSSL\OpenSSL\bin\openssl.exe", @"req -new -sha256 -key D:\SaudiaEInvoiceOutput\CSR_And_Keys\NewPrivateKey.pem -extensions v3_req -config D:\SaudiaEInvoiceOutput\CSR_And_Keys\crs-config.cnf -out D:\SaudiaEInvoiceOutput\CSR_And_Keys\taxPayer.csr");
            Process.Start(startInfo);
            Thread.Sleep(1000);


            _logger.LogInformation("====================================================");



            var reader = new StreamReader(@"D:\SaudiaEInvoiceOutput\CSR_And_Keys\taxPayer.csr");
            var data = reader.ReadToEnd(); ;
            reader.Close();
            Thread.Sleep(1000);

            //reader = new StreamReader(@"D:\SaudiaEInvoiceOutput\CSR_And_Keys\PrivateKey.pem");
            //var privateKeyString = reader.ReadToEnd();
            //reader.Close();
            //Thread.Sleep(1000);
            signerData.CSR = ConvertStringToBase64(data);
            signerData.EnCodedPrivateKey = stringWriter.ToString().Replace("-----BEGIN PRIVATE KEY-----", "").Replace("-----END PRIVATE KEY-----", "").Trim();
            signerData.PrivateKey = ByteArrayToString(ecDsaKeypair.ExportECPrivateKey());
            signerData.KeyPairs = ecDsaKeypair;
            return signerData;
        }

        public void SaveToFile(string fileData, string fileFullPath)
        {
            StreamWriter wr = new StreamWriter(fileFullPath);
            wr.Write(fileData);
            wr.Close();
        }


        public string ConvertStringToBase64(string data)
        {
            data.Trim();
            byte[] bytes = Encoding.ASCII.GetBytes(data);

            var base64Data = Convert.ToBase64String(bytes);
            return base64Data;
        }

        public ComplianceCertificateResponse IssueComplianceCertificate(string OPT, string base64CSR)
        {
            ComplianceCertificateRequest request = new ComplianceCertificateRequest()
            {
                csr = base64CSR
            };

            var certificateResponse = new ComplianceCertificateResponse();
            string jsonData = JsonConvert.SerializeObject(request);
            HttpContent data = new StringContent(jsonData, Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Add("accept", "application/json");
            _httpClient.DefaultRequestHeaders.Add("OTP", OPT);


            data.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            string pathUrl = _isProduction == true ? EndPoints.ComplicanceProd : EndPoints.Complicance;
            //"e-invoicing/developer-portal/compliance"
            var response = _httpClient.PostAsync(pathUrl, data).GetAwaiter().GetResult();
            string jsonResponse = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            WriteLog("Issue Certificate response: \n" + jsonResponse);
            if (response.IsSuccessStatusCode)
            {
                certificateResponse = JsonConvert.DeserializeObject<ComplianceCertificateResponse>(jsonResponse);
            }


            return certificateResponse;
        }

        private void WriteLog(string msg)
        {
            try
            {
                StreamWriter wr = new StreamWriter(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"Logs\" + Guid.NewGuid().ToString() + ".txt", true);
                wr.Write(msg);
                wr.Close();
            }
            catch (Exception ex)
            {
            }

        }

        //public SignedInvoiceData GetSignerData(string complianceCertificate, string privateKey)
        //{
        //    throw new NotImplementedException();
        //}


        public string CreateSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }


        public byte[] CreateSha256HashByteArray(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                //Array.Reverse(bytes);
                return bytes;

                //// Convert byte array to a string   
                //StringBuilder builder = new StringBuilder();
                //for (int i = 0; i < bytes.Length; i++)
                //{
                //    builder.Append(bytes[i].ToString("x2"));
                //}
                //return builder.ToString();
            }
        }

        public ReportInvoiceRequest GenerateInvoiceRequest(string xmlFile, Guid invoiceGuid, Invoice invoice)
        {
            return new ReportInvoiceRequest()
            {
                uuid = invoiceGuid.ToString(),
                invoice = EncodeToBase64(xmlFile),
                invoiceHash = GeneratePureXmlHash(invoice)
            };
        }

        public string EncodeToBase64(string toEncode)
        {

            byte[] toEncodeAsBytes = ASCIIEncoding.ASCII.GetBytes(toEncode);

            string returnValue = Convert.ToBase64String(toEncodeAsBytes);

            return returnValue;

        }

        public string ByteArrayToString(byte[] bytes)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }


        public string Base64ToString(string base64String)
        {

            var data = Convert.FromBase64String(base64String);
            string utfString = Encoding.UTF8.GetString(data, 0, data.Length);

            return utfString;
        }

        public string GenerateXml(Invoice invoice)
        {
            string xmlFilePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            StreamReader rd = new StreamReader(xmlFilePath + @"\InvoiceXML\InvoiceLine.txt");
            string invoiceLineTempelate = rd.ReadToEnd();
            rd.Close();
            //if (invoice.InvoiceType == InvoiceType.B2B)
            //{
            //    xmlFilePath = xmlFilePath + @"\InvoiceXML\Simplified_Invoice.xml";
            //}
            //else if (invoice.InvoiceType == InvoiceType.B2C)
            //{
            //    xmlFilePath = xmlFilePath + @"\InvoiceXML\B2C.xml";
            //}
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlFilePath);
            string xmlcontents = doc.InnerXml;

            xmlcontents = xmlcontents.Replace("@InvoiceNumber", invoice.InvoiceNumber);
            xmlcontents = xmlcontents.Replace("@InvoiceGuid", invoice.InvoiceGuid.ToString());
            xmlcontents = xmlcontents.Replace("@IssueDate", invoice.IssueDate);
            xmlcontents = xmlcontents.Replace("@IssueTime", invoice.IssueTime);
            xmlcontents = xmlcontents.Replace("@TransactionTypeCode", invoice.TransactionTypeCode);
            xmlcontents = xmlcontents.Replace("@InvoiceTypeCode", invoice.InvoiceTypeCode);
            xmlcontents = xmlcontents.Replace("@Notes", invoice.Notes);
            xmlcontents = xmlcontents.Replace("@CurrencyId", invoice.CurrencyCode);
            xmlcontents = xmlcontents.Replace("@TaxCurrencyId", invoice.TaxCurrencyCode);
            xmlcontents = xmlcontents.Replace("@InvoiceNumberForCreditOrDebitNote", invoice.InvoiceNumberForCreditOrDebitNote);
            xmlcontents = xmlcontents.Replace("@ContractID", invoice.ContractID);
            //xmlcontents = xmlcontents.Replace("@InvoiceCounterValue", invoice.InvoiceCounterValue);
            xmlcontents = xmlcontents.Replace("@Order", invoice.Order);
            xmlcontents = xmlcontents.Replace("@PreviousHashInvoice", invoice.PreviousHashInvoice);
            xmlcontents = xmlcontents.Replace("@QrCode", invoice.QrCode);
            xmlcontents = xmlcontents.Replace("@LineCounts", invoice.LineCounts.ToString());
            xmlcontents = xmlcontents.Replace("@SellerSchemaIdType", invoice.SellerSchemaIdType);
            xmlcontents = xmlcontents.Replace("@SellerSchemaIdValue", invoice.SellerSchemaIdValue);
            xmlcontents = xmlcontents.Replace("@SellerStreetName", invoice.SellerStreetName);
            xmlcontents = xmlcontents.Replace("@SellerAdditionalStreetName", invoice.SellerAdditionalStreetName);
            xmlcontents = xmlcontents.Replace("@SellerBuildingNumber", invoice.SellerBuildingNumber);
            xmlcontents = xmlcontents.Replace("@SellerAdditionalNumber", invoice.SellerAdditionalNumber);
            xmlcontents = xmlcontents.Replace("@SellerCityName", invoice.SellerCityName);
            xmlcontents = xmlcontents.Replace("@SellerPostalCode", invoice.SellerPostalCode);
            xmlcontents = xmlcontents.Replace("@SellerProvince", invoice.SellerProvince);
            xmlcontents = xmlcontents.Replace("@SellerDistrict", invoice.SellerDistrict);
            xmlcontents = xmlcontents.Replace("@SellerCountryCode", invoice.SellerCountryCode);
            xmlcontents = xmlcontents.Replace("@SellerVatNumber", invoice.SellerVatNumber);
            xmlcontents = xmlcontents.Replace("@SellerName", invoice.SellerName);
            xmlcontents = xmlcontents.Replace("@BuyerSchemaIdType", invoice.BuyerSchemaIdType);
            xmlcontents = xmlcontents.Replace("@BuyerSchemaIdValue", invoice.BuyerSchemaIdValue);
            xmlcontents = xmlcontents.Replace("@BuyerStreetAddress", invoice.BuyerStreetName);
            xmlcontents = xmlcontents.Replace("@BuyerAdditionalAddress", invoice.BuyerAdditionalAddress);
            xmlcontents = xmlcontents.Replace("@BuyerBuildingNumber", invoice.BuyerBuildingNumber);
            xmlcontents = xmlcontents.Replace("@BuyerAdditionalNumber", invoice.BuyerAdditionalNumber);
            xmlcontents = xmlcontents.Replace("@BuyerCityName", invoice.BuyerCityName);
            xmlcontents = xmlcontents.Replace("@BuyerPostalCode", invoice.BuyerPostalCode);
            xmlcontents = xmlcontents.Replace("@BuyerProvince", invoice.BuyerProvince);
            xmlcontents = xmlcontents.Replace("@BuyerDistrict", invoice.BuyerDistrict);
            xmlcontents = xmlcontents.Replace("@BuyerCountryCode", invoice.BuyerCountryCode);
            xmlcontents = xmlcontents.Replace("@BuyerVatNumber", invoice.BuyerVatNumber);
            xmlcontents = xmlcontents.Replace("@BuyerName", invoice.BuyerName);
            xmlcontents = xmlcontents.Replace("@SupplyDate", invoice.SupplyDate);
            xmlcontents = xmlcontents.Replace("@SupplyEndDate", invoice.SupplyEndDate);
            xmlcontents = xmlcontents.Replace("@PaymentTypeCode", ((int)invoice.PaymentTypeCode).ToString());
            xmlcontents = xmlcontents.Replace("@ReasonOfIssueDebitOrCreditNote", invoice.ReasonOfIssueDebitOrCreditNote);
            xmlcontents = xmlcontents.Replace("@PaymentTerms", invoice.PaymentTerms);
            xmlcontents = xmlcontents.Replace("@PaymentAccountIdentifier", invoice.PaymentAccountIdentifier);
            xmlcontents = xmlcontents.Replace("@IsDiscountOnDocumentLevel", "false" /*invoice.IsDiscountOnDocumentLevel.ToString()*/);
            xmlcontents = xmlcontents.Replace("@DiscountCurrencyId", invoice.DiscountCurrencyId);
            xmlcontents = xmlcontents.Replace("@DiscountPercent", invoice.DiscountPercent.ToString());
            xmlcontents = xmlcontents.Replace("@BaseAmountCurrencyId", invoice.BaseAmountCurrencyId);
            //xmlcontents = xmlcontents.Replace("@DiscountAmount", invoice.DiscountAmount.ToString());
            xmlcontents = xmlcontents.Replace("@DiscountCurrencyId", invoice.DiscountCurrencyId);
            xmlcontents = xmlcontents.Replace("@DiscountPercent", invoice.DiscountPercent.ToString());
            xmlcontents = xmlcontents.Replace("@BaseAmountCurrencyId", invoice.BaseAmountCurrencyId);
            xmlcontents = xmlcontents.Replace("@DiscountAmount", invoice.DiscountAmount.ToString());
            xmlcontents = xmlcontents.Replace("@DocumentLevelDiscountVatCategoryCode", invoice.DocumentLevelDiscountVatCategoryCode);
            xmlcontents = xmlcontents.Replace("@DocumentLevelDiscountVatRate", invoice.DocumentLevelDiscountVatRate.ToString());
            xmlcontents = xmlcontents.Replace("@SumInoiveLineCurrencyId", invoice.SumInoiveLineCurrencyId);
            xmlcontents = xmlcontents.Replace("@SumInvoiceLineNetAmountWithVAT", invoice.SumInvoiceLineNetAmountWithVAT.ToString());
            xmlcontents = xmlcontents.Replace("@SumOfAllowanceInDocumentLevelCurrencyId", invoice.SumOfAllowanceInDocumentLevelCurrencyId);
            xmlcontents = xmlcontents.Replace("@SumOfAllowanceInDocumentLevel", invoice.SumOfAllowanceInDocumentLevel.ToString());
            xmlcontents = xmlcontents.Replace("@InvoiceTotalAmountWithoutVat", invoice.InvoiceTotalAmountWithoutVat.ToString());
            xmlcontents = xmlcontents.Replace("@InvoiceTotalVatAmount", invoice.InvoiceTotalVatAmount.ToString());
            xmlcontents = xmlcontents.Replace("@InvoiceTotalWithVat", invoice.InvoiceTotalWithVat.ToString());
            xmlcontents = xmlcontents.Replace("@PaidAmount", invoice.PaidAmount.ToString());
            xmlcontents = xmlcontents.Replace("@AmountDueForPayment", invoice.AmountDueForPayment.ToString());
            xmlcontents = xmlcontents.Replace("@TaxableAmount", invoice.TaxableAmount.ToString());
            xmlcontents = xmlcontents.Replace("@VatCategoryTaxAmount", invoice.VatCategoryTaxAmount.ToString());
            xmlcontents = xmlcontents.Replace("@VatCategoryCode", invoice.VatCategoryCode);
            xmlcontents = xmlcontents.Replace("@VatCategoryPercent", invoice.VatCategoryPercent.ToString());
            xmlcontents = xmlcontents.Replace("@TaxExemptionReasonCode", invoice.TaxExemptionReasonCode);
            xmlcontents = xmlcontents.Replace("@TaxExemptionReason", invoice.TaxExemptionReason);
            xmlcontents = xmlcontents.Replace("@AllowenceBaseAmount", invoice.AllowenceBaseAmount.ToString());





            string finalInvoiceLines = "";
            foreach (var item in invoice.InvoiceLines)
            {
                string invoiceLine = invoiceLineTempelate.Replace("@InvoiceLineGuid", item.Guid.ToString());

                invoiceLine = invoiceLine.Replace("@InvoiceLineUnitCode", item.UnitCode);
                invoiceLine = invoiceLine.Replace("@InvoiceLineQty", item.Qty.ToString());
                invoiceLine = invoiceLine.Replace("@CurrencyId", item.CurrencyCode);
                invoiceLine = invoiceLine.Replace("@InvoiceLineNetAmountBeforeVat", item.NetAmountBeforeVat.ToString());
                invoiceLine = invoiceLine.Replace("@InvoiceDiscountIndicator", item.InvoiceDiscountIndicator.ToString());
                invoiceLine = invoiceLine.Replace("@InvoiceLineDiscountPercent", item.InvoiceLineDiscountPercent.ToString());
                invoiceLine = invoiceLine.Replace("@InvoiceLineDiscountAmount", item.InvoiceLineDiscountAmount.ToString());
                invoiceLine = invoiceLine.Replace("@InvoiceLineDiscountBaseAmount", item.InvoiceLineDiscountBaseAmount.ToString());
                invoiceLine = invoiceLine.Replace("@InvoiceLineVatAmount", item.InvoiceLineVatAmount.ToString());
                invoiceLine = invoiceLine.Replace("@InvoiceLineWithVat", item.InvoiceLineWithVat.ToString());
                invoiceLine = invoiceLine.Replace("@InvoiceLineItemName", item.InvoiceLineItemName);
                invoiceLine = invoiceLine.Replace("@ItemBuyerIdentification", item.ItemBuyerIdentification);
                invoiceLine = invoiceLine.Replace("@ItemSellerIdentification", item.ItemSellerIdentification);
                invoiceLine = invoiceLine.Replace("@ItemStandardIdentification", item.ItemStandardIdentification);
                invoiceLine = invoiceLine.Replace("@InvoiceLinePrice", item.InvoiceLinePrice.ToString());
                invoiceLine = invoiceLine.Replace("@InvoiceItemVatCategoryCode", item.InvoiceItemVatCategoryCode);
                invoiceLine = invoiceLine.Replace("@InvoiceLineVatRate", item.InvoiceLineVatRate.ToString());
                invoiceLine = invoiceLine.Replace("@ItemPriceBaseQty", item.ItemPriceBaseQty.ToString());
                invoiceLine = invoiceLine.Replace("@PriceAllownceIndicator", item.PriceAllownceIndicator.ToString());
                invoiceLine = invoiceLine.Replace("@ItemPriceDiscount", item.ItemPriceDiscount.ToString());
                invoiceLine = invoiceLine.Replace("@ItemGrossPrice", item.ItemGrossPrice.ToString());
                finalInvoiceLines += invoiceLine;
            }
            xmlcontents = xmlcontents.Replace("@InvoiceLines", finalInvoiceLines);

            return xmlcontents;
        }


        public string GenerateQr(string sellerName, string vatNo, double vatAmount, double totalWithVat, DateTime invoiceDate, string invoiceHash)
        {

            byte[] sellerNameHex = Encoding.ASCII.GetBytes(sellerName);
            int sellerNameLengthHex = sellerName.Length;

            byte[] vatNoHex = Encoding.ASCII.GetBytes(vatNo);
            int vatNoHexLength = vatNo.Length;

            byte[] dateHex = Encoding.ASCII.GetBytes(invoiceDate.ToUniversalTime().ToString("u").Replace(" ", "T"));
            int dateLengthHex = (invoiceDate.ToUniversalTime().ToString("u").Replace(" ", "T")).Length;

            byte[] totalHex = Encoding.ASCII.GetBytes(String.Format("{0:0.00}", totalWithVat));
            int totalHexLength = String.Format("{0:0.00}", totalWithVat).Length;

            byte[] vatHex = Encoding.ASCII.GetBytes(String.Format("{0:0.00}", vatAmount));
            int vatHexLength = String.Format("{0:0.00}", vatAmount).Length;

            List<Byte> qrHex = new List<byte>();
            qrHex.Add(0x01);
            qrHex.Add((byte)sellerNameLengthHex);
            qrHex.AddRange(sellerNameHex);

            qrHex.Add(0x02);
            qrHex.Add((byte)vatNoHexLength);
            qrHex.AddRange(vatNoHex);

            qrHex.Add(0x03);
            qrHex.Add((byte)dateLengthHex);
            qrHex.AddRange(dateHex);

            qrHex.Add(0x04);
            qrHex.Add((byte)totalHexLength);
            qrHex.AddRange(totalHex);

            qrHex.Add(0x05);
            qrHex.Add((byte)vatHexLength);
            qrHex.AddRange(vatHex);






            return Convert.ToBase64String(qrHex.ToArray());
        }
        public string GetInvoiceXMlBeforeOperations(Invoice invoice)
        {
            string xmlFilePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            StreamReader rd = new StreamReader(xmlFilePath + @"\InvoiceXML\SimplifiedInvoiceLine.txt");
            string invoiceLineTempelate = rd.ReadToEnd();
            rd.Close();
            //if (invoice.InvoiceType == InvoiceType.B2B)
            //{
            //    xmlFilePath = xmlFilePath + @"\InvoiceXML\PureSimplifiedInvoice.xml";
            //}
            //else if (invoice.InvoiceType == InvoiceType.B2C)
            //{
            //    xmlFilePath = xmlFilePath + @"\InvoiceXML\PureSimplifiedInvoice.xml";
            //}
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlFilePath);
            string xmlcontents = doc.InnerXml;
            xmlcontents = xmlcontents.Replace("@InvoiceCode", invoice.InvoiceCode);
            xmlcontents = xmlcontents.Replace("@InvoiceGuid", invoice.InvoiceGuid.ToString());
            xmlcontents = xmlcontents.Replace("@IssueDate", invoice.IssueDate);
            xmlcontents = xmlcontents.Replace("@IssueTime", invoice.IssueTime);
            xmlcontents = xmlcontents.Replace("@InvoiceType", invoice.InvoiceTypeCode);
            xmlcontents = xmlcontents.Replace("@Notes", invoice.Notes);
            xmlcontents = xmlcontents.Replace("@CurrencyCode", invoice.CurrencyCode);
            xmlcontents = xmlcontents.Replace("@InvoiceNumber", invoice.InvoiceNumber);
            xmlcontents = xmlcontents.Replace("@PIH", invoice.PreviousHashInvoice);

            xmlcontents = xmlcontents.Replace("@ComRegNo", invoice.SellerSchemaIdValue);
            xmlcontents = xmlcontents.Replace("@StreetName", invoice.SellerStreetName);
            xmlcontents = xmlcontents.Replace("@BuildingNo", invoice.SellerBuildingNumber);
            xmlcontents = xmlcontents.Replace("@PlotIdentification", invoice.SellerAdditionalNumber);
            xmlcontents = xmlcontents.Replace("@CitySubdivisionName", invoice.SellerDistrict);
            xmlcontents = xmlcontents.Replace("@CityName", invoice.SellerCityName);
            xmlcontents = xmlcontents.Replace("@PostalCode", invoice.SellerPostalCode);
            xmlcontents = xmlcontents.Replace("@CountryCode", invoice.SellerCountryCode);
            xmlcontents = xmlcontents.Replace("@VatRegNo", invoice.SellerVatNumber);
            xmlcontents = xmlcontents.Replace("@CompanyName", invoice.SellerName);
            xmlcontents = xmlcontents.Replace("@CustomerCity", "Dammam");
            xmlcontents = xmlcontents.Replace("@ZATCAPaymentMethods", ((int)invoice.PaymentTypeCode).ToString());
            xmlcontents = xmlcontents.Replace("@DiscountIndicator", "false");
            xmlcontents = xmlcontents.Replace("@DiscountNotes", "");
            xmlcontents = xmlcontents.Replace("@DiscountAmount", "0.00");
            xmlcontents = xmlcontents.Replace("@DiscountCurrencyCode", invoice.DiscountCurrencyId);
            xmlcontents = xmlcontents.Replace("@TaxPercent", invoice.VatCategoryPercent);
            xmlcontents = xmlcontents.Replace("@TaxAmount", invoice.VatCategoryTaxAmount);
            xmlcontents = xmlcontents.Replace("@TaxableAmount", invoice.TaxableAmount);
            xmlcontents = xmlcontents.Replace("@TotalInvoiceLineBeforeVAT", invoice.InvoiceTotalAmountWithoutVat);
            xmlcontents = xmlcontents.Replace("@TotalTaxableInvoiceLine", invoice.InvoiceTotalAmountWithoutVat);
            xmlcontents = xmlcontents.Replace("@TotalInvoiceLineWithVat", invoice.InvoiceTotalWithVat);
            xmlcontents = xmlcontents.Replace("@TotalDiscountInvoiceLine", "0.00");
            xmlcontents = xmlcontents.Replace("@PrepaidAmount", "0.00");
            xmlcontents = xmlcontents.Replace("@PayableAmount", invoice.InvoiceTotalWithVat);
            string finalInvoiceLines = "";
            foreach (var item in invoice.InvoiceLines)
            {
                string invoiceLine = invoiceLineTempelate.Replace("@InvoiceLineNumber", item.Number.ToString());

                invoiceLine = invoiceLine.Replace("@UnitName", item.UnitCode);
                invoiceLine = invoiceLine.Replace("@Qty", item.Qty.ToString());
                invoiceLine = invoiceLine.Replace("@CurrencyCode", item.CurrencyCode);
                invoiceLine = invoiceLine.Replace("@TotalBeforeVAT", item.NetAmountBeforeVat.ToString());
                invoiceLine = invoiceLine.Replace("@LineTaxAmount", item.InvoiceLineVatAmount.ToString());
                invoiceLine = invoiceLine.Replace("@TotalWithVat", item.InvoiceLineWithVat.ToString());
                invoiceLine = invoiceLine.Replace("@ItemName", item.InvoiceLineItemName.ToString());
                invoiceLine = invoiceLine.Replace("@LineTaxPercent", item.InvoiceLineVatRate.ToString());
                invoiceLine = invoiceLine.Replace("@Price", item.InvoiceLinePrice.ToString());
                invoiceLine = invoiceLine.Replace("@Price", item.InvoiceLinePrice.ToString());
                invoiceLine = invoiceLine.Replace("@InvoiceLineDiscountIndicator", "false");
                invoiceLine = invoiceLine.Replace("@InvoiceLineDiscountNotes", "discount");
                invoiceLine = invoiceLine.Replace("@LineDiscount", "0.00");
                finalInvoiceLines += invoiceLine;
            }
            xmlcontents = xmlcontents.Replace("@InvoiceLines", finalInvoiceLines);
            xmlcontents = xmlcontents.Replace("<cbc:ProfileID>", "\n    <cbc:ProfileID>");
            xmlcontents = xmlcontents.Replace("<cac:AccountingSupplierParty>", "\n    \n    <cac:AccountingSupplierParty>");
            return xmlcontents;
        }
        public string GeneratePureXmlHash(Invoice invoice)
        {

            var xmlcontents = GetInvoiceXMlBeforeOperations(invoice);
            xmlcontents = xmlcontents.Replace("@QrTag", "");
            xmlcontents = xmlcontents.Replace("@SignatureTag", "");
            xmlcontents = xmlcontents.Replace("@DigitalSignature", "");
            //var hashedPureInvoice = CreateSha256Hash(xmlcontents);
            var hashedPureInvoice = CreateHash(xmlcontents);
            return hashedPureInvoice;
        }


        public byte[] GeneratePureXmlHashArray(Invoice invoice)
        {

            var xmlcontents = GetInvoiceXMlBeforeOperations(invoice);
            xmlcontents = xmlcontents.Replace("@QrTag", "");
            xmlcontents = xmlcontents.Replace("@SignatureTag", "");
            xmlcontents = xmlcontents.Replace("@DigitalSignature", "");
            //var hashedPureInvoice = CreateSha256HashByteArray(xmlcontents);
            var hashedPureInvoice = CreateHashByteArray(xmlcontents);
            return hashedPureInvoice;
        }

        public string GetCleanCertificat(string cerificate)
        {
            //var decodedCert = Base64ToString(cerificate);
            var decodedCert = cerificate;
            decodedCert = decodedCert.Replace("-----BEGIN CERTIFICATE-----", "");
            decodedCert = decodedCert.Replace("-----END CERTIFICATE-----", "");
            decodedCert = decodedCert.Replace("\r", "");
            decodedCert = decodedCert.Replace("\n", "");
            decodedCert = decodedCert.Trim();
            return decodedCert;
        }


        public string GetCleanPrivateKey(string privateKey)
        {

            privateKey = privateKey.Replace("-----BEGIN EC PRIVATE KEY-----", "");
            privateKey = privateKey.Replace("-----END EC PRIVATE KEY-----", "");
            privateKey = privateKey.Replace("\r", "");
            privateKey = privateKey.Replace("\n", "");
            privateKey = privateKey.Trim();
            return privateKey;
        }

        public string GetWrapedCertificat(string cleanCertificate)
        {
            return "-----BEGIN CERTIFICATE-----\n" + cleanCertificate + "\n-----END CERTIFICATE-----";
        }

        public X509Certificate GenerateX509Certificate(string wrapedCertificate)
        {
            return new X509Certificate(Encoding.ASCII.GetBytes(wrapedCertificate));
        }

        public X509Certificate2 GenerateCertificate(string wrapedCertificate)
        {
            return new X509Certificate2(Encoding.ASCII.GetBytes(wrapedCertificate));
        }

        public string CreateInvoiceDigitalSignature(string invoiceHash, string privateKey)
        {

            // encoding my privateKey from string to byte[] by using DecodeOpenSSLPrivateKey function from OpenSSLKey source code
            byte[] pemprivatekey = DecodeOpenSSLPrivateKey(privateKey);

            // enconding my string to sign in byte[]
            byte[] byteSign = Encoding.ASCII.GetBytes(invoiceHash);

            // using DecodeRSAPrivateKey function from OpenSSLKey source code to get the RSACryptoServiceProvider with all needed parameters
            var rsa = DecodeRSAPrivateKey(pemprivatekey);

            // Signing my string with previously get RSACryptoServiceProvider in SHA256
            var byteRSA = rsa.SignData(byteSign, CryptoConfig.MapNameToOID("SHA256"));

            // As required by docs converting the signed string to base64
            string Signature = Convert.ToBase64String(byteRSA);
            return Signature;

        }


        public byte[] DecodeOpenSSLPrivateKey(string instr)
        {
            const String pemprivheader = "-----BEGIN EC PRIVATE KEY-----";
            const String pemprivfooter = "-----END EC PRIVATE KEY-----";
            String pemstr = instr.Trim();
            byte[] binkey;
            if (!pemstr.StartsWith(pemprivheader) || !pemstr.EndsWith(pemprivfooter))
                return null;

            StringBuilder sb = new StringBuilder(pemstr);
            sb.Replace(pemprivheader, "");  //remove headers/footers, if present
            sb.Replace(pemprivfooter, "");

            String pvkstr = sb.ToString().Trim();   //get string after removing leading/trailing whitespace

            try
            {        // if there are no PEM encryption info lines, this is an UNencrypted PEM private key
                binkey = Convert.FromBase64String(pvkstr);
                return binkey;
            }
            catch (System.FormatException)
            {       //if can't b64 decode, it must be an encrypted private key
                    //Console.WriteLine("Not an unencrypted OpenSSL PEM private key");  
            }

            StringReader str = new StringReader(pvkstr);

            //-------- read PEM encryption info. lines and extract salt -----
            if (!str.ReadLine().StartsWith("Proc-Type: 4,ENCRYPTED"))
                return null;
            String saltline = str.ReadLine();
            if (!saltline.StartsWith("DEK-Info: DES-EDE3-CBC,"))
                return null;
            String saltstr = saltline.Substring(saltline.IndexOf(",") + 1).Trim();
            byte[] salt = new byte[saltstr.Length / 2];
            for (int i = 0; i < salt.Length; i++)
                salt[i] = Convert.ToByte(saltstr.Substring(i * 2, 2), 16);
            if (!(str.ReadLine() == ""))
                return null;

            //------ remaining b64 data is encrypted RSA key ----
            String encryptedstr = str.ReadToEnd();

            try
            {   //should have b64 encrypted RSA key now
                binkey = Convert.FromBase64String(encryptedstr);
            }
            catch (System.FormatException)
            {  // bad b64 data.
                return null;
            }

            //------ Get the 3DES 24 byte key using PDK used by OpenSSL ----

            SecureString despswd = GetSecPswd("Enter password to derive 3DES key==>");
            //Console.Write("\nEnter password to derive 3DES key: ");
            //String pswd = Console.ReadLine();
            byte[] deskey = GetOpenSSL3deskey(salt, despswd, 1, 2);    // count=1 (for OpenSSL implementation); 2 iterations to get at least 24 bytes
            if (deskey == null)
                return null;
            //showBytes("3DES key", deskey) ;

            //------ Decrypt the encrypted 3des-encrypted RSA private key ------
            byte[] rsakey = DecryptKey(binkey, deskey, salt);   //OpenSSL uses salt value in PEM header also as 3DES IV
            if (rsakey != null)
                return rsakey;  //we have a decrypted RSA private key
            else
            {
                Console.WriteLine("Failed to decrypt RSA private key; probably wrong password.");
                return null;
            }
        }

        public RSACryptoServiceProvider DecodeRSAPrivateKey(byte[] privkey)
        {
            byte[] MODULUS, E, D, P, Q, DP, DQ, IQ;

            // ---------  Set up stream to decode the asn.1 encoded RSA private key  ------
            MemoryStream mem = new MemoryStream(privkey);
            BinaryReader binr = new BinaryReader(mem);    //wrap Memory Stream with BinaryReader for easy reading
            byte bt = 0;
            ushort twobytes = 0;
            int elems = 0;
            try
            {
                twobytes = binr.ReadUInt16();
                if (twobytes == 0x8130) //data read as little endian order (actual data order for Sequence is 30 81)
                    binr.ReadByte();        //advance 1 byte
                else if (twobytes == 0x8230)
                    binr.ReadInt16();       //advance 2 bytes
                else
                    return null;

                twobytes = binr.ReadUInt16();
                if (twobytes != 0x0102) //version number
                    return null;
                bt = binr.ReadByte();
                if (bt != 0x00)
                    return null;


                //------  all private key components are Integer sequences ----
                elems = GetIntegerSize(binr);
                MODULUS = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                E = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                D = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                P = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                Q = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                DP = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                DQ = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                IQ = binr.ReadBytes(elems);

                // ------- create RSACryptoServiceProvider instance and initialize with public key -----
                RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
                RSAParameters RSAparams = new RSAParameters();
                RSAparams.Modulus = MODULUS;
                RSAparams.Exponent = E;
                RSAparams.D = D;
                RSAparams.P = P;
                RSAparams.Q = Q;
                RSAparams.DP = DP;
                RSAparams.DQ = DQ;
                RSAparams.InverseQ = IQ;
                RSA.ImportParameters(RSAparams);
                return RSA;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                binr.Close();
            }
        }


        public int GetIntegerSize(BinaryReader binr)
        {
            byte bt = 0;
            byte lowbyte = 0x00;
            byte highbyte = 0x00;
            int count = 0;
            bt = binr.ReadByte();
            if (bt != 0x02)     //expect integer
                return 0;
            bt = binr.ReadByte();

            if (bt == 0x81)
                count = binr.ReadByte();    // data size in next byte
            else
            if (bt == 0x82)
            {
                highbyte = binr.ReadByte(); // data size in next 2 bytes
                lowbyte = binr.ReadByte();
                byte[] modint = { lowbyte, highbyte, 0x00, 0x00 };
                count = BitConverter.ToInt32(modint, 0);
            }
            else
            {
                count = bt;     // we already have the data size
            }



            while (binr.ReadByte() == 0x00)
            {   //remove high order zeros in data
                count -= 1;
            }
            binr.BaseStream.Seek(-1, SeekOrigin.Current);       //last ReadByte wasn't a removed zero, so back up a byte
            return count;
        }


        private SecureString GetSecPswd(String prompt)
        {
            SecureString password = new SecureString();

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(prompt);
            Console.ForegroundColor = ConsoleColor.Magenta;

            while (true)
            {
                ConsoleKeyInfo cki = Console.ReadKey(true);
                if (cki.Key == ConsoleKey.Enter)
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine();
                    return password;
                }
                else if (cki.Key == ConsoleKey.Backspace)
                {
                    // remove the last asterisk from the screen...
                    if (password.Length > 0)
                    {
                        Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                        Console.Write(" ");
                        Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                        password.RemoveAt(password.Length - 1);
                    }
                }
                else if (cki.Key == ConsoleKey.Escape)
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine();
                    return password;
                }
                else if (Char.IsLetterOrDigit(cki.KeyChar) || Char.IsSymbol(cki.KeyChar))
                {
                    if (password.Length < 20)
                    {
                        password.AppendChar(cki.KeyChar);
                        Console.Write("*");
                    }
                    else
                    {
                        Console.Beep();
                    }
                }
                else
                {
                    Console.Beep();
                }
            }
        }

        private byte[] GetOpenSSL3deskey(byte[] salt, SecureString secpswd, int count, int miter)
        {
            IntPtr unmanagedPswd = IntPtr.Zero;
            int HASHLENGTH = 16;    //MD5 bytes
            byte[] keymaterial = new byte[HASHLENGTH * miter];     //to store contatenated Mi hashed results


            byte[] psbytes = new byte[secpswd.Length];
            unmanagedPswd = Marshal.SecureStringToGlobalAllocAnsi(secpswd);
            Marshal.Copy(unmanagedPswd, psbytes, 0, psbytes.Length);
            Marshal.ZeroFreeGlobalAllocAnsi(unmanagedPswd);

            //UTF8Encoding utf8 = new UTF8Encoding();
            //byte[] psbytes = utf8.GetBytes(pswd);

            // --- contatenate salt and pswd bytes into fixed data array ---
            byte[] data00 = new byte[psbytes.Length + salt.Length];
            Array.Copy(psbytes, data00, psbytes.Length);        //copy the pswd bytes
            Array.Copy(salt, 0, data00, psbytes.Length, salt.Length);   //concatenate the salt bytes

            // ---- do multi-hashing and contatenate results  D1, D2 ...  into keymaterial bytes ----
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = null;
            byte[] hashtarget = new byte[HASHLENGTH + data00.Length];   //fixed length initial hashtarget

            for (int j = 0; j < miter; j++)
            {
                // ----  Now hash consecutively for count times ------
                if (j == 0)
                    result = data00;    //initialize 
                else
                {
                    Array.Copy(result, hashtarget, result.Length);
                    Array.Copy(data00, 0, hashtarget, result.Length, data00.Length);
                    result = hashtarget;
                    //Console.WriteLine("Updated new initial hash target:") ;
                    //showBytes(result) ;
                }

                for (int i = 0; i < count; i++)
                    result = md5.ComputeHash(result);
                Array.Copy(result, 0, keymaterial, j * HASHLENGTH, result.Length);  //contatenate to keymaterial
            }
            //showBytes("Final key material", keymaterial);
            byte[] deskey = new byte[24];
            Array.Copy(keymaterial, deskey, deskey.Length);

            Array.Clear(psbytes, 0, psbytes.Length);
            Array.Clear(data00, 0, data00.Length);
            Array.Clear(result, 0, result.Length);
            Array.Clear(hashtarget, 0, hashtarget.Length);
            Array.Clear(keymaterial, 0, keymaterial.Length);

            return deskey;
        }


        public byte[] DecryptKey(byte[] cipherData, byte[] desKey, byte[] IV)
        {
            MemoryStream memst = new MemoryStream();
            TripleDES alg = TripleDES.Create();
            alg.Key = desKey;
            alg.IV = IV;
            try
            {
                CryptoStream cs = new CryptoStream(memst, alg.CreateDecryptor(), CryptoStreamMode.Write);
                cs.Write(cipherData, 0, cipherData.Length);
                cs.Close();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
                return null;
            }
            byte[] decryptedData = memst.ToArray();
            return decryptedData;
        }

        public string GenerateQR(Invoice invoice, string publicKey, string digitalSignature, string invoiceHash, string certificateSignature)
        {

            byte[] sellerNameHex = Encoding.ASCII.GetBytes(invoice.SellerName);
            int sellerNameLengthHex = invoice.SellerName.Length;

            byte[] vatNoHex = Encoding.ASCII.GetBytes(invoice.SellerVatNumber);
            int vatNoHexLength = invoice.SellerVatNumber.Length;

            byte[] dateHex = Encoding.ASCII.GetBytes(invoice.InvoiceDateTime.ToUniversalTime().ToString("u").Replace(" ", "T"));
            int dateLengthHex = (invoice.InvoiceDateTime.ToUniversalTime().ToString("u").Replace(" ", "T")).Length;

            byte[] totalHex = Encoding.ASCII.GetBytes(String.Format("{0:0.00}", invoice.InvoiceTotalWithVat));
            int totalHexLength = String.Format("{0:0.00}", invoice.InvoiceTotalWithVat).Length;

            byte[] vatHex = Encoding.ASCII.GetBytes(String.Format("{0:0.00}", invoice.InvoiceTotalVatAmount));
            int vatHexLength = String.Format("{0:0.00}", invoice.InvoiceTotalVatAmount).Length;

            byte[] invoiceHashHex = Encoding.ASCII.GetBytes(invoiceHash);
            int invoiceHashLength = invoiceHash.Length;


            byte[] digitalSignatureHex = Encoding.ASCII.GetBytes(digitalSignature);
            int digitalSignatureLength = digitalSignature.Length;

            byte[] publicKeyHex = Encoding.ASCII.GetBytes(publicKey);
            int publicKeyLength = publicKey.Length;

            byte[] certificateSignatureHex = Encoding.ASCII.GetBytes(certificateSignature);
            int certificateSignatureLength = certificateSignature.Length;

            List<Byte> qrHex = new List<byte>();
            qrHex.Add(0x01);
            qrHex.Add((byte)sellerNameLengthHex);
            qrHex.AddRange(sellerNameHex);

            qrHex.Add(0x02);
            qrHex.Add((byte)vatNoHexLength);
            qrHex.AddRange(vatNoHex);

            qrHex.Add(0x03);
            qrHex.Add((byte)dateLengthHex);
            qrHex.AddRange(dateHex);

            qrHex.Add(0x04);
            qrHex.Add((byte)totalHexLength);
            qrHex.AddRange(totalHex);

            qrHex.Add(0x05);
            qrHex.Add((byte)vatHexLength);
            qrHex.AddRange(vatHex);

            var invoiceHashLengthBytes = BitConverter.GetBytes(invoiceHashLength);
            Array.Reverse(invoiceHashLengthBytes);


            qrHex.Add(0x06);
            qrHex.AddRange(invoiceHashLengthBytes);
            qrHex.AddRange(invoiceHashHex);

            qrHex.Add(0x07);
            qrHex.Add((byte)digitalSignatureLength);
            qrHex.AddRange(digitalSignatureHex);

            qrHex.Add(0x08);
            qrHex.Add((byte)publicKeyLength);
            qrHex.AddRange(publicKeyHex);

            qrHex.Add(0x09);
            qrHex.Add((byte)certificateSignatureLength);
            qrHex.AddRange(certificateSignatureHex);


            return Convert.ToBase64String(qrHex.ToArray());


        }



        public string GenerateQR(Invoice invoice, byte[] publicKey, string digitalSignature, byte[] invoiceHash, byte[] certificateSignature)
        {
            demo(invoiceHash);
            CultureInfo culture = new CultureInfo("en-US");
            var invoiceDate = invoice.InvoiceDateTime.ToString("yyyy-MM-ddTHH:mm:ss", culture) + "Z";
            byte[] sellerNameHex = Encoding.ASCII.GetBytes(invoice.SellerName);
            int sellerNameLengthHex = invoice.SellerName.Length;
            byte[] vatNoHex = Encoding.ASCII.GetBytes(invoice.SellerVatNumber);
            int vatNoHexLength = invoice.SellerVatNumber.Length;
            byte[] dateHex = Encoding.ASCII.GetBytes(invoiceDate);
            int dateLengthHex = invoiceDate.Length;
            byte[] totalHex = Encoding.ASCII.GetBytes(String.Format("{0:0.00}", invoice.InvoiceTotalWithVat));
            int totalHexLength = String.Format("{0:0.00}", invoice.InvoiceTotalWithVat).Length;
            byte[] vatHex = Encoding.ASCII.GetBytes(String.Format("{0:0.00}", invoice.InvoiceTotalVatAmount));
            int vatHexLength = String.Format("{0:0.00}", invoice.InvoiceTotalVatAmount).Length;


            byte[] digitalSignatureHex = Encoding.ASCII.GetBytes(digitalSignature);
            int digitalSignatureLength = digitalSignature.Length;



            List<Byte> qrHex = new List<byte>();
            qrHex.Add(0x01);
            qrHex.Add((byte)sellerNameLengthHex);
            qrHex.AddRange(sellerNameHex);

            qrHex.Add(0x02);
            qrHex.Add((byte)vatNoHexLength);
            qrHex.AddRange(vatNoHex);

            qrHex.Add(0x03);
            qrHex.Add((byte)dateLengthHex);
            qrHex.AddRange(dateHex);

            qrHex.Add(0x04);
            qrHex.Add((byte)totalHexLength);
            qrHex.AddRange(totalHex);

            qrHex.Add(0x05);
            qrHex.Add((byte)vatHexLength);
            qrHex.AddRange(vatHex);

            qrHex.Add(0x06);

            //var x = Convert.ToBase64String(invoiceHash);
            //var y = Encoding.UTF8.GetBytes(x);
            qrHex.Add((byte)invoiceHash.Length);
            qrHex.AddRange(invoiceHash);

            qrHex.Add(0x07);
            qrHex.Add((byte)digitalSignatureLength);
            qrHex.AddRange(digitalSignatureHex);

            qrHex.Add(0x08);
            qrHex.Add((byte)publicKey.Length);
            qrHex.AddRange(publicKey);

            qrHex.Add(0x09);
            qrHex.Add((byte)certificateSignature.Length);
            qrHex.AddRange(certificateSignature);


            return Convert.ToBase64String(qrHex.ToArray());


        }

        public SignedPropertiesData GetSignedProperties(SignedProperties properties)
        {
            string xmlFilePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            StreamReader rd = new StreamReader(xmlFilePath + @"\InvoiceXML\SignedProperties.txt");
            string signedPropertiesTempelate = rd.ReadToEnd();
            rd.Close();

            signedPropertiesTempelate = signedPropertiesTempelate.Replace("@SigningTime", properties.SignTimeStamp);
            signedPropertiesTempelate = signedPropertiesTempelate.Replace("@CertificateHash", properties.CertificateHash);
            signedPropertiesTempelate = signedPropertiesTempelate.Replace("@CertificateIssuer", properties.CertificateIssuer);
            signedPropertiesTempelate = signedPropertiesTempelate.Replace("@CertificateSerialNumber", properties.CertificateSerialNumber);

            return new SignedPropertiesData()
            {
                SignedPropertiesXMLString = signedPropertiesTempelate,
                SignedPropertiesHash = ConvertStringToBase64(CreateSha256Hash(signedPropertiesTempelate))
            };




        }


        public string GetFullSignature(SignedPropertiesData signedProperties, string invoiceHash, string digitalSignature, string certificateString)
        {
            string xmlFilePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            StreamReader rd = new StreamReader(xmlFilePath + @"\InvoiceXML\FullSignature.txt");
            string fullSignTempelate = rd.ReadToEnd();
            rd.Close();

            fullSignTempelate = fullSignTempelate.Replace("@InvoiceHash", invoiceHash);
            fullSignTempelate = fullSignTempelate.Replace("@SignedPropertiesHash", signedProperties.SignedPropertiesHash);
            fullSignTempelate = fullSignTempelate.Replace("@DigitalSignature", digitalSignature);
            fullSignTempelate = fullSignTempelate.Replace("@Certificate", certificateString);
            fullSignTempelate = fullSignTempelate.Replace("@SignedPropetiesXml", signedProperties.SignedPropertiesXMLString);
            return fullSignTempelate;




        }

        public string GenerateFullXml(Invoice invoice, string Qr, string fullSignature)
        {
            string xmlFilePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var invoiceLineTempelate = GetInvoiceXMlBeforeOperations(invoice);
            StreamReader rd = new StreamReader(xmlFilePath + @"\InvoiceXML\SignatureTag.txt");
            string signatureTag = rd.ReadToEnd();
            rd.Close();
            rd = new StreamReader(xmlFilePath + @"\InvoiceXML\QrTag.txt");
            string qrTag = rd.ReadToEnd();
            rd.Close();
            qrTag = qrTag.Replace("@QRData", Qr);

            invoiceLineTempelate = invoiceLineTempelate.Replace("@DigitalSignature", fullSignature);
            invoiceLineTempelate = invoiceLineTempelate.Replace("@QrTag", qrTag);
            invoiceLineTempelate = invoiceLineTempelate.Replace("@SignatureTag", signatureTag);

            return invoiceLineTempelate;

        }





        public string GetSignature(AsymmetricKeyParameter privateKeyParameter, string invoiceHash)
        {
            var signer = SignerUtilities.GetSigner("SHA-256withECDSA");

            signer.Init(true, privateKeyParameter);

            signer.BlockUpdate(Encoding.ASCII.GetBytes(invoiceHash), 0, Encoding.ASCII.GetBytes(invoiceHash).Length);

            var signature = signer.GenerateSignature();


            return ByteArrayToString(signature);

        }

        public AsymmetricCipherKeyPair ReadAsymmetricCipherKeyPairFromPem(string pathToPem)
        {
            using var reader = File.OpenText(pathToPem);
            var keyPair = (AsymmetricCipherKeyPair)new PemReader(reader).ReadObject();

            return keyPair;
        }

        public byte[] Sha256_hashAsBytes(string value)
        {
            using (SHA256 shA256 = SHA256.Create())
            {
                StringBuilder stringBuilder = new StringBuilder();
                Encoding utF8 = Encoding.UTF8;
                return shA256.ComputeHash(utF8.GetBytes(value));
            }
        }

        public string ToBase64Encode(byte[] value)
        {
            return (value == null ? (string)null : Convert.ToBase64String(value));
        }

        public string CreateHash(string xmlString)
        {



            using (MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(xmlString)))
            {
                XmlDsigC14NTransform dsigC14Ntransform = new XmlDsigC14NTransform(false);
                dsigC14Ntransform.LoadInput((object)memoryStream);
                sbyte[] array = ((IEnumerable<byte>)Sha256_hashAsBytes(Encoding.UTF8.GetString((dsigC14Ntransform.GetOutput() as MemoryStream).ToArray()))).Select<byte, sbyte>((Func<byte, sbyte>)(x => (sbyte)x)).ToArray<sbyte>();
                return ToBase64Encode((byte[])(object)array);

            }



        }


        public byte[] CreateHashByteArray(string xmlString)
        {




            using (MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(xmlString)))
            {
                XmlDsigC14NTransform dsigC14Ntransform = new XmlDsigC14NTransform(false);
                dsigC14Ntransform.LoadInput((object)memoryStream);
                sbyte[] array = ((IEnumerable<byte>)Sha256_hashAsBytes(Encoding.UTF8.GetString((dsigC14Ntransform.GetOutput() as MemoryStream).ToArray()))).Select<byte, sbyte>((Func<byte, sbyte>)(x => (sbyte)x)).ToArray<sbyte>();
                return (byte[])(object)array;
            }



        }

        public string SignInvoice(string dataToSignString, ECDsa ecDsaKeypair)
        {
            var signedInvoiceResult = "";
            //var dataToSign = Encoding.UTF8.GetBytes(dataToSignString);

            //Console.WriteLine("dataToSign: " + dataToSignString);

            try
            {
                Console.WriteLine("\n* * * sign the plaintext with the EC private key * * *");



                // Normally here:
                //ecDsaKeypair.ImportFromPem()

                Console.WriteLine("EC keysize: " + ecDsaKeypair.KeySize);

                byte[] hashedData = null;
                byte[] signature = null;
                // create new instance of SHA256 hash algorithm to compute hash
                //HashAlgorithm hashAlgo = new SHA256Managed();
                //hashedData = hashAlgo.ComputeHash(dataToSign);

                hashedData = CreateHashByteArray(dataToSignString);

                // sign Data using private key
                signature = ecDsaKeypair.SignHash(hashedData);
                string signatureBase64 = Convert.ToBase64String(signature);
                Console.WriteLine("signature (Base64): " + signatureBase64);
                signedInvoiceResult = signatureBase64;


                // get public key from private key
                string ecDsaPublicKeyParameters = Convert.ToBase64String(ecDsaKeypair.ExportSubjectPublicKeyInfo());

                // verify
                Console.WriteLine("\n* * *verify the signature against hash of plaintext with the EC public key * * *");

                var ecDsaVerify = ECDsa.Create(ECCurve.NamedCurves.nistP256);
                bool signatureVerified = false;

                // Normally here:
                //ecDsaKeypair.ImportFromPem()
                var publicKey = Convert.FromBase64String(ecDsaPublicKeyParameters);
                ecDsaVerify.ImportSubjectPublicKeyInfo(publicKey, out _);

                signatureVerified = ecDsaVerify.VerifyHash(hashedData, signature);
                Console.WriteLine("signature verified: " + signatureVerified);
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("The data was not signed or verified");
            }

            return signedInvoiceResult;
        }

        public string ReportSingeInvoice(string invoiceXml, string securtyToken, string password, string guid)
        {
            ReportInvoiceRequest request = new ReportInvoiceRequest()
            {
                uuid = guid,
                invoice = ConvertStringToBase64(invoiceXml),
                invoiceHash = ConvertStringToBase64(CreateSha256Hash(invoiceXml)),

            };
            var auth = "Basic " + btoa(securtyToken + ":" + password);
            var reportInvoiceResponse = new InvoiceReportResponse();
            string jsonData = JsonConvert.SerializeObject(request);
            HttpContent data = new StringContent(jsonData, Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Add("accept", "application/json");
            _httpClient.DefaultRequestHeaders.Add("accept-language", "en");
            _httpClient.DefaultRequestHeaders.Add("Clearance-Status", "0");
            _httpClient.DefaultRequestHeaders.Add("Authorization", auth);

            data.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            string pathUrl = _isProduction == true ? EndPoints.ReportSimpleSingleInvoiceProd : EndPoints.ReportSimpleSingleInvoice;

            var response = _httpClient.PostAsync(pathUrl, data).GetAwaiter().GetResult();
            string jsonResponse = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            _eInvoiceResponseLogRepos.AddLog(new Models.EInvoiceResponseLog()
            {
                ApiResponse = jsonResponse,
                StatusCode = (int)response.StatusCode,
                InvGuid = new Guid(),
                Operation = "ReportSingeInvoice",
                OperationDate = DateTime.Now,
                ExtApiUrl = pathUrl

            });
            WriteLog("Simple Invoice Report Result is: " + jsonResponse);
            return jsonResponse;
        }

        private string btoa(string toEncode)
        {
            byte[] bytes = Encoding.GetEncoding(28591).GetBytes(toEncode);
            string toReturn = System.Convert.ToBase64String(bytes);
            return toReturn;
        }

        public string GenerateFullXmlWithoutQr(Invoice invoice, string fullSignature)
        {
            string xmlFilePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var invoiceLineTempelate = GetInvoiceXMlBeforeOperations(invoice);
            StreamReader rd = new StreamReader(xmlFilePath + @"\InvoiceXML\SignatureTag.txt");
            string signatureTag = rd.ReadToEnd();
            rd.Close();
            invoiceLineTempelate = invoiceLineTempelate.Replace("@DigitalSignature", fullSignature);
            invoiceLineTempelate = invoiceLineTempelate.Replace("@QrTag", "");
            invoiceLineTempelate = invoiceLineTempelate.Replace("@SignatureTag", signatureTag);

            return invoiceLineTempelate;

        }




        public string ConvertHashStringToBase64(string hashString)
        {
            //string text = "cd69ef0284bba33bc0b320e6479c2da2d411a5e46af060d8639f0e0bfc24f26d";
            byte[] data = ParseHex(hashString);
            var base64String = Convert.ToBase64String(data);
            Console.WriteLine(base64String);
            return base64String;
        }

        // Taken from https://stackoverflow.com/questions/795027/code-golf-hex-to-raw-binary-conversion/795036#795036
        private byte[] ParseHex(string text)
        {
            Func<char, int> parseNybble = c => (c >= '0' && c <= '9') ? c - '0' : char.ToLower(c) - 'a' + 10;
            return Enumerable.Range(0, text.Length / 2)
                .Select(x => (byte)((parseNybble(text[x * 2]) << 4) | parseNybble(text[x * 2 + 1])))
                .ToArray();
        }

        private void demo(byte[] arr)
        {
            StringBuilder Sb = new StringBuilder();
            foreach (Byte b in arr)
            {
                Sb.Append(b.ToString("x2"));
            }
            var x = Sb.ToString();
            var base64 = ConvertStringToBase64(x);
            Console.WriteLine(base64);
            var d = Convert.ToBase64String(arr);
        }









        public string GetDigitalSignature(string xmlHashing, string privateKeyContent)
        {
            string digitalSignature = "";
            try
            {
                sbyte[] array = ((IEnumerable<byte>)ToBase64DecodeAsBinary(xmlHashing)).Select<byte, sbyte>((Func<byte, sbyte>)(x => (sbyte)x)).ToArray<sbyte>();
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

                digitalSignature = Convert.ToBase64String(signature);
            }
            catch
            {
                digitalSignature = "";
            }
            return digitalSignature;
        }


        public byte[] ToBase64DecodeAsBinary(string base64EncodedText)
        {
            return string.IsNullOrEmpty(base64EncodedText) ? (byte[])null : Convert.FromBase64String(base64EncodedText);
        }

        public string SignXmlInvoice(Invoice invoice, string certificateContent, string privateKeyContent)
        {

            try
            {
                var xmlcontents = GetInvoiceXMlBeforeOperations(invoice);
                xmlcontents = xmlcontents.Replace("@QrTag", "");
                xmlcontents = xmlcontents.Replace("@SignatureTag", "");
                xmlcontents = xmlcontents.Replace("@DigitalSignature", "");

                string einvoiceHashing = CreateHash(xmlcontents);
                string digitalSignature = GetDigitalSignature(einvoiceHashing, privateKeyContent);
                string certificateHash = "";





                X509Certificate2 x509Cert = new X509Certificate2((byte[])(object)((IEnumerable<byte>)Encoding.UTF8.GetBytes(certificateContent)).Select<byte, sbyte>((Func<byte, sbyte>)(x => (sbyte)x)).ToArray<sbyte>());
                Org.BouncyCastle.X509.X509Certificate x509Certificate = DotNetUtilities.FromX509Certificate((System.Security.Cryptography.X509Certificates.X509Certificate)x509Cert);
                sbyte[] array = ((IEnumerable<byte>)SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(x509Certificate.GetPublicKey()).GetEncoded()).Select<byte, sbyte>((Func<byte, sbyte>)(x => (sbyte)x)).ToArray<sbyte>();
                BigInteger bigInteger = new BigInteger((byte[])(object)((IEnumerable<byte>)x509Cert.GetSerialNumber()).Select<byte, sbyte>((Func<byte, sbyte>)(x => (sbyte)x)).ToArray<sbyte>());
                if (x509Cert != null)
                {


                    try
                    {

                        certificateHash = ToBase64Encode(Sha256_hashAsString(certificateContent));

                    }
                    catch (Exception ex)
                    {

                    }

                    string xmlFilePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                    StreamReader rd = new StreamReader(xmlFilePath + @"\InvoiceXML\SignedProperties.txt");
                    string signedPropertiesTempelate = rd.ReadToEnd();
                    rd.Close();


                    string signedProperties = PopulateSignedSignatureProperties(signedPropertiesTempelate, certificateHash, GetCurrentTimestamp(), x509Cert.IssuerName.Name, bigInteger.ToString());
                    string signedPropertiesHash = ToBase64Encode(Sha256_hashAsString(signedProperties.Replace("\r", "")));


                    rd = new StreamReader(xmlFilePath + @"\InvoiceXML\FullSignature.txt");
                    string fullSignatureProps = rd.ReadToEnd();
                    rd.Close();





                    string fullSignature = PopulateUBLExtensions(fullSignatureProps, digitalSignature, signedPropertiesHash, einvoiceHashing, certificateContent, signedProperties);

                    rd = new StreamReader(xmlFilePath + @"\InvoiceXML\QrTag.txt");
                    string qrTag = rd.ReadToEnd();
                    rd.Close();
                    string qrTagResult = PopulateQRCode(qrTag, array, digitalSignature, einvoiceHashing, ((IEnumerable<byte>)x509Certificate.GetSignature()).Select<byte, sbyte>((Func<byte, sbyte>)(x => (sbyte)x)).ToArray<sbyte>(), invoice);

                    rd = new StreamReader(xmlFilePath + @"\InvoiceXML\SignatureTag.txt");
                    string signatureTag = rd.ReadToEnd();
                    rd.Close();


                    var finalXml = GetInvoiceXMlBeforeOperations(invoice);


                    finalXml = finalXml.Replace("@QrTag", qrTagResult);
                    finalXml = finalXml.Replace("@SignatureTag", signatureTag);
                    finalXml = finalXml.Replace("@DigitalSignature", fullSignature);

                    return finalXml;
                }

            }
            catch (Exception ex)
            {

            }
            return "";
        }


        public string Sha256_hashAsString(string rawData)
        {
            StringBuilder stringBuilder = new StringBuilder();
            using (SHA256 shA256 = SHA256.Create())
            {
                Encoding utF8 = Encoding.UTF8;
                foreach (byte num in shA256.ComputeHash(Encoding.UTF8.GetBytes(rawData)))
                    stringBuilder.Append(num.ToString("x2"));
            }
            return stringBuilder.ToString();
        }

        public string ToBase64Encode(string toEncode)
        {
            return Convert.ToBase64String(Encoding.ASCII.GetBytes(toEncode));
        }



        private string PopulateSignedSignatureProperties(
      string signedProperties,
      //Dictionary<string, string> nameSpacesMap,
      string publicKeyHashing,
      string signatureTimestamp,
      string x509IssuerName,
      string serialNumber)
        {
            //Result result = new Result();

            //Utility.SetNodeValue(document, SettingsParams.PUBLIC_KEY_HASHING_XPATH, publicKeyHashing);
            signedProperties = signedProperties.Replace("@CertificateHash", publicKeyHashing);

            //Utility.SetNodeValue(document, SettingsParams.SIGNING_TIME_XPATH, signatureTimestamp);
            signedProperties = signedProperties.Replace("@SigningTime", signatureTimestamp);
            //Utility.SetNodeValue(document, SettingsParams.ISSUER_NAME_XPATH, x509IssuerName);
            signedProperties = signedProperties.Replace("@CertificateIssuer", x509IssuerName);
            //Utility.SetNodeValue(document, SettingsParams.X509_SERIAL_NUMBER_XPATH, serialNumber);
            signedProperties = signedProperties.Replace("@CertificateSerialNumber", serialNumber);
            //string str = Utility.GetNodeInnerXML(document, SettingsParams.SIGNED_PROPERTIES_XPATH).Replace(" />", "/>").Replace("></ds:DigestMethod>", "/>");
            //Sha256_hashAsString(str.Replace("\r", ""));
            //result.ResultedValue = Utility.ToBase64Encode(Utility.Sha256_hashAsString(str.Replace("\r", "")));
            //result.IsValid = true;
            return signedProperties;


        }

        private string GetCurrentTimestamp()
        {
            CultureInfo culture = new CultureInfo("en-US");
            return DateTime.Now.ToString("yyyy-MM-dd'T'HH:mm:ss'Z'",culture);
        }



        private string PopulateUBLExtensions(
      string fullSignContent,
      string digitalSignature,
      string signedPropertiesHashing,
      string xmlHashing,
      string certificate,
      string signedProperties)
        {

            fullSignContent = fullSignContent.Replace("@SignedPropetiesXml", signedProperties);

            //Utility.SetNodeValue(xmlDoc, SettingsParams.SIGNATURE_XPATH, digitalSignature);
            fullSignContent = fullSignContent.Replace("@DigitalSignature", digitalSignature);

            //Utility.SetNodeValue(xmlDoc, SettingsParams.CERTIFICATE_XPATH, certificate);
            fullSignContent = fullSignContent.Replace("@Certificate", certificate);
            //Utility.SetNodeValue(xmlDoc, SettingsParams.SIGNED_Properities_DIGEST_VALUE_XPATH, signedPropertiesHashing);
            fullSignContent = fullSignContent.Replace("@SignedPropertiesHash", signedPropertiesHashing);
            //Utility.SetNodeValue(xmlDoc, SettingsParams.Hash_XPATH, xmlHashing);
            fullSignContent = fullSignContent.Replace("@InvoiceHash", xmlHashing);

            return fullSignContent;
        }


        private string PopulateQRCode(
      string qrTag,
      sbyte[] publicKeyArr,
      string signature,
      string hashedXml,
      sbyte[] certificateSignatureBytes, Invoice invoice)
        {

            string sellerName = invoice.SellerName;

            string sellerVatNumber = invoice.SellerVatNumber;
            //string invoiceTimeStamp = invoice.InvoiceDateTime.ToString("yyyy-MM-ddTHH:mm:ss'Z'");
            string totalWithVat = invoice.InvoiceTotalWithVat;

            string vatValue = invoice.InvoiceTotalVatAmount;

            //Utility.GetNodeInnerText(xmlDoc, SettingsParams.QR_CODE_XPATH);
            //DateTime result2 = new DateTime();
            //string str = invoice.IssueDate + " " + invoice.IssueTime;
            //DateTime.TryParseExact(invoice.IssueDate, new string[19], (IFormatProvider)CultureInfo.InvariantCulture, DateTimeStyles.None, out result2);
            //string[] strArray = invoice.IssueTime.Split(':');
            //int result3 = 0;
            //int result4 = 0;
            //int result5 = 0;
            //if (!string.IsNullOrEmpty(strArray[0]) && int.TryParse(strArray[0], out result3))
            //    result2 = result2.AddHours((double)result3);
            //if (strArray.Length > 1 && !string.IsNullOrEmpty(strArray[1]) && int.TryParse(strArray[1], out result4))
            //    result2 = result2.AddMinutes((double)result4);
            //if (strArray.Length > 2 && !string.IsNullOrEmpty(strArray[2]) && int.TryParse(strArray[2], out result5))
            //    result2 = result2.AddSeconds((double)result5);
            CultureInfo culture = new CultureInfo("en-US");
            string timeStamp = invoice.InvoiceDateTime.ToString("yyyy-MM-dd'T'HH:mm:ss'Z'", culture);
            bool isSimplified = true;
            //if (Utility.GetInvoiceType(xmlDoc) == "Simplified")
            //    isSimplified = true;
            string qrCodeFromValues = GenerateQrCodeFromValues(sellerName, sellerVatNumber, timeStamp, totalWithVat, vatValue, hashedXml, publicKeyArr, signature, isSimplified, certificateSignatureBytes);
            qrTag = qrTag.Replace("@QRData", qrCodeFromValues);
            return qrTag;
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
            byte[] array2 = ((IEnumerable<byte>)WriteTlv(1U, Encoding.UTF8.GetBytes(sellerName)).ToArray()).Concat<byte>((IEnumerable<byte>)WriteTlv(2U, Encoding.UTF8.GetBytes(vatRegistrationNumber)).ToArray()).Concat<byte>((IEnumerable<byte>)WriteTlv(3U, Encoding.UTF8.GetBytes(timeStamp)).ToArray()).Concat<byte>((IEnumerable<byte>)WriteTlv(4U, Encoding.UTF8.GetBytes(invoiceTotal)).ToArray()).Concat<byte>((IEnumerable<byte>)WriteTlv(5U, Encoding.UTF8.GetBytes(vatTotal)).ToArray()).Concat<byte>((IEnumerable<byte>)WriteTlv(6U, Encoding.UTF8.GetBytes(hashedXml)).ToArray()).Concat<byte>((IEnumerable<byte>)WriteTlv(7U, (byte[])(object)array1).ToArray()).Concat<byte>((IEnumerable<byte>)WriteTlv(8U, (byte[])(object)publicKey).ToArray()).ToArray<byte>();
            if (isSimplified)
                array2 = ((IEnumerable<byte>)array2).Concat<byte>((IEnumerable<byte>)WriteTlv(9U, (byte[])(object)certificateSignature).ToArray()).ToArray<byte>();
            return ToBase64Encode(array2);
        }

        public MemoryStream WriteTlv(uint tag, byte[] value)
        {
            MemoryStream memoryStream = new MemoryStream();
            WriteTag((Stream)memoryStream, tag);
            int count = value != null ? value.Length : 0;
            WriteLength((Stream)memoryStream, new int?(count));
            if (value == null)
                throw new Exception("Please provide a value!");
            memoryStream.Write(value, 0, count);
            return memoryStream;
        }

        public void WriteTag(Stream stream, uint tag)
        {
            bool flag = true;
            for (int index = 3; index >= 0; --index)
            {
                byte num = (byte)(tag >> 8 * index);
                if (!(num == (byte)0 & flag) || index <= 0)
                {
                    if (flag)
                    {
                        if (index == 0)
                        {
                            if (((int)num & 31) == 31)
                                throw new Exception("Invalid tag value: first octet indicates subsequent octets, but no subsequent octets found");
                        }
                        else if (((int)num & 31) != 31)
                            throw new Exception("Invalid tag value: first octet indicates no subsequent octets, but subsequent octets found");
                    }
                    else if (index == 0)
                    {
                        if (((int)num & 128) == 128)
                            throw new Exception("Invalid tag value: last octet indicates subsequent octets");
                    }
                    else if (((int)num & 128) != 128)
                        throw new Exception("Invalid tag value: non-last octet indicates no subsequent octets");
                    stream.WriteByte(num);
                    flag = false;
                }
            }
        }

        public void WriteLength(Stream stream, int? length)
        {
            if (!length.HasValue)
            {
                stream.WriteByte((byte)128);
            }
            else
            {
                int? nullable1 = length;
                int num1 = 0;
                long? nullable2;
                int num2;
                if (!(nullable1.GetValueOrDefault() < num1 & nullable1.HasValue))
                {
                    nullable1 = length;
                    nullable2 = nullable1.HasValue ? new long?((long)nullable1.GetValueOrDefault()) : new long?();
                    long maxValue = (long)uint.MaxValue;
                    num2 = nullable2.GetValueOrDefault() > maxValue & nullable2.HasValue ? 1 : 0;
                }
                else
                    num2 = 1;
                if (num2 != 0)
                    throw new Exception(string.Format("Invalid length value: {0}", (object)length));
                nullable1 = length;
                int maxValue1 = (int)sbyte.MaxValue;
                if (nullable1.GetValueOrDefault() <= maxValue1 & nullable1.HasValue)
                {
                    stream.WriteByte(checked((byte)length.Value));
                }
                else
                {
                    nullable1 = length;
                    int maxValue2 = (int)byte.MaxValue;
                    byte num3;
                    if (nullable1.GetValueOrDefault() <= maxValue2 & nullable1.HasValue)
                    {
                        num3 = (byte)1;
                    }
                    else
                    {
                        nullable1 = length;
                        int maxValue3 = (int)ushort.MaxValue;
                        if (nullable1.GetValueOrDefault() <= maxValue3 & nullable1.HasValue)
                        {
                            num3 = (byte)2;
                        }
                        else
                        {
                            nullable1 = length;
                            int num4 = 16777215;
                            if (nullable1.GetValueOrDefault() <= num4 & nullable1.HasValue)
                            {
                                num3 = (byte)3;
                            }
                            else
                            {
                                nullable1 = length;
                                nullable2 = nullable1.HasValue ? new long?((long)nullable1.GetValueOrDefault()) : new long?();
                                long maxValue4 = (long)uint.MaxValue;
                                if (!(nullable2.GetValueOrDefault() <= maxValue4 & nullable2.HasValue))
                                    throw new Exception(string.Format("Length value too big: {0}", (object)length));
                                num3 = (byte)4;
                            }
                        }
                    }
                    stream.WriteByte((byte)((uint)num3 | 128U));
                    for (int index = (int)num3 - 1; index >= 0; --index)
                    {
                        nullable1 = length;
                        int num5 = 8 * index;
                        byte num6 = (byte)(nullable1.HasValue ? new int?(nullable1.GetValueOrDefault() >> num5) : new int?()).Value;
                        stream.WriteByte(num6);
                    }
                }
            }
        }


        public bool ValidateSignature(ref string errorMessage, string xmlFilePath, string certificateContent, Invoice invoice)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.PreserveWhitespace = true;
            try
            {
                xmlDoc.Load(xmlFilePath);
            }
            catch
            {
            }

            if (string.IsNullOrEmpty(xmlDoc.InnerText))
            {
                return false;
            }
            string str1 = GetNodeInnerText(xmlDoc, Settings.CERTIFICATE_XPATH).Trim();
            if (string.IsNullOrEmpty(str1))
            {
                errorMessage = "Unable to get CERTIFICATE value from E-invoice XML";
                return false;
            }
            sbyte[] array1 = ((IEnumerable<byte>)ToBase64DecodeAsBinary(str1)).Select<byte, sbyte>((Func<byte, sbyte>)(x => (sbyte)x)).ToArray<sbyte>();
            if (array1 != null && array1.Length == 0)
            {
                errorMessage = "Invalid CERTIFICATE";
                return false;
            }

            string einvoiceHashing = GeneratePureXmlHash(invoice);

            //Result result = ValidateEInvoiceHashing(xmlFilePath);

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
            string nodeInnerText1 = GetNodeInnerText(xmlDoc, Settings.SIGNATURE_XPATH);
            if (string.IsNullOrEmpty(nodeInnerText1))
            {
                errorMessage = "Unable to get Signature value in E-invoice XML.";
                return false;
            }
            sbyte[] array3 = ((IEnumerable<byte>)ToBase64DecodeAsBinary(einvoiceHashing)).Select<byte, sbyte>((Func<byte, sbyte>)(x => (sbyte)x)).ToArray<sbyte>();
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
            string str2 = GetNodeInnerXML(xmlDoc, Settings.SIGNED_PROPERTIES_XPATH).Replace(" />", "/>").Replace("></ds:DigestMethod>", "/>");
            string nodeInnerText2 = GetNodeInnerText(xmlDoc, Settings.SIGNED_Properities_DIGEST_VALUE_XPATH);
            if (ToBase64Encode(Sha256_hashAsString(str2.Replace("\r", ""))) != nodeInnerText2)
            {
                errorMessage = "Wrong signing properties digest value.";
                return false;
            }
            if (ToBase64Encode(Sha256_hashAsString(str1)) != GetNodeInnerText(xmlDoc, Settings.SIGNED_Certificate_DIGEST_VALUE_XPATH))
            {
                errorMessage = "Wrong signing certificate digest value.";
                return false;
            }
            X509Certificate2 x509Certificate2 = new X509Certificate2((byte[])(object)((IEnumerable<byte>)Encoding.UTF8.GetBytes(certificateContent)).Select<byte, sbyte>((Func<byte, sbyte>)(x => (sbyte)x)).ToArray<sbyte>());
            if (GetNodeInnerText(xmlDoc, Settings.X509_SERIAL_NUMBER_XPATH) != new BigInteger((byte[])(object)((IEnumerable<byte>)x509Certificate2.GetSerialNumber()).Select<byte, sbyte>((Func<byte, sbyte>)(x => (sbyte)x)).ToArray<sbyte>()).ToString())
            {
                errorMessage = "Invalid certificate serial number.";
                return false;
            }
            if (!(GetNodeInnerText(xmlDoc, Settings.ISSUER_NAME_XPATH) != x509Certificate2.IssuerName.Name))
                return flag;
            errorMessage = "Invalid certificate issuer name.";
            return false;


        }


        public string GetNodeInnerText(XmlDocument doc, string xPath)
        {
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("cac", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
            nsmgr.AddNamespace("cbc", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            nsmgr.AddNamespace("ext", "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2");
            XmlNode xmlNode = doc.SelectSingleNode(xPath, nsmgr);
            return xmlNode != null ? xmlNode.InnerText : "";
        }



        public string GetNodeInnerXML(XmlDocument doc, string xPath)
        {
            XmlNode xmlNode = doc.SelectSingleNode(xPath);
            if (xmlNode == null)
                return "";
            string str = "";
            using (MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(xmlNode.OuterXml)))
            {
                XmlDsigC14NTransform dsigC14Ntransform = new XmlDsigC14NTransform(false);
                dsigC14Ntransform.LoadInput((object)memoryStream);
                str = Encoding.UTF8.GetString((dsigC14Ntransform.GetOutput() as MemoryStream).ToArray());
            }
            return str.Replace("></ds:DigestMethod>", "/>");
        }

        public InvoiceReportResponse ReportSingeInvoice(ReportInvoiceRequest request, string securtyToken, string password, int clearance)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = _baseUri;


            //var decodedUserName = Base64ToString(securtyToken);
            //var decodedPassword = Base64ToString(password);
            var auth = "Basic " + btoa(securtyToken + ":" + password);

            var reportInvoiceResponse = new InvoiceReportResponse();
            string jsonData = JsonConvert.SerializeObject(request);
            HttpContent data = new StringContent(jsonData, Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Add("accept", "application/json");
            _httpClient.DefaultRequestHeaders.Add("Accept-Language", "en");
            _httpClient.DefaultRequestHeaders.Add("Clearance-Status", clearance.ToString());
            _httpClient.DefaultRequestHeaders.Add("Accept-version", "V2");
            _httpClient.DefaultRequestHeaders.Add("Authorization", auth);


            data.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            string pathUrl = _isProduction == true ? EndPoints.ReportSimpleSingleInvoiceProd : EndPoints.ReportSimpleSingleInvoice;
            //"e-invoicing/developer-portal/invoices/reporting/single"
            var response = _httpClient.PostAsync(pathUrl, data).GetAwaiter().GetResult();
            string jsonResponse = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            _eInvoiceResponseLogRepos.AddLog(new Models.EInvoiceResponseLog()
            {
                ApiResponse = jsonResponse,
                StatusCode = (int)response.StatusCode,
                InvGuid = new Guid(),
                Operation = "ReportSingeInvoice",
                OperationDate = DateTime.Now,
                ExtApiUrl = pathUrl

            });
            reportInvoiceResponse = JsonConvert.DeserializeObject<InvoiceReportResponse>(jsonResponse);
            return reportInvoiceResponse;

        }



        public ClearanceInvoiceResponse ReportAndClearanceStandardInvoice(ReportInvoiceRequest request, string securtyToken, string password)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = _baseUri;


            //var decodedUserName = Base64ToString(securtyToken);
            //var decodedPassword = Base64ToString(password);
            var auth = "Basic " + btoa(securtyToken + ":" + password);

            var clearanceInvoiceResponse = new ClearanceInvoiceResponse();
            string jsonData = JsonConvert.SerializeObject(request);
            HttpContent data = new StringContent(jsonData, Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Add("accept", "application/json");
            _httpClient.DefaultRequestHeaders.Add("Accept-Language", "en");
            _httpClient.DefaultRequestHeaders.Add("Clearance-Status", "1");
            _httpClient.DefaultRequestHeaders.Add("Accept-version", "V2");
            _httpClient.DefaultRequestHeaders.Add("Authorization", auth);


            data.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            string pathUrl = _isProduction == true ? EndPoints.ClearanceSingleInvoiceProd : EndPoints.ClearanceSingleInvoice;
            //"e-invoicing/developer-portal/invoices/clearance/single"
            var response = _httpClient.PostAsync(pathUrl, data).GetAwaiter().GetResult();
            string jsonResponse = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            WriteLog("Cleare Invoice Result is " + jsonResponse);
            _eInvoiceResponseLogRepos.AddLog(new Models.EInvoiceResponseLog()
            {
                ApiResponse = jsonResponse,
                StatusCode = (int)response.StatusCode,
                InvGuid =Guid.Parse(request.uuid),
                Operation = "ClearStandardInvoice",
                OperationDate = DateTime.Now,
                ExtApiUrl = pathUrl

            });

            if ((int)response.StatusCode == 401)
            {
                clearanceInvoiceResponse.clearanceStatus = "Error Not Authorized";
                clearanceInvoiceResponse.validationResults = new InvoiceValidationResult();
                clearanceInvoiceResponse.validationResults.errorMessages = new List<InvoiceMessage>(){
                    new InvoiceMessage()
                {
                    code="ZATCA ERROR",
                    category = "ERROR",
                    message = "Not Authotized Please Check PCID Certificate"
                }
                };
                clearanceInvoiceResponse.validationResults.status = "ERROR NOT AUTHORIZED";
            }
            else
            {
                clearanceInvoiceResponse = JsonConvert.DeserializeObject<ClearanceInvoiceResponse>(jsonResponse);
            }



            return clearanceInvoiceResponse;

        }



        public ComplianceInvoiceResponse CheckComplianceInvoices(ReportInvoiceRequest request, string securtyToken, string password)
        {
            // WriteLog("Inside Check");
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = _baseUri;


            var auth = "Basic " + btoa(securtyToken + ":" + password);

            var complienceInvoiceResponse = new ComplianceInvoiceResponse();
            string jsonData = JsonConvert.SerializeObject(request);
            HttpContent data = new StringContent(jsonData, Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Add("accept", "application/json");
            _httpClient.DefaultRequestHeaders.Add("Accept-Language", "en");
            _httpClient.DefaultRequestHeaders.Add("Clearance-Status", "1");
            _httpClient.DefaultRequestHeaders.Add("Accept-version", "V2");
            _httpClient.DefaultRequestHeaders.Add("Authorization", auth);


            data.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            string pathUrl = _isProduction == true ? EndPoints.ValidateInvoiceProd : EndPoints.ValidateInvoice;
            //"e-invoicing/developer-portal/compliance/invoices"
            var response = _httpClient.PostAsync(pathUrl, data).GetAwaiter().GetResult();
            //WriteLog("After Read Response");
            string jsonResponse = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            WriteLog("Check Compliance:" + jsonResponse);


            _eInvoiceResponseLogRepos.AddLog(new Models.EInvoiceResponseLog() {
                ApiResponse = jsonResponse,
                StatusCode = (int) response.StatusCode,
                InvGuid = Guid.Parse(request.uuid),
                Operation = "CheckCompliance",
                OperationDate = DateTime.Now,
                ExtApiUrl = pathUrl
            });



            if (response.IsSuccessStatusCode)
            {
                complienceInvoiceResponse = JsonConvert.DeserializeObject<ComplianceInvoiceResponse>(jsonResponse);
                return complienceInvoiceResponse;
            }
            return new ComplianceInvoiceResponse()
            {
                status = "Error: "+((int)response.StatusCode).ToString() + jsonResponse
            };




        }

        public ProductionCSIDResponse RequestProductionCSIDS(string requestId, string securtyToken, string password)
        {
            ProductionCSIDRequest request = new ProductionCSIDRequest()
            {
                compliance_request_id = requestId
            };

            var productionCSIDResponse = new ProductionCSIDResponse();
            //string auth = "Basic " + btoa(securtyToken + ":" + password);
            string auth = btoa(securtyToken + ":" + password);
            string jsonData = JsonConvert.SerializeObject(request);
            HttpContent data = new StringContent(jsonData, Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Add("accept", "application/json");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", auth);
            //_httpClient.DefaultRequestHeaders.Add("Authorization", auth);
            data.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            string productionCsid = _isProduction == true ? EndPoints.IssueCsidProduction : EndPoints.IssueCsid;
            var response = _httpClient.PostAsync(productionCsid, data).GetAwaiter().GetResult();
            string jsonResponse = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();


            _eInvoiceResponseLogRepos.AddLog(new Models.EInvoiceResponseLog()
            {
                ApiResponse = jsonResponse,
                StatusCode = (int)response.StatusCode,
                InvGuid = new Guid(),
                Operation = "RequestProductionCSIDS",
                OperationDate = DateTime.Now,
                ExtApiUrl = productionCsid

            });



            if (response.IsSuccessStatusCode)
            {
                productionCSIDResponse = JsonConvert.DeserializeObject<ProductionCSIDResponse>(jsonResponse);
                return productionCSIDResponse;
            }
            else
            {

                return new ProductionCSIDResponse()
                {
                    dispositionMessage = "Error:" + jsonResponse
                };
            }



        }

        public ProductionCSIDResponse RenewProductionCSIDS(string csr, string OPT, string securtyToken, string password)
        {
            ComplianceCertificateRequest request = new ComplianceCertificateRequest()
            {
                csr = ConvertStringToBase64(csr)
            };

            _httpClient = new HttpClient();
            _httpClient.BaseAddress = _baseUri;

            string auth = "Basic " + btoa(securtyToken + ":" + password);
            var productionCSIDResponse = new ProductionCSIDResponse();
            string jsonData = JsonConvert.SerializeObject(request);
            HttpContent data = new StringContent(jsonData, Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Add("accept", "application/json");
            _httpClient.DefaultRequestHeaders.Add("OTP", OPT);
            _httpClient.DefaultRequestHeaders.Add("Accept-Language", "en");
            _httpClient.DefaultRequestHeaders.Add("Accept-version", "V2");
            _httpClient.DefaultRequestHeaders.Add("Authorization", auth);


            string productionCsid = _isProduction == true ? EndPoints.IssueCsidProduction : EndPoints.IssueCsid;

            data.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = _httpClient.PatchAsync(productionCsid, data).GetAwaiter().GetResult();
            string jsonResponse = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            _eInvoiceResponseLogRepos.AddLog(new Models.EInvoiceResponseLog()
            {
                ApiResponse = jsonResponse,
                StatusCode = (int)response.StatusCode,
                InvGuid = new Guid(),
                Operation = "RequestProductionCSIDS",
                OperationDate = DateTime.Now,
                ExtApiUrl = productionCsid

            });

            productionCSIDResponse = JsonConvert.DeserializeObject<ProductionCSIDResponse>(jsonResponse);
            return productionCSIDResponse;
        }



        public string PrepareXmlFile(string tempelatePath, InvoiceType invoiceType, Invoice invoiceData, string invoiceMainPath)
        {
            string tempelateXMLString = ReadFile(tempelatePath);

            tempelateXMLString = tempelateXMLString.Replace("@CurrencyCode", invoiceData.CurrencyCode);
            tempelateXMLString = tempelateXMLString.Replace("@Code", invoiceData.InvoiceCode);
            tempelateXMLString = tempelateXMLString.Replace("@Guid", invoiceData.InvoiceGuid.ToString().ToLower());
            tempelateXMLString = tempelateXMLString.Replace("@Date", invoiceData.IssueDate);
            //if (invoiceType == InvoiceType.B2C)
            //{
            //    tempelateXMLString = tempelateXMLString.Replace("@Time", invoiceData.IssueTime+"Z");
            //}
            //else
            //{
            //    tempelateXMLString = tempelateXMLString.Replace("@Time", invoiceData.IssueTime);
            //}

            tempelateXMLString = tempelateXMLString.Replace("@Time", invoiceData.IssueTime);

            tempelateXMLString = tempelateXMLString.Replace("@Number", invoiceData.InvoiceNumber);
            tempelateXMLString = tempelateXMLString.Replace("@PIH", invoiceData.PreviousHashInvoice);
            tempelateXMLString = tempelateXMLString.Replace("@InvoiceTypeCode", invoiceData.InvoiceTypeCode);
            tempelateXMLString = tempelateXMLString.Replace("@TransactionTypeCode", invoiceData.TransactionTypeCode);
            tempelateXMLString = tempelateXMLString.Replace("@Notes", invoiceData.Notes);
            if (invoiceData.InvoiceTypeCode == "381" || invoiceData.InvoiceTypeCode == "383")
            {
                //DebitOrCreditNote
                tempelateXMLString = tempelateXMLString.Replace("@Reference", GetRefernce(invoiceData.ReferenceCode, invoiceData.RefernceDate));

            }
            else
            {
                tempelateXMLString = tempelateXMLString.Replace("@Reference", "");
            }



            double totalInvoiceLineExtensionAmount = invoiceData.InvoiceLines.Sum(x => double.Parse(x.NetAmountBeforeVat) - double.Parse(x.InvoiceLineDiscountAmount));

            //double vatAmount = Math.Round( invoiceData.InvoiceLines.Sum(x => double.Parse(x.InvoiceLineVatAmount)),2);
            //double totalWithTax = totalInvoiceLineExtensionAmount + vatAmount;



            tempelateXMLString = tempelateXMLString.Replace("@SellerComRegNumber", invoiceData.SellerSchemaIdValue);
            tempelateXMLString = tempelateXMLString.Replace("@SellerStreetName", invoiceData.SellerStreetName);
            tempelateXMLString = tempelateXMLString.Replace("@SellerBuilding", invoiceData.SellerBuildingNumber);
            tempelateXMLString = tempelateXMLString.Replace("@SellerAdditionalNumber", invoiceData.SellerAdditionalNumber);
            tempelateXMLString = tempelateXMLString.Replace("@SellerDistrict", invoiceData.SellerDistrict);
            tempelateXMLString = tempelateXMLString.Replace("@SellerCity", invoiceData.SellerCityName);
            tempelateXMLString = tempelateXMLString.Replace("@SellerPostalCode", invoiceData.SellerPostalCode);
            tempelateXMLString = tempelateXMLString.Replace("@SellerCountryCode", invoiceData.SellerCountryCode);
            tempelateXMLString = tempelateXMLString.Replace("@SellerVatNumber", invoiceData.SellerVatNumber);
            tempelateXMLString = tempelateXMLString.Replace("@SellerVatRegName", invoiceData.SellerName);

            tempelateXMLString = tempelateXMLString.Replace("@BuyerComRegNumber", invoiceData.BuyerSchemaIdValue);
            tempelateXMLString = tempelateXMLString.Replace("@BuyerStreetName", invoiceData.BuyerStreetName);
            tempelateXMLString = tempelateXMLString.Replace("@BuyerBuilding", invoiceData.BuyerBuildingNumber);
            tempelateXMLString = tempelateXMLString.Replace("@BuyerAdditionalNumber", invoiceData.BuyerAdditionalNumber);
            tempelateXMLString = tempelateXMLString.Replace("@BuyerDistrict", invoiceData.BuyerDistrict);
            tempelateXMLString = tempelateXMLString.Replace("@BuyerCity", invoiceData.BuyerCityName);
            tempelateXMLString = tempelateXMLString.Replace("@BuyerPostalCode", invoiceData.BuyerPostalCode);
            tempelateXMLString = tempelateXMLString.Replace("@BuyerCountryCode", invoiceData.BuyerCountryCode);
            tempelateXMLString = tempelateXMLString.Replace("@BuyerVatNumber", invoiceData.BuyerVatNumber);
            tempelateXMLString = tempelateXMLString.Replace("@BuyerVatRegName", invoiceData.BuyerName);

            tempelateXMLString = tempelateXMLString.Replace("@ActualDeliveryDate", invoiceData.SupplyDate);
            tempelateXMLString = tempelateXMLString.Replace("@PaymentCode", ((int)invoiceData.PaymentTypeCode).ToString());
            tempelateXMLString = tempelateXMLString.Replace("@IsDiscount", invoiceData.IsDiscountOnDocumentLevel ? "false" : "false");
            //tempelateXMLString = tempelateXMLString.Replace("@DiscountAmount", invoiceData.DiscountAmount);
            tempelateXMLString = tempelateXMLString.Replace("@DiscountAmount", "0.00"); //Document Level Discount
            tempelateXMLString = tempelateXMLString.Replace("@TaxType", invoiceData.VatCategoryCode);
            tempelateXMLString = tempelateXMLString.Replace("@TaxPercent", invoiceData.VatCategoryPercent);
            tempelateXMLString = tempelateXMLString.Replace("@TotalTaxAmount", invoiceData.VatCategoryTaxAmount);

            tempelateXMLString = tempelateXMLString.Replace("@TaxableAmount", invoiceData.TaxableAmount);
            //tempelateXMLString = tempelateXMLString.Replace("@TotalBeforeTax", invoiceData.InvoiceTotalAmountWithoutVat);
            tempelateXMLString = tempelateXMLString.Replace("@TotalBeforeTax", String.Format("{0:0.00}", invoiceData.InvoiceTotalAmountWithoutVat));
            tempelateXMLString = tempelateXMLString.Replace("@TotalWithTax", String.Format("{0:0.00}", invoiceData.InvoiceTotalWithVat));


            //tempelateXMLString = tempelateXMLString.Replace("@TotalDiscountAmount", invoiceData.DiscountAmount);
            tempelateXMLString = tempelateXMLString.Replace("@TotalDiscountAmount", "0.00"); //Sum of document level Allowance

            tempelateXMLString = tempelateXMLString.Replace("@PrepaidAmount", invoiceData.PaidAmount);
            tempelateXMLString = tempelateXMLString.Replace("@PayableAmount", String.Format("{0:0.00}", invoiceData.InvoiceTotalWithVat));
            //AllowanceTaxType
            //AllowanceTaxPercent
            tempelateXMLString = tempelateXMLString.Replace("@AllowanceTaxType", "S");
            tempelateXMLString = tempelateXMLString.Replace("@AllowanceTaxPercent", "15.00");

            tempelateXMLString = tempelateXMLString.Replace("@TotalLineExtensionAmount", String.Format("{0:0.00}", totalInvoiceLineExtensionAmount));
            //tempelateXMLString = tempelateXMLString.Replace("@TotalLineExtensionAmount", String.Format("{0:0.00}", invoiceData.InvoiceTotalWithVat));










            tempelateXMLString = tempelateXMLString.Replace("@InvoiceLines", GetInvoiceLines(invoiceData.InvoiceLines));

            //double totalTaxInclusiveAmount = double.Parse(invoiceData.InvoiceTotalAmountWithoutVat) + invoiceData.InvoiceLines.Select(x=> double.Parse(x.InvoiceLineVatAmount)).Sum() ;
            double totalTaxInclusiveAmount = double.Parse(invoiceData.InvoiceTotalAmountWithoutVat) + GetStandardInvoiceTaxAmount(invoiceData.InvoiceLines, "S", invoiceData.CurrencyCode, 15, "", "");
            tempelateXMLString = tempelateXMLString.Replace("@TotalTaxInclusiveAmount", String.Format("{0:0.00}", totalTaxInclusiveAmount));
            var x = string.Format("{0:0.00}", totalTaxInclusiveAmount);

            if (invoiceData.InvoiceLines.Any(x => x.InvoiceItemVatCategoryCode == "S"))
            {
                tempelateXMLString = tempelateXMLString.Replace("@InvoiceLevelStandardVAT", GetStandardInvoiceTax(invoiceData.InvoiceLines, "S", invoiceData.CurrencyCode, 15, "", ""));
            }
            else
            {
                tempelateXMLString = tempelateXMLString.Replace("@InvoiceLevelStandardVAT", "");
            }

            if (invoiceData.InvoiceLines.Any(x => x.InvoiceItemVatCategoryCode == "Z"))
            {
                tempelateXMLString = tempelateXMLString.Replace("@InvoiceLevelZeroVAT", GetInvoiceTax(invoiceData.InvoiceLines, "Z", invoiceData.CurrencyCode, 0, "<cbc:TaxExemptionReasonCode>VATEX-SA-32</cbc:TaxExemptionReasonCode>", "<cbc:TaxExemptionReason>Export goods</cbc:TaxExemptionReason>"));
            }
            else if (invoiceData.InvoiceLines.Any(x => x.InvoiceItemVatCategoryCode == "O")) 
            {
                tempelateXMLString = tempelateXMLString.Replace("@InvoiceLevelZeroVAT", GetInvoiceTax(invoiceData.InvoiceLines, "O", invoiceData.CurrencyCode, 0, "<cbc:TaxExemptionReasonCode>VATEX-SA-OOS</cbc:TaxExemptionReasonCode>", "<cbc:TaxExemptionReason>Not Subject to VAT</cbc:TaxExemptionReason>"));
            }
            else
            {
                tempelateXMLString = tempelateXMLString.Replace("@InvoiceLevelZeroVAT", "");
            }

            if (invoiceData.InvoiceLines.Any(x => x.InvoiceItemVatCategoryCode == "E"))
            {
                tempelateXMLString = tempelateXMLString.Replace("@InvoiceLevelExemptedVAT", GetInvoiceTax(invoiceData.InvoiceLines, "E", invoiceData.CurrencyCode, 0, "<cbc:TaxExemptionReasonCode>4</cbc:TaxExemptionReasonCode>",
                    "<cbc:TaxExemptionReason>Export</cbc:TaxExemptionReason>"));

            }
            else
            {
                tempelateXMLString = tempelateXMLString.Replace("@InvoiceLevelExemptedVAT", "");
            }

            if (invoiceData.InvoiceLines.Any(x => x.InvoiceItemVatCategoryCode != "S"))
            {
                tempelateXMLString = tempelateXMLString.Replace("@OnlyStandardTaxInvoice", "");
            }
            else
            {
                tempelateXMLString = tempelateXMLString.Replace("@OnlyStandardTaxInvoice", GetOnlyStandardTaxInvoice(invoiceData.InvoiceLines, invoiceData.CurrencyCode, invoiceData.CurrencyRate));
            }

            string fileFullPathName = invoiceMainPath + (invoiceData.InvoiceGuid.ToString()) + ".xml";
            SaveFile(fileFullPathName, tempelateXMLString);




            return fileFullPathName;
        }

        public string GetInvoiceLines(List<InvoiceLine> invoiceLines)
        {

            string invoiceLineTempelate = ReadFile(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\InvoiceXML\InvoiceLineFinal.txt");
            string finalInvoiceLines = "";
            foreach (var item in invoiceLines)
            {
                var basePrice = double.Parse(item.Qty) * double.Parse(item.InvoiceLinePrice);
                var netPrice = basePrice - double.Parse(item.InvoiceLineDiscountAmount);

                //double itemPrice = double.Parse(item.InvoiceLinePrice) -( double.Parse(item.InvoiceLineDiscountAmount) / double.Parse(item.Qty));
                //double itemPrice = double.Parse(item.InvoiceLinePrice);
                string invoiceLine = invoiceLineTempelate.Replace("@InvoiceLineGuid", item.Guid.ToString());
                //double amountWithTax = Convert.ToDouble(item.NetAmountBeforeVat) + Convert.ToDouble(item.InvoiceLineVatAmount);
                double amountWithTax = netPrice + Math.Round(Convert.ToDouble(item.InvoiceLineVatAmount), 2, MidpointRounding.AwayFromZero);
                invoiceLine = invoiceLine.Replace("@LineNumber", item.Number.ToString());
                invoiceLine = invoiceLine.Replace("@UnitName", item.UnitCode);
                invoiceLine = invoiceLine.Replace("@Qty", item.Qty.ToString());
                invoiceLine = invoiceLine.Replace("@CurrencyCode", item.CurrencyCode);
                //invoiceLine = invoiceLine.Replace("@AmountBeforeTax", item.NetAmountBeforeVat);
                invoiceLine = invoiceLine.Replace("@AmountBeforeTax", string.Format("{0:0.00}", netPrice));
                invoiceLine = invoiceLine.Replace("@TaxAmount", String.Format("{0:0.00}", double.Parse(item.InvoiceLineVatAmount)));
                invoiceLine = invoiceLine.Replace("@AmountWithTax", String.Format("{0:0.00}", amountWithTax));
                invoiceLine = invoiceLine.Replace("@ItemName", item.InvoiceLineItemName.Replace("&", "_"));
                invoiceLine = invoiceLine.Replace("@TaxType", item.InvoiceItemVatCategoryCode);
                invoiceLine = invoiceLine.Replace("@TaxPercent", item.InvoiceLineVatRate);
                //invoiceLine = invoiceLine.Replace("@Price", item.InvoiceLinePrice);
                //invoiceLine = invoiceLine.Replace("@NetPrice", string.Format("{0:0.00}", itemPrice));
                invoiceLine = invoiceLine.Replace("@NetPrice", string.Format("{0:0.00}", netPrice));
                invoiceLine = invoiceLine.Replace("@IsDiscount", item.InvoiceDiscountIndicator ? "false" : "false"); //ChargeIndicator = true, means Adds, ChargeIndicator = false, means disc
                invoiceLine = invoiceLine.Replace("@DiscountAmount", item.InvoiceLineDiscountAmount);
                //invoiceLine = invoiceLine.Replace("@DiscountAmount", "0.00");
                invoiceLine = invoiceLine.Replace("@LineAmountBeforeDiscAndVat", String.Format("{0:0.00}", basePrice));
                finalInvoiceLines += invoiceLine;
            }
            return finalInvoiceLines;

        }

        public string GetOnlyStandardTaxInvoice(List<InvoiceLine> invoiceLines, string currencyCode, double currencyRate)
        {
            string invoiceTaxLineTempelate = ReadFile(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\InvoiceXML\OnlyStandardTaxInvoice.txt");
            double taxAmount = invoiceLines.Where(x => x.InvoiceItemVatCategoryCode == "S").Sum(x => double.Parse(x.InvoiceLineVatAmount));
            invoiceTaxLineTempelate = invoiceTaxLineTempelate.Replace("@CurrencyCode", "SAR");
            invoiceTaxLineTempelate = invoiceTaxLineTempelate.Replace("@TotalTaxAmount", String.Format("{0:0.00}", taxAmount* currencyRate));

            return invoiceTaxLineTempelate;

        }


        public string GetInvoiceTax(List<InvoiceLine> invoiceLines, string vatCategoryCode, string currencyCode, double vatPercent, string taxExptionReasonCode, string taxExctionReasonText)
        {

            string invoiceTaxLineTempelate = ReadFile(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\InvoiceXML\InvoiceLevelVATCategory.txt");


            double totalTaxAmount = 0;
            totalTaxAmount = invoiceLines.Where(x => x.InvoiceItemVatCategoryCode == vatCategoryCode).Sum(x => double.Parse(x.InvoiceLineVatAmount));

            double totalTaxableAmount = 0;
            totalTaxableAmount = invoiceLines.Where(x => x.InvoiceItemVatCategoryCode == vatCategoryCode).Sum(x => double.Parse(x.NetAmountBeforeVat) - double.Parse(x.InvoiceLineDiscountAmount));


            invoiceTaxLineTempelate = invoiceTaxLineTempelate.Replace("@CurrencyCode", currencyCode);

            invoiceTaxLineTempelate = invoiceTaxLineTempelate.Replace("@TotalTaxAmount", String.Format("{0:0.00}", totalTaxAmount));
            invoiceTaxLineTempelate = invoiceTaxLineTempelate.Replace("@TaxableAmount", String.Format("{0:0.00}", totalTaxableAmount));
            invoiceTaxLineTempelate = invoiceTaxLineTempelate.Replace("@TaxType", vatCategoryCode);
            invoiceTaxLineTempelate = invoiceTaxLineTempelate.Replace("@TaxPercent", String.Format("{0:0.00}", vatPercent));

            //<cbc:TaxExemptionReason>Reason for tax exempt</cbc:TaxExemptionReason>
            invoiceTaxLineTempelate = invoiceTaxLineTempelate.Replace("@TaxExeptionReasonCode", taxExptionReasonCode);
            invoiceTaxLineTempelate = invoiceTaxLineTempelate.Replace("@TaxExeptionReason", taxExctionReasonText);



            return invoiceTaxLineTempelate;

        }


        public string GetStandardInvoiceTax(List<InvoiceLine> invoiceLines, string vatCategoryCode, string currencyCode, double vatPercent, string taxExptionReasonCode, string taxExctionReasonText)
        {

            string invoiceTaxLineTempelate = ReadFile(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\InvoiceXML\InvoiceLevelVATCategory.txt");




            double totalTaxableAmount = 0;
            totalTaxableAmount = invoiceLines.Where(x => x.InvoiceItemVatCategoryCode == vatCategoryCode).Sum(x => double.Parse(x.NetAmountBeforeVat) - double.Parse(x.InvoiceLineDiscountAmount));

            double totalTaxAmount = 0;
            //totalTaxAmount = invoiceLines.Where(x => x.InvoiceItemVatCategoryCode == vatCategoryCode).Sum(x => double.Parse(x.InvoiceLineVatAmount));
            totalTaxAmount = Math.Round(totalTaxableAmount * vatPercent / 100, 2);


            invoiceTaxLineTempelate = invoiceTaxLineTempelate.Replace("@CurrencyCode", currencyCode);

            invoiceTaxLineTempelate = invoiceTaxLineTempelate.Replace("@TotalTaxAmount", String.Format("{0:0.00}", totalTaxAmount));
            invoiceTaxLineTempelate = invoiceTaxLineTempelate.Replace("@TaxableAmount", String.Format("{0:0.00}", totalTaxableAmount));
            invoiceTaxLineTempelate = invoiceTaxLineTempelate.Replace("@TaxType", vatCategoryCode);
            invoiceTaxLineTempelate = invoiceTaxLineTempelate.Replace("@TaxPercent", String.Format("{0:0.00}", vatPercent));

            //<cbc:TaxExemptionReason>Reason for tax exempt</cbc:TaxExemptionReason>
            invoiceTaxLineTempelate = invoiceTaxLineTempelate.Replace("@TaxExeptionReasonCode", taxExptionReasonCode);
            invoiceTaxLineTempelate = invoiceTaxLineTempelate.Replace("@TaxExeptionReason", taxExctionReasonText);



            return invoiceTaxLineTempelate;

        }


        public double GetStandardInvoiceTaxAmount(List<InvoiceLine> invoiceLines, string vatCategoryCode, string currencyCode, double vatPercent, string taxExptionReasonCode, string taxExctionReasonText)
        {

            string invoiceTaxLineTempelate = ReadFile(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\InvoiceXML\InvoiceLevelVATCategory.txt");




            double totalTaxableAmount = 0;
            totalTaxableAmount = invoiceLines.Where(x => x.InvoiceItemVatCategoryCode == vatCategoryCode).Sum(x => double.Parse(x.NetAmountBeforeVat) - double.Parse(x.InvoiceLineDiscountAmount));

            double totalTaxAmount = 0;
            //totalTaxAmount = invoiceLines.Where(x => x.InvoiceItemVatCategoryCode == vatCategoryCode).Sum(x => double.Parse(x.InvoiceLineVatAmount));
            totalTaxAmount = Math.Round(totalTaxableAmount * vatPercent / 100, 2);
            return totalTaxAmount;

            //invoiceTaxLineTempelate = invoiceTaxLineTempelate.Replace("@CurrencyCode", currencyCode);

            //invoiceTaxLineTempelate = invoiceTaxLineTempelate.Replace("@TotalTaxAmount", String.Format("{0:0.00}", totalTaxAmount));
            //invoiceTaxLineTempelate = invoiceTaxLineTempelate.Replace("@TaxableAmount", String.Format("{0:0.00}", totalTaxableAmount));
            //invoiceTaxLineTempelate = invoiceTaxLineTempelate.Replace("@TaxType", vatCategoryCode);
            //invoiceTaxLineTempelate = invoiceTaxLineTempelate.Replace("@TaxPercent", String.Format("{0:0.00}", vatPercent));

            ////<cbc:TaxExemptionReason>Reason for tax exempt</cbc:TaxExemptionReason>
            //invoiceTaxLineTempelate = invoiceTaxLineTempelate.Replace("@TaxExeptionReasonCode", taxExptionReasonCode);
            //invoiceTaxLineTempelate = invoiceTaxLineTempelate.Replace("@TaxExeptionReason", taxExctionReasonText);



            //return invoiceTaxLineTempelate;

        }

        public string GetRefernce(string refernceCode, DateTime? refDate)
        {

            string invoiceRefTempelate = ReadFile(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\InvoiceXML\DebitOrCeditNoteReference.txt");
            invoiceRefTempelate = invoiceRefTempelate.Replace("@DebitOrCreditNoteRefernce", "Reference Code:" + refernceCode);
            return invoiceRefTempelate;


        }
    }






}
