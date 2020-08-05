using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using static Exercises.Chapter7.Exercises;
using LaYumba.Functional;
using static LaYumba.Functional.F;

namespace MyExercisesSolutions
{
    public class Chapter7Tests
    {
        [Theory]
        [InlineData(5, 0)]
        [InlineData(-5, 0)]
        [InlineData(6, 1)]
        [InlineData(-6, -1)]
        public void RemainderOfDivisionOfOneNumberByFive(int firstNumber, int expectedResult)
        {
            Func<int, int> restOfDivisionByFive = Remainder.ApplyR(5);
            int remainder = restOfDivisionByFive(firstNumber);

            Assert.Equal(expectedResult, remainder);
        }

        Func<int, int, int, int> sumThreeNumbers
            = (first, second, third)
            => first + second + third;
        [Theory]
        [InlineData(1, 2, 3, 6)]
        [InlineData(1, 2, -3, 0)]
        public void SumThreeNumbersWithApplyR(int firstNumber, int secondNumber, int thirdNumber, int expectedResult)
        {

            Func<int, int, int> sumTwoNumbers = sumThreeNumbers.ApplyR(thirdNumber);
            int result = sumTwoNumbers(firstNumber, secondNumber);

            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData("mobile", "uk", "983963550", "983963550")]
        public void PartialCreatePhoneNumber(string numberType, string countryCode, string number, string expectedResultPhoneNumber)
        {
            PhoneNumber resultPhoneNumber = CreatePhoneNumber.ApplyR(number)
                                                                (numberType, countryCode);

            Assert.Equal(expectedResultPhoneNumber, resultPhoneNumber.number);
            Assert.Equal(numberType, resultPhoneNumber.numberType);
            Assert.Equal(countryCode, resultPhoneNumber.countryCode);
        }
    }
}
