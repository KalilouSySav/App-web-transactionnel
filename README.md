# Projet : Application de Vente de Produits Écologiques

## Description

Cette application web est développée en C# avec ASP.NET sur Visual Studio. Elle suit les principes de la programmation orientée objet (POO), avec une attention particulière à la modularité et la réutilisabilité du code. Le projet utilise Entity Framework comme framework de persistance, en combinaison avec une couche DAO (Data Access Object) pour gérer les opérations de base de données. Le cycle de vie du projet est géré efficacement, depuis la planification jusqu'à la mise en production, en utilisant Git pour le contrôle de version.

## Fonctionnalités Principales

- **Authentification des utilisateurs** : Système de connexion et d'inscription sécurisé.
- **Gestion de panier** : Ajouter, modifier et supprimer des produits dans le panier.
- **Catalogue de produits** : Affichage dynamique des produits écologiques avec filtres de recherche.
- **Gestion des commandes** : Traitement et suivi des commandes des utilisateurs.
- **Interface utilisateur intuitive** : Design moderne et ergonomique pour une meilleure expérience utilisateur.

## Technologies Utilisées

- **Langage** : C#
- **Framework Web** : ASP.NET
- **IDE** : Visual Studio
- **Base de données** : SQL Server avec Entity Framework
- **Gestion des versions** : Git
- **Framework Front-end** : Bootstrap, HTML5, CSS3, JavaScript

## Architecture du Projet

L'application suit une architecture en couches pour séparer les différentes responsabilités :

1. **Couche de Présentation (UI)** : Gère l'affichage et les interactions utilisateur.
2. **Couche de Logique Métier (Business Logic)** : Contient la logique de gestion des données et des règles métiers.
3. **Couche DAO (Data Access Object)** : Fournit une interface pour la communication avec la base de données via Entity Framework.
4. **Couche de Persistance** : Gère les entités et les connexions à la base de données.

## Installation et Configuration

### Prérequis

- Visual Studio 2022 ou plus récent
- .NET SDK 6.0 ou plus récent
- SQL Server (LocalDB ou une autre version)
- Git
