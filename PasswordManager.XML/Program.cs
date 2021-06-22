using System;
using System.Xml;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Text;
using PasswordManager.Domain;
using System.Collections.Generic;

namespace CSCrypto
{
    class Program : ILogic
    {
        private static Aes GetAESKey(string password)
        {
            byte[] IV = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
            int BlockSize = 128;

            Aes result = Aes.Create();
            byte[] bytes = Encoding.Unicode.GetBytes(string.Format("!{0},?,{0}?", password));
            HashAlgorithm hash = MD5.Create();
            result.BlockSize = BlockSize;
            result.Key = hash.ComputeHash(bytes);
            result.IV = IV;

            return result;
        }


        static void Main(string[] args)
        {
            

            try
            {
                Aes encryptKey = GetAESKey("1234");

        
                // Load an XML document.
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.PreserveWhitespace = true;
                xmlDoc.Load("test.xml");

                // Encrypt the "creditcard" element.
                Encrypt(xmlDoc, "password", encryptKey);

                Console.WriteLine("The element was encrypted");

                Console.WriteLine(xmlDoc.InnerXml);


                Aes decryptKey = GetAESKey("1234");

               
                Decrypt(xmlDoc, decryptKey);

                Console.WriteLine("The element was decrypted");

                Console.WriteLine(xmlDoc.InnerXml);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                // Clear the key.                

                Console.ReadLine();
            }
        }

        public static void Encrypt(XmlDocument Doc, string ElementName, SymmetricAlgorithm Key)
        {
            // Check the arguments.
            if (Doc == null)
                throw new ArgumentNullException("Doc");
            if (ElementName == null)
                throw new ArgumentNullException("ElementToEncrypt");
            if (Key == null)
                throw new ArgumentNullException("Alg");

            ////////////////////////////////////////////////
            // Find the specified element in the XmlDocument
            // object and create a new XmlElement object.
            ////////////////////////////////////////////////

            XmlNodeList list = Doc.GetElementsByTagName(ElementName);
            while (list.Count > 0)                         
            {
                XmlElement elementToEncrypt = list[0] as XmlElement;

                //XmlElement elementToEncrypt = Doc.GetElementsByTagName(ElementName)[0] as XmlElement;

                // Throw an XmlException if the element was not found.
                if (elementToEncrypt == null)
                {
                    throw new XmlException("The specified element was not found");
                }

                //////////////////////////////////////////////////
                // Create a new instance of the EncryptedXml class
                // and use it to encrypt the XmlElement with the
                // symmetric key.
                //////////////////////////////////////////////////

                EncryptedXml eXml = new EncryptedXml();

                byte[] encryptedElement = eXml.EncryptData(elementToEncrypt, Key, false);
                ////////////////////////////////////////////////
                // Construct an EncryptedData object and populate
                // it with the desired encryption information.
                ////////////////////////////////////////////////

                EncryptedData edElement = new EncryptedData();
                edElement.Type = EncryptedXml.XmlEncElementUrl;

                // Create an EncryptionMethod element so that the
                // receiver knows which algorithm to use for decryption.
                // Determine what kind of algorithm is being used and
                // supply the appropriate URL to the EncryptionMethod element.

                string encryptionMethod = null;

                if (Key is Aes)
                {
                    encryptionMethod = EncryptedXml.XmlEncAES256Url;
                }
                else
                {
                    // Throw an exception if the transform is not AES
                    throw new CryptographicException("The specified algorithm is not supported or not recommended for XML Encryption.");
                }

                edElement.EncryptionMethod = new EncryptionMethod(encryptionMethod);

                // Add the encrypted element data to the
                // EncryptedData object.
                edElement.CipherData.CipherValue = encryptedElement;

                ////////////////////////////////////////////////////
                // Replace the element from the original XmlDocument
                // object with the EncryptedData element.
                ////////////////////////////////////////////////////
                EncryptedXml.ReplaceElement(elementToEncrypt, edElement, false);

                list = Doc.GetElementsByTagName(ElementName);
            }
        }

        public static void Decrypt(XmlDocument Doc, SymmetricAlgorithm Alg)
        {
            const string ENCRYPTED_TAG = "EncryptedData";

            // Check the arguments.
            if (Doc == null)
                throw new ArgumentNullException("Doc");
            if (Alg == null)
                throw new ArgumentNullException("Alg");

            XmlNodeList list = Doc.GetElementsByTagName(ENCRYPTED_TAG);
            while (list.Count > 0)
            {
                // Find the EncryptedData element in the XmlDocument.
                XmlElement encryptedElement = list[0] as XmlElement;

                // If the EncryptedData element was not found, throw an exception.
                if (encryptedElement == null)
                {
                    throw new XmlException("The EncryptedData element was not found.");
                }

                // Create an EncryptedData object and populate it.
                EncryptedData edElement = new EncryptedData();
                edElement.LoadXml(encryptedElement);

                // Create a new EncryptedXml object.
                EncryptedXml exml = new EncryptedXml();

                // Decrypt the element using the symmetric key.
                byte[] rgbOutput = exml.DecryptData(edElement, Alg);

                // Replace the encryptedData element with the plaintext XML element.
                exml.ReplaceData(encryptedElement, rgbOutput);

                list = Doc.GetElementsByTagName(ENCRYPTED_TAG);
            }
        }

        public void Add(Entry entry)
        {
            throw new NotImplementedException();
        }

        public void Remove(Entry entry)
        {
            throw new NotImplementedException();
        }

        public void Update(Entry entry)
        {
            throw new NotImplementedException();
        }

        public IList<Entry> GetFromUser(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}