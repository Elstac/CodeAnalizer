using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using NUnit.Framework;
using CodeAnalizer;
namespace CodeAnalizerTests
{
    [TestFixture]
    class EncrypterTests
    {
        [Test]
        public void EncryptTest()
        {
            string input = "narkotik\n", key = "kwas", output, expected = "ZYT_[lK_!";

            output = Encrypter.Encrypt(input, key.ToArray());

            Assert.AreEqual(expected, output);
        }
        [Test]
        public void DecryptTest()
        {
            string input = "ZYT_[lK_!", key = "kwas", output, expected = "narkotik\n";
            output = Encrypter.Decrypt(input, key.ToArray());
            Assert.AreEqual(expected, output);
        }
        [Test]
        public void ToRnageTest()
        {
            Assert.AreEqual(24, Encrypter.NumberToRange(19, 20, 24));
        }
        [Test]
        public void MassEncryptTest()
        {
            List<string> texts = new List<string>();
            List<string> key = new List<string>();
            List<string> output = new List<string>();
            for (int i = 10; i <= 40; i++)
            {
                texts.Add(GetRandomString(i));
                key.Add(GetRandomString(i/4));
            }

            string tmp;
            for (int i = 0; i < texts.Count; i++)
            {
                tmp = Encrypter.Encrypt(texts[i], key[i].ToArray());
                tmp = Encrypter.Decrypt(tmp, key[i].ToArray());
                output.Add(tmp);
            }
            Assert.AreEqual(texts, output);
        }

        private string GetRandomString(int length)
        {
            Random rn = new Random(length);
            string ret = "";
            for (int i = 0; i < length; i++)
            {
                ret += (char)((rn.Next()+33)%127);
            }
            return ret;
        }
    }
}
