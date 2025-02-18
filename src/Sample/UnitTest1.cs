namespace Sample;

public class UnitTest1
{
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public void Test1(int value)
    {
        Assert.True(value > 0);
    }

    [Fact]
    public void FailsOnce()
    {
        if (!File.Exists("failsonce.txt"))
        {
            File.WriteAllText("failsonce.txt", "");
            Assert.Fail("Fails once");
        }

        File.Delete("failsonce.txt");
    }

    [Fact]
    public void FailsTwice()
    {
        // Add random delay to simulate actual test execution
        Thread.Sleep(Random.Shared.Next(1000, 5000));

        var attempt = int.Parse(
            File.Exists("failstwice.txt") ?
            File.ReadAllText("failstwice.txt") :
            "0");

        if (attempt < 2)
        {
            File.WriteAllText("failstwice.txt", (attempt + 1).ToString());
            Assert.Fail("Fails twice");
        }

        // Succeeds
        File.Delete("failstwice.txt");
    }

    public static IEnumerable<object[]> GetNumbers() => Enumerable.Range(0, 1100).Select(x => new object[] { x });

    [Theory]
    [MemberData(nameof(GetNumbers))]
    public void NumberIsPositive(int value)
    {
        Assert.True(value >= 0);
    }
}