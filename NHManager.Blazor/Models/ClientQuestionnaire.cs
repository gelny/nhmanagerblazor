using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NHManager.Blazor.Resources;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NHManager.Blazor.Models;

public class ClientQuestionnaire : BaseModelObject
{

	[Required]
	public int ClientId { get; set; }

	[ForeignKey("ClientId")]
	[DeleteBehavior(DeleteBehavior.NoAction)]
	public Client? Client { get; set; }

	[Required]
	public DateTime Date { get; set; }

	public bool Completed_1 { get; set; } = false; // osobni cile
	public bool Completed_2 { get; set; } = false; // existujici diagnozy
	public bool Completed_3 { get; set; } = false; // rizikove faktory
	public bool Completed_4 { get; set; } = false; // rodinna anamneza
	public bool Completed_5 { get; set; } = false; // chutove preference
	public bool Completed_6 { get; set; } = false; // zivotni styl
	public bool Completed_7 { get; set; } = false; // lymfaticky system

	// Mým cílem je redukce hmotnosti
	[Display(Name = nameof(Questions.Q_1_1), ResourceType = typeof(Questions))]
	public int Q_1_1 { get; set; } = 0;
	// Mým cílem je přibírání hmotnosti
	[Display(Name = nameof(Questions.Q_1_2), ResourceType = typeof(Questions))]
	public int Q_1_2 { get; set; } = 0;
	// Mým cílem je udržování hmotnosti (nechci ani redukovat, ani přibrat)
	[Display(Name = nameof(Questions.Q_1_3), ResourceType = typeof(Questions))]
	public int Q_1_3 { get; set; } = 0;
	// O kolik kg chcete zhubnout (přibrat)? [kg]
	[Display(Name = nameof(Questions.Q_1_4), ResourceType = typeof(Questions))]
	public int Q_1_4 { get; set; } = 0;
	// Mým cílem je zhubnout hlavně rychle
	[Display(Name = nameof(Questions.Q_1_5), ResourceType = typeof(Questions))]
	public int Q_1_5 { get; set; } = 0;
	// Mým cílem je zhubnout pomaleji, ale zaměřit se na udržitelný výsledek
	[Display(Name = nameof(Questions.Q_1_6), ResourceType = typeof(Questions))]
	public int Q_1_6 { get; set; } = 0;
	// Mým cílem je zbavit se problémů doprovázejících prediabetes nebo diabetes 2. typu
	[Display(Name = nameof(Questions.Q_1_7), ResourceType = typeof(Questions))]
	public int Q_1_7 { get; set; } = 0;
	// Mým cílem je zbavit se problémů doprovázejících diabetes 1. typu
	[Display(Name = nameof(Questions.Q_1_8), ResourceType = typeof(Questions))]
	public int Q_1_8 { get; set; } = 0;
	// Mým cílem je snížit množství užívaných léků
	[Display(Name = nameof(Questions.Q_1_9), ResourceType = typeof(Questions))]
	public int Q_1_9 { get; set; } = 0;
	// Mým cílem je zdravě se stravovat
	[Display(Name = nameof(Questions.Q_1_10), ResourceType = typeof(Questions))]
	public int Q_1_10 { get; set; } = 0;
	// Mým cílem je zlepšit svou fyzickou kondici
	[Display(Name = nameof(Questions.Q_1_11), ResourceType = typeof(Questions))]
	public int Q_1_11 { get; set; } = 0;
	// Mým cílem je líbit se sám/sama sobě
	[Display(Name = nameof(Questions.Q_1_12), ResourceType = typeof(Questions))]
	public int Q_1_12 { get; set; } = 0;
	// Co mi ve splnění cíle nejvíce pomůže: (např. pomůže mi chystat si obědy doma)
	[Display(Name = nameof(Questions.Q_1_13), ResourceType = typeof(Questions))]
	public string? Q_1_13 { get; set; }
	// Co mi může splnění cíle zhatit: (např. sladkosti doma ve skříni)
	[Display(Name = nameof(Questions.Q_1_14), ResourceType = typeof(Questions))]
	public string? Q_1_14 { get; set; }
	// Co udělám jako první: (např. nakoupím si zdravé potraviny)
	[Display(Name = nameof(Questions.Q_1_15), ResourceType = typeof(Questions))]
	public string? Q_1_15 { get; set; }

