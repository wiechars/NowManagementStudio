DROP procedure IF EXISTS `SEC_GetUserRoles`;

DELIMITER $$
CREATE PROCEDURE `SEC_GetUserRoles` ()
BEGIN
SELECT `Name` FROM aspnetroles;
END

$$

DELIMITER ;

