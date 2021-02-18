using System;
using _META_Helpers;

namespace CovarianceContravariance
{
    /// <summary>
    /// Contains examples of various implicit conversion behaviors, as well as covariance and contravariance.
    /// https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/covariance-contravariance/
    /// </summary>
    public class CovarianceAndContravariance
    {
        private class Fruit { }

        private class Apple : Fruit { }

        private void PrintInfo(Type expectedType, object arg)
        {
            var expectedTypeName = expectedType.GetFriendlyName();
            var argTypeName = arg.GetType().GetFriendlyName();
            var info = $"Expected object of type '{expectedTypeName}' as parameter, passed type is '{argTypeName}' - ";
            if (expectedType.IsInstanceOfType(arg))
            {
                info += "okay!";
            }
            else
            {
                info += $"WRONG! Cannot implicitly cast '{argTypeName}' to '{expectedTypeName}'.";
            }

            Console.WriteLine(info);
        }

        #region Example 1 - simple inheritance

        public void Test1()
        {
            Fruit fruit = new Fruit();
            Apple apple = new Apple();

            PrintInfo(typeof(Fruit), fruit);
            PrintInfo(typeof(Fruit), apple);
            PrintInfo(typeof(Apple), fruit);
            PrintInfo(typeof(Apple), apple);
        }

        #endregion

        #region Example 2 - standard generic interface

        private interface IStandardSample<T> { }

        private class StandardSample<T> : IStandardSample<T> { }

        public void Test2()
        {
            IStandardSample<Fruit> fruit = new StandardSample<Fruit>();
            IStandardSample<Apple> apple = new StandardSample<Apple>();

            PrintInfo(typeof(IStandardSample<Fruit>), fruit);
            PrintInfo(typeof(IStandardSample<Fruit>), apple);
            PrintInfo(typeof(IStandardSample<Apple>), fruit);
            PrintInfo(typeof(IStandardSample<Apple>), apple);
        }

        #endregion

        #region Example 3 - covariance and contravariance

        private interface ICovariantSample<out T> // 'out' indicates covariance
        {
            T Test(); // covariance only accepts generic type as output
            //void Test(T arg); // compiler error 
        }

        private interface IContravariantSample<in T> // 'in' indicates contravariance
        {
            void Test(T arg); // contravariance only accepts generic type as input
            //T Test(); // compiler error 
        }

        private class CovariantSample<T> : ICovariantSample<T>
        {
            public T Test()
            {
                throw new NotImplementedException();
            }
        }

        private class ContravariantSample<T> : IContravariantSample<T>
        {
            public void Test(T arg)
            {
                throw new NotImplementedException();
            }
        }

        public void Test3()
        {
            ICovariantSample<Fruit> covariantFruit = new CovariantSample<Fruit>();
            ICovariantSample<Apple> covariantApple = new CovariantSample<Apple>();

            PrintInfo(typeof(ICovariantSample<Fruit>), covariantFruit); // works fine
            PrintInfo(typeof(ICovariantSample<Fruit>), covariantApple); // works fine because the interface is covariant, so it accepts more derived generic type parameter Apple as Fruit
            PrintInfo(typeof(ICovariantSample<Apple>), covariantFruit); // doesn't work, can't pass less derived generic parameter type Fruit as Apple
            PrintInfo(typeof(ICovariantSample<Apple>), covariantApple); // works fine

            IContravariantSample<Fruit> contravariantFruit = new ContravariantSample<Fruit>();
            IContravariantSample<Apple> contravariantApple = new ContravariantSample<Apple>();

            PrintInfo(typeof(IContravariantSample<Fruit>), contravariantFruit); // works fine
            PrintInfo(typeof(IContravariantSample<Fruit>), contravariantApple); // doesn't work, can't pass more derived generic parameter type Apple as Fruit
            PrintInfo(typeof(IContravariantSample<Apple>), contravariantFruit); // works fine because the interface is contravariant, so it accepts less derived generic type parameter Fruit as Apple
            PrintInfo(typeof(IContravariantSample<Apple>), contravariantApple); // works fine
        }

        #endregion
    }
}