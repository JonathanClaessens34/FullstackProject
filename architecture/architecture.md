# Architecture

  

Voor de communicatie wordt er gebruikgemaakt van Rabbit MQ als Event bus. Als frontend hebben we 2 punten.
Een website voor de brouwer om menukaarten te maken en pop-up bars toe te voegen en een Maui applicatie voor de klanten om te bestellen. De klanten kunnen zich inloggen met de identity server die geschreven wordt in c# net zo als de "order management" microservice. Deze microservices staan in direct contact met de Maui app frontend.
De pop-up bar en menukaart microservices worden in java geschreven. Daar wordt er gebruik gemaakt van een configuratie en discovery microservice voor makkelijke uitbreidbaarheid. Dezen worden via een web gateway verbonden met de angular frontend.

  

![Architectuur schema](https://github.com/pxlit-projects/project---pxl-pop-up-cocktailbars-fs_11/blob/main/architecture/Architectuurschema_v1.1.png)

  