
DROP procedure IF EXISTS `INV_UpdateLot`;

DELIMITER $$

CREATE PROCEDURE `INV_UpdateLot`(IN lotID INT(11), IN serialNo VARCHAR(256), IN uomID INT(11),IN quantity VARCHAR(45), IN price VARCHAR(128)
								,IN dateAdded DATETIME, IN expirationDate DATETIME,IN userName VARCHAR(128), IN matTypeID INT
								 ,IN propValsID VARCHAR(255), IN propVals VARCHAR(255), IN userNotes VARCHAR(255))
BEGIN
	UPDATE inv_cur_lots 

	SET inv_cfg_uom_id = uomID
		,serial_no = serialNo
		,born_date = dateAdded
		,expiration_date = expirationDate
		,user_name = userName
		,notes = userNotes
        ,inv_cfg_mat_types_id = matTypeID
	WHERE inv_cur_lots.id = lotID;

	CALL `INV_UpdateLotQuantity` (lotID, quantity, userName);
	CALL `INV_UpdateLotPrice` (lotID, price, userName);
	CALL `INV_UpdateLotCustomProps` (lotID, propValsID, propVals, userName);



END$$

DELIMITER ;