	// Máte diagnostikovanou cukrovku 1. typu?
	[Display(Name = nameof(Questions.Q_2_1), ResourceType = typeof(Questions))]
	public int Q_2_1 { get; set; } = 0;
	// Máte diagnostikovaný prediabetes? (náběh na cukrovku)
	[Display(Name = nameof(Questions.Q_2_2), ResourceType = typeof(Questions))]
	public int Q_2_2 { get; set; } = 0;
	// Máte zvýšenou hladinu krevního cukru? (nad 5,6 mmol/l)
	[Display(Name = nameof(Questions.Q_2_3), ResourceType = typeof(Questions))]
	public int Q_2_3 { get; set; } = 0;
	// Máte diagnostikovanou cukrovku 2. typu?
	[Display(Name = nameof(Questions.Q_2_4), ResourceType = typeof(Questions))]
	public int Q_2_4 { get; set; } = 0;
	// Pokud máte diagnostikovanou cukrovku 2. typu, jakým způsobem ji léčíte?
	// POUZE DIETA;  DIETA A LÉKY;  DIETA A INZULÍN
	[Display(Name = nameof(Questions.Q_2_5), ResourceType = typeof(Questions))]
	public int Q_2_5 { get; set; } = 0;
	// Berete léky na ovlivnění tuků v krvi? (statiny – např. léky na cholesterol, fibráty, jedná se např. Lipanthyl, Atoris, Rosucard, Ezetrol)
	[Display(Name = nameof(Questions.Q_2_6), ResourceType = typeof(Questions))]
	public int Q_2_6 { get; set; } = 0;
	// Máte zvýšený LDL (“špatný”) cholesterol (rovno nebo vyšší jak 3 mmol/l), a/nebo snížený HDL (“hodný”) cholesterol (méně než 1,2 mmol/l u žen, resp. 1 u mužů)?
	[Display(Name = nameof(Questions.Q_2_7), ResourceType = typeof(Questions))]
	public int Q_2_7 { get; set; } = 0;
	// Máte zvýšené triacylglyceroly v krvi?  (hodnota vyšší nebo rovna 1,7 mmol/l)
	[Display(Name = nameof(Questions.Q_2_8), ResourceType = typeof(Questions))]
	public int Q_2_8 { get; set; } = 0;
	// Užíváte léky na vysoký krevní tlak?
	[Display(Name = nameof(Questions.Q_2_9), ResourceType = typeof(Questions))]
	public int Q_2_9 { get; set; } = 0;
	// Prodělal/a jste některé z následujících onemocnění – mozková mrtvice, ischemická choroba srdeční (angina pectoris) nebo infarkt myokardu, ischemická choroba dolních končetin?
	[Display(Name = nameof(Questions.Q_2_10), ResourceType = typeof(Questions))]
	public int Q_2_10 { get; set; } = 0;
	// Prodělal/a jste transplantaci a nyní užíváte imunosupresiva (léky na potlačení imunity)?
	[Display(Name = nameof(Questions.Q_2_11), ResourceType = typeof(Questions))]
	public int Q_2_11 { get; set; } = 0;
	// Prodělal/a jste transplantaci ledvin?
	[Display(Name = nameof(Questions.Q_2_12), ResourceType = typeof(Questions))]
	public int Q_2_12 { get; set; } = 0;
	// Selhávají Vám ledviny? 
	[Display(Name = nameof(Questions.Q_2_13), ResourceType = typeof(Questions))]
	public int Q_2_13 { get; set; } = 0;
	// Selhává Vám srdce?
	[Display(Name = nameof(Questions.Q_2_14), ResourceType = typeof(Questions))]
	public int Q_2_14 { get; set; } = 0;
	// Chodíte na dialýzu ledvin?
	[Display(Name = nameof(Questions.Q_2_15), ResourceType = typeof(Questions))]
	public int Q_2_15 { get; set; } = 0;
	// Proděláváte nebo jste prodělal/a onkologické onemocněnía jste nyní v remisi? (ukončená léčba, pravidelné kontroly)
	[Display(Name = nameof(Questions.Q_2_16), ResourceType = typeof(Questions))]
	public int Q_2_16 { get; set; } = 0;
	// Měla jste někdy diagnostikované nádorové onemocnění prsu, vaječníku nebo vejcovodu?
	[Display(Name = nameof(Questions.Q_2_17), ResourceType = typeof(Questions))]
	public int Q_2_17 { get; set; } = 0;
	// Máte diagnostikovanou dnu nebo zvýšenou hladinu kyseliny močové?
	// ANO DNU;  ANO KYSELINU MOČOVOU; NE HODNOTU NEVÍM
	[Display(Name = nameof(Questions.Q_2_18), ResourceType = typeof(Questions))]
	public int Q_2_18 { get; set; } = 0;
	// V případě, že máte diagnostikovanou dnu nebo zvýšenou hladinu kyseliny močové, užíváte léky na její snížení?
	[Display(Name = nameof(Questions.Q_2_19), ResourceType = typeof(Questions))]
	public int Q_2_19 { get; set; } = 0;
	// Máte diagnostikované onemocnění štítné žlázy? 
	[Display(Name = nameof(Questions.Q_2_20), ResourceType = typeof(Questions))]
	public int Q_2_20 { get; set; } = 0;
	// Máte diagnostikovanou sníženou funkci štítné žlázy (hypofunkci)? (užíváte léky Letrox, Euthyrox)
	[Display(Name = nameof(Questions.Q_2_21), ResourceType = typeof(Questions))]
	public int Q_2_21 { get; set; } = 0;
	// Pokud máte diagnostikované jiné onemocnění štítné žlázy, popište, o jaké onemocnění jde:
	[Display(Name = nameof(Questions.Q_2_22), ResourceType = typeof(Questions))]
	public string? Q_2_22 { get; set; }
	// Máte zvýšené jaterní testy?  (hodnoty jaterních enzymů ALT, AST, ALP a/nebo GGT)
	[Display(Name = nameof(Questions.Q_2_23), ResourceType = typeof(Questions))]
	public int Q_2_23 { get; set; } = 0;
	// Trápí Vás žlučníkové problémy, nebo máte žlučník vyoperovaný? (akutní zánět žlučníku, žlučníkové kameny, vyoperovaný? (akutní zánět žlučníku, žlučníkové kameny, žlučová kolika) 
	[Display(Name = nameof(Questions.Q_2_24), ResourceType = typeof(Questions))]
	public int Q_2_24 { get; set; } = 0;
	// Užíváte léky na ředění krve? (například warfarin, anopyrin, godasal)
	[Display(Name = nameof(Questions.Q_2_25), ResourceType = typeof(Questions))]
	public int Q_2_25 { get; set; } = 0;
	// V případě, že užíváte léky na ředění krve, užíváte konkrétně warfarin?
	[Display(Name = nameof(Questions.Q_2_26), ResourceType = typeof(Questions))]
	public int Q_2_26 { get; set; } = 0;
	// Trpíte Crohnovou chorobou?
	[Display(Name = nameof(Questions.Q_2_27), ResourceType = typeof(Questions))]
	public int Q_2_27 { get; set; } = 0;
	// Trpíte ulcerózní kolitidou?
	[Display(Name = nameof(Questions.Q_2_28), ResourceType = typeof(Questions))]
	public int Q_2_28 { get; set; } = 0;
	// Trpíte bolestí kloubů?
	[Display(Name = nameof(Questions.Q_2_29), ResourceType = typeof(Questions))]
	public int Q_2_29 { get; set; } = 0;
	// Máte diagnostikovanou refluxní chorobu jícnu nebo trpíte pyrózou (pálením žáhy)?
	[Display(Name = nameof(Questions.Q_2_30), ResourceType = typeof(Questions))]
	public int Q_2_30 { get; set; } = 0;
	// Máte nějaké kožní problémy?
	// ANO, AKNÉ;  ANO, ALE JINÉ; NE
	[Display(Name = nameof(Questions.Q_2_31), ResourceType = typeof(Questions))]
	public int Q_2_31 { get; set; } = 0;
	// Pokud máte jiné kožní problémy, prosím, popište je:
	[Display(Name = nameof(Questions.Q_2_32), ResourceType = typeof(Questions))]
	public string? Q_2_32 { get; set; }
	// Užíváte nějaké léky?
	[Display(Name = nameof(Questions.Q_2_33), ResourceType = typeof(Questions))]
	public int Q_2_33 { get; set; } = 0;
	// Pokud užíváte jakékoliv léky, prosím, vypište jejich celý název, dávkování a dobu po kterou je užíváte: 
	[Display(Name = nameof(Questions.Q_2_34), ResourceType = typeof(Questions))]
	public string? Q_2_34 { get; set; }
	// Pokud užíváté léky, obsahuje některý z těchto léků účinnou látku methotrexát? (např. léky při nádorových onemocněních, psoriáze nebo revmatoidní artritidě,  např. Aldesta, Methotrexat, Metoject, Injexate, Nordimet, Trexan)                                                                                                                                                                                                   
	[Display(Name = nameof(Questions.Q_2_35), ResourceType = typeof(Questions))]
	public int Q_2_35 { get; set; } = 0;
	// Užíváte nějaké doplňky stravy?
	[Display(Name = nameof(Questions.Q_2_36), ResourceType = typeof(Questions))]
	public int Q_2_36 { get; set; } = 0;
	// Pokud užíváte jakékoliv doplňky stravy, prosím, vypište jejich celý název, dávkování a dobu po kterou je užíváte:
	[Display(Name = nameof(Questions.Q_2_37), ResourceType = typeof(Questions))]
	public string? Q_2_37 { get; set; }
	// Máte od lékaře doporučenou nějakou speciální dietu (například bezezbytková, nízkobílkovinová,...)?
	[Display(Name = nameof(Questions.Q_2_38), ResourceType = typeof(Questions))]
	public int Q_2_38 { get; set; } = 0;
	// Pokud máte od lékaře doporučenou dietu, napište, prosím, jakou dietu Vám lékař doporučil a v čem spočívá?
	[Display(Name = nameof(Questions.Q_2_39), ResourceType = typeof(Questions))]
	public string? Q_2_39 { get; set; }
	// Pokud trpíte ještě i jinými onemocněními, která nebyla v dotazníku zmíněna, prosím, vypište je:
	[Display(Name = nameof(Questions.Q_2_40), ResourceType = typeof(Questions))]
	public string? Q_2_40 { get; set; }

