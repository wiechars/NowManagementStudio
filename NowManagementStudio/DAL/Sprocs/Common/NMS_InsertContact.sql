
DROP procedure IF EXISTS `NMS_InsertContact`;

DELIMITER $$
CREATE PROCEDURE `NMS_InsertContact` (IN name VARCHAR(256), IN email VARCHAR(256), IN phoneNumber VARCHAR(256), OUT id INT)
BEGIN

		INSERT INTO  nms_cur_contacts (name, email, phone_number)
		VALUES (name, email, phoneNumber);
		SET id = LAST_INSERT_ID();
END

$$

DELIMITER ;

