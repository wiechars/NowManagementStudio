
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
WHERE `is_deleted` = 0 and id > 6120 Limit 200;
END

$$

DELIMITER ;

