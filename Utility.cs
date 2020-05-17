public class Utility
{
          static Double CalculateCompoundInterest(Double principle, Double interestRate, int noOfYears) 
            => (principle) * Math.Pow((1 + (interestRate) / 100), noOfYears);
}
