# P6_NexaWorks

NexaWorks développe différents produits logiciels. 

Chaque produit dispose de plusieurs versions, et chacune d’elles est compatible avec un ou plusieurs systèmes d’exploitation.

Notamment : Windows, MacOS, Linux, Android, et iOS. 

L’entreprise doit assurer le suivi des problèmes qui surviennent pour chaque version et chaque système d’exploitation. 
 
Elle ne dispose actuellement d’aucune application pour ce faire. 

L'objectif de ce projet est de concevoir et de créer une base de données relationnelle capable de stocker 

et de suivre tous les problèmes qui surviennent avec les produits au cours du cycle de vie de chaque version,

ainsi que la résolution de chacun de ces problèmes.

Le Modèle Conceptuel de Données est le suivant

```mermaid
erDiagram

    PRODUCT {
        int Id
        string Name
    }

    VERSION {
        int Id
        string Number
        datetime DateRelease
        int ProductId
    }

    OS {
        int Id
        string Name
    }

    VERSION_OS {
        int Id
        int VersionId
        int OSId
    }

    ISSUE {
        int Id
        string Description
        datetime DateCreation
        string Resolution
        datetime DateResolution
        string Statut
        int VersionOSId
    }

    PRODUCT -- VERSION : "has versions"
    VERSION -- VERSION_OS : "supports"
    OS -- VERSION_OS : "runs on"
    VERSION_OS -- ISSUE : "has issues"
```

