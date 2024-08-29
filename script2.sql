-- Create a Common Table Expression (CTE) to assign random numbers to each image URL
WITH RandomImages AS (
    SELECT
        ImageUrl,
        ROW_NUMBER() OVER (ORDER BY NEWID()) AS RowNum
    FROM TempImageUrls
),

-- Create a CTE to match items with the random images
ItemsWithImages AS (
    SELECT
        i.ItemId AS ItemId,
        ri.ImageUrl
    FROM TbItems i
    INNER JOIN RandomImages ri
    ON i.ItemId % (SELECT COUNT(*) FROM RandomImages) + 1 = ri.RowNum
)

-- Update TbItems with the random image URLs
UPDATE i
SET i.ImageName = ri.ImageUrl
FROM TbItems i
INNER JOIN ItemsWithImages ri
ON i.ItemId = ri.ItemId;


DROP TABLE TempImageUrls;
