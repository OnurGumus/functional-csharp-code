using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using static Exercises.Chapter6.Exercises;
using LaYumba.Functional;
using static LaYumba.Functional.F;

namespace MyExercisesSolutions
{
    public class Chapter6Tests
    {
        [Theory]
        [InlineData(2, true)]
        [InlineData(-2, false)]
        [InlineData(0, false)]
        public void Question1(int number, bool expected)
        {
            Option<bool> isGreaterThanZero = Right(number)
                .Bind(IsNumberGreaterThanZero)
                .ToOption();

            bool isGreater = false;

            isGreater = isGreaterThanZero.Match(
                None: () => false,
                Some: (isGreaterValue) => isGreaterValue
                );

            Assert.Equal(expected, isGreater);
        }

        Either<string, bool> IsNumberGreaterThanZero(int number)
        {
            if (number > 0)
                return true;
            else
                return "Não é maior que zero";
        }

        [Fact]
        public void Question2()
        {
            Option<string> helloPhrase = Some("Heverton")
                                    .Bind(ToCaptol)
                                    .Bind(ToHeloPhrase)
                                    .Bind(ExclamationMark);

            string formatedName = helloPhrase.Match(
                                        () => null,
                                        (name) => name);

            Assert.Equal("Hello! My name is HEVERTON!!!", formatedName);
        }

        private Option<string> FormatName(string name)
        {
            if (name == null)
                return None;
            return name;
        }

        public Either<string, string> ToCaptol(string phrase)
        {
            if (phrase == null)
            {
                return Left("Não deu");
            } else
            {
                return Right(phrase.ToUpper());
            }
        }            

        private Option<string> ToHeloPhrase(string name)
            => //(name == null) ? None : Some($"Hello! My name is {name}");
                Some(name)
                    .Map((nameValue) => $"Hello! My name is {nameValue}");

        private Option<string> ExclamationMark(string phrase)
            => //(phrase == null) ? None : Some($"{phrase}!!!");
                Some(phrase)
                    .Map((phraseValue) => $"{phraseValue}!!!");
            
        [Fact]
        public void Question3Exception()
        {
            string exceptionPrase = 
                TryRun<string>(() => throw new Exception("DeuRuim")).Match(
                    Exception: (Exception ex) => ex.Message,
                    Success: _ => "DeuCerto");

            Assert.Equal("DeuRuim", exceptionPrase);
        }

        [Fact]
        public void Question3WithoutException()
        {
            string exceptionPrase =
                TryRun<string>(() => "DeuRuim").Match(
                    Exception: (Exception ex) => ex.Message,
                    Success: _ => "DeuCerto");

            Assert.Equal("DeuCerto", exceptionPrase);
        }

        [Fact]
        public void Question4WithoutException()
        {
            Func<string> firstFunction = () => "primeira";
            Func<Exception, string> handler = (ex) => $"erro: {ex.Message}";

            string resultMessage = Safely(firstFunction, handler).Match(
                Left: error => error,
                Right: resultCorrectMessage => resultCorrectMessage);

            Assert.Equal("primeira", resultMessage);
        }

        [Fact]
        public void Question4WithException()
        {
            Func<string> firstFunction = () => throw new Exception("DeuRuim");
            Func<Exception, string> handler = (ex) => $"erro: {ex.Message}";

            string resultMessage = Safely(firstFunction, handler).Match(
                Left: error => error,
                Right: resultCorrectMessage => resultCorrectMessage);

            Assert.Equal("erro: DeuRuim", resultMessage);
        }
    }
}
