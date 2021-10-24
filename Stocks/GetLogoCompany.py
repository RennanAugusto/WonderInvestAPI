from selenium import webdriver
from selenium.webdriver.common.by import By
import psycopg2 as ps2
import time
from selenium.webdriver.common.keys import Keys
from selenium.webdriver.firefox.service import Service
from selenium.webdriver.firefox.options import Options
import requests
import base64
import PIL.Image

cSQL_Get_Company = 'SELECT "Id", "Name" FROM "Company"'
cInsert_IMG_Company = 'UPDATE "Company" SET "LogoBase64" = %s WHERE "Id" = %s'

class Company:
    def __init__(self, Id, name):
        self.Name = name
        self.ID = Id
        self.Base64 = ''

class Copy_Image_Logo:
    def __init__(self, database_name, user_name, password):
        self.Connection = ps2.connect(host='localhost', database=database_name, user=user_name, password=password)
        self.URL = 'https://www.google.com/search?q={0}&sxsrf=AOaemvLaxlj6pk3y2ti6kJjHlHD7WObO-A:1634956740464&source=lnms&tbm=isch&sa=X&ved=2ahUKEwjn2PO9wN_zAhVGIbkGHSN6A18Q_AUoAXoECAQQAw&biw=1920&bih=979&dpr=1'
        options = Options()

        ## Enable headless
        options.headless = False
        options.binary_location = r'C:\Program Files\Mozilla Firefox\firefox.exe'
        # Specify custom geckodriver path
        service = Service('geckodriver')
        self.Driver = webdriver.Firefox(options=options, service=service)
        self.Company_List = []

    def load_company_list(self):
        cursor = self.Connection.cursor()
        cursor.execute(cSQL_Get_Company)
        result = cursor.fetchall()
        for company in result:
            self.Company_List.append(Company(company[0], company[1]))

    def get_logos(self):
        for company in self.Company_List:
            try:
                self.Driver.get(self.URL.format(company.Name + ' Logo'))
                time.sleep(1)
                # input = self.Driver.find_element(By.CLASS_NAME, 'gLFyf')
                # input.send_keys(company.Name + 'Logo')
                # input.send_keys(Keys.ENTER)
                # time.sleep(1)
                # go_to_images = self.Driver.find_element(By.XPATH, "//a[contains(text(),'Imagens')")
                # go_to_images.click()
                # time.sleep(1)
                image = self.Driver.find_element(By.CLASS_NAME, 'Q4LuWd')
                img_64 = image.screenshot_as_base64
                company.Base64 = img_64
            except:
                #self.Driver.close()
                print(f'Empresa {company.Name}, deu ruim dms')
            #self.Driver.close()

    def update_Logos(self):
        cursor = self.Connection.cursor()
        for company in self.Company_List:
            cursor.execute(cInsert_IMG_Company, (company.Base64, company.ID))
        self.Connection.commit()



logos = Copy_Image_Logo('WonderInvest', 'postgres', 'root')
logos.load_company_list()
logos.get_logos()
logos.update_Logos()