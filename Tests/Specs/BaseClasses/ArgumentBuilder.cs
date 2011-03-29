using System;
using Rhino.Mocks;
using Rhino.Mocks.Constraints;
using Rhino.Mocks.Interfaces;

namespace Tests.Specs.BaseClasses
{
    public class ArgumentBuilder<TypeToMock, ResultOfAction> where TypeToMock : class
    {
        private readonly TypeToMock mock;
        private readonly Function<TypeToMock, ResultOfAction> action;


        public ArgumentBuilder(TypeToMock mock, Function<TypeToMock, ResultOfAction> action)
        {
            this.mock = mock;
            this.action = action;
        }

        public IMethodOptions<ResultOfAction> Return(ResultOfAction result)
        {
            return mock.Stub(action).Return(result);
        }

        public IMethodOptions<ResultOfAction> IgnoreArgumentsAndReturn(ResultOfAction result)
        {
            return Return(result).IgnoreArguments();
        }

        public IMethodOptions<ResultOfAction> Do(Delegate actionToPerform)
        {
            return mock.Stub(action).Do(actionToPerform);
        }

        public IMethodOptions<ResultOfAction> IgnoreArgumentsAndDo(Delegate actionToPerform)
        {
            return mock.Stub(action).Constraints(Is.Anything()).Do(actionToPerform);
        }

        public IMethodOptions<ResultOfAction> ActAsAProperty()
        {
            return mock.Stub(action).PropertyBehavior();
        }
    }
}