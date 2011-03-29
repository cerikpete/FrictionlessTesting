using NUnit.Framework;
using Rhino.Mocks;
using StructureMap.AutoMocking;
using Tests.BaseClasses;

namespace Tests.Specs.BaseClasses
{
    [Category("Specifications")]
    public abstract class SpecificationsFor<SystemUnderTest> where SystemUnderTest : class
    {
        protected SystemUnderTest sut;
        private RhinoAutoMocker<SystemUnderTest> mockSource;
        protected ReflectiveMapper _reflectiveMapper;

        [SetUp]
        public void SetUp()
        {
            _reflectiveMapper = new ReflectiveMapper();
            sut = SetUpSystemUnderTestWithMockedDependencies();
            Set_up_context();
            And_calling();
        }

        protected InterfaceType AMockOf<InterfaceType>() where InterfaceType : class
        {
            return MockRepository.GenerateMock<InterfaceType>();
        }

        protected DependencyType AMockedDependencyOfType<DependencyType>() where DependencyType : class
        {
            return mockSource.Get<DependencyType>();
        }

        protected SystemUnderTest SetUpSystemUnderTestWithMockedDependencies()
        {
            mockSource = new RhinoAutoMocker<SystemUnderTest>();
            ConfigureAutoMocker(mockSource);
            return mockSource.ClassUnderTest;
        }

        protected ExpectationBuilder<DependencyType> WhenThe<DependencyType>(DependencyType dependency) where DependencyType : class
        {
            return new ExpectationBuilder<DependencyType>(dependency);
        }

        public virtual void Set_up_context() { }

        public abstract void And_calling();

        protected void SetField(object target, string fieldName, object value)
        {
            _reflectiveMapper.SetField(target, fieldName, value);
        }

        protected void SetIdField(object target, string fieldName, int value)
        {
            _reflectiveMapper.SetField(target, fieldName, value);
        }

        /// <summary>
        /// Provides the ability to do any additional configuration on the <see cref="AutoMocker{TARGETCLASS}" />
        /// before it is used.
        /// </summary>
        /// <param name="mocker">The <see cref="AutoMocker{TARGETCLASS}" /> instance to be used.</param>
        protected virtual void ConfigureAutoMocker(AutoMocker<SystemUnderTest> mocker) { }
    }
}