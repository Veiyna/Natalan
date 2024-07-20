using System;
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity;
using WorldServer.Game.Event;
using WorldServer.Game.Map;

namespace WorldServer.Script.InstanceContent;

[InstanceContentScript(4)]
public class Sastasha : InstanceContentScript
{
    enum Corals
    {
        Blue = 2000213,
        Red = 2000214,
        Green = 2000215
    };

    enum Sequence : byte
    {
        Seq1 = 1,
        Seq2 = 3,
        Seq3 = 7,
        Seq4 = 23,
        Seq5 = 31,
        SeqFinish = 255
    };

    private BNpc madison;
    private BNpc denn;
    private BNpc madison2;
    private BNpc chopper;
    public override void OnInit()
    {
        InstanceContent.RegisterEObj("unknown_0", 2000211, 0, 3280858, 4, new Vector3(367.827087f, 47.400051f, -226.694305f), 4.714991f, 0.000432f, 0);
        InstanceContent.RegisterEObj("sgvf_w_lvd_b0250", 2001504, 4323996, 4036038, 1, new Vector3(94.597588f, 26.865030f, -68.584061f), 1.000000f,
            0.000000f, 7);

        // States -> vf_bextwall_on (id: 3) vf_bextwall_of (id: 4)
        InstanceContent.RegisterEObj("sgvf_w_lvd_b0249", 2001505, 4323997, 4036039, 4, new Vector3(95.510597f, 26.620729f, -67.853653f), 1.000000f,
            0.000000f, 0);

        // States -> vf_line_on (id: 10) vf_line_of (id: 11)
        InstanceContent.RegisterEObj("unknown_1", 2001506, 3653862, 4056797, 1, new Vector3(-9.239832f, 24.789940f, 35.778252f), 0.991760f, 0.000048f, 7);
        InstanceContent.RegisterEObj("sgvf_w_lvd_b0094", 2001507, 4035750, 4056798, 4, new Vector3(-2.841087f, 23.114571f, 38.090420f), 0.991760f,
            0.000048f, 0);

        // States -> vf_line_on (id: 12) vf_line_of (id: 13)
        InstanceContent.RegisterEObj("unknown_2", 2001539, 3653864, 4036041, 1, new Vector3(-158.560898f, 8.099012f, 214.344803f), 0.991760f, 0.000048f,
            7);

        InstanceContent.RegisterEObj("sgvf_w_lvd_b0094_1", 2001540, 4056793, 4036043, 4, new Vector3(-163.598602f, 8.026373f, 214.030106f), 0.991760f,
            0.000048f, 0);

        // States -> vf_line_on (id: 12) vf_line_of (id: 13)
        InstanceContent.RegisterEObj("sgpl_s1d1_bosswall", 2001508, 4236989, 4036045, 1, new Vector3(-303.983612f, 5.576412f, 276.214111f), 1.000000f,
            0.000000f, 7);

        InstanceContent.RegisterEObj("sgpl_s1d1_bossline", 2001509, 4236990, 4036046, 4, new Vector3(-305.302002f, 5.542851f, 275.750885f), 1.000000f,
            0.000000f, 0);

        InstanceContent.RegisterEObj("Entrance", 2000182, 4096706, 4096707, 5, new Vector3(361.881714f, 46.092751f, -225.181305f), 1.000000f, 0.000000f,
            0);

        // States -> vf_lock_on (id: 11) vf_lock_of (id: 12)
        InstanceContent.RegisterEObj("Shortcut", 2000700, 0, 4033735, 1, new Vector3(344.705688f, 43.781551f, -217.365997f), 0.991760f, 0.000048f, 7);
        InstanceContent.RegisterEObj("Exit", 2000139, 0, 3281180, 4, new Vector3(-314.279114f, 5.630589f, 348.735596f), 0.900235f, 0.000336f, 0);
        InstanceContent.RegisterEObj("Bluecoralformation", 2000213, 3668215, 3280868, 4, new Vector3(75.869797f, 35.101421f, -32.537209f), 0.930753f,
            0.000240f, 0);

        InstanceContent.RegisterEObj("Redcoralformation", 2000214, 3668214, 3280879, 4, new Vector3(88.769371f, 31.135691f, -40.869640f), 0.930753f,
            0.000240f, 0);

        InstanceContent.RegisterEObj("Greencoralformation", 2000215, 3668216, 3280932, 4, new Vector3(64.988159f, 33.672821f, -56.690559f), 0.991789f,
            0.000048f, 0);

        InstanceContent.RegisterEObj("Hiddendoor", 2000217, 3653517, 3280959, 1, new Vector3(59.000000f, 32.000000f, -35.000000f), 1.000000f, -2.007129f,
            0);

        InstanceContent.RegisterEObj("Giantclam", 2000222, 4208408, 3284776, 1, new Vector3(181.170303f, 32.104599f, -128.069000f), 0.991789f, -0.862350f,
            0);

        // States -> vf_kai_off (id: 4) vf_kai_on (id: 5) vf_kai_pop (id: 6) close_open (id: 7) open_close (id: 8)
        InstanceContent.RegisterEObj("sgbg_s1d1_p1_shel1", 2000260, 4208409, 3424283, 4, new Vector3(166.318695f, 30.735420f, -128.312103f), 0.991789f,
            0.481030f, 0);

        // States -> vf_kai_off (id: 4) vf_kai_on (id: 5) vf_kai_pop (id: 6) close_open (id: 7) open_close (id: 8)
        InstanceContent.RegisterEObj("sgbg_s1d1_p1_shel1_1", 2000261, 4208410, 3425471, 4, new Vector3(158.800598f, 28.586321f, -76.340927f), 0.991789f,
            1.471638f, 0);

        // States -> vf_kai_off (id: 4) vf_kai_on (id: 5) vf_kai_pop (id: 6) close_open (id: 7) open_close (id: 8)
        InstanceContent.RegisterEObj("sgbg_s1d1_p1_shel1_2", 2000262, 4208411, 3425472, 4, new Vector3(125.463402f, 29.260550f, -51.934608f), 0.991789f,
            -0.375975f, 0);

        // States -> vf_kai_off (id: 4) vf_kai_on (id: 5) vf_kai_pop (id: 6) close_open (id: 7) open_close (id: 8)
        InstanceContent.RegisterEObj("sgbg_s1d1_p1_shel1_3", 2000263, 4208412, 3425473, 4, new Vector3(126.070198f, 28.913260f, -99.908722f), 1.000000f,
            0.020540f, 0);

        // States -> vf_kai_off (id: 4) vf_kai_on (id: 5) vf_kai_pop (id: 6) close_open (id: 7) open_close (id: 8)
        InstanceContent.RegisterEObj("sgbg_s1d1_p1_shel1_4", 2000264, 4208413, 3425474, 4, new Vector3(97.055313f, 27.081551f, -70.264381f), 0.991789f,
            -0.618915f, 0);

        // States -> vf_kai_off (id: 4) vf_kai_on (id: 5) vf_kai_pop (id: 6) close_open (id: 7) open_close (id: 8)
        InstanceContent.RegisterEObj("Rambadedoor", 2000225, 3653865, 3281037, 1, new Vector3(-35.299999f, 24.000000f, 60.799999f), 1.000000f, -2.007129f,
            0);

        InstanceContent.RegisterEObj("Captainsquarters", 2000227, 3687697, 3281045, 4, new Vector3(-95.044670f, 20.513069f, 172.039597f), 0.991789f,
            0.000048f, 0);

        InstanceContent.RegisterEObj("WaveriderGate", 2000231, 3655909, 3281133, 4, new Vector3(-130.600006f, 16.000000f, 156.800003f), 1.000000f,
            -2.007129f, 0);

        InstanceContent.RegisterEObj("TheHole", 2000232, 3656260, 3281161, 4, new Vector3(-36.000000f, 16.500000f, 163.300003f), 1.000000f, 1.047198f, 0);
        InstanceContent.RegisterEObj("Rambadedoor_1", 2000236, 3655908, 3281175, 1, new Vector3(-190.000000f, 7.000000f, 252.000000f), 1.000000f,
            -2.443461f, 0);

        InstanceContent.RegisterEObj("unknown_3", 2000235, 3656262, 3281178, 4, new Vector3(-156.500000f, 8.600000f, 252.500000f), 1.000000f, 1.134464f,
            0);

        InstanceContent.RegisterEObj("WaveriderGatekey", 2000255, 0, 3332312, 4, new Vector3(-95.515343f, 20.000000f, 177.197800f), 1.000000f, 0.000000f,
            0);

        InstanceContent.RegisterEObj("KeytotheHole", 2000256, 0, 3332313, 4, new Vector3(-38.076599f, 17.232731f, 158.839401f), 0.991760f, 1.561760f, 0);
        InstanceContent.RegisterEObj("Captainsquarterskey", 2000250, 0, 4192068, 4, new Vector3(-100.625000f, 15.600010f, 137.150696f), 1.000000f,
            0.000000f, 0);

        InstanceContent.RegisterEObj("sgpl_s1d1_sghit_ctrl", 2000223, 4200832, 4200772, 4, new Vector3(-24.018980f, 18.475060f, 111.404900f), 1.000000f,
            0.000000f, 0);

        InstanceContent.RegisterEObj("Unnaturalripples", 2000405, 3992454, 3741845, 4, new Vector3(-301.973206f, 6.500000f, 300.029388f), 0.991789f,
            0.000048f, 0);

        InstanceContent.RegisterEObj("Unnaturalripples_1", 2000406, 3992452, 3741894, 4, new Vector3(-302.037598f, 6.500000f, 336.047302f), 1.000000f,
            0.000000f, 0);

        InstanceContent.RegisterEObj("Unnaturalripples_2", 2000407, 3992449, 3741895, 4, new Vector3(-338.036499f, 6.500000f, 300.206512f), 0.991789f,
            0.000048f, 0);

        InstanceContent.RegisterEObj("Unnaturalripples_3", 2000408, 3992453, 3741897, 4, new Vector3(-337.929596f, 6.500000f, 335.975311f), 1.000000f,
            0.000000f, 0);

        InstanceContent.Director.SetCustomVar(0, (ulong)Random.Shared.Next((int)Corals.Blue, (int)Corals.Green));

        switch ((Corals)InstanceContent.Director.GetCustomVar(0))
        {
            case Corals.Blue:
                InstanceContent.RegisterEObj("Bloodymemo", 2000212, 0, 4072691, 4, new Vector3(320.812988f, 47.862450f, -130.776306f), 0.600000f,
                    -0.898762f);

                break;
            case Corals.Red:
                InstanceContent.RegisterEObj("Bloodymemo", 2001548, 0, 40726912, 4, new Vector3(320.812988f, 47.862450f, -130.776306f), 0.600000f,
                    -0.898762f);

                break;
            case Corals.Green:
                InstanceContent.RegisterEObj("Bloodymemo", 2001549, 0, 40726913, 4, new Vector3(320.812988f, 47.862450f, -130.776306f), 0.600000f,
                    -0.898762f);

                break;
        }
    }

