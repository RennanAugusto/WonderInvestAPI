CREATE MATERIALIZED VIEW StockProgression AS
SELECT COALESCE(TO_CHAR(Anual.date, 'month'), 'January') description
     , COALESCE(Anual.price, 0) price
     , COALESCE(Anual.Date, '1900-01-01') date
, "Stock"."Code" Code
, "Stock"."Id" StockId
, 'Anual progression' legend
, 'Year' typeProgression
, ROW_NUMBER () OVER (ORDER BY "Stock"."Id") AS IDP
FROM "Stock"
    LEFT JOIN LATERAL(
    SELECT "Number"."number" Mes FROM "Number"
    WHERE "Number"."number" <= 12
    ) Mes ON True
    LEFT JOIN LATERAL (
    SELECT priceAux."Date" Date FROM "PriceStock" priceAux
    WHERE priceAux."StockId" = "Stock"."Id"
    AND priceAux."Active" = true
    ORDER BY priceAux."Date" DESC
    LIMIT 1
    ) LastDate ON True
    LEFT JOIN LATERAL (
    SELECT price."Date" date
    , price."Price" price
    FROM "PriceStock" price
    WHERE price."StockId" = "Stock"."Id"
    AND EXTRACT(MONTH FROM price."Date") = EXTRACT(MONTH FROM lastDate.Date - INTERVAL '1' MONTH * (Mes.Mes - 1))
    AND price."Date" IS NOT NULL
    ORDER BY price."Date" DESC
    LIMIT 1
    ) Anual ON True

UNION

SELECT DISTINCT CAST(EXTRACT(DAY FROM Mensal.Date) AS CHAR(2)) description
              , Mensal.price
              , Mensal.Date date
, "Stock"."Code" Code
, "Stock"."Id" StockId
, 'Month Progression' legend
, 'Month' typeProgression
, ROW_NUMBER () OVER (ORDER BY "Stock"."Id") AS IDP
FROM "Stock"
    LEFT JOIN LATERAL(
    SELECT "Number"."number" Dia FROM "Number"
    WHERE "Number"."number" <= 31
    ORDER BY "Number"."number" ASC
    ) Dia ON True
    LEFT JOIN LATERAL (
    SELECT priceAux."Date" Date FROM "PriceStock" priceAux
    WHERE priceAux."StockId" = "Stock"."Id"
    AND priceAux."Active" = true
    ORDER BY priceAux."Date" DESC
    LIMIT 1
    ) LastDate ON True
    LEFT JOIN LATERAL (
    SELECT price."Date" date
    , price."Price" price
    , Dia.Dia - 1 Dia
    FROM "PriceStock" price
    WHERE price."StockId" = "Stock"."Id"
    AND EXTRACT(DAY FROM price."Date") = EXTRACT(DAY FROM lastDate.Date - INTERVAL '1' DAY * (Dia.Dia - 1))
    AND EXTRACT(MONTH FROM price."Date") = EXTRACT(MONTH FROM lastDate.Date)
    AND EXTRACT(YEAR FROM price."Date") = EXTRACT(YEAR FROM lastDate.Date)
    AND price."Date" IS NOT NULL
    ORDER BY price."Date" DESC
    LIMIT 1
    ) Mensal ON True
WHERE Mensal.Date IS NOT NULL