	// Trpíte častou únavou? (v průběhu dne, ráno po probuzení)
	[Display(Name = nameof(Questions.Q_3_1), ResourceType = typeof(Questions))]
	public int Q_3_1 { get; set; } = 0;
	// Trpíte nespavostí či nekvalitním spánkem? (špatné usínání, večerní probouzení, pocit nevyspalosti ráno)
	[Display(Name = nameof(Questions.Q_3_2), ResourceType = typeof(Questions))]
	public int Q_3_2 { get; set; } = 0;
	// V kolik hodin nejčastěji chodíte spát?
	[Display(Name = nameof(Questions.Q_3_3), ResourceType = typeof(Questions))]
	public string? Q_3_3 { get; set; }
	// V kolik hodin nejčastěji vstáváte?
	[Display(Name = nameof(Questions.Q_3_4), ResourceType = typeof(Questions))]
	public string? Q_3_4 { get; set; }
	// Trpíte průjmy? (normální stolice je nejen pravidelná, ale i pevná a zároveň lehce vytlačitelná, pokud máte denně 3 a více vodnatých stolic hovoříme o průjmu)
	[Display(Name = nameof(Questions.Q_3_5), ResourceType = typeof(Questions))]
	public int Q_3_5 { get; set; } = 0;
	// Trpíte zácpou? (normální stolice je nejen pravidelná, ale i pevná a zároveň lehce vytlačitelná, pokud stolice nejde dobře vytlačit hovoříme o zácpě)
	[Display(Name = nameof(Questions.Q_3_6), ResourceType = typeof(Questions))]
	public int Q_3_6 { get; set; } = 0;
	// Trpíte jinými zažívacími problémy? (pálení žáhy, bolesti břicha)
	[Display(Name = nameof(Questions.Q_3_7), ResourceType = typeof(Questions))]
	public int Q_3_7 { get; set; } = 0;
	// Užíváte hormonální antikoncepci? (pokud ano, napište, prosím, její název a jak dlouho ji užíváte)
	[Display(Name = nameof(Questions.Q_3_8), ResourceType = typeof(Questions))]
	public string? Q_3_8 { get; set; }
	// Jste těhotná / v šestinedělí?
	[Display(Name = nameof(Questions.Q_3_9), ResourceType = typeof(Questions))]
	public int Q_3_9 { get; set; } = 0;
	// Kojíte?
	[Display(Name = nameof(Questions.Q_3_10), ResourceType = typeof(Questions))]
	public int Q_3_10 { get; set; } = 0;
	// Trpíte premenstruačním syndromem? (např. změny nálad, deprese, nafouknuté břicho, nesoustředěnost či bolest hlavy v období mezi ovulací a menstruací)
	[Display(Name = nameof(Questions.Q_3_11), ResourceType = typeof(Questions))]
	public int Q_3_11 { get; set; } = 0;
	// Byla jste někdy těhotná?
	[Display(Name = nameof(Questions.Q_3_12), ResourceType = typeof(Questions))]
	public int Q_3_12 { get; set; } = 0;
	// Máte/měla jste těhotenskou cukrovku?
	[Display(Name = nameof(Questions.Q_3_13), ResourceType = typeof(Questions))]
	public int Q_3_13 { get; set; } = 0;
	// Plánujete v následujících dvou letech těhotenství?
	[Display(Name = nameof(Questions.Q_3_14), ResourceType = typeof(Questions))]
	public int Q_3_14 { get; set; } = 0;
	// Užíváte hormonální substituci? (např. hormony při menopauze)
	[Display(Name = nameof(Questions.Q_3_15), ResourceType = typeof(Questions))]
	public int Q_3_15 { get; set; } = 0;
	// Trpíte návaly nebo jinými příznaky menopauzy?
	[Display(Name = nameof(Questions.Q_3_16), ResourceType = typeof(Questions))]
	public int Q_3_16 { get; set; } = 0;
	// Létáte často letadlem a/nebo máte dlouhodobé znehybnění? (sádrový obvaz, upoutání na lůžko)
	[Display(Name = nameof(Questions.Q_3_17), ResourceType = typeof(Questions))]
	public int Q_3_17 { get; set; } = 0;
	// Kouříte?
	[Display(Name = nameof(Questions.Q_3_18), ResourceType = typeof(Questions))]
	public int Q_3_18 { get; set; } = 0;

