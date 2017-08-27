SELECT Permission_Id, PermissionDescription 
       FROM PERMISSIONS
       WHERE Permission_Id IN (
             SELECT DISTINCT(Permission_Id) 
                    FROM LNK_ROLE_PERMISSION 
		            WHERE Role_Id IN (
                          SELECT DISTINCT(Role_Id) 
                                 FROM LNK_USER_ROLE ur 
		                         JOIN USERS u ON u.User_Id=ur.User_Id 
		                         WHERE u.Username='swloch'))