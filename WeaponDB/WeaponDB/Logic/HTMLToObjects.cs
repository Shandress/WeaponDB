﻿using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WeaponDB.Data;
using WeaponDB.Data.Weapon_stuff;
using WeaponDB.Exceptions;
using Wintellect.Sterling.Database;
using Wintellect.Sterling.Serialization;

namespace WeaponDB.Logic
{
    /// <summary>
    /// Used to transform data extracted from HTMLNodes into objects.
    /// </summary>
    class HTMLToObjects
    {
        /// <summary>
        /// Transforms each a list of HTML elements into a list of Weapon objects.
        /// </summary>
        /// <param name="actionTypes">Used to read abbreviations found in the.</param>
        /// <param name="subTypes">Used to read abbreviations found in the article.</param>
        /// <param name="rows">A collection of nodes to extract data from.</param>
        /// <param name="weaponType">A weapon category or type e.g. a submachine gun or a shotgun</param>
        /// <returns>A list of Weapon objects (row ==> Weapon).</returns>
        internal static List<Weapon> ParseRows(Dictionary<string, string> actionTypes,
                                           Dictionary<string, string> subTypes,
                                           IEnumerable<HtmlNode> rows, string weaponType)
        {
            if(actionTypes == null || subTypes == null)
            {
                return new List<Weapon>();
            }

            List<Weapon> res = new List<Weapon>();
            HtmlNode headerNode = rows.First();

            // List of headers.
            IEnumerable<string> headers = headerNode.ChildNodes
                .Where(n => n.NodeType != HtmlNodeType.Text)
                .Select(el => el.InnerText);

            // List of rows (string lists) of data.
            IEnumerable<IEnumerable<HtmlNode>> data = rows
                .Except(new HtmlNode[] { headerNode })
                .Select(r => r.ChildNodes
                    .Where(n => n.NodeType != HtmlNodeType.Text));

            foreach (IEnumerable<HtmlNode> row in data)
            {
                Weapon weapon = new Weapon();
                for (int i = 0; i < row.Count(); i++)
                {
                    if (i >= 6) { break; }
                    HtmlNode currentColumn = row.ElementAt(i);
                    switch (headers.ElementAt(i))
                    {
                        case "Type":
                            WeaponType wType = new WeaponType();
                            wType.Name = weaponType;
                            string[] types = currentColumn.InnerText.Split('\n');
                            if(types.Length == 0)
                            {
                                wType.ActionTypes.Add(Constants.Unknown);
                                wType.SubType = Constants.Unknown;
                                //throw new UnexpectedInputException("There should be something, I guess");
                            }

                            // Only action type is present.
                            if (types.Length == 1)
                            {
                                wType.SubType = weaponType;

                                string actionType = (actionTypes.ContainsKey(types[0]))
                                ? actionTypes[types[0]] : Constants.Unknown;
                                wType.ActionTypes.Add(actionType);
                            }
                            else
                            {
                                int idx = types.Length - 1;
                                wType.SubType = (subTypes.ContainsKey(types[idx]))
                                    ? subTypes[types[idx]] : Constants.Unknown;
                                idx--;
                                for (int j = 0; j <= idx; j++)
                                {
                                    string actionType = (actionTypes.ContainsKey(types[j]))
                                    ? actionTypes[types[j]] : Constants.Unknown;
                                    wType.ActionTypes.Add(actionType);
                                }
                            }  
                            weapon.WeaponType = wType;
                            break;
                        case "Image":
                            string path = Constants.WeaponImgDir;
                            string filename = weapon.Name; 
                          
                            Image img = (Image)GetImage(currentColumn, ParsingTarget.WeaponImage).Clone();
                           

                            SaveImage(img, Constants.WeaponImgDir, weapon.Name);

                            weapon.ImageReference = Constants.WeaponImgDir + "/" + weapon.Name + ".jpg";
                            break;
                        case "Country":
                            weapon.Country = GetCountryInfo(currentColumn);
                            break;
                        case "Cartridge":
                            weapon.Cartridges = GetCartridges(currentColumn);
                            break;
                        default:
                            PropertyInfo property = weapon.GetType().GetProperty(headers.ElementAt(i));
                            string text = row.ElementAt(i).InnerText;
                            try
                            {
                                if (text != string.Empty)
                                {
                                    property.SetValue(weapon,
                                        (property.PropertyType is UInt32)
                                            ? (object)UInt32.Parse(text)
                                            : (object)text);
                                }

                            }
                            catch (ArgumentException)
                            {
                                throw new UnexpectedInputException("Unexpected unparseable to int.");
                            }
                            break;
                    }
                }
                res.Add(weapon);
            }

            return res;
        }

