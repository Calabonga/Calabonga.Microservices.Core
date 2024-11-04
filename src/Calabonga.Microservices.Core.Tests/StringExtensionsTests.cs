using Calabonga.Microservices.Core.Extensions;

namespace Calabonga.Microservices.Core.Tests;

public class StringExtensionsTests
{
    [Fact]
    public void It_IsNullOrEmpty_should_returns_true_when_not_empty()
    {
        // arrange
        var testString = "some text";
        var expected = false;

        // act
        var actual = testString.IsNullOrEmpty();


        // assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void It_IsNullOrEmpty_should_returns_false_when_not_empty()
    {
        // arrange
        const string testString = "";
        const bool expected = true;

        // act
        var actual = testString.IsNullOrEmpty();


        // assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void It_IsNullOrEmpty_should_returns_false_when_not_null()
    {
        // arrange
        const string testString = null;
        const bool expected = true;

        // act
        var actual = testString.IsNullOrEmpty();


        // assert
        Assert.Equal(expected, actual);
    }


    [Fact]
    public void It_should_returns_true_when_not_empty()
    {
        // arrange
        const string testString = "some text";
        const bool expected = true;

        // act
        var actual = testString.IsNotNullOrEmpty();


        // assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void It_should_returns_false_when_not_empty()
    {
        // arrange
        const string testString = "";
        const bool expected = false;

        // act
        var actual = testString.IsNotNullOrEmpty();


        // assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void It_should_returns_false_when_not_null()
    {
        // arrange
        const string testString = null;
        const bool expected = false;

        // act
        var actual = testString.IsNotNullOrEmpty();


        // assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void It_should_returns_true_when_enumerable_is_null()
    {
        // arrange
        IEnumerable<int> testString = null;
        const bool expected = true;

        // act
        var actual = testString.IsNullOrEmpty();


        // assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void It_should_returns_false_when_not_null_enumerable()
    {
        // arrange
        IEnumerable<int> testString = new List<int> { 1, 3, 4, 56 };
        const bool expected = false;

        // act
        var actual = testString.IsNullOrEmpty();


        // assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void It_should_returns_false_when_null_enumerable()
    {
        // arrange
        IEnumerable<int> testString = new List<int> { 1, 3, 4, 56 };
        const bool expected = true;

        // act
        var actual = testString.IsNotNullOrEmpty();


        // assert
        Assert.Equal(expected, actual);

    }
}