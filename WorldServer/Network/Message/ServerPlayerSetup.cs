using System;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices.JavaScript;
using Shared.Database.Datacentre;
using Shared.Network;

namespace WorldServer.Network.Message;

[SubPacket(SubPacketServerHandlerId.ServerPlayerSetup)]
public class ServerPlayerSetup : SubPacket
{
    public CharacterInfo Character;
    // TODO: finish mapping this, it needs a lot of work
    public override void Write(BinaryWriter writer)
    {
        writer.Write(Character.Id);
        writer.Pad(8u);
        writer.Pad(4u);
        writer.Pad(4u);
        writer.Write(Character.ActorId);
        writer.Write(0u); // rested XP
        writer.Write(0u);
        writer.Write(0u);
        writer.Write(0u);
        writer.Write((ushort)0);
        writer.Write((ushort)0);
        writer.Write(0u);
        writer.Write(0u);
        writer.Write(0u);
        writer.Write(0u);
        writer.Write(0u);
        writer.Write(0u);
        writer.Write(0f);
        writer.Write(0u);
        writer.Write((ushort)0);
        writer.Pad(8u);
        writer.Write((ushort)0);
        writer.Write((ushort)0);
        writer.Write((ushort)0);
        writer.Write((ushort)0);
        writer.Write((ushort)0);
        writer.Write((ushort)69); // player commendations
        writer.Pad(16u);
        writer.Write((ushort)0);
        writer.Write((ushort)0);
        writer.Write((ushort)0);
        writer.Write((ushort)0);
        writer.Write((byte)100); // max level
        writer.Write((byte)5); // expansion
        writer.Write((byte)0);
        writer.Write((byte)0);
        writer.Write((byte)0);
        writer.Write((byte)Character.Appearance.Race);
        writer.Write(Character.Appearance.Clan);
        writer.Write((byte)Character.Appearance.Sex);
        writer.Write(Character.ClassJobId);
        writer.Write(Character.ClassId);
        writer.Write(Character.Guardian);
        writer.Write(Character.BirthMonth);
        writer.Write(Character.BirthDay);
        writer.Write(Character.StartTown); // city state
        writer.Write(Character.Homepoint); // return Aetheryte
        writer.Write((byte)0);
        writer.Write((byte)0);
        // companion related
        writer.Write((byte)0); // shows companion action bar
        writer.Write((byte)0); // companion rank
        writer.Write((byte)0); // companion stars
        writer.Write((byte)0); // companion available skill points
        writer.Write((byte)0); // companion unknown
        writer.Write(this.Character.CompanionInfo.Color); // companion color (retail seems to use ActorAction 0x112 instead?)
        writer.Write((byte)0); // companion favorite feed
        writer.Write((byte)3); // favored destination count
        writer.Write((byte)0);
        writer.Write((byte)0);
        writer.Write((byte)0);
        writer.Write((byte)0);
        writer.Write((byte)0);
        writer.Write((byte)0);
        writer.Write((byte)0);
        writer.Pad(2u);
        writer.Write((byte)0);
        writer.Pad(9u);
        writer.Pad(3u);
        writer.Write((byte)0);
        writer.Write((byte)0);
        writer.Write((byte)0);
        writer.Write((byte)0);
        writer.Pad(7u);
        for (int i = 0; i < CharacterClassInfo.MaxClassId; i++)
            writer.Write(Character.Classes[i].Experience);
        writer.Write(0u);
        writer.Write(0u);
        writer.Write(0u);
        writer.Write(0u);
        writer.Write(0u);
        writer.Write(0u);
        writer.Write(0u);
        for (int i = 0; i < CharacterClassInfo.MaxClassId; i++)
            writer.Write(Character.Classes[i].Level);
        writer.Pad(218);
        if (Version.Version.StartsWith("7"))
        {
            var dawntrailMask = new BitArray(44 * 8, true);
            writer.Write(dawntrailMask.ToArray());
        }
        writer.WriteStringLength(this.Character.CompanionInfo.Name, 21);
        writer.Write(this.Character.CompanionInfo.DefenseRank); // companion defender level
        writer.Write(this.Character.CompanionInfo.AttackRank); // companion attacker level
        writer.Write(this.Character.CompanionInfo.HealerRank); // companion healer level
        
        writer.Write(this.Character.Progression.Mount.ToArray(Version.DataSize["Mount"]));

        var ornamentMask = new BitArray(Version.DataSize["Ornament"] * 8, false);
        writer.Write(ornamentMask.ToArray());

        if (Version.Version.StartsWith("7"))
        {
            var glassesMask = new BitArray(2 * 8, true);
            writer.Write(glassesMask.ToArray(Version.DataSize["Glasses"]));
        }
        
        if (Version.Version.StartsWith("7"))
        {
            writer.Pad(6);
        }
        
        var testMask = new BitArray(17 * 8, false);
        writer.Write(testMask.ToArray());
        writer.WriteStringLength(Character.Name, 32);
        writer.Pad(16u);
        writer.Pad(16u);

        // enables various features (emotes) and UI elements
        writer.Write(this.Character.Progression.MasterMask.ToArray());

        writer.Write(this.Character.Progression.Aetheryte.ToArray());

        writer.Pad(8u);
        writer.Write((ushort)0);
        writer.Write((ushort)0);

        // territory discovery mask
        this.Character.Progression.Discovery.SetAll(true);
        writer.Write(this.Character.Progression.Discovery.ToArray(Version.DataSize["Discovery"]));

        /*
         var territoryDiscovery2 = new BitArray(160 * 8, true);
        writer.Write(territoryDiscovery2.ToArray());
        */

        writer.Write(this.Character.Progression.HowTo.ToArray(Version.DataSize["HowTo"]));

        writer.Pad(4u);
        writer.Write(this.Character.Progression.Minion.ToArray(Version.DataSize["Minion"]));

        var chocoboPorterMask = new BitArray(12 * 8, true);
        writer.Write(chocoboPorterMask.ToArray());

        writer.Write(this.Character.Progression.Cutscene.ToArray());

        var companionBardingMask = new BitArray(12 * 8, true);
        writer.Write(companionBardingMask.ToArray());

        var unknownMask3 = new BitArray(23 * 8, false);
        writer.Write(unknownMask3.ToArray());

        // companion related
        writer.Write((byte)0); // companion gear head
        writer.Write((byte)0); // companion gear body
        writer.Write((byte)0); // companion gear feet


        var fishingLogCatchMask = new BitArray(161 * 8, true);
        writer.Write(fishingLogCatchMask.ToArray());

        var fishingLogLocationMask = new BitArray(38 * 8, true);
        writer.Write(fishingLogLocationMask.ToArray());

        writer.Pad(34u);
        writer.Pad(7u);

        // PvP related
        writer.Pad(3u);

        writer.Pad(17u);
        writer.Write((byte)0);
        writer.Write((byte)0);
        writer.Write((byte)0);
        writer.Write((byte)0);
        writer.Write((byte)0);
        writer.Write(Character.Pose);
        writer.Write(!Character.IsNewAdventurer); // new adventurer off
        writer.Write((byte)0);
        writer.Write((byte)0);
        writer.Write((byte)0);
        writer.Write((byte)0);
        writer.Write((byte)0);
        writer.Write((byte)0);
        writer.Write((byte)0);


        writer.Write((byte)0); // 056C
        writer.Write((byte)0); // 056D
        writer.Write((byte)0); // 056E

        // beast tribe
        writer.Write((byte)0); // 056F
        writer.Write((byte)0); // 0570
        writer.Write((byte)0); // 0571
        writer.Write((byte)0); // 0572
        writer.Write((byte)0); // 0573
        writer.Write((byte)0); // 0574
        writer.Write((byte)0); // 0575
        writer.Write((byte)0); // 0576
        writer.Write((byte)0); // 0577
        writer.Write((ushort)0); // 0578
        writer.Write((ushort)0); // 057A
        writer.Write((ushort)0); // 057C
        writer.Write((ushort)0); // 057E
        writer.Write((ushort)0); // 0580
        writer.Write((ushort)0); // 0582
        writer.Write((ushort)0); // 0584
        writer.Write((ushort)0); // 0586
        writer.Write((ushort)0); // 0588

        writer.Write((byte)0); // 058A
        writer.Write((byte)0); // 058B
        writer.Write((byte)0); // 058C
        writer.Write((byte)0); // 058D
        writer.Write((byte)0); // 058E
        writer.Write((byte)0); // 058F
        writer.Write((byte)0); // 0590
        writer.Write((byte)0); // 0591
        writer.Write((byte)0); // 0592
        writer.Write((byte)0); // 0593
        writer.Write((ushort)0); // 0594
        writer.Write((ushort)0); // 0596
        writer.Write((ushort)0); // 0598
        writer.Write((ushort)0); // 059A
        writer.Write((ushort)0); // 059C

        var someMask3 = new BitArray(5 * 8, true);
        writer.Write(someMask3.ToArray());

        writer.Write((byte)0); // 05A3
        writer.Write((byte)0); // 05A4

        writer.Write(0u); // 05A5
        writer.Write((ushort)0); // 05A9
        writer.Write((byte)0); // 05AB

        writer.Write((byte)0); // 05AC
        writer.Write((byte)0); // 05AD
        writer.Write((byte)0); // 05AE
        writer.Write((byte)0); // 05AF
        writer.Write((byte)0); // 05B0
        writer.Write((byte)0); // 05B1
        writer.Write((byte)0); // 05B2

        var someMask4 = new BitArray(28 * 8, true);
        writer.Write(someMask4.ToArray());

        writer.Pad(1u); // gap05D9
        writer.Write(0u); // 05D0

        // related, all read in same client function
        writer.Write((byte)0); // 05D4
        writer.Write((byte)0); // 05D5
        writer.Write((byte)0); // 05D6
        writer.Write((byte)0); // 05D7
        writer.Write((byte)0); // 05D8
        writer.Write((byte)0); // 05D9
        writer.Write((byte)0); // 05DA
        writer.Write((byte)0); // 05DB
        writer.Write((byte)0); // 05DC
        writer.Write((byte)0); // 05DD
        writer.Write((byte)0); // 05DE
        writer.Write((byte)0); // 05DF

        var someMask5 = new BitArray(26 * 8, true);
        writer.Write(someMask5.ToArray());

        writer.Write(0u); // Frontline 1st place
        writer.Write(0u); // Frontline 2nd place
        writer.Write(0u); // Frontline 3rd place
        writer.Write((ushort)0); // Frontline 1st place weekly
        writer.Write((ushort)0); // Frontline 2nd place weekly
        writer.Write((ushort)0); // Frontline 3rd place weekly
        writer.Pad(2u);

        // not a mask, 55 seperate bytes
        for (int i = 0; i < 55; i++)
            writer.Write((byte)0);

        var tripleTriadCardMask = new BitArray(27 * 8, true);
        writer.Write(tripleTriadCardMask.ToArray());

        writer.Write((byte)0); // 0660
        writer.Write((byte)0); // 0661
        writer.Write((byte)0); // 0662
        writer.Write((byte)0); // 0663
        writer.Write((byte)0); // 0664
        writer.Write((byte)0); // 0665
        writer.Write((byte)0); // 0666
        writer.Write((byte)0); // 0667
        writer.Write((byte)0); // 0668
        writer.Write((byte)0); // 0669
        writer.Write((byte)0); // 066A

        var someMask6 = new BitArray(22 * 8, true);
        writer.Write(someMask6.ToArray());

        writer.Write((byte)0); // 0681
        writer.Write((byte)0); // 0682
        writer.Write((byte)0); // 0683

        var orchestrionMask = new BitArray(40 * 8, true);
        writer.Write(orchestrionMask.ToArray());

        writer.Write((byte)0); // 06AC
        writer.Write((byte)0); // 06AD
        writer.Write((byte)0); // 06AE

        // shows certain key items in bag
        var keyItemMask = new BitArray(11 * 8, false);
        writer.Write(keyItemMask.ToArray());

        var someMask7 = new BitArray(16 * 8, true);
        writer.Write(someMask7.ToArray());

        writer.Write((byte)0); // 06CA
        writer.Write((byte)0); // 06CB
        writer.Write((byte)0); // 06CC
        writer.Write((byte)0); // 06CD
        writer.Pad(8u);
        writer.Write((byte)0); // 06D6
        
        var someMask9 = new BitArray(28 * 8, true);
        writer.Write(someMask9.ToArray());

        var someMask10 = new BitArray(18 * 8, true);
        writer.Write(someMask10.ToArray());

        writer.Write((byte)0); // 0705
        writer.Write((byte)0); // 0706
        writer.Write((byte)0); // 0707
        writer.Write((byte)0); // 0708
        writer.Write((byte)0); // 0709
        writer.Write((byte)0); // 070A
        writer.Write((byte)0); // 070B
        writer.Write((byte)0); // 070C
        writer.Write((byte)0); // 070D
        writer.Write((byte)0); // 070E
        writer.Write((byte)0); // 070F
        writer.Write((byte)0); // 0710
        writer.Write((byte)0); // 0711
        writer.Write((byte)0); // 0712
        writer.Write((byte)0); // 0713
        writer.Write((byte)0); // 0714
        writer.Write((byte)0); // 0715
        writer.Write((byte)0); // 0716
        writer.Write((byte)0); // 0717
        writer.Write((byte)0); // 0718
        writer.Write((byte)0); // 0719
        writer.Write((byte)0); // 071A


        var someMask12 = new BitArray(18 * 8, false);
        writer.Write(someMask12.ToArray());

        var someMask11 = new BitArray(28 * 8, false);
        writer.Write(someMask11.ToArray());

        var someMask13 = new BitArray(18 * 8, false);
        writer.Write(someMask12.ToArray());

        writer.Write((byte)0); // 0749
        writer.Write((byte)0); // 074A
        writer.Write((byte)0); // 074B
        writer.Write((byte)0); // 074C
        writer.Write((byte)0); // 074D
        writer.Write((byte)0); // 074E
        writer.Write((byte)0); // 074F
        writer.Write((byte)0); // 0750
        writer.Write((byte)0); // 0751
        writer.Write((byte)0); // 0752
        writer.Write((byte)0); // 0753
        writer.Write((byte)0); // 0754
        writer.Write((byte)0); // 0755
        writer.Write((byte)0); // 0756
        writer.Write((byte)0); // 0757
        writer.Write((byte)0); // 0758
        writer.Write((byte)0); // 0759
        writer.Write((byte)0); // 075A
        writer.Write((byte)0); // 075B
        writer.Write((byte)0); // 075C
        writer.Write((byte)0); // 075D
        writer.Write((byte)0); // 075E
    }
}