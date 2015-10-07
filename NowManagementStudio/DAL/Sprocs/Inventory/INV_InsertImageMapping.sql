DROP procedure IF EXISTS `INV_InsertImageMapping`;

DELIMITER $$
CREATE PROCEDURE `INV_InsertImageMapping`(IN lotID INT,IN fileName VARCHAR(256))
BEGIN


	INSERT INTO inv_cur_image_mapping (inv_cur_lots_id
								,file_path)
	VALUES (lotID
			,fileName);




END$$

DELIMITER ;

