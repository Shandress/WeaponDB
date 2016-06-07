using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VelocityDb.Session;
using VelocityDb;

namespace WeaponDB.Data
{
    /// <summary>
    /// Contains useful data.
    /// </summary>
    static class Constants
    {
        public static readonly string SystemDir = "WeaponDB";
        public static readonly string Unknown = "N/A";
        
        /// <summary>
        /// Contains xPath strings to different parts of the article.
        /// </summary>
        public static class XPaths
        {
            public static readonly string actionTypesHeaders = @"//table[@class='wikitable'][1]/tr[1]/td";
            public static readonly string actionTypesContent = @"//table[@class='wikitable'][1]/tr[2]/td";
            public static readonly string subTypesHeaders = @"//table[@class='wikitable'][2]/tr[1]/td";
            public static readonly string subTypesContent = @"//table[@class='wikitable'][2]/tr[2]/td";
            public static readonly string article = @"//div[@id='mw-content-text']/table[3]/tr";
            public static readonly string header = @"//h1[@id='firstHeading']";
            public static readonly string articleLinks = @"//div[@id='mw-content-text']/ul[2]/li[5]/ul/li/a";
        }
    }
}