	// Měli/mají Vaši rodiče, sourozenci nebo prarodiče cukrovku 2. typu?
	[Display(Name = nameof(Questions.Q_4_1), ResourceType = typeof(Questions))]
	public int Q_4_1 { get; set; } = 0;
	// Měli/mají Vaši rodiče, sourozenci nebo prarodiče infarkt / mozkovou mrtvici?
	[Display(Name = nameof(Questions.Q_4_2), ResourceType = typeof(Questions))]
	public int Q_4_2 { get; set; } = 0;
	// Měli/mají Vaši rodiče, sourozenci nebo prarodiče vysoký cholesterol?
	[Display(Name = nameof(Questions.Q_4_3), ResourceType = typeof(Questions))]
	public int Q_4_3 { get; set; } = 0;
	// Měli/mají Vaši rodiče, sourozenci nebo prarodiče nadváhu nebo obezitu?
	[Display(Name = nameof(Questions.Q_4_4), ResourceType = typeof(Questions))]
	public int Q_4_4 { get; set; } = 0;
	// Měli/mají Vaši rodiče, sourozenci nebo prarodiče nádorové onemocnění?
	[Display(Name = nameof(Questions.Q_4_5), ResourceType = typeof(Questions))]
	public int Q_4_5 { get; set; } = 0;
	// Měli/mají Vaši rodiče, sourozenci nebo prarodiče nádorové onemocnění prsu/ vaječníků/vejcovodu?
	[Display(Name = nameof(Questions.Q_4_6), ResourceType = typeof(Questions))]
	public int Q_4_6 { get; set; } = 0;
	// Měli/mají Vaši rodiče, sourozenci nebo prarodiče nádorové onemocnění prsu u muže?
	[Display(Name = nameof(Questions.Q_4_7), ResourceType = typeof(Questions))]
	public int Q_4_7 { get; set; } = 0;
	// Byly v rodině častější potraty? (spontánní, nikoliv plánovaná interrupce)
	[Display(Name = nameof(Questions.Q_4_8), ResourceType = typeof(Questions))]
	public int Q_4_8 { get; set; } = 0;
	// Byly v rodině předčasné porody? (spontánní, nikoliv plánované lékařem)
	[Display(Name = nameof(Questions.Q_4_9), ResourceType = typeof(Questions))]
	public int Q_4_9 { get; set; } = 0;
	// Měli/mají Vaši rodiče, sourozenci nebo prarodiče tromboembolickou chorobu?
	[Display(Name = nameof(Questions.Q_4_10), ResourceType = typeof(Questions))]
	public int Q_4_10 { get; set; } = 0;
	// Léčí se někdo ve Vaší rodině na onemocnění srdce a cév? (onemocněním se myslí i vysoký krevní tlak a cholesterol)
	[Display(Name = nameof(Questions.Q_4_11), ResourceType = typeof(Questions))]
	public int Q_4_11 { get; set; } = 0;
	// Vyskytuje/vyskytovalo se u Vašich rodičů, sourozenců nebo prarodiče ucpávání cév dolních končetin?
	[Display(Name = nameof(Questions.Q_4_12), ResourceType = typeof(Questions))]
	public int Q_4_12 { get; set; } = 0;

