using Cider.Sql;
using Xunit;

namespace Tests.Sql;

public class SqlTextTester
{
    [Fact]
    public void Where()
    {
        Assert.Equal(string.Empty, SqlText.Empty);
    }

    [Fact]
    public void Where_1EQ1()
    {
        SqlText subject = SqlText.Empty.Where("1 = 1");

        Assert.Equal("WHERE (1 = 1)", subject);
    }

    [Fact]
    public void Where_1EQ1_OR_2EQ2()
    {
        SqlText subject = SqlText.Empty
            .Where("1 = 1")
            .OrWhere("2 = 2");

        Assert.Equal("WHERE (1 = 1) OR (2 = 2)", subject);
    }

    [Fact]
    public void Where_1EQ1_AND_2EQ2()
    {
        SqlText subject = SqlText.Empty
            .Where("1 = 1")
            .Where("2 = 2");

        Assert.Equal("WHERE (1 = 1) AND (2 = 2)", subject);
    }

    [Fact]
    public void Where_NestedWhere()
    {
        SqlText subject = SqlText.Empty
            .Where("1 = 1")
            .Where(w => w
                .Where("X = X")
                .Where("Y = Y"));

        Assert.Equal("WHERE (1 = 1) AND ((X = X) AND (Y = Y))", subject);
    }

    [Fact]
    public void IfWhere_True_1EQ1()
    {
        SqlText subject = SqlText.Empty
            .IfWhere(true, "1 = 1");

        Assert.Equal("WHERE (1 = 1)", subject);
    }

    [Fact]
    public void IfWhere_False_IsEmpty()
    {
        SqlText subject = SqlText.Empty
            .IfWhere(false, "1 = 1");

        Assert.Equal(string.Empty, subject);
    }

    [Fact]
    public void IfNotWhere_True_Empty()
    {
        SqlText subject = SqlText.Empty
            .IfNotWhere(true, "1 = 1");

        Assert.Equal(string.Empty, subject);
    }

    [Fact]
    public void IfNotWhere_False_1EQ1()
    {
        SqlText subject = SqlText.Empty
            .IfNotWhere(false, "1 = 1");

        Assert.Equal("WHERE (1 = 1)", subject);
    }

    [Fact]
    public void BuildConditions_1EQ1_NoWhere()
    {
        SqlText subject = SqlText.Empty.Where("1 = 1");

        Assert.Equal("(1 = 1)", subject.BuildConditions());
    }
}