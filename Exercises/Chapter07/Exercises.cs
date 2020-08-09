using LaYumba.Functional;
using NUnit.Framework;
using System;

namespace Exercises.Chapter7
{
   public static class Exercises
   {
        // 1. Partial application with a binary arithmethic function:
        // Write a function `Remainder`, that calculates the remainder of 
        // integer division(and works for negative input values!). 
        public static Func<int, int, int> Remainder = (dividend, divisor)
                                                        => dividend % divisor;

        // Notice how the expected order of parameters is not the
        // one that is most likely to be required by partial application
        // (you are more likely to partially apply the divisor).

        // Write an `ApplyR` function, that gives the rightmost parameter to
        // a given binary function (try to write it without looking at the implementation for `Apply`).
        // Write the signature of `ApplyR` in arrow notation, both in curried and non-curried form
        // ((T1, T2) => R, T2) => T1 => (T1, T2) => R
        public static Func<T1, R> ApplyR<T1, T2, R>(this Func<T1, T2, R> f, T2 t2)
              => t1 => f(t1, t2);

        // Use `ApplyR` to create a function that returns the
        // remainder of dividing any number by 5. 

        // Write an overload of `ApplyR` that gives the rightmost argument to a ternary function
        public static Func<T1, T2, R> ApplyR<T1, T2, T3, R>(this Func<T1, T2, T3, R> f, T3 t3)
          => (t1, t2) => f(t1, t2, t3);

        // 2. Let's move on to ternary functions. Define a class `PhoneNumber` with 3
        // fields: number type(home, mobile, ...), country code('it', 'uk', ...), and number.
        // `CountryCode` should be a custom type with implicit conversion to and from string.
        public class PhoneNumber
        {
            public string numberType; // home, mobile, etc
            public CountryCode countryCode; // t, uk, etc
            public string number;

            public PhoneNumber(string numerType, CountryCode countryCode, string number)
            {
                this.numberType = numerType;
                this.countryCode = countryCode;
                this.number = number;
            }
        }

        public class CountryCode
        {
            public string Value;

            public CountryCode(string value)
            {
                Value = value;
            }

            public static implicit operator CountryCode(string countryCode)
                => new CountryCode(countryCode);

            public static implicit operator string(CountryCode countryCode)
                => countryCode.Value;

            public override string ToString() => Value;
        }

        // Now define a ternary function that creates a new number, given values for these fields.
        // What's the signature of your factory function? 
        // (string, CountryCode, number) => PhoneNumber
        public static Func<CountryCode, string, string, PhoneNumber> CreatePhoneNumber
            = (countryCode, numberType, number)
                    => new PhoneNumber(numberType, countryCode, number);

        // Use partial application to create a binary function that creates a UK number, 
        // and then again to create a unary function that creates a UK mobile
        public static Func<string, string, PhoneNumber> CreateUkNumber
            = CreatePhoneNumber
                .Apply((CountryCode)"uk");

        public static Func<string, PhoneNumber> CreateUkMobileNumber
            = CreateUkNumber.Apply("Mobile");

        // 3. Functions everywhere. You may still have a feeling that objects are ultimately 
        // more powerful than functions. Surely, a logger object should expose methods 
        // for related operations such as Debug, Info, Error? 
        // To see that this is not necessarily so, challenge yourself to write 
        // a very simple logging mechanism without defining any classes or structs. 
        // You should still be able to inject a Log value into a consumer class/function, 
        // exposing operations like Debug, Info, and Error, like so:

            //static void ConsumeLog(Log log) 
            //   => log.Info("look! no objects!");

        enum Level { Debug, Info, Error }


        // 5.

   }
}
