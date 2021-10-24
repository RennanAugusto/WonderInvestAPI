import investpy as inv
import psycopg2 as ps2
import json

sql_insert_stock = 'INSERT INTO "Stock" VALUES((SELECT COALESCE(MAX(st."Id"),0) +1 FROM "Stock" st), %s, (SELECT c."Id" FROM "Company" c WHERE c."Name" = %s), %s) ';
sql_insert_company = 'INSERT INTO "Company" VALUES((SELECT MAX(cp."Id")+1 FROM "Company" cp), %s, %s, %s) ';

class Stock:
    def __init__(self, code, company_name):
        self.Id = 0
        self.Code = code
        self.Company_Name = company_name
        self.Active = True

class Company:
    def __init__(self, name):
        self.Id = 0
        self.Name = name
        self.Active = True
        self.Acting = 0

class InsertStocks:
    def __init__(self, database_name, user_name, password):
        self.Connection = ps2.connect(host = 'localhost', database = database_name, user = user_name, password = password)
        self.Companies_List = []
        self.Stock_List = []

    def get_company_id_by_name(self, company_name):
        for company in self.Companies_List:
            if company.Name == company_name:
                return company.Id
        return 0

    def check_company_list(self, company_name):
        for company in self.Companies_List:
            if company.Name == company_name:
                return True
        return False

    def load_stocks_companies(self):
        list_stocks = inv.get_stocks_overview('Brazil', as_json=True, n_results = 1000)
        for stock in list_stocks:
            data = json.dumps(stock)
            data = json.loads(data)
            if not self.check_company_list(data['name']):
                cmp_name = data['name']
                company = Company(cmp_name)
                self.Companies_List.append(company)
            self.Stock_List.append(Stock(data['symbol'], data['name']))

    def insert_stocks_companies(self):
        cursor = self.Connection.cursor()

        for company in self.Companies_List:
            try:
                cursor.execute(sql_insert_company, (company.Name, company.Acting, company.Active))
                print(f"Inseri a empresa {company.Name}")
            except:
                print('VISH JA TEM A EMPRESA NO BANCO, TRISTE :(')

        for stock in self.Stock_List:
            cursor.execute(sql_insert_stock, (stock.Code, stock.Company_Name, stock.Active))
            print(f"Inseri a ação {stock.Code}")

        self.Connection.commit()

script = InsertStocks('WonderInvest', 'postgres', 'root')
script.load_stocks_companies()
script.insert_stocks_companies()

