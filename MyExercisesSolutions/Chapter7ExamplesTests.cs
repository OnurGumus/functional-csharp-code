using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using static Exercises.Chapter6.Exercises;
using LaYumba.Functional;
using static LaYumba.Functional.F;

using Name = System.String;
using Greeting = System.String;
using PersonalizedGreeting = System.String;

namespace MyExercisesSolutions
{
    public class Chapter7ExamplesTests
    {
        [Fact]
        public void BinaryFunctionMappedOverAList()
        {
            Func<Greeting, Name, PersonalizedGreeting> greet
                = (gr, name) => $"{gr}, {name}";
            Name[] names = { "Tristan", "Ivan" };
            List<string> writedPhrases = new List<string>();

            names.Map(g => greet("Hello", g)).ForEach(
                (greeting) => writedPhrases.Add(greeting)
                );

            Assert.Equal("Hello, Tristan", writedPhrases[0]);
            Assert.Equal("Hello, Ivan", writedPhrases[1]);
        }

        [Fact]
        public void ManuallyEnablingPartialApplication()
        {
            List<string> writedPhrases = new List<string>();
            Name[] names = { "Tristan", "Ivan" };
            Func<Greeting, Func<Name, PersonalizedGreeting>> greetWith
                = gr => name => $"{gr}, {name}";
            var greetFormally = greetWith("Good evening");            

            names.Map(greetFormally).ForEach(
                (greeting) => writedPhrases.Add(greeting));

            Assert.Equal("Good evening, Tristan", writedPhrases[0]);
            Assert.Equal("Good evening, Ivan", writedPhrases[1]);
        }

        [Fact]
        public void UsingApply()
        {
            List<string> writedPhrases = new List<string>();
            Name[] names = { "Tristan", "Ivan" };
            Func<Greeting, Name, PersonalizedGreeting> greet
                = (gr, name) => $"{gr}, {name}";

            var greetInformally = greet.Apply("Hey");
            names.Map(greetInformally).ForEach(
                (greeting) => writedPhrases.Add(greeting));

            Assert.Equal("Hey, Tristan", writedPhrases[0]);
            Assert.Equal("Hey, Ivan", writedPhrases[1]);
        }
    }
}
