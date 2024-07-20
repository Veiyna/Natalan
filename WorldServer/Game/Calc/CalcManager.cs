using System;
using WorldServer.Game.Action.Enums;
using WorldServer.Game.Entity;
using WorldServer.Game.Entity.Enums;

namespace WorldServer.Game.Calc
{
    public static class CalcManager
    {
        public static uint[,] levelTable = { 
              // MAIN,SUB,DIV,HP,ELMT,THREAT
              { 1, 1, 1, 1, 1, 1 },
              { 20, 56, 56, 86, 52, 2 },
              { 21, 57, 57, 101, 54, 2 },
              { 22, 60, 60, 109, 56, 3 },
              { 24, 62, 62, 116, 58, 3 },
              { 26, 65, 65, 123, 60, 3 },
              { 27, 68, 68, 131, 62, 3 },
              { 29, 70, 70, 138, 64, 4 },
              { 31, 73, 73, 145, 66, 4 },
              { 33, 76, 76, 153, 68, 4 },
              { 35, 78, 78, 160, 70, 5 },
              { 36, 82, 82, 174, 73, 5 },
              { 38, 85, 85, 188, 75, 5 },
              { 41, 89, 89, 202, 78, 6 },
              { 44, 93, 93, 216, 81, 6 },
              { 46, 96, 96, 230, 84, 7 },
              { 49, 100, 100, 244, 86, 7 },
              { 52, 104, 104, 258, 89, 8 },
              { 54, 109, 109, 272, 93, 9 },
              { 57, 113, 113, 286, 95, 9 },
              { 60, 116, 116, 300, 98, 10 },
              { 63, 122, 122, 333, 102, 10 },
              { 67, 127, 127, 366, 105, 11 },
              { 71, 133, 133, 399, 109, 12 },
              { 74, 138, 138, 432, 113, 13 },
              { 78, 144, 144, 465, 117, 14 },
              { 81, 150, 150, 498, 121, 15 },
              { 85, 155, 155, 531, 125, 16 },
              { 89, 162, 162, 564, 129, 17 },
              { 92, 168, 168, 597, 133, 18 },
              { 97, 173, 173, 630, 137, 19 },
              { 101, 181, 181, 669, 143, 20 },
              { 106, 188, 188, 708, 148, 22 },
              { 110, 194, 194, 747, 153, 23 },
              { 115, 202, 202, 786, 159, 25 },
              { 119, 209, 209, 825, 165, 27 },
              { 124, 215, 215, 864, 170, 29 },
              { 128, 223, 223, 903, 176, 31 },
              { 134, 229, 229, 942, 181, 33 },
              { 139, 236, 236, 981, 186, 35 },
              { 144, 244, 244, 1020, 192, 38 },
              { 150, 253, 253, 1088, 200, 40 },
              { 155, 263, 263, 1156, 207, 43 },
              { 161, 272, 272, 1224, 215, 46 },
              { 166, 283, 283, 1292, 223, 49 },
              { 171, 292, 292, 1360, 231, 52 },
              { 177, 302, 302, 1428, 238, 55 },
              { 183, 311, 311, 1496, 246, 58 },
              { 189, 322, 322, 1564, 254, 62 },
              { 196, 331, 331, 1632, 261, 66 },
              { 202, 341, 341, 1700, 269, 70 },
              { 204, 342, 393, 1774, 270, 84 },
              { 205, 344, 444, 1851, 271, 99 },
              { 207, 345, 496, 1931, 273, 113 },
              { 209, 346, 548, 2015, 274, 128 },
              { 210, 347, 600, 2102, 275, 142 },
              { 212, 349, 651, 2194, 276, 157 },
              { 214, 350, 703, 2289, 278, 171 },
              { 215, 351, 755, 2388, 279, 186 },
              { 217, 352, 806, 2492, 280, 200 },
              { 218, 354, 858, 2600, 282, 215 },
              { 224, 355, 941, 2700, 283, 232 },
              { 228, 356, 1032, 2800, 284, 250 },
              { 236, 357, 1133, 2900, 286, 269 },
              { 244, 358, 1243, 3000, 287, 290 },
              { 252, 359, 1364, 3100, 288, 313 },
              { 260, 360, 1497, 3200, 290, 337 },
              { 268, 361, 1643, 3300, 292, 363 },
              { 276, 362, 1802, 3400, 293, 392 },
              { 284, 363, 1978, 3500, 294, 422 },
              { 292, 364, 2170, 3600, 295, 455 },

              // todo: add proper shbr values - hp/elmt/threat
              // sub/div added from http://theoryjerks.akhmorning.com/resources/levelmods/
              { 296, 365, 2263, 3600, 295, 466 },
              { 300, 366, 2360, 3600, 295, 466 },
              { 305, 367, 2461, 3600, 295, 466 },
              { 310, 368, 2566, 3600, 295, 466 },
              { 315, 370, 2676, 3600, 295, 466 },
              { 320, 372, 2790, 3600, 295, 466 },
              { 325, 374, 2910, 3600, 295, 466 },
              { 330, 376, 3034, 3600, 295, 466 },
              { 335, 378, 3164, 3600, 295, 466 },
              { 340, 380, 3300, 3600, 569, 569 },
              // Endwalker
              { 345, 382, 3300, 3600, 569, 569 },
              { 350, 384, 3300, 3600, 569, 569 },
              { 355, 386, 3300, 3600, 569, 569 },
              { 360, 388, 3300, 3600, 569, 569 },
              { 365, 390, 3300, 3600, 569, 569 },
              { 370, 392, 3300, 3600, 569, 569 },
              { 375, 394, 3300, 3600, 569, 569 },
              { 380, 396, 3300, 3600, 569, 569 },
              { 385, 398, 3300, 3600, 569, 569 },
              { 390, 400, 3300, 3600, 569, 569 },
              //DAWNTRAIL
              { 345, 382, 3300, 3600, 569, 569 },
              { 350, 384, 3300, 3600, 569, 569 },
              { 355, 386, 3300, 3600, 569, 569 },
              { 360, 388, 3300, 3600, 569, 569 },
              { 365, 390, 3300, 3600, 569, 569 },
              { 370, 392, 3300, 3600, 569, 569 },
              { 375, 394, 3300, 3600, 569, 569 },
              { 380, 396, 3300, 3600, 569, 569 },
              { 385, 398, 3300, 3600, 569, 569 },
              { 390, 400, 3300, 3600, 569, 569 },
            };

