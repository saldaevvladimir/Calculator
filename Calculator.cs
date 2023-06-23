using System;

namespace Calculator
{
    public class Calculator : ICalculator
    {
        private double firstArgument = 0;
        private double memory = 0;
    
        public void SetFirstArgument(double firstArgument)
        {
            this.firstArgument = firstArgument;
        }

        public double GetFirstArgument()
        {
            return this.firstArgument;
        }
    
        public void ClearFirstArgument()
        {
            firstArgument = 0;
        }
    
        public double Multiplication(double secondArgument)
        {
            return firstArgument *secondArgument;
        }
    
        public double Division(double secondArgument)
        {
            return firstArgument / secondArgument;
        }
    
        public double Sum(double secondArgument)
        {
            return firstArgument + secondArgument;
        }
    
        public double Subtraction(double secondArgument)
        {
            return firstArgument - secondArgument;
        }
    
        public double DegreeY(double secondArgument)
        {
            return Math.Pow(firstArgument, secondArgument);
        }
    
        public double Sqrt(double argument)
        {
            return Math.Sqrt(argument);
        }
    
        public double Factorial(double argument)
        {
            double factorial = 1;
    
            for (int i = 1; i <= argument; i++)
                factorial *= (double)i;
    
            return factorial;
        }
    
        public double M_Show()
        {
            return memory;
        }

        public void M_Clear()
        {
            memory = 0.0;
        }

        public void M_Multiplication(double secondArgument)
        {
            memory *= secondArgument;
        }
    
        public void M_Division(double secondArgument)
        {
            memory /= secondArgument;
        }
    
        public void M_Sum(double secondArgument)
        {
            memory += secondArgument;
        }
    
        public void M_Subtraction(double secondArgument)
        {
            memory -= secondArgument;
        }
    }
}
