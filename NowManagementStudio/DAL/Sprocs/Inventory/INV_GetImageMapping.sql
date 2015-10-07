DROP procedure IF EXISTS `INV_GetImageMapping`;

DELIMITER $$
CREATE PROCEDURE `INV_GetImageMapping`(IN lotID INT)
BEGIN

		SELECT id
			,file_path
		FROM inv_cur_image_mapping
        where inv_cur_lots_id = lotID;

END$$

DELIMITER ;

