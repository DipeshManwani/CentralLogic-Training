//Write a program to prompt the user for two numbers and then
//print the square of sum of numbers.



class Program
{
    static void Main()
    {
        Console.Write("Enter the first number: ");
        double firstNumber = Convert.ToDouble(Console.ReadLine());

        Console.Write("Enter the second number: ");
        double secondNumber = Convert.ToDouble(Console.ReadLine());

        double sum = firstNumber + secondNumber;
        double squareOfSum = Math.Pow(sum, 2);

        Console.WriteLine($"The square of the sum of {firstNumber} and {secondNumber} is {squareOfSum}");
    }
}