    public override void OnGossip(Player player, EventObject eobj, Event Event)
    {
        if (eobj.Name == "Bloodymemo")
        {
            player.Event.NewScene(Event.Id, 1, SceneFlags.HIDE_HOTBAR);
        }

        if (eobj.Name == "Bluecoralformation" || eobj.Name == "Redcoralformation" || eobj.Name == "Greencoralformation")
        {
            player.Event.NewScene(Event.Id, 1, SceneFlags.HIDE_HOTBAR, result =>
            {
                if (result.param2 == 0)
                {
                    if (eobj.ObjectId == instance.Director.GetCustomVar(0))
                    {
                        eobj.UpdatePermissionInvisibility(1);
                        instance.Director.SetVar(0, (byte)Sequence.Seq1);
                        instance.RegisterEObj("Inconspicuousswitch", 2000216, 3653858, 3280956, 4, new Vector3(62.907951f, 33.969521f, -31.172279f),
                            1.000000f, -1.396264f, 0);
                    }

                }
            });
        }

        if (eobj.Name == "Inconspicuousswitch")
        {
            eobj.UpdatePermissionInvisibility(1);
            instance.GetEObjByName("Hiddendoor").UpdatePermissionInvisibility(7);
            instance.Director.SetVar(0, 3);
            madison = instance.CreateBNpcFromLayoutId(3988325);
        }

        if (eobj.Name == "Captainsquarterskey")
        {
            eobj.UpdatePermissionInvisibility(1);
            instance.SetVar(0, (byte)Sequence.Seq4);
            //instance.sendEventLogMessage( player, instance, 2031, { 2000512 } );
        }

        if (eobj.Name == "Captainsquarters" && instance.GetVar(0) == (byte)Sequence.Seq4)
        {
            player.Event.NewScene(Event.Id, 1, SceneFlags.HIDE_HOTBAR, Callback: result => { eobj.UpdatePermissionInvisibility(1); });
        }

        if (eobj.Name == "WaveriderGate" && instance.GetVar(0) == (byte)Sequence.Seq5)
        {
            player.Event.NewScene(Event.Id, 1, SceneFlags.HIDE_HOTBAR, Callback: result => { eobj.UpdatePermissionInvisibility(1); });
        }

        if (eobj.Name == "WaveriderGatekey")
        {
            eobj.UpdatePermissionInvisibility(1);
            instance.SetVar(0, (byte)Sequence.Seq5);
            denn = instance.CreateBNpcFromLayoutId(3978771);
        }

        if (eobj.Name == "Exit")
        {
            player.Event.NewScene(Event.Id, 1, SceneFlags.HIDE_HOTBAR, result =>
            {
                if (result.GetResult(0) != 1)
                {
                    player.TeleportTo(new WorldPosition(132, Vector3.Zero, 0));
                }

            });

        }
    }

    public override void OnBNpcKill(BNpc bNpc)
    {
        if (bNpc == this.madison)
        {
            instance.SetVar(0, 7);
            instance.GetEObjByName("Rambadedoor").UpdatePermissionInvisibility(7);
            this.madison = null;
            this.madison2 = instance.CreateBNpcFromLayoutId(4035056);
        }

        if (bNpc == this.madison2)
        {
            instance.GetEObjByName("Rambadedoor_1").UpdatePermissionInvisibility(7);
        }

        if (bNpc == this.denn)
        {
            instance.SetVar(0, (byte)Sequence.SeqFinish);
            instance.DutyComplete();
        }


    }

    public override void OnLeaveTerritory(Player player)
    {
        if (instance.GetVar(0) != (int)Sequence.SeqFinish)
            return;

        var quest = player.GetQuest(66211);
        if (quest.Sequence == 3)
        {
            player.UpdateQuest(66211, 255);
        }

        var quest2 = player.GetQuest(65781);
        if (quest2.Sequence == 4)
        {
            player.UpdateQuest(65781, 255);
        }
    }

}