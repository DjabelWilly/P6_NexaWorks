using Microsoft.EntityFrameworkCore;
using P6_NexaWorks.Models.Entities;
using Version = P6_NexaWorks.Models.Entities.Version;

namespace P6_NexaWorks.Data.Seed
{
    public static class DatabaseSeeder
    {
        public static async Task SeedAsync(AppDbContext db)
        {
            await db.Database.MigrateAsync();

            if (await db.Products.AnyAsync())
                return;

            // -----------------------------
            // PRODUCTS
            // -----------------------------
            var pTrader = new Product { Name = "Trader en Herbe" };
            var pInvest = new Product { Name = "Maître des Investissements" };
            var pTraining = new Product { Name = "Planificateur d’Entraînement" };
            var pAnxiety = new Product { Name = "Planificateur d’Anxiété Sociale" };

            db.Products.AddRange(pTrader, pInvest, pTraining, pAnxiety);
            await db.SaveChangesAsync();

            // -----------------------------
            // OS (normalisés)
            // -----------------------------
            var osAndroid = new OS { Name = "Android" };
            var osWindows = new OS { Name = "Windows" };
            var osWindowsMobile = new OS { Name = "Windows Mobile" };
            var osIOS = new OS { Name = "iOS" };
            var osMac = new OS { Name = "MacOS" };
            var osLinux = new OS { Name = "Linux" };

            db.OSes.AddRange(osAndroid, osWindows, osWindowsMobile, osIOS, osMac, osLinux);
            await db.SaveChangesAsync();

            // -----------------------------
            // VERSIONS
            // -----------------------------
            Version V(string num, Product p, DateTime d) => new Version
            {
                Number = num,
                Product = p,
                DateRelease = d
            };

            // Trader en Herbe
            var vTH_10 = V("1.0", pTrader, new(2024, 1, 1));
            var vTH_11 = V("1.1", pTrader, new(2024, 2, 15));
            var vTH_12 = V("1.2", pTrader, new(2024, 4, 14));
            var vTH_13 = V("1.3", pTrader, new(2024, 5, 25));

            // Maître des Investissements
            var vMI_10 = V("1.0", pInvest, new(2024, 9, 4));
            var vMI_20 = V("2.0", pInvest, new(2024, 3, 3));
            var vMI_21 = V("2.1", pInvest, new(2024, 6, 12));

            // Planificateur d’Entraînement
            var vPT_10 = V("1.0", pTraining, new(2024, 1, 10));
            var vPT_11 = V("1.1", pTraining, new(2024, 2, 8));
            var vPT_20 = V("2.0", pTraining, new(2024, 7, 15));

            // Planificateur d’Anxiété Sociale
            var vPA_10 = V("1.0", pAnxiety, new(2024, 6, 13));
            var vPA_11 = V("1.1", pAnxiety, new(2024, 8, 1));

            db.Versions.AddRange(
                vTH_10, vTH_11, vTH_12, vTH_13,
                vMI_10, vMI_20, vMI_21,
                vPT_10, vPT_11, vPT_20,
                vPA_10, vPA_11
            );
            await db.SaveChangesAsync();

            // -----------------------------
            // 25 VersionOS EXPLICITES (un par ticket)
            // -----------------------------
            VersionOS VO(Version v, OS os) => new VersionOS { Version = v, OS = os };

            var vosList = new List<VersionOS>
            {
                VO(vTH_12, osAndroid),        //1
                VO(vTH_11, osWindows),        //2
                VO(vMI_20, osAndroid),        //3
                VO(vMI_21, osIOS),            //4
                VO(vTH_13, osWindows),        //5
                VO(vPT_11, osAndroid),        //6
                VO(vPT_20, osWindows),        //7
                VO(vPA_11, osIOS),            //8
                VO(vTH_12, osWindowsMobile),  //9
                VO(vMI_10, osIOS),            //10
                VO(vPT_10, osLinux),          //11
                VO(vTH_11, osMac),            //12
                VO(vMI_20, osMac),            //13
                VO(vPT_11, osWindowsMobile),  //14
                VO(vTH_10, osWindows),        //15
                VO(vPA_10, osMac),            //16
                VO(vMI_21, osAndroid),        //17
                VO(vPT_20, osMac),            //18
                VO(vTH_13, osIOS),            //19
                VO(vMI_10, osMac),            //20
                VO(vPT_11, osLinux),          //21
                VO(vTH_12, osIOS),            //22
                VO(vPA_10, osWindows),        //23
                VO(vMI_20, osIOS),            //24
                VO(vPT_20, osWindows)         //25
            };

            db.VersionOSes.AddRange(vosList);
            await db.SaveChangesAsync();

            // -----------------------------
            // ISSUES (25 exactes)
            // -----------------------------
            Issue I(string desc, DateTime dc, string st, VersionOS v,
                    DateTime? dr = null, string? res = null)
                => new Issue
                {
                    Description = desc,
                    DateCreation = dc,
                    Statut = st,
                    DateResolution = dr,
                    Resolution = res,
                    VersionOS = v
                };

            var issues = new List<Issue>
            {
                I("L’application se ferme de manière inattendue lors de la consultation du portefeuille.",
                  new(2024,4,14),"Résolu", vosList[0], new(2024,4,22),
                  "Mise à jour du module graphique."),

                I("Impossible d’enregistrer un nouveau portefeuille, le bouton “Sauvegarder” reste inactif.",
                  new(2024,5,9),"En cours", vosList[1]),

                I("Les notifications d’alerte d’investissement s’affichent en double.",
                  new(2024,3,3),"Résolu", vosList[2], new(2024,3,15),
                  "Doublon détecté dans le module de notification."),

                I("Le mot de passe de l’utilisateur n’est pas encrypté correctement.",
                  new(2024,6,12),"En cours", vosList[3]),

                I("Navbar non responsive.",
                  new(2024,5,25),"Résolu", vosList[4], new(2024,6,4),
                  "Correctif responsive."),

                I("Plantage lors de l’ajout d’une séance avec photo.",
                  new(2024,2,8),"Résolu", vosList[5], new(2024,2,11),
                  "Correction du format d’image non supporté."),

                I("L’historique d’entraînement ne s’affiche plus après mise à jour.",
                  new(2024,7,15),"En cours", vosList[6]),

                I("Certaines données de suivi quotidien ne se synchronisent pas.",
                  new(2024,8,1),"Résolu", vosList[7], new(2024,8,3),
                  "Correction des jetons d’authentification."),

                I("Valeurs incohérentes pour les prix d’achat.",
                  new(2024,4,18),"Résolu", vosList[8], new(2024,4,20),
                  "Erreur de conversion devise corrigée."),

                I("Le bouton “Ajouter un actif” ne répond plus.",
                  new(2024,9,4),"En cours", vosList[9]),

                I("Le suivi hebdomadaire ne se met pas à jour.",
                  new(2024,1,10),"Résolu", vosList[10], new(2024,1,14),
                  "Problème de cache corrigé."),

                I("Impossible d’importer un portefeuille sauvegardé.",
                  new(2024,3,21),"Résolu", vosList[11], new(2024,3,28),
                  "Correction encodage UTF-8."),

                I("Le graphique de performance s’affiche vide.",
                  new(2024,8,7),"En cours", vosList[12]),

                I("Caractères illisibles.",
                  new(2024,10,3),"Résolu", vosList[13], new(2024,10,9),
                  "Mise à jour police + encodage."),

                I("L’export CSV échoue.",
                  new(2024,11,5),"En cours", vosList[14]),

                I("Les notifications quotidiennes ne s’envoient plus.",
                  new(2024,6,13),"Résolu", vosList[15], new(2024,6,17),
                  "Service push réinitialisé + certificat mis à jour."),

                I("Le tableau de bord reste vide après import JSON.",
                  new(2024,5,2),"Résolu", vosList[16], new(2024,5,10),
                  "Correction du format JSON."),

                I("Le programme ne se lance plus après mise à jour.",
                  new(2024,2,10),"En cours", vosList[17]),

                I("Mauvais arrondi sur les gains journaliers.",
                  new(2024,9,25),"Résolu", vosList[18], new(2024,9,30),
                  "Ajustement du format numérique."),

                I("Impossible de valider l’achat d’une action.",
                  new(2024,4,15),"En cours", vosList[19]),

                I("Les notifications de rappel ne s’affichent pas.",
                  new(2024,2,6),"Résolu", vosList[20], new(2024,2,9),
                  "Cron interne redémarré."),

                I("Mauvais affichage du graphique en mode paysage.",
                  new(2024,5,12),"Résolu", vosList[21], new(2024,5,14),
                  "Ajustement responsive."),

                I("Application très lente à l’ouverture.",
                  new(2024,9,29),"En cours", vosList[22]),

                I("Thème sombre rend les textes illisibles.",
                  new(2024,11,1),"Résolu", vosList[23], new(2024,11,5),
                  "Palette haute visibilité."),

                I("Le module d’export PDF ne génère aucun fichier.",
                  new(2024,10,30),"En cours", vosList[24]),
            };

            db.Issues.AddRange(issues);
            await db.SaveChangesAsync();
        }
    }
}