        public static (float, ActionHitSeverityType) ActionDamage(Action.Action action, Character character, uint potency)
        {
            ActionHitSeverityType hittype = ActionHitSeverityType.Normal;
            var factor = BaseDamageForPotency(character, potency);
            Random rand = new Random();
            var critProb = CriticalHitProbability(character);
            if (critProb > rand.Next(0, 100))
            {
                hittype = ActionHitSeverityType.Crit;
                factor *= CriticalHitBonus(character);
            }
                
            
            factor *= 1f + (rand.Next(0, 100) - 50f) / 1000f;

            return (factor, hittype);
        }
        public static (float, ActionHitSeverityType) ActionHeal(Action.Action action, Character character, uint potency)
        {
            ActionHitSeverityType hittype = ActionHitSeverityType.Normal;
            var factor = BaseHealForPotency(character, potency);
            
            Random rand = new Random();
            
            var critProb = CriticalHitProbability(character);
            if (critProb > rand.Next(0, 100))
            {
                hittype = ActionHitSeverityType.Crit;
                factor *= CriticalHitBonus(character); 
            }
                
            
            
            factor *= 1f + (rand.Next(0, 100) - 50f) / 1000f;

            return (factor, hittype);
        }

        public static uint SimplePotencyToValue(uint potency)
        {
            Random rand = new Random();
            float value = potency;
            value *= 1f + (rand.Next(0, 100) - 50f) / 1000f;
            return (uint)value;
        }
        private static float BaseDamageForPotency(Character character, uint potency)
        {
            var potency2 = potency / 100f;
            var weapondmg = WeaponDamage(character);
            var attackpower = attackPower(character);
            var det = determination(character);
            var tenacity = 1f;

            var factor = (float)Math.Floor(potency2 * weapondmg * attackpower * det * tenacity);

            return factor;
        }
        
        private static float BaseHealForPotency(Character character, uint potency)
        {
            var potency2 = potency / 100f;
            var weapondmg = WeaponDamage(character);
            var attackpower = attackPower(character);
            var det = determination(character);
            var tenacity = 1f;

            var factor = (float)Math.Floor(potency2 * weapondmg * attackpower * det * tenacity);

            return factor;
        }


        private static float WeaponDamage(Character character)
        {
            var weapondmg = 0;

            if (character.IsPlayer)
            {
                var player = character.ToPlayer;
                var weapon = player.Inventory.GetItemAtSlot(ContainerType.Equipped, ContainerEquippedSlot.MainHand);
                if (weapon is not null)
                {
                    weapondmg = weapon.Entry.DamagePhys + weapon.Entry.DamageMag;
                }
            }

            return weapondmg;

        }
        
        private static float attackPower(Character character )
        {
            return calcAttackPower( character, character.GetBaseStat( BaseParam.AttackPower ) );
        }

        private static float magicattackPower(Character character)
        {
            return calcAttackPower( character, character.GetBaseStat( BaseParam.AttackMagicPotency ) );
        }

        private static float healingattackPower(Character character)
        {
            return calcAttackPower( character, character.GetBaseStat( BaseParam.HealingMagicPotency ) );
        }
        
        private static float determination(Character character)
        {
            var level = 0;
            if (character.IsPlayer) level = character.ToPlayer.Character.Class.Level;
            var main = (float)levelTable[level, (uint)LevelTableEntry.MAIN];
            var div = (float)levelTable[level, (uint)LevelTableEntry.DIV];
            
            return (float)Math.Floor(130f * (character.GetBaseStat(BaseParam.Determination) - main) /div + 1000f)/ 1000f ;
        }


        private static float calcAttackPower(Character character, uint attackPower)
        {
            var level = 0;
            if (character.IsPlayer) level = character.ToPlayer.Character.Class.Level;
            var main = (float)levelTable[level, (uint)LevelTableEntry.MAIN];
            var div = (float)levelTable[level, (uint)LevelTableEntry.DIV];
            
            return (float)Math.Floor(125f * (attackPower - main) /div + 100f)/ 100f ;
        }

        private static float CriticalHitProbability(Character character)
        {
            var level = 0;
            if (character.IsPlayer) level = character.ToPlayer.Character.Class.Level;

            var critrate = character.GetBaseStat(BaseParam.CriticalHit);
            var div = (float)levelTable[level, (uint)LevelTableEntry.DIV];
            var sub = (float)levelTable[level, (uint)LevelTableEntry.SUB];
            
            return (float)Math.Floor(200f * (critrate - sub) /div + 50f)/ 10f ;
        }
        
        private static float CriticalHitBonus(Character character)
        {
            var level = 0;
            if (character.IsPlayer) level = character.ToPlayer.Character.Class.Level;

            var critrate = character.GetBaseStat(BaseParam.CriticalHit);
            var div = (float)levelTable[level, (uint)LevelTableEntry.DIV];
            var sub = (float)levelTable[level, (uint)LevelTableEntry.SUB];
            
            return (float)Math.Floor(200f * (critrate - sub) /div + 1400f)/ 1000f ;
        }
        

    }
}