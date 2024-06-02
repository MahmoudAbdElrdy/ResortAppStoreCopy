using Org.BouncyCastle.Crypto;
using SaudiEinvoiceService.ApiModels.Requests;
using SaudiEinvoiceService.ApiModels.Responses;
using SaudiEinvoiceService.Constants;
using SaudiEinvoiceService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SaudiEinvoiceService.IRepos
{
    public interface IEInvoiceManager
    {
        SignerData GenerateKeyPairsAndCSR(string privateKeyPath, string publicKeyPath, string csrConfigPath, string csrPath);
        string ConvertStringToBase64(string data);
        ComplianceCertificateResponse IssueComplianceCertificate(string OPT, string base64CSR);
        //SignedInvoiceData GetSignerData(string complianceCertificate, string privateKey);
        string GenerateQr(string sellerName, string vatNo, double vatAmount, double totalWithVat, DateTime invoiceDate, string invoiceHash);
        string GenerateXml(Invoice invoice);
        string GeneratePureXmlHash(Invoice invoice);
        string GetInvoiceXMlBeforeOperations(Invoice invoice);
        string Base64ToString(string base64String);
        string EncodeToBase64(string toEncode);
        ReportInvoiceRequest GenerateInvoiceRequest(string xmlFile, Guid invoiceGuid, Invoice invoice);
        string CreateSha256Hash(string rawData);
        string GetCleanCertificat(string cerificate);
        string GetCleanPrivateKey(string privateKey);

        string GetWrapedCertificat(string cleanCertificate);
        X509Certificate GenerateX509Certificate(string wrapedCertificate);
        X509Certificate2 GenerateCertificate(string wrapedCertificate);
        string ByteArrayToString(byte[] bytes);
        string CreateInvoiceDigitalSignature(string invoiceHash, string cleanedPrivateKey);
        //byte[] DecodeOpenSSLPrivateKey(string instr);
        //RSACryptoServiceProvider DecodeRSAPrivateKey(byte[] privkey);
        //int GetIntegerSize(BinaryReader binr);
        string GenerateQR(Invoice invoice, string publicKey, string digitalSignature, string invoiceHash, string certificateSignature);

        SignedPropertiesData GetSignedProperties(SignedProperties properties);
        string GetFullSignature(SignedPropertiesData signedProperties, string invoiceHash, string digitalSignature, string certificateString);

        string GenerateFullXml(Invoice invoice, string Qr, string fullSignature);
        string GenerateFullXmlWithoutQr(Invoice invoice, string fullSignature);
        public string GetSignature(AsymmetricKeyParameter privateKeyParameter, string invoiceHash);

        public string SignInvoice(string invoiceXML, ECDsa keyPairs);
        public AsymmetricCipherKeyPair ReadAsymmetricCipherKeyPairFromPem(string pathToPem);
        SignerData GenerateKeyPairsAndCSRUsingCode();

        string GenerateQR(Invoice invoice, byte[] publicKey, string digitalSignature, byte[] invoiceHash, byte[] certificateSignature);
        string ReportSingeInvoice(string invoiceXml, string securtyToken, string password, string guid);
        InvoiceReportResponse ReportSingeInvoice(ReportInvoiceRequest request, string securtyToken, string password, int clearance);
        byte[] CreateSha256HashByteArray(string rawData);
        byte[] GeneratePureXmlHashArray(Invoice invoice);
        string ConvertHashStringToBase64(string hashString);
        string CreateHash(string xmlString);
        byte[] CreateHashByteArray(string xmlString);

        string SignXmlInvoice(Invoice invoice, string certificateContent, string privateKeyContent);
        bool ValidateSignature(ref string errorMessage, string xmlFilePath, string certificateContent, Invoice invoice);

        ProductionCSIDResponse RequestProductionCSIDS(string requestId, string securtyToken, string password);
       

        ProductionCSIDResponse RenewProductionCSIDS(string csr, string opt, string securityToken, string password);

        ComplianceInvoiceResponse CheckComplianceInvoices(ReportInvoiceRequest request, string securtyToken, string password);
        public ClearanceInvoiceResponse ReportAndClearanceStandardInvoice(ReportInvoiceRequest request, string securtyToken, string password);

        public string ReadFile(string path);
        bool SaveFile(string path, string data);

        string PrepareXmlFile(string tempelatePath, InvoiceType invoiceType, Invoice invoiceData, string invoiceMainPath);
        string GetInvoiceLines(List<InvoiceLine> invoiceLinse);
        string GetQrValueFromClearedInvoice(string xmlDataBase64);
    }
}
