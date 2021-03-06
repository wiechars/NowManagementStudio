
DROP procedure IF EXISTS `NMS_GetContacts`;

DELIMITER $$
CREATE PROCEDURE `NMS_GetContacts` ()
BEGIN
	SELECT `id`,
`name`,
`email`,
`phone_number`,
`last_modified`
FROM `nms_cur_contacts`
WHERE `is_deleted` = 0 Limit 1000;
END

$$

DELIMITER ;

