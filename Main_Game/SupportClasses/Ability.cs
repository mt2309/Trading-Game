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
using System.Collections.Generic;

namespace Main_Game
{

    public abstract class Ability
    {
        public const int initialAbilityLimit = 2;
        public const int iconSize = 40;

        public static IDictionary<string, Ability> allAbilities = new Dictionary<string, Ability>();
        public string name { get; set; }
        public string description { get; set; }
        public int manaCost { get; set; }
        public Uri icon { get; set; }
        public Effect abilityEffect { get; set; }

        protected float p_attackbonus;
        public float attackbonus { get { return p_attackbonus; } }

        public Ability(string _name, string _description, int _manacost, Uri _icon, Effect _abilityEffect)
        {
            name = _name;
            description = _description;
            manaCost = _manacost;
            icon = _icon;
            abilityEffect = _abilityEffect;
            p_attackbonus = 0;
        }

        public static void populateAllAbility()
        {
            allAbilities.Add("Fireball", new MagicAbility("Fireball", "Burns the enemy for massive damage", 50,
                                        new Uri("Images/fireball.png", UriKind.Relative),
                                        new Effect(), 40));
            allAbilities.Add("Energy arrow", new MagicAbility("Energy arrow", "Ouch", 30,
                                        new Uri("Images/energyarrow.png", UriKind.Relative),
                                        new Effect(), 30));
            allAbilities.Add("Attack", new AttackAbility("Attack", "Attacks with equipped weapon", 0,
                                        new Uri("Images/attack.png", UriKind.Relative),
                                        new Effect(), 30));
            allAbilities.Add("Maim", new AttackAbility("Maim", "Injures the enemy with all your might", 10,
                                        new Uri("Images/maim.png", UriKind.Relative),
                                        new Effect(), 40));
        }

        public static Ability fetchAbility(string abilityname)
        {
            Ability a;
            allAbilities.TryGetValue(abilityname, out a);
            return a;
        }

        public abstract uint attack(Entity attacker, Entity defender);
    }

    public class AttackAbility : Ability
    {

        public AttackAbility(string _name, string _description, int _manacost, Uri _icon, Effect _abilityEffect, float _attackBonus)
            : base(_name, _description, _manacost, _icon, _abilityEffect)
        {
            this.p_attackbonus = _attackBonus;
        }

        public override uint attack(Entity attacker, Entity defender)
        {

            int hit = attacker.dice.roll();
            uint damage = 0;

            if (hit == 1 || (this.manaCost > attacker.currentMana))
                return damage;
            else
            {
                defender.applyEffect(abilityEffect);
                damage = (uint)Math.Floor(p_attackbonus * (float)attacker.strength * attacker.buffs.strength_mod);
                if (hit == 20)
                    damage *= 2;

                return damage;
            }

        }
    }

    public class SelfAbility : Ability
    {
        public SelfAbility(string _name, string _description, int _manacost, Uri _icon, Effect _abilityEffect)
            : base(_name, _description, _manacost, _icon, _abilityEffect)
        {
        }

        public override uint attack(Entity attacker, Entity defender)
        {
            if (manaCost < attacker.currentMana)
                attacker.applyEffect(abilityEffect);

            /* Will always return the same value */
            return 0;
        }
    }

    public class MagicAbility : AttackAbility
    {

        public MagicAbility(string _name, string _description, int _manacost, Uri _icon, Effect _abilityEffect, uint _attackBonus)
            : base(_name, _description, _manacost, _icon, _abilityEffect, _attackBonus) { }

        public override uint attack(Entity attacker, Entity defender)
        {
            int hit = attacker.dice.roll();
            uint damage = 0;

            if (hit == 1 || (manaCost > attacker.currentMana))
                return damage;
            else
            {
                defender.applyEffect(abilityEffect);
                damage = (uint)Math.Floor(p_attackbonus * (float)attacker.intelligence * attacker.buffs.intelligence_mod);
                if (hit == 20)
                    damage *= 2;

                return damage;
            }
        }
    }
}