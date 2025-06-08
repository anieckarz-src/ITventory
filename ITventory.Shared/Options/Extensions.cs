using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace ITventory.Shared.Options
{
    public static class Extensions
    {
        public static TOptions GetOptions<TOptions>(this IConfiguration configuration, string sectionName)
            where TOptions : new()

        {
            var options = new TOptions();
            configuration.GetSection(sectionName).Bind(options);

            //Metoda get section pobiera dany fragment z pliku konfiguracyjnego, a Binder dopasowywuje
            //Parametry z pliku konfiguracyjnego do pól klasy

            return options;

            //Parametr generyczny jest po to, abyśmy mogli użyć tej metody wobec kilku różnych rodzajów konfiguracji
            //Bez koniecznośći pisania kolejnych osobnych metod
        }
    }
}
