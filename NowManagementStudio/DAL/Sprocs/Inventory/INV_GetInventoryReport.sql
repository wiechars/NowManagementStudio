DROP procedure IF EXISTS INV_GetInventoryReport;

DELIMITER $$
CREATE PROCEDURE INV_GetInventoryReport (IN lotID INT)
BEGIN
SELECT 
        inv_cur_lots.id AS id,
        inv_cur_lots.serial_no AS serial_no,
        inv_cfg_mat_brANDs.id AS brandID,
        inv_cfg_mat_brANDs.brAND AS brand,
        inv_cfg_mat_types.id AS typeID,
        inv_cfg_mat_types.type AS type,
        inv_cur_lots.quantity AS quantity,
        inv_cfg_uom.id AS uomID,
        inv_cfg_uom.uom AS uom,
        inv_cfg_lot_status.id AS lotStatusID,
        inv_cfg_lot_status.status AS lotStatus,
        inv_cfg_locations.id AS locationID,
        inv_cfg_locations.location AS location,
		weight.id AS 'weight_id',
        weight_val.value AS weight,
		width.id AS 'width_id',
        width_val.value AS width,
		height.id AS 'height_id',
        height_val.value AS height,
		volume.id AS 'volume_id',
        volume_val.value AS volume,
        inv_cur_lots.price AS price,
        date_format(inv_cur_lots.born_date, '%Y-%m-%d') AS purchase_date,
        date_format(inv_cur_lots.expiration_date, '%Y-%m-%d') AS expiration_date,
		next_inv_date.id AS 'next_inv_date_id',
        date_format(next_inv_date_val.value, '%Y-%m-%d') AS next_inv_date,
		last_inv_date.id AS 'last_inv_date_id',
        date_format(last_inv_date_val.value, '%Y-%m-%d') AS last_inv_date,
        inv_cur_lots.user_name AS user_name,
        inv_cur_lots.notes AS notes
    FROM
       inv_cur_lots
        LEFT JOIN inv_cfg_mat_brands ON (inv_cfg_mat_brands.id = inv_cur_lots.inv_cfg_mat_brands_id)
        LEFT JOIN inv_cfg_mat_types ON (inv_cfg_mat_types.id = inv_cur_lots.inv_cfg_mat_types_id)
        LEFT JOIN inv_cfg_uom ON (inv_cfg_uom.id = inv_cur_lots.inv_cfg_uom_id)
        LEFT JOIN inv_cfg_locations ON (inv_cfg_locations.id = inv_cur_lots.inv_cfg_locations_id)
        LEFT JOIN inv_cfg_lot_status ON (inv_cfg_lot_status.id = inv_cur_lots.inv_cfg_lot_status_id)

        LEFT JOIN inv_cfg_custom_props weight ON (weight.property = 'weight')
        LEFT JOIN inv_cur_prop_vals weight_val ON ((weight_val.inv_cfg_custom_props_id = weight.id)
            AND (weight_val.inv_cur_lots_id = inv_cur_lots.id))
        LEFT JOIN inv_cfg_custom_props width ON (width.property = 'width')
        LEFT JOIN inv_cur_prop_vals width_val ON ((width_val.inv_cfg_custom_props_id = width.id)
            AND (width_val.inv_cur_lots_id = inv_cur_lots.id))
        LEFT JOIN inv_cfg_custom_props height ON (height.property = 'height')
        LEFT JOIN inv_cur_prop_vals height_val ON ((height_val.inv_cfg_custom_props_id = height.id)
            AND (height_val.inv_cur_lots_id = inv_cur_lots.id))
        LEFT JOIN inv_cfg_custom_props volume ON (volume.property = 'volume')
        LEFT JOIN inv_cur_prop_vals volume_val ON ((volume_val.inv_cfg_custom_props_id = volume.id)
            AND (volume_val.inv_cur_lots_id = inv_cur_lots.id))
        LEFT JOIN inv_cfg_custom_props next_inv_date ON (next_inv_date.property = 'next_inv_date')
        LEFT JOIN inv_cur_prop_vals next_inv_date_val ON ((next_inv_date_val.inv_cfg_custom_props_id = next_inv_date.id)
            AND (next_inv_date_val.inv_cur_lots_id = inv_cur_lots.id))
        LEFT JOIN inv_cfg_custom_props last_inv_date ON (last_inv_date.property = 'last_inv_date')
        LEFT JOIN inv_cur_prop_vals last_inv_date_val ON ((last_inv_date_val.inv_cfg_custom_props_id = last_inv_date.id)
            AND (last_inv_date_val.inv_cur_lots_id = inv_cur_lots.id))
    WHERE
		inv_cur_lots.id = lotID;
END$$

DELIMITER ;

