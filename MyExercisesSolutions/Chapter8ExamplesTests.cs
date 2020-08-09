using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using LaYumba.Functional;
using static LaYumba.Functional.F;
using FsCheck.Xunit;

namespace MyExercisesSolutions
{
    public class Chapter8ExamplesTests
    {
        Func<int, int> @double = i => i * 2;

        [Fact]
        public void DoubleNumberWithMap()
        {
            Option<int> doubleInteger = Some(3).Map(@double);

            Assert.Equal(Some(6), doubleInteger);
        }

        Func<int, Func<int, int>> multiply = x => y => x * y;
        Func<int, int, int> multiplyNotCurried = (x, y) => x * y;

        [Fact]
        public void MappingACurriedFunctionOntoAOption()
        {
            var multBy3 = Some(3)
                            .Map(multiply);

            Option<int> result = multBy3.Apply(Some(4));

            Assert.Equal(Some(12), result);
        }

        [Fact]
        public void MappingANotCurriedFunctionOntoAOption()
        {
            var multBy3 = Some(3)
                            .Map(multiplyNotCurried);

            Option<int> result = multBy3.Apply(Some(4));

            Assert.Equal(Some(12), result);
        }

        [Fact]
        public void WhenNoneValueIsPassedANoneIsReturned()
        {
            var multBy3 = Some(3)
                            .Map(multiplyNotCurried);

            Option<int> result = multBy3.Apply(None);

            Assert.Equal(None, result);
        }
        
        [Property]
        public void ApplicativeLawHolds(int a, int b)
        {
            var first = Some(multiplyNotCurried)
                .Apply(Some(a))
                .Apply(Some(b));
            var second = Some(a)
                .Map(multiplyNotCurried)
                .Apply(b);

            Assert.Equal(first, second);
        }
    }
}
