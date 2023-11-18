SELECT TOP 100
    M.MealId,
    M.Name,
    M.Price,
    M.Amount,
    M.Description,
    M.Image,
    T.Name AS TagName
FROM
    Meals M
LEFT JOIN
    MealTags MT ON M.MealId = MT.MealId
LEFT JOIN
    Tags T ON MT.TagId = T.TagId;