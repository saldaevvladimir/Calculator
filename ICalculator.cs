using System;

namespace Calculator
{
    public interface ICalculator
    {
        void SetFirstArgument(double firstArgument);

        double GetFirstArgument();
    
        void ClearFirstArgument();
    
        double Multiplication(double secondArgument);
    
        double Division(double secondArgument);
    
        double Sum(double secondArgument);
    
        double Subtraction(double secondArgument);

        double DegreeY(double secondArgument);
    
        double Sqrt(double argument);
    
        double Factorial(double argument);
    
        double M_Show();
    
        void M_Clear();
    
        void M_Multiplication(double secondArgument);
    
        void M_Division(double secondArgument);
    
        void M_Sum(double secondArgument);
    
        void M_Subtraction(double secondArgument);
    }
}