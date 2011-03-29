using System;
using Rhino.Mocks;

namespace Tests.Specs.BaseClasses
{
    public static class AssertionExtensions
    {
        public static void WasCalled<T>(this T objectUnderTest, Action<T> action)
        {
            objectUnderTest.AssertWasCalled(action);
        }

        public static void WasCalledWithAnyArguments<T>(this T objectUnderTest, Action<T> action)
        {
            objectUnderTest.AssertWasCalled(action, c => c.IgnoreArguments());
        }

        public static void WasCalledWithAnyArgumentsXTimes<T>(this T objectUnderTest, Action<T> action, int numberOfTimesExpected)
        {
            objectUnderTest.AssertWasCalled(action, c => c.IgnoreArguments().Repeat.Times(numberOfTimesExpected));
        }

        public static void WasNotCalled<T>(this T objectUnderTest, Action<T> action)
        {
            objectUnderTest.AssertWasNotCalled(action);
        }

        public static void WasNotCalledWithAnyArguments<T>(this T objectUnderTest, Action<T> action)
        {
            objectUnderTest.AssertWasNotCalled(action, c => c.IgnoreArguments());
        }
    }
}