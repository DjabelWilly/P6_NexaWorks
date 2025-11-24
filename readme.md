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
classDiagram

    class Product {
        int Id
        string Name
    }

    class Version {
        int Id
        string Number
        DateTme DateRelease
        int ProductId  _FK_
    }

    class OS {
        int Id
        string Name
    }

    class VersionOS {
        int Id
        int VersionId _FK_
        int OSId _FK_
    }

    class Issue {
        int Id
        string Description
        DateTme DateCreation
        string Resolution
        DateTme DateResolution
        string Statut
        int VersionOSId _FK_
    }

    Product "1" --> "N" Version
    Version "1" --> "N" VersionOS
    OS "1" --> "N" VersionOS
    VersionOS "1" --> "N" Issue
```

