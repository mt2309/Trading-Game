﻿using System;
using System.Net;
using System.Linq;
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
    public abstract class Entity
    {
        public string name { get; set; }
        public int strength { get; set; }
        public int agility { get; set; }
        public int intelligence { get; set; }
        public int speed { get; set; }
        public int maxHealth { get; set; }
        public int currentHealth { get; set; }
        public int maxMana { get; set; }
        public int currentMana { get; set; }
        public Effect buffs { get; set; }
        public IDictionary<string, Ability> abilities { get; set; }
        public D20 dice;

        public void applyEffect(Effect effect)
        {
            buffs.strength_mod *= effect.strength_mod;
            buffs.agility_mod *= effect.agility_mod;
            buffs.intelligence_mod *= effect.intelligence_mod;
            buffs.speed_mod *= effect.speed_mod;

            if (currentHealth + effect.health_restore >= maxHealth)
            {
                currentHealth = maxHealth;
            }
            else
            {
                currentHealth += (int)effect.health_restore;
            }

        }

        public void Attack(Ability move, Entity enemy)
        {
            uint damage = move.attack(this, enemy);
            if ((int)(this.currentHealth - damage) <= 0)
                enemy.currentHealth = 0;
            this.currentHealth -= (int)damage;
        }

        public abstract Ability getAbility();
    }

    public class Creep : Entity
    {

        public int expValue { get; set; }
        public ICollection<Item> lootTable { get; set; }
        public int moneyDrop {get;set;}
        public Uri icon { get; set; }

        private uint no_turns { get; set; }

        public static IDictionary<string, Creep> creepDictionary = new Dictionary<string, Creep>();

        public Creep(string _name, int _strength, int _agility, int _intelligence, int _speed, int _maxHealth, int _currentHealth,
                        int _maxMana, int _currentMana, IDictionary<string, Ability> _abilities, ICollection<Item> _lootTable,
                        int _moneyDrop, Uri _icon)
        {
            name = _name;
            strength = _strength;
            agility = _agility;
            intelligence = _intelligence;
            speed = _speed;
            maxHealth = _maxHealth;
            currentHealth = _currentHealth;
            maxMana = _maxMana;
            currentMana = _currentMana;
            icon = _icon;
            buffs = new Effect();
            abilities = new Dictionary<string, Ability>(_abilities);
            lootTable = new List<Item>(_lootTable);
            moneyDrop = _moneyDrop;
            dice = new D20();
        }

        public static void populateCreeps()
        {
            IDictionary<string, Ability> abilitySet = new Dictionary<string, Ability>();
            Ability a = Ability.fetchAbility("Attack");
            abilitySet.Add("Attack", a);
            ICollection<Item> lootSet = new List<Item>();
            lootSet.Add(ItemSet.retrieveItem(1));
            Creep c = new Creep("Troll", 30, 10, 0, 10, 300, 300, 50, 50, abilitySet, lootSet, 100, new Uri("troll"));
        }

        public override Ability getAbility()
        {
            this.no_turns++;

            if (this.no_turns < 4)
                return earlyMove();
            else
                return lateMove();
        }

        public Ability earlyMove()
        {
            Ability max_str_pen;
            Ability max_dex_pen;
            Ability max_int_pen;
            Ability max_spd_pen;
            Ability max_mana_pen;

            Random rnd = new Random();
            int move_choice = rnd.Next(1, 5);

            max_str_pen = max_dex_pen = max_int_pen = max_spd_pen = max_mana_pen = this.abilities.First().Value;

            foreach (var ab in this.abilities)
            {
                if ((ab.Value.abilityEffect.strength_mod < max_str_pen.abilityEffect.strength_mod)
                    && (ab.Value is AttackAbility)
                    && (ab.Value.manaCost <= this.currentMana))
                    max_str_pen = ab.Value;

                if ((ab.Value.abilityEffect.agility_mod < max_str_pen.abilityEffect.agility_mod)
                    && (ab.Value is AttackAbility)
                    && (ab.Value.manaCost <= this.currentMana))
                    max_dex_pen = ab.Value;

                if ((ab.Value.abilityEffect.intelligence_mod < max_str_pen.abilityEffect.intelligence_mod)
                    && (ab.Value is AttackAbility)
                    && (ab.Value.manaCost <= this.currentMana))
                    max_int_pen = ab.Value;

                if ((ab.Value.abilityEffect.speed_mod < max_str_pen.abilityEffect.speed_mod)
                    && (ab.Value is AttackAbility)
                    && (ab.Value.manaCost <= this.currentMana))
                    max_spd_pen = ab.Value;

                if ((ab.Value.abilityEffect.mana_restore < max_str_pen.abilityEffect.mana_restore)
                    && (ab.Value is AttackAbility)
                    && (ab.Value.manaCost <= this.currentMana))
                    max_mana_pen = ab.Value;
            }

            switch (move_choice)
            {
                case 1:
                    return max_str_pen;
                case 2:
                    return max_dex_pen;
                case 3:
                    return max_int_pen;
                case 4:
                    return max_spd_pen;
                default:
                    return max_mana_pen;

            }
        }

        public Ability lateMove()
        {
            Ability max_attack;
            Ability max_health; // Most health restored to NPC
            Ability min_health; // Most damage done to Player

            Random rnd = new Random();
            int move_choice = rnd.Next(1, 3);

            max_attack = max_health = min_health = this.abilities.First().Value;

            foreach (var ab in this.abilities)
            {
                if ((ab.Value is AttackAbility)
                    && (ab.Value.attackbonus < max_attack.attackbonus))
                    max_attack = ab.Value;

                if ((ab.Value is SelfAbility)
                    && (ab.Value.abilityEffect.health_restore > max_health.abilityEffect.health_restore))
                    max_health = ab.Value;

                if ((ab.Value is AttackAbility) 
                    && ab.Value.abilityEffect.health_restore < min_health.abilityEffect.health_restore)
                    min_health = ab.Value;
            }

            switch (move_choice)
            {
                case 1:
                    return max_attack;
                case 2:
                    return max_health;
                default:
                    return min_health;

            }
        }


    }

    public class Character : Entity
    {
        public const int BASEHEALTH = 0;
        public const int BASEMANA = 0;
        public const int INVENTORYSIZE = 20;
        public static Character currentCharacter { get; set; }

        public Class charClass { get; set; }
        public int level { get; set; }
        public int expToNext { get; set; }
        public int money { get; set; }

        public ClassType type { get; set; }
        public ICollection<ItemStack> inventory { get; set; }
        public Weapon weapon { get; set; }
        public Armour chest { get; set; }
        public Armour helm { get; set; }
        public Armour gloves { get; set; }
        public Armour boots { get; set; }
        public Armour legs { get; set; }

        public Character(string _name, Class _class, int _level, int _expToNext, int _maxHealth, int _currentHealth, 
                            int _maxMana, int _currentMana, int _money, int _strength, int _agility, int _intelligence,
                                int _speed, IDictionary<string, Ability> _abilities, ICollection<ItemStack> _inventory, Weapon _weapon,
                                Armour _chest, Armour _helm, Armour _gloves, Armour _boots, Armour _legs)
        {
            name = _name;
            charClass = _class;
            level = _level;
            expToNext = _expToNext;
            maxHealth = _maxHealth;
            currentHealth = _currentHealth;
            maxMana = _maxMana;
            currentMana = _currentMana;
            money = _money;
            strength = _strength;
            agility = _agility;
            intelligence = _intelligence;
            speed = _speed;
            type = _class.type;
            abilities = new Dictionary<string, Ability>(_abilities);
            inventory = new List<ItemStack>(_inventory);
            weapon = _weapon;
            chest = _chest;
            helm = _helm;
            gloves = _gloves;
            boots = _boots;
            legs = _legs;
            dice = new D20();
            buffs = new Effect();
        }

        public static Character createNewCharacter(string _name, Class _class, IDictionary<string, Ability> _abilities)
        {
            StatModifier mod = _class.initialMod;
            int _maxHealth = calculateMaxHealth(mod.strength);
            int _maxMana = calculateMaxMana(mod.intelligence);
            return new Character(_name, _class, 1, calculateExpToNextLevel(1), _maxHealth, _maxHealth, _maxMana, 
                                    _maxMana, 100, mod.strength, mod.agility, mod.intelligence, mod.speed, _abilities, initialItems(), 
                                    new Weapon(_class.startingWeapon.id, _class.startingWeapon, 1),
                                    new Armour(_class.startingChest.id, _class.startingChest, 1),
                                    new Armour(_class.startingHelm.id, _class.startingHelm, 1),
                                    new Armour (_class.startingGloves.id, _class.startingGloves, 1),
                                    new Armour (_class.startingBoots.id, _class.startingBoots, 1),
                                    new Armour (_class.startingLegs.id, _class.startingLegs, 1));
        }

        private static ICollection<ItemStack> initialItems()
        {
            ICollection<ItemStack> startingInventory = new List<ItemStack>((int)ItemStack.MAXSTACKSIZE);
            Consumable minorHealthPot = (Consumable) ItemSet.retrieveItem(1);
            Item i = ItemSet.retrieveItem(100);
            Weapon w = new Weapon(i.id, i as Weapon, 1);
            startingInventory.Add(new ItemStack(minorHealthPot, 3));
            startingInventory.Add(new ItemStack(w, 1));
            return startingInventory;
        }

        public void submitCharacter(UploadStringCompletedEventHandler characterUploaded)
        {
            string charFormatString = String.Format("name={0}&classid={1}&lvl=1&exptonext={2}&maxhealth={3}&currenthealth={3}&" +
                                                    "maxmana={4}&currentmana={4}&money={5}&strength={6}&agility={7}&intelligence={8}&" +
                                                    "speed={9}",
                                                    name, (int)type, expToNext, maxHealth,
                                                    maxMana, money, strength, agility, intelligence, speed);
            charFormatString += formatAbilities(abilities) + formatItems(inventory) + formatWeapon() + formatChest() + formatHelm()
                                + formatGloves() + formatBoots() + formatLegs();
            MessageBox.Show(charFormatString);
            HttpConnection.httpPost(new Uri("characterCreate.php", UriKind.Relative), charFormatString, characterUploaded);
        }

        private string formatAbilities(IDictionary<string, Ability> abilityList)
        {
            Ability[] abilityArray = abilityList.Values.ToArray();
            string formatString = "";
            for (int i = 0; i < abilityArray.LongCount(); i++)
            {
                formatString += "&ability" + i + "=" + abilityArray[i].name;
            }
            return formatString;
        }

        private string formatItems(ICollection<ItemStack> inventory)
        {
            ItemStack[] itemArray = inventory.ToArray();
            string formatString = "";
            for (int i = 0; i < itemArray.LongCount(); i++)
            {
                formatString += "&itemid" + i + "=" + itemArray[i].item.id + "&itemcount" + i + "=" + itemArray[i].stackSize;
                if (itemArray[i].item is Armour)
                {
                    Armour a = itemArray[i].item as Armour;
                    formatString += "&itemlevel" + i + "=" + a.level;
                }
                else if (itemArray[i].item is Weapon)
                {
                    Weapon w = itemArray[i].item as Weapon;
                    formatString += "&itemlevel" + i + "=" + w.level;
                }
            }
            return formatString;
        }

        private string formatWeapon()
        {
            return "&weaponid=" + weapon.id + "&weaponlevel=" + weapon.level;
        }

        private string formatChest()
        {
            return "&chestid=" + chest.id + "&chestlevel=" + chest.level; 
        }

        private string formatHelm()
        {
            return "&helmid=" + helm.id + "&helmlevel=" + helm.level;
        }

        private string formatGloves()
        {
            return "&glovesid=" + gloves.id + "&gloveslevel=" + gloves.level;
        }

        private string formatBoots()
        {
            return "&bootsid=" + boots.id + "&bootslevel=" + boots.level;
        }

        private string formatLegs()
        {
            return "&legsid=" + legs.id + "&legslevel=" + legs.level;
        }

        public void calculateStats()
        {
            int baseStrength = charClass.initialMod.strength + level * charClass.levelMod.strength;
            int baseInt = charClass.initialMod.intelligence + level * charClass.levelMod.intelligence;
            int baseAgi = charClass.initialMod.agility + level * charClass.levelMod.agility;
            int baseSpeed = charClass.initialMod.speed + level * charClass.levelMod.speed;

            int itemStrength = 0;
            int itemAgility = 0;
            int itemIntelligence = 0;
            int itemSpeed = 0;
            int itemHealth = 0;
            int itemMana = 0;
            if (weapon != null)
            {
                itemStrength += weapon.effect.strengthMod;
                itemAgility += weapon.effect.agilityMod;
                itemIntelligence += weapon.effect.intelligenceMod;
                itemSpeed += weapon.effect.speedMod;
                itemHealth += weapon.effect.healthMod;
                itemMana += weapon.effect.manaMod;
            }
            if (chest != null)
            {
                itemStrength += chest.stats.strengthMod;
                itemAgility += chest.stats.agilityMod;
                itemIntelligence += chest.stats.intelligenceMod;
                itemSpeed += chest.stats.speedMod;
                itemHealth += chest.stats.healthMod;
                itemMana += chest.stats.manaMod;
            }
            if (helm != null)
            {
                itemStrength += helm.stats.strengthMod;
                itemAgility += helm.stats.agilityMod;
                itemIntelligence += helm.stats.intelligenceMod;
                itemSpeed += helm.stats.speedMod;
                itemHealth += helm.stats.healthMod;
                itemMana += helm.stats.manaMod;
            }
            if (gloves != null)
            {
                itemStrength += gloves.stats.strengthMod;
                itemAgility += gloves.stats.agilityMod;
                itemIntelligence += gloves.stats.intelligenceMod;
                itemSpeed += gloves.stats.speedMod;
                itemHealth += gloves.stats.healthMod;
                itemMana += gloves.stats.manaMod;
            }
            if (boots != null)
            {
                itemStrength += boots.stats.strengthMod;
                itemAgility += boots.stats.agilityMod;
                itemIntelligence += boots.stats.intelligenceMod;
                itemSpeed += boots.stats.speedMod;
                itemHealth += boots.stats.healthMod;
                itemMana += boots.stats.manaMod;
            }
            if (legs != null)
            {
                itemStrength += legs.stats.strengthMod;
                itemAgility += legs.stats.agilityMod;
                itemIntelligence += legs.stats.intelligenceMod;
                itemSpeed += legs.stats.speedMod;
                itemHealth += legs.stats.healthMod;
                itemMana += legs.stats.manaMod;
            }

            strength = baseStrength + itemStrength;
            agility = baseAgi + itemAgility;
            intelligence = baseInt + itemIntelligence;
            speed = baseSpeed + itemSpeed;

            maxHealth = calculateMaxHealth(strength) + itemHealth;
            currentHealth = maxHealth;
            maxMana = calculateMaxMana(intelligence) + itemMana;
            currentMana = maxMana;
        }

        public void levelUp()
        {
            level++;
            calculateStats();
            currentHealth = maxHealth;
            currentMana = maxMana;
            expToNext = calculateExpToNextLevel(level);
        }

        public void resetStats()
        {
            buffs =  new Effect();
        }

        public override Ability getAbility()
        {
            throw new NotImplementedException();
        }

        public static int calculateMaxHealth(int _strength)
        {
            return BASEHEALTH + 20 * _strength;        
        }

        public static int calculateMaxMana(int _intelligence)
        {
            return BASEMANA + 30 * _intelligence;
        }

        public static int calculateExpToNextLevel(int level)
        {
            return level * 100;
        }

    }
}
