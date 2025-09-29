using System.Security.Cryptography;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace Sample;

[TestFixture]
public class NUnitTest
{
    [TestCase("this test, shouldn't break")]
    [TestCase("successful case")]
    public void ParameterEscapingRetries(string value)
    {
        // get a simple sha from the string to use as filename using the hex value from the sha1 of the value
        var file = "failed" + string.Concat(SHA1.HashData(System.Text.Encoding.UTF8.GetBytes(value)).Select(b => b.ToString("x2"))) + ".txt";

        if (!File.Exists(file))
        {
            File.WriteAllText(file, "");
            Assert.Fail("Fails once");
        }

        File.Delete(file);
    }

}
