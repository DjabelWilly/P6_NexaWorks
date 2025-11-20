<Query Kind="Statements">
  <Connection>
    <ID>b2fcf9dd-c6cd-42a0-94b2-aa4b1fa60e7b</ID>
    <NamingServiceVersion>3</NamingServiceVersion>
    <Persist>true</Persist>
    <Driver Assembly="EF7Driver" PublicKeyToken="469b5aa5a4331a8c">EF7Driver.StaticDriver</Driver>
    <CustomAssemblyPathEncoded>&lt;UserProfile&gt;\source\repos\P6_NexaWorks\code\bin\Debug\net8.0\P6_NexaWorks.dll</CustomAssemblyPathEncoded>
    <CustomTypeName>P6_NexaWorks.Data.AppDbContext</CustomTypeName>
    <CustomCxString>Server=.;Database=P6_NexaWorks;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True</CustomCxString>
    <DriverData>
      <UseDbContextOptions>true</UseDbContextOptions>
      <EFProvider>Microsoft.EntityFrameworkCore.SqlServer</EFProvider>
    </DriverData>
  </Connection>
</Query>


//SELECTIONNEZ UNE REQUETE CI-DESSOUS PUIS EXECUTER AVEC F5.


//Obtenir tous les problèmes en cours (tous produits, toutes versions)
Issues
    .Where(i => i.Statut == "En cours")
    .Select(i => new
    {
        i.Id,
        i.Description,
        Product = i.VersionOS.Version.Product.Name,
        Version = i.VersionOS.Version.Number,
        OS = i.VersionOS.OS.Name,
        i.DateCreation
    })
    .ToList()
	.Dump();
	
	
//Obtenir tous les problèmes en cours pour un produit (toutes les versions)
Issues
    .Where(i => i.Statut == "En cours")
    .Where(i => i.VersionOS.Version.Product.Name == "Maître des Investissements")
    .Select(i => new
    {
        i.Id,
        i.Description,
        Product = i.VersionOS.Version.Product.Name,
        Version = i.VersionOS.Version.Number,
        OS = i.VersionOS.OS.Name,
        i.DateCreation
    })
    .ToList()
	.Dump();
	
	
//Obtenir tous les problèmes en cours pour un produit (une seule version)
	Issues
    .Where( i => 
	     i.Statut == "En cours" &&
	     i.VersionOS.Version.Product.Name == "Maître des Investissements" &&
	     i.VersionOS.Version.Number == "1.0"
	     )
       .Select(i => new
        {
        i.Id,
        i.Description,
        Product = i.VersionOS.Version.Product.Name,
        Version = i.VersionOS.Version.Number,
        OS = i.VersionOS.OS.Name,
        i.DateCreation
       })
    .ToList()
	.Dump();
	
	
//Obtenir tous les problèmes rencontrés au cours d’une période donnée pour un produit (une seule version)
	Issues
    .Where(	i => 
	      i.Statut == "En cours" &&
	      i.VersionOS.Version.Product.Name == "Maître des Investissements" &&
	      i.VersionOS.Version.Number == "1.0" &&
	      i.DateCreation >= new DateTime(2024, 04, 01) &&
	      i.DateCreation <= new DateTime(2024, 04, 30)
	    )
    .Select(i => new
    {
        i.Id,
        i.Description,
        Product = i.VersionOS.Version.Product.Name,
        Version = i.VersionOS.Version.Number,
        OS = i.VersionOS.OS.Name,
        i.DateCreation
    })
    .ToList()
	.Dump();
	
	
//Obtenir tous les problèmes en cours contenant une liste de mots-clés
var keywords = new[] { "portefeuille", "encrypté", "PDF" };

Issues
    .Where(i => i.Statut == "En cours" &&
                keywords.Any(k => i.Description.Contains(k)))
    .Select(i => new
    {
        i.Id,
        i.Description,
        Product = i.VersionOS.Version.Product.Name,
        Version = i.VersionOS.Version.Number,
        OS = i.VersionOS.OS.Name,
        i.DateCreation
    })
    .ToList()
	.Dump();
	
	
	
	
//Obtenir tous les problèmes en cours contenant une liste de mots-clés pour une version	 
var keyWords = new[] { "portefeuille", "encrypté", "PDF" };

Issues
    .Where(i => i.Statut == "En cours" &&
	 	   i.VersionOS.Version.Number == "1.1" &&
           keyWords.Any(k => i.Description.Contains(k)))
    .Select(i => new
    {
        i.Id,
        i.Description,
        Product = i.VersionOS.Version.Product.Name,
        Version = i.VersionOS.Version.Number,
        OS = i.VersionOS.OS.Name,
        i.DateCreation
    })
    .ToList()
	.Dump();
