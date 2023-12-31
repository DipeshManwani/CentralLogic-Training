﻿/*Write a program to Make simple calculator which which takes two
a numbers as an input. (add, subtract, multiply, divide, modulus) 
  Note: Print modulus value in decimals.*/


class Calculator
{
    static void Main()
    {
        Console.Write("Enter the first number: ");
        double num1 = Convert.ToDouble(Console.ReadLine());

        Console.Write("Enter the second number: ");
        double num2 = Convert.ToDouble(Console.ReadLine());

        Console.WriteLine("Choose an operation:");
        Console.WriteLine("1. Addition");
        Console.WriteLine("2. Subtraction");
        Console.WriteLine("3. Multiplication");
        Console.WriteLine("4. Division");
        Console.WriteLine("5. Modulus");
        Console.Write("Enter the operation (1-5): ");

        int choice = Convert.ToInt32(Console.ReadLine());

        double result = 0;

        switch (choice)
        {
            case 1:
                result = num1 + num2;
                Console.WriteLine($"Addition: {num1} + {num2} = {result}");
                break;
            case 2:
                result = num1 - num2;
                Console.WriteLine($"Subtraction: {num1} - {num2} = {result}");
                break;
            case 3:
                result = num1 * num2;
                Console.WriteLine($"Multiplication: {num1} * {num2} = {result}");
                break;
            case 4:
                if (num2 != 0)
                {
                    result = num1 / num2;
                    Console.WriteLine($"Division: {num1} / {num2} = {result}");
                }
                else
                {
                    Console.WriteLine("Error: Division by zero is not allowed.");
                }
                break;
            case 5:
                result = num1 % num2;
                Console.WriteLine($"Modulus: {num1} % {num2} = {result}");
                break;
            default:
                Console.WriteLine("Invalid choice. Please select a valid operation (1-5).");
                break;
        }
    }
}
