using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeaponDB.Data.Weapon_stuff;
using HtmlAgilityPack;
using System.Reflection;
using System.Drawing;
using System.Net;
using System.IO;
using WeaponDB.Exceptions;
using WeaponDB.Data;


namespace WeaponDB.Logic
{

    /// <summary>
    /// 
    /// </summary>
    class HTMLParser
    {
        /// <summary>
        /// The root node of a document.
        /// </summary>
        private HtmlNode root;

        /// <summary>
        /// Used to construct relative paths.
        /// </summary>
        private string baseUrl;

        /// <summary>
        /// Creates a new instance of an HtmlParser for a specified web page. 
        /// </summary>
        /// <param name="url">A web page url.</param>
        /// <param name="baseUrl">Used to construct relative paths.</param>
        /// <param name="isRelative">shows if the path for this parser is relative.</param>
        public HTMLParser(string url, string baseUrl)
        {
            HtmlWeb w = new HtmlWeb();

            HtmlDocument doc = w.Load(baseUrl + url);
            root = doc.DocumentNode;
            this.baseUrl = baseUrl;
        }

        /// <summary>
        /// Parses a table with weapons action type or subtype abbreviations.
        /// </summary>
        /// <param name="xPaths">Two xPaths to parse table header and content.</param>
        /// <returns>A dictionary with pairs { abbreviation, meaning }.</returns>
        private Dictionary<string, string> ParseTables(params string[] xPaths)
        {
            Dictionary<string, string> res = new Dictionary<string,string>();
            if(xPaths.Length != 2)
            {
                return null;
            }
            IEnumerable<HtmlNode> keyNodes = root.SelectNodes(xPaths[0]);
            IEnumerable<HtmlNode> valueNodes = root.SelectNodes(xPaths[1]);

            if (keyNodes == null || valueNodes == null)
            {
                return null;
            }

            IEnumerable<string> keys = keyNodes.Select(n => n.InnerText);
            IEnumerable<string> values = valueNodes.Select(n => n.InnerText);

            bool success = true;
            if(keys.Count() != values.Count())
            {
                success = false;
            }

            if(success)
            {
                for(int i = 1; i < values.Count(); i++)
                {
                    res.Add(keys.ElementAt(i), values.ElementAt(i));
                }
            }
            else
            {
                // Use custom data.
                //throw new UnexpectedInputException("Oh, crap! Source data is messed up.");
            }

            return res;
        }

        /// <summary>
        /// Gets a string representing a weapon type from an article header.
        /// </summary>
        /// <returns>A string representing weapon type.</returns>
        private string GetWeaponType()
        {
            string header = root.SelectSingleNode(Constants.XPaths.header).InnerText;
            string[] words = header.Split(new char[] { ' ' });
            int idx = words.Length - 1;
            
            string res = CapitalizeFirstLetter(words[2]) + " ";

            if(words.Length == 3)
            {
                return ProcessWord(res);
            }
            else
            {
                string lastWord = ProcessWord(words[idx]); 
                for (int i = 3; i < words.Length - 1; i++)
                {
                    res += words[i] + " ";
                }
                return string.Concat(res, lastWord);
            }
            
        }

        /// <summary>
        /// Makes the first letter of a word UPPERCASE.
        /// </summary>
        /// <param name="word">An input string.</param>
        private string CapitalizeFirstLetter(string word)
        {
           return word.First().ToString().ToUpper() + string.Join("", word.Skip(1));
        }

        /// <summary>
        /// Removes last letter from a word.
        /// </summary>
        /// <param name="word">An imput string.</param>
        /// <returns></returns>
        private string ProcessWord(string word)
        {
            string res = string.Empty;
            for(int i = 0; i < word.Length - 1; i++)
            {
                res += word[i];
            }
            return res;
        }

        /// <summary>
        /// Processes an article on weapons of certain category e.g. submachine gun or pistols.
        /// </summary>
        /// <returns>A list of Weapon object</returns>
        public List<Weapon> ProcessArticle()
        {
            Dictionary<string, string> actionTypes = ParseTables
                (Constants.XPaths.actionTypesHeaders, Constants.XPaths.actionTypesContent);

            Dictionary<string, string> subTypes = ParseTables
                (Constants.XPaths.subTypesHeaders, Constants.XPaths.subTypesContent);
            bool headersPresent = actionTypes != null && subTypes != null;

            IEnumerable<HtmlNode> rows = root
                .SelectNodes(Constants.XPaths.article);
            string weaponType = GetWeaponType();

            return HTMLToObjects.ParseRows(actionTypes, subTypes, rows, weaponType);
     
        }

        /// <summary>
        /// Processes all articles and returns a list of Weapon objects.
        /// </summary>
        /// <returns>A list of Weapon object</returns>
        public IEnumerable<Weapon> ProcessData()
        {
            List<Weapon> res = new List<Weapon>();
            IEnumerable<string> links = root
                .SelectNodes(Constants.XPaths.articleLinks)
                .Select(n => n.Attributes["href"].Value);

            foreach(string link in links)
            {
                HTMLParser articleParser = new HTMLParser(link, this.baseUrl);
                res.AddRange(articleParser.ProcessArticle());
            }
            return res;
            
        }
        
        
    }
}
