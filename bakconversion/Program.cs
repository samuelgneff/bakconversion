using System;
using static Restorinator;

namespace bakconversion {
    class Program {
        static void Main(string[] args) {
        string filePath = @"C:\Fifth_Partners_backup.bak";
        string databaseName = "Fifth_Partners";
        string serverName = "DESKTOP-IP5TIF2\\SQLEXPRESS";
        string dataFilePath = @"C:\";
        string logFilePath = @"C:\Users\neffs\OneDrive\Documents\sqlserverwork\";

        Console.Out.WriteLine("is this working?");
            Restorinator restoreEngine = new Restorinator();
            restoreEngine.RestoreDatabase(databaseName, filePath, serverName,
        " ", " ", dataFilePath, logFilePath);


            Console.WriteLine("Hello World!");
        }
    }
}
