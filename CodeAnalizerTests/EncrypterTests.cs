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
    }
}
