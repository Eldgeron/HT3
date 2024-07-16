// See https://aka.ms/new-console-template for more information
//Cceate user and password list
using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Transactions;

internal class Program
{
    private static void Main(string[] args)
    {
        Dictionary<string, string> userDataBase = new Dictionary<string, string>();
        Dictionary<string, double> userBankAccount = new Dictionary<string, double>();

        userDataBase.Add("Godzilla", "password");
        userDataBase.Add("useruser2", "password2");
        userBankAccount.Add("user1", 0);
        userBankAccount.Add("user2", 0);

        foreach (KeyValuePair<string, string> ele1 in userDataBase)
        {
            Console.WriteLine("for {0} the password is {1}", ele1.Key, ele1.Value);
        }
        //variables
        string userAnswer;
        string userName;
        string password;
        bool answerCompair = true;
        bool transitionKey = true;
        string specialCharacters;
        
        //Welcome screen aka Start menu
        Console.WriteLine("Welcome to Super ATM");

        do
        {
            Console.WriteLine("Please, choose the opthion:" +
            "\n Sign in --> press 1" +
            "\n Log in  --> press 2" +
            "\n Exit    --> press E");
            userAnswer = Console.ReadLine().ToLower();
            switch (userAnswer)
            {
                //User choose Sign In
                case "1":
                    SignIn(userDataBase, userBankAccount);
                    transitionKey = false;
                    break;
                //User choose Log In
                case "2":
                    Login(userDataBase, userBankAccount);
                    break;
                //User choose Exit the program
                case "e":
                    transitionKey = true;
                    break;
                //User press irrelevant button
                default:
                    Console.WriteLine("something went complitly wrong");
                    break;
            }
        }
        while (!transitionKey);
    }
    public static void SignIn (Dictionary<string, string> dictionary, Dictionary<string, double> userAccount)
    {
        bool validKey = true;
        
        Console.WriteLine("Registration form. " +
                    "\nUsername Requirements: " +
                    "\n-- can be simple " +
                    "\n-- must containe only latin letters" +
                    "\n-- must be unique " +
                    "\n-- not case-sensitive " +
                    "\n-- must be from 8 to 13 characters " +
                    "\n-- can't be only from white spaces" +
                    "\nPassword requirements" +
                    "\n-- must be from 8 to 13 characters " +
                    "\n-- must containe only latin letters, digits and symbols" +
                    "\n-- case-sensitive " +
                    "\n-- can't be only from white spacec");
        do
        {
            validKey = false;
            Console.WriteLine("Enter a Username:");
            string userName = Console.ReadLine();
            if (ValidUsername(dictionary, userName))
            {
                do
                {
                    validKey = false;
                    Console.WriteLine("Enter a password");
                    string userPassword = Console.ReadLine();
                    if (ValidPassword(userPassword))
                    {
                        dictionary.Add(userName, userPassword);
                        userAccount.Add(userName, 0);
                        Console.WriteLine("New User was created");
                        validKey = true;
                    }
                }
                while (!validKey);
            }
        }
        while (!validKey);
    }
    public static bool ValidUsername (Dictionary<string, string> dictionary, string input)
    {
        bool compairAnswer = true;
        if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input))
        {
            Console.WriteLine("Username can't be empty or be only from spaces");
            compairAnswer = false;
        }
        else if (input.Length < 8 || input.Length >= 13)
        {
            Console.WriteLine("Username must be from 8 to 13 characters");
            compairAnswer = false;
        }
        else if (dictionary.ContainsKey(input))
        {
            Console.WriteLine("This usernsme is used. Try another username");
            compairAnswer = false;
        }
        else if (!input.All(char.IsLetter) || isContaineRegEx(input))
        {
            Console.WriteLine("Username must containe only latin letter. No digits, no symbols");
            compairAnswer = false;
        }
        else { compairAnswer = true; }

        if (compairAnswer) { return true; }
        return false;
    }

    public static bool ValidUsername(string input)
    {
        bool compairAnswer = true;
        if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input))
        {
            Console.WriteLine("Username can't be empty or be only from spaces");
            compairAnswer = false;
        }
        else if (input.Length < 8 || input.Length >= 13)
        {
            Console.WriteLine("Username must be from 8 to 13 characters");
            compairAnswer = false;
        }
        else if (!input.All(char.IsLetter) || isContaineRegEx(input))
        {
            Console.WriteLine("Username must containe only latin letter. No digits, no symbols");
            compairAnswer = false;
        }
        else { compairAnswer = true; }

        if (compairAnswer) { return true; }
        return false;
    }

    public static bool ValidPassword (string input)
    {
        bool compairAnswer = true;
        if (string.IsNullOrEmpty(input))
        {
            Console.WriteLine("Username can't be empty");
            compairAnswer = false;
        }
        else if (!input.All(char.IsLetterOrDigit))
        {
            Console.WriteLine("Password must containe only latin letter, digits and symbols");
            compairAnswer = false;
        }
        else if (input.Length >= 13 || input.Length < 8)
        {
            Console.WriteLine("Password must be from 8 to 13 characters");
            compairAnswer = false;
        }
        else { compairAnswer = true; }

        if (compairAnswer) { return true; }
        return false;

    }
    public static bool isContaineRegEx(string input)
    {
        string strRegex = @"(^[0-9]{10}$)|(^\+[0-9]{2}\s+[0-9] 
                        {2}[0-9]{8}$)|(^[0-9]{3}-[0-9]{4}-[0-9]{4}$)";
        Regex re = new Regex(strRegex);
        if (re.IsMatch(input))
        {
            return true;
        }
        else { return false; }

    }

    public static void Login (Dictionary<string, string> userCredentials, Dictionary<string, double> userAccount)
    {
        bool transitionKey = true;
        do
        {
            transitionKey = false;
            Console.WriteLine("Enter Username:");
            string userName = Console.ReadLine();
            if (ValidUsername(userName))
            {
                Console.WriteLine("Enter Password");
                string userPassword = Console.ReadLine();
                if (ValidPassword(userPassword))
                {
                    //finding the pair username and password
                    if (ContainsPair(userCredentials, userName, userPassword))
                    {
                        do
                        {
                            Console.WriteLine("Select operation:\n1 --> Check Balance\n2 --> Deposit\n3 --> Withdraw\nE --> Exit to main menu");
                            string userAnswer = Console.ReadLine().ToLower();
                            switch (userAnswer)
                            {
                                case "1":
                                    CheckBalance(userAccount, userName);
                                    break;
                                case "2":
                                    Deposite(userAccount, userName);
                                    break;
                                case "3":
                                    Withdraw(userAccount, userName);
                                    break;
                                case "e":
                                    transitionKey = true;
                                    break;
                                default:
                                    Console.WriteLine("Unknown command. Please choose a valid option.");
                                    break;
                            }
                        } while (!transitionKey);

                    }
                }
                else
                {
                    Console.WriteLine("User with this credentials does't exist. Please, check username and password");
                    transitionKey = false;
                }
            }
        }
        while (!transitionKey);
    }

    public static bool ContainsPair (Dictionary<string, string> userCredentials, string username, string password)
    {
        if(userCredentials.ContainsKey(username))
        {
            if(userCredentials.TryGetValue(username, out var storedPassword))
            {
                return storedPassword == password;
            }  
            else return false;
        }
        else return false;


    }

    public static void CheckBalance (Dictionary<string, double> balance, string username) 
    {
           if (balance.TryGetValue(username, out var userBalance))
            {
                Console.WriteLine($"Баланс для {username}: {userBalance:F2}");
            }
            else { Console.WriteLine("User not found"); }
    }

    public static void Deposite (Dictionary<string, double> balance, string username)
    {

        if (balance.TryGetValue(username, out var userBalance))
        {
            Console.WriteLine($"Баланс для {username}: {userBalance:F2}");
            Console.WriteLine("How much money do you want to put on your deposite:");
            string userInput = Console.ReadLine();
            bool isNumeric = float.TryParse(userInput, out float depositAmount);
            if (isNumeric )
            {
                balance[username] += depositAmount;
                Console.WriteLine($"You top up your account on {depositAmount:F2}. New balance is {balance[username]:F2}");
            }
            else
                Console.WriteLine("You should enter only digits");
        }
        else { Console.WriteLine("User not found"); }
    }

    public static void Withdraw (Dictionary<string, double> balance, string username)
    {
        if (balance.TryGetValue (username, out var userBalance))
        {
            Console.WriteLine($"Balance: {userBalance:F2}");
            Console.WriteLine("How much would you like to withdraw?");
            string usernameInput = Console.ReadLine();
            bool isNumeric = float.TryParse(usernameInput, out float withdrawAmount);
            if (isNumeric )
            {
                if (userBalance >= withdrawAmount)
                {
                    userBalance -= withdrawAmount;
                    Console.WriteLine($"New balance: {userBalance:F2}");
                }
                else Console.WriteLine("Not enough money on your account");
                
            }
            else Console.WriteLine("You should enter only digits");
        }
    }

    public static bool CheckIfNumericInput(string userInput)
    {
        bool isNumeric = float.TryParse(Console.ReadLine(), out float inputNumber);
        if (!isNumeric)
        {
            return false;        
        }
        else
        {
            return true;
        }
    }
}



