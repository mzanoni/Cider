using Cider;
using Xunit;

namespace Tests;

public class EnumeratedTester
{
    public class EnumeratedSubject : Enumerated<EnumeratedSubject>
    {
        public static readonly EnumeratedSubject One = new EnumeratedSubject("one");
        public static readonly EnumeratedSubject Two = new EnumeratedSubject("two");
        public static readonly EnumeratedSubject Three = new EnumeratedSubject("three");

        private EnumeratedSubject(string name) : base(name) { }
    }

    [Fact]
    public void Values_HasAllOptions()
    {
        Assert.InRange(EnumeratedSubject.Values.Length, 3, 3);
    }

    [Fact]
    public void Parse_One_Success()
    {
        Assert.Equal(EnumeratedSubject.Parse("one"), EnumeratedSubject.One);
    }

    [Fact]
    public void Parse_Ones_Fails()
    {
        Assert.Throws<KeyNotFoundException>(() => EnumeratedSubject.Parse("ones"));
    }

    [Fact]
    public void TryParse_One_Success()
    {
        EnumeratedSubject.TryParse("one", out EnumeratedSubject? value);
        Assert.Equal(value, EnumeratedSubject.One);
    }

    [Fact]
    public void TryParse_Ones_Null()
    {
        EnumeratedSubject.TryParse("ones", out EnumeratedSubject? value);
        Assert.Null(value);
    }
}