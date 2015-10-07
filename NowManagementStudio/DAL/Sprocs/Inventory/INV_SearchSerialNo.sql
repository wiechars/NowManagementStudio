DROP procedure IF EXISTS INV_SearchSerialNo;

DELIMITER $$
CREATE PROCEDURE INV_SearchSerialNo (IN serialSearchTerm VARCHAR(256))
BEGIN
SELECT 
		id
		,serial_no
FROM inv_cur_lots
WHERE
	(serialSearchTerm IS NULL OR MATCH(serial_no) AGAINST (serialSearchTerm IN BOOLEAN MODE))
       AND inv_cur_lots.is_deleted = 0;
END$$

DELIMITER ;