	// Máte diagnostikovanou intoleranci na laktózu (mléčný cukr) nebo alergii na mléko? (ano odpovídejte pouze v případě, že je intolerance/alergie lékařsky diagnostikována)
	// NE;ANO INTOLERANCI;ANO, ALERGII ;ANO, ALE NEVÍM JESTLI INTOLERANCI NEBO ALERGII  
	[Display(Name = nameof(Questions.Q_5_1), ResourceType = typeof(Questions))]
	public int Q_5_1 { get; set; } = 0;
	// Bezprostředně po konzumaci potravin s mlékem nebo mléka máte zažívací problémy?
	[Display(Name = nameof(Questions.Q_5_2), ResourceType = typeof(Questions))]
	public int Q_5_2 { get; set; } = 0;
	// Vyskytuje se laktózová intolerance ve Vaší rodině? (rodiče, prarodiče, sourozenci)
	[Display(Name = nameof(Questions.Q_5_3), ResourceType = typeof(Questions))]
	public int Q_5_3 { get; set; } = 0;
	// Máte diagnostikovanou intoleranci nebo alergii na lepek? (ano odpovídejte pouze v případě, že je alergie lékařsky diagnostikována)
	[Display(Name = nameof(Questions.Q_5_4), ResourceType = typeof(Questions))]
	public int Q_5_4 { get; set; } = 0;
	// Bezprostředně po konzumaci potravin s lepkem máte zažívací problémy?
	[Display(Name = nameof(Questions.Q_5_5), ResourceType = typeof(Questions))]
	public int Q_5_5 { get; set; } = 0;
	// Vyskytuje se celiakie ve Vaší rodině? (rodiče, prarodiče, sourozenci)
	[Display(Name = nameof(Questions.Q_5_6), ResourceType = typeof(Questions))]
	public int Q_5_6 { get; set; } = 0;
	// Máte diagnostikovanou histaminovou intoleranci?
	[Display(Name = nameof(Questions.Q_5_7), ResourceType = typeof(Questions))]
	public int Q_5_7 { get; set; } = 0;
	// Pokud máte intoleranci na jiné potraviny, vypište je, prosím:
	[Display(Name = nameof(Questions.Q_5_8), ResourceType = typeof(Questions))]
	public string? Q_5_8 { get; set; }
	// Pokud máte alergii na jiné potraviny, vypište je, prosím:
	[Display(Name = nameof(Questions.Q_5_9), ResourceType = typeof(Questions))]
	public string? Q_5_9 { get; set; }
	// Jste vegan/ka?
	[Display(Name = nameof(Questions.Q_5_10), ResourceType = typeof(Questions))]
	public int Q_5_10 { get; set; } = 0;
	// Jste vegetarián/ka?
	[Display(Name = nameof(Questions.Q_5_11), ResourceType = typeof(Questions))]
	public int Q_5_11 { get; set; } = 0;
	// Jíte běžně minimálně  4x týdně maso?
	[Display(Name = nameof(Questions.Q_5_12), ResourceType = typeof(Questions))]
	public int Q_5_12 { get; set; } = 0;
	// Jíte ryby?
	// ANO; NE; ZŘIDKA
	[Display(Name = nameof(Questions.Q_5_13), ResourceType = typeof(Questions))]
	public int Q_5_13 { get; set; } = 0;
	// Jíte vejce a jídla z nich (např. pomazánku) obvykle více než 1x týdně?
	[Display(Name = nameof(Questions.Q_5_14), ResourceType = typeof(Questions))]
	public int Q_5_14 { get; set; } = 0;
	// Konzumujete mléko, mléčné výrobky (zakysané mléčné výrobky, jogurty, tvarohy,  smetany, sýry) a jídla z nich (např. smetanové omáčky) více než 3x týdně?
	[Display(Name = nameof(Questions.Q_5_15), ResourceType = typeof(Questions))]
	public int Q_5_15 { get; set; } = 0;
	// Máte rád/a pálivá (ostrá) jídla?
	[Display(Name = nameof(Questions.Q_5_16), ResourceType = typeof(Questions))]
	public int Q_5_16 { get; set; } = 0;
	// Míváte nepřekonatelnou chuť na sladké?
	// ANO; NE; NEVADÍ MI
	[Display(Name = nameof(Questions.Q_5_17), ResourceType = typeof(Questions))]
	public int Q_5_17 { get; set; } = 0;
	// Vypište potraviny, které nejíte/nemají být v jídelníčku zařazené:
	[Display(Name = nameof(Questions.Q_5_18), ResourceType = typeof(Questions))]
	public string? Q_5_18 { get; set; }


