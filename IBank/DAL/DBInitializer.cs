using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace DAL
{
    public class IBankInitializer : System.Data.Entity.CreateDatabaseIfNotExists<IBankContext>//DropCreateDatabaseIfModelChanges<IBankContext>
    {
        protected override void Seed(IBankContext context)
        {
            var customers = new List<Customer>
            {
                new Customer() { CustomerID = 1, Login="Customer1", Passoword="Customer1", FirstName="Вадим", LastName="Попёл", MiddleName="Александрович", Country="Беларусь", Address="г.Минск", Email="vadim@ibank.by", PassportNumber="ВМ123123" },
                new Customer() { CustomerID = 2, Login="Customer2", Passoword="Customer2", FirstName="Павел", LastName="Мудреченко", MiddleName="Александрович", Country="Беларусь", Address="г.Минск", Email="pavel@ibank.by", PassportNumber="ВМ222222" },
                new Customer() { CustomerID = 3, Login="Customer3", Passoword="Customer3", FirstName="Руслан", LastName="Гурбанмухамедов", MiddleName="Александрович", Country="Беларусь", Address="г.Минск", Email="ruslan@ibank.by", PassportNumber="ВМ122223" },
                new Customer() { CustomerID = 4, Login="Customer4", Passoword="Customer4", FirstName="Дарья", LastName="Телеш", MiddleName="Александровна", Country="Беларусь", Address="г.Минск", Email="dasha@ibank.by", PassportNumber="ВМ126623" },
            };
            customers.ForEach(s => context.Customers.Add(s));
            context.SaveChanges();

            var accessCards = new List<AccessCard>
            {             
                new AccessCard(){ AccessCardID = 1, CustomerID = 1, EnteredCodeFail = 0 },
                new AccessCard(){ AccessCardID = 2, CustomerID = 2, EnteredCodeFail = 0 },
                new AccessCard(){ AccessCardID = 3, CustomerID = 3, EnteredCodeFail = 0 },
                new AccessCard(){ AccessCardID = 4, CustomerID = 4, EnteredCodeFail = 0 }
            };
            accessCards.ForEach(s => context.AccessCards.Add(s));
            context.SaveChanges();

            var accessCodes = new List<AccessCode>
            {
                new AccessCode(){ AccessCodeID = 1, AccessCardID = 1, Number = 1, Code = "1111" },
                new AccessCode(){ AccessCodeID = 2, AccessCardID = 1, Number = 2, Code = "2222" },
                new AccessCode(){ AccessCodeID = 3, AccessCardID = 1, Number = 3, Code = "3333" },
                new AccessCode(){ AccessCodeID = 4, AccessCardID = 1, Number = 4, Code = "4444" },
                new AccessCode(){ AccessCodeID = 5, AccessCardID = 1, Number = 5, Code = "5555" },
                new AccessCode(){ AccessCodeID = 6, AccessCardID = 1, Number = 6, Code = "6666" },
                new AccessCode(){ AccessCodeID = 7, AccessCardID = 1, Number = 7, Code = "7777" },
                new AccessCode(){ AccessCodeID = 8, AccessCardID = 1, Number = 8, Code = "8888" },                
                new AccessCode(){ AccessCodeID = 9, AccessCardID = 1, Number = 9, Code = "9999" },

                new AccessCode(){ AccessCodeID = 1, AccessCardID = 2, Number = 1, Code = "1111" },
                new AccessCode(){ AccessCodeID = 2, AccessCardID = 2, Number = 2, Code = "2222" },
                new AccessCode(){ AccessCodeID = 3, AccessCardID = 2, Number = 3, Code = "3333" },
                new AccessCode(){ AccessCodeID = 4, AccessCardID = 2, Number = 4, Code = "4444" },
                new AccessCode(){ AccessCodeID = 5, AccessCardID = 2, Number = 5, Code = "5555" },
                new AccessCode(){ AccessCodeID = 6, AccessCardID = 2, Number = 6, Code = "6666" },
                new AccessCode(){ AccessCodeID = 7, AccessCardID = 2, Number = 7, Code = "7777" },
                new AccessCode(){ AccessCodeID = 8, AccessCardID = 2, Number = 8, Code = "8888" },                
                new AccessCode(){ AccessCodeID = 9, AccessCardID = 2, Number = 9, Code = "9999" },

                new AccessCode(){ AccessCodeID = 1, AccessCardID = 3, Number = 1, Code = "1111" },
                new AccessCode(){ AccessCodeID = 2, AccessCardID = 3, Number = 2, Code = "2222" },
                new AccessCode(){ AccessCodeID = 3, AccessCardID = 3, Number = 3, Code = "3333" },
                new AccessCode(){ AccessCodeID = 4, AccessCardID = 3, Number = 4, Code = "4444" },
                new AccessCode(){ AccessCodeID = 5, AccessCardID = 3, Number = 5, Code = "5555" },
                new AccessCode(){ AccessCodeID = 6, AccessCardID = 3, Number = 6, Code = "6666" },
                new AccessCode(){ AccessCodeID = 7, AccessCardID = 3, Number = 7, Code = "7777" },
                new AccessCode(){ AccessCodeID = 8, AccessCardID = 3, Number = 8, Code = "8888" },                
                new AccessCode(){ AccessCodeID = 9, AccessCardID = 3, Number = 9, Code = "9999" },

                new AccessCode(){ AccessCodeID = 1, AccessCardID = 4, Number = 1, Code = "1111" },
                new AccessCode(){ AccessCodeID = 2, AccessCardID = 4, Number = 2, Code = "2222" },
                new AccessCode(){ AccessCodeID = 3, AccessCardID = 4, Number = 3, Code = "3333" },
                new AccessCode(){ AccessCodeID = 4, AccessCardID = 4, Number = 4, Code = "4444" },
                new AccessCode(){ AccessCodeID = 5, AccessCardID = 4, Number = 5, Code = "5555" },
                new AccessCode(){ AccessCodeID = 6, AccessCardID = 4, Number = 6, Code = "6666" },
                new AccessCode(){ AccessCodeID = 7, AccessCardID = 4, Number = 7, Code = "7777" },
                new AccessCode(){ AccessCodeID = 8, AccessCardID = 4, Number = 8, Code = "8888" },                
                new AccessCode(){ AccessCodeID = 9, AccessCardID = 4, Number = 9, Code = "9999" },
            };
            accessCodes.ForEach(s => context.AccessCodes.Add(s));
            context.SaveChanges();

            var ssisgroups = new List<SSISGroup>
            {
                new SSISGroup(){ SSISGroupID = 1, GroupName = "Система \"Расчет\"", ParentGroupID = 0 },

                new SSISGroup(){ SSISGroupID = 2, GroupName = "БЕЛТЕЛЕКОМ", ParentGroupID = 1},
                new SSISGroup(){ SSISGroupID = 3, GroupName = "Брест и Брестская область", ParentGroupID = 2},
                new SSISGroup(){ SSISGroupID = 4, GroupName = "Витебск и Витебская область", ParentGroupID = 2},
                new SSISGroup(){ SSISGroupID = 5, GroupName = "Гомель и Гомельская область", ParentGroupID = 2},
                new SSISGroup(){ SSISGroupID = 6, GroupName = "Гродно и Гродненская область", ParentGroupID = 2},
                new SSISGroup(){ SSISGroupID = 7, GroupName = "Минск", ParentGroupID = 2},
                new SSISGroup(){ SSISGroupID = 8, GroupName = "Минская область", ParentGroupID = 2},
                new SSISGroup(){ SSISGroupID = 9, GroupName = "Могилев и Могилевская область", ParentGroupID = 2},

                new SSISGroup(){ SSISGroupID = 10, GroupName = "Билеты, Лотереи", ParentGroupID = 1},
                new SSISGroup(){ SSISGroupID = 11, GroupName = "ВАШЕ ЛОТО", ParentGroupID = 10},
                new SSISGroup(){ SSISGroupID = 12, GroupName = "ПЯТЕРОЧКА", ParentGroupID = 10},

                new SSISGroup(){ SSISGroupID = 13, GroupName = "Домофонные системы", ParentGroupID = 1},
                new SSISGroup(){ SSISGroupID = 14, GroupName = "Альфаслав", ParentGroupID = 13},
                new SSISGroup(){ SSISGroupID = 15, GroupName = "Белсплат ОАО", ParentGroupID = 13},

                new SSISGroup(){ SSISGroupID = 16, GroupName = "Интернет, Телевидение", ParentGroupID = 1},
                new SSISGroup(){ SSISGroupID = 17, GroupName = "Хостинг и домены", ParentGroupID = 16},
                new SSISGroup(){ SSISGroupID = 18, GroupName = "Аксиома-Сервис ООО", ParentGroupID = 16},
                new SSISGroup(){ SSISGroupID = 19, GroupName = "Активные технологии", ParentGroupID = 17},

                new SSISGroup(){ SSISGroupID = 20, GroupName = "МВД", ParentGroupID = 1},
                new SSISGroup(){ SSISGroupID = 21, GroupName = "ГАИ-фотофиксация", ParentGroupID = 20},
                
                new SSISGroup(){ SSISGroupID = 22, GroupName = "Мобильная связь", ParentGroupID = 1},
                new SSISGroup(){ SSISGroupID = 23, GroupName = "life :)", ParentGroupID = 22},
                new SSISGroup(){ SSISGroupID = 24, GroupName = "velcom", ParentGroupID = 22},
                new SSISGroup(){ SSISGroupID = 25, GroupName = "МТС", ParentGroupID = 22},                

                new SSISGroup(){ SSISGroupID = 26, GroupName = "Образование и развитие", ParentGroupID = 1},
                new SSISGroup(){ SSISGroupID = 27, GroupName = "ЕШКО дистанционные курсы", ParentGroupID = 26},
                new SSISGroup(){ SSISGroupID = 28, GroupName = "БАМЭ-Экспедитор образов. центр", ParentGroupID = 26},

                new SSISGroup(){ SSISGroupID = 29, GroupName = "СМИ", ParentGroupID = 1},

                new SSISGroup(){ SSISGroupID = 30, GroupName = "Социальное обслуживание, Здравоохранение", ParentGroupID = 1},
                new SSISGroup(){ SSISGroupID = 31, GroupName = "Респуб. научно-практические центры", ParentGroupID = 30},         
       
                new SSISGroup(){ SSISGroupID = 32, GroupName = "Туристические услуги", ParentGroupID = 1},
                new SSISGroup(){ SSISGroupID = 33, GroupName = "Автоматизир.технологии туризма", ParentGroupID = 32},                    

                new SSISGroup(){ SSISGroupID = 34, GroupName = "Финансовые услуги", ParentGroupID = 1},
                new SSISGroup(){ SSISGroupID = 35, GroupName = "Страхование", ParentGroupID = 34},
                new SSISGroup(){ SSISGroupID = 36, GroupName = "Электронные деньги", ParentGroupID = 34},
            };
            ssisgroups.ForEach(g => context.SSISGroups.Add(g));
            context.SaveChanges();

            var ssisPayments = new List<SSISPayment>
            {
                new SSISPayment(){ SSISPaymentID = 1, Information="Введите 13 цифр номера договора", NumberLabel="Номер договора", Name="Интернет (BYFLY,ZALA)", SSISNumber="55555", GroupID = 3, Regex = @"^\d+$", Error = "Поле должно состоять из цифр" },
                new SSISPayment(){ SSISPaymentID = 2, Information="Необходимо ввести: \n областной центр: 2 + номер телефона  \n районные центры: две последних цифры кода региона + номер телефона", NumberLabel="Номер телефона", Name="Телефон", SSISNumber="11111", GroupID = 3, Regex = @"^\d+$", Error = "Поле должно состоять из цифр"},
                new SSISPayment(){ SSISPaymentID = 3, Information="Введите 13 цифр номера договора", NumberLabel="Номер договора", Name="Интернет (BYFLY,ZALA)", SSISNumber="55555", GroupID = 4, Regex = @"^\d+$", Error = "Поле должно состоять из цифр" },
                new SSISPayment(){ SSISPaymentID = 4, Information="Необходимо ввести: \n областной центр: 2 + номер телефона  \n районные центры: две последних цифры кода региона + номер телефона", NumberLabel="Номер телефона", Name="Телефон", SSISNumber="11111", GroupID = 4, Regex = @"^\d+$", Error = "Поле должно состоять из цифр"},
                new SSISPayment(){ SSISPaymentID = 5, Information="Введите 13 цифр номера договора", NumberLabel="Номер договора", Name="Интернет (BYFLY,ZALA)", SSISNumber="55555", GroupID = 5 , Regex = @"^\d+$", Error = "Поле должно состоять из цифр"},
                new SSISPayment(){ SSISPaymentID = 6, Information="Необходимо ввести: \n областной центр: 2 + номер телефона  \n районные центры: две последних цифры кода региона + номер телефона", NumberLabel="Номер телефона", Name="Телефон", SSISNumber="11111", GroupID = 5, Regex = @"^\d+$", Error = "Поле должно состоять из цифр"},
                new SSISPayment(){ SSISPaymentID = 7, Information="Введите 13 цифр номера договора", NumberLabel="Номер договора", Name="Интернет (BYFLY,ZALA)", SSISNumber="55555", GroupID = 6, Regex = @"^\d+$", Error = "Поле должно состоять из цифр" },
                new SSISPayment(){ SSISPaymentID = 8, Information="Необходимо ввести: \n областной центр: 2 + номер телефона  \n районные центры: две последних цифры кода региона + номер телефона", NumberLabel="Номер телефона", Name="Телефон", SSISNumber="11111", GroupID = 6, Regex = @"^\d+$", Error = "Поле должно состоять из цифр"},
                new SSISPayment(){ SSISPaymentID = 9, Information="Введите 13 цифр номера договора", NumberLabel="Номер договора", Name="Интернет (BYFLY,ZALA)", SSISNumber="55555", GroupID = 7, Regex = @"^\d+$", Error = "Поле должно состоять из цифр" },
                new SSISPayment(){ SSISPaymentID = 10, Information="Необходимо ввести: \n областной центр: 2 + номер телефона  \n районные центры: две последних цифры кода региона + номер телефона", NumberLabel="Номер телефона", Name="Телефон", SSISNumber="11111", GroupID = 7, Regex = @"^\d+$", Error = "Поле должно состоять из цифр"},
                new SSISPayment(){ SSISPaymentID = 11, Information="Введите 13 цифр номера договора", NumberLabel="Номер договора", Name="Интернет (BYFLY,ZALA)", SSISNumber="55555", GroupID = 8, Regex = @"^\d+$", Error = "Поле должно состоять из цифр" },
                new SSISPayment(){ SSISPaymentID = 12, Information="Необходимо ввести: \n областной центр: 2 + номер телефона  \n районные центры: две последних цифры кода региона + номер телефона", NumberLabel="Номер телефона", Name="Телефон", SSISNumber="11111", GroupID = 8, Regex = @"^\d+$", Error = "Поле должно состоять из цифр"},
                new SSISPayment(){ SSISPaymentID = 13, Information="Введите 13 цифр номера договора", NumberLabel="Номер договора", Name="Интернет (BYFLY,ZALA)", SSISNumber="55555", GroupID = 9, Regex = @"^\d+$", Error = "Поле должно состоять из цифр" },                
                new SSISPayment(){ SSISPaymentID = 14, Information="Необходимо ввести: \n областной центр: 2 + номер телефона  \n районные центры: две последних цифры кода региона + номер телефона", NumberLabel="Номер телефона", Name="Телефон", SSISNumber="11111", GroupID = 9, Regex = @"^\d+$", Error = "Поле должно состоять из цифр"},
                

                new SSISPayment(){ SSISPaymentID = 2, Information="Введите 12 цифр номера моб телефона в формате 375YYXXXXXXX, зарегистрированного на сайте lotopay.by", NumberLabel="Номер телефона", Name="Билет", SSISNumber="11111", GroupID = 11, Regex = @"^\d+$", Error = "Поле должно состоять из цифр"},
                new SSISPayment(){ SSISPaymentID = 2, Information="Введите 12 цифр номера моб телефона в формате 375YYXXXXXXX, зарегистрированного на сайте lotopay.by", NumberLabel="Номер телефона", Name="Двойка", SSISNumber="11111", GroupID = 11, Regex = @"^\d+$", Error = "Поле должно состоять из цифр"},
                new SSISPayment(){ SSISPaymentID = 2, Information="Введите 12 цифр номера моб телефона в формате 375YYXXXXXXX, зарегистрированного на сайте lotopay.by", NumberLabel="Номер телефона", Name="Билет", SSISNumber="11111", GroupID = 12, Regex = @"^\d+$", Error = "Поле должно состоять из цифр"},
                new SSISPayment(){ SSISPaymentID = 2, Information="Введите 12 цифр номера моб телефона в формате 375YYXXXXXXX, зарегистрированного на сайте lotopay.by", NumberLabel="Номер телефона", Name="Четверка", SSISNumber="11111", GroupID = 12, Regex = @"^\d+$", Error = "Поле должно состоять из цифр"},

                new SSISPayment(){ SSISPaymentID = 2, Information="Код платежа вы можете получить на сайте poezd.rw.by", NumberLabel="Код платежа", Name="Оплата железнодорожных билетов", SSISNumber="11111", GroupID = 10, Regex = @"^\d+$", Error = "Поле должно состоять из цифр"},
                new SSISPayment(){ SSISPaymentID = 2, Information="Номер заказа можно получить на сайте www.ticketpro.by", NumberLabel="Номер заказа", Name="ticketpro.by - оплата билетов", SSISNumber="11111", GroupID = 10, Regex = @"^\d+$", Error = "Поле должно состоять из цифр"},
                new SSISPayment(){ SSISPaymentID = 2, Information="Номер заказа билета можно получить на сайте www.kvitki.by", NumberLabel="Номер заказа билета", Name="kvitki.by - оплата билетов", SSISNumber="11111", GroupID = 10, Regex = @"^\d+$", Error = "Поле должно состоять из цифр"},
                new SSISPayment(){ SSISPaymentID = 2, Information="Номер заказа билетов можно получить на сайте kino.bycard.by", NumberLabel="Номер заказа билетов", Name="bycard.by - билеты", SSISNumber="11111", GroupID = 10, Regex = @"^\d+$", Error = "Поле должно состоять из цифр"},
                new SSISPayment(){ SSISPaymentID = 2, Information="Введите номер заказа, полученный Вами при бронировании авиабилетов", NumberLabel="Номер заказа", Name="belavia.by -авиабилеты Белавиа", SSISNumber="11111", GroupID = 10, Regex = @"^\d+$", Error = "Поле должно состоять из цифр"},
                
                new SSISPayment(){ SSISPaymentID = 2, Information="Лицевой счет = № договора + № Вашей квартиры. Справка по тел. (029) 654-78-25", NumberLabel="Лицевой счет", Name="Изготовление ключей", SSISNumber="11111", GroupID = 14, Regex = @"^\d+$", Error = "Поле должно состоять из цифр"},
                new SSISPayment(){ SSISPaymentID = 2, Information="Лицевой счет = № договора + № Вашей квартиры. Справка по тел. (029) 654-78-25", NumberLabel="Лицевой счет", Name="Техническое обслуживание", SSISNumber="11111", GroupID = 14, Regex = @"^\d+$", Error = "Поле должно состоять из цифр"},
                new SSISPayment(){ SSISPaymentID = 2, Information="Введите 12 цифр лицевого счета, предоставляемого Вам компанией", NumberLabel="Лицевой счет", Name="Установка и ремонт", SSISNumber="11111", GroupID = 15, Regex = @"^\d+$", Error = "Поле должно состоять из цифр"},                
                new SSISPayment(){ SSISPaymentID = 2, Information="Введите 12 цифр лицевого счета, предоставляемого Вам компанией", NumberLabel="Лицевой счет", Name="Техническое обслуживание", SSISNumber="11111", GroupID = 15, Regex = @"^\d+$", Error = "Поле должно состоять из цифр"},                
                new SSISPayment(){ SSISPaymentID = 2, Information="Введите 12 цифр лицевого счета, предоставляемого Вам компанией", NumberLabel="Лицевой счет", Name="Ключи, прочие услуги компании", SSISNumber="11111", GroupID = 15, Regex = @"^\d+$", Error = "Поле должно состоять из цифр"},                
                new SSISPayment(){ SSISPaymentID = 2, Information="Введите номер абонента", NumberLabel="Номер абонента", Name="Домосервис", SSISNumber="11111", GroupID = 13, Regex = @"^\d+$", Error = "Поле должно состоять из цифр"},
                new SSISPayment(){ SSISPaymentID = 2, Information="Введите номер абонента", NumberLabel="Номер абонента", Name="СтройДомофонСвет", SSISNumber="11111", GroupID = 13, Regex = @"^\d+$", Error = "Поле должно состоять из цифр"},


                new SSISPayment(){ SSISPaymentID = 5, Group="Интернет", Information="Введите номер контракта", NumberLabel="Номер контракта", Name="Космос ТВ", SSISNumber="44444" , GroupID = 16, Regex = @"^\d+$", Error = "Поле должно состоять из цифр" },
                new SSISPayment(){ SSISPaymentID = 6, Group="Интернет", Information="Введите номер контракта", NumberLabel="Номер контракта", Name="NIKS", SSISNumber="44444", GroupID = 16, Regex = @"^\d+$", Error = "Поле должно состоять из цифр" },
                new SSISPayment(){ SSISPaymentID = 6, Group="Интернет", Information="Введите номер контракта", NumberLabel="Номер контракта", Name="МТС - Домашний интернет", SSISNumber="44444", GroupID = 16, Regex = @"^\d+$", Error = "Поле должно состоять из цифр" },
                new SSISPayment(){ SSISPaymentID = 6, Group="Интернет", Information="Введите номер контракта", NumberLabel="Номер контракта", Name="Айчына Плюс", SSISNumber="44444", GroupID = 16, Regex = @"^\d+$", Error = "Поле должно состоять из цифр" },
                new SSISPayment(){ SSISPaymentID = 6, Group="Интернет", Information="Введите номер лицевого счета", NumberLabel="Введите номер лицевого счета", Name="Абонплата за КТВ", SSISNumber="44444", GroupID = 18, Regex = @"^\d+$", Error = "Поле должно состоять из цифр" },
                new SSISPayment(){ SSISPaymentID = 6, Group="Интернет", Information="Введите номер лицевого счета", NumberLabel="Введите номер лицевого счета", Name="Подключение к КТВ", SSISNumber="44444", GroupID = 18, Regex = @"^\d+$", Error = "Поле должно состоять из цифр" },
                new SSISPayment(){ SSISPaymentID = 6, Group="Интернет", Information="Введите номер код учетной записи", NumberLabel="Код учетной записи", Name="По коду учетной записи", SSISNumber="44444", GroupID = 19, Regex = @"^\d+$", Error = "Поле должно состоять из цифр" },
                new SSISPayment(){ SSISPaymentID = 6, Group="Интернет", Information="Введите номер заказа", NumberLabel="Номер заказа", Name="По номеру заказа", SSISNumber="44444", GroupID = 19, Regex = @"^\d+$", Error = "Поле должно состоять из цифр" },
                new SSISPayment(){ SSISPaymentID = 6, Group="Интернет", Information="Введите номер лицевого счета", NumberLabel="Введите номер лицевого счета", Name="Хостер Бай hoster.by", SSISNumber="44444", GroupID = 17, Regex = @"^\d+$", Error = "Поле должно состоять из цифр" },
                new SSISPayment(){ SSISPaymentID = 6, Group="Интернет", Information="Введите номер лицевого счета", NumberLabel="Введите номер лицевого счета", Name="Freespace.by", SSISNumber="44444", GroupID = 17, Regex = @"^\d+$", Error = "Поле должно состоять из цифр" },

                new SSISPayment(){ SSISPaymentID = 6, Group="Интернет", Information="введите регистрационный номер правонарушения (11 цифр), указанный в копии постановления", NumberLabel="регистрационный N нарушения", Name="Скоростной режим", SSISNumber="44444", GroupID = 21, Regex = @"^\d+$", Error = "Поле должно состоять из цифр" },
                
                new SSISPayment(){ SSISPaymentID = 1, Group="Мобильная связь", Information="Введите 9 цифр номера телефона в формате: 29ххххххх, 33ххххххх, 25ххххххх, 44ххххххх", NumberLabel="Номер телефона", Name="МТС по N телефона", SSISNumber="11111", GroupID = 25, Regex = @"^\d{9}$", Error = "Номер телефона должен состоять из девяти цифр (+375*********)"},
                new SSISPayment(){ SSISPaymentID = 2, Group="Мобильная связь", Information="Введите 9 цифр номера телефона в формате: 29ххххххх, 44ххххххх, 25ххххххх, 33ххххххх", NumberLabel="Номер телефона", Name="Velcom по N телефона", SSISNumber="22222", GroupID = 24, Regex = @"^\d{9}$", Error = "Номер телефона должен состоять из девяти цифр (+375*********)"},                
                new SSISPayment(){ SSISPaymentID = 3, Group="Мобильная связь", Information="Введите 9 цифр номера телефона в формате: 29ххххххх, 33ххххххх, 25ххххххх, 44ххххххх", NumberLabel="Номер телефона", Name="Life :) на номер телефона", SSISNumber="33333", GroupID = 23, Regex = @"^\d{9}$", Error = "Номер телефона должен состоять из девяти цифр (+375*********)"},
                new SSISPayment(){ SSISPaymentID = 1, Group="Мобильная связь", Information="Введите 12 цифр лицевого счета в формате: 207ххххххххх или 44хххххххххх", NumberLabel="Лицевой счет", Name="МТС по л/счету", SSISNumber="11111", GroupID = 25, Regex = @"^\d+$", Error = "Поле должно состоять из цифр"},
                new SSISPayment(){ SSISPaymentID = 2, Group="Мобильная связь", Information="Введите номер лицевого счета", NumberLabel="Лицевой счет", Name="velcom - по л/счету", SSISNumber="22222", GroupID = 24, Regex = @"^\d+$", Error = "Поле должно состоять из цифр"},                
                new SSISPayment(){ SSISPaymentID = 3, Group="Мобильная связь", Information="Введите номер лицевого счета", NumberLabel="Лицевой счет", Name="life :) на расчетный счет", SSISNumber="33333", GroupID = 23, Regex = @"^\d+$", Error = "Поле должно состоять из цифр"},

                new SSISPayment(){ SSISPaymentID = 2, Group="Мобильная связь", Information="Введите номер счета, полученный при оформлении подписки", NumberLabel="Номер счета", Name="Советская Белоруссия sb.by", SSISNumber="22222", GroupID = 29, Regex = @"^\d+$", Error = "Поле должно состоять из цифр"},                
                new SSISPayment(){ SSISPaymentID = 3, Group="Мобильная связь", Information="Введите номер счета, полученный при оформлении подписки", NumberLabel="Номер счета", Name="Наша Нiва", SSISNumber="33333", GroupID = 29, Regex = @"^\d+$", Error = "Поле должно состоять из цифр"},

                new SSISPayment(){ SSISPaymentID = 1, Group="Мобильная связь", Information="Введите номер телефонв (9 цифр)", NumberLabel="Номер телефона", Name="Кнопка помощи ООО", SSISNumber="11111", GroupID = 30, Regex = @"^\d+$", Error = "Поле должно состоять из цифр"},
                new SSISPayment(){ SSISPaymentID = 2, Group="Мобильная связь", Information="Введите номер лицевого счета", NumberLabel="Лицевой счет", Name="Свилсофт коррекция зрения", SSISNumber="22222", GroupID = 30, Regex = @"^\d+$", Error = "Поле должно состоять из цифр"},                
                new SSISPayment(){ SSISPaymentID = 3, Group="Мобильная связь", Information="Введите номер договора", NumberLabel="Номер договора", Name="РНПЦ травмотологии и ортопедии", SSISNumber="33333", GroupID = 31, Regex = @"^\d+$", Error = "Поле должно состоять из цифр"},

                new SSISPayment(){ SSISPaymentID = 1, Group="Мобильная связь", Information="Введите номер заказа", NumberLabel="Номер заказа", Name="Топ-Тур - toptour.by", SSISNumber="11111", GroupID = 32, Regex = @"^\d+$", Error = "Поле должно состоять из цифр"},
                new SSISPayment(){ SSISPaymentID = 2, Group="Мобильная связь", Information="Введите номер заказа", NumberLabel="Номер заказа", Name="Интеллектсофт медиа itravel.by", SSISNumber="22222", GroupID = 32, Regex = @"^\d+$", Error = "Поле должно состоять из цифр"},                
                new SSISPayment(){ SSISPaymentID = 3, Group="Мобильная связь", Information="Введите номер заявки, который Вы получили на сайте www.ekskursii.by", NumberLabel="Номер заявки", Name="ekskursii.by - экскурсии", SSISNumber="33333", GroupID = 33, Regex = @"^\d+$", Error = "Поле должно состоять из цифр"},

                new SSISPayment(){ SSISPaymentID = 1, Group="Мобильная связь", Information="Введите номер эл. кошелька (8 цифр)", NumberLabel="Номер кошелька", Name="Продажа EasyPay", SSISNumber="11111", GroupID = 36, Regex = @"^\d+$", Error = "Поле должно состоять из цифр"},
                new SSISPayment(){ SSISPaymentID = 2, Group="Мобильная связь", Information="Введите номер эл. кошелька", NumberLabel="Номер кошелька", Name="Продажа WMB (эл.денег)", SSISNumber="22222", GroupID = 36, Regex = @"^\d+$", Error = "Поле должно состоять из цифр"},                
                new SSISPayment(){ SSISPaymentID = 3, Group="Мобильная связь", Information="Введите номер полиса", NumberLabel="Номер полиса", Name="Белросстрах", SSISNumber="33333", GroupID = 35, Regex = @"^\d+$", Error = "Поле должно состоять из цифр"},
                new SSISPayment(){ SSISPaymentID = 1, Group="Мобильная связь", Information="Введите номер полиса", NumberLabel="Номер полиса", Name="Стравита", SSISNumber="11111", GroupID = 35, Regex = @"^\d+$", Error = "Поле должно состоять из цифр"},
                
            };
            ssisPayments.ForEach(s => context.SSISPayments.Add(s));
            context.SaveChanges();

            context.MobileAutoPayments.Add(new MobileAutoPay()
            {
                Amount = 0,
                CustomerID = 0,
                Interval = new TimeSpan(12341234),
                LastExecutionDate = DateTime.Now,
                MobileNumber = "1",
                MobileOperator = "asdf",
                PayCardId = 123,
                StartDate = DateTime.Now
            });
            context.SaveChanges();
            var removeobj = context.MobileAutoPayments.Find(1);
            context.MobileAutoPayments.Remove(removeobj);
            context.SaveChanges();


            var ssisOwnPayments = new List<OwnSSISPayment>
            {               
                
            };
            ssisOwnPayments.ForEach(s => context.OwnSSISPayments.Add(s));
            context.SaveChanges();
        }
    }    
}




