# Keycloak

---
# Présentation générale

---
# Au programme

- Introduction générale
- IAM et principales fonctionnalités 
- Les protocoles utilisés par Keycloak (OAuth 2.0, Open ID Connect)

+++

# Discover & Practice !

- Docker + Keycloak
- Exploration de la console administrateur de Keycloak
- Composition token JWT
- Sécurisation avec Keycloak d'une API fournie en ajoutant une authentification
- Web app qui permet à l'utilisateur de se connecter et d'accéder à la ressource protégée

---

# Avant de commencer (prérequis) ...

- Téléchargement et installation de Docker for Windows et Oracle VirtualBox
- Téléchargement de l'image docker Keycloak
- Clone de mon repo git : https://github.com/GerbenCdg/Keycloak4Fun

+++

## Téléchargement et installation de Docker for Windows et Oracle VirtualBox

https://docs.docker.com/docker-for-windows/install/

+++

## Téléchargement de l'image docker Keycloak

````
docker pull jboss/keycloak
````

+++

## Clone de mon repo git
-Le repository suivant contient un projet ASP.NET avec une API qui sera utilisée pour la dernière partie 
qui portera sur la sécurisation d'une API avec Keycloak.

https://github.com/GerbenCdg/Keycloak4Fun

---

# IAM (Identity and Access Management)
## What is IAM ? 

- Gestion d'identité
    - Comptes utilisateurs
    - Droits utilisateurs

- Gestion des accès
    - Contrôler l'accès à des resources


+++

# IAM (Identity and Access Management)
## Besoins d'IAM

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

# Keycloak
## Introduction 

- Développé depuis 2014 en open-source par JBoss, une filiale de Redhat
- Solution d'IAM très complète
- Facilite la vie aux développeurs

+++

# Keycloak 
## Avantages
- Options "click to enable"
- Très paramètrable
- Pas besoin d'écrire du code
- Console administrateur très complète
- Admin API

---

# Passons à la pratique !
- La doc ! 
<br> https://www.keycloak.org/docs/latest/getting_started/index.html

+++
# Pratique
## Démarrage de Keycloak

```
docker image ls
docker run -p 8080:8080 <imageID>
```

- Le démarrage peut durer quelques minutes sur un docker émulé sous Windows (4 minutes sur mon PC)

+++
# Pratique

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
# Pratique
## Explorons la console administrateur

- Connectez-vous sur localhost:8080/auth/admin avec vos identifiants
- Je vous laisse regarder par vous-mêmes pendant 2 min ;)

+++
# Pratique
## Ajout d'un realm

- En direct depuis mon pc
- C'est quoi un realm ?
- Config d'un realm

+++
# Pratique
## Ajout d'un utilisateur

- En direct depuis mon pc

+++
# Pratique
## Connexion en tant que cet utilisateur

[localhost:8080/auth/realms/coding4fun/account]()
                    
+++
# Pratique
## Ajout d'un client

- En direct depuis mon pc

---

#Les protocoles
##OpenID Connect et OAuth 2.0
- Fonctionnement
- TODO ajouter schémas

+++
#Les protocoles
## Les différents Authentication Flows
TODO expliquer, inclure schémas

---

#Sécurisation d'une Web API

Clone de mon repo git qui contient une API déjà prête à être utilisée ;)
<br> https://github.com/GerbenCdg/Keycloak4Fun

+++
#Sécurisation d'une Web API
## Paramétrage du client

TODO à définir. Donner paramètres à appliquer

+++
#Sécurisation d'une Web API
## Que contient un token JWT ?
 
Token JWT : 
jwt.io






















