using System.Collections.Generic;

namespace CalamityCN
{
    public class NPCNameDict
    {
        public static Dictionary<string, string> NPCName;
        public static void Load()
        {
            NPCName = new Dictionary<string, string>()
            {
                { "Bloatfish", "肿胀翻车鱼".zh() },
                { "BobbitWormHead", "博比特虫".zh() },
                { "BobbitWormSegment", "博比特虫".zh() },
                { "BoxJellyfish", "灯水母".zh() },
                { "Catfish", "鲶鱼".zh() },
                { "ChaoticPuffer", "混乱河豚".zh() },
                { "ColossalSquid", "巨像乌贼".zh() },
                { "Cuttlefish", "红乌贼".zh() },
                { "EidolonWyrmBody", "幻海妖龙".zh() },
                { "EidolonWyrmBodyAlt", "幻海妖龙".zh() },
                { "EidolonWyrmHead", "幻海妖龙".zh() },
                { "EidolonWyrmTail", "幻海妖龙".zh() },
                { "AdultEidolonWyrmBody", "成年幻海妖龙".zh() },
                { "AdultEidolonWyrmBodyAlt", "成年幻海妖龙".zh() },
                { "AdultEidolonWyrmHead", "成年幻海妖龙".zh() },
                { "AdultEidolonWyrmTail", "成年幻海妖龙".zh() },
                { "Flounder", "比目鱼".zh() },
                { "Frogfish", "躄鱼".zh() },
                { "GiantSquid", "巨大乌贼".zh() },
                { "Gnasher", "噬咬龟".zh() },
                { "GulperEelBody", "大嘴鳗".zh() },
                { "GulperEelBodyAlt", "大嘴鳗".zh() },
                { "GulperEelHead", "大嘴鳗".zh() },
                { "GulperEelTail", "大嘴鳗".zh() },
                { "Laserfish", "激光鱼".zh() },
                { "LuminousCorvina", "流光石首鱼".zh() },
                { "MantisShrimp", "螳螂虾".zh() },
                { "Mauler", "渊海狂鲨".zh() },
                { "MirageJelly", "幻海水母".zh() },
                { "MorayEel", "海鳝".zh() },
                { "OarfishBody", "桨鱼".zh() },
                { "OarfishHead", "桨鱼".zh() },
                { "OarfishTail", "桨鱼".zh() },
                { "ReaperShark", "猎魂鲨".zh() },
                { "SeaUrchin", "海胆".zh() },
                { "ToxicMinnow", "剧毒米诺鱼".zh() },
                { "Trasher", "捣鱼".zh() },
                { "Viperfish", "蝰蛇鱼".zh() },
                { "IrradiatedSlime", "辐射史莱姆".zh() },
                { "Radiator", "辐射海参".zh() },
                { "AquaticScourgeBody", "渊海灾虫".zh() },
                { "AquaticScourgeBodyAlt", "渊海灾虫".zh() },
                { "AquaticScourgeHead", "渊海灾虫".zh() },
                { "AquaticScourgeTail", "渊海灾虫".zh() },
                { "AquaticUrchin", "渊海海胆".zh() },
                { "Aries", "白羊座".zh() },
                { "AstralachneaGround", "星幻蛛".zh() },
                { "AstralachneaWall", "星幻蛛".zh() },
                { "AstralProbe", "幻星探测器".zh() },
                { "AstralSeekerSpit", "跟踪液".zh() },
                { "AstralSlime", "幻星史莱姆".zh() },
                { "Atlas", "擎星者".zh() },
                { "BigSightseer", "大监星眼".zh() },
                //{ "EnchantedNightcrawler", "附魔夜行者".zh() },
                { "FusionFeeder", "融食鲨".zh() },
                { "Hadarian", "哈德安翼龙".zh() },
                { "HiveEnemy", "星幻巢".zh() },
                { "Hiveling", "星幻蜂".zh() },
                { "Mantis", "星幻螳螂".zh() },
                { "Nova", "新星".zh() },
                { "SmallSightseer", "小监星眼".zh() },
                { "StellarCulex", "星幻蚊".zh() },
                { "Twinkler", "星幻萤火虫".zh() },
                { "AstrumAureus", "白金星舰".zh() },
                { "AureusSpawn", "小白星".zh() },
                { "AstrumDeusBody", "星神游龙".zh() },
                { "AstrumDeusHead", "星神游龙".zh() },
                { "AstrumDeusTail", "星神游龙".zh() },
                { "Brimling", "小硫火灵".zh() },
                { "BrimstoneElemental", "硫磺火元素".zh() },
                { "Bumblefuck", "痴愚金龙".zh() },
                { "Bumblefuck2", "癫狂龙裔".zh() },
				{ "WildBumblefuck", "癫狂龙裔".zh() },
                { "Cataclysm", "灾祸".zh() },
                { "Catastrophe", "灾难".zh() },
                { "CalamitasClone", "灾厄".zh() },
                { "SoulSeeker", "探魂者".zh() },
                { "CeaselessVoid", "无尽虚空".zh() },
                { "DarkEnergy", "暗能量".zh() },
                { "CrabShroom", "真菌孢子".zh() },
                { "Crabulon", "菌生蟹".zh() },
                { "CalamityEye", "灾厄眼".zh() },
                { "CharredSlime", "焦炭史莱姆".zh() },
                { "CultistAssassin", "邪教刺客".zh() },
                { "DespairStone", "绝望之石".zh() },
                { "Scryllar", "恶魔仆从".zh() },
                { "ScryllarRage", "恶魔仆从".zh() },
                { "SoulSlurper", "灵魂饮食者".zh() },
                { "Cryogen", "极地之灵".zh() },
                { "CryogenShield", "冰川护盾".zh() },
                { "DesertScourgeBody", "荒漠灾虫".zh() },
                { "DesertScourgeHead", "荒漠灾虫".zh() },
                { "DesertScourgeTail", "荒漠灾虫".zh() },
                { "DevourerofGodsBody", "神明吞噬者".zh() },
                { "CosmicGuardianBody", "星宇护卫".zh() },
                { "DevourerofGodsHead", "神明吞噬者".zh() },
                { "CosmicGuardianHead", "星宇护卫".zh() },
                { "DevourerofGodsTail", "神明吞噬者".zh() },
                { "CosmicGuardianTail", "星宇护卫".zh() },
                { "DankCreeper", "沼泽之眼".zh() },
                { "DarkHeart", "暗之心".zh() },
                { "HiveBlob", "腐化球".zh() },
                { "HiveBlob2", "腐化球".zh() },
                { "HiveCyst", "腐化囊".zh() },
                { "HiveMind", "腐巢意志".zh() },
                { "AquaticAberration", "深海吞食者".zh() },
                { "Leviathan", "利维坦".zh() },
                { "Anahita", "阿娜希塔".zh() },
                { "AnahitasIceShield", "冰护盾".zh() },
                { "AeroSlime", "天蓝史莱姆".zh() },
                { "Rimehound", "雾凇猎犬".zh() },
                { "ArmoredDiggerBody", "装甲掘地虫".zh() },
                { "ArmoredDiggerHead", "装甲掘地虫".zh() },
                { "ArmoredDiggerTail", "装甲掘地虫".zh() },
                { "BloomSlime", "龙蒿史莱姆".zh() },
                { "Bohldohr", "神庙滚石".zh() },
                { "Cnidrion", "卧龙海马".zh() },
                { "CosmicElemental", "宇宙元素".zh() },
                { "CrawlerAmber", "琥珀爬虫".zh() },
                { "CrawlerAmethyst", "紫水晶爬虫".zh() },
                { "CrawlerCrystal", "水晶爬虫".zh() },
                { "CrawlerDiamond", "钻石爬虫".zh() },
                { "CrawlerEmerald", "绿宝石爬虫".zh() },
                { "CrawlerRuby", "红宝石爬虫".zh() },
                { "CrawlerSapphire", "蓝宝石爬虫".zh() },
                { "CrawlerTopaz", "黄玉爬虫".zh() },
                { "CrimulanBlightSlime", "血腥枯萎史莱姆".zh() },
                { "Cryon", "冰人".zh() },
                { "CryoSlime", "寒元史莱姆".zh() },
                { "EbonianBlightSlime", "黑檀枯萎史莱姆".zh() },
                { "Eidolist", "幻妖灵巫".zh() },
                { "FearlessGoldfishWarrior", "无畏金鱼战士".zh() },
                { "HeatSpirit", "热能之魂".zh() },
                { "IceClasper", "食魂冰虫".zh() },
                { "ImpiousImmolator", "亵渎献祭者".zh() },
                { "KingSlimeJewel", "王冠宝石".zh() },
                { "OverloadedSoldier", "过载装甲骷髅".zh() },
                { "PerennialSlime", "永恒史莱姆".zh() },
                { "PhantomSpirit", "幻影幽灵".zh() },
                { "PhantomSpiritL", "幻魂".zh() },
                { "PhantomSpiritM", "幻魂".zh() },
                { "PhantomSpiritS", "幻魂".zh() },
                { "Piggy", "小猪猪".zh() },
                { "PlagueCharger", "瘟疫蜜蜂".zh() },
                { "PlagueChargerLarge", "瘟疫蜜蜂".zh() },
                { "Viruling", "毒飞虫".zh() },
                { "Melter", "蚀化者".zh() },
                { "PestilentSlime", "鸠毒史莱姆".zh() },
                { "Plagueshell", "瘟疫龟".zh() },
                { "ProfanedEnergyBody", "亵渎能量".zh() },
                { "ProfanedEnergyLantern", "亵渎能量".zh() },
                { "ScornEater", "吞食魔".zh() },
                { "ShockstormShuttle", "风暴飞碟".zh() },
                { "Stormlion", "风暴蚁狮".zh() },
                { "Sunskater", "寻阳鱼".zh() },
                { "Horse", "地元魔魂"},
                { "ThiccWaifu", "雨云元素".zh() },
                { "WulfrumDrone", "钨钢无人机".zh() },
                { "PerforatorBodyLarge", "血肉寄生者".zh() },
                { "PerforatorBodyMedium", "血肉寄生者".zh() },
                { "PerforatorBodySmall", "血肉寄生者".zh() },
                { "PerforatorCyst", "血肉囊".zh() },
                { "PerforatorHeadLarge", "血肉寄生者".zh() },
                { "PerforatorHeadMedium", "血肉寄生者".zh() },
                { "PerforatorHeadSmall", "血肉寄生者".zh() },
                { "PerforatorHive", "血肉宿主".zh() },
                { "PerforatorTailLarge", "血肉寄生者".zh() },
                { "PerforatorTailMedium", "血肉寄生者".zh() },
                { "PerforatorTailSmall", "血肉寄生者".zh() },
                { "PlaguebringerGoliath", "瘟疫使者歌莉娅".zh() },
                { "PlagueHomingMissile", "制导瘟疫飞弹".zh() },
                { "PlagueMine", "瘟疫之雷".zh() },
                { "PhantomFuckYou", "幽花之灵".zh() },
                { "Polterghast", "噬魂幽花".zh() },
                { "PolterghastHook", "幽花触爪".zh() },
                { "PolterPhantom", "噬魂幽花".zh() },
                { "ProfanedGuardianCommander", "统御守卫".zh() },
                { "ProfanedGuardianDefender", "神石守卫".zh() },
                { "ProfanedGuardianHealer", "圣晶守卫".zh() },
                { "Providence", "亵渎天神普罗维登斯".zh() },
                { "ProvSpawnDefense", "圣御守卫".zh() },
                { "ProvSpawnHealer", "天术守卫".zh() },
                { "ProvSpawnOffense", "神攻守卫".zh() },
                { "FlamePillar", "火焰柱".zh() },
                { "RavagerBody", "毁灭魔像".zh() },
                { "RavagerClawLeft", "毁灭魔像".zh() },
                { "RavagerClawRight", "毁灭魔像".zh() },
                { "RavagerHead", "毁灭魔像".zh() },
                { "RavagerHead2", "毁灭魔像".zh() },
                { "RavagerLegLeft", "毁灭魔像".zh() },
                { "RavagerLegRight", "毁灭魔像".zh() },
                { "RockPillar", "石元柱".zh() },
                { "CosmicLantern", "无限灯笼".zh() },
                { "Signus", "神之使徒西格纳斯".zh() },
                { "CosmicMine", "星界雷".zh() },
                { "EbonianSlimeGod", "黑檀史莱姆".zh() },
                { "SlimeGodCore", "史莱姆之神".zh() },
                { "CrimulanSlimeGod", "血化史莱姆".zh() },
                { "SplitCrimulanSlimeGod", "血化史莱姆".zh() },
                { "SplitEbonianSlimeGod", "黑檀史莱姆".zh() },
                { "CorruptSlimeSpawn", "腐化史莱姆".zh() },
                { "CorruptSlimeSpawn2", "腐化史莱姆".zh() },
                { "CrimsonSlimeSpawn", "血腥史莱姆".zh() },
                { "CrimsonSlimeSpawn2", "尖刺血腥史莱姆".zh() },
                { "StormWeaverBody", "风暴编织者".zh() },
                { "StormWeaverHead", "风暴编织者".zh() },
                { "StormWeaverTail", "风暴编织者".zh() },
                { "BlindedAngler", "盲鮟鱇".zh() },
                { "Clam", "蛤".zh() },
                { "EutrophicRay", "富养剑鱼".zh() },
                { "GhostBell", "鬼铃水母".zh() },
                { "BabyGhostBell", "小鬼铃水母".zh() },
                { "GiantClam", "巨像蛤".zh() },
                { "PrismBack", "棱晶背龟".zh() },
                { "SeaFloaty", "海蜉蝣".zh() },
                { "SeaMinnow", "海洋米诺鱼".zh() },
                { "SeaSerpent1", "深海巨蛇".zh() },
                { "SeaSerpent2", "深海巨蛇".zh() },
                { "SeaSerpent3", "深海巨蛇".zh() },
                { "SeaSerpent4", "深海巨蛇".zh() },
                { "SeaSerpent5", "深海巨蛇".zh() },
                { "SepulcherBody", "灾坟魔物".zh() },
                { "SepulcherHead", "灾坟魔物".zh() },
                { "SepulcherTail", "灾坟魔物".zh() },
				{ "SepulcherArm", "灾坟魔物".zh() },
                { "SoulSeekerSupreme", "至尊探魂者".zh() },
                { "SupremeCalamitas", "至尊灾厄".zh() },
                { "SupremeCataclysm", "至尊灾祸".zh() },
                { "SupremeCatastrophe", "至尊灾难".zh() },
                { "DILF", "大法师".zh() },
                { "FAP", "醉仙女".zh() },
                { "SEAHOE", "海王".zh() },
                { "THIEF", "强盗".zh() },
                { "Yharon", "丛林龙，犽戎".zh() },
                { "AnthozoanCrab", "珊瑚蟹".zh() },
                { "BelchingCoral", "喷嗝珊瑚".zh() },
                { "DevilFish", "魔鬼鱼".zh() },
				{ "DevilFishAlt", "魔鬼鱼".zh() },
                { "AcidEel", "酸水鳗".zh() },
                { "BloodwormFleeing", "血蠕虫".zh() },
                { "BloodwormNormal", "血蠕虫".zh() },
                { "CragmawMire", "峭咽潭".zh() },
                { "BabyFlakCrab", "小高口蟹".zh() },
                { "FlakCrab", "高口蟹".zh() },
                { "GammaSlime", "伽马史莱姆".zh() },
                { "NuclearTerror", "辐核骇兽".zh() },
                { "NuclearToad", "辐核蟾蜍".zh() },
                { "Orthocera", "直角石".zh() },
                { "Skyfin", "天鳍鱼".zh() },
                { "SulphurousSkater", "硫海爬虫".zh() },
                { "Trilobite", "三叶虫".zh() },
                { "SuperDummyNPC", "大草人".zh() },
                { "OldDuke", "硫海遗爵".zh() },
                { "SulphurousSharkron", "硫海龙鲨".zh() },
                { "OldDukeToothBall", "鲨牙刺球".zh() },
                { "MicrobialCluster", "微生物群".zh() },
                { "WulfrumGyrator", "钨钢回转器".zh() },
                { "WulfrumHovercraft", "钨钢悬浮坦克".zh() },
                { "WulfrumAmplifier", "钨钢增幅器".zh() },
                { "WulfrumRover", "钨钢漫步者".zh() },
                { "DesertNuisanceBody", "黄沙恶虫".zh() },
                { "DesertNuisanceHead", "黄沙恶虫".zh() },
                { "DesertNuisanceTail", "黄沙恶虫".zh() },
                { "RepairUnitCritter", "修复机器人".zh() },
                { "Apollo", "XS-03“阿波罗”".zh() },
                { "AresBody", "XF-09“阿瑞斯”".zh() },
                { "AresGaussNuke", "XF-09“阿瑞斯”高斯核弹发射器".zh() },
                { "AresLaserCannon", "XF-09“阿瑞斯”镭射加农炮".zh() },
                { "AresPlasmaFlamethrower", "XF-09“阿瑞斯”离子加农炮".zh() },
                { "AresTeslaCannon", "XF-09“阿瑞斯”特斯拉加农炮".zh() },
                { "Artemis", "XS-01“阿尔忒弥斯”".zh() },
                { "Draedon", "嘉登".zh() },
                { "ThanatosBody1", "XM-05“塔纳托斯”".zh() },
                { "ThanatosBody2", "XM-05“塔纳托斯”".zh() },
                { "ThanatosHead", "XM-05“塔纳托斯”".zh() },
                { "ThanatosTail", "XM-05“塔纳托斯”".zh() },
                { "CalamitasEnchantDemon", "恶魔".zh() },
                { "DemonPortal", "神秘传送门".zh() },
                { "ExhumedHeart", "死灵硫火心".zh() },
                { "LecherousOrb", "淫欲之眼".zh() },
                { "BrimstoneHeart", "硫磺火之心".zh() },
                { "WITCH", "硫磺火女巫".zh() },
                { "Rotdog", "血犬".zh() },
                { "PlaguebringerMiniboss", "瘟疫使者".zh() },
                { "AuroraSpirit", "极光之灵".zh() },
                { "Nanodroid", "纳米机器人".zh() },
                { "NanodroidDysfunctional", "纳米机器人".zh() },
                { "NanodroidPlagueRed", "纳米机器人".zh() },
                { "NanodroidPlagueGreen", "纳米机器人".zh() },
                { "Androomba", "仙女座扫除机".zh() },
                { "AndroombaFriendly", "仙女座扫除机老友".zh() },
            };
        }
        public static void Unload()
        {
            NPCName = null;
        }
    }
}
