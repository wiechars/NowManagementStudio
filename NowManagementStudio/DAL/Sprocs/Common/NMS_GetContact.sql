
DROP procedure IF EXISTS `NMS_GetContact`;

DELIMITER $$
CREATE PROCEDURE `NMS_GetContact` (IN contactID INT)
BEGIN
	SELECT `id`,
`name`,
`email`,
`phone_number`,
`last_modified`
FROM `nms_cur_contacts`
WHERE id = contactID;
END

$$

DELIMITER ;

