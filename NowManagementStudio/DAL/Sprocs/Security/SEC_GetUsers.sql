DROP procedure IF EXISTS `SEC_GetUsers`;

DELIMITER $$
CREATE PROCEDURE `SEC_GetUsers` ()
BEGIN
	SELECT
		aspnetusers.Id
		,aspnetusers.UserName
		,aspnetusers.PhoneNumber
        ,aspnetusers.PasswordHash
		,aspnetusers.Email
	FROM aspnetusers;
END

$$

DELIMITER ;

