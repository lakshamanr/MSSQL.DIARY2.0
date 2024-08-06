﻿using MSSQL.DIARY.COMN.Models;
using System.Collections.Generic;

namespace MSSQL.DIARY.COMN.Helper
{
    public class Node
    {
        public bool IblnFirstNode { get; set; }
        public List<ReferencesModel> ReferencesModels { get; set; }

        public Node(string n)
        {
            name = n;
            Soon = new List<Node>();
        }

        public string name;
        public string StyleClass { get; set; }
        public List<Node> Soon;

        public string PrimengToJson()
        {
            string s = "";
            if (IblnFirstNode)
            {
                s = s + "{" + "\"data\":[";
                IblnFirstNode = true;
            }
            else
            {
                s = s
                    //+ "{\"data\":{\""
                    + "{\"label\":\""
                    + name
                    + "\","
                    + "\"" + "expandedIcon" + "\"" + ":" + "\"" + "fa fa-folder-open" + "\"" + ","
                    + "\"" + "styleClass" + "\"" + ":" + "\"" + "TreeViewColor" + "\"" + ","
                    + "\"" + "collapsedIcon" + "\"" + ":" + "\"" + "fa fa-folder-close" + "\""
                    + ",\"children\":[";
            }
            bool f = true;
            foreach (Node n in Soon)
            {
                if (f) { f = !f; } else { s = s + ","; }
                s = s + n.PrimengToJson();
            }
            s = s + "]}";
            return s;
        }

        public string amexioToJson()
        {
            string s = "";
            if (IblnFirstNode)
            {
                s = s + "{\"data\":{\"" + "name" + "\":" + "\"" + name + "\"}" + ",\"data\":[";
                IblnFirstNode = true;
            }
            else
            {
                s = s
                        + "{\"text\":\"" + name + "\","
                        + "\"" + "icon" + "\"" + ":" + "\"" + "fa fa-folder-open" + "\"" + ","
                        + "\"" + "expand" + "\"" + ":" + "\"" + "true" + "\""
                        + ",\"children\":["
                    ;
            }
            bool f = true;
            foreach (Node n in Soon)
            {
                if (f) { f = !f; } else { s = s + ","; }
                s = s + n.amexioToJson();
            }
            s = s + "]}";
            return s;
        }
    }
}