using System;
using System.Collections.Generic;
using System.Text;
using LaYumba.Functional;
using static LaYumba.Functional.F;
using FsCheck;

namespace MyExercisesSolutions
{
    static class ArbitraryOption
    {
        public static Arbitrary<Option<T>> Option<T>()
        {
            var gen = from iSSome in Arb.Generate<bool>()
                      from val in Arb.Generate<T>()
                      select iSSome && val != null ? Some(val) : None;
            return gen.ToArbitrary();
        }
    }
}
