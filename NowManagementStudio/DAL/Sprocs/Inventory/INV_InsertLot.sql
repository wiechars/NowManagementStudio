DROP procedure IF EXISTS `INV_InsertLot`;

DELIMITER $$
CREATE PROCEDURE `INV_InsertLot`(IN uomID INT,IN lotStatusID INT, IN locationID INT,IN brandID INT, IN typeID INT, IN serialNo VARCHAR(128), IN quantity VARCHAR(45)
					,IN price VARCHAR(128), IN dateAdded DATETIME, IN expirationDate DATETIME, IN userName VARCHAR(128), IN userNotes VARCHAR(256)
					,IN propValsID VARCHAR(255), IN propVals VARCHAR(255), OUT insertID INT)
BEGIN


	INSERT INTO inv_cur_lots (`inv_cfg_uom_id`
								,`inv_cfg_lot_status_id`
								,`inv_cfg_locations_id`
								,`inv_cfg_mat_brands_id`
								,`inv_cfg_mat_types_id`
								,`serial_no`
								,`quantity`
								,`price`
								,`born_date`
								,`expiration_date`
								,`user_name`
								,`notes`
								,`is_deleted`)
	VALUES (uomID
			,lotStatusID
			,locationID
			,brandID
			,typeID
			,serialNo
			,quantity
			,price
			,dateAdded
			,expirationDate
			,userName
			,userNotes
			,0);

SET insertID = LAST_INSERT_ID();

CALL `INV_UpdateLotCustomProps` (insertID, propValsID, propVals, userName);
 



END$$

DELIMITER ;

