RESTORE DATABASE productstoredb
  FROM Disk = 'C:\Users\lenovo\source\repos\OnlineStoreMVC\DBBackup\productstoredb_v0.5.bak' 
  WITH MOVE 'productstoredb' TO 'C:\Users\lenovo\source\repos\OnlineStoreMVC\DBBackup\db.mdf',
  MOVE 'productstoredb_log' TO 'C:\Users\lenovo\source\repos\OnlineStoreMVC\DBBackup\db_log.ldf';