public class Utility
{
    public static Double CalculateCompoundInterest(Double principle, Double interestRate, int noOfYears)
      => (principle) * Math.Pow((1 + (interestRate) / 100), noOfYears);

    public static int CalculateAge(DateTime dateOfBirth)
    {
        int calculatedAge = DateTime.Today.Year - dateOfBirth.Year;
        if (dateOfBirth > DateTime.Today.AddYears(-calculatedAge))
            calculatedAge--;
        
        return calculatedAge;
    }
}
