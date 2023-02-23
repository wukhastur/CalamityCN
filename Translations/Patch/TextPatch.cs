using MonoMod.Cil;
using MonoMod.RuntimeDetour;
using System;
using System.Collections.Generic;
using System.Reflection;
using CalamityMod.Projectiles.Melee;
using CalamityMod.Projectiles.Summon;
using CalamityMod.Systems;
using CalamityMod.NPCs.TownNPCs;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Items.Potions;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Cooldowns;
using CalamityMod.Projectiles.Summon.SmallAresArms;
using CalamityMod.NPCs.CalClone;
using CalamityMod.NPCs.Ravager;
using CalamityMod.NPCs.Bumblebirb;
using CalamityMod.NPCs.ProfanedGuardians;
using CalamityCN.Utils;
using CalamityMod.Items.LoreItems;
using Terraria.ModLoader;
using CalamityMod.Tiles.Ores;
using CalamityMod.Tiles.Abyss.AbyssAmbient;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using CalamityMod.Buffs.DamageOverTime;
using Terraria.DataStructures;
using Terraria.Audio;

namespace CalamityCN.Translations.Patch {
    public class TextPatch {
        private static List<ILHook> ILHooksT;
        private static Hook temp_AuricOreDeath;

        public static void Load() {
            MethodBase auric = typeof(CalamityPlayer).GetMethod("HandleTileEffects", BindingFlags.NonPublic | BindingFlags.Instance);
            temp_AuricOreDeath = new Hook(auric, HandleTileEffects);
            if (temp_AuricOreDeath is not null)
                temp_AuricOreDeath.Apply();

            ILHooksT = new List<ILHook>();
            //武器提示
            QuickTranslate(typeof(ExoskeletonPanel), "PreDraw", "Insufficient minion slots!", "召唤栏位不足！");
            //世界加载
            string[][] WMS = new string[][]
            {
                new string[2]{"Corrupting a floating island", "正在邪恶化一处空岛"},
                new string[2]{"Partially flooding an overblown desert", "正在淹没部分沙漠"},
                new string[2]{"Building a bigger jungle temple", "正在创建更大的丛林神庙"},
                new string[2]{"Placing the Lihzahrd altar", "正在放置神庙电饭煲"},
                new string[2]{"Reallocating gem deposits to match cavern depth", "正在洞穴深度生成宝石洞穴"},
                new string[2]{"Uncovering the ruins of a fallen empire", "正在令失落帝国的废墟重见天日"},
                new string[2]{"Polluting one of the oceans", "正在污染一个海洋"},
                new string[2]{"Placing hidden shrines", "正在放置特殊神龛"},
                new string[2]{"Rust and Dust", "锈蚀和灰尘"},
                new string[2]{"Discovering the new Challenger Deep", "正在探索更深处的新挑战"},
                new string[2]{"Further polluting one of the oceans", "正在进一步污染海洋"},
                new string[2]{"I Wanna Rock", "石头，嘿嘿嘿，我的石头" }

            };
            foreach (string[] il in WMS) {
                if (!il[1].Equals("")) {
                    QuickTranslate(typeof(WorldgenManagementSystem), "ModifyWorldGenTasks", il[0], il[1]);
                }
            }
            //对话按钮
            QuickTranslate(typeof(THIEF), "SetChatButtons", "Refund", "退款".zh());
            QuickTranslate(typeof(SEAHOE), "SetChatButtons", "Help", "帮助".zh());
            QuickTranslate(typeof(WITCH), "SetChatButtons", "Enchant", "咒术炼注".zh());
            QuickTranslate(typeof(FAP), "SetChatButtons", "Death Count", "死亡次数".zh());
            //死亡信息
            QuickTranslate(typeof(CalamityPlayer), "KillPlayer", " is food for the Wyrms.", "成为了妖龙的食物。");
            QuickTranslate(typeof(CalamityPlayer), "KillPlayer", "Oxygen failed to reach ", "在");
            QuickTranslate(typeof(CalamityPlayer), "KillPlayer", " from the depths of the Abyss.", "溺亡时肺里没有一丝氧气。");
            QuickTranslate(typeof(CalamityPlayer), "KillPlayer", " failed the challenge at hand.", "没能逃脱命运之手的操控。");
            QuickTranslate(typeof(CalamityPlayer), "UpdateBadLifeRegen", "'s flesh was dissolved by sulphuric water.", "的血肉被硫磺海水溶解了。");

            string[][] CPPK = new string[][]
            {
            new string[2]{" downed too many shots.", "喝的太多了。"},
            new string[2]{"'s liver failed.", "的肝衰竭了。"},
            new string[2]{" was charred by the brimstone inferno.", "被硫磺烈焰烧焦了。"},
            new string[2]{"'s soul was released by the lava.", "的灵魂与岩浆融为一体了。"},
            new string[2]{"'s soul was extinguished.", "的灵魂消散了。"},
            new string[2]{" was melted by the toxic waste.", "被剧毒废物融化了。"},
            new string[2]{"'s ashes scatter in the wind.", "离解成灰。"},
            new string[2]{" was turned to ashes by the Profaned Goddess.", "被亵渎之神变成了灰烬。"},
            new string[2]{" fell prey to their sins.", "被自己的罪孽吞噬了。"},
            new string[2]{"'s spirit was turned to ash.", "的灵魂化作了灰烬。"},
            new string[2]{" became a blood geyser.", "血如泉涌。"},
            new string[2]{" was crushed by the pressure.", "被压力碾碎了。"},
            new string[2]{"'s lungs collapsed.", "的肺坍缩成了一团。"},
            new string[2]{" was consumed by the black flames.", "被漆黑的烈焰燃尽。"},
            new string[2]{"'s flesh was melted by the plague.", "的血肉被瘟疫所融化。"},
            new string[2]{" didn't vaccinate.", "已无药可医。"},
            new string[2]{"'s infection spread too far.", "成为了星辉瘟疫的一部分。"},
            new string[2]{"'s skin was replaced by the astral virus.", "的皮肤被星辉病毒侵蚀了。"},
            new string[2]{" was incinerated by lunar rays.", "被月之射线焚化了。"},
            new string[2]{" vaporized into thin air.", "气化了。"},
            new string[2]{"'s life was completely converted into mana.", "的生命完全升华为了魔力。"},
            new string[2]{" succumbed to alcohol sickness.", "因酗酒痛苦死去。"},
            new string[2]{" withered away.", "凋谢了。"},
            new string[2]{" was summoned too soon.", "没来得及积蓄足够的力量。"},
            new string[2]{" lost too much blood.", "失血过多。"},
            new string[2]{" was blown away by miraculous technological advancements.", "被奇迹般的科技进步扫进了历史的垃圾堆。"},
            new string[2]{" disintegrated from the overpowering exotic resonance.", "与星流能量共振而塌缩了。"}
            };
            foreach (string[] il in CPPK) {
                if (!il[1].Equals("")) {
                    QuickTranslate(typeof(CalamityPlayer), "PreKill", il[0], il[1]);
                }
            }
            QuickTranslate(typeof(CalamityPlayer), "OnConsumeMana", " converted all of their life to mana.", "的生命完全升华为了魔力。");
            QuickTranslate(typeof(BloodBoiler), "Shoot", " suffered from severe anemia.", "极度贫血而死。");
            QuickTranslate(typeof(BloodBoiler), "Shoot", " was unable to obtain a blood transfusion.", "没能及时完成输血。");
            QuickTranslate(typeof(AstralInjection), "OnConsumeItem", "'s blood vessels burst from drug overdose.", "的血管由于药物过量爆裂了。");
            QuickTranslate(typeof(ThornBlossom), "Shoot", " was violently pricked by a flower.", "被花上的刺扎死了。");
            //进入世界
            QuickTranslate(typeof(CalamityPlayer), "OnEnterWorld", " [c/EE4939:Note: The Fandom wiki is no longer supported by Calamity.] ", " [c/EE4939:备注：灾厄已不再支持Fandom Wiki。] ");
            QuickTranslate(typeof(CalamityPlayer), "OnEnterWorld", " [c/EE4939:Check out the Official Calamity Mod Wiki at ][c/3989FF:calamitymod.wiki.gg][c/EE4939:!] ", " [c/EE4939:官方灾厄维基][c/3989FF:calamitymod.wiki.gg][c/EE4939:!] [c/EE4939:灾厄中文维基][c/3989FF:soammer.com][c/EE4939:!] ");
            //冷却图标
            QuickTranslate(typeof(ChaosState), "get_DisplayName", "Teleportation Cooldown", "传送冷却");
            QuickTranslate(typeof(AquaticHeartIceShield), "get_DisplayName", "Ice Shield Cooldown", "冰盾冷却");
            QuickTranslate(typeof(BloodflareFrenzy), "get_DisplayName", "Blood Frenzy Cooldown", "鲜血狂怒冷却");
            QuickTranslate(typeof(BloodflareRangedSet), "get_DisplayName", "Bloodflare Soul Cooldown", "幽火游魂冷却");
            QuickTranslate(typeof(BrimflameFrenzy), "get_DisplayName", "Brimflame Frenzy Cooldown", "硫火狂怒冷却");
            QuickTranslate(typeof(CounterScarf), "get_DisplayName", "Scarf Cooldown", "围巾冷却");
            QuickTranslate(typeof(DivineBless), "get_DisplayName", "Divine Bless Cooldown", "神圣祝福冷却");
            QuickTranslate(typeof(DivingPlatesBreaking), "get_DisplayName", "Abyssal Diving Suit Plates Durability", "深渊潜游服耐久");
            QuickTranslate(typeof(DivingPlatesBroken), "get_DisplayName", "Abyssal Diving Suit Broken Plates", "深渊潜游服外壳破损");
            //QuickTranslate(typeof(CalamityMod.Cooldowns.DraconicElixir), "get_DisplayName", "Draconic Surge Cooldown", "龙魂加持冷却");
            QuickTranslate(typeof(EvasionScarf), "get_DisplayName", "Scarf Cooldown", "围巾冷却");
            QuickTranslate(typeof(FleshTotem), "get_DisplayName", "Contact Damage Halving Cooldown", "接触伤害减半冷却");
            QuickTranslate(typeof(GlobalDodge), "get_DisplayName", "Dodge Cooldown", "闪避冷却");
            QuickTranslate(typeof(GodSlayerDash), "get_DisplayName", "God Slayer Dash Cooldown", "弑神者冲刺冷却");
            QuickTranslate(typeof(InkBomb), "get_DisplayName", "Ink Bomb Cooldown", "墨水炸弹冷却");
            QuickTranslate(typeof(LionHeartShield), "get_DisplayName", "Energy Shell Cooldown", "能量庇护冷却");
            QuickTranslate(typeof(NebulousCore), "get_DisplayName", "Nebulous Core Cooldown", "星云之核冷却");
            QuickTranslate(typeof(OmegaBlue), "get_DisplayName", "Abyssal Madness Cooldown", "深渊狂乱冷却");
            QuickTranslate(typeof(OmegaBlue), "get_DisplayName", "Abyssal Madness", "深渊狂乱");
            QuickTranslate(typeof(PermafrostConcoction), "get_DisplayName", "Permafrost's Concoction Cooldown", "永冻秘药冷却");
            QuickTranslate(typeof(PlagueBlackout), "get_DisplayName", "Plague Blackout Cooldown", "瘟疫爆发冷却");
            QuickTranslate(typeof(PotionSickness), "get_DisplayName", "Healing Cooldown", "治疗冷却");
            QuickTranslate(typeof(PrismaticLaser), "get_DisplayName", "Prismatic Laser Barrage Cooldown", "光棱冷却");
            QuickTranslate(typeof(ProfanedSoulArtifact), "get_DisplayName", "Profaned Soul Artifact Burnout", "渎魂神物灼伤冷却");
            QuickTranslate(typeof(RelicOfResilience), "get_DisplayName", "Relic of Resilience Cooldown", "塑魂冷却");
            QuickTranslate(typeof(RogueBooster), "get_DisplayName", "Rogue Booster Cooldown", "瘟疫染料背包冷却");
            QuickTranslate(typeof(SandCloak), "get_DisplayName", "Sand Cloak Cooldown", "沙尘披风冷却");
            QuickTranslate(typeof(SandsmokeBomb), "get_DisplayName", "Sandsmoke Bomb Cooldown", "沙尘覆盖冷却");
            QuickTranslate(typeof(SandsmokeBomb), "get_DisplayName", "Sandsmoke Bomb Duration", "沙尘覆盖持续时间");
            QuickTranslate(typeof(SilvaRevive), "get_DisplayName", "Silva Revive Cooldown", "始源林海复活冷却");
            QuickTranslate(typeof(TarragonCloak), "get_DisplayName", "Tarragon Cloak Cooldown", "龙蒿披风冷却");
            QuickTranslate(typeof(TarragonImmunity), "get_DisplayName", "Tarragon Immunity Cooldown", "龙蒿无敌冷却");
            QuickTranslate(typeof(UniverseSplitter), "get_DisplayName", "Universe Splitter Cooldown", "渊宇天基炮冷却");
            QuickTranslate(typeof(WulfrumBastion), "get_DisplayName", "Wulfrum Bastion Cooldown", "钨钢转换冷却");
            QuickTranslate(typeof(WulfrumBastion), "get_DisplayName", "Power Armor Durability", "能量护甲耐久");
            QuickTranslate(typeof(WulfrumRoverDriveDurability), "get_DisplayName", "Protective Matrix Durability", "保护矩阵耐久");
            QuickTranslate(typeof(WulfrumRoverDriveRecharge), "get_DisplayName", "Protective Matrix Recharge", "保护矩阵充能");
            //死亡名称
            QuickTranslate(typeof(CalamitasClone), "BossLoot", "The Calamitas Clone", "灾厄之影");
            QuickTranslate(typeof(RavagerBody), "BossLoot", "Ravager", "毁灭魔像");
            QuickTranslate(typeof(Bumblefuck), "BossLoot", "A Dragonfolly", "痴愚金龙");
            QuickTranslate(typeof(ProfanedGuardianCommander), "BossLoot", "A Profaned Guardian", "亵渎守卫");
            QuickTranslate(typeof(ProfanedGuardianDefender), "BossLoot", "A Profaned Guardian", "亵渎守卫");
            QuickTranslate(typeof(ProfanedGuardianHealer), "BossLoot", "A Profaned Guardian", "亵渎守卫");
            //Lore
            QuickTranslate(typeof(LoreAbyss), "get_Lore", "While there are many sightless crevasses in the deep sea, this one is a unique geological marvel.\nIt is located unsettlingly close to the shoreline. Somehow, even eons of tectonics could not seal or crush it.\nThe isolated Abyss is the debatably tranquil home of the naiad Anahita and some reclusive sea creatures.\nHere, I disposed of the burgeoning remains of Silva, the Goddess of Life itself. Obviously, she of all Gods refused to truly die.\nMy wishes were that she would be forgotten, but her tenacity is remarkable.\nDiffused, her influence inundated that pit of crushing pressure with flora and fauna aplenty.\nHer great roots continue to thrash and tear at the impossibly dense stone, growing uncontrollably.\nShe will soon remake it in her image. I can think of no worse fate for this accursed, hadal domain.",
                "尽管深海中有无数道肉眼难辨的裂痕，这一道却足以称得上是地质学的奇迹。\n它位于离海岸线很近的地方，令人不安。不知何故，即使是长年累月的地质构造也无法封住或压碎它。\n这道与世隔绝的深渊便是水之仙女阿娜希塔和一些隐居的海洋生物的宁静家园，尽管是否真的宁静，还有待商榷。\n在这里，我处理了生命女神席尔瓦那具迅速增长的遗体。很明显，就算是在所有的神明之中，她也是真正拒绝死亡的那一位。\n我曾希望她会为世人所遗忘，可她的坚韧引人瞩目。\n她的余响于此扩散，无数的茎叶占据了这道重压的深坑。\n从她身上延展而出的巨大根系不断鞭笞和撕扯着这些致密的石头，不受控制地生长着。\n很快，她就会以自己的形象重新塑造这块区域。对于这片被诅咒的深海，我想不出有什么比这更加糟糕的命运。");
            QuickTranslate(typeof(LoreAquaticScourge), "get_Lore", "Another once grand sea serpent, well-adapted to its harsh environs.\nUnlike the other Scourge, which was half starved and chasing scraps for its next meal, it lived comfortably.\nMicroorganisms evolve rapidly, so it was able to maintain its filter feeding habits as the sea putrefied.\nWhat a stark contrast to the rest of the ecosystem. Nearly every other creature in the Sulphur Sea is hostile.\nA shame that its last bastion of tranquility has fallen.",
                "另一条曾经的大海蛇，它很好地适应了这片恶劣的环境。\n与那条永远在半饿半饱的状态下追猎食物的灾虫相比，它活得很舒服。\n由于此地的微生物进化很快，它得以无忧无虑地在这片糜烂的海洋中保持被动过滤食物的进食习惯。\n考虑到硫磺海中的几乎所有的生物都充满敌意，它的存在与这个生态系统形成了鲜明的对比。\n遗憾的是，守护这片宁静的最后堡垒已然陨落。");
            QuickTranslate(typeof(LoreArchmage), "get_Lore", "He yet lives…?! I thought him slain by Calamitas. It appears she imprisoned the Archmage to spare his life.\nI assumed that frigid mass was an old construct of his, running amok without its master to shepherd it.\nPermafrost was an old ally of mine, wielding the prestigious title of Archmage and great renown.\nHis wisdom guided my original conquests, or rather, made much of them possible at all.\nAs my crusade evolved and my ambitions grew, he expressed vehement disapproval.\nWhere once he saw justice, there was now tyranny. He departed, and the Witch not long after.",
                "他还活着...？我以为灾厄已经杀害了他，看来她最后还是没能痛下杀手，只是把他囚禁了起来。\n我还以为那团冰冷的造物只是他的一个旧构件，在没有主人看管的情况下四处作乱。\n永冻是我的一个老盟友，他是著名的大法师，享有巨大的声望。\n是他的智慧指导了我最初的征战，至少，使它们中的大部分得以化为现实。\n可随着征讨的持续，我的野心不断增长，他因此开始表现出强烈的反对。\n对他来说，过去被视作正义的事物，现在只剩下了暴政。他离开了。不久，女巫也随他而去。");
            QuickTranslate(typeof(LoreAstralInfection), "get_Lore", "This twisted dreamscape is a starborne equivalent of the mundane rot you see in your lands.\nI do not claim to understand the process in detail, but even the stars above can die. Left unchecked, their corpses bloat and fester.\nTypically, some semblance of order is maintained. It is not unlike the circle of life.\nCosmic beings patrol the fathomless void and pick at the carrion, leaving clean bones.\nThe infection itself is a disturbance from deep space. It has a mind of its own, and projects its will upon life and land.\nThose whose minds can grasp the true form of the universe, are largely immune.\nThey cannot be starstruck by a supposed higher truth, let alone one preached by a pustule.",
                "这片扭曲的星疫梦景，和你在尘世间所见到的腐败并无二致。\n并不是说我了解这个过程的细节。但即便是高悬穹顶的星星也会死亡。它们的尸体也会膨胀和溃烂，除非我们加以控制。\n通常情况下，秩序的表象依然会在此得到维持，这与生命的循环没有什么不同。\n宇宙生物在深不可测的虚空中游荡，它们挑拣腐肉，留下干净的骨骸。\n感染本质上是一种来自深空的干扰。它不仅拥有自己的思想，还将其意志投射在生命于与土地之上。\n那些掌握宇宙真理之人，在很大程度上是免疫这些东西的。\n他们不可能被一个所谓更高级的真理所迷惑，何况是一团披着疫病外衣的妄论。");
            QuickTranslate(typeof(LoreAstrumAureus), "get_Lore", "Ever pragmatic, Draedon dispatched this machine to locate and analyze the source of the Astral Infection.\nWhile nominally for reconnaissance, the Aureus model is heavily armed and can scale any terrain.\nIt performed admirably, at least until it was assimilated into the Infection.\nSapient minds have enough willpower to resist the Infection's call indefinitely.\nHowever, even the finest silicon is not beyond its reach. Draedon prefers his creations to serve, after all.\nWith this experiment concluded, he will certainly be examining you next. Watch yourself.",
                "一向务实的嘉登派遣这台机器来定位和分析星辉瘟疫的源头。\n侦察只是名义上的说法，白金式实质上可谓全副武装，其机动性足以翻越任何地形。\n它的表现成果令人钦佩，至少在它被感染同化之前是如此。\n智慧者的意志力完全可以抵挡瘟疫的呼唤。\n然而，即使是最精密的电路也不可能达到这个境界。毕竟，嘉登更偏好让自己的创造物为他人役使。\n随着这场实验盖棺定论，他接下来肯定会对你进行调查。你自己小心。");
            QuickTranslate(typeof(LoreAstrumDeus), "get_Lore", "On our world, this being is revered as the God of the night sky. It is said to devour dying stars and birth new ones in turn.\nUnlike the many Gods you or I know, it is guiltless. An important distinction, for it was equally as diseased as they.\nThe infection that tainted its body is from beyond Terraria. Neither I nor Draedon recognize it fully.\nWith its will subsumed, it hurled a chunk of infested astral matter at our world, then came to guard it.\nThankfully, such a grandiose being that walks amongst the stars is likely not truly dead.\nWhile the land has paid a terrible price, the price of a wrongful conviction is higher still.",
                "在我们的世界里，这种生物被尊为夜空之神。据说它能吞噬垂死的星星，并以此产生新的星。\n与你我所知的众多神明不同，它并不背负任何罪恶。这个区别十分重要，因为它和他们一样恶疾缠身。\n玷污它身体的感染来自泰拉瑞亚之外，我和嘉登都没能完全认识它的全貌。\n随着这位神明的意志为瘟疫所征服，它向我们的世界散播了一大块受感染的星辉物质并将其守护。\n值得庆幸的是，这样一位在星空中徘徊的宏伟神明很可能没有真正死去。\n虽然这片土地已经为此付出了可怕的代价，但与错判的代价相比还是相形见绌。");
            QuickTranslate(typeof(LoreAwakening), "get_Lore", "The tombs of the Dragons stir. My eyes lift to see ancient dust dancing from high ledges.\nThese grand wings… how long has it been since I was a hero worthy of their name?\nIt feels like centuries have passed, and all I've done is blink.\nLook upon my works, as they are. Ruined. None would dare seek me out, tread my path.\nNaught awaits them in this cruel world.",
                "巨龙的墓穴在躁动不安。我抬起眼，古老的尘埃在高耸的峭壁随风飞扬。\n这些宏伟的翅膀......我有多久没有成为配得上他们名字的英雄了？\n仿佛已经过了几个世纪，我却只是眨了眨眼。\n看看我曾经取得的成就吧，现在早已被破坏殆尽，荡然无存。没人敢来寻找我，踏上我所踏上过的道路。\n在这个残酷的世界里，等待他们的唯有空虚。");
            QuickTranslate(typeof(LoreAzafure), "get_Lore", "Oft called the First City, its tumultuous history stretches back to the Draconic Era.\nAn odd jewel of civilization, the immense heat of the underworld provided it unlimited potential in defense and industry.\nSuch was the renown of the forgemasters that when I swayed them to my cause, I was never lacking for arms.\nIt pains me to say even in hindsight, but their artisanry paved the downfall of the entire city.\nFor the Witch and I, the air here will forever be laden with regret. There is nothing to be done.",
                "通常被称为第一城，其动荡的历史可以追溯到巨龙时代。\n作为文明的一朵奇葩，地狱的巨大热量为它的城防和工业提供了无限的潜力。\n锻造大师的名声如此之大，以至于当我让他们投身于我的事业时，我从来不需要担心武器的问题。\n就算是事后诸葛地去陈述这件事也让我感到痛苦，但，他们的工匠精神无疑是为整个城市的陨落铺平了道路。\n对于女巫和我而言，这里的空气永远只有遗憾。我们无能为力。");
            QuickTranslate(typeof(LoreBloodMoon), "get_Lore", "This malevolence is not the work of any God. Blood moons trace their origins to the dawn of history.\nIt is an occurrence equally sinister and banal. Everyone is acclimated to the shambling hordes of undead.\nOrganized societies are not threatened in the slightest. If anything, they welcome the opportunity to train green foot soldiers.\nThose with fire in their veins may strike out on their own, to revel in the slaughter.\nThat is how I remember the sleepless nights from my younger days… Knee deep in corpses.",
                "如此恶意并非任何一位神明的杰作。血月的起源甚至可以追溯到历史诞生之时。\n这个现象险恶而平庸，所有人都已经适应了成群结队的亡灵。\n组织性强的社会丝毫不担心此类威胁。事实上，他们十分欢迎这些训练新兵的机会。\n那些血管里充斥着怒火的人倒是可能会自己出击，在屠杀中享受狂欢。\n是的，这就是我年轻时不眠之夜的记忆......那时，尸体一度埋过了我的膝盖。");
            QuickTranslate(typeof(LoreBrainofCthulhu), "get_Lore", "It is true that unspeakable abominations may now be commonplace, largely by my hand.\nThough they have always been a product of the folly of the Gods, the same Gods would cull them in equal measure.\nMy decimation of the falsely divine left many old horrors unconstrained, with new ones birthed every year.\nNow, they are your stepping stones.",
                "那些现在人们早以司空见惯的憎恶着实让我难以启齿，尤其是它们都因我而起的时候。\n虽然它们从来都是诞生自神明的愚行，但这些神明也会以同样的方式将它们剔除。\n我杀死了虚假的神灵，但却让那些未被剔除的可憎之物失去了约束，甚至每年都有新的恐怖从中孕育而出。\n好在，它们已经成为了你的垫脚石。");
            QuickTranslate(typeof(LoreBrimstoneElemental), "get_Lore", "A peculiar being. Until recently, she had laid dormant for ages, posing as the city's silent matron.\nAs its economy boomed, traces of brimstone found their way all across the known world.\nIt was never clear why her slumber ended. At first she stirred. The people were cautiously optimistic.\nWhen she woke, it was horrific. Her inferno billowed through the streets. None were safe.\nFate had a sick sense of humor that day, for Calamitas was there to match her.\nPerhaps the two were attuned somehow. They fought to a standstill, fire against fire.\nNeither were victorious, and despite her intentions, the city was razed by her flames.",
                "这是一个奇特的存在。她直到最近都一直处于休眠状态，假扮这座城市沉默的女主人。\n随着经济的蓬勃发展，硫磺火的痕迹早已遍布整个已知世界。\n我们至今仍不清楚是什么扰了她的梦乡。起初，她只是被稍稍打扰到，人们尚对此谨慎地保持乐观态度。\n然后，她醒了，那简直就是灾难。她的地狱之火吞噬了街道，没人能够独善其身。\n那一天，命运开了个病态般的玩笑。灾厄刚好就在那儿，充当她的对手。\n也许这两位在某种程度上十分合拍。火焰对火焰，她们一直打到了精疲力竭。\n这场战斗没有胜利者，无论她的意图如何，这座城市在最后还是被她的烈火夷为了平地。");
            QuickTranslate(typeof(LoreCalamitas), "get_Lore", "None have borne the brunt of misfortune quite like the Brimstone Witch, Calamitas.\nWhen first I saw her, she was still a girl. Prostrated in my court, garbed in charred rags, trembling.\nI could not grasp the unfathomable, raw power of the fell magics that coursed through her.\nShe could scant control it herself. Permafrost recognized this immediately. With a pained face, he counseled me to look after her.\nThe Witch entered his tutelage, and soon after, my service. She was ablaze with desire to douse the Gods in her wicked wrath.\nIndeed, the faithful already quaked in her presence. Her name was a moniker of theirs, one uttered quietly in fear.\nIn my campaigns, I counted on her sheer capacity for annihilation as my ace in the hole.\nNo man, no army, no city, and no God could stand against her unbridled fury.\nEventually, the girl's horrific sin was too much for her to bear. She left my side along with her mentor.\nThe weight of her deeds haunts her to this day. She despises me, and I cannot blame her.\nPlease, if you would, show her respect where I did not.",
                "没有人曾像硫磺火女巫灾厄那样承受过如此的厄运。\n我第一次见到她的时候，她还是个女孩。那天，她穿着烧焦的破衣服，浑身颤抖地跪在我的审判庭上。\n我无法掌握她身上所流淌着的，原始而深不可测的堕落魔法之力。\n她自己也很难控制它。永冻立即意识到了这一点。他满脸痛苦地向我建议，由他来照顾这孩子。\n女巫接受了他的指导，不久之后，她开始为我服务。她渴望以自己的无尽愤怒来毁灭神明。\n事实上，信徒们已经在她面前颤抖不已。她的名字成为了他们的代名词，那是一个只能在恐惧中低声道出的名讳。\n在我的战役中，她完全毁灭一切的能力一向是我的王牌。\n任何人，任何军队，任何城市，乃至任何神明都不可能招架她的极端怒火。\n最终，这个女孩再也无法承受自己身上的可怕罪恶。她和她的导师一起从我的身边离开了。\n她的罪孽至今仍囚禁着她。她因此唾弃我，我不能怪她。\n求求你，如果你愿意的话，请向她表达我没能做到的尊重。");
            QuickTranslate(typeof(LoreCalamitasClone), "get_Lore", "I had seen this monster roaming the night in the past, and thought nothing of it.\nWith its technology, it was certainly one of Draedon's creations.\nBut, to think it was housing a clone of the Witch… Detestable.\nSurely Calamitas would want nothing to do with such a project.\nI know not how it wields her brimstone magic. Perhaps some day one of us may find answers.",
                "我过去见过这个在夜里徘徊的怪物，但我曾对此毫不在意。\n这样的技术，它肯定是嘉登的作品之一。\n但是，一想到它是女巫的复制体......令人可憎。\n很明显灾厄绝不会想和这样的东西有任何关系。\n我不知道这只怪物会如何使用她的硫磺魔法。也许我们中有一个人能在某天找到答案。");
            QuickTranslate(typeof(LoreCeaselessVoid), "get_Lore", "A contained, previously inert portal sealed in the Dungeon. The presiding cult kept it a closely guarded secret.\nUpon sighting the Devourer of Gods, their leader hurriedly led me to its chamber to reveal its existence to me.\nThe portal led to the Devourer's home. It was identical to his, only ancient and perfectly stable.\nThe serpent claimed it, too, was his creation. Its permanence was a mistake he later rectified.\nBut this rift was unquestionably far older than he. It dated back to the Golden Age of Dragons.\nHis lie was thin and forced. Something far more powerful than the Devourer was at hand.\nIts eerie persistence gnawed at my mind. It did not just threaten me. It threatened everyone. Everything.\nEven faced with such lies and eminent danger, I simply walked away, and did not return.",
                "曾有一个封闭且没有生命力的传送门被封印在地牢中。主导的邪教徒一直死守着这个秘密。\n在见过神明吞噬者后，他们的领袖急忙把我带到它的房间，向我揭示它的存在。\n这扇门通向吞噬者的家乡。和他的传送门一样，古老而无比稳定。\n巨蛇对它也发表了声明。他声称这也是他的作品。这个传送门能持续如此之久，只是他当时没能纠正的一个错误。\n他的谎话狗屁不通，比他强大得多的东西明明就摆在我的眼前。\n它如怪诞一般永久存在于此，不断啃噬着我的心灵。它不仅仅威胁着我，它还威胁着每一个人，它威胁着世间的一切。\n面对胡言乱语的大话和迫在眉睫的危险，我所做的仅仅只是离开了这个房间，不再回来。");
            QuickTranslate(typeof(LoreCorruption), "get_Lore", "To not properly dispose of the essence of a slain God is a fatal mistake. This wasteland stands as proof of such.\nHaving slain my first Gods, I turned a blind eye as corrupt essence gushed from their rent forms and burrowed into the bowels of Terraria.\nThe mere existence of this putrid place proves that the Gods of old were beyond redemption.",
                "弑神却不妥善处理它遗留的精纯，是一个致命的错误。这片废土就是最好的证据。\n在我第一次杀死神明时，祂的腐化精纯从伤口中涌出，渗透进泰拉瑞亚的深处，而我却假装对此视而不见。\n仅仅是这块腐化之地的存在，就足以证明那些远古的神明根本不配得到救赎。");
            QuickTranslate(typeof(LoreCrabulon), "get_Lore", "Fungus and a sea crab. One sought a host; the other, a new home.\nThese mushrooms possess a disturbing amount of tenacity. Nothing that lays down to die in their domain is left to rest.\nIt is this sort of ghastly, forceful exertion of control over the unwilling that led me down my path.",
                "真菌与海蟹，一个寻求宿主，一个寻求新家。\n这些真菌展现出的坚韧令人不适。被它们选中的一切将死之物都不可能得到安息。\n正是这种对弱者恐怖而暴力控制，驱使我走上了这条道路。");
            QuickTranslate(typeof(LoreCrimson), "get_Lore", "The foul air, the morbid fauna, the disgusting terrain… Here lies my first mistake of my crusade.\nThe essence of a God does not simply vanish when the body dies. It must be properly disposed of or destroyed entirely.\nEssence of a pious God could never fester into a mire as dreadful as this.",
                "污浊的空气，病态的生物，恶心的地形……这是我的军队所犯下的第一个错误。\n神明的精纯不会随着肉体的死亡而直接消失，这些精纯必须被妥善处理或彻底清除。\n一位善良神明的精纯永远不可能溃烂成如此可怕的泥沼。");
            QuickTranslate(typeof(LoreCynosure), "get_Lore", "You now stand at the brink of infinity. The power you have amassed is extraordinary.\nValor and deceit, truth and falsehood, loyalty and betrayal… you are beyond these notions.\nAll, you have rent asunder as they crossed your path. The very land now bends to your will.\nDo you not see how the grass parts where you step, how the stars illuminate where you gaze?\nTerraria itself kneels to you, whether it be out of fear or respect.\nThis is the strength the Dragons held. The primordial power they commanded.\nLittle stands between us now. If you did not seek battle with me, I doubt you would have come so far.\nWhen you are prepared, seek the grave of the Light, at the summit of the Dragon Aerie.\nI await your challenge.",
                "你所立之处，乃无尽之边缘。你所聚之力，为世间无可及。\n勇气与阴险、真实与虚假、忠诚与背叛……你早已超越了这些概念。\n你曾撕碎了前进道路上的一切，而如今，整片大地都将在你面前俯首称臣。\n你曾否留意，路边的青草如何因你的步伐而倾倒；你可否想过，天上的星辰如何因你的注视而闪耀。\n出于恐惧也好，出于敬重也罢，整个泰拉瑞亚都因你而屈膝。\n这就是巨龙的力量，它们所统御的原始力量。\n现在已经没有什么东西阻挡在我们之间了。你所寻求的只剩下与我的一战，若非如此，我无完全法想象还能是什么支撑你走到了这一步。\n你准备好的话，就来巨龙天巢之顶寻找光辉之墓。\n我静候你的挑战。");
            QuickTranslate(typeof(LoreDesertScourge), "get_Lore", "Once, it was a majestic sea serpent that threatened none but the microscopic creatures it consumed.\nAfter Ilmeris was incinerated, it became familiar with the hunt. To survive, it quickly learned to seek greater prey.\nUnfortunately for the scourge, it seems that it too was prey in the end. After all, there is always a bigger fish in the sea.",
                "这条海蛇曾有着十足的尊严，它只依靠吞食微生物为生，与世无争。\n在伊尔梅里斯被焚尽后，它不得不开始主动狩猎。为了生存下去，它开始寻找更大的猎物。\n可惜天外有天，这条灾虫并不知道，其实它自己也是猎物。");
            QuickTranslate(typeof(LoreDestroyer), "get_Lore", "The Godseeker Knights of my company were far and away my finest soldiers.\nThey championed my cause, and I championed them in return.\nI bestowed upon them hulking armor and colossal weaponry, so their Might would never falter.\nSome days, I would take time to train by their side, inspiring them to new heights of righteous fury.\nDraedon understood well, and granted them these massive forms, bristling with weaponry and interlocked armor forged of blessed metal.\nWhile in truth it was repurposed mining equipment, their sheer presence on the battlefield was immense.",
                "在跟随我征战的人中，寻神者骑士无疑是最好的士兵。\n他们捍卫我的事业，我也同样庇护着他们以作回报。\n我赐予他们巨大的盔甲与武器，让他们的“力量”永不动摇。\n嘉登对此的理解十分到位，他使用神佑的金属为他们制造出了这些重型武器与连锁盔甲。\n尽管这些装备本质只是采矿设备的再利用，但使用它们的士兵在战场上披荆斩棘，绝不容小觑。");
            QuickTranslate(typeof(LoreDevourerofGods), "get_Lore", "The infamous, otherworldly glutton, in the flesh. His imposing title was self-granted, but truth made it stick.\nHe is a formidable foe, capable of swallowing Gods whole, absorbing their essence in its entirety.\nI ordered Draedon to armor his gargantuan form, so he could safely best even great Gods in single combat.\nFittingly, he had a serpent's tongue. He manipulated me incessantly, driving me to awful acts.\nI recruited him out of desperation. My war had dragged on for decades, and I would do anything to have it end.\nIt was then my negligence was born. My descent began the moment recruiting this scoundrel crossed my mind.\nHis absence of loyalty was clear as day, even at the time. However, I suspect it goes beyond that.\nThe Devourer's alien capabilities and domineering tactics hint that his allegiance lay elsewhere.\nIs he but one soldier, of a malevolence far beyond…?",
                "臭名昭著的饕餮，来自另一个世界。虽然这个威风的头衔只是他自封的，但他绝对名副其实。\n作为敌人他十分可怕，他能将神明整个吞下，并彻底吸收祂们的精华。\n我让嘉登设计一副与他身躯相称的铠甲，如此一来，就算是那些十分强大的神明在单挑中也不会是他的对手。\n好巧不巧，他巧“蛇”如簧。他不断地诱导我，最终让我走上了不归路。\n我招募他纯粹是出于无奈。这场战争已经持续了数十载，为了让它结束我什么都愿意做。\n那个时候我疏忽了。想来，我的失败就是从招募这无耻之徒开始算起的。\n就算是当时，他也丝毫不隐藏自己的不忠。但我对他的怀疑远不止如此。\n它吞噬万物的异界能力，还有它专横跋扈的战斗方式，无不在暗示我他宣誓效忠的对象另有他人。\n远方恶意送来的士卒...他真的只是如此吗？");
            QuickTranslate(typeof(LoreDragonfolly), "get_Lore", "Near the close of the Draconic Era, there are records of the “impure” Dragon species.\nWyverns, basilisks, Pigrons and the like are documented, though none are sure how exactly they came to be.\nTo this day, scholars argue over the true names and lineages of these creatures.\nNames aside, it is clear the first offshoots are pure enough to retain the great strength of their forebears.\nNaturally, this led them to be targeted by cruel divine mandates, and most were hunted to extinction.\nIt is known that Fishrons, Follies, and the Abyssal Wyrms survived the purging hunts of the Deific Era.\nNotably, they now are all reclusive or exceedingly violent. It is tragic how they evolved to be that way.",
                "在巨龙时代终结的前夕，曾出现了有关“血统不纯”的飞龙亚种的目击报告。\n飞龙、蛇蜥、猪龙之类的生物都有过记载，尽管没有人能肯定它们从何而来。\n时至今日，学者们依然在争论这些生物的真实名称和世系。\n撇开名字不谈，很明显巨龙最初的那些分支足够纯粹，保留了来自它们祖先的巨大力量。\n自然，这让它们成为了那场残忍神授的牺牲品，它们中的大多数都被猎杀到几近灭绝。\n现在世人所知从神攫时代那场净狩中存活下来的，只剩下猪龙鱼、痴愚金龙和幻海妖龙。\n值得注意的是，现在的它们要么隐居起来，与世隔绝；要么变得极端暴力而嗜血。它们最后变成了现在这个样子，真是悲哀。");
            QuickTranslate(typeof(LoreDukeFishron), "get_Lore", "Outlandish as they may seem, this species is the single mightiest of the seas.\nThey are relentless hunters and can easily spend significant time out of the water.\nFolklore holds that the Fishrons claim heritage from the true Dragons, countless years back.\nWhile there are many such tales of creatures of draconic descent, this case is factual.\nGenetic heritage or not, though, the Fishrons lack Dragonblood, or Auric souls. I would well know.",
                "虽然它们看起来很奇怪，但这些物种算得上海洋中唯一的前者。\n这些无情的猎手甚至可以离开水面活动很久。\n民间传说认为，猪龙鱼继承了无数年前那些真龙的遗产。\n尽管像这样关于巨龙后裔的故事数不胜数，猪龙鱼的传说却并非虚构。\n但无论有没有基因上的联系，有一件事我很清楚。猪龙鱼的体内既无龙血，也没有金源魄。");
            QuickTranslate(typeof(LoreEaterofWorlds), "get_Lore", "Any powerful being will call forth fable and legend, of both its grandeur and terror. That monstrous worm was no exception.\nThat measly thing, devouring a planet? Ridiculous. However, ridicule spreads quickly with even an ounce of truth behind it.\nOne will not need to search long for examples. We are all surrounded by rampant superstition and assumption.\nI myself have been subjected to a litany of baseless boasts and accusations in my time.",
                "任何强大的存在都会促生出各种寓言与传说，无论那是为了歌颂宏伟还是警示恐怖。对于那只畸形的虫子而言，也不例外。\n就那么一条小家伙，可以吞噬整个星球？笑死人了。但就算是一个被添油加醋到如此地步的故事，只要它有哪怕一丝捕风捉影的可能性，这个故事就会不胫而走。\n人不会因为一小块浮冰而去调查水面之下的整座冰山。我们所处的世界充斥着愚昧与虚妄。\n就连我自己，在某段时间里也经受过过一连串毫无根据的吹捧和指责。");
            QuickTranslate(typeof(LoreEmpressofLight), "get_Lore", "Though her title is lofty, she is more an emissary for the powers beyond and forces of nature.\nIn broad daylight, she can channel the Primordial Light itself, making her nigh untouchable.\nThankfully, left with only starlight to wield, she falls like any other graceless despot.\nHer penchant for leeching the strength of other great beings is uniquely deplorable.\nIt made her sickeningly obedient. Dependent, but willingly so, as they enabled her to slake her base thirst.\nI had deigned to slay her myself for her treachery, but she was a notoriously evasive mark.",
                "尽管这个名称听起来很崇高，但她更像是一位代表着绝对力量与自然之怒的使者。\n在白天，她甚至可以引导原始之光本身，让她几乎无法为人所触及。\n庆幸的是，如果她所能操控的只剩下星光，那她的本事就和其余那些狼狈的暴君没什么差别。\n她的嗜好十分独特，她喜好吸取其他强大生命的力量，这让我感到十分可悲。\n这让她产生了一种病态般的顺从。尽管依赖于此但她仍心甘情愿，毕竟只有这样她永恒的饥渴才能得到满足。\n我曾因她的变节而决定亲手杀掉她，但她在隐藏踪迹这一方面可谓万里挑一。");
            QuickTranslate(typeof(LoreExoMechs), "get_Lore", "What a terrifying marvel of engineering. Draedon's specialty always lay in the machines of war, but these are immaculate.\nHis bold claim that no God can match his work is, however, incorrect. He is not privy to the Traitor almighty.\nRegardless, from steel and wit alone, he has forged engines of destruction that rival Calamitas.\nIt brings me little comfort to remark that even she, at least, has a heart to speak of.\nDraedon is an amoral monster beyond compare. He is entirely devoid of humanity and compassion.\nWith technology this incomprehensibly advanced, he stands at the precipice of apotheosis.\nHe can fabricate such dreadful, synthetic nightmares at will. His resources must be nigh unlimited.\nWere he to lose his temper, if he even has one, all of life's hopes would be smothered in an instant, silenced by a torrent of silicon.\nThough, perhaps you may leverage his unimaginable craft to your advantage, and seek insight from him.",
                "如此可怕的工程奇迹！我很清楚嘉登最擅长的就是制造战争机器，但这样的机器，就算是我，也挑不出任何毛病。\n他曾鼓吹没有任何神明能够比肩他的造物，可惜，他错了。他并不了解那名全能的背叛者。\n但不管怎么说，他仅凭钢铁和智慧，就锻造出了可与灾厄相媲美的毁灭造物。\n就算是她，至少也有一颗值得演说的心，我很欣慰。\n与她相比，嘉登完全就是一个没有任何道德的衣冠禽兽。人性？同情？你根本不可能从他身上看到丝毫。\n拥有如此先进的技术，他与登神只隔了一扇门。\n能够如愿制造出令人如此惊骇的梦魇，他一定掌握着近乎无穷的资源。\n假如他哪一天发了脾气，（如果“脾气”真的存在于他身上的话），恐怕所有生命的希望都会在瞬间被扼杀，为钢铁的洪流所碾。\n尽管如此，或许你也可以充分发挥他那无人能出其右的工艺优势，或是从他那儿寻求领悟。");
            QuickTranslate(typeof(LoreEyeofCthulhu), "get_Lore", "In ages past, heroes made names for themselves facing such monsters.\nNow they run rampant, spawning from vile influences left unchecked. They blend well with the horrific injustice of their forebears.\nSlaying one merely paves the way for a dozen more. Surely this does not concern you, either.",
                "在过去，英雄的称号，就是在面对这些怪物时打出来的。\n现在，它们从无羁的邪恶中滋生，横行无忌。它们已经完美融入了那些曾令人恐惧的不义之中。\n杀死一只眼只是为更多的眼铺平道路，当然你也不在乎这些。");
            QuickTranslate(typeof(LoreGolem), "get_Lore", "What a sad, piteous thing. Truly, a mockery in every sense of the word.\nThe Lihzahrds were abandoned by their deity long ago. They set upon creating the idol as a replacement.\nThe result is an amalgamation of the concepts and themes of many Gods, most prominently the heat of the sun.\nIt is a far cry from a mechanical god… for the better. The alternative is too chilling to consider.\nWhile I believe it barely deserves mention, the Lihzahrds revere it unflinchingly.\nI see no need to intervene in affairs beneath me and my people.",
                "多么可悲，多么可叹。从各种意义上来说，这都是一种嘲弄。\n蜥蜴的神明很久以前就抛弃了他们，因此，他们开始创造神像作为代替。\n最后的结果是众多神明概念的大杂烩，其中最为突出的还是太阳之炙热。\n与一个机器神明相比它还差得太远.....幸好如此。另一种情形我想都不敢想。\n虽然我认为它几乎不值一提，但蜥蜴们对它的崇拜却却坚定不移。\n远低于我和我人民的事物，我没有干预的必要。");
            QuickTranslate(typeof(LoreHiveMind), "get_Lore", "Some semblance of a God's mind may survive death, like the twitches of a crushed insect.\nWhat little remains attempts to convene, to coalesce in worship, so that its power may yet be restored. How pitiful.\nFortunately for us, the futility of this effort is unmatched. The biomass obeys, but nothing is accomplished.\nFar from all divine power flows from faith. A God is forged of its own strength; followers may choose to worship.",
                "神明思维的一些表征，甚至可以持续到祂死亡之后，宛如一只被碾碎却仍在抽搐的虫子。\n剩下的那些微不足道的事物试图召集彼此，在崇拜中重新凝聚起来，好让祂的力量再次恢复。真是可悲。\n对我们而言这是一种幸运，因为它们的所谓努力只是纯粹的徒劳。这些生物体绝对循规蹈矩，也绝对一事无成。\n远非一切神力都来自信仰。神明的力量铸造其本身，但崇拜哪一位神明则是追随者自己的选择。");
            QuickTranslate(typeof(LoreKingSlime), "get_Lore", "Given time, these gelatinous creatures absorb each other and slowly grow in both size and strength.\nThere is little need to worry about this. Naturally, slimes are nearly mindless and amass only by chance.\nThough it appears they are capable of absorbing knowledge, if only in rudimentary form.",
                "假以时日，这些凝胶状的生物就会互相吸收，让体态与力量得到缓慢增长。\n不过这一点基本不用担心。一般来说，史莱姆不存在意识，它们的聚集只是出于偶然。\n尽管它们似乎有能力吸收那些最基础的知识。");
            QuickTranslate(typeof(LoreLeviathanAnahita), "get_Lore", "Although she claims dominion over all the world's oceans, in truth she is a recluse of the deep.\nElementals pose a grave threat to all those around them. Other Elementals are no exception.\nAnahita was driven from her home in the Abyss by Silva's encroaching greenery.\nAccounts vary as to the majestic beast at her side. Some claim Anahita summoned the Leviathan herself.\nRegardless of what you believe, they are inseparable even in death.\nSuch stalwart loyalty! It reminds me of Yharon.",
                "尽管她声称自己统治了世界上所有的海洋，但她实际只是深海中的一名隐者。\n元素生物会严重威胁她们周围的一切，其他元素也不例外。\n席尔瓦身上蔓延出的绿色植物将阿娜系塔赶出了她在深渊的家。\n关于她身边的雄伟魔物则说法不一，有的人认为是阿娜希塔自己召唤了利维坦。\n但无论你相信哪种说法，无可辩驳的事实是，就算是死亡也不能让她们分开。\n如此忠贞！这让我想起了犽戎。");
            QuickTranslate(typeof(LoreMechs), "get_Lore", "These unwieldy beasts of steel were the experiments of Draedon, my former colleague and prodigious engineer.\nHis intent was to fuel a war machine with soul energy, allowing it to fight with purpose and zeal.\nThe creations were a success; so much so, that the souls continued to express their own free will.\nDraedon was displeased. But these were my soldiers, and their loyalty was forged anew in iron.\nI dismissed them from duty, and yet here they are, scouring the land for evidence of the divine.\nUnfortunately for you, that puts you in their crosshairs. Give them a battle worth dying in.",
                "我的前同僚，一位杰出的工程师，嘉登，制造出了这些笨重的钢铁巨兽作为实验品。\n他意图使用灵魂的能量来驱动这些战争机器，让它们的战斗拥有目标，充满热情。\n这些造物成功了。不仅如此，这些灵魂甚至可以继续展现出他们自己的意志。\n嘉登对此很不高兴。但他们毕竟是我的士兵，他们的忠诚被重新注入进钢铁之中。\n我免去了他们应履的职责。但他们仍不离不弃，在这片大地上寻找神明的蛛丝马迹。\n对你而言这很不幸，因为你会因此成为他们的目标。请为他们献上一场值得让他们献出生命的战斗。");
            QuickTranslate(typeof(LoreOldDuke), "get_Lore", "That was possibly one of the oldest mundane living beings on the face of the planet.\nThe first Fishrons were spotted in the middle of the Draconic Era. What exotic prestige…!\nFishrons were one of the original offshoots of pure-blooded Auric Dragons.\nThey are so old and venerated that many historians are convinced they are the original sea monsters of folklore.\nThis particular Duke's guile is self-evident; it evaded centuries of hunting, and until now had survived a most thorough poisoning.\nAbove almost all others, this creature was a living fable. One must wonder what goes through the mind of a fading legend.",
                "这可能是这颗星球上最古老的生物之一。\n据说早在巨龙时代的中期就已经出现了猪龙鱼的目击报告。多么独特的名誉！\n猪龙鱼是纯血金源巨龙的原始分支，\n它们是如此的古老，如此的受人尊敬，以至于许多历史学家都相信它们就是民间传说中最初的海怪。\n这位特殊公爵的狡诈不言而喻。它不仅躲过了长达几个世纪的猎杀，甚至如今的这场剧毒灾害也没能杀死它。\n这头生物远超一切，它分明就是一个活着的神话。这样一位逝去的传奇，它心中的所思所想，想必是任何人都会感到好奇的。");
            QuickTranslate(typeof(LorePerforators), "get_Lore", "These creatures were unique, for they wielded the slain Gods' power as purely as possible, veins flowing with spilt ichor.\nAll that exists in the Crimson is truly the divine turned inside out; their gore now glistens with its perverse treachery, for all to bear witness.\nThe mire reeks of centuries of vile manipulation and callous domination of the hapless.\nJudgment is long passed, and extinction is left waiting.",
                "这些生物十分独特，体内流淌着脓血的它们只是纯粹地操弄着已死之神的力量。\n事实上，猩红之地存在的一切，都是所谓神性的另一面；它们的尸体上承载着低劣的背弃。这一点，每一个人都得以见证。\n这片泥潭所散发的，只有长达数个世纪的邪恶统治，以及对弱者的冷酷无情。\n审判早已过去，等待的唯有灭亡。");
            QuickTranslate(typeof(LorePlaguebringerGoliath), "get_Lore", "An innocent queen, forced to bear an agonizing existence. This is nothing short of a crime against nature.\nWithout consulting me, Draedon sought to weaponize the already well-organized Jungle bees.\nWhen he revealed his finished project, I was enraged. Enslaving the bees was despicable.\nDraedon cared little for my outrage and returned to his other work without further incident.\nFrom that point on, I stopped making requests of Draedon. He had shown me his true colors.\nIn my later days I was far from virtuous. But I would not shackle a creature to fight in my name.\nThat would make me no better than the divine scoundrels I pursued.",
                "一只无辜的蜂后，被迫成为了苦难的载体，这简直是对自然的亵渎。\n在没有征求我意见的情况下，嘉登试图将早以准备好的丛林蜜蜂武器化。\n当他向我展示他的成果时，我彻底愤怒了。奴役蜜蜂着实无耻。\n从那以后，我不再向嘉登提出任何要求。我已经见到了他的真面目。\n尽管后来我自己也远非一位贤明之人，但我不会给一个生物戴上镣铐，让它以我的名义战斗。\n做出如此恶行的人，和我所追猎的那些披着神圣外皮的畜生相比，好不到哪去。");
            QuickTranslate(typeof(LorePlantera), "get_Lore", "This floral aberration is another example of the volatile power of harnessed souls.\nTaking their mastery of agriculture to new heights, the Jungle settlers bred a special sprout.\nThrough ritual blessing of the soil, it was fed legions of souls.\nElders of the village wished to bring forth a new age of botanical prosperity.\nIndeed, the plant was awe inspiring. But it was wild and untamed, with a will of its own.\nNow that you have slain it, still more disorderly spiritual energies are flooding the lands.\nThe village's ignorance was shameful in its own right, but this is worse still.",
                "这朵反常的花卉，再次展现了试图驾驭灵魂那不稳定的力量会导致什么样的后果。\n丛林定居者的农业水平提升到了新的高度，他们培育出了一种特殊的萌芽。\n通过对土壤的祝福，它被喂养了大量的灵魂。\n村里的长老们希望看到一个植物业高度繁荣的新时代。\n诚然，这株植物令人敬畏且激动人心。但它并未被驯化，它仍保留着野性且拥有自己的意志。\n你现在杀了它，只是让更多的灵魂能量充斥在这片土地上，杂乱无章。\n村庄应为自己的无知感到羞愧，可你的所作所为只是让事情变得更糟。");
            QuickTranslate(typeof(LorePolterghast), "get_Lore", "The further my war dragged on, the further I sank into negligence. This specter is the crux of my failure.\nI hid behind my excuses, calling them duties. Fighting the gods. Training. Ruling.\nI had the time and resources to devote. I was simply paralyzed by apathy.\nThe scores of prisoners I kept in the dungeon I claimed perished alongside their jailors.\nWithin those hexed walls, none may know rest, and their souls coalesced into a formless monster.\nBoiling with rage, wallowing in sorrow, screaming in madness. The amalgamation was uncontrollable.\nThe dragon cult was furious, their leader demanding I put the haunt down myself.\nI did not answer. I had long since become deaf to the world outside my crusade. Do not fall as I have.",
                "战争拖得越久，我越沉沦于疏忽。这个鬼魂便是我失败的症结所在。\n训练士兵，管理城市，讨伐神明，我把它们称作职责，自己却躲在这些借口的背后。\n我本有足够时间和资源投入其中，但我却被冷漠麻痹了双眼。\n被我关押在地牢中的几十名囚犯，与他们的狱卒一同走向了灭亡。\n在那些被施加了魔法的囚牢里，没人能真正得到安息，他们的灵魂凝聚成一个了没有形体的怪物。\n在愤怒中沸腾，在悲伤中沉湎，在疯狂中尖啸。它们的融合失去了控制。\n信奉巨龙的教徒们十分愤怒，他们的领袖要求我亲自清剿那只幽灵。\n我没有回答。我早已对讨伐神明以外的事情充耳不闻。不要像我这样堕落。");
            QuickTranslate(typeof(LorePrelude), "get_Lore", "In ages past, now named the Draconic Era, the majestic Dragons protected Terraria from all threats.\nTheir famed might was put to the ultimate test by an aberrant behemoth from beyond the stars.\nFighting with all their strength, the Dragons could wound and weaken it, but not destroy it.\nLacking options, they tore the monster down to a shadow of its former self, and sealed it away.\nWhat is left of it now lies imprisoned in the Moon, as far away as the Dragons could banish it.\nMuch of dragonkind was lost as casualties in that struggle, and they never recovered.\nZeratros himself was gravely injured. It seemed his power, along with his life, would be lost forever.\nOne mortal, sworn to the service of the Dragons, rose in determination to save their virtuous King.",
                "在遥远的过去，那个如今被我们称作巨龙时代的日子里，雄伟的巨龙们曾保护泰拉瑞亚免受一切威胁。\n那无与伦比之力的最后考验，来自星界之外的可怖巨兽。\n巨龙们竭尽全力进行战斗，尽管他们可以伤害和削弱它，但没法真正摧毁它。\n他们别无选择，便把这个怪物撕成了它曾经的影子，封存了起来。\n如今，它剩下的一切都被囚禁在月球之上，那是巨龙所能将它驱逐到的最远的地方。\n这场战争让巨龙伤亡惨重，它们再也没能恢复过来。\n泽拉托斯本人也受到了严重的伤害。似乎他的力量与生命都将永远消失。\n这时，一个发誓要为巨龙服务的凡人，决心拯救他们贤明的国王。");
            QuickTranslate(typeof(LoreProfanedGuardians), "get_Lore", "The Guardians are rather simple constructs, extensions of the Profaned Goddess' power.\nThey are given partial autonomy to hunt down threats and are rarely seen outside of temples sanctified in her name.\nShe has been attempting to expand her domain, and it is no surprise she sees you as her largest threat.\nAfter all, it was you that finished off the star-spawned horror that catalyzed the downfall of the Dragons.\nDraw her out from hiding. Have no mercy, for the Profaned Goddess shows none herself.",
                "亵渎守护的构造相当简单，它们每一个都是亵渎天神权力的延伸。\n它们被赋予了部分自主权来清理威胁，我们很少能在以她之名神化的庙宇之外见到它们。\n她一直试图扩大她的领域，因此她把你视作她最大的威胁，这并不奇怪。\n毕竟，是你干掉了导致巨龙衰落的群星之惧。\n把她揪出来。不要留情，毕竟亵渎天神也不会对你留情。");
            QuickTranslate(typeof(LoreProvidence), "get_Lore", "A glorious day.\nDeeds of valor of this caliber are enshrined in legend. Of this age, only the Witch, Braelor and myself can compare.\nProvidence was perhaps one of the wickedest Gods, hellbent on purification through erasure.\nHer worshippers saw little value in life. Pain was not a price they felt justified to pay.\nThe Profaned Goddess promised her followers she would end inequality by reducing all to featureless ash.\nThose devoted to her were weak-willed, and yet she reigned as one of the mightiest Gods.\nPerhaps it was their easily-swayed nature, that let her draw so much power from them…",
                "光荣之日。\n如此的英勇事迹足以被载入传奇。在这个时代，只有女巫、布瑞莱尔和我自己可以相比。\n普罗维登斯也许是最邪恶的神之一，她一心只想通过灭绝来净化。\n她的崇拜者漠视生命，痛苦的代价在他们看来完全不值一提。\n亵渎天神向她的信徒允诺，她会创造一个人人平等的世界，在那儿，万物都只是平凡的灰烬。\n献身于她的人无不意志薄弱，但她却作为最强大的神明之一统治于此。\n也许正是这些信徒脆弱的天性，让她从他们身上汲取了如此多的力量......");
            QuickTranslate(typeof(LoreQueenBee), "get_Lore", "While of tremendous size, these creatures are docile until aggravated. Their idyllic demeanor is a rarity nowadays, a thing of beauty.\nIn the past, entire villages would spring up around these grand hives, peacefully harvesting their share of the honey and protecting them from danger.\nThough its death is understandable given the circumstances, I do feel pity for these majestic beings.\nFate was cruel to many of their kind.",
                "尽管这些生物的体型相当巨大，只要不去激怒它们，它们便会表现出相当温顺的一面。这种宛如田园诗般的举止在当下非常罕见的，这无疑是一件值得令人欣慰的事情。\n在过去，整个村庄都是围绕这些巨大的蜂巢而建。村民们保护它们免受危险，并安然收下那些来自蜜蜂的馈赠。\n鉴于当下的情况，我能理解它不得不死亡。但我着实对这些伟大的生命感到惋惜。\n对它们中的大部分而言，命运都太残酷了。");
            QuickTranslate(typeof(LoreQueenSlime), "get_Lore", "Having fled after your battle, it seems the Slime God fashioned a new guardian from the unleashed essences.\nEnsnared in the absorption process of its newfound power, it could not flee again.\nOr, perhaps, it was overcome by arrogance or desperation.\nA glorious hunt, a fine foe. Now you know you must chase them to the ends of Terraria.",
                "在从你们的战斗中捡回一条命之后，史莱姆之神似乎从被释放出来的神圣精华中创造出了一个新的守护者。\n在吸收新发现的这份力量的过程中，它没法再逃一次。\n或者，也许，傲慢或绝望会征服它。\n一次光荣的狩猎，一个优秀的敌人。现在你已经明白，自己必须追猎他们到泰拉瑞亚的尽头。");
            QuickTranslate(typeof(LoreRavager), "get_Lore", "A sickening flesh golem built for the sole purpose of savage, relentless destruction.\nThe monstrosity was a desperate bid to turn the tides against my God-seeking armies.\nI could scarcely believe it myself, but it was born of a ritual of great sacrifice, performed in ardent faith.\nThe ritual condemned and fused the bodies and souls of their fallen allies into this hideous thing.\nWhen the warlocks pledged their very lives to it as an offering, it awoke and swiftly slew them.\nNow caked in fresh blood, it hungered for more, and set off on an aimless rampage.\nI suppose its brutality serves as a reminder to be careful what you believe.",
                "这是一个令人作呕的血肉傀儡，它只为野蛮而无情的破坏而生。\n尽管我自己都无法相信。但它，却是在热诚信仰的拥护下，从一个伟大的牺牲仪式中诞生。\n仪式产生了排异，并将他们死去战友的尸体与灵魂融合进这个恐怖的东西中。\n当术士们把自己的生命作为祭品献给这怪物时，它醒了过来。而它做的第一件事，便是杀死了在场的每一个人。\n它现在浑身沾满了鲜血，但它对鲜血的渴望远不止于此，于是，它开始了漫无目的的狂暴。\n我想，它的残暴时时刻刻都在提醒我们，要小心那些你所相信之物。");
            QuickTranslate(typeof(LoreRequiem), "get_Lore", "As the Light Dragon was fading, the monk visited him. Nearly none understand what transpired that day.\nMost say his passing was eased. The truth? Zeratros' Auric soul was consumed, utterly.\nThe monk stood, wreathed in Primordial Light, and declared themselves Xeroc, the First God.\nWhen a Dragon is laid to rest on the Aerie, its powers are relinquished so they may one day return.\nXeroc renounced their sworn oath and broke the cycle, becoming a traitor without equal.\nWord of the ascension spread quickly. Many attempted to follow suit and claim an Auric soul for themselves.\nNow you know… Good intentions or no, all Gods are sinners. Each and every one complicit in genocide.\nWherever your journey may lead, whether you are with me or against, may fortune favor you.\nFor nothing else will.",
                "光辉巨龙消失的时候，那名僧侣拜访了他。几乎没有人知道那天发生了什么。\n大多数人认为它最后安详地离开了这个世界。可事实是什么呢？泽拉托斯的金源魄被彻底地吞噬了。\n僧侣站在那儿，沐浴着原始之光的温暖，而后，他向世人宣告了自己的名讳——原初之神，克洛希克。\n躺在天巢的巨龙会交出自己的力量，以让它们有一天能回归世间。\n克罗希克背弃了他们的誓言，他打破了这个循环，成为了前无古人后无来者的叛徒。\n原初之神飞升的消息迅速传开。无数人趋之若鹜，意图为自己夺得一个金源魄。\n现在你已经明白了......无谓意图，众神皆罪。每一个神明都是参与了巨龙灭绝的同伙。\n无论你的旅程将走向何方，无论你是支持我还是反对我，愿幸运眷顾你。\n毕竟，仅此而已。");
            QuickTranslate(typeof(LoreSignus), "get_Lore", "An aberration that defies all explanation, borne of the Distortion and revered by the Onyx Kinsmen.\nAlmost all information about this entity is sourced from that enigmatic clan. All else is hearsay.\nIt has been reported to manifest in multiple places at once. Its capacity for deceit and ruthless cunning is peerless.\nStatis' compatriot Braelor dueled me to a standstill. With our blades locked, the ronin lunged for the lethal blow.\nThe Devourer is not one for honor or loyalty. But he sensed weakness. Hesitation. An easy prey.\nThe serpent ensnared both my assailants in a dimensional vortex. He assured me they were as good as dead.\nYet, Statis must have struck a bargain with Signus, as he escaped his banishment unscathed.",
                "诞生自扭曲的异常现象，玛瑙族人所崇拜的对象。\n几乎所有关于这个实体的信息均来自那个神秘的部族。其他的一切都是道听途说。\n据说，它可以同时在多个地方现身。其阴险与狡诈无人可比。\n斯塔提斯的同族布瑞莱尔和我的决斗陷入了僵局，当我们彼此锁住对方的武器时，这个神秘人突然冲向我们，给予了致命的一击。\n吞噬者并不是一个注重荣誉或忠诚的家伙，但它的确察觉到了那一瞬间的弱点。犹豫片刻，便为瓮中捉鳖。\n巨蛇把这两个杀手都困在了一个维度漩涡之中。他向我保证，他们和死了没有区别。\n然而，斯塔提斯一定是和西格纳斯达成了某种协议，不然他不可能毫发无损地逃脱放逐的命运。");
            QuickTranslate(typeof(LoreSkeletron), "get_Lore", "A sorry old man, cursed by an even older cult, caught trespassing on their ancient library.\nThey were once my friends. Their leader is infatuated with Dragons, and dreams of resurrecting one.\nThe very walls of that place are cursed further still. The magic has long since faded, and the soldiers rotted.\nDo not expect to learn much from those tattered tomes. They were penned with misguided zeal.",
                "一位可怜的老人，在侵入一个更古老的邪教图书馆时被抓到，因而被诅咒。\n他们曾经是我的朋友，他们的领袖痴迷于巨龙，并梦想复活其中一条。\n那个地方的墙壁已经被进一步诅咒了。魔法早已消逝，士兵也早以朽烂。\n不要指望从那些破烂的书本里学到什么，写成它们的热情本身就是一个错误。");
            QuickTranslate(typeof(LoreSkeletronPrime), "get_Lore", "So consumed by hatred were some souls, that they pledged they would do anything in my name.\nTheir devotion was unerring. Absolute. No atrocity was beyond them; their vengeance knew no bounds.\nI organized them into shock troops, dreaded for their flamethrowers and incendiaries.\nLeveling places of worship and torching those falsely devout, their expertise lay in unmaking faith with flame.\nDraedon understood well. For them he crafted a visage so grim, it evoked oblivion itself.",
                "有的灵魂被仇恨蒙蔽了双眼，他们以我的名义起誓，会为我做任何事情。\n他们的奉献绝对坚定不移。他们的所为绝非暴行，他们的复仇没有界限。\n我把他们组织为冲击部队，而他们的喷火器和燃烧弹则令人生畏。\n他们将神庙夷为平地，将那些虚假的虔诚者付之一炬。用火焰摧毁信仰是他们的专长。\n嘉登对此的理解十分到位，他为此所创造的外形令人“恐惧”，以至让人想起了遗忘本身。");
            QuickTranslate(typeof(LoreSlimeGod), "get_Lore", "An old clan once revered this thing as a paragon of the balance of nature. Now its purity is sullied by freshly absorbed muck and grime.\nThe gelatinous being neither knows nor cares for the last surviving clansman.\nSuch tragedy is all too common in worship.\nAlas, the Slime God is wise enough to be cowardly, fleeing battles it cannot win when its servants are destroyed.\nPerhaps fortune will favor you if you catch it unaware.",
                "一个古老的部族曾经敬此物为自然界平衡的典范。如今，它的纯洁早以被新吸收的泥土和污垢所玷污。\n这个凝胶生物既不知道也不关心最后幸存的那位族人。\n如此悲剧在崇拜中比比皆是。\n唉，史莱姆之神狡诈得很。当它的随从被摧毁时，它会毫不犹豫地逃离这场无法胜利的战斗。\n如果你能在它不知不觉的时候把它抓住，也许幸运就会眷顾你。");
            QuickTranslate(typeof(LoreStormWeaver), "get_Lore", "This beast, while of lesser stature than the Devourer, is a great danger in its own right.\nThey are clearly of the same species. Even this serpent was known to devour Wyverns whole.\nVery little is known about the realm or space that the Great Devourer hails from.\nEven Draedon and his obsessive research has been unable to discern its true nature.\nThe Weaver slipped through a rift from this place opened by the Devourer, and he has monitored it since.\nIn his mind, the lesser serpent's similar powers could lead it to be too threatening for him to let live.\nHe thinks himself invincible. Little does he know, he has ever stood in a similar position.",
                "这头巨兽尽管体型不如神明吞噬者，但它本身仍是一个巨大的威胁。\n二者显然属于同一物种，甚至这条大蛇也可以吞噬一整条飞龙。\n关于神明吞噬者来自哪个领域或空间，我们知之甚少。\n即使是嘉登和他所痴迷的研究也无法辨别它真正的本质。\n自风暴编织者从神明吞噬者打开的此地的裂缝中溜走后，他就一直在监视着它。\n在他看来，一条拥有与自己类似的力量的小蛇可能会对自己构成严重威胁，因此，他绝不能让它活着。\n他自认自己无敌。可惜他不知道，其实他自己也处在一个同样的位置上。");
            QuickTranslate(typeof(LoreSulphurSea), "get_Lore", "This seaside has never been pleasant, though it has seen far better days.\nIncessant fumes rising from the industry of Azafure inundate the water with caustic ions.\nYet still, the hardy life adapted. No doubt aided by Silva as she burrowed through to the underworld.\nLong considered uninhabitable, its further deterioration led Draedon to designate it as a dumping ground.\nYears of careless mass waste disposal has now left the coast's transformation irreversible.",
                "这片海岸从来只会让人感到压抑，然而它过去也曾有美丽的时光。\n源自阿萨福勒工业的烟雾不断升起，最终使这里的水充满了腐蚀性离子。\n然而，生命仍在顽强地生存于此。毫无疑问，是一度生根发芽直至地狱的席尔瓦在帮助它们。\n一直以来，这里都不被认为是一个适合居住的地方，随着它进一步恶化，嘉登最终将其作为了一个指定的垃圾场。\n此地多年来相当粗暴的大规模垃圾处理，已经让这片海岸的环境发生了不可逆转的变化。");
            QuickTranslate(typeof(LoreTwins), "get_Lore", "Not all of warfare is open combat. Logistics and intelligence are paramount to decisive victory.\nThese machines are my finest scouts and agents, reborn in a form that gives them Sight unrivaled.\nArchers or snipers, spies or assassins. An enemy is only as safe as you allow him to be.\nDraedon understood well that the only fair fight is the one you win.\nHis assistance was infallible, and his calculus cold and cruel.\nNot even the most evasive target stood a chance.",
                "并非每一场的战争都硝烟迷茫，后勤和情报同样是胜利与否的决定性因素。\n这些机器曾是我最好的侦察员和特工，他们重生为了这种具有超凡“视域”的存在。\n弓箭手也好狙击手也好，间谍也好刺客也好。敌人的命运就掌握在你的手里。\n嘉登对此的理解十分到位，只有取胜的战争才是公平的战争。\n他的援助无懈可击，他的计算冷酷无情。\n即便是最狡诈的目标也绝无可能逃脱。");
            QuickTranslate(typeof(LoreUnderworld), "get_Lore", "The hellish reputation the underworld gets is rather a modern thing.\nThe layers of ash choking the previously great cities are still warm.\nThe more domineering of Gods wished for me to champion their cause, and rule their society from here.\nYet, surrounded by magma as it were, Azafure simply burned when their wishes were not met.\nSuch is the unfortunate price of war, though I have no regrets fighting for my people.",
                "地狱被如此称呼的时间，并不算久远。\n令伟大城市黯然失色的灰烬仍留有余温。\n专横跋扈的神明们希望我能拥护祂们的事业，从这里统治他们的社会。\n然而，当祂们的愿望没能得到满足时，被岩浆所包围的阿萨福勒就这样被焚毁了。\n战争的代价就是如此不幸，尽管，我从不后悔为我的人民而战。");
            QuickTranslate(typeof(LoreWallofFlesh), "get_Lore", "To contain the essence of a slain God is no small thing. It is rather a towering, ghastly construct.\nThe Wall was lashed together with foul sinew and fouler magics, forming a rudimentary prison of flesh.\nIt served its purpose: halting the diffusion of undue divine influence.\nWere it not for this alchemical breakthrough, the very world I fought for may have been lost in the carnage I wrought.\nMy methods have since evolved. I need not contain such essences, when they can be devoured.\nMay you channel my valor in combating the resulting outpour of energies.",
                "容纳一位已死神明的精纯绝非易事，这正是这面高墙如此宏伟而可怕的原因。\n恶臭的筋肉与更为肮脏的魔法交织在一起，形成了这个简陋的血肉囚牢。\n至少它达到了它的目的：防止神明精纯的不良影响扩散出去。\n若非这个源自炼金术的巨大成就，我所为之奋斗的这个世界可能早就消失在了我所造成的屠杀之中。\n后来我找到了新的方法。精纯除了被容纳外，还可以被吞噬吸收掉。\n愿你在与这些能量洪流的对抗中引导我的勇气。");
            QuickTranslate(typeof(LoreYharon), "get_Lore", "The return of the Age of Dragons, dashed. Just like that, it is but ashes in the wind…\nYharon was the last of the Auric Dragons. As a phoenix, his domain of power includes rebirth.\nThe Gods thought him culled with the rest of his kind, but he returned as an egg, hidden on the Aerie.\nI was destined to consume his Auric soul when he hatched, and rule forever as God-King.\n\n[c/FCA92B:Destiny is for the weak.]\n\nI rejected their whims, and upended their scheme. I was sentenced to execution for treason.\nTheir meek, ingratiated swine cast both Yharon's egg and I into the magma of Hell.\nThe intense heat hideously scarred me, but birthed Yharon anew. He rose, wreathed in fire, and saved my life.\nFrom that day, our souls were one. He shared with me the tale of Zeratros, and the genocide of his kind.\nI promised him I would have justice. So the war began, Yharon rallying all as a beacon of hope.\nNow, that hope is long withered. I am but a husk of the hero I once was, and this is the ultimate proof.\nYharon may yet return, as he does, but he… he has bade me farewell.",
                "巨龙时代的回归，破灭了。这个念想，如今已化作风中的余烬。\n犽戎是最后一只金源巨龙。作为一只不死之龙，就连重生也是他的能力之一。\n所有神明都以为他和他的同类一样死在了那场屠杀之中，但他以蛋的形式涅槃于此，藏身于天巢。\n我本应在他孵化时吞噬他的金源魄，然后作为神王施行永恒的统治。\n[c/FCA92B:只有弱者才接纳命运。]\n我拒绝像他们那样随波逐流，我颠覆了他们的计划，我因叛国罪被判处死刑。\n那些最为阿谀奉承的佞人把我和犽戎的蛋一同扔进了岩浆中。\n剧烈的高温将我重创，但却重新孕育了犽戎。他身披烈焰，振翅九霄，救下了我的生命。\n从那天起，我们的灵魂合二为一。他与我分享了泽拉托斯的故事，以及诸神对他同类惨无人道的种族灭绝。\n我向他保证，我将为他伸张正义。于是战争开始了，犽戎作为希望的灯塔召集了每一个人。\n而今，希望早已凋亡。我现在只是徒有往日英雄之名的空壳罢了，这正是最终的证据。\n犽戎或许还会再回来，就像他以往那样，但他……他已经和我告了别。");

            //QuickTranslate(typeof(), "", "", "");

            foreach (ILHook hook in ILHooksT) {
                if (hook is not null)
                    hook.Apply();
            }
        }
        public static void Unload() {
            foreach (ILHook hook in ILHooksT) {
                if (hook is not null)
                    hook.Dispose();
            }
            ILHooksT = null;
            if (temp_AuricOreDeath is not null) {
                temp_AuricOreDeath.Dispose();
                temp_AuricOreDeath = null;
            }
        }
        private static void QuickTranslate(Type type, string methodName, string origin, string trans) {
            ILHooksT.Add(new ILHook(
            type.GetCachedMethod(methodName),
            new ILContext.Manipulator(il => {
                var cursor = new ILCursor(il);
                if (!cursor.TryGotoNext(i => i.MatchLdstr(origin)))
                    return;
                cursor.Index++;
                cursor.EmitDelegate<Func<string, string>>((eng) => trans);
            })));
        }

