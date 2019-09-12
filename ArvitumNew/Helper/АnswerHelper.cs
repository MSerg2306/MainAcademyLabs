using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC1.Helper
{
    public static class АnswerHelper
    {
        public static HtmlString CreateАnswer(this IHtmlHelper html, int TypeАnswer)
        {
            string result = null;
            switch (TypeАnswer)
            {
                case 1:
                    {
                        result += "<h1>Данное имя уже используется</h1>";
                        result += "<a href=\"/Home/NewClient\">Вернуться к окну регистрации</a>";
                        break;
                    }
                case 2:
                    {
                        result += "<h1>Спасибо за регистрацию</h1>";
                        result += "<a href=\"/Home/Login\">Вернуться к окну авторизации</a>";
                        break;
                    }
                case 3:
                    {
                        result += "<h1>Добавлен новый телефон</h1>";
                        result += "<br />";
                        result += "<a href=\"/PhoneShop/Home/Index\">Вернуться к списку телефонов</a>";
                        result += "<br />";
                        result += "<a href=\"/PhoneShop/Home/AddPhone\">Добавить еще один телефон</a>";
                        result += "<br />";
                        break;
                    }
                case 4:
                    {
                        result += "<h1>Нужны права администратора, обратитесь в тех.поддержку...</h1>";
                        result += "<br />";
                        result += "<a href=\"/Home/Index\">Вернуться на Главную страницу</a>";
                        result += "<br />";
                        break;
                    }
                default:
                    {
                        result += "<a href=\"/PhoneShop/Home/Index\">Вернуться к списку телефонов</a>";
                        break;
                    }
            }
            return new HtmlString(result);
        }

    }
}
