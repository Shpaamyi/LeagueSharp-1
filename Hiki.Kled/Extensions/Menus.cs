﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp.Common;

namespace Hiki.Kled.Extensions
{
    internal static class Menus
    {
        public static Menu Config;
        public static Orbwalking.Orbwalker Orbwalker;

        public static void Initialize()
        {
            Config = new Menu("Hiki Kled","Hiki Kled",true);
            {
                Orbwalker = new Orbwalking.Orbwalker(Config.SubMenu("Orbwalker Settings"));

                var combomenu = new Menu("Combo Settings","Combo Settings");
                {
                    combomenu.AddItem(new MenuItem("q.combo", "Use [Q] ").SetValue(true));
                    combomenu.AddItem(new MenuItem("skaarl.q.combo", "Use [Skaarl Q] ").SetValue(true));
                    combomenu.AddItem(new MenuItem("e.mode", "E Type").SetValue(new StringList(new[] { "Safe", "Cursor Position" },1)));
                    combomenu.AddItem(new MenuItem("e.combo", "Use [E] ").SetValue(true));
                    Config.AddSubMenu(combomenu);
                }

                var harassmenu = new Menu("Harass Settings", "Harass Settings");
                {
                    harassmenu.AddItem(new MenuItem("q.harass", "Use [Q] ").SetValue(true));
                    harassmenu.AddItem(new MenuItem("skaarl.q.harass", "Use [Skaarl Q] ").SetValue(true));
                    Config.AddSubMenu(harassmenu);
                }

                var clearmenu = new Menu("Wave Settings", "Wave Settings");
                {
                    clearmenu.AddItem(new MenuItem("q.clear", "Use [Q]").SetValue(true));
                    clearmenu.AddItem(new MenuItem("min.count", "Min. Minion Count").SetValue(new Slider(3, 1, 5)));
                    Config.AddSubMenu(clearmenu);
                }

                var junglemenu = new Menu("Jungle Clear Settings", "Jungle Clear Settings");
                {
                    junglemenu.AddItem(new MenuItem("q.jungle", "Use [Q]").SetValue(true));
                    junglemenu.AddItem(new MenuItem("q.skaarl.jungle", "Use [Skaarl Q]").SetValue(true));
                    junglemenu.AddItem(new MenuItem("e.jungle", "Use [E]").SetValue(true));
                    Config.AddSubMenu(junglemenu);
                }
                Config.AddItem(new MenuItem("keysinfo", "                           General Settings").SetFontStyle(System.Drawing.FontStyle.Bold, SharpDX.Color.Gold));
                Config.AddItem(new MenuItem("hitchance", "Hit Chance ?").SetValue(new StringList(Utilities.HitchanceNameArray, 2)));
                Config.AddItem(new MenuItem("prediction", "Prediction ?").SetValue(new StringList(new[] { "Common", "Sebby", "sPrediction" }, 1)))
                    .ValueChanged += (s, ar) =>
                    {
                        Config.Item("pred.info").Show(ar.GetNewValue<StringList>().SelectedIndex == 2);
                    };
                Config.AddItem(new MenuItem("pred.info", "                 PRESS F5 FOR LOAD SPREDICTION").SetFontStyle(System.Drawing.FontStyle.Bold)).Show(Config.Item("prediction").GetValue<StringList>().SelectedIndex == 0);
                if (Config.Item("prediction").GetValue<StringList>().SelectedIndex == 2)
                {
                    SPrediction.Prediction.Initialize(Config, ":: sPrediction Settings");
                }
                Config.AddToMainMenu();
            }
        }
    }
}
