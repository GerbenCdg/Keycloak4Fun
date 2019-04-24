# Keycloak

---
# Présentation générale

---
## Au programme

- Introduction générale
- IAM et principales fonctionnalités 
- Les protocoles utilisés par Keycloak (OAuth 2.0, Open ID Connect)

+++

## Discover & Practice !

- Docker + Keycloak
- Exploration de la console administrateur de Keycloak
- Composition token JWT
- Sécurisation avec Keycloak d'une API fournie en ajoutant une authentification
- Gestion de rôles utilisateur avec Keycloak

---

## Avant de commencer (prérequis) ...

PC perso ou Michelin avec droits admin 
<br> Programmes (liens de téléchargement sur les slides en-dessous)
- Visual Studio ou VS Code
- Docker for Windows et Oracle VirtualBox
- Téléchargement de l'image docker Keycloak
- .NET CORE 2.2
- Clone de mon repo git : https://github.com/GerbenCdg/Keycloak4Fun
- PostMan pour tester des requêtes vers la Web API

+++
## Téléchargement et installation Visual Studio

[Téléchargement Visual Studio Code](https://visualstudio.microsoft.com/thank-you-downloading-visual-studio/?sku=Community&rel=16) 

+++
## Téléchargement et installation de Docker for Windows et Oracle VirtualBox

https://docs.docker.com/docker-for-windows/install/

<br> Configuration Oracle Virtual Box pour mapping de ports
<br> https://gitpitch.com/Pierre48/Coding4Fun.Net


+++
## Téléchargement de l'image docker Keycloak

````
docker pull jboss/keycloak
````

+++

## Téléchargement .NET CORE 2.2

https://dotnet.microsoft.com/download/dotnet-core/2.2

https://www.microsoft.com/net/learn/get-started-with-dotnet-tutorial

+++
## Clone de mon repo git
- Le repository suivant contient un projet ASP.NET avec une API qui sera utilisée pour la dernière partie 
qui portera sur la sécurisation d'une API avec Keycloak.

https://github.com/GerbenCdg/Keycloak4Fun

---

## IAM (Identity and Access Management)
### What is IAM ? 

- Gestion d'identité
    - Comptes utilisateurs
    - Droits utilisateurs

- Gestion des accès
    - Contrôler l'accès à des resources


+++

## IAM (Identity and Access Management)
### Besoins d'IAM

- Centraliser les comptes utilisateurs 
- Basé sur des protocoles standards (SAML, OpenID Connect + OAuth 2.0)
- Sécurité
- Utilisation de SSO (Single Sign-On : Authentification unique)

Note : 
- Eviter la duplication des données entre différents comptes, facilite la maintenance
- Protocoles standard pour avoir une solution connue, permet théoriquement plus facilement de changer d'un provider d'IAM à un autre
; permet de passer au point suivant :
- Sécurité indispensable (données utilisateurs en jeu), éviter le vol de données utilisateurs
- SSO : évite de se connecter 5 fois au même compte

---

## Keycloak
### Introduction 

- Développé depuis 2014 en open-source par JBoss, une filiale de Redhat
- Solution d'IAM très complète
- Facilite la vie aux développeurs

+++

## Keycloak 
### Avantages
- Options "click to enable"
- Très paramètrable
- Pas besoin d'écrire du code
- Console administrateur très complète
- Agrégation de comptes avec LDAP
- Ajout de providers (Google, Facebook, Twitter...)  
- Admin API

---

## Passons à la pratique !
- La doc ! 
<br> https://www.keycloak.org/docs/latest/getting_started/index.html

+++
## Pratique
### Démarrage de Keycloak

- Pensez à mapper les ports que vous voulez exposer dans Virtual Box
<br>

```
docker image ls
docker run -p 8080:8080 <imageID>
```

- Le démarrage peut durer quelques minutes sur un docker émulé sous Windows (jusqu'à 4 minutes sur mon PC)

+++
## Pratique
### Accès à la console admin

- Ajoutez un utilisateur admin pour débloquer l'accès à la console administrateur
- Prenez la main de la console bash du container docker démarré pour exécuter le script qui permet d'ajouter un nouvel utilisateur
- Redémarrez pour appliquer les changements
```
docker exec -it loving_taussig /bin/bash
cd keycloak/bin
./add-user-keycloak.sh -u admin
motdepasseadmin
exit
docker restart loving_taussig
```

+++
## Pratique
### Explorons la console administrateur

- Connectez-vous sur localhost:8080/auth/admin avec vos identifiants
- Je vous laisse regarder par vous-mêmes pendant 2 min ;)

+++
## Pratique
### Ajout d'un realm

- En direct depuis mon pc
- C'est quoi un realm ?
- Config d'un realm

+++
## Pratique
### Ajout d'un utilisateur

- En direct depuis mon pc

+++
## Pratique
### Connexion en tant que cet utilisateur

[localhost:8080/auth/realms/coding4fun/account]()

---

## Les protocoles
### OpenID Connect et OAuth 2.0
- Fonctionnement
- TODO ajouter schémas

+++
## Les protocoles
### Les différents Authentication Flows
TODO expliquer, inclure schémas

---

# Pratique
## Partie 1 : Sécurisation d'une Web API

+++
## Sécurisation d'une Web API

Clone de mon repo git qui contient une API déjà prête à être utilisée ;)
<br> https://github.com/GerbenCdg/Keycloak4Fun