	// SNÍDANĚ: (například “1 silný plátek chleba šumava s tenkou vrstvou másla a plátkem 30% eidamu, 3 kolečka okurky; káva s polotučným mlékem a 1 lžičkou cukru", "nic")
	[Display(Name = nameof(Questions.Q_6_1), ResourceType = typeof(Questions))]
	public string? Q_6_1 { get; set; }
	// DOPOLEDNÍ SVAČINA: (například "1 velké jablko a 3 kostičky mléčné čokolády", "nic")
	[Display(Name = nameof(Questions.Q_6_2), ResourceType = typeof(Questions))]
	public string? Q_6_2 { get; set; }
	// OBĚD: (například "čtvrtka pečeného kuřete s kůží, výpek, 2 střední brambory přelité máslem a po obědě 1 malý větrník", "nic")
	[Display(Name = nameof(Questions.Q_6_3), ResourceType = typeof(Questions))]
	public string? Q_6_3 { get; set; }
	// ODPOLEDNÍ SVAČINA: (například "150g kelímek bílého smetanového jogurt, 1 polévková lžíce marmelády; Latté", "nic")
	[Display(Name = nameof(Questions.Q_6_4), ResourceType = typeof(Questions))]
	public string? Q_6_4 { get; set; }
	// VEČEŘE: (například "1 silný krajíc žitného chleba s tvarohovou pomazánka s pažitkou, 1 vařené vajíčko, půl červené papriky", "nic")
	[Display(Name = nameof(Questions.Q_6_5), ResourceType = typeof(Questions))]
	public string? Q_6_5 { get; set; }
	// II. VEČEŘE: (například "půl bločku sýra a 2 dcl červeného vína", "nic")
	[Display(Name = nameof(Questions.Q_6_6), ResourceType = typeof(Questions))]
	public string? Q_6_6 { get; set; }
	// Při současném způsobu stravování pociťujete často hlad?
	[Display(Name = nameof(Questions.Q_6_7), ResourceType = typeof(Questions))]
	public int Q_6_7 { get; set; } = 0;


	//// Jaké denní porce chcete mít v jídelníčku zařazené?
	//// SNÍDANĚ; DOPOLEDNÍ SVAČINA; OBĚD ; ODPOLEDNÍ SVAČINA; VEČEŘE ;II. VEČEŘE
	//[Display(Name = nameof(Questions.Q_6_8), ResourceType = typeof(Questions))]
	//public int Q_6_8 { get; set; } = 0;



