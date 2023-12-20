using System;
using System.Collections.Generic;

namespace TaskListApp
{
    class Program
    {
        static List<string> tasks = new List<string>();

        static void Main(string[] args)
        {
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("Task List Application");
                Console.WriteLine("---------------------");
                Console.WriteLine("1. Add Task");
                Console.WriteLine("2. View Tasks");
                Console.WriteLine("3. Update Task");
                Console.WriteLine("4. Delete Task");
                Console.WriteLine("5. Exit");
                Console.WriteLine();

                Console.Write("Enter your choice: ");
                int choice = int.Parse(Console.ReadLine());
                Console.WriteLine();

                switch (choice)
                {
                    case 1:
                        AddTask();
                        break;
                    case 2:
                        ViewTasks();
                        break;
                    case 3:
                        UpdateTask();
                        break;
                    case 4:
                        DeleteTask();
                        break;
                    case 5:
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }

                Console.WriteLine();
            }
        }

        static void AddTask()
        {
            Console.Write("Enter task description: ");
            string task = Console.ReadLine();
            tasks.Add(task);
            Console.WriteLine("Task added successfully!");
        }

        static void ViewTasks()
        {
            if (tasks.Count == 0)
            {
                Console.WriteLine("No tasks found.");
            }
            else
            {
                Console.WriteLine("Tasks:");
                foreach (string task in tasks)
                {
                    Console.WriteLine(task);
                }
            }
        }

        static void UpdateTask()
        {
            Console.Write("Enter task index to update: ");
            int index = int.Parse(Console.ReadLine());

            if (index >= 0 && index < tasks.Count)
            {
                Console.Write("Enter new task description: ");
                string newTask = Console.ReadLine();
                tasks[index] = newTask;
                Console.WriteLine("Task updated successfully!");
            }
            else
            {
                Console.WriteLine("Invalid task index. Please try again.");
            }
        }

        static void DeleteTask()
        {
            Console.Write("Enter task index to delete: ");
            int index = int.Parse(Console.ReadLine());

            if (index >= 0 && index < tasks.Count)
            {
                tasks.RemoveAt(index);
                Console.WriteLine("Task deleted successfully!");
            }
            else
            {
                Console.WriteLine("Invalid task index. Please try again.");
            }
        }
    }
}