        private delegate void HandleTileEffects_tr(CalamityPlayer calPlayer);
        private static void HandleTileEffects(HandleTileEffects_tr orig, CalamityPlayer calPlayer) {
            Player player = calPlayer.Player;
            int num = ModContent.TileType<AstralOre>();
            int num2 = ModContent.TileType<AuricOre>();
            int num3 = ModContent.TileType<ScoriaOre>();
            int num4 = ModContent.TileType<AbyssKelp>();
            int num5 = 300;
            float num6 = player.noKnockback ? 20f : 40f;
            foreach (Point point in Collision.GetEntityEdgeTiles(player, true, true, true, true)) {
                Tile tile = Main.tile[point];
                if (tile.HasTile && tile.HasUnactuatedTile) {
                    if (tile.TileType == num4) {
                        if (player.velocity.Length() == 0f) {
                            break;
                        }
                        Dust dust = Main.dust[Dust.NewDust(player.Center, 16, 16, DustID.Firefly, 0.23255825f, 10f, 0, new Color(117, 55, 15), 1.5116279f)];
                        dust.noGravity = true;
                        dust.noLight = true;
                        dust.fadeIn = 2.5813954f;
                    }
                    if (tile.TileType == num) {
                        player.AddBuff(ModContent.BuffType<AstralInfectionDebuff>(), 2, true, false);
                    }
                    if (tile.TileType == num3 && !player.fireWalk) {
                        player.AddBuff(67, 2, true, false);
                    } else if (tile.TileType == num2 && !calPlayer.auricSet) {
                        player.RemoveAllGrapplingHooks();
                        AuricOre.Animate = true;
                        Vector2 vector = Vector2.Normalize(player.Center - Terraria.Utils.ToWorldCoordinates(point, 8f, 8f));
                        player.velocity += vector * num6;
                        player.Hurt(PlayerDeathReason.ByCustomReason(player.name + "不配。".zh()), num5, 0, false, false, false, -1);
                        player.AddBuff(144, 300, true, false);
                        SoundStyle soundStyle = new SoundStyle("CalamityMod/Sounds/Custom/ExoMechs/TeslaShoot1", 0);
                        SoundEngine.PlaySound(in soundStyle, default(Vector2?));
                    }
                }
            }
        }

    }
}
