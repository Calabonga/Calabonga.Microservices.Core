using System.Runtime.Serialization;
using Calabonga.Utils.Extensions;
using Shouldly;
using Xunit.Abstractions;

namespace Calabonga.Microservices.Core.Tests
{
    public class EnumHelperType2Tests
    {
        private readonly ITestOutputHelper _outputHelper;

        public EnumHelperType2Tests(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        [Fact]
        [Trait("EnumHelper", "Parsing")]
        public void ItShould_be_under_testing()
        {
            // arrange

            // act
            var sut = EnumHelper<TestType2>.Parse("Value1");

            // assert
            sut.ShouldBe(TestType2.Value1);
        }

        [Fact]
        [Trait("EnumHelper", "Parsing")]
        public void ItShould_extract_attribute_by_string()
        {
            // arrange

            var value = TestType2.Value2;

            // act
            var sut = EnumHelper<TestType2>.TryGetFromAttribute<EnumMemberAttribute>(value.ToString());

            // assert
            sut.ShouldBe(null);
        }

        [Fact]
        [Trait("EnumHelper", "Parsing")]
        public void ItShould_be_parsed_but_not_equals_to_Value()
        {
            // arrange
            var value = TestType2.Value3;

            // act

            var sut = EnumHelper<TestType2>.GetDisplayValue(value);

            // assert
            sut.ShouldBe("Значение3");
        }

        [Fact]
        [Trait("EnumHelper", "Parsing")]
        public void ItShould_not_be_parsed()
        {
            // arrange

            // act

            // assert
            Assert.Throws<ArgumentException>(() => EnumHelper<TestType2>.Parse("NOT_FOUND"));
        }

        [Fact]
        [Trait("EnumHelper", "Parsing")]
        public void ItShould_parse_DisplayAttribute()
        {
            // arrange

            // act
            var sut = EnumHelper<TestType2>.Parse("Значение4");

            // assert
            sut.ShouldBe(TestType2.Value4);
        }

        [Fact]
        [Trait("EnumHelper", "Parsing")]
        public void ItShould_parse_Value3()
        {
            // arrange

            // act
            var sut = EnumHelper<TestType2>.Parse("Value3");

            // assert
            sut.ShouldBe(TestType2.Value3);
        }

        [Fact]
        [Trait("EnumHelper", "Parsing")]
        public void ItShould_parse_DisplayAttribute_Value3()
        {
            // arrange

            // act
            var sut = EnumHelper<TestType2>.Parse("Значение3");

            // assert
            sut.ShouldNotBe(TestType2.Value2);
        }

        [Fact]
        [Trait("EnumHelper", "Parsing")]
        public void ItShould_parse_DisplayAttribute_None()
        {
            // arrange

            // act
            var sut = EnumHelper<TestType2>.GetDisplayValue(TestType2.Value3);

            // assert
            sut.ShouldBe("Значение3");
        }

        [Fact]
        [Trait("EnumHelper", "Parsing")]
        public void ItShould_return_DisplayAttributes()
        {
            // arrange

            // act
            var sut = EnumHelper<TestType2>.GetDisplayValues();
            _outputHelper.WriteLine("{0}", string.Join(" ", sut));

            // assert
            sut.ShouldNotBeEmpty();
        }

        [Fact]
        [Trait("EnumHelper", "Parsing")]
        public void ItShould_return_DisplayAttributes_4_items()
        {
            // arrange

            // act
            var sut = EnumHelper<TestType2>.GetDisplayValues();
            _outputHelper.WriteLine("{0}", string.Join(" ", sut));

            // assert
            sut.Count.ShouldBe(4);
        }

        [Fact]
        [Trait("EnumHelper", "Parsing")]
        public void ItShould_return_DisplayAttributes_4_items_with_values()
        {
            // arrange
            var flags = TestType2.Value1 | TestType2.Value2 | TestType2.Value3;

            // act
            var sut1 = EnumHelper<TestType2>.GetUniqueFlags(flags).ToList();
            var result = sut1.Select(EnumHelper<TestType2>.GetDisplayValue).ToList();

            // assert
            result.Count.ShouldBe(3);
        }

        [Fact]
        [Trait("EnumHelper", "Parsing")]
        public void ItShould_return_DisplayAttributes_4_items_with_values_and_be_unique()
        {
            // arrange
            var flags = TestType2.Value1 | TestType2.Value2 | TestType2.Value3;

            // act
            var sut1 = EnumHelper<TestType2>.GetUniqueFlags(flags).ToList();
            var result = sut1.Select(EnumHelper<TestType2>.GetDisplayValue).ToList();

            // assert
            result.ShouldBeUnique();
        }

        [Fact]
        [Trait("EnumHelper", "Parsing")]
        public void ItShould_return_DisplayAttributes_4_items_with_values_and_contains_Value1()
        {
            // arrange
            var flags = TestType2.Value1 | TestType2.Value2 | TestType2.Value3;

            // act
            var sut1 = EnumHelper<TestType2>.GetUniqueFlags(flags).ToList();
            var result = sut1.Select(EnumHelper<TestType2>.GetDisplayValue).ToList();

            // assert
            result.ShouldContain("Значение1");
            result.ShouldContain("Значение2");
            result.ShouldContain("Значение3");
            result.ShouldNotContain("Значение4");
        }

        [Fact]
        [Trait("EnumHelper", "Parsing")]
        public void ItShould_not_return_flags_for_enum()
        {
            // arrange
            var notFlags = TestType.Value;

            // act
            var sut1 = EnumHelper<TestType2>.GetUniqueFlags(notFlags);

            // assert
            sut1.ToList().Count.ShouldBe(0);
        }
    }
}