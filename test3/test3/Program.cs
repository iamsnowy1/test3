using System;

// Клас Logger, що реалізує патерн Singleton для логування транзакцій
public sealed class Logger
{
    private static readonly Logger instance = new Logger();

    // Приватний конструктор для Singleton
    private Logger() { }

    // Властивість для доступу до єдиного екземпляру Logger
    public static Logger Instance
    {
        get { return instance; }
    }

    // Метод для логування повідомлення
    public void Log(string message)
    {
        Console.WriteLine($"[LOG] {message}");
    }
}

// Клас BankAccount з полями і методами для управління рахунком
public class BankAccount
{
    public string AccountNumber { get; private set; }
    public decimal Balance { get; private set; }

    // Конструктор для ініціалізації рахунку
    public BankAccount(string accountNumber, decimal initialBalance)
    {
        AccountNumber = accountNumber;
        Balance = initialBalance;
    }

    // Метод для внесення коштів на рахунок
    public void Deposit(decimal amount)
    {
        Balance += amount;
        Logger.Instance.Log($"Deposited {amount:C}. New balance: {Balance:C}");
    }

    // Метод для зняття коштів з рахунку
    public void Withdraw(decimal amount)
    {
        if (amount > Balance)
        {
            throw new InvalidOperationException("Insufficient funds.");
        }

        Balance -= amount;
        Logger.Instance.Log($"Withdrawn {amount:C}. New balance: {Balance:C}");
    }
}

class Program
{
    static void Main()
    {
        // Приклад використання класу BankAccount і Logger

        // Створення рахунку з початковим балансом
        BankAccount account = new BankAccount("123456789", 1000);

        // Внесення коштів на рахунок
        account.Deposit(500);

        // Зняття коштів з рахунку
        try
        {
            account.Withdraw(200);
            account.Withdraw(1500); // Це викличе виняток
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        Console.ReadLine();
    }
}