	// Jaké množství nealkoholických nápojů denně vypijete:
	[Display(Name = nameof(Questions.Q_6_9), ResourceType = typeof(Questions))]
	public string? Q_6_9 { get; set; }
	// Napište, prosím, jaké nápoje nejčastěji pijete:  (např. neperlivá voda, ochucené minerálky, džus, coca-cola, čaj)
	[Display(Name = nameof(Questions.Q_6_10), ResourceType = typeof(Questions))]
	public string? Q_6_10 { get; set; }
	// Pijete kávu?
	[Display(Name = nameof(Questions.Q_6_11), ResourceType = typeof(Questions))]
	public int Q_6_11 { get; set; } = 0;
	// Jak často pijete kávu?
	// NEPRAVIDELNĚ ; 1 x DENNĚ; 2 - 3 x DENNĚ ; MINIMÁLNĚ 4 x DENNĚ
	[Display(Name = nameof(Questions.Q_6_12), ResourceType = typeof(Questions))]
	public int Q_6_12 { get; set; } = 0;
	// Pokud pijete kávu, popište, prosím, jakou kávu pijete: (např. s 1 lžičkou cukru, s plnotučným mlékem, espresso, turek)
	[Display(Name = nameof(Questions.Q_6_13), ResourceType = typeof(Questions))]
	public string? Q_6_13 { get; set; }
	// Doslazujete často nápoje a pokrmy cukrem (min. 3x týdně)?
	[Display(Name = nameof(Questions.Q_6_14), ResourceType = typeof(Questions))]
	public int Q_6_14 { get; set; } = 0;
	// Pijete minimálně 1x týdně sladké a kalorické nealkoholické nápoje? (coca-cola, náhražky coca-coly, džusy,sladké minerální vody, ledové čaje)
	[Display(Name = nameof(Questions.Q_6_15), ResourceType = typeof(Questions))]
	public int Q_6_15 { get; set; } = 0;
	// Konzumujete více než 2x týdně alkoholické nápoje ? (pivo, víno, míchané drinky, tvrdý alkohol)
	[Display(Name = nameof(Questions.Q_6_16), ResourceType = typeof(Questions))]
	public int Q_6_16 { get; set; } = 0;
	// Máte sedavé zaměstnání? (pokud máte zaměstnání, kde se střídá sezení a pohyb, vybírejte podle častější převahy)
	[Display(Name = nameof(Questions.Q_6_17), ResourceType = typeof(Questions))]
	public int Q_6_17 { get; set; } = 0;
	// Trávíte často delší dobu v autě na cestách? (často znamená několikrát týdně)
	[Display(Name = nameof(Questions.Q_6_18), ResourceType = typeof(Questions))]
	public int Q_6_18 { get; set; } = 0;
	// Pracujete na směny?
	[Display(Name = nameof(Questions.Q_6_19), ResourceType = typeof(Questions))]
	public int Q_6_19 { get; set; } = 0;
	// Pokud ano, vyberte způsob střídání směn:
	// STŘÍDÁNÍ DENNÍ X NOČNÍ; POUZE NOČNÍ ; TŘÍSMĚNNÝ PROVOZ; JINÉ
	[Display(Name = nameof(Questions.Q_6_20), ResourceType = typeof(Questions))]
	public int Q_6_20 { get; set; } = 0;
	// Stíháte v zaměstnání pravidelně jíst? (v případě potřeby se můžete najíst)
	[Display(Name = nameof(Questions.Q_6_21), ResourceType = typeof(Questions))]
	public int Q_6_21 { get; set; } = 0;
	// Máte v zaměstnání možnost ohřát si jídlo?
	[Display(Name = nameof(Questions.Q_6_22), ResourceType = typeof(Questions))]
	public int Q_6_22 { get; set; } = 0;
	// Vaříte si a jste schopni si většinu jídel připravovat doma?
	[Display(Name = nameof(Questions.Q_6_23), ResourceType = typeof(Questions))]
	public int Q_6_23 { get; set; } = 0;
	// Stravujete se více jak 2x týdně v restauracích? (včetně závodních jídelen)
	[Display(Name = nameof(Questions.Q_6_24), ResourceType = typeof(Questions))]
	public int Q_6_24 { get; set; } = 0;
	// Stravujete se více jak 2 x týdně ve fastfoodu?
	[Display(Name = nameof(Questions.Q_6_25), ResourceType = typeof(Questions))]
	public int Q_6_25 { get; set; } = 0;
	// Konzumujete ve večerních hodinách (min. 1x týdně) nezdravé jídlo? (za nezdravé jídlo ve večerních hodinách můžete považovat obecně nezdravé potraviny, velké množství jídla a také potraviny nebo pokrmy s vysokým obsahem sacharidů nebo příliš velkým množstvím tuku)
	[Display(Name = nameof(Questions.Q_6_26), ResourceType = typeof(Questions))]
	public int Q_6_26 { get; set; } = 0;
	// Chodíte rádi pěšky? (jít 15 minut místo použití dopravy mi nedělá problém)
	[Display(Name = nameof(Questions.Q_6_27), ResourceType = typeof(Questions))]
	public int Q_6_27 { get; set; } = 0;
	// Jak často a jakou pohybovou aktivitu máte?
	[Display(Name = nameof(Questions.Q_6_28), ResourceType = typeof(Questions))]
	public string? Q_6_28 { get; set; }
	// Jaké pohybové aktivity preferujete?  (např. běh, cyklistika, plavání, vyberte preferované pohybové aktivity i v případě,  že v současné době aktivně nesportujete)
	[Display(Name = nameof(Questions.Q_6_29), ResourceType = typeof(Questions))]
	public string? Q_6_29 { get; set; }
	// Trpíte omezenou hybností kloubů či bolestí kloubů, která Vás limituje v pohybové aktivitě?
	[Display(Name = nameof(Questions.Q_6_30), ResourceType = typeof(Questions))]
	public int Q_6_30 { get; set; } = 0;
	// Jste často ve stresu?
	[Display(Name = nameof(Questions.Q_6_31), ResourceType = typeof(Questions))]
	public int Q_6_31 { get; set; } = 0;
	// Pokud jste často ve stresu, co je jeho hlavní příčinou?
	// ZAMĚSTNÁNÍ ; FINANCE; VZTAHY ; VZHLED ; JINÉ  
	[Display(Name = nameof(Questions.Q_6_32), ResourceType = typeof(Questions))]
	public int Q_6_32 { get; set; } = 0;
	// Zkoušeli jste už různé diety a bez stálého výsledku nebo jste i přibrali?
	[Display(Name = nameof(Questions.Q_6_33), ResourceType = typeof(Questions))]
	public int Q_6_33 { get; set; } = 0;
	// Pokud jste zkoušeli diety, vypište, prosím, jaké a s jakým výsledkem: (např. ketodieta mi pomohla zhubnou 5 kg, ale po měsíci byly kg zpátky)
	[Display(Name = nameof(Questions.Q_6_34), ResourceType = typeof(Questions))]
	public string? Q_6_34 { get; set; }

