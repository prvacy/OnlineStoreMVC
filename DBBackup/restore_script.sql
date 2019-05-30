RESTORE DATABASE productstoredb
  FROM Disk = '[Path]\productstoredb[version].bak'; 
--  WITH MOVE 'productstoredb' TO '[Path]\[name].mdf',
--  MOVE 'productstoredb_log' TO '[Path]\[name].ldf';
