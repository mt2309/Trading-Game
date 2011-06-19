﻿using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Main_Game
{
    public class Effect
    {
        private uint p_health_restore;

        private double p_strength_mod;
        private double p_agility_mod;
        private double p_speed_mod;
        private double p_intelligence_mod;

        public uint health_restore { get { return p_health_restore; } set { p_health_restore = value; } }
        public double strength_mod { get { return p_strength_mod; } set { p_strength_mod = value; } }
        public double agility_mod { get { return p_agility_mod; } set { p_agility_mod = value; } }
        public double speed_mod { get { return p_speed_mod; } set { p_speed_mod = value; } }
        public double intelligence_mod { get { return p_intelligence_mod; } set { p_intelligence_mod = value; } }

        public Effect(uint hr, double stm, double am, double im, double spm)
        {
            p_health_restore = hr;
            p_strength_mod = stm;
            p_agility_mod = am;
            p_intelligence_mod = im;
            p_speed_mod = spm;
        }

        public Effect()
        {
            p_health_restore = 0;
            p_strength_mod = 1;
            p_agility_mod = 1;
            p_intelligence_mod = 1;
            p_speed_mod = 1;
        }
    } 
}