+++
## Sécurisation d'une Web API
### Découverte du projet
- Rappel : [.NET CORE 2.2 Requis](https://dotnet.microsoft.com/download/dotnet-core/2.2)
- Familiarisation avec le projet C4FWebApi 
- Nous allons mettre en place la gestion de l'authentification

+++
## Sécurisation d'une Web API
### Découverte du projet

- Startup.cs : Configuration des services d'authentification, 
- Controllers/ : Définition des différents endpoints de notre API
- [Imports pour les utilisateurs de VSCode](https://gist.github.com/GerbenCdg/88b6c27aa8b6ed6819530117a4f30d09)

+++
## Sécurisation d'une Web API
### Configuration de Startup.cs

- On souhaite sécuriser l'accès à notre API. On peut faire cela grâce [au tag Authorize](https://docs.microsoft.com/en-us/aspnet/core/security/authorization/simple?view=aspnetcore-2.2)
- Le tag peut se mettre devant le controller (voir exemple), soit devant une méthode. 

```
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    { ... }
```

+++
## Sécurisation d'une Web API
### Configuration de Startup.cs

- Ajouter app.UseAuthentication
```
public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
    ...
    app.UseAuthentication();
}
```

+++
## Sécurisation d'une Web API
### Création d'un client

- Retour à la [console admin](http://localhost:8080/auth/admin/)
- Ajout d'un client que nous utiliserons pour notre WebAPI

+++
## Sécurisation d'une Web API
### Création et configuration d'un client

- En live depuis mon PC
- Client Protocol : OpenID Connect
- Access Type : Confidential
- On reviendra plus tard sur le choix du flow (manière d'obtention de notre access token)

+++
## Sécurisation d'une Web API
### Configuration de Startup.cs

- Définition des services d'authentification : Utilisation des schemas JWT. 
- A ajouter dans la méthode ConfigureServices : 
```
services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false; // On n'utilise pas le HTTPS
    options.Authority = "http://localhost:8080/auth/realms/coding4fun"; //Authorité OpenID Connect
    options.Audience = "webapi"; // L'ID de notre client OpenID Connect
});
```

## Sécurisation d'une Web API
### OpenID Connect Discovery Service

- Comment notre WebAPI connaît les liens de Keycloak grâce à la configuration que nous venons de lui renseigner ?
- Grâce au [well-known/openid-configuration !](http://localhost:8080/auth/realms/coding4fun/.well-known/openid-configuration)

+++
## Sécurisation d'une Web API
### Configuration de Startup.cs

- Validation du token

```
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
        ValidateIssuer = true,
        ValidIssuer = "http://localhost:8080/auth/realms/coding4fun",
        ValidateLifetime = true
    };
}
```
+++
## Sécurisation d'une Web API
### Configuration de Startup.cs

- Relancez le projet. Vous devriez constater une 401.


+++
## Sécurisation d'une Web API
### Test de requête dans Postman

- Tentons d'effectuer une première requête GET sur notre API
- Tab "Authorization" -> "Access Token" -> "Get new Access Token"
- Quel flow choisir ? voir diapo suivante.

+++
## Sécurisation d'une Web API
### Les différents flows d'autorisation

- Il existe une multitude de flows d'authentification. Son choix dépendra de plusieurs critères.
https://cdn2.auth0.com/docs/media/articles/api-auth/oauth2-grants-flow.png
TODO embed image

+++
## Sécurisation d'une Web API
### Retour à Postman

- Utilisation du flow "Client Credentials" dans un premier temps
- "Service accounts enabled" dans Keycloak

+++
## Sécurisation d'une Web API
### Test requête Postman

- 200 OK ? Well done !

+++
## Sécurisation d'une Web API
### Test requête Postman

- 200 OK ? Well done !

+++
## Sécurisation d'une Web API
### Contenu d'un Access Token

- Format JWT
- Explorez le contenu de votre token sur [jwt.io](https://jwt.io/) !

---

# Pratique
## Partie 2 : Gestion de rôles
#### Si vous en arrivez-là, <br>C'est que vous êtes sacrément doué !

+++
## Gestion de rôles
### Retournons sur Keycloak

- Ajout d'un role "administrator" 
- Assignation de ce rôle à un utilisateur (autre que celui qui accède à la console admin !)
- Ajout d'un mapper de rôles à notre client

+++
## Gestion de rôles
### ValuesController.cs

- Ajout de la policy "Administrator"
```
    [Authorize(Policy =  "Administrator")]
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
```

+++
## Gestion de rôles
### Startup.cs

- Nous devons faire le lien entre notre role Keycloak et la Policy
- Dans la méthode ConfigureServices() 
```
services.AddAuthorization(options =>
{
    options.AddPolicy("Administrator", policy => policy.RequireRole("administrator"));
});
```

+++
## Gestion de rôles
### Postman, encore toi ! 
- Cette fois, nous allons récupérer un Access Token en tant que l'utilisateur qui possède le rôle
- Nous utiliserons le "Password Credentials Grant" dans Postman
- Activer "Direct Access Grant" dans la config du client Keycloak

## Gestion de rôles
#### Ca fonctionne ? Trop fort ;)
- Regardons à nouveau le contenu de notre Access Token
[jwt.io](https://jwt.io) :)

---
# Fin

+++
# Fin
### Félicitations !

####Vous êtes maintenant un expert certifié Keycloak et OpenID Connect




