import investpy as inv
import psycopg2 as ps2
import json
import datetime

cSQL_Get_Stocks = 'SELECT "Id", "Code" FROM "Stock"'
cSQL_Insert_Price = 'INSERT INTO "PriceStock" VALUES((SELECT COALESCE(MAX("Id"), 1) + 1 FROM "PriceStock"), %s, %s, %s, %s, %s)'

class Stock:
    def __init__(self, code, id):
        self.Id = id
        self.Code = code

class Insert_Price_Stocks:
    def __init__(self, database_name, user_name, password, ini_date, end_date):
        self.Connection = ps2.connect(host = 'localhost', database = database_name, user = user_name, password = password)
        self.Stock_List = []
        self.Initial_Date = ini_date
        self.End_Date = end_date

    def get_all_stocks(self):
        cursor = self.Connection.cursor()
        cursor.execute(cSQL_Get_Stocks)
        result = cursor.fetchall()
        for stock in result:
            self.Stock_List.append(Stock(stock[1], stock[0]))


    def insert_prices(self):
        cursor = self.Connection.cursor()
        for stock in self.Stock_List:
            try:
                prices = inv.get_stock_historical_data(stock.Code, 'BRAZIL', self.Initial_Date, self.End_Date, as_json = True)
                data = json.loads(prices)
                for price in data['historical']:
                    price_data = json.dumps(price)
                    price_data = json.loads(price_data)
                    cursor.execute(cSQL_Insert_Price, (price_data['open'], price_data['date'], 1, stock.Id, True))
                    cursor.execute(cSQL_Insert_Price, (price_data['close'], price_data['date'], 0, stock.Id, True))
            except:
                print(f"Stock {stock.Code}, deu ruim")
            self.Connection.commit()

opa = Insert_Price_Stocks('WonderInvest', 'postgres', 'root', '01/01/2015', '31/12/2018')
opa.get_all_stocks()
opa.insert_prices()