	// Chcete mít v jídelníčku snídani?
	[Display(Name = nameof(Questions.Q_6_35), ResourceType = typeof(Questions))]
	public int Q_6_35 { get; set; } = 0;
	// Chcete mít v jídelníčku dopolední svačinu?
	[Display(Name = nameof(Questions.Q_6_36), ResourceType = typeof(Questions))]
	public int Q_6_36 { get; set; } = 0;
	// Chcete mít v jídelníčku oběd?
	[Display(Name = nameof(Questions.Q_6_37), ResourceType = typeof(Questions))]
	public int Q_6_37 { get; set; } = 0;
	// Chcete mít v jídelníčku odpolední svačinu?
	[Display(Name = nameof(Questions.Q_6_38), ResourceType = typeof(Questions))]
	public int Q_6_38 { get; set; } = 0;
	// Chcete mít v jídelníčku večeři?
	[Display(Name = nameof(Questions.Q_6_39), ResourceType = typeof(Questions))]
	public int Q_6_39 { get; set; } = 0;
	// Chcete mít v jídelníčku druhou večeři?
	[Display(Name = nameof(Questions.Q_6_40), ResourceType = typeof(Questions))]
	public int Q_6_40 { get; set; } = 0;


	// Na jakém místě se Vám začíná ukládat tuk jako první?
	// STEHNA A HÝŽDĚ; SOUMĚRNĚ;BŘICHO 
	[Display(Name = nameof(Questions.Q_7_1), ResourceType = typeof(Questions))]
	public int Q_7_1 { get; set; } = 0;
	// Na kterých místech hubnete jako první?
	// OBLIČEJ A PRSA;SOUMĚRNĚ;HÝŽDĚ A NOHY;NEVÍM
	[Display(Name = nameof(Questions.Q_7_2), ResourceType = typeof(Questions))]
	public int Q_7_2 { get; set; } = 0;
	// Trápí Vás celulitida na hýždích a nohou?
	// ANO, VIDITELNÁ;ANO, PŘI ZMÁČKNUTÍ KŮŽE;NE
	[Display(Name = nameof(Questions.Q_7_3), ResourceType = typeof(Questions))]
	public int Q_7_3 { get; set; } = 0;
	// Máte křeče dolních končetin?
	// ANO; ANO POUZE U SPORTU;NE
	[Display(Name = nameof(Questions.Q_7_4), ResourceType = typeof(Questions))]
	public int Q_7_4 { get; set; } = 0;
	// Máte bolesti dolních končetin?(stupnice: 1 = vůbec–10 = často)
	// 1;2;3;4;5;6;7;8;9;10
	[Display(Name = nameof(Questions.Q_7_5), ResourceType = typeof(Questions))]
	public int Q_7_5 { get; set; } = 0;
	// Pokud jste míru bolesti končetin označil/a mezi 6 - 10, vyberte, prosím, kdy k bolesti dochází.
	// NIKDE;PŘI CHŮZI;PŘI SEZENÍ;NEDOKÁŽU URČIT 
	[Display(Name = nameof(Questions.Q_7_6), ResourceType = typeof(Questions))]
	public int Q_7_6 { get; set; } = 0;
	// Máte otoky dolních končetin?
	// ČASTO;OBČAS;NEMÁM
	[Display(Name = nameof(Questions.Q_7_7), ResourceType = typeof(Questions))]
	public int Q_7_7 { get; set; } = 0;
	// Máte křečové žíly?
	// ANO VELKÉ A VIDITELNÉ;ANO MIKROVARIXY (METLIČKY);NE
	[Display(Name = nameof(Questions.Q_7_8), ResourceType = typeof(Questions))]
	public int Q_7_8 { get; set; } = 0;
	// Prodělal/a jste v minulosti zánět křečových žil?
	[Display(Name = nameof(Questions.Q_7_9), ResourceType = typeof(Questions))]
	public int Q_7_9 { get; set; } = 0;
	// Trpíte hypermobilitou kloubů? (zvýšený rozsah kloubní pohyblivosti)
	[Display(Name = nameof(Questions.Q_7_10), ResourceType = typeof(Questions))]
	public int Q_7_10 { get; set; } = 0;
	// Chcete sdělit svému specialistovi ještě něco co v dotazníku nezaznělo a ovlivní to váš výživový plán?
	[Display(Name = nameof(Questions.Q_7_11), ResourceType = typeof(Questions))]
	public string? Q_7_11 { get; set; }


	[NotMapped]
	public List<int> Q_5_8_FoodIds { get; set; } = new List<int>();

	[NotMapped]
	public List<int> Q_5_9_FoodIds { get; set; } = new List<int>();

	[NotMapped]
	public List<int> Q_5_18_FoodIds { get; set; } = new List<int>();

    public virtual ICollection<ClientQuestionnaireResult> Results { get; set; } = new List<ClientQuestionnaireResult>();


	public void MapFoodIds()
	{
		if (!string.IsNullOrEmpty(Q_5_8))
		{
			Q_5_8_FoodIds = Q_5_8.Split(';').Select(int.Parse).ToList();
		}

		if (!string.IsNullOrEmpty(Q_5_9))
		{
			Q_5_9_FoodIds = Q_5_9.Split(';').Select(int.Parse).ToList();
		}

		if (!string.IsNullOrEmpty(Q_5_18))
		{
			Q_5_18_FoodIds = Q_5_18.Split(';').Select(int.Parse).ToList();
		}
	}

	public void MapFoodStrings()
	{
		Q_5_8 = string.Join(";", Q_5_8_FoodIds);
		Q_5_9 = string.Join(";", Q_5_9_FoodIds);
		Q_5_18 = string.Join(";", Q_5_18_FoodIds);
	}
}
