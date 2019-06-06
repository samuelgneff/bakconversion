using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
//using Microsoft.SqlServer.Management.Sdk.Sfc;

public class Restorinator
{

    public void RestoreDatabase(String databaseName, String filePath, String serverName,
                String userName, String password,
                String dataFilePath, String logFilePath)
    {
        Restore sqlRestore = new Restore();

        BackupDeviceItem deviceItem = new BackupDeviceItem(filePath, DeviceType.File);
        sqlRestore.Devices.Add(deviceItem);
        sqlRestore.Database = databaseName;

        ServerConnection connection = new ServerConnection(serverName);
        //sqlServer.KillAllProcesses(databaseName);
        Server sqlServer = new Server(connection);

        Database db = sqlServer.Databases[databaseName];
        sqlRestore.Action = RestoreActionType.Database;
        String dataFileLocation = dataFilePath + databaseName + ".mdf";
        String logFileLocation = logFilePath + databaseName + "_Log.ldf";

        db = sqlServer.Databases[databaseName];


        sqlRestore.RelocateFiles.Add(new RelocateFile(databaseName, dataFileLocation));
        sqlRestore.RelocateFiles.Add(new RelocateFile(databaseName + "_log", logFileLocation));
        sqlRestore.ReplaceDatabase = true;
        sqlRestore.Complete += new ServerMessageEventHandler((sender, e) => {
            Console.WriteLine("\nThe dishes are done");
        });
        sqlRestore.PercentCompleteNotification = 10;
        sqlRestore.PercentComplete +=
           new PercentCompleteEventHandler((sender1, e1) => {
               Console.Write(".");
           });

        sqlRestore.SqlRestore(sqlServer);
        db = sqlServer.Databases[databaseName];
        db.SetOnline();
        sqlServer.Refresh();
    }
}