        /// <summary>
        /// Loads an image from a specified source.
        /// </summary>
        /// <param name="node">An ansector node to the one that contains the link to the image.</param>
        /// <param name="toParse">Specifies the type of a node to load an image from.</param>
        /// <returns>A System.Drawing.Image.</returns>
        private static Image GetImage(HtmlNode node, ParsingTarget toParse)
        {
            string imgClass;
            switch (toParse)
            {
                case ParsingTarget.Flag:
                    imgClass = "thumbborder";
                    break;
                case ParsingTarget.WeaponImage:
                    imgClass = "thumbimage";
                    break;
                default:
                    throw new UnexpectedInputException("Unexpected parsing target.");
            }
            HtmlNode imgNode = node.SelectSingleNode(@"//img[@class='" + imgClass + "']");
           if(imgNode == null)
           {
               if(node.HasChildNodes)
               {
                   imgNode = node.SelectSingleNode(@"//a/img");
               }
               else
               {
                   return (Image)Image.FromFile(Constants.imgNotFound).Clone();
               }
               
           }
            string src = (imgNode.Attributes["src"] != null) ? imgNode.Attributes["src"].Value : string.Empty;
            if (src == string.Empty)
            {
                return null;
            }
            try
            {
                using (var wc = new WebClient())
                {
                    using (var imgStream = new MemoryStream(wc.DownloadData("https:" + src)))
                    {
                        using (var image = Image.FromStream(imgStream))
                        {
                            return new Bitmap(image);
                        }
                    }
                }
            }
            catch (IOException)
            {
                return null;
            }

        }

        /// <summary>
        /// Gets the information about the weapon's country of origin: it's name and a picture of a flag.
        /// </summary>
        /// <param name="node">An ansector node to nodes that contain the necessary data. </param>
        /// <returns>A Country</returns>
        private static Country GetCountryInfo(HtmlNode node)
        {
            var x = node.ChildNodes
                .Where(n => n.NodeType != HtmlNodeType.Text);
            string name;
            if(x.Count() == 0)
            {
                name = node.InnerText;
            }
            else
            {
            name = (x.Count() == 1)
                ? x.ElementAt(0).InnerText
                : x.ElementAt(1).InnerText;
            }

            if(x.Count() > 1)
            {
                string fullPath = Constants.flagImgDir + "/" + name;
                Image img = (Image)GetImage(x.First(), ParsingTarget.Flag).Clone();
                SaveImage(img, Constants.flagImgDir, name);
                return new Country(name, fullPath);  
                
            }
            else
            {
                return new Country(name, Constants.imgNotFound);
            }
            
        }

        /// <summary>
        /// Gets the information about the ammo the weapon's using.
        /// </summary>
        /// <param name="node">An ansector node to nodes that contain the necessary data.</param>
        /// <returns></returns>
        private static List<string> GetCartridges(HtmlNode node)
        {
            return node.InnerText.Split('\n').ToList(); //node.SelectNodes(@"//a")
                //.Where(n => n.NodeType == HtmlNodeType.Element)
                //.Select(n => n.InnerText)
                //.ToList();
        }

        private static void SaveImage(Image toSave, string path, string filename)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if (!File.Exists(path + "/" + filename))
            {
                if (filename.Contains("/"))
                {
                    filename = filename.Replace('/', ' ');
                }
                toSave.Save(path + "/" + filename + ".jpg");
            }
            toSave.Dispose();
        }
    }

    /// <summary>
    /// Shows what is needed to be parsed.
    /// </summary>
    enum ParsingTarget { None, Flag, WeaponImage }
}
