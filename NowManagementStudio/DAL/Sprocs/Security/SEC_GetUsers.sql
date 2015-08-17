DROP procedure IF EXISTS `SEC_GetUsers`;

DELIMITER $$
CREATE PROCEDURE `SEC_GetUsers` ()
BEGIN
	SELECT
		aspnetusers.UserName
		,aspnetusers.PhoneNumber
		,aspnetusers.Email
	FROM aspnetusers;
END

$$

DELIMITER ;

